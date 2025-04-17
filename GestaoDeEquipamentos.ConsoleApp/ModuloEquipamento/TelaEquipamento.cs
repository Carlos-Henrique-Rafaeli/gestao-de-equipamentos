using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class TelaEquipamento : TelaBase
{

    public RepositorioEquipamento repositorioEquipamento;
    public RepositorioFabricante repositorioFabricante;

    public TelaEquipamento(RepositorioFabricante repositorioFabricante, 
        RepositorioEquipamento repositorioEquipamento)
        : base("Equipamento", repositorioEquipamento)
    {
        this.repositorioFabricante = repositorioFabricante;
        this.repositorioEquipamento = repositorioEquipamento;
    }
    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Cadastrando Equipamento...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Equipamento novoEquipamento = (Equipamento)ObterDados();

        string erros = novoEquipamento.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);

            CadastrarRegistro();

            return;
        }

        Fabricante fabricante = novoEquipamento.Fabricante;

        fabricante.AdicionarEquipamento(novoEquipamento);

        repositorioEquipamento.CadastrarRegistro(novoEquipamento);

        Console.WriteLine("O registro foi concluído com sucesso!");
    }

    public override void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Editando Equipamento...");
        Console.WriteLine("--------------------------------------------");

        VisualizarRegistros(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Equipamento equipamentoAntigo = (Equipamento)repositorioEquipamento.SelecionarRegistroPorId(idSelecionado);
        Fabricante fabricanteAntigo = equipamentoAntigo.Fabricante;

        Console.WriteLine();

        Equipamento equipamentoEditado = (Equipamento)ObterDados();

        Fabricante fabricanteEditado = equipamentoEditado.Fabricante;

        if (fabricanteAntigo != fabricanteEditado)
        {
            fabricanteAntigo.RemoverEquipamento(equipamentoAntigo);

            fabricanteEditado.AdicionarEquipamento(equipamentoEditado);
        }

        bool conseguiuEditar = repositorioEquipamento.EditarRegistro(idSelecionado, equipamentoEditado);

        if (!conseguiuEditar)
        {
            Console.WriteLine("Houve um erro durante a edição de um registro...");

            return;
        }


        Console.WriteLine("O registro foi editado com sucesso!");
    }

    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Excluindo Equipamento...");
        Console.WriteLine("--------------------------------------------");

        VisualizarRegistros(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Equipamento equipamentoSelecionado = (Equipamento)repositorioEquipamento.SelecionarRegistroPorId(idSelecionado);

        bool conseguiuExcluir = repositorioEquipamento.ExcluirRegistro(idSelecionado);

        if (!conseguiuExcluir)
        {
            Console.WriteLine("Houve um erro durante a exclusão de um registro...");

            return;
        }

        Fabricante fabricanteSelecionado = equipamentoSelecionado.Fabricante;

        fabricanteSelecionado.RemoverEquipamento(equipamentoSelecionado);

        Console.WriteLine("O registro foi excluído com sucesso!");
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Visualizando Equipamentos...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -11} | {3, -15} | {4, -15} | {5, -10}",
            "Id", "Nome", "Num. Série", "Fabricante", "Preço", "Data de Fabricação"
        );

        EntidadeBase[] registros = repositorioEquipamento.SelecionarRegistros();

        Equipamento[] equipamentosCadastrados = new Equipamento[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            equipamentosCadastrados[i] = (Equipamento)registros[i];

        for (int i = 0; i < equipamentosCadastrados.Length; i++)
        {
            Equipamento e = equipamentosCadastrados[i];

            if (e == null) continue;

            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -11} | {3, -15} | {4, -15} | {5, -10}",
                e.Id, e.Nome, e.NumeroSerie, e.Fabricante.Nome, e.PrecoAquisicao.ToString("C2"), e.DataFabricacao.ToShortDateString()
            );
        }

        Console.WriteLine();

        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    public override EntidadeBase ObterDados()
    {
        string nome;
        do
        {
            Console.Write("Digite o nome do equipamento: ");
            nome = Console.ReadLine()!;

            if (nome.Length < 6) Console.WriteLine("\nNecessita no mínimo 6 caracteres!\n");

        } while (nome.Length < 6);

        VisualizarFabricantes();

        int idFabricante;
        bool fabricanteValido;
        do
        {
            Console.Write("Digite o id do fabricante: ");
            fabricanteValido = int.TryParse(Console.ReadLine(), out idFabricante);

            if (!fabricanteValido) Console.WriteLine("\nId Inválido...\n");

        } while (!fabricanteValido);

        Fabricante novoFabricante = (Fabricante)repositorioFabricante.SelecionarRegistroPorId(idFabricante);

        decimal precoAquisicao;
        bool precoValido;
        do
        {
            Console.Write("Digite o preço de aquisição: R$ ");
            precoValido = decimal.TryParse(Console.ReadLine(), out precoAquisicao);

            if (!precoValido) Console.WriteLine("\nPreço Inválido...\n");
        } while (!precoValido);

        DateTime dataFabricacao;
        bool dataValida;
        do
        {
            Console.Write("Digite a data de fabricação do produto: (dd/mm/yyyy) ");
            dataValida = DateTime.TryParse(Console.ReadLine(), out dataFabricacao);

            if (!dataValida) Console.WriteLine("\nData Inválida...\n");

        } while (!dataValida);

        Equipamento equipamento = new Equipamento(nome, novoFabricante, precoAquisicao, dataFabricacao);


        return equipamento;
    }

    public void VisualizarFabricantes()
    {
        Console.WriteLine();

        Console.WriteLine("Visualizando Fabricantes...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -6} | {1, -20} | {2, -30} | {3, -30} | {4, -20}",
            "Id", "Nome", "Email", "Telefone", "Qtd. Equipamentos"
        );

        EntidadeBase[] registros = repositorioFabricante.SelecionarRegistros();
        Fabricante[] fabricantesCadastrados = new Fabricante[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            fabricantesCadastrados[i] = (Fabricante)registros[i];

        for (int i = 0; i < fabricantesCadastrados.Length; i++)
        {
            Fabricante f = fabricantesCadastrados[i];

            if (f == null)
                continue;

            Console.WriteLine(
            "{0, -6} | {1, -20} | {2, -30} | {3, -30} | {4, -20}",
                f.Id, f.Nome, f.Email, f.Telefone, f.QuantidadeEquipamentos
            );
        }

        Console.WriteLine();

        Console.WriteLine("Pressione ENTER para continuar...");
    }
}
