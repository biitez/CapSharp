namespace CapSharp.Services.TwoCaptchaService.Models
{
    public class KeyCaptcha
    {
        /// <summary>
        /// Find <see cref="s_s_c_user_id"/> KeyCaptcha parameter in the page source code
        /// </summary>
        public string s_s_c_user_id { get; set; } = string.Empty;

        /// <summary>
        /// Find <see cref="s_s_c_session_id"/> KeyCaptcha parameter in the page source code
        /// </summary>
        public string s_s_c_session_id { get; set; } = string.Empty;

        /// <summary>
        /// Find <see cref="s_s_c_web_server_sign"/> KeyCaptcha parameter in the page source code
        /// </summary>
        public string s_s_c_web_server_sign { get; set; } = string.Empty;

        /// <summary>
        /// Find <see cref="s_s_c_web_server_sign2"/> KeyCaptcha parameter in the page source code
        /// </summary>
        public string s_s_c_web_server_sign2 { get; set; } = string.Empty;

        public bool ParametersFilled()
        {
            return 
                !string.IsNullOrWhiteSpace(s_s_c_user_id) &&
                !string.IsNullOrWhiteSpace(s_s_c_session_id) &&
                !string.IsNullOrWhiteSpace(s_s_c_web_server_sign) &&
                !string.IsNullOrWhiteSpace(s_s_c_web_server_sign2);
        }
    }
}
