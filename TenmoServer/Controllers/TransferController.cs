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
            List<Transfer> transfers = transferDAO.GetTransfers(userId);

            if (transfers != null)
            {
                return Ok(transfers);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Transfer> TransferMoney(Transfer transfer)
        {
            if (transfer.TransferTypeID == 2 && transfer.AccountFrom.AccountId != int.Parse(User.FindFirst("sub").Value))
            {
                return Forbid();
            }
            else if (transfer.TransferTypeID == 1 && transfer.AccountTo.AccountId != int.Parse(User.FindFirst("sub").Value))
            {
                return Forbid();
            }

            if (transfer.TransferTypeID == 2 && transfer.AccountFrom.Balance >= transfer.Amount && transfer.Amount > 0)
            {
                decimal money = transferDAO.TransferMoney(transfer);
                return Created("transfer",money);
            }

            if (transfer.TransferTypeID == 1)
            {
                return Created("transfer", transferDAO.RequestMoney(transfer));
            }

            return BadRequest();
        }
    }
}
