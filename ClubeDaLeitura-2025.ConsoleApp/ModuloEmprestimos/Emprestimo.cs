using ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloEmprestimos;

public class Emprestimo
{
    public int id = 0;
    public Amigo Amigo;
    public Revista Revista;
    public DateTime Data;
    public string Situacao;


    public Emprestimo(Amigo amigo, Revista revista, string situacao)
    {
        Amigo = amigo;
        Revista = revista;
        Data = DateTime.Now;
        Situacao = situacao;
    }

    public string Validar()
    {
        string erros = "";

        if (Amigo == null)
            erros += "\nVocê precisa selecionar ao menos um Amigo.\n";

        if (Revista == null) 
            erros += "\nVocê precisa selecionar ao menos uma Revista.";

        if (string.IsNullOrEmpty(Situacao))
            erros += "\nCampo 'Situacao' é obrigatório.";
        else
        {
            if (Situacao != "Aberto" || Situacao != "Concluído")
                erros += "\nCampo 'Situacao' precisa ser 'Aberta' ou 'Concluído'!";
        }
        return erros;
    }

    public DateTime ObterDataDevolucao()
    {
        return Data.AddDays(Revista.Caixa.DiasEmprestimo);
    }

    public void RegistrarDevolucao()
    {
        Situacao = "Concluído";
        Revista.Devolver();
    }

}
