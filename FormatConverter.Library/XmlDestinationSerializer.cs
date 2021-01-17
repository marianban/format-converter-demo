using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FormatConverter.Interfaces;

namespace FormatConverter.Library
{
    public class XmlDestinationSerializer<TModel> : IDestinationSerializer<string, TModel> where TModel : class
    {
        private readonly Encoding _encoding;

        public XmlDestinationSerializer(Encoding encoding)
        {
            _encoding = encoding;
        }

        ///<inheritdoc/>
        public string Serialize(TModel model)
        {
            string output;
            var serializer = new XmlSerializer(typeof(TModel));
            using (var memoryStream = new MemoryStream())
            using (var writer = new XmlTextWriter(memoryStream, _encoding))
            {
                serializer.Serialize(writer, model);
                writer.Flush();

                using (var reader = new StreamReader(memoryStream, _encoding))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    output = reader.ReadToEnd();
                }
            }
            return output;
        }
    }
}