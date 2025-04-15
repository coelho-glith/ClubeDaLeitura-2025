using ClubeDaLeitura_2025.ConsoleApp.ModuloCaixas;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

public class TelaRevista
{
    public RepositorioRevistas RepositorioRevista;
    public RepositorioCaixa RepositorioCaixa;

    public TelaRevista(RepositorioRevistas repositorioRevista, RepositorioCaixa repositorioCaixa)
    {
        RepositorioRevista = repositorioRevista;
        RepositorioCaixa = repositorioCaixa;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Inserir Revista");
        Console.WriteLine("2 - Editar Revista");
        Console.WriteLine("3 - Excluir Revista");
        Console.WriteLine("4 - Visualizar Lista de Revistas");
        Console.WriteLine("S - Voltar");

        Console.Write("\nOpção: ");

        return Console.ReadLine()!.ToUpper();
    }

    public void InserirRevista()
    {
        Console.WriteLine("Inserindo Revista...");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("");

        Revista novaRevista = ObterDadosRevista();

        if (novaRevista == null)
            return;

        string erros = novaRevista.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione qualquer tecla para tentar novamente");
            Console.ReadKey();
            InserirRevista();
            return;


        }

        if (RepositorioRevista.VerificarNomeNovoRegistro(novaRevista))
        {
            Console.WriteLine("\nJá existe uma revista dessa edição!");
            Console.Write("\nPressione Qualquer tecla para tentar novamente!");
            Console.ReadKey();
            InserirRevista();
            return;
        }

        RepositorioRevista.InserirRevista(novaRevista);

        Console.WriteLine("\nRevista Inserida com sucesso");
    }

    public void VisualizarRevistas(bool exibirCabecalho)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Revistas...");
        Console.WriteLine("--------------------------------------------\n");


        Console.WriteLine(
        "{0,-6} | {1,-30} | {2,-20} | {3,-25} | {4,-25} | {5,-15}",
        "Id", "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status");

        Revista[] revistasCadastradas = RepositorioRevista.BuscarListaRevistas();

        int quantidadeRevistas = 0;

        for (int i = 0; i < revistasCadastradas.Length; i++)
        {
            Revista r = revistasCadastradas[i];

            if (r == null)
                continue;

            quantidadeRevistas++;
            RepositorioRevista.ListaSemNada = false;

            Console.WriteLine(
            "{0,-6} | {1,-30} | {2,-20} | {3,-25} | {4,-25} | {5,-15}",
            r.id, r.Nome, r.NumeroEdicao, r.DataPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo);
        }

        if (quantidadeRevistas == 0)
        {
            Console.WriteLine("\nNenhuma revista Cadastrada");
            RepositorioRevista.ListaSemNada = true;
        }
    }

    public void EditarRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Revista...");
        Console.WriteLine("--------------------------------------------");

        VisualizarRevistas(false);

        if (RepositorioRevista.ListaSemNada)
            return;

        bool idValido;
        int idRevistaEncontrada;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEncontrada);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                EditarRevista();
                return;
            }
        } while (!idValido);

        Revista revistaEncontrada = RepositorioRevista.SelecionarPorId(idRevistaEncontrada);

        if (revistaEncontrada == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione qualquer tecla para tentar novamente");
            Console.ReadKey();
            EditarRevista();
            return;
        }

        Revista dadosEditados = ObterDadosRevista();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione qualquer tecla para tentar novamente");
            Console.ReadKey();
            EditarRevista();
            return;
        }

        if (RepositorioRevista.VerificarNomeEditarRegistro(revistaEncontrada, dadosEditados))
        {
            Console.WriteLine("\nJá existe uma revista dessa edição");
            Console.Write("\nPressione qualquer tecla para tentar novamente");
            Console.ReadKey();
            EditarRevista();
            return;
        }

        RepositorioRevista.EditarRevista(revistaEncontrada, dadosEditados);

        Console.WriteLine("\nRevista editada com sucesso");
    }

    public void ExcluirRevista()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Revista...");
        Console.WriteLine("--------------------------------------------");

        VisualizarRevistas(false);

        if (RepositorioRevista.ListaSemNada)
            return;

        bool idValido;
        int idRevistaEncontrada;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevistaEncontrada);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                ExcluirRevista();
                return;
            }
        } while (!idValido);

        Revista revistaEncontrada = RepositorioRevista.SelecionarPorId(idRevistaEncontrada);

        if (revistaEncontrada == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione [Enter] para tentar novamente!");
            Console.ReadKey();
            ExcluirRevista();
            return;
        }

        if (RepositorioRevista.VerificarRevistaEmprestada(revistaEncontrada))
        {
            Console.WriteLine($"\nA revista {revistaEncontrada.Nome} ainda está com um amigo, Não é possivel excluir por agora.");
            return;
        }

        RepositorioRevista.ExcluirRevista(revistaEncontrada);

        Console.WriteLine("\nRevista excluída com sucesso!");
    }

    public void VisualizarCaixas(bool exibirCabecalho)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Caixas...");
        Console.WriteLine("--------------------------------------------\n");

        Console.WriteLine(
        "{0, -7} | {1, -22} | {2, -23}",
        "Id", "Etiqueta", "Dias de Empréstimo");

        Console.WriteLine("-------------------------------------------------------------");

        Caixa[] caixasRegistradas = RepositorioCaixa.BuscarListaCaixas();

        int quantidadeCaixas = 0;

        for (int i = 0; i < caixasRegistradas.Length; i++)
        {
            Caixa c = caixasRegistradas[i];

            if (c == null)
                continue;

            quantidadeCaixas++;
            RepositorioCaixa.ListaSemNada = false;

            Console.WriteLine(
            "{0, -7} | {1, -22} | {2, -23}",
            c.id, c.Etiqueta, c.DiasEmprestimo);
        }

        if (quantidadeCaixas == 0)
        {
            Console.WriteLine("\nNenhuma caixa registrada");
            RepositorioCaixa.ListaSemNada = true;
        }
    }

    public Revista ObterDadosRevista()
    {
        Console.Write("Digite o Nome da Revista: ");
        string nome = Console.ReadLine()!;

        bool numeroValido;
        int numeroEdicao;


        do
        {
            Console.Write("Digite o N° de Edição da Revista: ");
            numeroValido = int.TryParse(Console.ReadLine(), out numeroEdicao);

            if (!numeroValido)
            {
                Console.WriteLine("\nEsse não é um número válido!");
                return null!;
            }
        } while (!numeroValido);

        Console.Write("Digite o Ano de Publicação da Revista: ");
        string dataPublicacao = Console.ReadLine()!;

        VisualizarCaixas(true);

        if (RepositorioCaixa.ListaSemNada)
            return null!;

        bool idValido;
        int idCaixaEscolhida;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Caixa para guardar a revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEscolhida);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido");
                return null!;
            }
        } while (!idValido);

        Caixa caixaEncontrada = RepositorioCaixa.SelecionarPorId(idCaixaEscolhida);

        Revista revista = new Revista(nome, numeroEdicao, dataPublicacao, caixaEncontrada);

        return revista;
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gerenciamento de Revistas");
        Console.WriteLine("--------------------------------------------\n");
    }
}

