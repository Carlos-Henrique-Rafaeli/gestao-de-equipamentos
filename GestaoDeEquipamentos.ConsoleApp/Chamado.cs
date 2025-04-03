namespace GestaoDeEquipamentos.ConsoleApp;

class Chamado
{
    public int id;
    public string titulo;
    public string descricao;
    public Equipamento equipamento;
    public DateTime dataAbertura;


    public Chamado(string titulo, string descricao, Equipamento equipamento, DateTime dataAbertura)
    {
        this.titulo = titulo;
        this.descricao = descricao;
        this.equipamento = equipamento;
        this.dataAbertura = dataAbertura;
    }

}
