using System.Net;

namespace CapSharp.Models
{
    public class Credentials
    {
        public Credentials(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public NetworkCredential GetNetworkCredential()
        {
            return new NetworkCredential(Username, Password);
        }
    }
}
