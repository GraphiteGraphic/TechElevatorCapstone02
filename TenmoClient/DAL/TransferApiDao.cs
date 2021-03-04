using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.DAL
{
    class TransferApiDao
    {
        private RestClient client;
        private API_User User;

        public TransferApiDao(string api_url, API_User user)
        {
            client = new RestClient($"{api_url}transfer");
            User = user;
            client.Authenticator = new JwtAuthenticator(User.Token);
        }

        public Dictionary<int, Transfer> GetTransfers()
        {
            RestRequest request = new RestRequest();
            IRestResponse<Dictionary<int, Transfer>> transferResponse = client.Get<Dictionary<int, Transfer>>(request);   // This de-serializes json into Exchange
            return transferResponse.Data;
        }

        public Decimal TransferMoney(int from_account, int to_account, decimal amount, decimal balance)
        {
            RestRequest request = new RestRequest();
            
            Transfer newTransfer = new Transfer();
            newTransfer.AccountFrom = from_account;
            newTransfer.AccountTo = to_account;
            newTransfer.Amount = amount;
            newTransfer.AcctFromBal = balance;

            request.AddJsonBody(newTransfer);
            IRestResponse<Decimal> response = client.Post<Decimal>(request);
            return response.Data;
        }
    }
}
