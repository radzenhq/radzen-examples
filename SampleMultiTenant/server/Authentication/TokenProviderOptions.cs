using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace MultiTenantSample.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "MultiTenantSampleAudience";
        public static string Issuer { get; } = "MultiTenantSample";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MultiTenantSampleSecretSecurityKeyMultiTenantSample"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
