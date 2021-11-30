namespace CapSharp
{
    public interface ICapSharp
    {
        /// <summary>
        /// Try to solve the captcha
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns></returns>
        public bool TrySolveCaptcha(out string AccessToken);

        /// <summary>
        /// Attempt to obtain the user balance
        /// </summary>
        /// <param name="Balance">Balance</param>
        /// <returns>Returns <see cref="false"/> if a problem occurred, if throwing exceptions is enabled, an exception with error details will throw. If everything is correct, returns <see cref="true"/>. </returns>
        public bool TryGetUserBalance(out string Balance);
    }
}