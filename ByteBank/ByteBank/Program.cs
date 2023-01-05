using System;

public class ByteBank
{
    private static List<LoginData> SavedAccounts = new List<LoginData>();
    private const  String InvalidOptionMessage = "Invalid option. Please type a valid number from the menu below:";

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
            return _login + "\n" + _password + "\n" + _personName + "\n" + _LoginStatus + "\n" + _AccountBalance;
        } 
    }

    class BankMenu
    {
        private static bool MainLoopControler = true;

        public static void WriteMenu()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        public static void ProcessOption() {

            while (MainLoopControler)
            {
                BankMenu.WriteMenu();
                String userInput = Console.ReadLine();
                int choosedOption;
                if (!Int32.TryParse(userInput, out choosedOption)) { Console.WriteLine(InvalidOptionMessage); continue; }

                switch (choosedOption)
                {
                    case 0:
                        MainLoopControler = false;
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
                        break;
                    case 5:
                        break;
                    case 6:
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

        private static void _ListAllAccountData()
        {
            Console.WriteLine("--------------------------------------");
            SavedAccounts.ForEach(account => Console.WriteLine(account.ToString()));
            Console.WriteLine("--------------------------------------"); 
        }


    }

    public static void Main(string[] args)
    {
        BankMenu.ProcessOption();
        Console.Write("The application has been successfully terminated");
    }
}