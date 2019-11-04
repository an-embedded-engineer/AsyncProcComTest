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
            Console.WriteLine("Start Sender Process");

            int id = 0;

            while(true)
            {
                Console.Write("Input Tx Message >");
                var message_content = Console.ReadLine();

                Task.WaitAll(new[]
                {
                    Client(1, new Message { ID = id, Content = message_content }),
                });

                id++;

                if (message_content == "End")
                {
                    break;
                }

                Thread.Sleep(1000);
            }

            Console.WriteLine("Stop Sender(Client) Process");
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        private static async Task<Message> Client(int client_id, Message request)
        {
            using (var client_stream = new NamedPipeClientStream("my_named_pipe"))
            {
                Console.WriteLine($"Client#{client_id} connecting...");
                await client_stream.ConnectAsync();
                Console.WriteLine($"Client#{client_id} connected");

                Console.WriteLine($"Client#{client_id} {nameof(request)} : {request}");
                using (var writer = new BinaryWriter(client_stream, Encoding.UTF8, true))
                {
                    writer.WriteObject(request);
                }

                var response = default(Message);
                using(var reader = new BinaryReader(client_stream, Encoding.UTF8, true))
                {
                    response = reader.ReadObject<Message>();
                }

                Console.WriteLine($"Client#{client_id} {nameof(response)} : {response}");

                return response;
            }
        }

    }
}
