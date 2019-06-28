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
    public partial class CategoriesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<Category> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Category> _getCategoriesResult;
        protected IEnumerable<Category> getCategoriesResult
        {
            get
            {
                return _getCategoriesResult;
            }
            set
            {
                if(_getCategoriesResult != value)
                {
                    _getCategoriesResult = value;
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
            var northwindGetCategoriesResult = await Northwind.GetCategories();
                getCategoriesResult = northwindGetCategoriesResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddCategory>("Add Category", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Category args)
        {
            var result = await DialogService.OpenAsync<EditCategory>("Edit Category", new Dictionary<string, object>() { {"CategoryID", $"{args.CategoryID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Category data)
        {
            var northwindDeleteCategoryResult = await Northwind.DeleteCategory(data.CategoryID);
                if (northwindDeleteCategoryResult != null) {
                    grid0.Reload();
}
        }
    }
}
