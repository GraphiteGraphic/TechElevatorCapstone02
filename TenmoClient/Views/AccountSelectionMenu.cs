using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    class AccountSelectionMenu : ConsoleMenu
    {
        private MainMenu parent;
        private List<Account> accounts;
        
        public AccountSelectionMenu(List<Account> accounts, MainMenu mainMenu)
        {
            parent = mainMenu;
            this.accounts = accounts;

            foreach (Account account in accounts)
            { 
                AddOption<Account>($"{account.AccountId}||   {account.Balance}", ReturnAccount, account);
            }

            AddOption("Exit", ExitSelect);
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine("Please Select the Recipient\n");
        }

        private MenuOptionResult ReturnAccount(Account selection)
        {
            parent.acctSelection = selection;

            return MenuOptionResult.CloseMenuAfterSelection;
        }

        private MenuOptionResult ExitSelect()
        {
            parent.acctSelection = null;

            return MenuOptionResult.CloseMenuAfterSelection;
        }
    }
}
