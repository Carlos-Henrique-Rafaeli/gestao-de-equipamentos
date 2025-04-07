namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

public class TelaEquipamento
{

    public RepositorioEquipamento repositorioEquipamento;

    public TelaEquipamento()
    {
        repositorioEquipamento = new RepositorioEquipamento();
    }

    public string ApresentarMenu()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Equipamentos");
        Console.WriteLine("-------------------------------------");
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

        string nome;
        do
        {
            Console.Write("Digite o nome do equipamento: ");
            nome = Console.ReadLine()!;

            if (nome.Length < 6) Console.WriteLine("\nNecessita no mínimo 6 caracteres!\n");

        } while (nome.Length < 6);

        string fabricante;
        do
        {
            Console.Write("Digite o nome do fabricante: ");
            fabricante = Console.ReadLine()!;
            if (string.IsNullOrEmpty(fabricante)) Console.WriteLine("\nFabricante Inválido...\n");

        } while (string.IsNullOrEmpty(fabricante));

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


        Equipamento novoEquipamento = new Equipamento(nome, fabricante, precoAquisicao, dataFabricacao);

        repositorioEquipamento.CadastarEquipamento(novoEquipamento);
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

        string nome;
        do
        {
            Console.Write("Digite o nome do equipamento: ");
            nome = Console.ReadLine()!;

            if (nome.Length < 6) Console.WriteLine("\nNecessita no mínimo 6 caracteres!\n");

        } while (nome.Length < 6);

        string fabricante;
        do
        {
            Console.Write("Digite o nome do fabricante: ");
            fabricante = Console.ReadLine()!;
            if (string.IsNullOrEmpty(fabricante)) Console.WriteLine("\nFabricante Inválido...\n");

        } while (string.IsNullOrEmpty(fabricante));

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

        Equipamento novoEquipamento = new Equipamento(nome, fabricante, precoAquisicao, dataFabricacao);

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
        Console.WriteLine("Gestão de Equipamentos");
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
            e.id, e.nome, e.ObterNumeroSerie(), e.fabricante, e.precoAquisicao.ToString("C2"), e.dataFabricacao.ToShortDateString()
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }
}
