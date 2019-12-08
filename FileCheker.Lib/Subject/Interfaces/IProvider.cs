using System;

namespace FileCheker.Lib
{
    interface IProvider
    {
        event EventHandler<FileEventArgs> Notify;
        void FileWasCreated(string filename);

    }

}
