using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class TelaEquipamento
{

    public RepositorioEquipamento repositorioEquipamento;
    public RepositorioFabricante repositorioFabricante;

    public TelaEquipamento(RepositorioFabricante repositorioFabricante)
    {
        repositorioEquipamento = new RepositorioEquipamento();
        this.repositorioFabricante = repositorioFabricante;
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

        bool conseguiuCadastrar = repositorioEquipamento.CadastarEquipamento(novoEquipamento);

        if (!conseguiuCadastrar)
        {
            Console.WriteLine("Houve um erro durante o cadastro...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Equipamento cadastrado com sucesso!");
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

        Fabricante novoFabricante = repositorioFabricante.SelecionarEquipamentoPorId(idFabricante);

        if (novoFabricante == null) return null;

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

        Equipamento novoEquipamento = ObterDadosEquipamentos();

        bool conseguiuEditar = repositorioEquipamento.EditarEquipamento(idSelecionado, novoEquipamento);
        
        if (!conseguiuEditar)
        {
            Console.WriteLine("Houve um erro durante a edição...");
            Console.ReadLine();
            return;
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
        

        bool conseguiuExcluir = repositorioEquipamento.ExcluirEquipamento(idSelecionado);


        if (!conseguiuExcluir)
        {
            Console.WriteLine("Houve um erro durante a exclusão...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Equipamento excluído com sucesso!");
        Console.ReadLine();
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

        Equipamento[] equipamentosCadastrados = repositorioEquipamento.SelecionarEquipamentos();

        for (int i = 0; i < equipamentosCadastrados.Length; i++)
        {
            Equipamento e = equipamentosCadastrados[i];

            if (e == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -11} | {3, -15} | {4, -15} | {5, -10}",
            e.id, e.nome, e.ObterNumeroSerie(), e.fabricante.nome, e.precoAquisicao.ToString("C2"), e.dataFabricacao.ToShortDateString()
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

        Fabricante[] equipamentosCadastrados = repositorioFabricante.SelecionarEquipamentos();

        for (int i = 0; i < equipamentosCadastrados.Length; i++)
        {
            Fabricante f = equipamentosCadastrados[i];

            if (f == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -30} | {3, -15}",
            f.id, f.nome, f.email, f.telefone
            );
        }
        Console.WriteLine();
    }
}
