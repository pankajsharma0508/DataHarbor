using DataHarbor.Common.Repository;
using DataHarbor.Extractors.Handlers;
using DataHarbor.Extractors.Readers;
using DataHarbor.Repository;
using MassTransit;

namespace DataHarbor.Extractors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Supported file types can be added here.
            builder.Services.AddSingleton<IFileReader, TextFileReader>();
            builder.Services.AddSingleton<FileReaderResolver>();

            builder.Services.AddTransient(typeof(IRepository<>), typeof(DocumentRepository<>));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<DataExtractionConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("ingest-queue", e =>
                    {
                        e.ConfigureConsumer<DataExtractionConsumer>(context);
                    });
                });
            });

            //builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}