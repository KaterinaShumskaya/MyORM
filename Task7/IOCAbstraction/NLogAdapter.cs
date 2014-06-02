namespace IOCAbstraction
{
    using NLog;

    public class NLogAdapter : ILogger
    {
        private readonly Logger _logger;

        public NLogAdapter()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }
    }
}
