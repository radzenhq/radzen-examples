using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Northwind.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "NorthwindAudience";
        public static string Issuer { get; } = "Northwind";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NorthwindSecretSecurityKeyNorthwind"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
