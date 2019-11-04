using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
using AsyncProcComTest.Lib;
using System.Threading;

namespace AsyncProcComTest.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageSender.Execute();

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }



    }
}
