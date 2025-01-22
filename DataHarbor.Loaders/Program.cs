using DataHarbor.Common.Repository;
using DataHarbor.Loaders.Services;
using DataHarbor.Repository;

namespace DataHarbor.Loaders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(DocumentRepository<>));

            builder.Services.AddTransient(typeof(IDbaseService<>), typeof(DbaseService<>));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            var host = builder.Build();
            host.Run();
        }
    }
}