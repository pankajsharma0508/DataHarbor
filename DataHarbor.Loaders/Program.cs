using DataHarbor.Common.Repository;
using DataHarbor.Loaders.Services;
using DataHarbor.Repository;
using MassTransit;

namespace DataHarbor.Loaders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddTransient(typeof(IRepository<>), typeof(DocumentRepository<>));

            builder.Services.AddTransient(typeof(IDbaseService<>), typeof(DbaseService<>));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<DataLoaderConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("ingest-queue", e =>
                    {
                        e.ConfigureConsumer<DataLoaderConsumer>(context);
                    });
                });
            });

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}