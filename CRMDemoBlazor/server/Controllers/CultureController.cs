using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace RadzenCrm
{
    [Route("[controller]/[action]")]
    public partial class CultureController : Controller
    {
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
            }

            return LocalRedirect(redirectUri);
        }
    }
}
