using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;
using ProductsApi.Models;
using ProductsAPI.Models;
using ProductsAPI.Exceptions;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> Products = new()
        {
            new Product { Id = 1, Name = "Keyboard", Price = 800, SalePrice = 700 },
            new Product { Id = 2, Name = "Mouse", Price = 400, SalePrice = 350 },
            new Product { Id = 3, Name = "Monitor", Price = 5000, SalePrice = 4500 }
        };

        private static int nextId = 4;

        // GET: api/products
        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid pagination parameters",
                    Detail = "Page must be at least 1 and pageSize must be between 1 and 100."
                });
            }

            int totalItems = Products.Count;

            var data = Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                data,
                meta = new
                {
                    page,
                    pageSize,
                    totalItems,
                    totalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                }
            };

            return Ok(response);
        }

        // GET: api/products/1

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException(
                    $"Product with ID {id} was not found.");
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> Create(CreateProductRequest request)
        {
            Product product = new Product
            {
                Id = nextId++,
                Name = request.Name,
                Price = request.Price,
                SalePrice = request.SalePrice
            };

            Products.Add(product);

            return Ok(product);
        }

        // PUT: api/products/1

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProductRequest request)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException(
                    $"Product with ID {id} was not found.");
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.SalePrice = request.SalePrice;

            return Ok(product);
        }


        // DELETE: api/products/1

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new NotFoundException(
                    $"Product with ID {id} was not found.");
            }

            Products.Remove(product);

            return NoContent();
        }

    }
}