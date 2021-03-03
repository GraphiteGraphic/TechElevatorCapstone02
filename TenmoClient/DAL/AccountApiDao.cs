using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.DAL
{
    class AccountApiDao
    {
        private RestClient client;
        public AccountApiDao(string api_url)
        {
            client = new RestClient(api_url);
        }


    }
}
