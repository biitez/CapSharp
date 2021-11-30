using System.Net;

namespace CapSharp.Models
{
    public class Credentials
    {
        /// <summary>
        /// Configure your proxy credentials
        /// </summary>
        /// <param name="Username">Proxy Username</param>
        /// <param name="Password">Proxy Password</param>
        public Credentials(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// I didn't implement it in 2Captcha because I had problems using proxies in that captcha solver, 
        /// besides it wouldn't make sense to do it because it works faster captchaless and
        /// if you get a rate limit, they block your key and not IP address
        /// </summary>
        /// <returns></returns>
        public NetworkCredential GetNetworkCredential()
        {
            return new NetworkCredential(Username, Password);
        }
    }
}
