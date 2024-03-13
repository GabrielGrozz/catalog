﻿using Catalog.Context;
using Catalog.Models;
using Microsoft.AspNetCore.Http;
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
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.categories.ToList();
            if (categories is null)
            {
                return NotFound();
            }

            return categories;
        }

        [HttpGet("{id:int}", Name = "getCategory")]
        public ActionResult<Category> GetById(int id)
        {
            var category = _context.categories.FirstOrDefault(e => e.CategoryId == id);
            if(category is null){
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            //a verificação do modelo(ModelState.IsValid) é feita automaticamente por causa do atributo [ApiController]

            if( category is null)
            {
                return BadRequest("A categoria informada é inválida");
            }

            _context.categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("getCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category) 
        {
            if(category.CategoryId != id)
            {
                return BadRequest("O id informado é inválido");
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            Category category = _context.categories.FirstOrDefault(e => e.CategoryId == id);
            if(category is null)
            {
                return NotFound();
            }

            _context.categories.Remove(category); 
            _context.SaveChanges();

            return Ok("a categoria foi removida com sucesso");
        }
    }
}