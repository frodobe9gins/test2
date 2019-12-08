using System.IO;
using System.Threading;

namespace FileCheker.Lib
{
    /// <summary>
    /// Класс для запуска отслеживания дирректорий
    /// </summary>
    public class Watcher
    {
        private readonly string _path;
        private  readonly bool _issubdir;
        private readonly IProvider _subject;
        
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        /// <param name="path">путь к дирректории для отслеживания</param>
        /// <param name="IsSubDir">флаг необходимости отслеживания поддиректорий</param>
        public Watcher(string path, bool IsSubDir)
        {
            IPrinter printer = new PrintResult(); /// сюда будем склдадывать результат

            _issubdir = IsSubDir;
            _path = path;

            _subject = new FileManager(); /// менеджер, отслеживающий изменения файлов  

            //// инициализация обработчиков файлов. При желании можно вынести 
            /// в конфиг и тут загружать рефлексией. Тогда управление добавлением обработчиков
            /// и их настройками не потребует изменения запускающего класса
            IObserver divObserver = new DivCounterObserver(printer, new string[] { "html", "htm" }, null);
            IObserver anyObserver = new AnyFileObserver(printer, null, new string[] { "html","htm","css","jpg","png"});
            IObserver cssObserver = new CssFileObserver(printer, new string[] { "css" }, null);

            divObserver.Subscribe(_subject);
            anyObserver.Subscribe(_subject);
            cssObserver.Subscribe(_subject);

        }
        /// <summary>
        /// необходимо для возможности остановки отслеживания
        /// </summary>
        public ManualResetEvent ResetEvent { get; } = new ManualResetEvent(false);

        /// <summary>
        /// процедура инициаци
        /// </summary>
        public void Created()
        {
          var  watcher = new FileSystemWatcher
            {
                Path = _path,
                NotifyFilter = NotifyFilters.CreationTime| NotifyFilters.FileName,
                Filter = "*.*",
                IncludeSubdirectories = _issubdir
            };

            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.EnableRaisingEvents = true;
            ResetEvent.WaitOne();
            watcher.EnableRaisingEvents = false;
        }

        /// <summary>
        /// событие обработки новых файлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created )
            {
                {
                    /// отплавляем имя файла для обрабоки в менеджер
                    _subject.FileWasCreated(e.FullPath);
                }
            }
        }
    }
}
