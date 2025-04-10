namespace ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;

public class TelaAmigo
{

    public RepositorioAmigos repositorioAmigo;



    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gerenciamento de Amigos");
        Console.WriteLine("--------------------------------------------");
    }


    public char ApresentarMenu()
    {
        Console.WriteLine();

        Console.WriteLine("1 - Inserir Amigo");
        Console.WriteLine("2 - Editar Amigo");
        Console.WriteLine("3 - Excluir Amigo");
        Console.WriteLine("4 - Visualizar Amigos");
        Console.WriteLine("4 - Visualizar Empréstimos do amigo");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Digite um opção válida: ");
        char opcaoEscolhida = Console.ReadLine()[0];

        return opcaoEscolhida;
    }




    public void InserirAmigo()
    {




    }

    public void Editar()
    {

    }

    public void Excluir()
    {

    }

    public void VisualizarTodos()
    {

    }

    public void VisualizarEmprestimos()
    {

    }

}
