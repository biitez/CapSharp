using CapSharp.Models;

/// <summary>
/// https://github.com/biitez/CapSharp
/// </summary>
namespace CapSharp
{
    public class CapSharp
    {
        /// <summary>
        /// <see cref="Proxy"/>
        /// </summary>
        /// 
        public Proxy Proxy { get; set; }

        /// <summary>
        /// <see cref="UseProxy"/>
        /// </summary>
        public bool UseProxy = false;

        /// <summary>
        /// If you activate this option, exceptions will be throwed whenever there is an internal 
        /// problem and will not return <see cref="false"/> in the methods.
        /// </summary>
        public bool ThrowExceptions = false;

        /// <summary>
        /// Initialize CapSharp constructor
        /// </summary>
        /// <param name="useProxy"></param>
        public CapSharp (bool useProxy = false)
        {
            UseProxy = useProxy;
        }
    }
}
