namespace ClubeDaLeitura_2025.ConsoleApp.ModuloAmigos;

public class Amigo
{
    public int Id;
    public string Nome;
    public string Responsavel;
    public string Telefone;

    public Amigo (string nome, string responsavel, string telefone)
    {
        Nome = nome;
        Responsavel = responsavel;
        Telefone = telefone;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";

        if (Nome.Length < 3 && Nome.Length > 100)
            erros += "O campo 'Nome' precisa conter ao menos 3 caracteres e não deve ser maior que 100 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Responsavel))
            erros += "O campo 'Responsavel' é obrigatório.\n";

        if (Responsavel.Length < 3 && Responsavel.Length > 100)
            erros += "O campo 'Responsavel' precisa conter ao menos 3 caracteres e não deve ser maior que 100 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";

        if (Telefone.Length < 13)
            erros += "O campo 'Telefone' deve seguir o formato 00 00000-0000.";

        return erros;
    }


    public void ObterEmprestimos()
    {

    }
}
