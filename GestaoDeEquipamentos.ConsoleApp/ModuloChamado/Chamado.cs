using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

public class Chamado
{
    public int id;
    public string titulo;
    public string descricao;
    public Equipamento equipamento;
    public DateTime dataAbertura;


    public Chamado(string titulo, string descricao, Equipamento equipamento)
    {
        this.titulo = titulo;
        this.descricao = descricao;
        this.equipamento = equipamento;
        this.dataAbertura = DateTime.Now;
    }

}
