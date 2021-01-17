using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FormatConverter.Interfaces;

namespace FormatConverter.Library
{
    public class FileSource : ISource<string>
    {
        private readonly string _path;
        private readonly Encoding _encoding;

        public FileSource(string path, Encoding encoding)
        {
            _path = path;
            _encoding = encoding;
        }

        ///<inheritdoc/>
        public async Task<string> ReadAsync()
        {
            return await File.ReadAllTextAsync(_path, _encoding);
        }
    }
}
