using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class Fabricante
{
    public int id;
    public string nome;
    public string email;
    public int telefone;

    public Fabricante(string nome, string email, int telefone)
    {
        this.nome = nome;
        this.email = email;
        this.telefone = telefone;
        telefone = GeradorIds.GerarIdFabricante();
    }
}
