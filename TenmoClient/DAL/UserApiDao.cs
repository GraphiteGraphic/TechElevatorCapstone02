using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.DAL
{  
    class UserApiDao
    {
        private RestClient client;
        private API_User User;

        public UserApiDao(string api_url, API_User user)
        {
            client = new RestClient(api_url);
            User = user;
            client.Authenticator = new JwtAuthenticator(User.Token);

        }
        public Dictionary<int, string> GetUsers()
        {
            RestRequest request = new RestRequest($"user", DataFormat.Json);
            IRestResponse<Dictionary<int, string>> accountResponse = client.Get<Dictionary<int, string>>(request);   // This de-serializes json into Exchange
            return accountResponse.Data;
        }
    }
}
