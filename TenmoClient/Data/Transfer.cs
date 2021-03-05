using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    class Transfer
    {
        public int TransferID { get; set; }
        public int TransferTypeID { get; set; }
        public int TransferStatusID { get; set; }
        public Account AccountFrom { get; set; } = new Account();
        public string FromUsername { get; set; }
        public Account AccountTo { get; set; } = new Account();
        public string ToUsername { get; set; }
        public decimal Amount { get; set; }
    }
}
