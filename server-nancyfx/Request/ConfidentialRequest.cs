using SocialNetwork2.Model;

namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to separate requests which require user authentication/token.
    /// NOTE: Class is not abstract - can be instantiated due to Liskov Inversion Principle.
    /// Substituting any subclass into its place will work without problems, therefore we
    /// consider it acceptable.
    /// </summary>
    public class ConfidentialRequest : SNRequest
    {
        public Token userToken { get; set; }


        public ConfidentialRequest() : base()
        {

        }
    }
}