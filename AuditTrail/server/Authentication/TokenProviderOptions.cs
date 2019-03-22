using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AuditTrail.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "AuditTrailAudience";
        public static string Issuer { get; } = "AuditTrail";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AuditTrailSecretSecurityKeyAuditTrail"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
