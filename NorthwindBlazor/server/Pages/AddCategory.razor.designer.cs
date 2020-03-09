using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class AddCategoryComponent : ComponentBase
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
        protected NorthwindService Northwind { get; set; }

        NorthwindBlazor.Models.Northwind.Category _category;
        protected NorthwindBlazor.Models.Northwind.Category category
        {
            get
            {
                return _category;
            }
            set
            {
                if(!object.Equals(_category, value))
                {
                    _category = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            category = new NorthwindBlazor.Models.Northwind.Category();
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.Category args)
        {
            try
            {
                var northwindCreateCategoryResult = await Northwind.CreateCategory(category);
                DialogService.Close(category);
            }
            catch (Exception northwindCreateCategoryException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Category!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
