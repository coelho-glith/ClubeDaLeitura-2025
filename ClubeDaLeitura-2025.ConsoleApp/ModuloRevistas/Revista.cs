using ClubeDaLeitura_2025.ConsoleApp.ModuloCaixas;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

public class Revista
{
    public int id = 0;
    public string Nome;
    public int NumeroEdicao;
    public string DataPublicacao;
    public string StatusEmprestimo;
    public Caixa Caixa;

    public Revista(string nome, int numeroEdicao, string dataPublicacao, Caixa caixa)
    {
        Nome = nome;
        NumeroEdicao = numeroEdicao;
        DataPublicacao = dataPublicacao;
        StatusEmprestimo = "Disponivel";
        Caixa = caixa;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "\nO campo 'Título' é obrigatório.\n";
        else
        {
            if (Nome.Length < 2 || Nome.Length > 100)
                erros += "O campo 'Titulo' precisa conter ao menos 2 caracteres e não pode ter mais que 100 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(NumeroEdicao.ToString()))
            erros += "O campo 'Número de Edição' é obrigatório.\n";
        else
        {
            if (NumeroEdicao < 0)
                erros += "O campo 'Número de Edição' precisa ser um número positivo.\n";
        }

        if (string.IsNullOrWhiteSpace(DataPublicacao))
            erros += "O campo 'Ano de Publicação' é obrigatório.\n";
        else
        {
            if (DataPublicacao.Length > 4 && DataPublicacao.All(char.IsDigit))
                erros += "O campo 'Ano de Publicação' está inválido! Insira somente o ano (yyyy).\n";

            if (!DateTime.TryParse($"03/03/{DataPublicacao}", out DateTime anoPublicacao))
                erros += "O campo 'Ano de Publicação' está inválido! Insira somente o ano (yyyy).\n";
            else
            {
                if (anoPublicacao > DateTime.Now)
                    erros += "O campo 'Ano de Publicação' não pode ser um ano futurístico.\n";
            }
        }

        return erros;
    }

    public void Emprestar()
    {
        StatusEmprestimo = "Emprestada";
    }

    public void Devolver()
    {
        StatusEmprestimo = "Disponível";
    }
}
