namespace GestaoDeEquipamentos.ConsoleApp;

public class TelaChamado
{
    Chamado[] chamados = new Chamado[100];
    Equipamento[] equipamentos;
    int contadorChamados = 0;

    public TelaChamado(Equipamento[] equipamentos)
    {
        this.equipamentos = equipamentos;
    }

    public string ApresentarMenu()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Chamados");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Escolha a operação desejada:");
        Console.WriteLine("1 - Cadastro de Chamado");
        Console.WriteLine("2 - Edição de Chamado");
        Console.WriteLine("3 - Exclusão de Chamado");
        Console.WriteLine("4 - Visualizar Chamados");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine("-------------------------------------");

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }

    public void CadastrarChamado()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Chamados");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Cadastrando Chamado...");
        Console.WriteLine("-------------------------------------");

        string titulo;
        do
        {
            Console.Write("Digite o título do chamado: ");
            titulo = Console.ReadLine()!;
            if (String.IsNullOrEmpty(titulo)) Console.WriteLine("\nTítulo Inválido...\n");

        } while (String.IsNullOrEmpty(titulo));

        string descricao;
        do
        {
            Console.Write("Digite a descrição do chamado: ");
            descricao = Console.ReadLine()!;
            if (String.IsNullOrEmpty(descricao)) Console.WriteLine("\nDescrição Inválida...\n");

        } while (String.IsNullOrEmpty(descricao));

        int idEquipamento;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do equipamento: ");
            idValido = int.TryParse(Console.ReadLine(), out idEquipamento);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        DateTime dataAbertura = DateTime.Now;

        Equipamento equipamentoNovo;

        Chamado novoChamado;

        bool conseguiuCriar = false;

        for (int i = 0; i < equipamentos.Length; i++)
        {
            equipamentoNovo = equipamentos[i];

            if (equipamentoNovo == null) continue;
            
            else if (equipamentoNovo.id == idEquipamento)
            {
                novoChamado = new Chamado(titulo, descricao, equipamentoNovo, dataAbertura);
                novoChamado.id = GeradorIds.GerarIdChamados();
                chamados[contadorChamados++] = novoChamado;
                conseguiuCriar = true;
                break;
            }
        }

        if (!conseguiuCriar)
        {
            Console.WriteLine("Erro ao criar chamado...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Chamado criado com sucesso!");
        Console.ReadLine();
    }

    public void VisualizarChamado(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Gestão de Chamados");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Visualizando Chamados...");
            Console.WriteLine("-------------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -17} | {5, -10}",
            "Id", "Título", "Descrição", "Equipamento", "Data de Abertura", "Dias Abertos"
        );

        for (int i = 0; i < chamados.Length; i++)
        {
            Chamado e = chamados[i];

            if (e == null) continue;

            TimeSpan diferencaTempo = DateTime.Now - e.dataAbertura;
            int diasPassados = (int)diferencaTempo.TotalDays;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -17} | {5, -10}",
            e.id, e.titulo, e.descricao, e.equipamento.nome, e.dataAbertura.ToShortDateString(), diasPassados
        );
        }

        if (exibirTitulo) Console.ReadLine();
    }

    public void EditarChamado()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Chamados");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Editando Chamado...");
        Console.WriteLine("-------------------------------------");

        VisualizarChamado(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        string titulo;
        do
        {
            Console.Write("Digite o título do chamado: ");
            titulo = Console.ReadLine()!;
            if (String.IsNullOrEmpty(titulo)) Console.WriteLine("\nTítulo Inválido...\n");

        } while (String.IsNullOrEmpty(titulo));

        string descricao;
        do
        {
            Console.Write("Digite a descrição do chamado: ");
            descricao = Console.ReadLine()!;
            if (String.IsNullOrEmpty(descricao)) Console.WriteLine("\nDescrição Inválida...\n");

        } while (String.IsNullOrEmpty(descricao));

        int idEquipamento;
        bool idEquipamentoValido;
        do
        {
            Console.Write("Digite o Id do equipamento: ");
            idEquipamentoValido = int.TryParse(Console.ReadLine(), out idEquipamento);

            if (!idEquipamentoValido) Console.WriteLine("\nId Inválido...");

        } while (!idEquipamentoValido);

        DateTime dataAbertura;
        bool dataValida;
        do
        {
            Console.Write("Digite a data da abertura do chamado: (dd/mm/yyyy) ");
            dataValida = DateTime.TryParse(Console.ReadLine(), out dataAbertura);

            if (!dataValida) Console.WriteLine("\nData Inválida...\n");

        } while (!dataValida);

        Equipamento equipamentoNovo;

        bool conseguiuEditar = false;

        for (int i = 0; i < chamados.Length; i++)
        {
            if (chamados[i] == null) continue;
            
            else if (chamados[i].id == idSelecionado)
            {
                for (int j = 0; j < equipamentos.Length; j++)
                {
                    equipamentoNovo = equipamentos[j];

                    if (equipamentoNovo == null) continue;

                    else if (equipamentoNovo.id == idEquipamento)
                    {
                        chamados[i].titulo = titulo;
                        chamados[i].descricao = descricao;
                        chamados[i].dataAbertura = dataAbertura;
                        chamados[i].equipamento = equipamentoNovo;
                        conseguiuEditar = true;
                        break;
                    }
                }
                
                break;
            }
        }

        if (!conseguiuEditar)
        {
            Console.WriteLine("Erro ao editar chamado...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Chamado editado com sucesso!");
        Console.ReadLine();
    }

    public void ExcluirChamado()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Gestão de Chamados");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Excluindo Chamado...");
        Console.WriteLine("-------------------------------------");

        VisualizarChamado(false);

        int idSelecionado;
        bool idValido;
        do
        {
            Console.Write("Digite o Id do registro que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idSelecionado);

            if (!idValido) Console.WriteLine("\nId Inválido...\n");

        } while (!idValido);

        bool conseguiuExcluir = false;

        int indice = -1;

        for (int i = 0; i < chamados.Length; i++)
        {
            if (chamados[i] == null) continue;

            else if (chamados[i].id == idSelecionado)
            {
                conseguiuExcluir = true;
                indice = i;
            }
        }

        if (conseguiuExcluir)
        {
            Console.Write("Deseja mesmo exlcuir? (S/N) ");
            string opcaoExcluir = Console.ReadLine()!.ToUpper();

            if (opcaoExcluir == "S") chamados[indice] = null;

            else return;
        }


        if (!conseguiuExcluir)
        {
            Console.WriteLine("Houve um erro durante a exclusão...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Chamado excluído com sucesso!");
        Console.ReadLine();
    }
}
