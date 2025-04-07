using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

public class RepositorioChamado
{
    Chamado[] chamados = new Chamado[100];
    int contadorChamados = 0;

    public void CadastrarChamado(Chamado novoChamado)
    {
        novoChamado.id = GeradorIds.GerarIdChamado();

        chamados[contadorChamados++] = novoChamado;
    }

    public bool EditarChamado(int idChamado, Chamado novoChamado)
    {
        for (int i = 0; i < chamados.Length; i++)
        {
            if (chamados[i] == null)
                continue;

            else if (chamados[i].id == idChamado)
            {
                chamados[i].titulo = novoChamado.titulo;
                chamados[i].descricao = novoChamado.descricao;
                chamados[i].equipamento = novoChamado.equipamento;
                chamados[i].dataAbertura = novoChamado.dataAbertura;

                return true;
            }
        }

        return false;
    }

    public bool ExcluirChamado(int idChamado)
    {
        for (int i = 0; i < chamados.Length; i++)
        {
            if (chamados[i] == null) continue;

            else if (chamados[i].id == idChamado)
            {
                chamados[i] = null;

                return true;
            }
        }

        return false;
    }

    public Chamado[] SelecionarChamados()
    {
        return chamados;
    }

}
