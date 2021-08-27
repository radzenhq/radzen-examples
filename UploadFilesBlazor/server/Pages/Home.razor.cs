using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components;
using System.IO;

namespace UploadFilesBlazor.Pages
{
    public partial class HomeComponent
    {
        [Inject]
        public IWebHostEnvironment HostEnvironment { get; set; }

        void DeleteFile(string name)
        {
            var fileName = Path.Combine(HostEnvironment.ContentRootPath, name);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
