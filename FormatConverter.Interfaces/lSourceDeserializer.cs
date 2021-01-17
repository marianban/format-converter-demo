namespace FormatConverter.Interfaces
{
    public interface ISourceDeserializer<in TRaw, out TModel> where TModel : class
    {
        /// <summary>
        /// Deserializes raw source data into a concrete model
        /// </summary>
        /// <param name="rawData">raw source data</param>
        /// <returns></returns>
        public TModel Deserialize(TRaw rawData);
    }
}
