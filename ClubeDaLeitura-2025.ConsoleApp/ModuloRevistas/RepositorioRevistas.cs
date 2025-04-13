using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;
using ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

public class RepositorioRevistas
{
    public Revista[] revistas = new Revista[100];
    public int contadorRevistas = 0;
    public bool ListaSemNada = false;

    public void InserirRevista(Revista novaRevista)
    {
        novaRevista.id = GeradorIds.GerarIdRevista();

        revistas[contadorRevistas++] = novaRevista;

    }

    public Revista[] BuscarListaRevistas()
    {
        return revistas;
    }

    public void EditarRevista(Revista revistaEncontrada, Revista revistaDadosEditados)
    {
        revistaEncontrada.Nome = revistaDadosEditados.Nome;
        revistaEncontrada.NumeroEdicao = revistaDadosEditados.NumeroEdicao;
        revistaEncontrada.DataPublicacao = revistaDadosEditados.DataPublicacao;
    }

    public void ExcluirRevista(Revista revistaEncontrada)
    {
        revistaEncontrada.Caixa.RemoverRevista(revistaEncontrada);

        for (int i = 0; i < revistas.Length; i++)
        {
            if (revistas[i] == null)
                continue;
            else if (revistas[i].id == revistaEncontrada.id)
            {
                revistas[i] = null!;
                break;
            }
        }
    }

    public Revista SelecionarPorId(int idRevistaEncontrada)
    {
        foreach (Revista r in revistas)
        {
            if (r == null)
                continue;

            if (r.id == idRevistaEncontrada)
                return r;

        }
        return null!;
    }

    public bool VerificarRevistaEmprestada(Revista revistaEncontrada)
    {
        if (revistaEncontrada.StatusEmprestimo == "Emprestada")
            return true;
        else
            return false;
    }



    public bool VerificarRevistaDisponivel(Revista revistaEncontrada)
    {
        if (revistaEncontrada.StatusEmprestimo == "Disponível")
            return true;
        else
            return false;
    }

    

    public bool VerificarNomeEditarRegistro(Revista revistaEncontrada, Revista revistaDadosEditados)
    {
        for (int i = 0; i < revistas.Length; i++)
        {
            if (revistas[i] == null)
                continue;

            if (revistaEncontrada.Nome == revistas[i].Nome && revistaEncontrada.NumeroEdicao == revistas[i].NumeroEdicao && revistaEncontrada.id == 0)
                return true;
        }

        return false;
    }

    public bool VerificarNomeNovoRegistro(Revista novaRevista)
    {
        for (int i = 0; i < revistas.Length; i++)
        {
            if (revistas[i] == null)
                continue;

            if (novaRevista.Nome == revistas[i].Nome && novaRevista.NumeroEdicao == revistas[i].NumeroEdicao && novaRevista.id == 0)
                return true;
        }

        return false;
    }
}
