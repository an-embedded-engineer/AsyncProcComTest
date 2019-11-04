using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProcComTest.Lib
{
    public static class MessageReceiver
    {
        public static void Execute()
        {
            Console.WriteLine("Start Receiver(Server) Process");

            Task.WaitAll(new[]
            {
                MessageProcessCommunicator.Server("my_message_pipe", 1, ServerProcess),
            });

            Console.WriteLine("Stop Receiver(Server) Process");
        }

        private static Message ServerProcess(Message message)
        {
            var response = new Message()
            {
                ID = message.ID,
                Content = new string(message.Content.Reverse().ToArray()),
            };

            return response;
        }
    }
}
