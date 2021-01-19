using System.IO;
using System.Linq;
using System.Text;
using FormatConverter.CLI;
using FormatConverter.Library;
using Xunit;

namespace FormatConverter.Tests
{
    public class ConverterTests
    {
        public ConverterTests()
        {
            _xmlExample = File.ReadAllText("./document.xml", _encoding);
            _jsonExample = File.ReadAllText("./document.json", _encoding);
            _xmlNullExample = File.ReadAllText("./null.xml", _encoding);
            _jsonNullExample = File.ReadAllText("./null.json", _encoding);
            // runs before each test
            ClearTempDirectory();
        }

        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _xmlExample;
        private readonly string _jsonExample;
        private readonly string _xmlNullExample;
        private readonly string _jsonNullExample;

        private void ClearTempDirectory()
        {
            var tempPath = "./Temp";
            if (Directory.Exists(tempPath))
                Directory.GetFiles(tempPath).ToList().ForEach(DeleteFile);
            else
                Directory.CreateDirectory(tempPath);
        }

        private void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // ignore exception
            }
        }

        [Fact]
        public async void ConvertJsonFileToXmlFile()
        {
            var source = new FileSource("./document.json", _encoding);
            var deserializer = new JsonSourceDeserializer<Document>();
            var serializer = new XmlDestinationSerializer<Document>(_encoding);
            var destinationPath = "./Temp/document.xml";
            var destination = new FileDestination(destinationPath, _encoding);
            var conversion = new FormatConversion<string, Document, string>(source, deserializer, serializer, destination);

            await conversion.RunAsync();

            var output = await File.ReadAllTextAsync(destinationPath, _encoding);
            Assert.Equal(_xmlExample, output);
        }

        [Fact]
        public async void ConvertNullJsonFileToXmlFile()
        {
            var source = new FileSource("./null.json", _encoding);
            var deserializer = new JsonSourceDeserializer<Document>();
            var serializer = new XmlDestinationSerializer<Document>(_encoding);
            var destinationPath = "./Temp/null.xml";
            var destination = new FileDestination(destinationPath, _encoding);
            var conversion = new FormatConversion<string, Document, string>(source, deserializer, serializer, destination);

            await conversion.RunAsync();

            var output = await File.ReadAllTextAsync(destinationPath, _encoding);
            Assert.Equal(_xmlNullExample, output);
        }

        [Fact]
        public async void ConvertNullXmlFileJsonFile()
        {
            var source = new FileSource("./null.xml", _encoding);
            var deserializer = new XmlSourceDeserializer<Document>(_encoding);
            ;
            var serializer = new JsonDestinationSerializer<Document>();
            var destinationPath = "./Temp/document.json";
            var destination = new FileDestination(destinationPath, _encoding);
            var conversion = new FormatConversion<string, Document, string>(source, deserializer, serializer, destination);

            await conversion.RunAsync();

            var output = await File.ReadAllTextAsync(destinationPath, _encoding);
            Assert.Equal(_jsonNullExample, output);
        }

        [Fact]
        public async void ConvertXmlFileToJsonFile()
        {
            var source = new FileSource("./document.xml", _encoding);
            var deserializer = new XmlSourceDeserializer<Document>(_encoding);
            var serializer = new JsonDestinationSerializer<Document>();
            var destinationPath = "./Temp/document.json";
            var destination = new FileDestination(destinationPath, _encoding);
            var conversion = new FormatConversion<string, Document, string>(source, deserializer, serializer, destination);

            await conversion.RunAsync();

            var output = await File.ReadAllTextAsync(destinationPath, _encoding);
            Assert.Equal(_jsonExample, output);
        }

        [Fact]
        public void DeserializeDocumentFromJson()
        {
            var expected = new Document {Title = "Heading", Text = "Text Content"};
            var deserializer = new JsonSourceDeserializer<Document>();

            var document = deserializer.Deserialize(_jsonExample);

            Assert.Equal(document.Title, expected.Title);
            Assert.Equal(document.Text, expected.Text);
        }

        [Fact]
        public void DeserializeDocumentFromXml()
        {
            var expected = new Document {Title = "Heading", Text = "Text Content"};
            var deserializer = new XmlSourceDeserializer<Document>(_encoding);

            var document = deserializer.Deserialize(_xmlExample);

            Assert.Equal(document.Title, expected.Title);
            Assert.Equal(document.Text, expected.Text);
        }

        [Fact]
        public void DeserializeNullDocumentFromJson()
        {
            var deserializer = new JsonSourceDeserializer<Document>();

            var document = deserializer.Deserialize(_jsonNullExample);

            Assert.Null(document);
        }

        [Fact]
        public void SerializeDocumentToJson()
        {
            var document = new Document {Title = "Heading", Text = "Text Content"};
            var serializer = new JsonDestinationSerializer<Document>();

            var json = serializer.Serialize(document);

            Assert.Equal(_jsonExample, json);
        }

        [Fact]
        public void SerializeDocumentToXml()
        {
            var document = new Document {Title = "Heading", Text = "Text Content"};
            var serializer = new XmlDestinationSerializer<Document>(_encoding);

            var xml = serializer.Serialize(document);

            Assert.Equal(_xmlExample, xml);
        }
    }
}