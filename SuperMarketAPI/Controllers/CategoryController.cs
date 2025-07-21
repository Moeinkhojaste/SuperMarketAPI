using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMarketAPI.Data;
using SuperMarketAPI.Models;
using SuperMarketAPI.Models.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace SuperMarketAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories
                .Include(c => c.Products)
                .ToList();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryCreateDTO categoryDto)
        {

            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateCategory(int id, CategoryUpdateDTO updateDto)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();
            _mapper.Map(updateDto, category);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            if (category.Products != null && category.Products.Any())
                return BadRequest("cannot delete category that has products in it");

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();

        }
            


    }
}
