using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using BlazorMultiTenant.Models;
using BlazorMultiTenant.Data;

namespace BlazorMultiTenant
{
    public class SecurityService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NavigationManager uriHelper;

        public SecurityService(ApplicationIdentityDbContext context,
            IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            NavigationManager uriHelper)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
            this.uriHelper = uriHelper;

            if (httpContextAccessor.HttpContext != null)
            {
                Principal = httpContextAccessor.HttpContext.User;
                User = context.Users.FirstOrDefault(u => u.Email == Principal.Identity.Name);
            }
        }

        public ApplicationIdentityDbContext context { get; set; }

        public ApplicationUser User { get; private set; }

        public ClaimsPrincipal Principal { get; private set; }

        public bool IsInRole(params string[] roles)
        {
            bool result = IsAuthenticated();

            if (User != null)
            {
                foreach (var roleName in roles.Where(r => r != "Authenticated"))
                {
                    var role = context.Roles.FirstOrDefault(r => r.Name == roleName);
                    if (role != null)
                    {
                        var userRole = context.UserRoles.FirstOrDefault(ur => ur.RoleId == role.Id && ur.UserId == User.Id);
                        if (userRole == null)
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        public bool IsAuthenticated()
        {
            return Principal.Identity.IsAuthenticated;
        }

        public async void Logout()
        {
            uriHelper.NavigateTo("/Account/Logout", true);
        }

        public async Task<bool> Login(string userName, string password)
        {
            if (env.EnvironmentName == "Development" && userName == "admin" && password == "admin")
            {
                var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, "admin"),
                        new Claim(ClaimTypes.Email, "admin")
                      };

                roleManager.Roles.ToList().ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));

                Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Basic"));

                return true;
            }

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return false;
            }

            var validPassword = await userManager.CheckPasswordAsync(user, password);

            if (!validPassword)
            {
                return false;
            }

            Principal = await signInManager.CreateUserPrincipalAsync(user);

            if (Principal == null)
            {
                return false;
            }

            User = user;

            return true;
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await Task.FromResult(roleManager.Roles);
        }

        public async Task<IdentityRole> CreateRole(IdentityRole role)
        {
            var result = await roleManager.CreateAsync(role);

            EnsureSucceeded(result);

            return role;
        }

        public async Task<IdentityRole> DeleteRole(string id)
        {
            var item = context.Roles
                .Where(i => i.Id == id)
                .FirstOrDefault();

            context.Roles.Remove(item);
            context.SaveChanges();

            return item;
        }

        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await Task.FromResult(context.Roles.Find(id));
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await Task.FromResult(context.Users);
        }

        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            user.UserName = user.Email;

            var result = await userManager.CreateAsync(user, user.Password);

            EnsureSucceeded(result);

            var roles = user.RoleNames;

            if (roles != null && roles.Any())
            {
                result = await userManager.AddToRolesAsync(user, roles);
                EnsureSucceeded(result);
            }

            user.RoleNames = roles;

            return user;
        }

        public async Task<ApplicationUser> DeleteUser(string id)
        {
            var item = context.Users
              .Where(i => i.Id == id)
              .FirstOrDefault();

            context.Users.Remove(item);
            context.SaveChanges();

            return item;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
            }

            return await Task.FromResult(user);
        }

        public async Task<ApplicationUser> UpdateUser(string id, ApplicationUser user)
        {
            var roles = user.RoleNames.ToArray();

            var result = await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));

            EnsureSucceeded(result);

            if (roles.Any())
            {
                result = await userManager.AddToRolesAsync(user, roles);

                EnsureSucceeded(result);
            }

            result = await userManager.UpdateAsync(user);

            EnsureSucceeded(result);

            if (!String.IsNullOrEmpty(user.Password) && user.Password == user.ConfirmPassword)
            {
                result = await userManager.RemovePasswordAsync(user);

                EnsureSucceeded(result);

                result = await userManager.AddPasswordAsync(user, user.Password);

                EnsureSucceeded(result);
            }

            return user;
        }

        private void EnsureSucceeded(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var message = string.Join(", ", result.Errors.Select(error => error.Description));

                throw new ApplicationException(message);
            }
        }
    }
}
