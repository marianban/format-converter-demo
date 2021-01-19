using System.Threading.Tasks;
using FormatConverter.Interfaces;

namespace FormatConverter.Library
{
    public class FormatConversion<TRawIn, TModel, TRawOut> : IFormatConversion where TModel : class
    {
        private readonly ISource<TRawIn> _source;
        private readonly ISourceDeserializer<TRawIn, TModel> _deserializer;
        private readonly IDestinationSerializer<TModel, TRawOut> _serializer;
        private readonly IDestination<TRawOut> _destination;

        public FormatConversion(ISource<TRawIn> source, ISourceDeserializer<TRawIn, TModel> deserializer,
            IDestinationSerializer<TModel, TRawOut> serializer, IDestination<TRawOut> destination)
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