using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class ContactsComponent : ComponentBase
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
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected CrmService Crm { get; set; }
        protected RadzenDataGrid<RadzenCrm.Models.Crm.Contact> grid0;

        string _contactFilter;
        protected string contactFilter
        {
            get
            {
                return _contactFilter;
            }
            set
            {
                if (!object.Equals(_contactFilter, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "contactFilter", NewValue = value, OldValue = _contactFilter };
                    _contactFilter = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.Contact> _getContactsResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Contact> getContactsResult
        {
            get
            {
                return _getContactsResult;
            }
            set
            {
                if (!object.Equals(_getContactsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getContactsResult", NewValue = value, OldValue = _getContactsResult };
                    _getContactsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
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

            var crmGetContactsResult = await Crm.GetContacts();
            getContactsResult = crmGetContactsResult;
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            var crmGetContactsResult = await Crm.GetContacts(new Query() { Filter = $@"i => (i.Company.Contains(@0) || i.FirstName.Contains(@1) || i.LastName.Contains(@2) || i.Email.Contains(@3))", FilterParameters = new object[] { contactFilter, contactFilter, contactFilter, contactFilter } });
            getContactsResult = crmGetContactsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddContact>("Add Contact", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(RadzenCrm.Models.Crm.Contact args)
        {
            var dialogResult = await DialogService.OpenAsync<EditContact>("Edit Contact", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmDeleteContactResult = await Crm.DeleteContact(data.Id);
                    if (crmDeleteContactResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception crmDeleteContactException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Contact" });
            }
        }
    }
}
