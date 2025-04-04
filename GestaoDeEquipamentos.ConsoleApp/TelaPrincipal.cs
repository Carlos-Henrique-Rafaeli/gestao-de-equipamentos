﻿namespace GestaoDeEquipamentos.ConsoleApp;

class TelaPrincipal
{
    public string ApresentarMenu()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestor do Inventário");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine("1 - Gestor de Equipamentos");
        Console.WriteLine("2 - Gestor de Chamados");
        Console.WriteLine("S - Sair do Aplicativo");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }
}
