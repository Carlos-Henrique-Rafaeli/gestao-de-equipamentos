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

        while (true)
        {
            bool deveRodar = true;

            telaPrincipal.ApresentarMenuPrincipal();

            TelaBase telaSelecionada = telaPrincipal.ObterTela();

            if (telaSelecionada == null) return;

            while (deveRodar)
            {
                string opcaoEscolhida = telaSelecionada.ApresentarMenu();

                switch (opcaoEscolhida)
                {
                    case "1": telaSelecionada.CadastrarRegistro(); break;

                    case "2": telaSelecionada.EditarRegistro(); break;

                    case "3": telaSelecionada.ExcluirRegistro(); break;

                    case "4": telaSelecionada.VisualizarRegistros(true); break;

                    case "S": deveRodar = false; break;

                    default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
                }
            }
        }
    }
}
