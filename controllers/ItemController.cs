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
                return NotFound("No users yet");
            }
        }

        // GET api/<ItemController>/5
        [HttpGet("{Closet_ID}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        public IActionResult Get(int ClosetId)
        {
            Item item = new Item();
            item.Closet_ID= ClosetId;
            List<Item> itemsList = item.ReadByCloset();
            if (itemsList.Count > 0)
            {
                return Ok(itemsList);
            }
            else
            {
                return NotFound();

            }
        }

        // POST api/<ItemController>
        [HttpPost]
        public bool Post([FromBody] Item item)
        {
            return item.Insert();

        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
