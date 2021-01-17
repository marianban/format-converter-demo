using System.Threading.Tasks;

namespace FormatConverter.Interfaces
{
    public interface IFormatConversion
    {
        /// <summary>
        /// Executes format conversion
        /// </summary>
        /// <returns></returns>
        Task RunAsync();
    }
}
