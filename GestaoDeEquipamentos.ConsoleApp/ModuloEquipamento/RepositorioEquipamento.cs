using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class RepositorioEquipamento : RepositorioBase<Equipamento>, IRepositorioEquipamento
{
    public RepositorioEquipamento(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Equipamento> ObterRegistros()
    {
        return contexto.Equipamentos;
    }
}