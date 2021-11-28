using System.Threading.Tasks;

namespace CapSharp
{
    public interface ICapSharp<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<T> SolveAsync();
    }
}
