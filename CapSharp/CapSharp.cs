using System.Threading.Tasks;

namespace CapSharp
{
    public interface ICapSharp<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Type</returns>
        public Task<T> SolveAsync();
    }
}
