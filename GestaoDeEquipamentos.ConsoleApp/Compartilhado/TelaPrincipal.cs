namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

class TelaPrincipal
{
    public string ApresentarMenu()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("|        Gestor do Inventário       |");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine("1 - Controle de Equipamentos");
        Console.WriteLine("2 - Controle de Chamados");
        Console.WriteLine("3 - Controle de Fabricantes");
        Console.WriteLine("S - Sair do Aplicativo");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }
}
