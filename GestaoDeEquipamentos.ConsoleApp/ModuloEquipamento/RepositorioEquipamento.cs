using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class RepositorioEquipamento
{
    public Equipamento[] equipamentos = new Equipamento[100];
    int contadorEquipamentos = 0;

    public void CadastarEquipamento(Equipamento novoEquipamento)
    {
        novoEquipamento.id = GeradorIds.GerarIdEquipamento();

        equipamentos[contadorEquipamentos++] = novoEquipamento;
    }

    public bool EditarEquipamento(int idEquipamento, Equipamento equipamentoEditado)
    {
        for (int i = 0; i < equipamentos.Length; i++)
        {
            if (equipamentos[i] == null) continue;

            else if (equipamentos[i].id == idEquipamento)
            {
                equipamentos[i].nome = equipamentoEditado.nome;
                equipamentos[i].fabricante = equipamentoEditado.fabricante;
                equipamentos[i].precoAquisicao = equipamentoEditado.precoAquisicao;
                equipamentos[i].dataFabricacao = equipamentoEditado.dataFabricacao;

                return true;
            }
        }

        return false;
    }

    public bool ExcluirEquipamento(int idEquipamento)
    {
        for (int i = 0; i < equipamentos.Length; i++)
        {
            if (equipamentos[i] == null) continue;

            else if (equipamentos[i].id == idEquipamento)
            {
                equipamentos[i] = null;

                return true;
            }
        }

        return false;
    }

    public Equipamento[] SelecionarEquipamentos()
    {
        return equipamentos;
    }

    public Equipamento SelecionarEquipamentoPorId(int idEquipamento)
    {
        for (int i = 0; i < equipamentos.Length; i++)
        {
            Equipamento e = equipamentos[i];

            if (e == null)
                continue;

            else if (e.id == idEquipamento)
                return e;
        }

        return null;
    }
}
