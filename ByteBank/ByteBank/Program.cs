using System;

public class ByteBank
{


    class LoginData
    {
        public String _login { get; set; }
        public String _password { get; set; }
        public String _personName { get; set; }
        public bool _LoginStatus { get; set; } = false;
        public decimal _AccountBalance { get; set; } = 0.00M;

        public LoginData(String login,String password, String personName)
        {
            _login = login;
            _password = password;
            _personName = personName;
        }

        public override String ToString()
        {
            return "Login: " + _login + "| " + 
                "Password: " + _password + "| " + 
                "PersonName: " + _personName + "| " +
                "Login Status: " + _LoginStatus + "| " +
                "Account Balance: " + _AccountBalance;
        } 
    }

    class BankApp
    {
        private static List<LoginData> SavedAccounts = new List<LoginData>();
        private static LoginData? LoggedAccount { get; set; }
        private const String InvalidOptionMessage = "Invalid option. Please type a valid number from the menu below:";
        private const String InvalidLoginMessage = "Failed to login: incorrect login and/or password";
        private static bool MainLoopControler = true;
        private static bool InternalMenuLoopControler = true;

        public static void WriteLoginMenu()
        {
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Create Account");
            Console.WriteLine("0 - Shutdown System");
            Console.Write("Type your option: ");
        }

        public static void WriteInternalMenu()
        {
            Console.WriteLine("1 - Insert new account");
            Console.WriteLine("2 - Delete account");
            Console.WriteLine("3 - List all account data");
            Console.WriteLine("4 - Account details");
            Console.WriteLine("5 - Balance");
            Console.WriteLine("6 - Withdrawn");
            Console.WriteLine("7 - Deposit");
            Console.WriteLine("8 - Transfer");
            Console.WriteLine("0 - Loggout");
            Console.Write("Type your option: ");
        }

        public static void LoginMenu()
        {
            while (MainLoopControler)
            {
                BankApp.WriteLoginMenu();
                String userInput = Console.ReadLine();
                int choosedOption;
                if (!Int32.TryParse(userInput, out choosedOption)) { Console.WriteLine(InvalidOptionMessage); continue; }

                switch (choosedOption)
                {
                    case 0:
                        _AppShutdown();
                        break;
                    case 1:
                        _Login();
                        break;
                    case 2:
                        _CreateNewAccount();
                        break;
                    default:
                        Console.WriteLine(InvalidOptionMessage);
                        break;
                }
            }
        }

        public static void MainMenu() {

            while (InternalMenuLoopControler)
            {
                BankApp.WriteInternalMenu();
                String userInput = Console.ReadLine();
                int choosedOption;
                if (!Int32.TryParse(userInput, out choosedOption)) { Console.WriteLine(InvalidOptionMessage); continue; }

                switch (choosedOption)
                {
                    case 0:
                        _Logout();
                        break;
                    case 1:
                        _CreateNewAccount();
                        break;
                    case 2:
                        _DeleteAccount();
                        break;
                    case 3:
                        _ListAllAccountData();
                        break;
                    case 4:
                        _ListAccountData();
                        break;
                    case 5:
                        _ShowBalance();
                        break;
                    case 6:
                        _Withdrawn();
                        break;
                    case 7:
                        _Deposit();
                        break;
                    case 8:
                        _Transfer();
                        break;
                    default:
                        Console.WriteLine(InvalidOptionMessage);
                        break;
                }               
            }
        }

        private static void _CreateNewAccount()
        {
            Console.WriteLine("Please type your login:");
            String? login = Console.ReadLine();
            Console.WriteLine("Please type a password for your account:");
            String? password = Console.ReadLine();
            Console.WriteLine("Please type your name:");
            String? personName = Console.ReadLine();

            if (login == null || password == null || personName == null) Console.WriteLine("Invalid credentials");
            
            SavedAccounts.Add(new LoginData(login, password, personName));
            return;
        }

