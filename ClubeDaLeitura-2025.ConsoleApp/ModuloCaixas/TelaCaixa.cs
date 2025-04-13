namespace ClubeDaLeitura_2025.ConsoleApp.ModuloCaixas;

public class TelaCaixa
{
    public RepositorioCaixa RepositorioCaixa;

    public TelaCaixa(RepositorioCaixa repositorioCaixa)
    {
        RepositorioCaixa = repositorioCaixa;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Inserir Revista");
        Console.WriteLine("2 - Visualizar Lista de Revistas");
        Console.WriteLine("3 - Excluir Revista");
        Console.WriteLine("4 - Editar Revista");
        Console.WriteLine("S - Voltar");

        Console.Write("\nOpção: ");

        return Console.ReadLine()!.ToUpper();
    }

    public void InserirCaixa()
    {
        ExibirCabecalho();

        Console.WriteLine("Registrando Caixa...");
        Console.WriteLine("--------------------------------------------\n");

        Caixa novaCaixa = ObterDadosCaixa();

        string erros = novaCaixa.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);
            Console.Write("\nPressione qualquer tecla para tentar novamente");
            Console.ReadKey();
            InserirCaixa();
            return;
        }

        if (RepositorioCaixa.VerificarEtiquetas(novaCaixa))
        {
            Console.WriteLine("\nJá existe uma caixa com essa etiqueta!");
            Console.Write("\nPressione qualquer tecla para tentar novamente!");
            Console.ReadKey();
            InserirCaixa();
            return;
        }

        RepositorioCaixa.InserirCaixa(novaCaixa);

        Console.WriteLine("\nCaixa registrado com sucesso!");
    }

    public void EditarCaixa()
    {
        bool idValido;
        int idCaixaEncontrada;

        ExibirCabecalho();

        Console.WriteLine("Excluindo Caixa...");
        Console.WriteLine("--------------------------------------------");

        VisualizarCaixas(false, true);

        if (RepositorioCaixa.ListaSemNada)
            return;

        

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Caixa: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEncontrada);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido");
                Console.Write("\nPressione qualquer tecla para tentar de novo");
                Console.ReadKey();
                ExcluirCaixa();
                return;
            }
        } while (!idValido);

        Caixa caixaEncontrada = RepositorioCaixa.SelecionarPorId(idCaixaEncontrada);

        if (caixaEncontrada == null)
        {
            Console.WriteLine("\nO ID escolhido não está Cadastrado.");
            Console.Write("\nPressione qualquer Tecla para tentar de novo\n");
            Console.ReadKey();
            ExcluirCaixa();
            return;
        }

        if (RepositorioCaixa.VerificarRevistasCaixa(caixaEncontrada))
        {
            Console.WriteLine($"A caixa {caixaEncontrada.Etiqueta} ainda possui revistas e não pode ser excluída.");
            return;
        }

        RepositorioCaixa.ExcluirCaixa(caixaEncontrada);

        Console.WriteLine("\nCaixa Removida.");
    }

    public void VisualizarCaixas(bool exibirCabecalho, bool caixaComId)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Caixas...");
        Console.WriteLine("--------------------------------------------\n");

        if (caixaComId)
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -20}",
                "Id", "Etiqueta", "Dias de Empréstimo", "Revistas na Caixa");
        else
            Console.WriteLine(
                "{0, -20} | {1, -20} | {2, -20}",
                "Etiqueta", "Dias de Empréstimo", "Revistas na Caixa");

        Caixa[] caixasRegistradas = RepositorioCaixa.BuscarListaCaixas();

        int quantidadeCaixas = 0;

        for (int i = 0; i < caixasRegistradas.Length; i++)
        {
            Caixa c = caixasRegistradas[i];

            if (c == null)
                continue;

            int quantidadeRevistas = c.revistas.Count(r => r != null);

            quantidadeCaixas++;
            RepositorioCaixa.ListaSemNada = false;

            if (caixaComId)
                Console.WriteLine(
                    "{0, -6} | {1, -27} | {2, -27} | {3, -27}",
                    c.id, c.Etiqueta, c.DiasEmprestimo, quantidadeRevistas);
            else
                Console.WriteLine(
                    "{0, -27} | {1, -27} | {2, -27}",
                    c.Etiqueta, c.DiasEmprestimo, quantidadeRevistas);
        }

        if (quantidadeCaixas == 0)
        {
            Console.WriteLine("\nNenhuma caixa registrada!");
            RepositorioCaixa.ListaSemNada = true;
        }
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Gerenciamento de Caixas");
        Console.WriteLine("--------------------------------------------\n");
    }

    public int BuscarCoresPaletas()
    {
        ExibirCabecalho();

        Console.WriteLine("Paleta de Cores");
        Console.WriteLine("--------------------------------------------");

        for (int i = 0; i <= 15; i++)
        {
            Console.ForegroundColor = (ConsoleColor)i;
            Console.WriteLine($"{i} - {(ConsoleColor)i}");
        }
        Console.ResetColor();

        bool idValido;
        int corPaleta;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione uma cor da paleta de cores: ");
            idValido = int.TryParse(Console.ReadLine(), out corPaleta);

            if (!idValido)
            {
                Console.WriteLine("\nOpção inválida.");
                Console.Write("\nPressione qualquer letra para tentar de novo");
                Console.ReadKey();
                BuscarCoresPaletas();
                break;
            }
            break;
        } while (!idValido);

        return corPaleta;
    }

    public Caixa ObterDadosCaixa()
    {
        Console.Write("Digite o Nome da Etiqueta da Caixa: ");
        string etiqueta = Console.ReadLine()!;

        int cor = BuscarCoresPaletas();

        ExibirCabecalho();

        Console.WriteLine("Inserindo Caixa...");
        Console.WriteLine("--------------------------------------------\n");

        Console.Write("Digite o número de Dias de Empréstimo das Revistas nesta Caixa (3 para revistas comuns e 7 para revistas raras): ");
        int diasEmprestimo = Convert.ToInt32(Console.ReadLine()!);

        Caixa caixa = new Caixa(etiqueta, cor, diasEmprestimo);

        return caixa;
    }

    public void ExcluirCaixa()
    {
        bool idValido;
        int idCaixaEncontrada;

        ExibirCabecalho();

        Console.WriteLine("Excluindo Caixa...");
        Console.WriteLine("--------------------------------------------");

        VisualizarCaixas(false, true);

        if (RepositorioCaixa.ListaSemNada)
            return;

        do
        {
            Console.WriteLine("\n--------------------------------------------");
            Console.Write("Selecione o ID de uma Caixa: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixaEncontrada);

            if (!idValido)
            {
                Console.WriteLine("\nO ID selecionado é inválido!");
                Console.Write("\nPressione [Enter] para tentar novamente!");
                Console.ReadKey();
                ExcluirCaixa();
                return;
            }
        } while (!idValido);

        Caixa caixaEscolhida = RepositorioCaixa.SelecionarPorId(idCaixaEncontrada);

        if (caixaEscolhida == null)
        {
            Console.WriteLine("\nO ID escolhido não está registrado.");
            Console.Write("\nPressione Qualquer tecla para tentar de novo");
            Console.ReadKey();
            ExcluirCaixa();
            return;
        }

        if (RepositorioCaixa.VerificarRevistasCaixa(caixaEscolhida))
        {
            Console.WriteLine($"\nA caixa {caixaEscolhida.Etiqueta} ainda possui revistas e não tem possibilidade de ser exluída.");
            return;
        }

        RepositorioCaixa.ExcluirCaixa(caixaEscolhida);

        Console.WriteLine("\nCaixa excluída com sucesso");
    }
}

