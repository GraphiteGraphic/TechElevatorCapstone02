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
            IRestResponse<List<Transfer>> transferResponse = client.Get<List<Transfer>>(request);   // This de-serializes json into Exchange

            Dictionary<int, Transfer> transfers = new Dictionary<int, Transfer> { };
            foreach(Transfer transfer in transferResponse.Data)
            {
                transfers.Add(transfer.TransferID, transfer);
            }

            return transfers;
        }

        public Decimal TransferMoney(Transfer newTransfer)
        {
            RestRequest request = new RestRequest();
            
            request.AddJsonBody(newTransfer);
            IRestResponse<Decimal> response = client.Post<Decimal>(request);
            return response.Data;
        }
    }
}
