using FormatConverter.Interfaces;
using Newtonsoft.Json;

namespace FormatConverter.Library
{
    public class JsonSourceDeserializer<TModel> : ISourceDeserializer<string, TModel> where TModel : class
    {
        ///<inheritdoc/>
        public TModel Deserialize(string rawData)
        {
            return JsonConvert.DeserializeObject<TModel>(rawData);
        }
    }
}
