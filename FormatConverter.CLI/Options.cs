using CommandLine;

namespace FormatConverter.CLI
{
    public class Options
    {
        [Option("sourceType", Default = SourceTypeEnum.File, HelpText = "Data Source Type (File)")]
        public SourceTypeEnum SourceType { get; set; }

        [Option('s', "source", Required = true, HelpText = "Data Source (file path)")]
        public string Source { get; set; }

        [Option('f', "sourceFormat", Required = true, HelpText = "Source Format (Json, Xml)")]
        public SourceFormatEnum SourceFormatEnum { get; set; }

        [Option("sourceType", Default = DestinationTypeEnum.File, HelpText = "Data Destination Type (File)")]
        public DestinationTypeEnum DestinationType { get; set; }

        [Option('d', "destination", Required = true, HelpText = "Data Destination (file path)")]
        public string Destination { get; set; }

        [Option('o', "destinationFormat", Required = true, HelpText = "Destination Format (Json, Xml)")]
        public DestinationFormatEnum DestinationFormatEnum { get; set; }
    }
}