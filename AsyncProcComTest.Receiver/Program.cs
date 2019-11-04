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
            Console.WriteLine("Start Receiver Process");

            Task.WaitAll(new[]
            {
                Server(1, ServerProcess),
            });

            Console.WriteLine("Stop Receiver(Server) Process");
            Console.WriteLine("Press any key to exit...");
            Console.Read();
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

        private static async Task Server(int server_id, Func<Message, Message> process)
        {
            while(true)
            {
                using(var server_stream = new NamedPipeServerStream("my_named_pipe", PipeDirection.InOut))
                {
                    Console.WriteLine($"Server#{server_id} waiting...");
                    await server_stream.WaitForConnectionAsync();
                    Console.WriteLine($"Server#{server_id} connected");

                    var request = default(Message);

                    using(var reader = new BinaryReader(server_stream, Encoding.UTF8, true))
                    {
                        request = reader.ReadObject<Message>();
                    }

                    Console.WriteLine($"Server#{server_id} {nameof(request)} : {request}");

                    var response = process(request);

                    Console.WriteLine($"Server#{server_id} {nameof(response)} : {response}");

                    using(var writer = new BinaryWriter(server_stream, Encoding.UTF8, true))
                    {
                        writer.WriteObject<Message>(response);
                    }

                    if (request.Content == "End")
                    {
                        break;
                    }
                }
            }
        }
    }
}
