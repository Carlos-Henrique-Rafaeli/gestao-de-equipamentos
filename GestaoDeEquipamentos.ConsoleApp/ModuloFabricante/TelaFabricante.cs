using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using System.Net.Mail;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class TelaFabricante
{
    public RepositorioFabricante repositorioFabricante;
    public RepositorioEquipamento repositorioEquipamento;

    public TelaFabricante(RepositorioFabricante repositorioFabricante, RepositorioEquipamento repositorioEquipamento)
    {
        this.repositorioFabricante = repositorioFabricante;
        this.repositorioEquipamento = repositorioEquipamento;
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

        string erros = novoFabricante.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();
            Console.WriteLine(erros);
            Console.ReadLine();

            CadastrarEquipamento();

            return;
        }

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

        string erros = novoFabricante.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();
            Console.WriteLine(erros);
            Console.ReadLine();

            EditarFabricante();

            return;
        }

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
            "{0, -10} | {1, -15} | {2, -30} | {3, -15} | {4, -20}",
            "Id", "Nome", "E-Mail", "Telefone", "Qtd. Equipamentos"
        );

        Fabricante[] equipamentosCadastrados = repositorioFabricante.SelecionarEquipamentos();

        for (int i = 0; i < equipamentosCadastrados.Length; i++)
        {
            Fabricante f = equipamentosCadastrados[i];

            if (f == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -30} | {3, -15} | {4, -20}",
            f.id, f.nome, f.email, f.telefone, repositorioEquipamento.ObterQuantidadeEquipamentos(f.id)
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }

    public Fabricante ObterDadosFabricante()
    {
        string nome;
        do
        {
            Console.Write("Digite o nome do Fabricante: ");
            nome = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(nome) && nome.Length < 3) Console.WriteLine("\nNome Inválido...\n");
        } while (string.IsNullOrWhiteSpace(nome) && nome.Length < 3);

        string email;
        do
        {
            Console.Write("Digite o e-mail do fabricante: ");
            email = Console.ReadLine()!;
            if (!MailAddress.TryCreate(email, out _)) Console.WriteLine("\nE-mail Inválido...\n");

        } while (!MailAddress.TryCreate(email, out _));

        string telefone;
        do
        {
            Console.Write("Digite o telefone do fabricante: ");
            telefone = Console.ReadLine()!;

            if (telefone.Length < 11) Console.WriteLine("\nTelefone necessita estar no formato 00 0000-0000...\n");
        } while (telefone.Length < 11);

        Fabricante novoFabricante = new Fabricante(nome, email, telefone);
        return novoFabricante;
    }

}
