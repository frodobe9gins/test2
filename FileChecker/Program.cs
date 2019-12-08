using FileCheker.Lib;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileChecker
{
    class Program
    {

        /// <summary>
        /// при необходимости легко перенести в win service
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {            
            var folder = @"e:\WatchedDir";
            var isSubdir = true ; 

            Watcher watcher = new Watcher(folder, isSubdir);
            new Thread(watcher.Created).Start();
            Console.WriteLine("Add files to "+folder);
            Console.WriteLine("Or press any key to stop watch!");
            Console.ReadKey();
            watcher.ResetEvent.Set();
            Console.WriteLine("watcher was stop! Press any key to exit");
            Console.ReadKey();
        }

    }
}
