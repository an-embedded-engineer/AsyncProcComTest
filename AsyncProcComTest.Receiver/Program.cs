using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using AsyncProcComTest.Lib;

namespace AsyncProcComTest.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageReceiver.Execute();

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
