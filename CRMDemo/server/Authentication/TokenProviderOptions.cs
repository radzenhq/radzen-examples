using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Crm.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "CrmAudience";
        public static string Issuer { get; } = "Crm";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CrmSecretSecurityKeyCrm"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(60);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
