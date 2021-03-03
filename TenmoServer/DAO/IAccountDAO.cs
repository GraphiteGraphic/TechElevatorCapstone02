using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        List<Account> GetAccounts(int user_id);
        decimal TransferMoney(int account_to, int account_from, decimal amount);
    }
}
