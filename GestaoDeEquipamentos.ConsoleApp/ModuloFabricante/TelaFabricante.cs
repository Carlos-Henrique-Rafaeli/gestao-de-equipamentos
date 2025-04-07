using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class TelaFabricante
{
    public RepositorioFabricante repositorioFabricante;

    public TelaFabricante()
    {
        repositorioFabricante = new RepositorioFabricante();
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine("1 - Cadastro de Fabricante");
        Console.WriteLine("2 - Edição de Fabricante");
        Console.WriteLine("3 - Exclusão de Fabricante");
        Console.WriteLine("4 - Visualização de Fabricantes");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }

    public void CadastrarEquipamento()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Fabricante...");
        Console.WriteLine("-------------------------------------");
        
        Fabricante novoFabricante = ObterDadosFabricante();

        repositorioFabricante.CadastarFabricante(novoFabricante);
    }

    public void EditarFabricante()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Fabricante...");
        Console.WriteLine("-------------------------------------");

        VisualizarFabricantes(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Fabricante novoFabricante = ObterDadosFabricante();

        bool conseguiuEditar = repositorioFabricante.EditarEquipamento(idSelecionado, novoFabricante);


        if (!conseguiuEditar)
        {
            Console.WriteLine("Houve um erro durante a edição...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Equipamento editado com sucesso!");
        Console.ReadLine();
    }

    public void ExcluirFabricante()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Fabricante...");
        Console.WriteLine("-------------------------------------");

        VisualizarFabricantes(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);


        bool conseguiuExcluir = repositorioFabricante.ExcluirEquipamento(idSelecionado);


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
        Console.WriteLine("|      Controle de Fabricantes      |");
        Console.WriteLine("-------------------------------------");
    }

    public void VisualizarFabricantes(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Fabricantes...");
            Console.WriteLine("-------------------------------------");
        }

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

        if (exibirTitulo) Console.ReadLine();
    }

    public Fabricante ObterDadosFabricante()
    {
        Console.Write("Digite o nome do Fabricante: ");
        string nome = Console.ReadLine()!;

        string email;
        do
        {
            Console.Write("Digite o e-mail do fabricante: ");
            email = Console.ReadLine()!;
            if (string.IsNullOrEmpty(email)) Console.WriteLine("\nE-mail Inválido...\n");

        } while (string.IsNullOrEmpty(email));

        string telefone;
        do
        {
            Console.Write("Digite o telefone do fabricante: ");
            telefone = Console.ReadLine()!;

            if (String.IsNullOrEmpty(telefone)) Console.WriteLine("\nTelefone Inválido...\n");
        } while (String.IsNullOrEmpty(telefone));

        Fabricante novoFabricante = new Fabricante(nome, email, telefone);
        return novoFabricante;
    }

}
