using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ExtendApplicationUser.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "ExtendApplicationUserAudience";
        public static string Issuer { get; } = "ExtendApplicationUser";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ExtendApplicationUserSecretSecurityKeyExtendApplicationUser"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
