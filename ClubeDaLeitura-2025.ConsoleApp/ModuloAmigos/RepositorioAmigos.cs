using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;
using ClubeDaLeitura_2025.ConsoleApp.ModuloEmprestimos;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;

public class RepositorioAmigos
{
    public Amigo[] amigos = new Amigo[40];
    public int contadorAmigos = 0;
    public bool ListaSemNada = false;

    public void InserirAmigo(Amigo novoAmigo)
    {
        novoAmigo.Id = GeradorIds.GerarIdAmigo();

        amigos[contadorAmigos++] = novoAmigo;

    }

    public bool EditarAmigo(int idAmigo, Amigo amigoEditado)
    {
        for (int i = 0; i < amigos.Length; i++)
        {
            if (amigos[i] == null)
                continue;
                amigos[i].Nome = amigoEditado.Nome;
                amigos[i].Responsavel = amigoEditado.Responsavel;
                amigos[i].Telefone = amigoEditado.Telefone;
                return true;
        }
        return false;
    }

    public bool VerificarNovoAmigo(Amigo novoAmigo)
    {
        for (int i = 0; i < amigos.Length; i++)
        {
            if (amigos[i] == null)
                continue;

            if (novoAmigo.Telefone == amigos[i].Telefone && novoAmigo.Id == 0)
                return true;
        }
        return false;
    }

    public bool ExcluirAmigo(Amigo amigoEncontrado)
    {
        for (int i = 0; i < amigos.Length; i++)
        {
            if (amigos[i] == null)
                continue;

            else if (amigos[i].Id == amigoEncontrado.Id)
            {
                amigos[i] = null;
                return true;

            }
        }
        return false;
    }

    public Amigo[] SelecionarAmigos()
    {
        return amigos;
    }

    public Amigo SelecionarPorId(int idAmigo)
    {
        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo e = amigos[i];

            if (e == null)
                continue;

            else if (e.Id == idAmigo)
                return e;
        }

        return null;
    }

    public bool VerificarEmprestimosAmigo(Amigo amigoEncontrado)
    {
        int emprestimos = 0;

        foreach (Emprestimo e in amigoEncontrado.Emprestimos)
        {
            if (e != null)
                emprestimos++;
        }

        if (emprestimos > 0)
            return true;
        else
            return false;
    }
}
