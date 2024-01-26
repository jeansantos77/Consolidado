using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consolidado.API.Application.Implementations
{
    public class RabbitMQWorkerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly QueueConfig _queueConfig;

        public RabbitMQWorkerService(IOptions<QueueConfig> queueConfig)
        {
            _queueConfig = queueConfig.Value;

            var factory = new ConnectionFactory
            {
                HostName = _queueConfig.HostName,
                Port = _queueConfig.Port,
                UserName = _queueConfig.UserName,
                Password = _queueConfig.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueConfig.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                Task task = ProcessWithRetry(ea);
            };

            _channel.BasicConsume(queue: _queueConfig.QueueName, autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken); // Aguarde 1 segundo antes de verificar novamente
            }
        }

        private async Task ProcessWithRetry(BasicDeliverEventArgs ea)
        {
            int retryCount = 0;

            while (retryCount < _queueConfig.RetryQuantityIfError)
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = System.Text.Encoding.UTF8.GetString(body);

                    //string msgJson = JsonSerializer.Deserialize<>(message);

                    Console.WriteLine($"Mensagem recebida: {message}");

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar mensagem (tentativa {retryCount + 1}): {ex.Message}");
                    retryCount++;
                    await Task.Delay(1000); // Aguarde 1 segundo antes de tentar novamente
                }
            }
                
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
