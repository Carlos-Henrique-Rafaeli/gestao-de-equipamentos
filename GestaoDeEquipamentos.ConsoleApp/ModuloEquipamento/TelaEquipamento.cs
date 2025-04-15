using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class TelaEquipamento
{

    public RepositorioEquipamento repositorioEquipamento;
    public RepositorioFabricante repositorioFabricante;

    public TelaEquipamento(RepositorioFabricante repositorioFabricante, RepositorioEquipamento repositorioEquipamento)
    {
        this.repositorioFabricante = repositorioFabricante;
        this.repositorioEquipamento = repositorioEquipamento;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine("1 - Cadastro de Equipamento");
        Console.WriteLine("2 - Edição de Equipamento");
        Console.WriteLine("3 - Exclusão de Equipamento");
        Console.WriteLine("4 - Visualização de Equipamentos");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }

    public void CadastrarEquipamento()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Equipamento...");
        Console.WriteLine("-------------------------------------");
        Equipamento novoEquipamento = ObterDadosEquipamentos();

        string erros = novoEquipamento.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();
            Console.WriteLine(erros);
            Console.ReadLine();

            CadastrarEquipamento();

            return;
        }

        Fabricante fabricante = novoEquipamento.Fabricante;

        fabricante.AdicionarEquipamento(novoEquipamento);

        repositorioEquipamento.CadastrarRegistro(novoEquipamento);

        Console.WriteLine("Equipamento cadastrado com sucesso!");
        Console.ReadLine();
    }
    public void EditarEquipamento()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Equipamento...");
        Console.WriteLine("-------------------------------------");
        
        VisualizarEquipamentos(false);

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

        Equipamento equipamentoEditado = ObterDadosEquipamentos();
        Fabricante fabricanteEditado = equipamentoEditado.Fabricante;

        string erros = equipamentoEditado.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();
            Console.WriteLine(erros);
            Console.ReadLine();

            EditarEquipamento();

            return;
        }

        bool conseguiuEditar = repositorioEquipamento.EditarRegistro(idSelecionado, equipamentoEditado);
        
        if (!conseguiuEditar)
        {
            Console.WriteLine("Houve um erro durante a edição...");
            Console.ReadLine();
            return;
        }

        if (fabricanteAntigo != fabricanteEditado)
        {
            fabricanteAntigo.RemoverEquipamento(equipamentoAntigo);

            fabricanteEditado.AdicionarEquipamento(equipamentoEditado);
        }

        Console.WriteLine("Equipamento editado com sucesso!");
        Console.ReadLine();
    }
    
    public void ExcluirEquipamento()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Equipamento...");
        Console.WriteLine("-------------------------------------");

        VisualizarEquipamentos(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Equipamento equipamentoSelecionado = (Equipamento)repositorioEquipamento.SelecionarRegistroPorId(idSelecionado);

        bool conseguiuExcluir = repositorioEquipamento.ExcluirRegistro(idSelecionado);

        if (!conseguiuExcluir)
        {
            Console.WriteLine("Houve um erro durante a exclusão...");
            Console.ReadLine();
            return;
        }

        Fabricante fabricanteSelecionado = equipamentoSelecionado.Fabricante;

        fabricanteSelecionado.RemoverEquipamento(equipamentoSelecionado);

        Console.WriteLine("Equipamento excluído com sucesso!");
        Console.ReadLine();
    }

    public Equipamento ObterDadosEquipamentos()
    {
        string nome;
        do
        {
            Console.Write("Digite o nome do equipamento: ");
            nome = Console.ReadLine()!;

            if (nome.Length < 6) Console.WriteLine("\nNecessita no mínimo 6 caracteres!\n");

        } while (nome.Length < 6);

        VisializarFabricantes();

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

        Equipamento novoEquipamento = new Equipamento(nome, novoFabricante, precoAquisicao, dataFabricacao);
        return novoEquipamento;
    }

    private void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("|       Controle de Equipamentos    |");
        Console.WriteLine("-------------------------------------");
    }

    public void VisualizarEquipamentos(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Equipamentos...");
            Console.WriteLine("-------------------------------------");
        }

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

        if (exibirTitulo) Console.ReadLine();
    }

    public void VisializarFabricantes()
    {
        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -30} | {3, -15}",
        "Id", "Nome", "E-Mail", "Telefone"
        );

        EntidadeBase[] registros = repositorioFabricante.SelecionarRegistros();
        Fabricante[] fabricantesCadastrados = new Fabricante[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            fabricantesCadastrados[i] = (Fabricante)registros[i];

        for (int i = 0; i < fabricantesCadastrados.Length; i++)
        {
            Fabricante f = fabricantesCadastrados[i];

            if (f == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -30} | {3, -15}",
            f.Id, f.Nome, f.Email, f.Telefone
            );
        }
        Console.WriteLine();
    }
}
