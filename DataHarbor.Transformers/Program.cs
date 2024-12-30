using DataHarbor.Common.Models;
using DataHarbor.Repository;

namespace DataHarbor.Transformers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddSingleton<ITransformationService, TransformationService>();
            builder.Services.AddTransient<IRepository<ProcessRequest>, RequestRepository>();

            var host = builder.Build();
            host.Run();
        }
    }
}