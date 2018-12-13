using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using client.Shared;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.App.Pages
{
    public partial class AddCategoryModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenTemplateForm<Category> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox categoryName;

        protected RadzenRequiredValidator categoryNameRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox description;

        protected RadzenLabel label3;

        protected RadzenTextBox picture;

        protected RadzenButton button1;

        protected RadzenButton button2;

        Category _category;
        protected Category category
        {
            get
            {
                return _category;
            }
            set
            {
                if(_category != value)
                {
                    _category = value;
                    StateHasChanged();
                }
            }
        }

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {
            category = new Category();
        }

        protected async void Form0Submit(Category args)
        {
            var northwindCreateCategoryResult = await Northwind.CreateCategory(category);
                UriHelper.NavigateTo("Categories");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Categories");
        }
    }
}
