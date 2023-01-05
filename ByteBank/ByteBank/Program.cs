using System;

public class ByteBank
{

    class LoginData
    {
        public String login { get; set; }
        public String password { get; set; }
    }

    class BankMenu
    {
        private List<LoginData> _logins = new List<LoginData>();

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

    }

    public static void Main(string[] args)
    {
        BankMenu BankMenu = new BankMenu();
        bool mainLoopControler = true;

        BankMenu.WriteMenu();

        while (mainLoopControler)
        {
            int choosedOption = Int16.Parse(Console.ReadLine());

            switch (choosedOption)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }

    }
}