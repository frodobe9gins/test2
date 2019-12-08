using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace FileCheker.Lib

{
    /// <summary>
    /// Абстрактный класс подписчика реализует типовой шаблон обработки
    /// </summary>
    abstract class FileReadObserver : Observer
    {
        protected Regex _regex;
        private readonly IPrinter _printer;

        private readonly string[] _includeTypes;
        private readonly string[] _excludeFileTypes;

        private const int NumberOfRetries = 6;
        private const int DelayOnRetry = 1000;


        /// <summary>
        /// В конструкторе указываем типы обрабатываемых файлов, если не указано
        /// будут обработаны все
        /// </summary>
        /// <param name="includeTypes">массив расширений файлов для обработки</param>
        /// <param name="excludeFileTypes">массив расширей, которые исключить из обработки</param>
        public FileReadObserver(IPrinter printer, string[] includeTypes, string[] excludeFileTypes)
        {
            _includeTypes = includeTypes;
            _excludeFileTypes = excludeFileTypes;
            _printer = printer;
        }

        /// <summary>
        /// Подсчет количества элемента по регулярному выражению
        /// </summary>
        /// <param name="fileText">текст, который нужно обработать</param>
        /// <returns></returns>
        protected virtual string DoWork(string fileText)
        {
            if (fileText == null)
                return "";
            var res = _regex.Matches(fileText).Count;
            return res.ToString();
        }

        /// <summary>
        /// событие создание нового файла обрабатывается тут
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Notify(object sender, FileEventArgs e)
        {
            string text=null;

            var type=e.FileName.Substring(e.FileName.LastIndexOf(".")+1);
            var isExcluded = _excludeFileTypes != null && _excludeFileTypes.Contains( type);
            var isIncluded = (!(_includeTypes != null)) || _includeTypes.Contains(type);
            if (isIncluded && !isExcluded)
            {
                try
                {
                    /// событие приходит несколько раз, даже еще до создания файла на диске
                    /// поэтому нужны доп. проверки и задержка, в случае, если файл еще копируется 
                    /// на диск (большой файл сохраняется из браузера например)
                    if (File.Exists(e.FileName))
                    {
                        for (int i = 0; i < NumberOfRetries; i++)
                        {
                            try
                            {
                                using (FileStream fileStream = File.OpenRead(e.FileName))
                                {
                                    using (var streamReader = new StreamReader(fileStream))
                                    {
                                        text= streamReader.ReadToEnd();
                                        break;
                                    }
                                }
                            }

                            catch (IOException ioex) when (i < NumberOfRetries)
                            {
                                Thread.Sleep(DelayOnRetry * i);
                            }
                        }

                        _printer.Send(e.FileName + " - " + GetType().Name + " - " + DoWork(text));
                    }
                }
                catch (Exception exeption)
                {
                    // TODO: append logger!
                }
            }
        }
    }

}
