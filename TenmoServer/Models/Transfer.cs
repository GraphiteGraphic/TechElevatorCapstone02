using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferID { get; set; }
        public int TransferTypeID { get; set; }
        public int TransferStatusID { get; set; }
        public int AccountFrom { get; set; }
        public string FromUsername { get; set; }
        public decimal AcctFromBal { get; set; }
        public int AccountTo { get; set; }
        public string ToUsername { get; set; }
        public decimal Amount { get; set; }

    }
}
