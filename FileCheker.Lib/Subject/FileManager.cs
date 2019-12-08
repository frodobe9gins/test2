using System;

namespace FileCheker.Lib
{
    /// <summary>
    /// Субъект на который все подписываются
    /// </summary>
    class FileManager : IProvider
    {
        public event EventHandler<FileEventArgs> Notify;

        /// <summary>
        /// При добавлении нового файла его название приходит сюда
        /// </summary>
        /// <param name="filename">название файла</param>
        public void FileWasCreated(string filename)
        {
            /// оповещаем всех подписчиков о создани новго файла
            Notify?.Invoke(this,new FileEventArgs(filename));
        }
    }

}
