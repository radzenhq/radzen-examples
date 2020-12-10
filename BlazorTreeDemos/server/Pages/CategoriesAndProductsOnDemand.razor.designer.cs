using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using TreeDemos.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace TreeDemos.Pages
{
    public partial class CategoriesAndProductsOnDemandComponent : ComponentBase
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
        protected NorthwindService Northwind { get; set; }

        IEnumerable<TreeDemos.Models.Northwind.Category> _categories;
        protected IEnumerable<TreeDemos.Models.Northwind.Category> categories
        {
            get
            {
                return _categories;
            }
            set
            {
                if (!object.Equals(_categories, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "categories", NewValue = value, OldValue = _categories };
                    _categories = value;
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
            var northwindGetCategoriesResult = await Northwind.GetCategories();
            categories = northwindGetCategoriesResult;
        }

        protected async System.Threading.Tasks.Task Tree0Expand(TreeExpandEventArgs args)
        {
            var northwindGetProductsResult = await Northwind.GetProducts(new Query() { Filter = $@"i => i.CategoryID == @0", FilterParameters = new object[] { (args.Value as Category).CategoryID } });
            args.Children.Data = northwindGetProductsResult;

            args.Children.TextProperty = "ProductName";

            args.Children.HasChildren = (product) => false;
        }
    }
}
