using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        TelaPrincipal telaPrincipal = new TelaPrincipal();
        TelaEquipamento telaEquipamento = new TelaEquipamento();
        TelaFabricante telaFabricante = new TelaFabricante();

        RepositorioEquipamento repositorioEquipamento = telaEquipamento.repositorioEquipamento;

        TelaChamado telaChamado = new TelaChamado(repositorioEquipamento);

        while (true)
        {
            bool deveRodar = true;

            string opcaoEscolhida = telaPrincipal.ApresentarMenu();

            switch (opcaoEscolhida)
            {
                case "1":
                    while (deveRodar)
                    {
                        opcaoEscolhida = telaEquipamento.ApresentarMenu();
                        
                        switch (opcaoEscolhida)
                        {
                            case "1": telaEquipamento.CadastrarEquipamento(); break;

                            case "2": telaEquipamento.EditarEquipamento(); break;
                            
                            case "3": telaEquipamento.ExcluirEquipamento(); break;
                            
                            case "4": telaEquipamento.VisualizarEquipamentos(true); break;
                            
                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida..."); Console.ReadLine(); break;
                        }
                    }
                    break;

                case "2":
                    while (deveRodar)
                    {
                        opcaoEscolhida = telaChamado.ApresentarMenu();

                        switch (opcaoEscolhida)
                        {
                            case "1": telaChamado.CadastrarChamado(); break;

                            case "2": telaChamado.EditarChamado(); break;

                            case "3": telaChamado.ExcluirChamado(); break;

                            case "4": telaChamado.VisualizarChamado(true); break;

                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida..."); Console.ReadLine(); break;
                        }
                    }
                    break;

                case "S": return;

                default: Console.WriteLine("Opção Inválida..."); Console.ReadLine(); break;
            }
        }
    }
}
