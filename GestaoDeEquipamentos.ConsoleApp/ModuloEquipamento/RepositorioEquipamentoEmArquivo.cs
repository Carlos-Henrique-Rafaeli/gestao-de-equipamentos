using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class RepositorioEquipamentoEmArquivo : RepositorioBaseEmArquivo<Equipamento>, IRepositorioEquipamento
{
    public RepositorioEquipamentoEmArquivo() : base("equipamentos.json")
    {
    }
}