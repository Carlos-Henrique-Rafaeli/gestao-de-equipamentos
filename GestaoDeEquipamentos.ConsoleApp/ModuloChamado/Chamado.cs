using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

public class Chamado : EntidadeBase
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Equipamento Equipamento { get; set; }
    public DateTime DataAbertura { get; set; }
    public int TempoDecorrido
    {
        get
        {
            TimeSpan diferencaTempo = DateTime.Now.Subtract(DataAbertura);

            return diferencaTempo.Days;
        }
    }

    public Chamado(string titulo, string descricao, Equipamento equipamento)
    {
        Titulo = titulo;
        Descricao = descricao;
        Equipamento = equipamento;
        DataAbertura = DateTime.Now;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Titulo)) erros += "O campo 'Título' é obrigatório.\n";

        else if (Titulo.Length < 3) erros += "O campo 'Título' precisa conter ao menos 3 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Descricao)) erros += "O campo 'Descrição' é obrigatório.\n";

        else if (Descricao.Length < 3) erros += "O campo 'Descrição' precisa conter ao menos 3 caracteres.\n";

        //if (equipamento == null) erros += "O campo 'Equipamento' esta inválido.\n";
        
        if (DataAbertura > DateTime.Now) erros += "O campo 'Data de Abertura' não pode ser do futuro.\n";

        return erros;
    }

    public override void AtualizarRegistro(EntidadeBase registroEditado)
    {
        Chamado novoChamado = (Chamado)registroEditado;

        Titulo = novoChamado.Titulo;
        Descricao = novoChamado.Descricao;
        Equipamento = novoChamado.Equipamento;
        DataAbertura = novoChamado.DataAbertura;
    }
}
