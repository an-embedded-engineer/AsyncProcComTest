using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProcComTest.Lib
{
    public static class MessageSender
    {
        public static void Execute()
        {
            Console.WriteLine("Start Sender(Client) Process");

            int id = 0;

            while (true)
            {
                Console.Write("Input Tx Message >");
                var message_content = Console.ReadLine();

                Task.WaitAll(new[]
                {
                    MessageProcessCommunicator.Client("my_message_pipe", 1, new Message { ID = id, Content = message_content }),
                });

                id++;

                if (message_content == "End")
                {
                    break;
                }

                Thread.Sleep(100);
            }

            Console.WriteLine("Stop Sender(Client) Process");
        }
    }
}
