using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMarketAPI.Data;
using SuperMarketAPI.Models;
using SuperMarketAPI.Models.DTOs;

namespace SuperMarketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            var productRead = _mapper.Map<ProductReadDto>(product);
            return Ok(productRead);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateDto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _mapper.Map(updateDto, product); // Maps only changed fields

            await _context.SaveChangesAsync();
            return NoContent(); // 204: success but nothing to return
        }






        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreateDto productDto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            _context.SaveChanges();

            var productRead = _mapper.Map<ProductReadDto>(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productRead);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
