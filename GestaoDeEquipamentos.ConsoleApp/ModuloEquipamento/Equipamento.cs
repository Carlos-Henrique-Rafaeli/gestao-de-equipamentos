using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class Equipamento
{
    public int id;
    public string nome;
    public Fabricante fabricante;
    public decimal precoAquisicao;
    public DateTime dataFabricacao;


    public Equipamento(string nome, Fabricante fabricante, decimal precoAquisicao, DateTime dataFabricacao)
    {
        this.nome = nome;
        this.fabricante = fabricante;
        this.precoAquisicao = precoAquisicao;
        this.dataFabricacao = dataFabricacao;
    }


    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(nome)) erros += "O campo 'Nome' é obrigatório.\n";

        else if (nome.Length < 6) erros += "O campo 'Nome' precisa conter ao menos 6 caracteres.\n";

        //if (fabricante == null) erros += "O campo 'Fabricante' esta inválido.\n";

        if (precoAquisicao < 0) erros += "O campo 'Preço de Aquisição' precisa ser maior do que zero.\n";

        if (dataFabricacao > DateTime.Now) erros += "O campo 'Data de Fabricação' não pode ser do futuro.\n";

        return erros;
    }


    public string ObterNumeroSerie()
    {
        string tresPrimeirosCaracteres = nome.Substring(0, 3).ToUpper();

        return $"{tresPrimeirosCaracteres}-{id}";
    }
}
