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
    public partial class EditCategoryModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string CategoryID { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<Category> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox categoryName;

        protected RadzenRequiredValidator categoryNameRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox description;

        protected RadzenLabel label4;

        protected RadzenTextBox picture;

        protected RadzenButton button2;

        protected RadzenButton button3;

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if(_canEdit != value)
                {
                    _canEdit = value;
                    StateHasChanged();
                }
            }
        }

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
            canEdit = true;

            var northwindGetCategoryByCategoryIdResult = await Northwind.GetCategoryByCategoryId(int.Parse(CategoryID));
                category = northwindGetCategoryByCategoryIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Categories");
        }

        protected async void Form0Submit(Category args)
        {
            var northwindUpdateCategoryResult = await Northwind.UpdateCategory(int.Parse(CategoryID), category);
                UriHelper.NavigateTo("Categories");
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Categories");
        }
    }
}
