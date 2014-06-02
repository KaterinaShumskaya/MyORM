namespace IOCAbstraction
{
    using log4net;

    public class Log4NetAdapter : ILogger
    {
        private static readonly ILog _log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Log4NetAdapter()
        {
            log4net.Config.DOMConfigurator.Configure();
        }

        public void Trace(string message)
        {
            _log.Debug(message);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }
    }
}
