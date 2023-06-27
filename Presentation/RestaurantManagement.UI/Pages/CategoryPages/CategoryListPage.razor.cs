using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.UI.Utils.Services.Interfaces;

namespace RestaurantManagement.UI.Pages.CategoryPages
{
    public partial class CategoryListPage
    {
        [Inject]
        private ICategoryService service { get; set; }

        bool isLoading;
        int count;
        IEnumerable<Category> categories;
        IList<Category> selectedCategories;
        RadzenDataGrid<Category> grid;

        List<string> titles = new List<string> { "Sales Representative", "Vice President, Sales", "Sales Manager", "Inside Sales Coordinator" };
        IEnumerable<string> selectedTitles;

        async Task OnSelectedTitlesChange(object value)
        {
            if (selectedTitles != null && !selectedTitles.Any())
            {
                selectedTitles = null;
            }

            await grid.FirstPage();
        }

        async Task OnSelectedCategoryChange(object value)
        {
            if (selectedCategories != null && !selectedCategories.Any())
            {
                selectedCategories = null;
            }

            await grid.FirstPage();
        }

        async Task LoadData(LoadDataArgs args)
        {
            isLoading = true;

            
            var result = await service.GetAllAsync(filter: args.Filter, top: args.Top, skip: args.Skip, orderby: args.OrderBy, count: true);
            // Update the Data property
            categories = result.AsODataEnumerable();
            // Update the count
            count = result.Count();

            isLoading = false;
        }
    }
}
