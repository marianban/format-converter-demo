
namespace FormatConverter.Interfaces
{
    public interface IDestinationSerializer<out TRaw, in TModel> where TModel : class
    {
        /// <summary>
        /// Serializes model into raw data
        /// </summary>
        /// <param name="model">model to serialize</param>
        /// <returns></returns>
        TRaw Serialize(TModel model);
    }
}
