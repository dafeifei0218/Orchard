using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Orchard.Caching;
using Orchard.Data;
using Orchard.Environment;
using Orchard.Environment.Configuration;
using Orchard.Environment.State;
using Orchard.FileSystems.VirtualPath;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Tasks;
using Orchard.Exceptions;

namespace Orchard.Commands {
    
    /// <summary>
    /// Different return codes for a command execution.
    /// 命令结果代码。
    /// 命令执行的不同返回代码。
    /// </summary>
    public enum CommandReturnCodes
    {
        /// <summary>
        /// 确定
        /// </summary>
        Ok = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 5,
        /// <summary>
        /// 重试
        /// </summary>
        Retry = 240
    }

    /// <summary>
    /// This is the guy instantiated by the orchard.exe host. It is reponsible for
    /// executing a single command.
    /// 命令主机代理。
    /// 这家伙的orchard.exe主机实例化。它用于执行一个单一的命令。
    /// </summary>
    public class CommandHostAgent {
        private IContainer _hostContainer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommandHostAgent() {
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
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="tenant"></param>
        /// <param name="args"></param>
        /// <param name="switches"></param>
        /// <returns></returns>
        public CommandReturnCodes RunSingleCommand(TextReader input, TextWriter output, string tenant, string[] args, Dictionary<string, string> switches) {
            CommandReturnCodes result = StartHost(input, output);
            if (result != CommandReturnCodes.Ok)
                return result;

            result = RunCommand(input, output, tenant, args, switches);
            if (result != CommandReturnCodes.Ok)
                return result;

            return StopHost(input, output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="tenant"></param>
        /// <param name="args"></param>
        /// <param name="switches"></param>
        /// <returns></returns>
        public CommandReturnCodes RunCommand(TextReader input, TextWriter output, string tenant, string[] args, Dictionary<string, string> switches) {
            try {
                tenant = tenant ?? ShellSettings.DefaultName;

                using (var env = CreateStandaloneEnvironment(tenant)) {
                    var commandManager = env.Resolve<ICommandManager>();

                    ITransactionManager transactionManager;
                    if (!env.TryResolve(out transactionManager))
                        transactionManager = null;

                    var parameters = new CommandParameters {
                        Arguments = args,
                        Switches = switches,
                        Input = input,
                        Output = output
                    };

                    try {
                        commandManager.Execute(parameters);
                    }
                    catch {
                        // any database changes in this using(env) scope are invalidated
                        if (transactionManager != null)
                            transactionManager.Cancel();

                        // exception handling performed below
                        throw;
                    }
                }

                // in effect "pump messages" see PostMessage circa 1980
                var processingEngine = _hostContainer.Resolve<IProcessingEngine>();
                while (processingEngine.AreTasksPending())
                    processingEngine.ExecuteNextTask();

                return CommandReturnCodes.Ok;
            }
            catch (OrchardCommandHostRetryException ex) {
                // Special "Retry" return code for our host
                output.WriteLine(T("{0} (Retrying...)", ex.Message));
                return CommandReturnCodes.Retry;
            }
            catch (Exception ex) {
                if (ex.IsFatal()) {
                    throw;
                }
                if (ex is TargetInvocationException && 
                    ex.InnerException != null) {
                    // If this is an exception coming from reflection and there is an innerexception which is the actual one, redirect
                    ex = ex.InnerException;
                }
                OutputException(output, T("Error executing command \"{0}\"", string.Join(" ", args)), ex);
                return CommandReturnCodes.Fail;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public CommandReturnCodes StartHost(TextReader input, TextWriter output) {
            try {
                _hostContainer = CreateHostContainer();
                return CommandReturnCodes.Ok;
            }
            catch (OrchardCommandHostRetryException ex) {
                // Special "Retry" return code for our host
                output.WriteLine(T("{0} (Retrying...)", ex.Message));
                return CommandReturnCodes.Retry;
            }
            catch (Exception ex) {
                if (ex.IsFatal()) {         
                    throw;
                } 
                OutputException(output, T("Error starting up Orchard command line host"), ex);
                return CommandReturnCodes.Fail;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public CommandReturnCodes StopHost(TextReader input, TextWriter output) {
            try {
                if (_hostContainer != null) {
                    _hostContainer.Dispose();
                    _hostContainer = null;
                }
                return CommandReturnCodes.Ok;
            }
            catch (Exception ex) {
                if (ex.IsFatal()) {
                    throw;
                } 
                OutputException(output, T("Error shutting down Orchard command line host"), ex);
                return CommandReturnCodes.Fail;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="title"></param>
        /// <param name="exception"></param>
        private void OutputException(TextWriter output, LocalizedString title, Exception exception) {
            // Display header
            output.WriteLine();
            output.WriteLine(T("{0}", title));

            // Push exceptions in a stack so we display from inner most to outer most
            var errors = new Stack<Exception>();
            for (var scan = exception; scan != null; scan = scan.InnerException) {
                errors.Push(scan);
            }

            // Display inner most exception details
            exception = errors.Peek();
            output.WriteLine(T("--------------------------------------------------------------------------------"));
            output.WriteLine();
            output.WriteLine(T("{0}", exception.Message));
            output.WriteLine();

            if (!((exception is OrchardException ||
                exception is OrchardCoreException) &&
                exception.InnerException == null)) {

                output.WriteLine(T("Exception Details: {0}: {1}", exception.GetType().FullName, exception.Message));
                output.WriteLine();
                output.WriteLine(T("Stack Trace:"));
                output.WriteLine();

                // Display exceptions from inner most to outer most
                foreach (var error in errors) {
                    output.WriteLine(T("[{0}: {1}]", error.GetType().Name, error.Message));
                    output.WriteLine(T("{0}", error.StackTrace));
                    output.WriteLine();
                }
            }

            // Display footer
            output.WriteLine("--------------------------------------------------------------------------------");
            output.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private IContainer CreateHostContainer() {
            var hostContainer = OrchardStarter.CreateHostContainer(ContainerRegistrations);
            var host = hostContainer.Resolve<IOrchardHost>();

            host.Initialize();
            return hostContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private IWorkContextScope CreateStandaloneEnvironment(string tenant) {
            var host = _hostContainer.Resolve<IOrchardHost>();
            var tenantManager = _hostContainer.Resolve<IShellSettingsManager>();

            // Retrieve settings for speficified tenant.
            var settingsList = tenantManager.LoadSettings();
            if (settingsList.Any()) {
                var settings = settingsList.SingleOrDefault(s => String.Equals(s.Name, tenant, StringComparison.OrdinalIgnoreCase));
                if (settings == null) {
                    throw new OrchardCoreException(T("Tenant {0} does not exist", tenant));
                }

                var env = host.CreateStandaloneEnvironment(settings);
                return env;
            }
            else {
                // In case of an uninitialized site (no default settings yet), we create a default settings instance.
                var settings = new ShellSettings { Name = ShellSettings.DefaultName, State = TenantState.Uninitialized };
                return host.CreateStandaloneEnvironment(settings);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected void ContainerRegistrations(ContainerBuilder builder) {
            MvcSingletons(builder);

            builder.RegisterType<CommandHostEnvironment>().As<IHostEnvironment>().SingleInstance();
            builder.RegisterType<CommandHostVirtualPathMonitor>().As<IVirtualPathMonitor>().As<IVolatileProvider>().SingleInstance();
            builder.RegisterInstance(CreateShellRegistrations()).As<IShellContainerRegistrations>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CommandHostShellContainerRegistrations CreateShellRegistrations() {
            return new CommandHostShellContainerRegistrations {
                Registrations = shellBuilder => {
                    shellBuilder.RegisterType<CommandHostVirtualPathMonitor>()
                        .As<IVirtualPathMonitor>()
                        .As<IVolatileProvider>()
                        .InstancePerMatchingLifetimeScope("shell");
                    shellBuilder.RegisterType<CommandBackgroundService>()
                        .As<IBackgroundService>()
                        .InstancePerLifetimeScope();
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        static void MvcSingletons(ContainerBuilder builder) {
            builder.Register(ctx => RouteTable.Routes).SingleInstance();
            builder.Register(ctx => ModelBinders.Binders).SingleInstance();
            builder.Register(ctx => ViewEngines.Engines).SingleInstance();
        }

        /// <summary>
        /// 
        /// </summary>
        private class CommandHostShellContainerRegistrations : IShellContainerRegistrations {
            public Action<ContainerBuilder> Registrations { get; set; }
        }
    }
}
