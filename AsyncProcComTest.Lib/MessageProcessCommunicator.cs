using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProcComTest.Lib
{
    public static class MessageProcessCommunicator
    {
        public static async Task Server(string pipe_name, int server_id, Func<Message, Message> process)
        {
            while (true)
            {
                using (var server_stream = new NamedPipeServerStream(pipe_name, PipeDirection.InOut))
                {
                    Console.WriteLine($"Server#{server_id} waiting...");
                    await server_stream.WaitForConnectionAsync();
                    Console.WriteLine($"Server#{server_id} connected");

                    var request = default(Message);

                    using (var reader = new BinaryReader(server_stream, Encoding.UTF8, true))
                    {
                        request = reader.ReadObject<Message>();
                    }

                    Console.WriteLine($"Server#{server_id} {nameof(request)} : {request}");

                    var response = process(request);

                    Console.WriteLine($"Server#{server_id} {nameof(response)} : {response}");

                    using (var writer = new BinaryWriter(server_stream, Encoding.UTF8, true))
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

        public static async Task<Message> Client(string pipe_name, int client_id, Message request)
        {
            using (var client_stream = new NamedPipeClientStream(pipe_name))
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
                using (var reader = new BinaryReader(client_stream, Encoding.UTF8, true))
                {
                    response = reader.ReadObject<Message>();
                }

                Console.WriteLine($"Client#{client_id} {nameof(response)} : {response}");

                return response;
            }
        }

    }
}
