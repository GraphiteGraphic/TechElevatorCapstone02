using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.DAL
{
    public class AccountApiDao : IAccount
    {
        private RestClient client;
        public AccountApiDao(string api_url)
        {
            client = new RestClient(api_url);
        }
        public List<Account> GetAccounts (int user_id)
        {
            RestRequest request = new RestRequest($"account/{user_id}", DataFormat.Json);
            IRestResponse <List<Account>> accountResponse = client.Get<List<Account>>(request);   // This de-serializes json into Exchange
            return accountResponse.Data;
        }

    }
}
