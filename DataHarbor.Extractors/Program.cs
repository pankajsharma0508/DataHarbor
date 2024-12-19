using DataHarbor.Extractors.Commands;
using DataHarbor.Extractors.Processors;
using System.Reflection;

namespace DataHarbor.Extractors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddSingleton<IFileProcessor, CsvFileProcessor>();
            builder.Services.AddSingleton<IFileProcessor, XmlFileProcessor>();
            builder.Services.AddSingleton<IFileProcessor, DatFileProcessor>();
            builder.Services.AddSingleton<IFileProcessor, TxtFileProcessor>();
            builder.Services.AddSingleton<FileProcessorResolver>();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            var host = builder.Build();
            host.Run();
        }
    }
}