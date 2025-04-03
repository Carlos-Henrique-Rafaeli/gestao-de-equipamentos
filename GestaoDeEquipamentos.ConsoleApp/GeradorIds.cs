namespace GestaoDeEquipamentos.ConsoleApp;

public static class GeradorIds
{
    public static int idEquipamentos = 0;

    public static int GerarIdEquipamento()
    {
        idEquipamentos++;
        return idEquipamentos;
    }

}
