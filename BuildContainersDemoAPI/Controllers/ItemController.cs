using BuildContainersDemoAPI.Models;
using BuildContainersDemoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildContainersDemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService) =>
            _itemService = itemService;

        [HttpGet]
        public async Task<List<Item>> Get() =>
            await _itemService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Item>> Get(string id)
        {
            var item = await _itemService.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Item newItem)
        {
            await _itemService.CreateAsync(newItem);

            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Item updatedItem)
        {
            var item = await _itemService.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            updatedItem.Id = item.Id;

            await _itemService.UpdateAsync(id, updatedItem);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _itemService.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            await _itemService.RemoveAsync(id);

            return NoContent();
        }
    }
}
