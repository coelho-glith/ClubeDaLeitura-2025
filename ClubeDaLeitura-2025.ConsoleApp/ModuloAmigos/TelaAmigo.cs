using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;

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
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Inserindo Amigo...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Amigo novoAmigo = ObterDadosAmigo();

        string erros = novoAmigo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            InserirAmigo();

            return;
        }

        repositorioAmigo.InserirAmigo(novoAmigo);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);



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

    public Amigo ObterDadosAmigo()
    {
        Console.Write("Digite o nome do fabricante: ");
        string nome = Console.ReadLine();

        Console.Write("Digite o Nome do Responsável: ");
        string responsavel = Console.ReadLine();

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine();

        Amigo amigo = new Amigo(nome, responsavel, telefone);

        return amigo;
    }
}
