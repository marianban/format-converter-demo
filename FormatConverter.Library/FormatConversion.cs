using System.Threading.Tasks;
using FormatConverter.Interfaces;

namespace FormatConverter.Library
{
    public class FormatConversion<TRaw, TModel> : IFormatConversion where TModel : class
    {
        private readonly ISource<TRaw> _source;
        private readonly ISourceDeserializer<TRaw, TModel> _deserializer;
        private readonly IDestinationSerializer<TRaw, TModel> _serializer;
        private readonly IDestination<TRaw> _destination;

        public FormatConversion(ISource<TRaw> source, ISourceDeserializer<TRaw, TModel> deserializer,
            IDestinationSerializer<TRaw, TModel> serializer, IDestination<TRaw> destination)
        {
            _source = source;
            _deserializer = deserializer;
            _serializer = serializer;
            _destination = destination;
        }

        ///<inheritdoc/>
        public async Task RunAsync()
        {
            var rawInput = await _source.ReadAsync();
            var model = _deserializer.Deserialize(rawInput);
            var rawOutput = _serializer.Serialize(model);
            await _destination.WriteAsync(rawOutput);
        }
    }
}