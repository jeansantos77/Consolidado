using AutoMapper;
using Consolidado.API.Application.Interfaces;
using Consolidado.API.Domain.Entities;
using Consolidado.API.Domain.Interfaces;
using Consolidado.API.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Consolidado.API.Application.Implementations
{
    public class RabbitMQWorkerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly IMapper _mapper;
        private readonly QueueConfig _queueConfig;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQWorkerService(IMapper mapper, IOptions<QueueConfig> queueConfig, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _queueConfig = queueConfig.Value;

            _serviceProvider = serviceProvider;

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

                using (var scope = _serviceProvider.CreateScope())
                {
                    ILactoConsolidadoService lactoConsolidadoService = scope.ServiceProvider.GetRequiredService<ILactoConsolidadoService>();
                    
                    Task task = ProcessWithRetry(lactoConsolidadoService, ea);
                }
            };

            _channel.BasicConsume(queue: _queueConfig.QueueName, autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken); // Aguarde 10 segundo antes de verificar novamente
            }
        }

        private async Task ProcessWithRetry(ILactoConsolidadoService lactoConsolidadoService, BasicDeliverEventArgs ea)
        {
            int retryCount = 0;

            while (retryCount < _queueConfig.RetryQuantityIfError)
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = System.Text.Encoding.UTF8.GetString(body);

                    IQueueMessageModel queueModel = JsonSerializer.Deserialize<QueueMessageModel>(message);

                    ProcessQueueMessage(lactoConsolidadoService, queueModel);

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

        private void ProcessQueueMessage(ILactoConsolidadoService lactoConsolidadoService, IQueueMessageModel queueModel)
        {
            ILactoConsolidadoModel model = _mapper.Map<LactoConsolidadoModel>(queueModel);

            ILactoConsolidadoModel modelConsolidadoBefore = lactoConsolidadoService.GetLastBeforeDate(model.Data);

            LactoConsolidado lactoExistente = lactoConsolidadoService.GetByDate(model.Data);

            decimal saldoAnterior;

            if (lactoExistente == null)
            {
                model.Saldo = ((modelConsolidadoBefore != null) ? modelConsolidadoBefore.Saldo : 0) + model.Creditos - model.Debitos;
                LactoConsolidado lactoConsolidado = _mapper.Map<LactoConsolidado>(model);
                lactoConsolidadoService.Add(lactoConsolidado);

                saldoAnterior = lactoConsolidado.Saldo;
            }
            else
            {
                lactoExistente.Creditos = model.Creditos;
                lactoExistente.Debitos = model.Debitos;
                lactoExistente.Saldo = ((modelConsolidadoBefore != null) ? modelConsolidadoBefore.Saldo : 0) + lactoExistente.Creditos - lactoExistente.Debitos;

                lactoConsolidadoService.Update(lactoExistente);

                saldoAnterior = lactoExistente.Saldo;
            }

            lactoConsolidadoService.ReprocessForward(model.Data, saldoAnterior);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
