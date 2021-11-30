namespace CapSharp.Services.TwoCaptchaService.Models
{
    public class GeeTest
    {

        /// <summary>
        /// Find the following GeeTest captcha parameter <see cref="gt"/>
        /// on the target website (usually you can find them inside initGeetest function).
        /// </summary>
        public string gt { get; set; } = string.Empty;

        /// <summary>
        /// Find the following GeeTest captcha parameter <see cref="challenge"/>
        /// on the target website (usually you can find them inside initGeetest function).
        /// </summary>
        public string challenge { get; set; } = string.Empty;

        /// <summary>
        /// (optional)
        ///
        /// Find the following GeeTest captcha parameter <see cref="api_server"/>
        /// on the target website (usually you can find them inside initGeetest function).        
        /// </summary>
        public string api_server { get; set; } = string.Empty;

        public bool ParametersFilled()
        {
            return
                !string.IsNullOrWhiteSpace(gt) &&
                !string.IsNullOrWhiteSpace(challenge);
        }

        public class Response
        {
            public string challenge { get; set; }
            public string validate { get; set; }
            public string seccode { get; set; }
        }
    }
}
