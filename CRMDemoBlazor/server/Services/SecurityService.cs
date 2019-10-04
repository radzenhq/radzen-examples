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
using RadzenCrm.Models;
using RadzenCrm.Data;

namespace RadzenCrm
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
                _principal = httpContextAccessor.HttpContext.User;
                _user = context.Users.FirstOrDefault(u => u.Email == Principal.Identity.Name);
            }

        }

        public ApplicationIdentityDbContext context { get; set; }

        ApplicationUser _user;
        public ApplicationUser User
        {
            get
            {
                return _user;
            }
        }

        ClaimsPrincipal _principal = null;
        public ClaimsPrincipal Principal
        {
            get
            {
                return _principal;
            }
        }

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
            return _principal.Identity.IsAuthenticated;
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

                _principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Basic"));

                return await Task.FromResult(true);
            }

            var user = await userManager.FindByNameAsync(userName);

            if(user == null)
            {
                return await Task.FromResult(false);
            }

            var validPassword = await userManager.CheckPasswordAsync(user, password);

            if(!validPassword)
            {
                return await Task.FromResult(false);
            }

            _principal = await signInManager.CreateUserPrincipalAsync(user);

            if(_principal == null)
            {
                return await Task.FromResult(false);
            }

            _user = user;

            return await Task.FromResult(true);
        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            return await Task.FromResult(false);
        }

        public async Task<bool> RegisterUser(dynamic data)
        {
            return await Task.FromResult(false);
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await Task.FromResult(
              roleManager.Roles
            );
        }

        public async Task<IdentityRole> CreateRole(IdentityRole role)
        {
            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return null;
            }

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
            return await Task.FromResult(
              context.Users
            );
        }

        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            user.UserName = user.Email;

            var result = await userManager.CreateAsync(user, user.Password);

            if (result.Succeeded)
            {
                var roles = user.RoleNames;

                if (roles != null && roles.Any())
                {
                    result = await userManager.AddToRolesAsync(user, roles);

                    if (!result.Succeeded)
                    {
                        return null;
                    }
                }

                user.RoleNames = roles;

                return user;
            }
            else
            {
                return null;
            }
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
            var user = context.Users.Where(i => i.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
            }
            return await Task.FromResult(user);
        }

        public async Task<ApplicationUser> UpdateUser(string id, ApplicationUser userData)
        {
            var user = await userManager.FindByIdAsync(id);

            IdentityResult result = null;
            var roles = userData.RoleNames.ToList();
            if (roles != null)
            {
                result = await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));

                if (!result.Succeeded)
                {
                    return null;
                }

                if (roles.Any())
                {
                    result = await userManager.AddToRolesAsync(user, roles);
                }

                if (!result.Succeeded)
                {
                    return null;
                }

                var password = userData.Password;

                if (password != null)
                {
                    result = await userManager.RemovePasswordAsync(user);

                    if (!result.Succeeded)
                    {
                        return null;
                    }

                    result = await userManager.AddPasswordAsync(user, password);

                    if (!result.Succeeded)
                    {
                        return null;
                    }
                }

                result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return null;
                }
            }

            return user;
        }
    }
}
