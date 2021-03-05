using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    class UserSelectionMenu : ConsoleMenu
    {
        private MainMenu parent;
        private int transferType;

        public UserSelectionMenu(Dictionary<int, API_User> names, string username, MainMenu mainMenu, int transferType)
        {
            parent = mainMenu;
            this.transferType = transferType;

            foreach (KeyValuePair<int, API_User> kvp in names)
            {
                if (kvp.Value.Username != username)
                {
                    AddOption<int>(kvp.Value.Username, ReturnName, kvp.Key);
                }
            }

            AddOption("Exit", ExitSelect);
        }

        protected override void OnBeforeShow()
        {
            if (transferType == 2)
            {
                Console.WriteLine("Please Select the Recipient\n");
            }
            else
            {
                Console.WriteLine("Please Select a User to Request from\n");
            }
        }

        private MenuOptionResult ReturnName(int selection)
        {
            parent.selectionId = selection;

            return MenuOptionResult.CloseMenuAfterSelection;
        }

        private MenuOptionResult ExitSelect()
        {
            parent.selectionId = 0;

            return MenuOptionResult.CloseMenuAfterSelection;
        }
    }
}
