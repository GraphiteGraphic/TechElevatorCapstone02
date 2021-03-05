using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenmoClient.Data;


namespace TenmoClient
{
    class ConsoleServices
    {
        public void PrintBalance(List<Account> list)
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
                if (t.Value.AccountFrom == user.UserId)
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

        public void PrintUsers(Dictionary<int, string> names, API_User user)
        {
            foreach (KeyValuePair<int, string> kvp in names)
            {
                if (kvp.Value == user.Username)
                {
                    continue;
                }
                Console.WriteLine($"{kvp.Key} || {kvp.Value}");
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
