using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private string opcaoPrincipal;
    private RepositorioFabricante repositorioFabricante;
    private RepositorioEquipamento repositorioEquipamento;
    private RepositorioChamado repositorioChamado;

    public TelaPrincipal()
    {
        repositorioFabricante = new RepositorioFabricante();
        repositorioEquipamento = new RepositorioEquipamento();
        repositorioChamado = new RepositorioChamado();
    }


    public void ApresentarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("|        Gestor do Inventário       |");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine("1 - Controle de Fabricantes");
        Console.WriteLine("2 - Controle de Equipamentos");
        Console.WriteLine("3 - Controle de Chamados");
        Console.WriteLine("S - Sair do Aplicativo");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        opcaoPrincipal = Console.ReadLine()!.ToUpper();
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == "1")
            return new TelaFabricante(repositorioFabricante);

        else if (opcaoPrincipal == "2")
            return new TelaEquipamento(repositorioFabricante, repositorioEquipamento);

        else if (opcaoPrincipal == "3")
            return new TelaChamado(repositorioEquipamento, repositorioChamado);

        return null;
    }
}
