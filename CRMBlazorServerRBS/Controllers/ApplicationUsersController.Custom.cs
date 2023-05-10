using System;
using System.Linq;
using CRMBlazorServerRBS.Models;

namespace CRMBlazorServerRBS.Controllers
{
    public partial class ApplicationUsersController
    {
        partial void OnUserUpdated(ApplicationUser user)
        {
            var item = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if(user != null)
            {
                item.FirstName = user.FirstName;
                item.LastName = user.LastName;
                item.Picture = user.Picture;
            }
        }
    }
}