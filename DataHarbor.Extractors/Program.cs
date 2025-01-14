using DataHarbor.Common.Repository;
using DataHarbor.Extractors.Handlers;
using DataHarbor.Extractors.Readers;
using DataHarbor.Repository;

namespace DataHarbor.Extractors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
         
            builder.Services.AddSingleton<IFileReader, CsvFileReader>();
            builder.Services.AddSingleton<IFileReader, XmlFileReader>();
            builder.Services.AddSingleton<IFileReader, DatFileReader>();
            builder.Services.AddSingleton<IFileReader, TxtFileReader>();
            builder.Services.AddSingleton<FileReaderResolver>();

            builder.Services.AddTransient(typeof(IRepository<>), typeof(DocumentRepository<>));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            var host = builder.Build();
            host.Run();
        }
    }
}