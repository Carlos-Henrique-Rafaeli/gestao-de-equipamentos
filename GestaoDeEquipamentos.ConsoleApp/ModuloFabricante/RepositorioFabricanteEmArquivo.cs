using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

class RepositorioFabricanteEmArquivo : RepositorioBaseEmArquivo<Fabricante>, IRepositorioFabricante
{
    public RepositorioFabricanteEmArquivo(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Fabricante> ObterRegistros()
    {
        return contexto.Fabricantes;
    }
}
