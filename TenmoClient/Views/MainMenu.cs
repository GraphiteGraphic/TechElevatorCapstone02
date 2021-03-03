using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.DAL;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    public class MainMenu : ConsoleMenu
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly API_User User = null;

        public MainMenu(API_User user)
        {
            this.User = user;

            AddOption("View your current balance", ViewBalance)
                .AddOption("View your past transfers", ViewTransfers)
                .AddOption("View your pending requests", ViewRequests)
                .AddOption("Send TE bucks", SendTEBucks)
                .AddOption("Request TE bucks", RequestTEBucks)
                .AddOption("Log in as different user", Logout)
                .AddOption("Exit", Exit);
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        {
            AccountApiDao acc = new AccountApiDao(API_BASE_URL, User);
            List<Account> list = acc.GetAccounts(User.UserId);

            foreach (Account a in list)
            {
                Console.WriteLine($"{a.Balance}, {a.AccountId}");
            }
            
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewRequests()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            UserApiDao u = new UserApiDao(API_BASE_URL);
            
            //Attempt to use submenu to select transfer target
            //UserSelectionMenu selection = new UserSelectionMenu(u.GetUsers(), User.Username);
            //selection.Show();

            Dictionary<int, string> names = u.GetUsers();
            foreach(KeyValuePair<int, string> kvp in names)
            {
                Console.WriteLine($"{kvp.Key}||       {kvp.Value}");
            }

            int toAccount = 0;
            while (!names.ContainsKey(toAccount))
            {
                toAccount = GetInteger("\nPlease input the ID of the User you are sending TEBucks to: ");
                
                if (!names.ContainsKey(toAccount))
                {
                    Console.WriteLine("Error: Invalid ID");
                }
            }

            AccountApiDao acc = new AccountApiDao(API_BASE_URL, User);
            List<Account> list = acc.GetAccounts(User.UserId);

            foreach (Account a in list)
            {
                Console.WriteLine($"Current Balance|| ${a.Balance}");
            }

            decimal amount = GetDecimal("\nEnter amount to transfer: ");
            while(amount > list[0].Balance || amount == 0)
            {
                if (amount == 0)
                {
                    Console.WriteLine("Error: Cannot transfer $0");
                }
                else if (amount > list[0].Balance)
                {
                    Console.WriteLine("Error: Attempt to overdraft");
                }

                amount = GetDecimal("\nEnter amount to transfer: ");
            }
            
            acc.TransferMoney(list[0].AccountId, toAccount, amount);

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult RequestTEBucks()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Logout()
        {
            UserService.SetLogin(new API_User()); //wipe out previous login info
            return MenuOptionResult.CloseMenuAfterSelection;
        }

    }
}
