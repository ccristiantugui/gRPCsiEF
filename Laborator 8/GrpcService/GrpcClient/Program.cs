using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcService;
using Grpc.Net.Client;
using Grpc.Core.Interceptors;
using Google.Protobuf.WellKnownTypes;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var postClient = new RemotePost.RemotePostClient(channel);
            //var reply = await client.SayHelloAsync(
            //new HelloRequest { Name = "GreeterClient" });
            var reply2 = await postClient.AddPostAsync(
                new PostModel
                {
                    Id = 1,
                    Description = "Nimic",
                    Domain = "Altul",
                    Date = Timestamp.FromDateTime(DateTime.UtcNow)
                });
            Console.WriteLine("Result: " + reply2.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}