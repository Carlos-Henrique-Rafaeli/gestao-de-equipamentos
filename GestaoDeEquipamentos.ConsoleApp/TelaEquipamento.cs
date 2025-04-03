namespace GestaoDeEquipamentos.ConsoleApp;

public class TelaEquipamento
{
    public static Equipamento[] equipamentos = new Equipamento[100];
    int contadorEquipamentos = 0;

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
        Console.WriteLine("4 - Visualizar Estoque");
        Console.WriteLine("5 - Gestão de Chamados");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }

    public void CadastrarEquipamento()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Equipamentos");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Cadastrando Equipamento...");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite o nome do equipamento: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o nome do fabricante: ");
        string fabricante = Console.ReadLine()!;

        Console.Write("Digite o preço de aquisição: R$ ");
        decimal precoAquisicao = Convert.ToDecimal(Console.ReadLine()!);

        Console.Write("Digite a data de fabricação do produto: (dd/mm/yyyy) ");
        DateTime dataFabricacao = Convert.ToDateTime(Console.ReadLine()!);

        Equipamento novoEquipamento = new Equipamento(nome, fabricante, precoAquisicao, dataFabricacao);
        novoEquipamento.id = GeradorIds.GerarIdEquipamento();

        equipamentos[contadorEquipamentos++] = novoEquipamento;
    }

    public void VisualizarEquipamentos(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Gestão de Equipamentos");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Visualizando Equipamentos...");
            Console.WriteLine("-------------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -11} | {3, -15} | {4, -15} | {5, -10}",
            "Id", "Nome", "Num. Série", "Fabricante", "Preço", "Data de Fabricação"
        );

        for (int i = 0; i < equipamentos.Length; i++)
        {
            Equipamento e = equipamentos[i];

            if (e == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -11} | {3, -15} | {4, -15} | {5, -10}",
            e.id, e.nome, e.ObterNumeroSerie(), e.fabricante, e.precoAquisicao.ToString("C2"), e.dataFabricacao.ToShortDateString()
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }

    public void EditarEquipamento()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Equipamentos");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Cadastrando Equipamento...");
        Console.WriteLine("-------------------------------------");
        
        VisualizarEquipamentos(false);

        Console.Write("Digite o Id do registro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine()!);

        Console.Write("Digite o nome do equipamento: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o nome do fabricante: ");
        string fabricante = Console.ReadLine()!;

        Console.Write("Digite o preço de aquisição: R$ ");
        decimal precoAquisicao = Convert.ToDecimal(Console.ReadLine()!);

        Console.Write("Digite a data de fabricação do produto: (dd/mm/yyyy) ");
        DateTime dataFabricacao = Convert.ToDateTime(Console.ReadLine()!);

        Equipamento novoEquipamento = new Equipamento(nome, fabricante, precoAquisicao, dataFabricacao);

        bool conseguiuEditar = false;

        for (int i = 0; i < equipamentos.Length; i++)
        {
            if (equipamentos[i] == null) continue;

            else if (equipamentos[i].id == idSelecionado)
            {
                equipamentos[i].nome = novoEquipamento.nome;
                equipamentos[i].fabricante = novoEquipamento.fabricante;
                equipamentos[i].precoAquisicao = novoEquipamento.precoAquisicao;
                equipamentos[i].dataFabricacao = novoEquipamento.dataFabricacao;
                conseguiuEditar = true;
            }
        }
        
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
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Equipamentos");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Excluindo Equipamento...");
        Console.WriteLine("-------------------------------------");

        VisualizarEquipamentos(false);

        Console.Write("Digite o Id do registro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine()!);

        bool conseguiuExcluir = false;

        for (int i = 0; i < equipamentos.Length; i++)
        {
            if (equipamentos[i] == null) continue;

            else if (equipamentos[i].id == idSelecionado)
                equipamentos[i] = null;
        }

        if (!conseguiuExcluir)
        {
            Console.WriteLine("Houve um erro durante a exclusão...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Equipamento excluído com sucesso!");
        Console.ReadLine();
    }
}
