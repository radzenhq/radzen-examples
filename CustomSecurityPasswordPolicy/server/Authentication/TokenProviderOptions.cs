using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace CustomSecurityPasswordPolicy.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "CustomSecurityPasswordPolicyAudience";
        public static string Issuer { get; } = "CustomSecurityPasswordPolicy";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CustomSecurityPasswordPolicySecretSecurityKeyCustomSecurityPasswordPolicy"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
