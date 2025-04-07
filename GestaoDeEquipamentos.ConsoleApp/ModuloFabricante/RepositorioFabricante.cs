using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class RepositorioFabricante
{
    public Fabricante[] fabricantes = new Fabricante[100];
    int contadorFabricantes = 0;

    public void CadastarFabricante(Fabricante novoFabricante)
    {
        novoFabricante.id = GeradorIds.GerarIdFabricante();

        fabricantes[contadorFabricantes++] = novoFabricante;
    }

    public bool EditarEquipamento(int idEquipamento, Fabricante fabricanteEditado)
    {
        for (int i = 0; i < fabricantes.Length; i++)
        {
            if (fabricantes[i] == null) continue;

            else if (fabricantes[i].id == idEquipamento)
            {
                fabricantes[i].nome = fabricanteEditado.nome;
                fabricantes[i].email = fabricanteEditado.email;
                fabricantes[i].telefone = fabricanteEditado.telefone;

                return true;
            }
        }

        return false;
    }

    public bool ExcluirEquipamento(int idEquipamento)
    {
        for (int i = 0; i < fabricantes.Length; i++)
        {
            if (fabricantes[i] == null) continue;

            else if (fabricantes[i].id == idEquipamento)
            {
                fabricantes[i] = null;

                return true;
            }
        }

        return false;
    }

    public Fabricante[] SelecionarEquipamentos()
    {
        return fabricantes;
    }

    public Fabricante SelecionarEquipamentoPorId(int idEquipamento)
    {
        for (int i = 0; i < fabricantes.Length; i++)
        {
            Fabricante f = fabricantes[i];

            if (f == null)
                continue;

            else if (f.id == idEquipamento)
                return f;
        }

        return null;
    }
}
