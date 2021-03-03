using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.DAL
{
    class UserApiDao
    {
        private RestClient client;
        public UserApiDao(string api_url)
        {
            client = new RestClient(api_url);
        }
        public Dictionary<int, string> GetUsers()
        {
            RestRequest request = new RestRequest($"user", DataFormat.Json);
            IRestResponse<Dictionary<int, string>> accountResponse = client.Get<Dictionary<int, string>>(request);   // This de-serializes json into Exchange
            return accountResponse.Data;
        }
    }
}
