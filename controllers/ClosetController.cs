using Final_project_server.Models;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_project_server.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClosetController : ControllerBase
    {
        // GET: api/<ClosetController>
        [HttpGet]
        public IActionResult Get()
        {
            Closet closet = new Closet();
            List<Closet> closetList = closet.Read();

            if (closetList.Count > 0)
            {
                return Ok(closetList);
            }
            else
            {
                return NotFound("No closets yet");
            }
        }

        // GET api/<ClosetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClosetController>
        [HttpPost]
        public bool Post([FromBody] Closet closet)
        {
            return closet.Insert();
        }

        // PUT api/<ClosetController>/5
        [HttpPut]
        public bool Put([FromBody] Closet closet)
        {
            return closet.Update();

        }

        // DELETE api/<ClosetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
