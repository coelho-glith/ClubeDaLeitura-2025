using ClubeDaLeitura_2025.ConsoleApp.Compartilhado;
using ClubeDaLeitura_2025.ConsoleApp.ModuloRevistas;

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

    public Caixa[] BuscarListaCaixas()
    {
        return caixas;
    }

    public void EditarCaixa(Caixa caixaEncontrada, Caixa caixaDadosEditados)
    {
        caixaEncontrada.Etiqueta = caixaDadosEditados.Etiqueta;
        caixaEncontrada.Cor = caixaDadosEditados.Cor;
        caixaEncontrada.DiasEmprestimo = caixaDadosEditados.DiasEmprestimo;
    }

    public void ExcluirCaixa(Caixa caixaEncontrada)
    {
        for (int i = 0; i < caixas.Length; i++)
        {
            if (caixas[i] == null)
                continue;
            else if (caixas[i].id == caixaEncontrada.id)
            {
                caixas[i] = null!;
                break;
            }
        }
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

    public bool VerificarRevistasCaixa(Caixa caixaEncontrada)
    {
        int revistas = 0;

        if (caixaEncontrada.revistas == null)
            return false;

        foreach (Revista r in caixaEncontrada.revistas)
        {
            if (r != null)
                revistas++;
        }

        if (revistas > 0)
            return true;
        else
            return false;
    }

    public bool VerificarEtiquetas(Caixa caixaVerificar)
    {
        for (int i = 0; i < caixas.Length; i++)
        {
            if (caixas[i] == null)
                continue;

            if (caixaVerificar.Etiqueta == caixas[i].Etiqueta)
                return true;
        }

        return false;
    }

}
