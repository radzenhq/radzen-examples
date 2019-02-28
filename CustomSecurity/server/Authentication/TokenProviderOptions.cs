using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace CustomSecurity.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "CustomSecurityAudience";
        public static string Issuer { get; } = "CustomSecurity";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CustomSecuritySecretSecurityKeyCustomSecurity"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(30);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
