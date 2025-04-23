namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase<T>
{
    protected string nomeEntidade;
    private RepositorioBase<T> repositorio;

    protected TelaBase(string nomeEntidade, RepositorioBase<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Controle de {nomeEntidade}s");
        Console.WriteLine("--------------------------------------------");
    }

    public virtual string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar {nomeEntidade}");
        Console.WriteLine($"2 - Editar {nomeEntidade}");
        Console.WriteLine($"3 - Excluir {nomeEntidade}");
        Console.WriteLine($"4 - Visualizar {nomeEntidade}s");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        string operacaoEscolhida = Console.ReadLine()!.ToUpper();

        return operacaoEscolhida;
    }

    public virtual void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        T novoRegistro = ObterDados();

        if (novoRegistro == null) return;

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);

            CadastrarRegistro();

            return;
        }

        repositorio.CadastrarRegistro(novoRegistro);

        Console.WriteLine("O registro foi concluído com sucesso!");
    }

    public virtual void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        int idRegistro;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistro);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Console.WriteLine();

        T registroEditado = ObterDados();

        if (registroEditado == null) return;

        string erros = registroEditado.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine(erros);

            EditarRegistro();

            return;
        }

        bool conseguiuEditar = repositorio.EditarRegistro(idRegistro, registroEditado);

        if (!conseguiuEditar)
        {
            Console.WriteLine("Houve um erro durante a edição do registro...");

            return;
        }

        Console.WriteLine("O registro foi editado com sucesso!");
    }

    public virtual void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Excluindo {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        int idRegistro;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistro);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        Console.WriteLine();

        bool conseguiuExcluir = repositorio.ExcluirRegistro(idRegistro);

        if (!conseguiuExcluir)
        {
            Console.WriteLine("Houve um erro durante a exclusão do registro...");

            return;
        }

        Console.WriteLine("O registro foi excluído com sucesso!");
    }

    public abstract void VisualizarRegistros(bool exibirTitulo);

    public abstract T ObterDados();
}
