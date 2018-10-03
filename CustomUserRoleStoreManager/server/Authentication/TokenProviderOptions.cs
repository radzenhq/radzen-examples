using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace CustomUserRoleStoreManager.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "CustomUserRoleStoreManagerAudience";
        public static string Issuer { get; } = "CustomUserRoleStoreManager";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CustomUserRoleStoreManagerSecretSecurityKeyCustomUserRoleStoreManager"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
