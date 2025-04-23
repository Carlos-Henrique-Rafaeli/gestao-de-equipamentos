using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class Equipamento : EntidadeBase<Equipamento>
{
    public string Nome { get; set; }
    public Fabricante Fabricante { get; set; }
    public decimal PrecoAquisicao { get; set; }
    public DateTime DataFabricacao { get; set; }
    public string NumeroSerie
    {
        get
        {
            string tresPrimeirosCaracteres = Nome.Substring(0, 3).ToUpper();

            return $"{tresPrimeirosCaracteres}-{Id}";
        }
    }

    public Equipamento(string nome, Fabricante fabricante, decimal precoAquisicao, DateTime dataFabricacao)
    {
        Nome = nome;
        Fabricante = fabricante;
        PrecoAquisicao = precoAquisicao;
        DataFabricacao = dataFabricacao;
    }


    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome)) erros += "O campo 'Nome' é obrigatório.\n";

        else if (Nome.Length < 6) erros += "O campo 'Nome' precisa conter ao menos 6 caracteres.\n";

        //if (fabricante == null) erros += "O campo 'Fabricante' esta inválido.\n";

        if (PrecoAquisicao < 0) erros += "O campo 'Preço de Aquisição' precisa ser maior do que zero.\n";

        if (DataFabricacao > DateTime.Now) erros += "O campo 'Data de Fabricação' não pode ser do futuro.\n";

        return erros;
    }

    public override void AtualizarRegistro(Equipamento equipamentoEditado)
    {
        Nome = equipamentoEditado.Nome;
        Fabricante = equipamentoEditado.Fabricante;
        PrecoAquisicao = equipamentoEditado.PrecoAquisicao;
        DataFabricacao = equipamentoEditado.DataFabricacao;
    }
}
