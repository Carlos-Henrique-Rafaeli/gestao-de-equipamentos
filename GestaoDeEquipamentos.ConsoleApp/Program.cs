namespace GestaoDeEquipamentos.ConsoleApp;

internal class Program
{
   

    static void Main(string[] args)
    {
        TelaEquipamento telaEquipamento = new TelaEquipamento();

        while (true)
        {
            string opcaoEscolhida = telaEquipamento.ApresentarMenu();

            switch (opcaoEscolhida)
            {
                case "1":
                    telaEquipamento.CadastrarEquipamento();
                    break;

                case "2":
                    telaEquipamento.EditarEquipamento();
                    break;

                case "3":
                    telaEquipamento.EditarEquipamento();
                    break;

                case "4":
                    telaEquipamento.VisualizarEquipamentos(true);
                    break;

                case "S":
                    return;

                default:
                    Console.WriteLine("Opção Inválida...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
