using Final_project_server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_project_server.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            User user = new User();
            List<User> usersList = user.Read();

            if (usersList.Count > 0)
            {
                return Ok(usersList);
            }
            else
            {
                return NotFound("No users yet");
            }
        }
        [HttpGet("email/{email}/password/{password}")]
        public IActionResult Get(string email, string password)
        {
            User user = new User();
            user.Email = email;
            user.Password = password;
            User resUser = user.Login();
            if (resUser.Full_name == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(resUser);
            }
        }


        // GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<UserController>
        [HttpPost]
        public bool Post([FromBody] User user)
        {
            return user.Insert();

        }


        // PUT api/<UserController>/5
        //[HttpPut]
        //public bool Put([FromBody] User user)
        //{
        //    return user.Update();

        //}

        // DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
    
}
