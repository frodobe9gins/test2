using System.Text.RegularExpressions;

namespace FileCheker.Lib
{
    /// <summary>
    /// обработчик css файлов
    /// </summary>
    class CssFileObserver : FileReadObserver
    {
        public CssFileObserver(IPrinter printer, string[] includeTypes, string[] excludeFileTypes) 
            : base(printer,includeTypes, excludeFileTypes)
        {
        }

        /// <summary>
        /// Сходу не придумал регулярку, чтобы проверить в один шаг
        /// пришлось переопределить функцию обработки текста
        /// </summary>
        /// <param name="fileText"></param>
        /// <returns></returns>
        protected override string DoWork(string fileText)
        {
            _regex = new Regex(@"{");
            var open= _regex.Matches(fileText).Count;
            _regex = new Regex(@"}");
            var closed = _regex.Matches(fileText).Count;
            if (closed == open)
            {
                return "valid";
            }
            else
            {
                return "not valid";
            }
        }
    }

}
