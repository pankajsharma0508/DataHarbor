using DataHarbor.Extractors.Processors;

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

            var host = builder.Build();
            host.Run();
        }
    }
}