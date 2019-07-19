using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using inventory_dot_core.Models;

namespace inventory_dot_core.Api
{
    [Route("api/[controller]")]
    public class UserApiController : Controller
    {
        public class ApiUser
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }
        }
                
        [HttpGet]
        public ApiUser[] Get()
        {
            return new[]
            {
                new ApiUser()
                {
                    Id = 1,
                    Firstname = "Ugo",
                    Lastname = "Lattanzi",
                    Email = "@imperugo"
                },
                new ApiUser()
                {
                    Id = 2,
                    Firstname = "Simone",
                    Lastname = "Chiaretta",
                    Email = "@simonech"
                },
            };
        }

        [HttpGet("{id}")]
        public ApiUser Get(int id)
        {
            var users = new[]
            {
                new ApiUser()
                {
                    Id = 1,
                    Firstname = "Ugo",
                    Lastname = "Lattanzi",
                    Email = "@imperugo"
                },
                new ApiUser()
                {
                    Id = 2,
                    Firstname = "Simone",
                    Lastname = "Chiaretta",
                    Email = "@simonech"
                },
            };

            return users.FirstOrDefault(x => x.Id == id);
        }

        // Adding user
        [HttpPost]
        public IActionResult Update([FromBody] ApiUser user)
        {
            var users = new List<ApiUser>()
            {
                user
            };
            //users.Add(user);
            return new CreatedResult($"/api/users/{user.Id}", user);
        }

        //Deleting user
        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            var users = new List<ApiUser>
            {
                new ApiUser()
                {
                    Id = 1,
                    Firstname = "Ugo",
                    Lastname = "Lattanzi",
                    Email = "@imperugo"
                },
                new ApiUser()
                {
                    Id = 2,
                    Firstname = "Simone",
                    Lastname = "Chiaretta",
                    Email = "@simonech"
                },
            };

            var user = users.SingleOrDefault(x => x.Id == id);
            if (user != null)
            {
                users.Remove(user);
                return new EmptyResult();
            }

            return new NotFoundResult();
        }
    }    
}