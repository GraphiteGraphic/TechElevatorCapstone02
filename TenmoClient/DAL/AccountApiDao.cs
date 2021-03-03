using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.DAL
{
    public class AccountApiDao : IAccount
    {
        private RestClient client;
        private API_User User;

        public AccountApiDao(string api_url, API_User user)
        {
            client = new RestClient(api_url);
            User = user;
            client.Authenticator = new JwtAuthenticator(User.Token);

        }
        public List<Account> GetAccounts (int user_id)
        {
            RestRequest request = new RestRequest($"account/{user_id}", DataFormat.Json);
            IRestResponse <List<Account>> accountResponse = client.Get<List<Account>>(request);   // This de-serializes json into Exchange
            return accountResponse.Data;
        }

        public Decimal TransferMoney(int from_account, int to_account, decimal amount)
        {
            RestRequest request = new RestRequest($"account/{from_account}/{to_account}/{amount}", DataFormat.Json);
            IRestResponse<Decimal> response = client.Post<Decimal>(request);
            return response.Data;
        }
    }
}
