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
    public class TransferController : Controller
    {
        private readonly ITransferDAO transferDAO;

        public TransferController(ITransferDAO transferDAO)
        {
            this.transferDAO = transferDAO;
        }

        [HttpGet]
        public IActionResult GetTransfers()
        {
            int userId = int.Parse(User.FindFirst("sub").Value);
            Dictionary<int, Transfer> transfers = transferDAO.GetTransfers(userId);

            if (transfers != null)
            {
                return Ok(transfers);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Transfer> TransferMoney(Transfer transfer)
        {
            if (transfer.AccountFrom != int.Parse(User.FindFirst("sub").Value))
            {
                return Forbid();
            }
            
            if (transfer.AcctFromBal >= transfer.Amount && transfer.Amount > 0)
            {
                decimal money = transferDAO.TransferMoney(transfer);
                return Created("transfer",money);
            }
            
            return BadRequest();
        }
    }
}
