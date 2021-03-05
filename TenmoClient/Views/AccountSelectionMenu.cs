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
        private int transferType;
        
        public AccountSelectionMenu(List<Account> accounts, MainMenu mainMenu, int transferType)
        {
            parent = mainMenu;
            this.accounts = accounts;
            this.transferType = transferType;

            foreach (Account account in accounts)
            { 
                AddOption<Account>($"{account.AccountId}||   {account.Balance}", ReturnAccount, account);
            }

            AddOption("Exit", ExitSelect);
        }

        protected override void OnBeforeShow()
        {
            if (transferType == 2)
            {
                Console.WriteLine("Please Select the Account to Send from\n");
            }
            else
            {
                Console.WriteLine("Please Select the Receiving Account\n");
            }
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
