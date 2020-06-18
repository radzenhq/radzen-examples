using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorCrmWasm.Models.Crm;
using Microsoft.AspNetCore.Identity;
using BlazorCrmWasm.Models;
using BlazorCrmWasm.Client.Pages;

namespace BlazorCrmWasm.Pages
{
    public partial class ContactsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }


        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }


        [Inject]
        protected CrmService Crm { get; set; }

        protected RadzenGrid<BlazorCrmWasm.Models.Crm.Contact> grid0;

        string _contactFilter;
        protected string contactFilter
        {
            get
            {
                return _contactFilter;
            }
            set
            {
                if(!object.Equals(_contactFilter, value))
                {
                    _contactFilter = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.Contact> _getContactsResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.Contact> getContactsResult
        {
            get
            {
                return _getContactsResult;
            }
            set
            {
                if(!object.Equals(_getContactsResult, value))
                {
                    _getContactsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        int _getContactsCount;
        protected int getContactsCount
        {
            get
            {
                return _getContactsCount;
            }
            set
            {
                if(!object.Equals(_getContactsCount, value))
                {
                    _getContactsCount = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!await Security.IsAuthenticatedAsync())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }

        }
        protected async System.Threading.Tasks.Task Load()
        {
            contactFilter = "";
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            grid0.Reload();
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddContact>("Add Contact", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetContactsResult = await Crm.GetContacts(filter:$@"contains(Email,""{contactFilter}"") or contains(Company,""{contactFilter}"") or contains(FirstName,""{contactFilter}"") or contains(LastName,""{contactFilter}"")", orderby:$@"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getContactsResult = crmGetContactsResult.Value.AsODataEnumerable();

                getContactsCount = crmGetContactsResult.Count;
            }
            catch (System.Exception crmGetContactsException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to load Contacts");
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCrmWasm.Models.Crm.Contact args)
        {
            var dialogResult = await DialogService.OpenAsync<EditContact>("Edit Contact", new Dictionary<string, object>() { {"Id", args.Id} });
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var crmDeleteContactResult = await Crm.DeleteContact(id:data.Id);
                if (crmDeleteContactResult != null) {
                    grid0.Reload();
}
            }
            catch (System.Exception crmDeleteContactException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Contact");
            }
        }
    }
}
