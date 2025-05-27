using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

class RepositorioFabricante : RepositorioBase<Fabricante>, IRepositorioFabricante
{
    public RepositorioFabricante(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Fabricante> ObterRegistros()
    {
        return contexto.Fabricantes;
    }
}
