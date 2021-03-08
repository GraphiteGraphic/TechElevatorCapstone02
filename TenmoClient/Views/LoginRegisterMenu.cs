using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;
using System.Threading;
using System.Threading.Tasks;

namespace TenmoClient.Views
{

    public class LoginRegisterMenu : ConsoleMenu
    {
        private readonly AuthService authService;
        private ConsoleServices consoleServices = new ConsoleServices();
        public LoginRegisterMenu(AuthService authService)
        {
            this.authService = authService;

            AddOption("Login", Login)
                .AddOption("Register", Register)
                .AddOption("Exit", Exit);

            Configure(cfg =>
            {
                cfg.ItemForegroundColor = ConsoleColor.Blue;
                cfg.SelectedItemForegroundColor = ConsoleColor.White;
            });
        }

        private MenuOptionResult Login()
        {
            API_User user = null;
            while (user == null)
            {
                LoginUser loginUser = new LoginUser();
                loginUser.Username = GetString("Username: ", true);
                if (loginUser.Username.Trim().Length == 0)
                {
                    Console.WriteLine("Login cancelled.");
                    return MenuOptionResult.WaitAfterMenuSelection;
                }

                loginUser.Password = GetPasswordFromConsole("Password: ");
                user = authService.Login(loginUser);
                if (user == null)
                {
                    consoleServices.ErrorMessage();
                }
            }
            UserService.SetLogin(user);

            // User is logged in, show the main menu now.
            return new MainMenu(user).Show();
        }

        private MenuOptionResult Register()
        {
            bool isRegistered = false;
            while (!isRegistered)
            {
                LoginUser registerUser = new LoginUser();
                registerUser.Username = GetString("Username: ", true);
                if (registerUser.Username.Trim().Length == 0)
                {
                    Console.WriteLine("Registration cancelled.");
                    return MenuOptionResult.WaitAfterMenuSelection;
                }
                registerUser.Password = GetPasswordFromConsole("Password: ");
                isRegistered = authService.Register(registerUser);
                if (!isRegistered)
                {
                    Console.WriteLine("Registration failed.");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Registration successful. You can now log in.");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        protected override void OnBeforeShow()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Jingle();
            Console.WriteLine(@"     #     #                                                                         #######  #######                          ###");
            Console.WriteLine(@"     #  #  #  ######  #        ####    ####   #    #  ######      #####   ####          #     #        #    #  #    #   ####   ###");
            Console.WriteLine(@"     #  #  #  #       #       #    #  #    #  ##  ##  #             #    #    #         #     #        ##   #  ##  ##  #    #  ###");
            Console.WriteLine(@"     #  #  #  #####   #       #       #    #  # ## #  #####         #    #    #         #     #####    # #  #  # ## #  #    #   #");
            Console.WriteLine(@"     #  #  #  #       #       #       #    #  #    #  #             #    #    #         #     #        #  # #  #    #  #    #      ");
            Console.WriteLine(@"     #  #  #  #       #       #    #  #    #  #    #  #             #    #    #         #     #        #   ##  #    #  #    #  ###");
            Console.WriteLine(@"      ## ##   ######  ######   ####    ####   #    #  ######        #     ####          #     #######  #    #  #    #   ####   ###");
            Console.WriteLine();

        }

        public void Jingle()
        {
            Task.Run(() => Console.Beep(784, 500));
            Task.Run(() => Console.Beep(1318, 500));
            Task.Run(() => Console.Beep(1046, 800));
        }

        #region Console Helper Functions
        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }
        #endregion
    }
}
