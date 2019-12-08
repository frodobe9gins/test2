using NLog;

namespace FileCheker.Lib
{

    interface IPrinter
    {
        void Send(string message);
    }

    /// <summary>
    /// Абстрактный клас сохранения реультата
    /// </summary>
    abstract class Printer: IPrinter
    {
        private readonly ILogger _log;

        public Printer(string logname)
        {
            _log = LogManager.GetLogger(logname);
        }

        public virtual void Send(string message)
        {
            _log.Debug(message);
        }

    }
}
