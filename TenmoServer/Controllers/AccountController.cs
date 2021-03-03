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

        [HttpPost("{userID}/{accountID)")]
        public IActionResult TransferMoney(int account_from, int account_to, decimal amount)
        { 
            decimal money = accountDAO.TransferMoney(account_from, account_to, amount);
            if (money >= 0)
            {
                return Ok(money);
            }
            return NotFound();
        }
    }
}