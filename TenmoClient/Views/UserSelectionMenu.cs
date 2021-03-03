using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Views
{
    class UserSelectionMenu : ConsoleMenu
    {

        public UserSelectionMenu(Dictionary<int, string> names, string username)
        {
            foreach (KeyValuePair<int, string> kvp in names)
            {
                if (kvp.Value != username)
                {
                    AddOption(kvp.Value, ReturnName);
                }
            }
        }

        private MenuOptionResult ReturnName()
        {
            
            return MenuOptionResult.CloseMenuAfterSelection;
        }
    }
}
