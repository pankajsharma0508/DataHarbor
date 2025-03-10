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
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("ingest-queue", e =>
                    {
                        e.ConfigureConsumer<DataTransformerConsumer>(context);
                    });
                });
            });
            //builder.Services.AddHostedService<Worker>();

            var host = builder.Build();

            host.Run();
        }
    }
}