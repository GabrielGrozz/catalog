using Catalog.Context;
using Catalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
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

        [HttpGet("{value:alpha:length(5)}")]
        public ActionResult<IEnumerable<Product>> Get(string value)
        {
            var param = value;
            var products = _context.products.AsNoTracking().ToList();
            if(products == null)
            {
                return BadRequest();
            }
            return products;
        }

        [HttpGet("{id:int}", Name ="getProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.products.AsNoTracking().FirstOrDefault(e => e.ProductId == id);
            if(product == null)
            {
                return BadRequest();
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product) 
        {
            if(product == null) 
            {
                return BadRequest();
            }
            _context.products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("getProduct", new {id = product.ProductId}, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product) 
        {
            if(product.ProductId != id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _context.products.FirstOrDefault(e => e.ProductId == id);
            if( product == null )
            {
                return BadRequest();
            }

            _context.Remove(product);
            _context.SaveChanges();

            return Ok();
        }


    }
}
