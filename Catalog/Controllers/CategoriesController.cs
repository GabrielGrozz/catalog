using Catalog.Context;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //a classe herdad controller base nos fornece varias propriedades e métodos que são usados nos métodos http
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context) { _context = context; }

        [HttpGet]
        // o action result é importante para nos dar acesso aos métodos action como notFound, Ok ...
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            try
            {
                var categories = await _context.categories.AsNoTracking().ToListAsync();
                if (categories is null)
                {
                    return NotFound();
                }

                return categories;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno de servido, Aguarde até o sistema voltar");
            }

        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Category>>> GetWithProducts() 
        {
            var categories = await _context.categories.AsNoTracking().Include(e => e.Products).ToListAsync();
            return categories;
        }

        [HttpGet("{id:int}", Name = "getCategory")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _context.categories.AsNoTracking().FirstOrDefaultAsync(e => e.CategoryId == id);
            if(category is null){
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            //a verificação do modelo(ModelState.IsValid) é feita automaticamente por causa do atributo [ApiController]

            if( category is null)
            {
                return BadRequest("A categoria informada é inválida");
            }

            _context.categories.Add(category);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("getCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        //nesse método put nós retornamos um tipo específico, poderiamos retornar tbm um tipo complexo, porém retornar um tipo específico não é muito eficaz
        public async Task<string> Put(int id, Category category) 
        {
            if(category.CategoryId != id)
            {
                return "invalido" ;
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return "OK!";
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Category category = _context.categories.FirstOrDefault(e => e.CategoryId == id);
            if(category is null)
            {
                return NotFound();
            }

            _context.categories.Remove(category); 
            await _context.SaveChangesAsync();

            return Ok("a categoria foi removida com sucesso");
        }
    }
}
