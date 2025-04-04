namespace GestaoDeEquipamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        bool equipamento = true;

        TelaEquipamento telaEquipamento = new TelaEquipamento();
        TelaChamado telaChamado = new TelaChamado();

        while (true)
        {
            string opcaoEscolhida;

            if (equipamento) opcaoEscolhida = telaEquipamento.ApresentarMenu();
            else opcaoEscolhida = telaChamado.ApresentarMenu();

            switch (opcaoEscolhida)
            {
                case "1":
                    if (equipamento) telaEquipamento.CadastrarEquipamento();
                    
                    else telaChamado.CadastrarChamado();
                    
                    break;

                case "2":
                    if (equipamento) telaEquipamento.EditarEquipamento();
                    
                    else telaChamado.EditarChamado();
                    
                    break;

                case "3":
                    if (equipamento) telaEquipamento.ExcluirEquipamento();

                    else telaChamado.ExcluirChamado();

                    break;

                case "4":
                    if (equipamento) telaEquipamento.VisualizarEquipamentos(true);
                    
                    else telaChamado.VisualizarChamado(true);

                    break;
                
                case "5":
                    equipamento = !equipamento;
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
