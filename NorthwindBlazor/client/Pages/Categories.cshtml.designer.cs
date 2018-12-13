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
    public partial class CategoriesModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getCategoriesCount;
        protected int getCategoriesCount
        {
            get
            {
                return _getCategoriesCount;
            }
            set
            {
                if(_getCategoriesCount != value)
                {
                    _getCategoriesCount = value;
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

        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("AddCategory");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetCategoriesResult = await Northwind.GetCategories(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"", null, null);
                getCategoriesResult = northwindGetCategoriesResult.Data;

                getCategoriesCount = northwindGetCategoriesResult.Count;
        }

        protected async void Grid0RowSelect(Category args)
        {
            UriHelper.NavigateTo($"EditCategory/{args.CategoryID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Category data)
        {
            var northwindDeleteCategoryResult = await Northwind.DeleteCategory(data.CategoryID);
                if (northwindDeleteCategoryResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
