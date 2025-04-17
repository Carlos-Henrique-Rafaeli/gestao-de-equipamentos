using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using System.Net.Mail;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class TelaFabricante : TelaBase
{
    public RepositorioFabricante repositorioFabricante;
    public RepositorioEquipamento repositorioEquipamento;

    public TelaFabricante(RepositorioFabricante repositorioFabricante, 
        RepositorioEquipamento repositorioEquipamento)
        : base("Fabricante", repositorioFabricante)
    {
        this.repositorioFabricante = repositorioFabricante;
        this.repositorioEquipamento = repositorioEquipamento;
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Fabricantes...");
            Console.WriteLine("-------------------------------------");
        }

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

            if (f == null) continue;

            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -30} | {3, -30} | {4, -20}",
                f.Id, f.Nome, f.Email, f.Telefone, f.QuantidadeEquipamentos
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }

    public override Fabricante ObterDados()
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
