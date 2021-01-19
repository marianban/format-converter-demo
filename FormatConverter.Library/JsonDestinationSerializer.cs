using FormatConverter.Interfaces;
using Newtonsoft.Json;

namespace FormatConverter.Library
{
    public class JsonDestinationSerializer<TModel> : IDestinationSerializer<TModel, string> where TModel : class
    {
        ///<inheritdoc/>
        public string Serialize(TModel model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
