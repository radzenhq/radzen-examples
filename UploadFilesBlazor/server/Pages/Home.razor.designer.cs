using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using UploadFilesBlazor.Models.UploadDb;
using Microsoft.EntityFrameworkCore;

namespace UploadFilesBlazor.Pages
{
    public partial class HomeComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected UploadDbService UploadDb { get; set; }
        protected RadzenDataGrid<UploadFilesBlazor.Models.UploadDb.File> datagrid0;

        IEnumerable<UploadFilesBlazor.Models.UploadDb.File> _getFilesResult;
        protected IEnumerable<UploadFilesBlazor.Models.UploadDb.File> getFilesResult
        {
            get
            {
                return _getFilesResult;
            }
            set
            {
                if (!object.Equals(_getFilesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFilesResult", NewValue = value, OldValue = _getFilesResult };
                    _getFilesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var uploadDbGetFilesResult = await UploadDb.GetFiles();
            getFilesResult = uploadDbGetFilesResult;
        }

        protected async System.Threading.Tasks.Task Upload0Complete(UploadCompleteEventArgs args)
        {
            await this.datagrid0.Reload();
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args, dynamic data)
        {
            var uploadDbDeleteFileResult = await UploadDb.DeleteFile(data.Id);
            DeleteFile($"{data.Name}");

            await this.datagrid0.Reload();
        }
    }
}
