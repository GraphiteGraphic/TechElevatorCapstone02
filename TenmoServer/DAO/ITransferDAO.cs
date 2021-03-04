using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        Dictionary<int, Transfer> GetTransfers(int user_id);

        decimal TransferMoney(Transfer transfer);
    }
}