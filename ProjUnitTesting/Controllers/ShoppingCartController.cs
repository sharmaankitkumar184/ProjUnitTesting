using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjUnitTesting.Models;
using ProjUnitTesting.Services;

namespace ProjUnitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;
        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<ShoppingItem> Get()
        {
            var items = _service.GetAllItems();
            return items;
        }

        [HttpGet("{id}")]
        public ShoppingItem Get(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        [HttpPost]
        public ShoppingItem Post([FromBody] ShoppingItem value)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return null;
            }
            var item =_service.Add(value);
            return item;
            //return CreatedAtAction("Get", new { id = item.Id }, item);
        }
        [HttpPut]
        public IActionResult Put(ShoppingItem shoppingData)
        {
            try
            {
                var productToUpdate = _service.GetById(shoppingData.Id);

                if (productToUpdate == null)
                    return NotFound($"Product with Id = {shoppingData.Id} not found");
                var item = _service.Update(shoppingData);
                return CreatedAtAction("Get", new { id = item.Id }, item);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        [HttpDelete("{id}")]
        public List<ShoppingItem> Remove(int id)
        {
            var existingItem = _service.GetById(id);
            if (existingItem == null)
            {
                //return NotFound();
                return null;
            }
            var data=_service.Remove(id);
            return data;
            //return NoContent();
        }
    }
}
