using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    class ConsoleServices
    {
        public void PrintBalance(List<Account> list)
        {
            foreach (Account a in list)
            {
                Console.WriteLine($"{a.Balance}, {a.AccountId}");
            }
        }

        public void PrintTransfers(Dictionary<int, Transfer> transfers)
        {
            foreach (KeyValuePair<int, Transfer> t in transfers)
            {
                Console.WriteLine($"{t.Value}");
            }
        }

        public void PrintUsers(Dictionary<int, string> names, API_User user)
        {
            foreach (KeyValuePair<int, string> kvp in names)
            {
                if (kvp.Value == user.Username)
                {
                    continue;
                }
                Console.WriteLine($"{kvp.Key} ||  {kvp.Value}");
            }
        }

        public void TransferComplete(decimal newBal)
        {
            Console.WriteLine($"\n||Transfer completed||\nCurrent Balance: ${newBal}");
        }
    }
}
