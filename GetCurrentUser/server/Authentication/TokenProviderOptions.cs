using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace GetCurrentUser.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "GetCurrentUserAudience";
        public static string Issuer { get; } = "GetCurrentUser";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("GetCurrentUserSecretSecurityKeyGetCurrentUser"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
