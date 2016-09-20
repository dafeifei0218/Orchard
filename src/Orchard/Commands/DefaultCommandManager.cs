using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Metadata;
using Orchard.Localization;
using Orchard.Logging;

namespace Orchard.Commands {
    /// <summary>
    /// 默认命令处理管理
    /// </summary>
    public class DefaultCommandManager : ICommandManager {
        private readonly IEnumerable<Meta<Func<ICommandHandler>>> _handlers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlers"></param>
        public DefaultCommandManager(IEnumerable<Meta<Func<ICommandHandler>>> handlers) {
            _handlers = handlers;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public Localizer T { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameters">命令参数</param>
        public void Execute(CommandParameters parameters) {
            var matches = MatchCommands(parameters);

            if (matches.Count() == 1) {
                var match = matches.Single();
                match.CommandHandlerFactory().Execute(match.Context);
            }
            else {
                var commandMatch = string.Join(" ", parameters.Arguments.ToArray());
                var commandList = string.Join(",", GetCommandDescriptors().Select(d => d.Name).ToArray());
                if (matches.Any()) {
                    throw new OrchardCoreException(T("Multiple commands found matching arguments \"{0}\". Commands available: {1}.",
                                                             commandMatch, commandList));
                }
                throw new OrchardCoreException(T("No command found matching arguments \"{0}\". Commands available: {1}.",
                                                 commandMatch, commandList));
            }
        }

        /// <summary>
        /// 获取命令描述集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommandDescriptor> GetCommandDescriptors() {
            return _handlers.SelectMany(h => GetDescriptor(h.Metadata).Commands);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IEnumerable<Match> MatchCommands(CommandParameters parameters) {
            // Command names are matched with as many arguments as possible, in decreasing order
            foreach (var argCount in Enumerable.Range(1, parameters.Arguments.Count()).Reverse()) {
                int count = argCount;
                var matches = _handlers.SelectMany(h => MatchCommands(parameters, count, GetDescriptor(h.Metadata), h.Value)).ToList();
                if (matches.Any())
                    return matches;
            }

            return Enumerable.Empty<Match>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="argCount"></param>
        /// <param name="descriptor"></param>
        /// <param name="handlerFactory"></param>
        /// <returns></returns>
        private static IEnumerable<Match> MatchCommands(CommandParameters parameters, int argCount, CommandHandlerDescriptor descriptor, Func<ICommandHandler> handlerFactory) {
            foreach (var commandDescriptor in descriptor.Commands) {
                var names = commandDescriptor.Name.Split(' ');
                if (!parameters.Arguments.Take(argCount).SequenceEqual(names, StringComparer.OrdinalIgnoreCase)) {
                    // leading arguments not equal to command name
                    continue;
                }

                yield return new Match {
                    Context = new CommandContext {
                        Arguments = parameters.Arguments.Skip(names.Count()),
                        Command = string.Join(" ", names),
                        CommandDescriptor = commandDescriptor,
                        Input = parameters.Input,
                        Output = parameters.Output,
                        Switches = parameters.Switches,
                    },
                    CommandHandlerFactory = handlerFactory
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        private static CommandHandlerDescriptor GetDescriptor(IDictionary<string, object> metadata) {
            return ((CommandHandlerDescriptor)metadata[typeof(CommandHandlerDescriptor).FullName]);
        }

        /// <summary>
        /// 
        /// </summary>
        private class Match {
            /// <summary>
            /// 命令上下文
            /// </summary>
            public CommandContext Context { get; set; }
            /// <summary>
            /// 命令处理工厂
            /// </summary>
            public Func<ICommandHandler> CommandHandlerFactory { get; set; }
        }
    }
}