        private static void _DeleteAccount()
        {
            Console.WriteLine("Type the account login to be deleted:");
            String? userInput = Console.ReadLine();

            if (userInput == null) { Console.WriteLine("Invalid account"); return; }
            int indexOfTheAccount= SavedAccounts.FindIndex(account => account._login == userInput);
            if(indexOfTheAccount == -1) { Console.WriteLine("Account not found"); return; }

            SavedAccounts.RemoveAt(indexOfTheAccount);
            Console.WriteLine("The account was succesfully removed!");
        }
        
        private static void _Login()
        {
            Console.WriteLine("Type your login:");
            String? userInputLogin = Console.ReadLine();
            if (userInputLogin == null) { Console.WriteLine("Invalid account login"); return; }

            Console.WriteLine("Type your password:");
            String? userInputPassword = Console.ReadLine();
            if (userInputLogin == null) { Console.WriteLine(InvalidLoginMessage); return; }

            int indexOfTheAccount = SavedAccounts.FindIndex(account => account._login == userInputLogin);
            if (indexOfTheAccount == -1) { Console.WriteLine(InvalidLoginMessage); return; }

            LoginData findAccount = SavedAccounts.ElementAt(indexOfTheAccount);
            if (findAccount._password != userInputPassword) { Console.WriteLine(InvalidLoginMessage); return; };

            LoggedAccount = SavedAccounts.ElementAt(indexOfTheAccount);
            Console.WriteLine("The login was successfull!");
            MainMenu();
        }

        private static void _Logout()
        {
            LoggedAccount = null;
            InternalMenuLoopControler = false;
            Console.WriteLine("Succesfully logged out.");
        }

        private static void _AppShutdown()
        {
            MainLoopControler = false;
        }

        private static void _ListAccountData()
        {
            Console.WriteLine("Type the account login to be shown:");
            String? userInput = Console.ReadLine();

            Console.WriteLine("--------------------------------------");
            int indexOfTheAccount = SavedAccounts.FindIndex(account => account._login == userInput);
            if (indexOfTheAccount == -1) { Console.WriteLine("Account not found"); return; }
            Console.WriteLine(SavedAccounts[indexOfTheAccount].ToString());
            Console.WriteLine("--------------------------------------");
        }
        private static void _ListAllAccountData()
        {
            Console.WriteLine("--------------------------------------");
            SavedAccounts.ForEach(account => Console.WriteLine(account.ToString()));
            Console.WriteLine("--------------------------------------"); 
        }

        private static void _ShowBalance()
        {
            Console.WriteLine("Your balance is: R$ {0:##}", LoggedAccount!._AccountBalance);
        }

        private static void _Withdrawn()
        {
            Console.WriteLine("Your balance is: R$ ${0:00}\nType the amount you wanna withdrawn:", LoggedAccount._AccountBalance);
            string userInput = Console.ReadLine();
            decimal withdrawnAmount;
            if (Decimal.TryParse(userInput, out withdrawnAmount)) {  
                if(LoggedAccount?._AccountBalance < withdrawnAmount) { Console.WriteLine("The withdrawn was not possible due to insuficient balance"); return;}
                LoggedAccount!._AccountBalance = LoggedAccount._AccountBalance - withdrawnAmount;
                Console.WriteLine("Withdrawn was succesfully!");
            }
            else
            {
                Console.WriteLine(InvalidOptionMessage);
            }

            Console.WriteLine("--------------------------------------");
        }
        private static void _Deposit()
        {
            Console.WriteLine("Type the amount you wanna deposit:");
            string userInput = Console.ReadLine();
            decimal depositAmount;
            if (Decimal.TryParse(userInput, out depositAmount))
            {
                LoggedAccount!._AccountBalance = LoggedAccount._AccountBalance + depositAmount;

            }
        }

            private static void _Transfer()
        {
            Console.WriteLine("--------------------------------------");
            SavedAccounts.ForEach(account => Console.WriteLine(account.ToString()));
            Console.WriteLine("--------------------------------------");
        }





    }

    public static void Main(string[] args)
    {
        BankApp.LoginMenu();
        Console.Write("The application has been successfully terminated");
    }
}