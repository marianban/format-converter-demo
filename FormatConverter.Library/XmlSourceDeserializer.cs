using System.IO;
using System.Text;
using System.Xml.Serialization;
using FormatConverter.Interfaces;

namespace FormatConverter.Library
{
    public class XmlSourceDeserializer<TModel> : ISourceDeserializer<string, TModel> where TModel : class
    {
        private readonly Encoding _encoding;

        public XmlSourceDeserializer(Encoding encoding)
        {
            _encoding = encoding;
        }

        ///<inheritdoc/>
        public TModel Deserialize(string rawData)
        {
            TModel document;
            var serializer = new XmlSerializer(typeof(TModel));
            using (var stream = new MemoryStream(_encoding.GetBytes(rawData)))
            using (var reader = new StreamReader(stream, _encoding))
            {
                document = (TModel) serializer.Deserialize(reader);
            }
            return document;
        }
    }
}