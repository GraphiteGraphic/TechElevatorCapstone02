﻿using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    class UserSelectionMenu : ConsoleMenu
    {
        private MainMenu parent;

        public UserSelectionMenu(Dictionary<int, API_User> names, string username, MainMenu mainMenu)
        {
            parent = mainMenu;

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
            Console.WriteLine("Please Select the Recipient\n");
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
