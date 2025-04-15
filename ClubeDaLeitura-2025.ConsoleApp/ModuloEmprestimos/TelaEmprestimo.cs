using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;
using ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloEmprestimos;

public class TelaEmprestimo
{
    public RepositorioEmprestimo RepositorioEmprestimo;
    public RepositorioAmigos RepositorioAmigos;
    public RepositorioRevistas RepositorioRevistas;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigos repositorioAmigo, RepositorioRevistas repositorioRevista)
    {
        RepositorioEmprestimo = repositorioEmprestimo;
        RepositorioAmigos = repositorioAmigo;
        RepositorioRevistas = repositorioRevista;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Registrar Empréstimo");
        Console.WriteLine("2 - Visualizar Lista de Empréstimos");
        Console.WriteLine("3 - Excluir Empréstimo");
        Console.WriteLine("4 - Editar Empréstimo");
        Console.WriteLine("5 - Registrar Devolução");
        Console.WriteLine("S - Voltar");
        

        Console.WriteLine("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }

    private void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gerenciamento de Empréstimos");
        Console.WriteLine("--------------------------------------------\n");
    }

    public void InserirEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine("Registrando Empréstimo...");
        Console.WriteLine("--------------------------------------------\n");

        Emprestimo novoEmprestimo = ObterDadosEmprestimo();

        if (novoEmprestimo == null)
            return;

        string erros = novoEmprestimo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Console.WriteLine("\nPressione qualquer tecla para novamente.");
            Console.ReadKey();
            InserirEmprestimo();
            return;
        }

        RepositorioEmprestimo.InserirEmprestimo(novoEmprestimo);

        Console.WriteLine("\nEmpréstimo registrado com sucesso");
    }

    private Emprestimo ObterDadosEmprestimo()
    {
        VisualizarAmigos(true, true);

        if (RepositorioAmigos.ListaSemNada)
            return null!;

        bool idAmigoValido;
        int idAmigoEscolhido;

        bool idRevistaValido;
        int idRevistaEncontrada;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("Selecione o ID de um Amigo: ");
            idAmigoValido = int.TryParse(Console.ReadLine(), out idAmigoEscolhido);

            if (!idAmigoValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido", ConsoleColor.Red);
                return null!;
            }
        } while (!idAmigoValido);

        Amigo amigoEscolhido = RepositorioAmigos.SelecionarPorId(idAmigoEscolhido);

        if (RepositorioEmprestimo.VerificarEmprestimoLigado(amigoEscolhido))
        {
            Notificador.ExibirMensagem("\nEsse amigo já tem um empréstimo em aberto!", ConsoleColor.Red);
            return null!;
        }

        VisualizarRevistas(true, true);

        if (RepositorioRevistas.ListaSemNada)
            return null!;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("Selecione o ID de uma Revista: ");
            idRevistaValido = int.TryParse(Console.ReadLine(), out idRevistaEncontrada);

            if (!idRevistaValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!", ConsoleColor.Red);
                return null!;
            }
        } while (!idRevistaValido);

        Revista revistaEncontrada = RepositorioRevistas.SelecionarPorId(idRevistaEncontrada);

        if (!RepositorioRevistas.VerificarRevistaDisponivel(revistaEncontrada))
        {
            Console.WriteLine("\nEssa revista não está disponível", ConsoleColor.Red);
            return null!;
        }

        Emprestimo emprestimo = new Emprestimo(amigoEscolhido, revistaEncontrada, "Aberto");

        return emprestimo;
    }

    public void EditarEmprestimo()
    {
        bool idValido;
        int idEmprestimoBuscado;

        ExibirCabecalho();

        Console.WriteLine("Editando Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        BuscarListaCadastrados(false, true);

        if (RepositorioEmprestimo.ListaSemNada)
            return;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoBuscado);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID selecionado é inválido", ConsoleColor.Red);
                Console.WriteLine("\nPressione qualquer tecla para ir novamente.");
                Console.ReadKey();
                EditarEmprestimo();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoBuscado);

        if (emprestimoEscolhido == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.WriteLine("\nPressione qualquer tecla para novamente.");
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        Emprestimo dadosEditados = ObterDadosEmprestimo();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros, ConsoleColor.Red);
            Console.WriteLine("\nPressione qualquer tecla para ir novamente.");
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        if (RepositorioEmprestimo.VerificarADevolucao(dadosEditados))
        {
            Console.WriteLine("\nEsse empréstimo já foi concluído.");
            Console.WriteLine("\nPressione [Enter] para novamente.");
            Console.ReadKey();
            EditarEmprestimo();
            return;
        }

        RepositorioEmprestimo.EditarEmprestimo(emprestimoEscolhido, dadosEditados);

        Console.WriteLine("\nEmpréstimo editado com sucesso", ConsoleColor.Green);
    }

    private void BuscarListaCadastrados(bool exibirCabecalho, bool emprestimoComId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Empréstimos...");
        Console.WriteLine("--------------------------------------------\n");

        if (emprestimoComId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -35} | {3, -20} | {4, -20}",
                "Id", "Amigo", "Revista", "Data de Devolução", "Situação");
        else
            Console.WriteLine(
                "{0, -20} | {1, -35} | {2, -20} | {3, -20}",
                "Amigo", "Revista", "Data de Devolução", "Situação");

        Emprestimo[] emprestimosEncontrados = RepositorioEmprestimo.BuscarListaEmprestimo();

        RepositorioEmprestimo.VerificarEmprestimosAtrasados(emprestimosEncontrados);

        int quantidadeEmprestimos = 0;

        for (int i = 0; i < emprestimosEncontrados.Length; i++)
        {
            Emprestimo e = emprestimosEncontrados[i];

            if (e == null)
                continue;

            quantidadeEmprestimos++;
            RepositorioEmprestimo.ListaSemNada = false;

            if (emprestimoComId)
                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -35} | {3, -20} | {4, -20}",
                    e.id, e.Amigo.Nome, e.Revista.Nome, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -35} | {2, -20} | {3, -20}",
                    e.Amigo.Nome, e.Revista.Nome, e.ObterDataDevolucao().ToShortDateString(), e.Situacao);
        }

        if (quantidadeEmprestimos == 0)
        {
            Console.WriteLine("\nNenhum empréstimo registrado!");
            RepositorioEmprestimo.ListaSemNada = true;
        }
    }

    public void ExcluirEmprestimo()
    {
        bool idValido;
        int idEmprestimoEncontrado;

        ExibirCabecalho();

        Console.WriteLine("Excluindo Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        BuscarListaCadastrados(false, true);

        if (RepositorioEmprestimo.ListaSemNada)
            return;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEncontrado);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido");
                Console.Write("\nPressione Qualquer tecla para tentar novamente");
                Console.ReadKey();
                EditarEmprestimo();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEscolhido = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEncontrado);

        if (emprestimoEscolhido == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione qualquer tecla para tentar novamente");
            Console.ReadKey();
            ExcluirEmprestimo();
            return;
        }

        RepositorioEmprestimo.ExcluirEmprestimo(emprestimoEscolhido);

        Console.WriteLine("\nEmpréstimo excluído com sucesso");
    }

    public void VisualizarRevistas(bool exibirCabecalho, bool emprestimoComId)
    {
        int quantidadeRevistas = 0;

        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Revistas...");
        Console.WriteLine("--------------------------------------------\n");

        if (emprestimoComId)
            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -15} | {3, -20} | {4, -20} | {5, -20}",
                "Id", "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status");
        else
            Console.WriteLine(
                "{0, -30} | {1, -15} | {2, -20} | {3, -20} | {4, -20}",
                "Título", "N° de Edição", "Ano de Publicação", "Caixa", "Status");

        Revista[] revistasEncontradas = RepositorioRevistas.BuscarListaRevistas();

        for (int i = 0; i < revistasEncontradas.Length; i++)
        {
            Revista r = revistasEncontradas[i];

            if (r == null)
                continue;

            quantidadeRevistas++;
            RepositorioRevistas.ListaSemNada = false;

            if (emprestimoComId)
                Console.WriteLine(
                    "{0, -6} | {1, -30} | {2, -15} | {3, -20} | {4, -20} | {5, -20}",
                    r.id, r.Nome, r.NumeroEdicao, r.DataPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo);
            else
                Console.WriteLine(
                    "{0, -30} | {1, -15} | {2, -20} | {3, -20} | {4, -20}",
                    r.Nome, r.NumeroEdicao, r.DataPublicacao, r.Caixa.Etiqueta, r.StatusEmprestimo);
        }
        if (quantidadeRevistas == 0)
        {
            Console.WriteLine("\nNenhuma revista registrada");
            RepositorioRevistas.ListaSemNada = true;
        }
    }

    public void VisualizarAmigos(bool exibirCabecalho, bool emprestimoComId)
    {
        int quantidadeAmigos = 0;

        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Amigos...");
        Console.WriteLine("--------------------------------------------\n");

        if (emprestimoComId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                "Id", "Nome", "Responsável", "Telefone");
        else
            Console.WriteLine(
                "{0, -20} | {1, -20} | {2, -20}",
                "Nome", "Responsável", "Telefone");

        Amigo[] amigosCadastrados = RepositorioAmigos.SelecionarAmigos();

        for (int i = 0; i < amigosCadastrados.Length; i++)
        {
            Amigo a = amigosCadastrados[i];

            if (a == null)
                continue;

            quantidadeAmigos++;
            RepositorioAmigos.ListaSemNada = false;

            if (emprestimoComId)
                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                    a.Id, a.Nome, a.Responsavel, a.Telefone);
            else
                Console.WriteLine(
                    "{0, -20} | {1, -20} | {2, -20}",
                    a.Nome, a.Responsavel, a.Telefone);
        }

        if (quantidadeAmigos == 0)
        {
            Console.WriteLine("\nNenhum amigo registrado!");
            RepositorioAmigos.ListaSemNada = true;
        }
    }

    public void RegistrarDevolucao()
    {
        bool idValido;
        int idEmprestimoEncontrado;

        ExibirCabecalho();

        Console.WriteLine("Devolução Empréstimo...");
        Console.WriteLine("--------------------------------------------");

        BuscarListaCadastrados(false, true);

        if (RepositorioEmprestimo.ListaSemNada)
            return;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de um Empréstimo: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimoEncontrado);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido");
                Console.Write("\nPressione qualquer tecla para tentar novamente");
                Console.ReadKey();
                RegistrarDevolucao();
                return;
            }
        } while (!idValido);

        Emprestimo emprestimoEncontrado = RepositorioEmprestimo.SelecionarPorId(idEmprestimoEncontrado);

        if (RepositorioEmprestimo.VerificarADevolucao(emprestimoEncontrado))
        {
            Console.WriteLine("\nA devolução escolhida não esta em aberto");
            Console.Write("\nPressione qualquer tecla para tentar novamente");
            Console.ReadKey();
            RegistrarDevolucao();
        }

        emprestimoEncontrado.RegistrarDevolucao();

        Console.WriteLine("\nDevolução feita com sucesso");
    }
}
