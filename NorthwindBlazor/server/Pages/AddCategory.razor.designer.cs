using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.Pages
{
    public partial class AddCategoryComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

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
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        protected override async Task OnInitAsync()
        {
            await Task.Run(Load);
        }

        protected async void Load()
        {
            category = new Category();
        }

        protected async void Form0Submit(Category args)
        {
            var northwindCreateCategoryResult = await Northwind.CreateCategory(category);
                DialogService.Close(category);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
