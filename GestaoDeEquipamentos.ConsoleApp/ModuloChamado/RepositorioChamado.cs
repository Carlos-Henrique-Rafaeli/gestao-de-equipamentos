using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

public class RepositorioChamado : RepositorioBase<Chamado>, IRepositorioChamado
{
    public RepositorioChamado(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Chamado> ObterRegistros()
    {
        return contexto.Chamados;
    }
}
