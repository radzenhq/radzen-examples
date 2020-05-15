using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    public partial class ReportController : Controller
    {
        partial void OnHttpClientHandlerCreate(ref HttpClientHandler handler)
        {
            //AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);

            //handler.UseDefaultCredentials = true;
            handler.PreAuthenticate = true;
            handler.Credentials = new NetworkCredential("52.175.240.149\\lacoeadmin", "lacoe0624###");
        }
    }
}
