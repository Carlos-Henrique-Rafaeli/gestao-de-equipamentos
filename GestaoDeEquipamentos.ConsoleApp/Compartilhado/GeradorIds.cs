namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;
public static class GeradorIds
{
    public static int idEquipamentos = 0;
    public static int idChamados = 0;

    public static int GerarIdEquipamento()
    {
        idEquipamentos++;
        return idEquipamentos;
    }

    public static int GerarIdChamado()
    {
        idChamados++;
        return idChamados;
    }
}
