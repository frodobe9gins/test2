using System.Text.RegularExpressions;

namespace FileCheker.Lib
{
    /// <summary>
    /// обрабтчик знаков препинания
    /// </summary>
    class AnyFileObserver : FileReadObserver
    {
        public AnyFileObserver(IPrinter printer,string[] includeTypes, string[] excludeFileTypes) 
            : base(printer, includeTypes, excludeFileTypes)
        {
            _regex = new Regex(@"[.,]");
        }
    }

}
