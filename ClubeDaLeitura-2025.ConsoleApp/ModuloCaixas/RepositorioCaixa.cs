using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura_2025.ConsoleApp.ModuloCaixas;

public class RepositorioCaixa
{
    public Caixa[] caixas = new Caixa[10];
    public int contadorCaixas = 0;
    public bool ListaSemNada = false;

    public void InserirCaixa(Caixa novaCaixa)
    {
        novaCaixa.id = GeradorIds.GerarIdCaixa();

        caixas[contadorCaixas++] = novaCaixa;
    }

    public Caixa[] BuscarListaRegistrados()
    {
        return caixas;
    }

    public void EditarCaixa(Caixa caixaEncontrada, Caixa caixaDadosEditados)
    {

    }

    public void Excluir()
    {

    }

    public void SelecionarTodos()
    {

    }

    public Caixa SelecionarPorId(int idCaixaEncontrada)
    {
        foreach (Caixa c in caixas)
        {
            if (c == null)
                continue;

            if (c.id == idCaixaEncontrada)
                return c;
        }

        return null!;
    }
}
