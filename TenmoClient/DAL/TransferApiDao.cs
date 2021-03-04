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
            client = new RestClient(api_url);
            User = user;
            client.Authenticator = new JwtAuthenticator(User.Token);
        }

        public Dictionary<int, Transfer> GetTransfers(int user_id)
        {
            RestRequest request = new RestRequest($"transfer/{user_id}", DataFormat.Json);
            IRestResponse<Dictionary<int, Transfer>> transferResponse = client.Get<Dictionary<int, Transfer>>(request);   // This de-serializes json into Exchange
            return transferResponse.Data;
        }
    }
}
