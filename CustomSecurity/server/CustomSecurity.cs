using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomSecurity.Data;
using CustomSecurity.Models;
using CustomSecurity.Models.CustomSecurity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomSecurity
{
    public partial class Startup
    {
        partial void OnConfigureServices(IServiceCollection services)
        {
            // Replace the default ASP.NET Identity implementations with the custom ones
            services.AddTransient<IUserStore<ApplicationUser>, CustomUserStore>();
            services.AddTransient<IRoleStore<IdentityRole>, CustomRoleStore>();
            services.AddTransient<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>();
        }
    }

    public class CustomPasswordHasher : IPasswordHasher<ApplicationUser>
    {
        // Hashes passwords using the SHA256 algorithm. You can use your existing hashing algorithm.
        public string HashPassword(ApplicationUser user, string password)
        {
            using (var algorithm = SHA256.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));

                return Encoding.ASCII.GetString(hash);
            }
        }

        // Compares two hashed passwords
        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            var providedHashedPasswored = HashPassword(user, providedPassword);

            if (hashedPassword == providedHashedPasswored)
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }
    }

    // Role management - mostly CRUD operations
    public class CustomRoleStore : IQueryableRoleStore<IdentityRole>
    {
        private readonly CustomSecurityContext context;

        public CustomRoleStore(CustomSecurityContext context)
        {
            this.context = context;
        }

        // Maps Role instances (custom) to IdentityRole instances (ASP.NET Core Identity)
        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return context.Roles.Select(role => new IdentityRole
                {
                    Name = role.Name,
                    Id = role.Id.ToString()
                });
            }
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            context.Roles.Add(new Role()
            {
                Name = role.Name
            });

            var result = IdentityResult.Success;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = IdentityResult.Failed(new IdentityError { Description = $"Could not insert role {role.Name}." });
            }

            return result;
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var id = Convert.ToInt32(role.Id);
            var customRole = await context.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();

            var result = IdentityResult.Success;

            if (customRole != null)
            {
                context.Roles.Remove(customRole);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    result = IdentityResult.Failed(new IdentityError { Description = $"Could not delete role {role.Name}." });
                }
            }

            return result;
        }

        public void Dispose()
        {
        }

        public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var id = Convert.ToInt32(roleId);

            var role = await context.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();

            IdentityRole result = null;

            if (role != null)
            {
                result = new IdentityRole()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name
                };
            }

            return result;
        }

        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var role = await context.Roles.Where(r => r.Name == normalizedRoleName).FirstOrDefaultAsync();

            IdentityRole result = null;

            if (role != null)
            {
                result = new IdentityRole()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name
                };
            }

            return result;
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    // User management - CRUD, password management and associating users with roles
    public class CustomUserStore : IQueryableUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly CustomSecurityContext context;

        public CustomUserStore(CustomSecurityContext context)
        {
            this.context = context;
        }

        // Maps User instances (custom) to ApplicationUser instances (ASP.NET Core Identity)
        public IQueryable<ApplicationUser> Users
        {
            get
            {
                return this.context.Users.Select(user => new ApplicationUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    Id = user.Id.ToString()
                });
            }
        }

        // Adds user to a role by adding a new record in the UserRoles table
        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = await context.Roles.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefaultAsync();
            var userId = Convert.ToInt32(user.Id);

            context.UserRoles.Add(new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            });

            await context.SaveChangesAsync();
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var customUser = new User()
            {
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };

            context.Users.Add(customUser);

            var result = IdentityResult.Success;

            try
            {
                await context.SaveChangesAsync();
                user.Id = customUser.Id.ToString();
            }
            catch (Exception)
            {
                result = IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
            }

            return result;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var id = Convert.ToInt32(user.Id);
            var customUser = await context.Users.Include(u => u.UserRoles).Where(r => r.Id == id).FirstOrDefaultAsync();

            var result = IdentityResult.Success;

            if (customUser != null)
            {
                context.Users.Remove(customUser);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    result = IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
                }
            }

            return result;
        }

        public void Dispose()
        {
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var id = Convert.ToInt32(userId);

            var result = await FindByAsync(u => u.Id == id);

            return result;
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result = await FindByAsync(u => u.Email == normalizedUserName);

            return result;
        }

        // Finds a User instance based on some condition (predicate)
        private async Task<ApplicationUser> FindByAsync(Expression<Func<User, bool>> predicate)
        {
            var user = await context.Users.Where(predicate).FirstOrDefaultAsync();

            ApplicationUser result = null;

            if (user != null)
            {
                result = new ApplicationUser
                {
                    Id = user.Id.ToString(),
                    UserName = user.Email,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash
                };
            }

            return result;
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(user.Id);

            var roles = await context.Users
                               .Include(u => u.UserRoles)
                               .Where(u => u.Id == userId)
                               .SelectMany(u => u.UserRoles.Select(ur => ur.Role.Name))
                               .ToListAsync();

            return roles;
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!String.IsNullOrEmpty(user.PasswordHash));
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var id = Convert.ToInt32(user.Id);

            var result = await context.Users
                               .Include(u => u.UserRoles)
                               .Where(u => u.Id == id)
                               .SelectMany(u => u.UserRoles.Where(ur => ur.Role.Name == roleName))
                               .AnyAsync();

            return result;
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = await context.Roles.Where(r => r.Name == roleName)
                                      .Select(r => r.Id)
                                      .FirstOrDefaultAsync();

            var userId = Convert.ToInt32(user.Id);

            var userRole = await context.UserRoles.Where(ur => ur.RoleId == roleId && ur.UserId == userId).FirstOrDefaultAsync();

            context.UserRoles.Remove(userRole);

            await context.SaveChangesAsync();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var id = Convert.ToInt32(user.Id);
            var customUser = await context.Users.Where(r => r.Id == id).FirstOrDefaultAsync();

            var result = IdentityResult.Success;

            if (customUser != null)
            {
                customUser.Email = user.Email;
                customUser.PasswordHash = user.PasswordHash;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    result = IdentityResult.Failed(new IdentityError { Description = $"Could not update user {user.Email}." });
                }
            }

            return result;
        }
    }
}
