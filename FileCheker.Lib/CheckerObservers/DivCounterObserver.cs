using System.Text.RegularExpressions;

namespace FileCheker.Lib
{
    class DivCounterObserver : FileReadObserver
    {

        /// <summary>
        /// обработчик, выполняющий подсчет <div> в файле
        /// </summary>
        /// <param name="include">типы файлов в которых будет работать подписчик</param>
        /// <param name="exclude">типы файлов в которых не будет работать подписчик</param>
        public DivCounterObserver(IPrinter printer, string[] include,string[] exclude) 
            : base(printer,include, exclude)
        {
            _regex= new Regex(@"(<div.*>)"); 
        }

        //// ToDo при необходимости переопределить DoWork и Notify базового класса

    }

}
