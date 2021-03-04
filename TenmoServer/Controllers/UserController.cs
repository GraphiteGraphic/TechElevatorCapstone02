using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserDAO userDAO;

        public UserController(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<User> accounts = userDAO.GetUsers();

            Dictionary<int, string> names = new Dictionary<int, string> { };

            foreach(User u in accounts)
            {
                names.Add(u.UserId, u.Username);
            }

            if (names != null)
            {
                return Ok(names);
            }

            return NotFound();
        }
    }
}
