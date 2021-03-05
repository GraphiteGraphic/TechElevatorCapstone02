using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenmoClient.Data;


namespace TenmoClient
{
    class ConsoleServices
    {
        public void PrintAccounts(List<Account> list)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"                                                                                                                                        ");
            Console.WriteLine(@":::   :::  ::::::::  :::    ::: :::::::::           :::      ::::::::   ::::::::   ::::::::  :::    ::: ::::    ::: ::::::::::: ::::::::  ");
            Console.WriteLine(@":+:   :+: :+:    :+: :+:    :+: :+:    :+:        :+: :+:   :+:    :+: :+:    :+: :+:    :+: :+:    :+: :+:+:   :+:     :+:    :+:    :+: ");
            Console.WriteLine(@" +:+ +:+  +:+    +:+ +:+    +:+ +:+    +:+       +:+   +:+  +:+        +:+        +:+    +:+ +:+    +:+ :+:+:+  +:+     +:+    +:+        ");
            Console.WriteLine(@"  +#++:   +#+    +:+ +#+    +:+ +#++:++#:       +#++:++#++: +#+        +#+        +#+    +:+ +#+    +:+ +#+ +:+ +#+     +#+    +#++:++#++ ");
            Console.WriteLine(@"   +#+    +#+    +#+ +#+    +#+ +#+    +#+      +#+     +#+ +#+        +#+        +#+    +#+ +#+    +#+ +#+  +#+#+#     +#+           +#+ ");
            Console.WriteLine(@"   #+#    #+#    #+# #+#    #+# #+#    #+#      #+#     #+# #+#    #+# #+#    #+# #+#    #+# #+#    #+# #+#   #+#+#     #+#    #+#    #+# ");
            Console.WriteLine(@"   ###     ########   ########  ###    ###      ###     ###  ########   ########   ########   ########  ###    ####     ###     ########  ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Account a in list)
            {
                Console.WriteLine($"Your balance is {a.Balance}, in account {a.AccountId}.");
            }
        }

        public void PrintBalance(Account account)
        {
            Console.WriteLine($"Current Balance: {account.Balance}");
        }

        public void PrintTransfers(Dictionary<int, Transfer> transfers, API_User user)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"::::::::::: :::::::::      :::     ::::    :::  ::::::::  :::::::::: :::::::::: :::::::::   :::::::: ");
            Console.WriteLine(@"    :+:     :+:    :+:   :+: :+:   :+:+:   :+: :+:    :+: :+:        :+:        :+:    :+: :+:    :+:");
            Console.WriteLine(@"    +:+     +:+    +:+  +:+   +:+  :+:+:+  +:+ +:+        +:+        +:+        +:+    +:+ +:+       ");
            Console.WriteLine(@"    +#+     +#++:++#:  +#++:++#++: +#+ +:+ +#+ +#++:++#++ :#::+::#   +#++:++#   +#++:++#:  +#++:++#++");
            Console.WriteLine(@"    +#+     +#+    +#+ +#+     +#+ +#+  +#+#+#        +#+ +#+        +#+        +#+    +#+        +#+ ");
            Console.WriteLine(@"    #+#     #+#    #+# #+#     #+# #+#   #+#+# #+#    #+# #+#        #+#        #+#    #+# #+#    #+# ");
            Console.WriteLine(@"    ###     ###    ### ###     ### ###    ####  ########  ###        ########## ###    ###  ######## ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("id    from/to   name    amount");
            foreach (KeyValuePair<int, Transfer> t in transfers)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (t.Value.AccountFrom.AccountId == user.UserId)
                {
                    Console.WriteLine($"{t.Key}      -->      {t.Value.ToUsername}    ${t.Value.Amount} ");
                } else
                {
                    Console.WriteLine($"{t.Key}      <--      {t.Value.FromUsername}    ${t.Value.Amount} ");
                }
                
            }
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void TransferDetails(Transfer transfer, API_User user)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"{transfer.TransferID}||   {transfer.FromUsername}     -->      {transfer.ToUsername}    ${transfer.Amount}");
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public Dictionary<int, API_User> NamesToDiction(List<API_User> list)
        {
            Dictionary<int, API_User> names = new Dictionary<int, API_User> { };
            foreach (API_User name in list)
            {
                names.Add(name.UserId, name);
            }

            return names;
        }

        public void PrintUsers(List<API_User> names, API_User user)
        {
            foreach (API_User name in names)
            {
                if (name.Username == user.Username)
                {
                    continue;
                }
                Console.WriteLine($"{name.UserId} || {name.Username}");
            }
        }

        public void TransferComplete(decimal newBal)
        {
            Task.Run(() => Console.Beep(1245, 100));
            Task.Run(() => Console.Beep(1245, 100)); 
            Task.Run(() => Console.Beep(1245, 100));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"  ::::::::  :::    :::  ::::::::   ::::::::  :::::::::: ::::::::   ::::::::  :::");
            Console.WriteLine(@" :+:    :+: :+:    :+: :+:    :+: :+:    :+: :+:       :+:    :+: :+:    :+: :+: ");
            Console.WriteLine(@" +:+        +:+    +:+ +:+        +:+        +:+       +:+        +:+        +:+ ");
            Console.WriteLine(@" +#++:++#++ +#+    +:+ +#+        +#+        +#++:++#  +#++:++#++ +#++:++#++ +#+ ");
            Console.WriteLine(@"        +#+ +#+    +#+ +#+        +#+        +#+              +#+        +#+ +#+ ");
            Console.WriteLine(@" #+#    #+# #+#    #+# #+#    #+# #+#    #+# #+#       #+#    #+# #+#    #+#     ");
            Console.WriteLine(@"  ########   ########   ########   ########  ########## ########   ########  ### ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n||Transfer completed||\nCurrent Balance: ${newBal}");
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void ErrorMessage()
        {
            Task.Run(() => Console.Beep(523, 200));
            Task.Run(() => Console.Beep(494, 200));
            Task.Run(() => Console.Beep(466, 200));
            Task.Run(() => Console.Beep(440, 800));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@":::::::::: :::::::::  :::::::::   ::::::::  :::::::::  :::      ::::::::::: ::::    ::: :::     :::     :::     :::        ::::::::::: :::::::::");
            Console.WriteLine(@":+:        :+:    :+: :+:    :+: :+:    :+: :+:    :+: :+:          :+:     :+:+:   :+: :+:     :+:   :+: :+:   :+:            :+:     :+:    :+:");
            Console.WriteLine(@"+:+        +:+    +:+ +:+    +:+ +:+    +:+ +:+    +:+ +:+          +:+     :+:+:+  +:+ +:+     +:+  +:+   +:+  +:+            +:+     +:+    +:+");
            Console.WriteLine(@"+#++:++#   +#++:++#:  +#++:++#:  +#+    +:+ +#++:++#:  +#+          +#+     +#+ +:+ +#+ +#+     +:+ +#++:++#++: +#+            +#+     +#+    +:+");
            Console.WriteLine(@"+#+        +#+    +#+ +#+    +#+ +#+    +#+ +#+    +#+ +#+          +#+     +#+  +#+#+#  +#+   +#+  +#+     +#+ +#+            +#+     +#+    +#+");
            Console.WriteLine(@"#+#        #+#    #+# #+#    #+# #+#    #+# #+#    #+#              #+#     #+#   #+#+#   #+#+#+#   #+#     #+# #+#            #+#     #+#    #+#");
            Console.WriteLine(@"########## ###    ### ###    ###  ########  ###    ### ###      ########### ###    ####     ###     ###     ### ########## ########### #########");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }
}
