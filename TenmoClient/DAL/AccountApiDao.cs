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
            client = new RestClient($"{api_url}account");
            User = user;
            client.Authenticator = new JwtAuthenticator(User.Token);

        }
        public List<Account> GetAccounts ()
        {
            RestRequest request = new RestRequest();
            IRestResponse <List<Account>> accountResponse = client.Get<List<Account>>(request);   // This de-serializes json into Exchange
            return accountResponse.Data;
        }

    }
}
