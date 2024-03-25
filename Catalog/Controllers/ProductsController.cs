using Catalog.Context;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
       
                var products = await _context.products.AsNoTracking().ToListAsync();
                if(products == null)
                {
                    return BadRequest();
                }
                return products;
            

        }

        [HttpGet("{id:int}", Name ="getProduct")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.products.AsNoTracking().FirstOrDefaultAsync(e => e.ProductId == id);
            if(product == null)
            {
                return BadRequest();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product) 
        {
            if(product == null) 
            {
                return BadRequest();
            }
            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("getProduct", new {id = product.ProductId}, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Product product) 
        {
            if(product.ProductId != id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = _context.products.FirstOrDefault(e => e.ProductId == id);
            if( product == null )
            {
                return BadRequest();
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
