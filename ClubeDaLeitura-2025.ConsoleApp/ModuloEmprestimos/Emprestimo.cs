using ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloEmprestimos;

public class Emprestimo
{
    public Amigo Amigo;
    public Revista Revista;
    public DateTime Data;
    public string Situacao;

    public Emprestimo(Amigo amigo, Revista revista, DateTime data, string situacao)
    {
        Amigo = amigo;
        Revista = revista;
        Data = data;
        Situacao = situacao;
    }

    public void Validar()
    {

    }

    public void ObterDataDevolucao()
    {

    }

    public void RegistrarDevolucao()
    {

    }

}
