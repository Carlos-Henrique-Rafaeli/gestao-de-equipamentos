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


    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(titulo)) erros += "O campo 'Título' é obrigatório.\n";

        else if (titulo.Length < 3) erros += "O campo 'Título' precisa conter ao menos 3 caracteres.\n";

        if (string.IsNullOrWhiteSpace(descricao)) erros += "O campo 'Descrição' é obrigatório.\n";

        else if (descricao.Length < 3) erros += "O campo 'Descrição' precisa conter ao menos 3 caracteres.\n";

        //if (equipamento == null) erros += "O campo 'Equipamento' esta inválido.\n";
        
        if (dataAbertura > DateTime.Now) erros += "O campo 'Data de Abertura' não pode ser do futuro.\n";

        return erros;
    }


}
