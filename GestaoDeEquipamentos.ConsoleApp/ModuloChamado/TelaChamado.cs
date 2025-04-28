using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

public class TelaChamado : TelaBase<Chamado>, ITelaCrud
{
    public IRepositorioEquipamento repositorioEquipamento;
    public IRepositorioChamado repositorioChamado;

    public TelaChamado(IRepositorioEquipamento repositorioEquipamento, 
        IRepositorioChamado repositorioChamado)
        : base("Chamado", repositorioChamado)
    {
        this.repositorioEquipamento = repositorioEquipamento;
        this.repositorioChamado = repositorioChamado;
    }
    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Chamados...");
            Console.WriteLine("-------------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -17} | {5, -15}",
            "Id", "Título", "Descrição", "Equipamento", "Data de Abertura", "Dias Abertos"
        );

        List<Chamado> registros = repositorioChamado.SelecionarRegistros();

        foreach (var c in registros)
        {
            string tempoDecorrido = $"{c.TempoDecorrido} dias";

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -17} | {5, -15}",
            c.Id, c.Titulo, c.Descricao, c.Equipamento.Nome, c.DataAbertura.ToShortDateString(), tempoDecorrido
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }

    public void VisializarEquipamentos()
    {
        Console.WriteLine("Visualizando Equipamentos...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -11} | {3, -15} | {4, -15} | {5, -10}",
            "Id", "Nome", "Num. Série", "Fabricante", "Preço", "Data de Fabricação"
        );

        List<Equipamento> registros = repositorioEquipamento.SelecionarRegistros();

        foreach (var e in registros)
        {
            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -11} | {3, -15} | {4, -15} | {5, -10}",
            e.Id, e.Nome, e.NumeroSerie, e.Fabricante.Nome, e.PrecoAquisicao.ToString("C2"), e.DataFabricacao.ToShortDateString()
            );
        }

        Console.WriteLine();
    }

    public override Chamado ObterDados()
    {
        string titulo;
        do
        {
            Console.Write("Digite o título do chamado: ");
            titulo = Console.ReadLine()!;
            if (string.IsNullOrEmpty(titulo)) Console.WriteLine("\nTítulo Inválido...\n");

        } while (string.IsNullOrEmpty(titulo));

        string descricao;
        do
        {
            Console.Write("Digite a descrição do chamado: ");
            descricao = Console.ReadLine()!;
            if (string.IsNullOrEmpty(descricao)) Console.WriteLine("\nDescrição Inválida...\n");

        } while (string.IsNullOrEmpty(descricao));

        VisializarEquipamentos();

        int idEquipamento;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do equipamento: ");
            idValido = int.TryParse(Console.ReadLine(), out idEquipamento);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(idEquipamento);

        if (equipamentoSelecionado == null) return null;

        Chamado novoChamado = new Chamado(titulo, descricao, equipamentoSelecionado);

        return novoChamado;
    }
}
