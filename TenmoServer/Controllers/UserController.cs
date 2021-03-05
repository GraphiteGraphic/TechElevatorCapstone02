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
           
            if (accounts != null)
            {
                return Ok(accounts);
            }

            return NotFound();
        }
    }
}
