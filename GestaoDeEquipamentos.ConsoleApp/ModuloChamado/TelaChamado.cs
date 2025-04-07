using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

public class TelaChamado
{
    public RepositorioEquipamento repositorioEquipamento;
    public RepositorioChamado repositorioChamado;

    public TelaChamado(RepositorioEquipamento repositorioEquipamento)
    {
        this.repositorioEquipamento = repositorioEquipamento;
        repositorioChamado = new RepositorioChamado();
    }

    public string ApresentarMenu()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Chamados");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine("1 - Cadastro de Chamado");
        Console.WriteLine("2 - Edição de Chamado");
        Console.WriteLine("3 - Exclusão de Chamado");
        Console.WriteLine("4 - Visualizar Chamados");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }

    public void CadastrarChamado()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Chamado...");
        Console.WriteLine("-------------------------------------");

        Chamado novoChamado = ObterDadosChamado();

        repositorioChamado.CadastrarChamado(novoChamado);
    }

    

    

    

    public void EditarChamado()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Chamado...");
        Console.WriteLine("-------------------------------------");

        VisualizarChamado(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Chamado novoChamado = ObterDadosChamado();

        bool conseguiuEditar = repositorioChamado.EditarChamado(idSelecionado, novoChamado);


        if (!conseguiuEditar)
        {
            Console.WriteLine("Erro ao editar chamado...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Chamado editado com sucesso!");
        Console.ReadLine();
    }

    public void ExcluirChamado()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Chamado...");
        Console.WriteLine("-------------------------------------");

        VisualizarChamado(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        bool conseguiuExcluir = repositorioChamado.ExcluirChamado(idSelecionado);

        if (!conseguiuExcluir)
        {
            Console.WriteLine("Houve um erro durante a exclusão...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Chamado excluído com sucesso!");
        Console.ReadLine();
    }

    public void VisualizarChamado(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Chamados...");
            Console.WriteLine("-------------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -17} | {5, -10}",
            "Id", "Título", "Descrição", "Equipamento", "Data de Abertura", "Dias Abertos"
        );

        Chamado[] chamadosCadastrados = repositorioChamado.SelecionarChamados();

        for (int i = 0; i < chamadosCadastrados.Length; i++)
        {
            Chamado c = chamadosCadastrados[i];

            if (c == null) continue;

            TimeSpan diferencaTempo = DateTime.Now - c.dataAbertura;
            int diasPassados = (int)diferencaTempo.TotalDays;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -17} | {5, -10}",
            c.id, c.titulo, c.descricao, c.equipamento.nome, c.dataAbertura.ToShortDateString(), diasPassados
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

        Console.WriteLine();
    }

    private void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Chamados");
        Console.WriteLine("-------------------------------------");
    }

    public Chamado ObterDadosChamado()
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

        Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarEquipamentoPorId(idEquipamento);

        Chamado novoChamado = new Chamado(titulo, descricao, equipamentoSelecionado);

        return novoChamado;
    }
}
