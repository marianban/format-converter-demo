using System.Threading.Tasks;

namespace FormatConverter.Interfaces
{
    public interface ISource<TRaw>
    {
        /// <summary>
        /// Reads the source data in raw format
        /// </summary>
        /// <returns></returns>
        Task<TRaw> ReadAsync();
    }
}
