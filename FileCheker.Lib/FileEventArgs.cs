using System;

namespace FileCheker.Lib
{
    /// <summary>
    /// параметры для передачи события от субъекта к подписчикам (обработчикам текста)
    /// </summary>
    public class FileEventArgs : EventArgs
    {
        private readonly string _filename;

        public FileEventArgs(string filename)
        {
            _filename = filename;
        }
        public string FileName => _filename;
    }

}
