using System.Threading.Tasks;

namespace FormatConverter.Interfaces
{
    public interface IDestination<in TRaw>
    {
        /// <summary>
        /// Writes data in row format to the output destination
        /// </summary>
        /// <returns></returns>
        Task WriteAsync(TRaw rawData);
    }
}
