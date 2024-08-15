using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SimpleExampleAuth
{
    public class JWTAuthOptions
    {
        public const string ISSUER = "Server";
        public const string AUDIENCE = "Client";
        const string KEY = "Q85k@=@&p(0^++*K%MY(:J8BR!*Nzbbs]XmA&rmZUfqCf<TrPdHtwvj$Fgt$Jd0Z";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(KEY));
    }
}