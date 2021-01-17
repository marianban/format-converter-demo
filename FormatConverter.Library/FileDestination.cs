using System.IO;
using System.Text;
using System.Threading.Tasks;
using FormatConverter.Interfaces;

namespace FormatConverter.Library
{
    public class FileDestination : IDestination<string>
    {
        private readonly string _path;
        private readonly Encoding _encoding;

        public FileDestination(string path, Encoding encoding)
        {
            _path = path;
            _encoding = encoding;
        }

        ///<inheritdoc/>
        public async Task WriteAsync(string rawData)
        {
            await File.WriteAllTextAsync(_path, rawData, _encoding);
        }
    }
}
