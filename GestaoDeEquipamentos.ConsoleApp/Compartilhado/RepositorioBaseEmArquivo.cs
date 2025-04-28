using System.Text.Json;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public abstract class RepositorioBaseEmArquivo<T> where T: EntidadeBase<T>
{
    private List<T> registros = new List<T>();
    private int contadorIds = 0;

    private string caminhoPastaTemp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "GestaoDeEquipamentos");
    private string nomeArquivo;

    protected RepositorioBaseEmArquivo(string nomeArquivo)
    {
        this.nomeArquivo = nomeArquivo;

        registros = Desserializar();

        int maiorId = 0;

        foreach (var registro in registros)
        {
            if (registro.Id > maiorId)
                maiorId = registro.Id;
        }

        contadorIds = maiorId;
    }

    public void CadastrarRegistro(T novoRegistro)
    {
        novoRegistro.Id = ++contadorIds;

        registros.Add(novoRegistro);

        Serializar();
    }

    public bool EditarRegistro(int idRegistro, T registroEditado)
    {
        foreach (T item in registros)
        {
            if (item.Id == idRegistro)
            {
                item.AtualizarRegistro(registroEditado);

                Serializar();

                return true;
            }
        }

        return false;
    }

    public bool ExcluirRegistro(int idRegistro)
    {
        T registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado != null)
        {
            registros.Remove(registroSelecionado);

            Serializar();

            return true;
        }

        return false;
    }

    public List<T> SelecionarRegistros()
    {
        return registros;
    }

    public T SelecionarRegistroPorId(int idRegistro)
    {
        foreach (T item in registros)
        {
            if (item.Id == idRegistro)
                return item;
        }

        return null;
    }

    protected void Serializar()
    {
        if (!Directory.Exists(caminhoPastaTemp))
            Directory.CreateDirectory(caminhoPastaTemp);
        
        string caminhoCompleto = Path.Combine(caminhoPastaTemp, nomeArquivo);

        string json = JsonSerializer.Serialize(registros);

        File.WriteAllText(caminhoCompleto, json);
    }

    protected List<T> Desserializar()
    {
        List<T> registrosArmazenados = [];

        string caminhoCompleto = Path.Combine(caminhoPastaTemp, nomeArquivo);

        if (!File.Exists(caminhoCompleto))
            return registrosArmazenados;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json))
            return registrosArmazenados;

        registrosArmazenados = JsonSerializer.Deserialize<List<T>>(json)!;

        return registrosArmazenados;
    }
}
