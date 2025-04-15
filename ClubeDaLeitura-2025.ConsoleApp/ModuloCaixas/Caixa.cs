using ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloCaixas;

public class Caixa
{
    public int id = 0;
    public string Etiqueta;
    public string Cor;
    public int DiasEmprestimo;
    public Revista[] revistas = new Revista[40];

    public Caixa(string etiqueta, string cor, int diasEmprestimo)
    {
        Etiqueta = etiqueta;
        Cor = cor;
        DiasEmprestimo = diasEmprestimo;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Etiqueta))
            erros += "\nO campo 'Nome' é obrigatório.\n";
        else
        {
            if (!Etiqueta.All(char.IsLetter))
                erros += "O campo 'Etiqueta' precisa conter apenas letras.\n";

            if (Etiqueta.Length > 50)
                erros += "O campo 'Etiqueta' não pode ter mais que 50 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(Cor.ToString()))
            erros += "O campo <Cor> é obrigatório.\n";
       

        if (string.IsNullOrEmpty(DiasEmprestimo.ToString()))
            erros += "O campo <Dias de Emprestimo> é obrigatório\n";
        else
            if (DiasEmprestimo != 3 && DiasEmprestimo != 7)
            erros += "O campo <Dias de Emprestimo> está inválido! Verifique novamente a raridade da revista Por favor.";

        return erros;
    }

    public void AdicionarRevista(Revista novaRevista)
    {
        for (int i = 0; i < revistas.Length; i++)
        {
            if (revistas[i] == null)
            {
                revistas[i] = novaRevista;
                return;
            }
        }
    }

    public void RemoverRevista(Revista revistaEncontrada)
    {
        for (int i = 0; i < revistas.Length; i++)
        {
            if (revistas[i] == null)
                continue;

            if (revistas[i].id == revistaEncontrada.id)
            {
                revistas[i] = null!;
                return;
            }
        }
    }
}