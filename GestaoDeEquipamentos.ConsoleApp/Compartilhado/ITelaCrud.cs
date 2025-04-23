namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public interface ITelaCrud
{
    string ApresentarMenu();
    void CadastrarRegistro();
    void EditarRegistro();
    void ExcluirRegistro();
    void VisualizarRegistros(bool exibirCabecalho);
}
