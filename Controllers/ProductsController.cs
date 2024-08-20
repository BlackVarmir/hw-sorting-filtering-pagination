using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hw_sorting_filtering_pagination.Data;
using hw_sorting_filtering_pagination.Models;

namespace hw_sorting_filtering_pagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string sortOrder, string searchString, int pageNumber = 1, int pageSize = 10)
        {
            var products = from p in _context.Products select p;

            if (!string.IsNullOrEmpty(sortOrder))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            switch (sortOrder) 
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            var count = await products.CountAsync();
            var items = await products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new
            {
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Items = items
            });
        }
    }
}
