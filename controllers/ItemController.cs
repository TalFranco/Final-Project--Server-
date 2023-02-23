using Final_project_server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_project_server.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // GET: api/<ItemController>
        [HttpGet]
        public IActionResult Get()
        {
            Item item = new Item();
            List<Item> itemsList = item.Read();

            if (itemsList.Count > 0)
            {
                return Ok(itemsList);
            }
            else
            {
                return NotFound("No items yet");
            }
        }

        // GET api/<ItemController>/5
        [HttpGet("ClosetId/{ClosetId}")]
        public IActionResult Get(int ClosetId)
        {
            Item item = new Item();
            item.Closet_ID = ClosetId;
            List<Item> itemsList = item.ReadByCloset();
            if (itemsList.Count > 0)
            {
                return Ok(itemsList);
            }
            else
            {
                return NotFound("No items yet");

            }
        }

        // POST api/<ItemController>
        [HttpPost]
        public bool Post([FromBody] Item item)
        {
            return item.Insert();

        }

        // PUT api/<ItemController>/5
        [HttpPut]
        public bool Put([FromBody] Item item)
        {
            return item.Update();

        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
