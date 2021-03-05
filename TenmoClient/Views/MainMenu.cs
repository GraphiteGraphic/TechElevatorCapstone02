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

            consoleServices.PrintAccounts(list);
            
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

        //These fields are only used in the following Method
        public int selectionId = 0;
        public Account acctSelection = null;

        private MenuOptionResult SendTEBucks()
        {
            //Creates new transfer object (defaults status to approved and type to sent)
            Transfer newTransfer = new Transfer();
            newTransfer.TransferStatusID = 2;
            newTransfer.TransferTypeID = 2;

            UserApiDao u = new UserApiDao(API_BASE_URL, User);    

            List<API_User> names = u.GetUsers();
            Dictionary<int, API_User> users = consoleServices.NamesToDiction(names);

            //User selects recipient
            UserSelectionMenu selectMenu = new UserSelectionMenu(users, User.Username, this);
            selectMenu.Show();

            if (selectionId == 0)
            {
                return MenuOptionResult.DoNotWaitAfterMenuSelection;
            }

            newTransfer.AccountTo.AccountId = selectionId;

            //User selects which balance to send from
            AccountApiDao acc = new AccountApiDao(API_BASE_URL, User);
            List<Account> list = acc.GetAccounts();

            AccountSelectionMenu acctSelect = new AccountSelectionMenu(list, this);
            acctSelect.Show();

            if (acctSelection == null)
            {
                return MenuOptionResult.DoNotWaitAfterMenuSelection;
            }

            newTransfer.AccountFrom = acctSelection;

            //User input amount of TE Bucks to send
            consoleServices.PrintBalance(acctSelection);
            decimal amount = GetDecimal("\nEnter amount to transfer: ", 0);
            while(amount > acctSelection.Balance || amount <= 0)
            {
                if (amount < 0)
                {
                    consoleServices.ErrorMessage();
                }
                else if (amount == 0)
                {
                    Console.WriteLine("Transfer Cancelled");
                    return MenuOptionResult.WaitAfterMenuSelection;
                }
                else if (amount > acctSelection.Balance)
                {
                    Console.WriteLine("Error: Attempt to overdraft");
                }

                amount = GetDecimal("\nEnter amount to transfer: ");
            }

            newTransfer.Amount = amount;

            TransferApiDao trans = new TransferApiDao(API_BASE_URL, User);
            decimal newBal = trans.TransferMoney(newTransfer);

            consoleServices.TransferComplete(newBal);

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult RequestTEBucks()
        {
            //Logic follows similar to SendTEBucks
                //Sub-menu for User selection
                //Sub-menu for account selection
                //Input amount to request (No check-logic required)
                //TransferApiDao requires RequestMoney method
                    //Generate transfer object and Post request (localhost:#####/transfer)
                        //Modify TransferMoney to accept transfer status id and type id 

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
