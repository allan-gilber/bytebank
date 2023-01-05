using System;
using System.Globalization;

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
            return "Login: " + _login + " | " + 
                "Password: " + _password + " | " + 
                "PersonName: " + _personName + " | " +
                "Login Status: " + _LoginStatus + " | " +
                "Account Balance: " + _AccountBalance;
        } 
    }

    class BankApp
    {
        private static List<LoginData> SavedAccounts = new List<LoginData>();
        private static LoginData? LoggedAccount { get; set; }
        private const String InvalidOptionMessage = "Invalid option. Please type a valid number from the menu below:";
        private const String InvalidLoginMessage = "Failed to login: incorrect login and/or password.";
        private static bool MainLoopControler = true;
        private static bool InternalMenuLoopControler = true;
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
        private static NumberStyles style = NumberStyles.AllowDecimalPoint;

        public static void WriteLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Create Account");
            Console.WriteLine("0 - Shutdown System");
            Console.Write("Type your option: ");
        }

        public static void WriteInternalMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Delete account");
            Console.WriteLine("2 - List all account data");
            Console.WriteLine("3 - Account details");
            Console.WriteLine("4 - Balance");
            Console.WriteLine("5 - Withdrawn");
            Console.WriteLine("6 - Deposit");
            Console.WriteLine("7 - Transfer");
            Console.WriteLine("0 - Loggout");
            Console.Write("Type your option: ");
        }

        public static void LoginMenu()
        {
            while (MainLoopControler)
            {
                BankApp.WriteLoginMenu();
                String? userInput = Console.ReadLine();
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
                        Console.ReadKey();
                        break;
                }
                InternalMenuLoopControler = true;
            }
        }

        public static void MainMenu() {

            while (InternalMenuLoopControler)
            {
                BankApp.WriteInternalMenu();
                String? userInput = Console.ReadLine();
                int choosedOption;
                if (!Int32.TryParse(userInput, out choosedOption)) { Console.WriteLine(InvalidOptionMessage); continue; }

                switch (choosedOption)
                {
                    case 0:
                        _Logout();
                        break;
                    case 1:
                        _DeleteAccount();
                        break;
                    case 2:
                        _ListAllAccountData();
                        break;
                    case 3:
                        _ListAccountData();
                        break;
                    case 4:
                        _ShowBalance();
                        break;
                    case 5:
                        _Withdrawn();
                        break;
                    case 6:
                        _Deposit();
                        break;
                    case 7:
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
            Console.Clear();
            Console.WriteLine("Please type your login:");
            String? login = Console.ReadLine();
            if (login == null || login == "") { Console.WriteLine("Invalid login!\nReturning to login menu..."); Console.ReadKey(); return; }
            int checkIfAccountAlreadyExists = SavedAccounts.FindIndex(account => account._login == login);
            if (!(checkIfAccountAlreadyExists == -1)) { Console.WriteLine("Account login already exists! Returning to login menu..."); Console.ReadKey(); Console.ReadKey(); return; }

            Console.WriteLine("Please type a password for your account:");
            String? password = Console.ReadLine();
            if (password == null || password == "") {Console.WriteLine("invalid/null Password!\nReturning to login menu..."); Console.ReadKey(); return; }

            Console.WriteLine("Please type your name:");
            String? personName = Console.ReadLine();
            if ( personName == null || personName == "") {Console.WriteLine("Invalid name!\nReturning to login menu..."); Console.ReadKey(); return; }

            SavedAccounts.Add(new LoginData(login, password, personName));
            Console.WriteLine("Account succesfully created! Returning to login menu...");
            Console.ReadKey();
        }

        private static void _DeleteAccount()
        {
            Console.Clear();
            Console.WriteLine("Type the account login to be deleted:");
            String? userInput = Console.ReadLine();

            if (userInput == null || userInput == "") { Console.WriteLine("invalid/null account"); Console.ReadKey(); return; }
            int indexOfTheAccount= SavedAccounts.FindIndex(account => account._login == userInput);
            if(indexOfTheAccount == -1) { Console.WriteLine("Account not found"); Console.ReadKey(); return; }

            SavedAccounts.RemoveAt(indexOfTheAccount);
            Console.WriteLine("The account was succesfully removed!");
            Console.ReadKey();
        }
        
        private static void _Login()
        {
            Console.Clear();
            Console.WriteLine("Type your login:");
            String? userInputLogin = Console.ReadLine();
            if (userInputLogin == null || userInputLogin == "") { Console.WriteLine("Invalid account login"); Console.ReadKey(); return; }
            Console.Clear();

            Console.WriteLine("Type your password:");
            String? userInputPassword = Console.ReadLine();
            if (userInputPassword == null || userInputPassword == "") { Console.WriteLine(InvalidLoginMessage); Console.ReadKey(); return; }
            Console.Clear();

            int indexOfTheAccount = SavedAccounts.FindIndex(account => account._login == userInputLogin);
            if (indexOfTheAccount == -1) { Console.WriteLine(InvalidLoginMessage); Console.ReadKey(); return; }

            LoginData findAccount = SavedAccounts.ElementAt(indexOfTheAccount);
            if (findAccount._password != userInputPassword) { Console.WriteLine(InvalidLoginMessage); Console.ReadKey(); return; };

            LoggedAccount = findAccount;
            Console.WriteLine("The login was successfull!");
            MainMenu();
        }

        private static void _Logout()
        {
            LoggedAccount = null;
            InternalMenuLoopControler = false;
            Console.WriteLine("Succesfully logged out.");
            Console.ReadKey();
        }

        private static void _AppShutdown()
        {
            MainLoopControler = false;
        }

        private static void _ListAccountData()
        {
            Console.Clear();
            Console.WriteLine("Type the account login to be shown:");
            String? userInput = Console.ReadLine();
            if (userInput == "" || userInput == null) { Console.WriteLine("Empty/invalid login.\nReturning to main menu..."); Console.ReadKey(); return; }
            int indexOfTheAccount = SavedAccounts.FindIndex(account => account._login == userInput);
            if (indexOfTheAccount == -1) { Console.WriteLine("Account not found.\nReturning to main menu..."); Console.ReadKey(); return; }

            Console.WriteLine("{0} user data:\n--------------------------------------", userInput);
            Console.WriteLine(SavedAccounts[indexOfTheAccount].ToString());
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void _ListAllAccountData()
        {
            Console.Clear();
            Console.WriteLine("All account data:\n--------------------------------------");
            SavedAccounts.ForEach(account => Console.WriteLine(account.ToString()));
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void _ShowBalance()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------\n");
            Console.WriteLine("Your balance is: R$ {0:F2}", LoggedAccount!._AccountBalance);
            Console.WriteLine("\n--------------------------------------");
            Console.ReadKey();
        }

        private static void _Withdrawn()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------\n");
            Console.WriteLine("Your balance is: R$ ${0:F2}\n\nType the amount you wanna withdrawn:", LoggedAccount!._AccountBalance);
            Console.WriteLine("\n--------------------------------------\n");

            string? userInput = Console.ReadLine();
            decimal withdrawnAmount;
            if (Decimal.TryParse(userInput, style, culture, out withdrawnAmount) 
                &&
                userInput!.Split(',').Length < 2 ||
                !(userInput!.Split(',').Length > 2)
                ||
                !(userInput!.Split(',')?[1]?.Length <= 2)
                ) {
                if(LoggedAccount?._AccountBalance < withdrawnAmount) { Console.WriteLine("The withdrawn was not possible due to insuficient balance."); Console.ReadKey(); return;}
                LoggedAccount!._AccountBalance = LoggedAccount._AccountBalance - withdrawnAmount;
                Console.WriteLine("Withdrawn was succesfully!");
                Console.ReadKey();
                UpdateAccountData();
            }
            else
            {
                Console.WriteLine("The provided amount to withdrawn was invalid/null.\nReturning to main menu...");
                Console.ReadKey();
            }

            Console.WriteLine("--------------------------------------");
        }
        private static void _Deposit()
        {
            Console.Clear();
            Console.WriteLine("Type the amount you wanna deposit:");
            string? userInput = Console.ReadLine();
            decimal depositAmount;

            if (Decimal.TryParse(userInput, style, culture, out depositAmount) &&
                userInput!.Split(',').Length < 2 || 
                !(userInput!.Split(',').Length > 2)
                ||
                !(userInput!.Split(',')?[1]?.Length <= 2)
                )
            {
                LoggedAccount!._AccountBalance += depositAmount;
                Console.WriteLine("Succesfuly added R${0:F2} to your balance!", depositAmount);
                UpdateAccountData();
                Console.ReadKey();
            }
            else { 
                Console.WriteLine("The provided amount to withdrawn was invalid/null.\nReturning to main menu...");
                Console.ReadKey();
            }
        }

        private static void _Transfer()
        {
            Console.Clear();
            Console.WriteLine("Type the account login that you wanna transfer:");
            String? userInputTargetAccount= Console.ReadLine();
            if (userInputTargetAccount == null || userInputTargetAccount == "" || LoggedAccount!._login == userInputTargetAccount) { 
                Console.WriteLine("invalid/null account login.\nReturning to main menu..."); Console.ReadKey(); 
                return; 
            }

            int indexOfTheTargetAccount = SavedAccounts.FindIndex(account => account._login == userInputTargetAccount);
            if (indexOfTheTargetAccount == -1) { Console.WriteLine("The target account was not found!\nReturning to main menu..."); Console.ReadKey(); return; }

            Console.WriteLine("Type the amount you wanna transfer:");

            string? userInputTransferAmount = Console.ReadLine();
            decimal transferAmount;

            if (
                !Decimal.TryParse(userInputTransferAmount, style, culture, out transferAmount) 
                || userInputTransferAmount!.Split(',').Length > 2 
                || (userInputTransferAmount!.Split(',').Length == 2 && userInputTransferAmount!.Split(',')?[1]?.Length > 2)
                || transferAmount == 0
                || transferAmount > LoggedAccount!._AccountBalance
               )
            { 
                Console.WriteLine("The provided amount to transfer was invalid/null.\nReturning to main menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Do you wanna trasnfer R$ {0:F2} to {1}?\nType your password to confirm:", transferAmount, userInputTargetAccount);
            String? userInputPassword = Console.ReadLine();
            if (userInputPassword == null || userInputPassword == "" || userInputPassword != LoggedAccount._password) { 
                Console.WriteLine("invalid/null password!\nReturning to main menu..."); Console.ReadKey(); return; 
            }


            LoggedAccount._AccountBalance -= transferAmount;
            SavedAccounts[indexOfTheTargetAccount]._AccountBalance += transferAmount;

            Console.WriteLine("Sucessfully transfered R$ {0:F2} to {1}\nReturning to main menu...", transferAmount, userInputTargetAccount);
            Console.ReadKey();
            UpdateAccountData();
        }

        private static void UpdateAccountData()
        {
            int indexOfTheTargetAccount = SavedAccounts.FindIndex(account => account._login == LoggedAccount!._login);
            if (indexOfTheTargetAccount != -1) {
                SavedAccounts[indexOfTheTargetAccount] = LoggedAccount!;
            }
            else
            {
                Console.WriteLine("Failure in updating account data.\nReturning to login menu....");
                Console.ReadKey();
                _Logout();
            }
        }

    }

    public static void Main(string[] args)
    {
        BankApp.LoginMenu();
        Console.Write("The application has been successfully terminated");
    }
}