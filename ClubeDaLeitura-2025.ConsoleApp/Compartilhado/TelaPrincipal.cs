namespace ClubeDaLeitura_2025.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    public char ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|           Clube De Leitura           |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Gerenciamento De amigos");
        Console.WriteLine("2 - Gerenciamento De Caixas");
        Console.WriteLine("3 - Gerenciamento De Revistas");
        Console.WriteLine("4 - Gerenciamento De Empréstimos");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        char opcaoEscolhida = Console.ReadLine()[0];

        return opcaoEscolhida;



    }
