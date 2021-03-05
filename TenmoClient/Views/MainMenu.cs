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
        private ConsoleServices consoleServices = new ConsoleServices();

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

            Configure(cfg =>
            {
                cfg.ItemForegroundColor = ConsoleColor.Blue;
                cfg.SelectedItemForegroundColor = ConsoleColor.White;
            });
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        {
            AccountApiDao acc = new AccountApiDao(API_BASE_URL, User);
            List<Account> list = acc.GetAccounts();

            consoleServices.PrintBalance(list);
            
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            TransferApiDao tran = new TransferApiDao(API_BASE_URL, User);
            Dictionary<int, Transfer> list = tran.GetTransfers();

            consoleServices.PrintTransfers(list, User);
            Console.WriteLine();
            Console.WriteLine( );
            int transId = GetInteger("To view more details, input transfer ID #: ", 0);
            if (transId == 0)
            {
                return MenuOptionResult.DoNotWaitAfterMenuSelection;
            }
            while (!list.ContainsKey(transId))
            {
                consoleServices.ErrorMessage();
                transId = GetInteger("To view more details, input transfer ID #: ", 0);
            }

            consoleServices.TransferDetails(list[transId], User);

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewRequests()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            UserApiDao u = new UserApiDao(API_BASE_URL, User);
            
            //Attempt to use submenu to select transfer target
            //UserSelectionMenu selection = new UserSelectionMenu(u.GetUsers(), User.Username);
            //selection.Show();

            Dictionary<int, string> names = u.GetUsers();
            consoleServices.PrintUsers(names, User);

            int toAccount = 0;
            while (!names.ContainsKey(toAccount))
            {
                toAccount = GetInteger("\nPlease input the ID of the User you are sending TEBucks to: ");
                
                if (!names.ContainsKey(toAccount))
                {
                    consoleServices.ErrorMessage();
                }
            }

            AccountApiDao acc = new AccountApiDao(API_BASE_URL, User);
            List<Account> list = acc.GetAccounts();

            consoleServices.PrintBalance(list);

            decimal amount = GetDecimal("\nEnter amount to transfer: ");
            while(amount > list[0].Balance || amount <= 0)
            {
                if (amount <= 0)
                {
                    consoleServices.ErrorMessage();
                }
                else if (amount > list[0].Balance)
                {
                    Console.WriteLine("Error: Attempt to overdraft");
                }

                amount = GetDecimal("\nEnter amount to transfer: ");
            }

            TransferApiDao trans = new TransferApiDao(API_BASE_URL, User);
            decimal newBal = trans.TransferMoney(list[0].AccountId, toAccount, amount, list[0].Balance);

            consoleServices.TransferComplete(newBal);

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
