using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database.Query;
using ProductC.Services;
using ProductC.Models;

namespace ProductC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;
        private const string CollectionName = "products";

        public ProductController(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _firebaseService.Client
                .Child(CollectionName)
                .OnceAsync<Product>();

            return products.Select(p =>
            {
                p.Object.Id = p.Key;
                return p.Object;
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await _firebaseService.Client
                .Child(CollectionName)
                .Child(id)
                .OnceSingleAsync<Product>();

            if (product == null) return NotFound();

            product.Id = id;
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Category = productDto.Category,
                Stock = productDto.Stock,
                ImageUrl = productDto.ImageUrl
            };

            var result = await _firebaseService.Client
                .Child(CollectionName)
                .PostAsync(product);

            product.Id = result.Key;

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Id))
                return BadRequest("Product ID is required for update");

            await _firebaseService.Client
                .Child(CollectionName)
                .Child(product.Id)
                .PutAsync(product);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _firebaseService.Client
                .Child(CollectionName)
                .Child(id)
                .DeleteAsync();

            return Ok();
        }
    }
}