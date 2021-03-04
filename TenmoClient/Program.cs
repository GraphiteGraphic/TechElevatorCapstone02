using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoClient.Views;

namespace TenmoClient
{
    class Program
    {

        static void Main(string[] args)
        {
            AuthService authService = new AuthService();
            Console.WindowWidth = 210; // makes text legible
            new LoginRegisterMenu(authService).Show();

            Console.WriteLine("\r\nThank you for using TEnmo!!!\r\n");
        }
    }
}
