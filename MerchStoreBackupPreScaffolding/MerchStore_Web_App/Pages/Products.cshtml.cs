using MerchStore.Models;
using MerchStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MerchStore_Web_App.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly MerchService _merchService;

        public ProductsModel(MerchService merchService)
        {
            _merchService = merchService;
        }

        public List<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = (await _merchService.GetAsyncQueryable()).ToList();
        }
    }
}
