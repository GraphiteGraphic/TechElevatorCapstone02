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

            [HttpGet("{userId}")]
            public IActionResult GetTransfers(int userId)
            {
                Dictionary<int, Transfer> transfers = transferDAO.GetTransfers(userId);

                if (transfers != null)
                {
                    return Ok(transfers);
                }

                return NotFound();
            }
        }
}
