using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using FormatConverter.Interfaces;
using FormatConverter.Library;

namespace FormatConverter.CLI
{
    internal class Program
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    Console.WriteLine("Conversion started ...");
                    ConvertFormatAsync(options).Wait();
                    Console.WriteLine("Conversion successfully completed");
                });
        }

        private static async Task ConvertFormatAsync(Options options)
        {
            var source = new FileSource(options.Source, Encoding);
            var deserializer = CreateDeserializer(options.SourceFormatEnum);
            var serializer = CreateSerializer(options.DestinationFormatEnum);
            var destination = new FileDestination(options.Destination, Encoding);

            var conversion =
                new FormatConversion<string, Document>(source, deserializer, serializer, destination);
            await conversion.RunAsync();
        }

        private static ISourceDeserializer<string, Document> CreateDeserializer(SourceFormatEnum sourceFormat)
        {
            switch (sourceFormat)
            {
                case SourceFormatEnum.Json:
                    return new JsonSourceDeserializer<Document>();
                case SourceFormatEnum.Xml:
                    return new XmlSourceDeserializer<Document>(Encoding);
                default:
                    throw new InvalidEnumArgumentException("Unknown source format");
            }
        }

        private static IDestinationSerializer<string, Document> CreateSerializer(
            DestinationFormatEnum destinationFormat)
        {
            switch (destinationFormat)
            {
                case DestinationFormatEnum.Json:
                    return new JsonDestinationSerializer<Document>();
                case DestinationFormatEnum.Xml:
                    return new XmlDestinationSerializer<Document>(Encoding);
                default:
                    throw new InvalidEnumArgumentException("Unknown destination format");
            }
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}