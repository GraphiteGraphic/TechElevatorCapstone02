using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("{account_from}/{account_to}/{amount}")]
        public IActionResult TransferMoney(int account_to, int account_from, decimal amount)
        { 
            decimal money = accountDAO.TransferMoney(account_to, account_from, amount);
            if (money >= 0)
            {
                return Ok(money);
            }
            return NotFound();
        }
    }
}