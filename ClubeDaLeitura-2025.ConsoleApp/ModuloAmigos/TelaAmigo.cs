using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;
using ClubeDaLeitura_2025.ConsoleApp.ModuloEmprestimos;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;

public class TelaAmigo
{

    public RepositorioAmigos repositorioAmigos;



    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gerenciamento de Amigos");
        Console.WriteLine("--------------------------------------------");
    }


    public char ApresentarMenu()
    {
        Console.Clear();
        ExibirCabecalho();

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

        if(repositorioAmigos.VerificarNovoAmigo(novoAmigo))
        {
            Console.WriteLine("Já existe um amigo com esses dados!");
            Console.Write("\nPressione qualquer tecla para tentar novamente!");
            Console.ReadKey();
            InserirAmigo();
        }

        repositorioAmigos.InserirAmigo(novoAmigo);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);

    }

    public void Editar()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Amigo...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarAmigos(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        Amigo amigoEditado = ObterDadosAmigo();

        bool conseguiuEditar = repositorioAmigos.EditarAmigo(idAmigo, amigoEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Houve um erro durante a edição do amigo...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
    }
    

    public void Excluir()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Amigo...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarAmigos(false, true);

        if (repositorioAmigos.ListaSemNada)
            return;

        Console.Write("Digite o ID do amigo que deseja selecionar: ");
        int idAmigoEncontrado = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        Amigo amigoEncontrado = repositorioAmigos.SelecionarPorId(idAmigoEncontrado);

        if (repositorioAmigos.VerificarEmprestimosAmigo(amigoEncontrado))
        {
            Console.WriteLine($"O {amigoEncontrado.Nome} ainda tem emprestimos em aberto!");
            return;
        }

        repositorioAmigos.ExcluirAmigo(amigoEncontrado);

        Console.WriteLine();
        Console.WriteLine("Amigo excluído com sucesso!");

    }

    public void VisualizarAmigos(bool exibirTitulo, bool amigoComId)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Amigos...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -6} | {1, -20} | {2, -30} | {3, -30} | ",
            "Id", "Nome", "Responsavel", "Telefone"
        );

        Amigo[] amigosRegistrados = repositorioAmigos.SelecionarAmigos();

        for (int i = 0; i < amigosRegistrados.Length; i++)
        {
            Amigo a = amigosRegistrados[i];

            if (a == null) continue;

            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -30} | {3, -30} |",
                a.Id, a.Nome, a.Responsavel, a.Telefone
            );

        }

        int quantidadeDeAmigos = 0;

        for (int i = 0; i < amigosRegistrados.Length; i++)
        {
            Amigo a = amigosRegistrados[i];

            if (a == null)
                continue;

            quantidadeDeAmigos++;
            repositorioAmigos.ListaSemNada = false;
            if (amigoComId)
                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                    a.Id, a.Nome, a.Responsavel, a.Telefone);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -20} | {2, -20}",
                    a.Nome, a.Responsavel, a.Telefone);
        }
        if (quantidadeDeAmigos == 0)
        {
            Console.WriteLine("\nNenhum amigo registrado!");
            repositorioAmigos.ListaSemNada = true;
        }

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    public void VisualizarEmprestimos(bool exibirCabecalho, bool amigoComId)
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Empréstimos Registrados...");
        Console.WriteLine("--------------------------------------------");

        VisualizarAmigos(false, true);

        if (repositorioAmigos.ListaSemNada)
            return;

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Amigo: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Amigo amigoEncontrado = repositorioAmigos.SelecionarPorId(idAmigo);

        Emprestimo[] emprestimosAmigoEncontrado = amigoEncontrado.ObterEmprestimos();

        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine();
        Console.WriteLine($"Visualizando Emprestimos de {amigoEncontrado.Nome}...");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine();

        if (amigoComId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20}",
                "Id", "Status", "Revista Emprestada");
        else
            Console.WriteLine(
                "{0, -20} | {1, -35} |",
                "Status", "Revista Emprestada");

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
