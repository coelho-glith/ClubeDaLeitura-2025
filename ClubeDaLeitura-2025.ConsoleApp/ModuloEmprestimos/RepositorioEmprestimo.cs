using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;
using ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloEmprestimos;

public class RepositorioEmprestimo
{
    public Emprestimo[] emprestimos = new Emprestimo[10];
    public int contadorEmprestimos = 0;
    public bool ListaSemNada = false;

    public void InserirEmprestimo(Emprestimo novoEmprestimo)
    {
        novoEmprestimo.id = GeradorIds.GerarIdEmprestimo();
        novoEmprestimo.Revista.Emprestar();
        novoEmprestimo.Amigo.BuscarEmprestimo(novoEmprestimo);
        emprestimos[contadorEmprestimos++] = novoEmprestimo;
    }

    public void EditarEmprestimo(Emprestimo emprestimoEncontrado, Emprestimo emprestimoDadosEditados)
    {
        emprestimoEncontrado.Amigo = emprestimoDadosEditados.Amigo;
        emprestimoEncontrado.Revista = emprestimoDadosEditados.Revista;
    }

    public bool VerificarEmprestimoLigado(Amigo amigoEncontrado)
    {
        if (amigoEncontrado.Emprestimos == null)
            return false;

        foreach (Emprestimo e in amigoEncontrado.Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Aberto")
                return true;
        }
        return false;
    }

    public Emprestimo SelecionarPorId(int idEmprestimoEncontrado)
    {
        foreach (Emprestimo e in emprestimos)
        {
            if (e == null)
                continue;

            if (e.id == idEmprestimoEncontrado)
                return e;
        }
        return null!;
    }

    public Emprestimo[] BuscarListaEmprestimo()
    {
        return emprestimos;
    }

    public bool VerificarADevolucao(Emprestimo emprestimoEncontrado)
    {
        for (int i = 0; i < emprestimos.Length; i++)
        {
            if (emprestimos[i] == null)
                continue;

            if (emprestimoEncontrado.id == emprestimos[i].id && emprestimoEncontrado.Situacao == "Concluído")
                return true;
        }
        return false;
    }

    public void ExcluirEmprestimo(Emprestimo emprestimoEncontrado)
    {
        for (int i = 0; i < emprestimos.Length; i++)
        {
            if (emprestimos[i] == null)
                continue;

            else if (emprestimos[i].id == emprestimoEncontrado.id)
            {
                emprestimos[i] = null!;
                break;
            }
        }
    }

    public void VerificarEmprestimosAtrasados(Emprestimo[] emprestimosCadastrados)
    {
        foreach (Emprestimo e in emprestimosCadastrados)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Concluído")
                continue;

            if (DateTime.Now > e.ObterDataDevolucao())
                e.Situacao = "Atrasado!!!";
        }
    }
}
