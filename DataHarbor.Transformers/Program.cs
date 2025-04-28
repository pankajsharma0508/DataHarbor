using DataHarbor.Common.Repository;
using DataHarbor.Repository;
using DataHarbor.Transformers.Interfaces;
using DataHarbor.Transformers.Services;
using MassTransit;

namespace DataHarbor.Transformers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddTransient<IStorageService, StorageService>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(DocumentRepository<>));

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<DataTransformerConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("transformation-queue", e =>
                    {
                        e.ConfigureConsumer<DataTransformerConsumer>(context);
                    });
                    cfg.UseMessageRetry(r =>
                    {
                        r.Immediate(2); // Retry immediately 2 times
                        r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(2));
                    });
                });
            });
            //builder.Services.AddHostedService<Worker>();

            var host = builder.Build();

            host.Run();
        }
    }
}