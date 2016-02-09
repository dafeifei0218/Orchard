using System;
using System.Linq;
using Autofac;

namespace Orchard.Caching
{
    /// <summary>
    /// 缓存模块
    /// </summary>
    public class CacheModule : Module
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="builder">容器建造者</param>
        protected override void Load(ContainerBuilder builder)
        {
            //将DefaultCacheManager注册为ICacheManager并且是瞬态的。
            builder.RegisterType<DefaultCacheManager>()
                .As<ICacheManager>()
                .InstancePerDependency();
        }

        /// <summary>
        /// 附件组件注册
        /// </summary>
        /// <param name="componentRegistry">组件注册</param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(Autofac.Core.IComponentRegistry componentRegistry, Autofac.Core.IComponentRegistration registration)
        {
            var needsCacheManager = registration.Activator.LimitType
                .GetConstructors()
                .Any(x => x.GetParameters()
                    .Any(xx => xx.ParameterType == typeof(ICacheManager)));

            if (needsCacheManager)
            {
                registration.Preparing += (sender, e) =>
                {
                    var parameter = new TypedParameter(
                        typeof(ICacheManager),
                        e.Context.Resolve<ICacheManager>(new TypedParameter(typeof(Type), registration.Activator.LimitType)));
                    e.Parameters = e.Parameters.Concat(new[] { parameter });
                };
            }
        }
    }
}
