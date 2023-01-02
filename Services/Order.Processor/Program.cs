using Newtonsoft.Json;
using Order.Processor.Models;
using Order.Processor.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Order.Processor;

internal class Program
{
    private static void Main(string[] args)
    {
        //var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@rabbitmq:5672") };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        var orderService = new OrderService();

        channel.QueueDeclare(
            queue: "createorder",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, args) =>
        {
            var body = args.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var newOrderDto = JsonConvert.DeserializeObject<NewOrderRequestDto>(json);

            Console.WriteLine($"Started processing {newOrderDto.Id}");
            await orderService.CreateOrder(newOrderDto);
            Console.WriteLine($"Finished processing {newOrderDto.Id}");
        };

        channel.BasicConsume(queue: "createorder", autoAck: true, consumer: consumer);
        Console.WriteLine("//////// Application started ////////");
        var exit = false;
        while (!exit)
        {
            var input = Console.ReadLine();
            if (input == "exit") exit = true;
        }
    }
}