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
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("loading-queue", e =>
                    {
                        e.ConfigureConsumer<DataLoaderConsumer>(context);
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