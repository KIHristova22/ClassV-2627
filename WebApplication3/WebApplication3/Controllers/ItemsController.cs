using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _context.Items
                .Select(i => new ItemDto(i.Id, i.Name, i.Description, i.Price, i.CreatedAt))
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto dto)
        {
            var item = new Item
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var responseDto = new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedAt);
            return CreatedAtAction(nameof(GetItems), new { id = item.Id }, responseDto);
        }
    }
}