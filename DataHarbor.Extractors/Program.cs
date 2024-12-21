using DataHarbor.Extractors.Commands;
using DataHarbor.Extractors.Handlers;
using DataHarbor.Extractors.Processors;
using DataHarbor.Extractors.Readers;

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

            builder.Services.AddSingleton<IFileReader, CsvFileReader>();
            builder.Services.AddSingleton<IFileReader, XmlFileReader>();
            builder.Services.AddSingleton<IFileReader, DatFileReader>();
            builder.Services.AddSingleton<IFileReader, TxtFileReader>();
            builder.Services.AddSingleton<FileReaderResolver>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            var host = builder.Build();
            host.Run();
        }
    }
}