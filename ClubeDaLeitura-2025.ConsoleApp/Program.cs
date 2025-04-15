using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;
using ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura_2025.ConsoleApp.ModuloCaixas;
using ClubeDaLeitura_2025.ConsoleApp.ModuloEmprestimos;
using ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

namespace ClubeDaLeitura_2025.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
        RepositorioRevistas repositorioRevista = new RepositorioRevistas();
        RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
        RepositorioAmigos repositorioAmigo = new RepositorioAmigos();

        TelaPrincipal telaPrincipal = new TelaPrincipal();
        TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);
        TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
        TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);
        TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);

        do
        {
            string opcao = telaPrincipal.ApresentarMenuPrincipal();

            if (opcao == "1")
            {
                bool menuAmigo = true;
                while (menuAmigo)
                {
                    string opcaoMenuAmigo = telaAmigo.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaAmigo.InserirAmigo();
                            Console.ReadKey();
                            break;
                        case "2":
                            telaAmigo.EditarAmigo();
                            Console.ReadKey();
                            break;
                        case "3":
                            telaAmigo.RemoverAmigo();
                            Console.ReadKey();
                            break;
                        case "4":
                            telaAmigo.VisualizarAmigos(false);
                            Console.ReadKey();
                            break;
                        case "5":
                            telaAmigo.VisualizarEmprestimos(false);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuAmigo = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida, Pressione qualquer tecla para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "2")
            {
                bool menuCaixa = true;
                while (menuCaixa)
                {
                    string opcaoMenuAmigo = telaCaixa.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaCaixa.InserirCaixa();
                            Console.ReadKey();
                            break;
                        case "2":
                            telaCaixa.EditarCaixa();
                            Console.ReadKey();
                            break;
                        case "3":
                            telaCaixa.ExcluirCaixa();
                            Console.ReadKey();
                            break;
                        case "4":
                            telaCaixa.VisualizarCaixas(true);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuCaixa = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida, Pressione qualquer tecla para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "3")
            {
                bool menuRevista = true;
                while (menuRevista)
                {
                    string opcaoMenuAmigo = telaRevista.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaRevista.InserirRevista();
                            Console.ReadKey();
                            break;
                        case "2":
                            telaRevista.EditarRevista();
                            Console.ReadKey();
                            break;
                        case "3":
                            telaRevista.ExcluirRevista();
                            Console.ReadKey();
                            break;
                        case "4":
                            telaRevista.VisualizarRevistas(true);
                            Console.ReadKey();
                            break;
                        case "S":
                            menuRevista = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida, Pressione qualquer tecla para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "4")
            {
                bool menuEmprestimo = true;
                while (menuEmprestimo)
                {
                    string opcaoMenuAmigo = telaEmprestimo.ApresentarMenu();
                    switch (opcaoMenuAmigo)
                    {
                        case "1":
                            telaEmprestimo.InserirEmprestimo();
                            Console.ReadKey();
                            break;
                        case "2":
                            telaEmprestimo.EditarEmprestimo();
                            Console.ReadKey();
                            break;
                        case "3":
                            telaEmprestimo.ExcluirEmprestimo();
                            Console.ReadKey();
                            break;
                        case "4":
                            telaEmprestimo.BuscarListaEmprestimo(false);
                            break;
                        case "5":
                            telaEmprestimo.RegistrarDevolucao();
                            Console.ReadKey();
                            break;
                        case "S":
                            menuEmprestimo = false;
                            continue;
                        default:
                            Console.WriteLine("Opção inválida, Pressione qualquer tecla para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            if (opcao == "S")
            {
                Console.Clear();
                Console.WriteLine("Adeus meu senhor\n");
                return;
            }
        } while (true);
    }
}
