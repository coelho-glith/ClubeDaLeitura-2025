using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;

public class RepositorioAmigos
{
    public Amigo[] amigos = new Amigo[10];
    public int contadorAmigos = 0;

    public void InserirAmigo(Amigo novoAmigo)
    {
        novoAmigo.Id = GeradorIds.GerarIdAmigo();

        amigos[contadorAmigos++] = novoAmigo;

    }

    public void Editar()
    {

    }

    public void Excluir()
    {

    }

    public void SelecionarTodos()
    {

    }

    public void SelecionarPorId()
    {

    }

}
