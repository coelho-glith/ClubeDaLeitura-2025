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
        Console.WriteLine("4 - Visualizar Amigos Inseridos");
        Console.WriteLine("5 - Visualizar Empréstimos do amigo");

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

    public void EditarAmigo()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Amigo...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarAmigos(false,true);

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

        Notificador.ExibirMensagem("O registro foi editado com sucesso", ConsoleColor.Green);
    }
    
    public void RemoverAmigo()
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
            Console.WriteLine($"O {amigoEncontrado.Nome} ainda tem emprestimos em aberto");
            return;
        }

        repositorioAmigos.ExcluirAmigo(amigoEncontrado);

        Console.WriteLine();
        Console.WriteLine("Amigo excluído.");

    }

    public void VisualizarAmigos(bool exibirTitulo, bool amigoComId)
    {
        int quantidadeDeAmigos = 0;

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
        bool idValido;
        int idAmigoEncontrado;

        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Empréstimos Registrados...");
        Console.WriteLine("--------------------------------------------");

        VisualizarAmigos(false, true);

        if (repositorioAmigos.ListaSemNada)
            return;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigoEncontrado);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido");
                Console.Write("\nPressione qualquer tecla para tentar novamente!");
                Console.ReadKey();
                VisualizarEmprestimos(true, false);
                return;
            }
        } while (!idValido);

        Console.WriteLine("\n--------------------------------------------");
        Console.Write("Selecione o ID de um Amigo: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Amigo amigoEncontrado = repositorioAmigos.SelecionarPorId(idAmigo);

        Emprestimo[] emprestimosAmigoEncontrado = amigoEncontrado.ObterEmprestimos();

        if (exibirCabecalho)
            ExibirCabecalho();

        
        Console.WriteLine($"\nVisualizando Emprestimos de {amigoEncontrado.Nome}...\n");
        Console.WriteLine("--------------------------------------------");
        

        if (amigoComId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20}",
                "Id", "Status", "Revista Emprestada");
        else
            Console.WriteLine(
                "{0, -20} | {1, -35} |",
                "Status", "Revista Emprestada");


        foreach (Emprestimo e in emprestimosAmigoEncontrado)
        {
            if (e == null)
                continue;

            if (amigoComId)
                Console.WriteLine(
                    "{0, -6} | {2, -35} | {3, -20} | {4, -20}",
                    e.id, e.Revista.Nome, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -20} | {2, -20}",
                    e.Revista.Nome, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);

        }
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
