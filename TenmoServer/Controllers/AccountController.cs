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
    public class AccountController : Controller
    {
        private readonly IAccountDAO accountDAO;

        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }

        [HttpGet("{userId}")]
        public IActionResult GetAccounts(int userId)
        {
            List<Account> accounts = accountDAO.GetAccounts(userId);

            if (accounts != null)
            {
                return Ok(accounts);
            }

            return NotFound();
        }
    }
}
