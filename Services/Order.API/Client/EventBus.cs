using RabbitMQ.Client;
using System.Text;

namespace Order.API.Client;

public class EventBus
{
    private readonly ConnectionFactory _factory;
    private readonly IConfiguration _configuration;

    public EventBus(IConfiguration configuration)
    {
        _configuration = configuration;
        _factory = new ConnectionFactory() { Uri = new Uri(_configuration.GetConnectionString("RabbitMQ")) };
    }

    public void SendToBus(string queueName, string payload)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        //Skapar en queue om den inte finns
        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        var body = Encoding.UTF8.GetBytes(payload);

        //Skickar ett event till bus
        channel.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body
            );
    }
}