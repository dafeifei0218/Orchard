﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Module = Autofac.Module;

namespace Orchard.Logging {
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggingModule : Module {
        /// <summary>
        /// 日志缓存
        /// </summary>
        private readonly ConcurrentDictionary<string, ILogger> _loggerCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoggingModule() {
            _loggerCache = new ConcurrentDictionary<string, ILogger>();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="moduleBuilder"></param>
        protected override void Load(ContainerBuilder moduleBuilder) {
            // by default, use Orchard's logger that delegates to Castle's logger factory
            //默认使用委托Orchard的日志记录器到Castle的日志记录器工厂
            moduleBuilder.RegisterType<CastleLoggerFactory>().As<ILoggerFactory>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrchardLog4netFactory>().As<Castle.Core.Logging.ILoggerFactory>().InstancePerLifetimeScope();

            // call CreateLogger in response to the request for an ILogger implementation
            //响应一个ILogger日志实现，请求CreateLogger
            moduleBuilder.Register(CreateLogger).As<ILogger>().InstancePerDependency();
        }

        /// <summary>
        /// 附加组件注册
        /// </summary>
        /// <param name="componentRegistry">组件注册</param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration) {
            var implementationType = registration.Activator.LimitType;

            // build an array of actions on this type to assign loggers to member properties
            //建造
            var injectors = BuildLoggerInjectors(implementationType).ToArray();

            // if there are no logger properties, there's no reason to hook the activated event
            //
            if (!injectors.Any())
                return;

            // otherwise, whan an instance of this component is activated, inject the loggers on the instance
            //
            registration.Activated += (s, e) => {
                foreach (var injector in injectors)
                    injector(e.Context, e.Instance);
            };
        }

        /// <summary>
        /// 建造日志注入器
        /// </summary>
        /// <param name="componentType">组件类型</param>
        /// <returns></returns>
        private IEnumerable<Action<IComponentContext, object>> BuildLoggerInjectors(Type componentType) {
            // Look for settable properties of type "ILogger" 
            var loggerProperties = componentType
                .GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new {
                    PropertyInfo = p,
                    p.PropertyType,
                    IndexParameters = p.GetIndexParameters(),
                    Accessors = p.GetAccessors(false)
                })
                .Where(x => x.PropertyType == typeof(ILogger)) // must be a logger
                .Where(x => x.IndexParameters.Count() == 0) // must not be an indexer
                .Where(x => x.Accessors.Length != 1 || x.Accessors[0].ReturnType == typeof(void)); //must have get/set, or only set

            // Return an array of actions that resolve a logger and assign the property
            foreach (var entry in loggerProperties) {
                var propertyInfo = entry.PropertyInfo;

                yield return (ctx, instance) => {
                    string component = componentType.ToString();
                    if (component != instance.GetType().ToString()) {
                        return;
                    }
                    var logger = _loggerCache.GetOrAdd(component, key => ctx.Resolve<ILogger>(new TypedParameter(typeof(Type), componentType)));
                    propertyInfo.SetValue(instance, logger, null);
                };
            }
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="context">组件上下文</param>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        private static ILogger CreateLogger(IComponentContext context, IEnumerable<Parameter> parameters) {
            // return an ILogger in response to Resolve<ILogger>(componentTypeParameter)
            //返回日志响应
            var loggerFactory = context.Resolve<ILoggerFactory>();
            var containingType = parameters.TypedAs<Type>();
            return loggerFactory.CreateLogger(containingType);
        }
    }
}