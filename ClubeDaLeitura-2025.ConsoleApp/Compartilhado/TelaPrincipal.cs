namespace ClubeDaLeitura_2025.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    public string ApresentarMenuPrincipal()
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
        string opcaoEscolhida = Console.ReadLine()!;

        if (opcaoEscolhida == null)
            return null!;
        else
            return opcaoEscolhida.Trim().ToUpper();

    }
}
