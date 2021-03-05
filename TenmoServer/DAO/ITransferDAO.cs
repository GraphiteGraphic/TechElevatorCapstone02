using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<Transfer> GetTransfers(int user_id);

        decimal TransferMoney(Transfer transfer);

        int RequestMoney(Transfer transfer);
    }
}