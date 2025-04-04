namespace GestaoDeEquipamentos.ConsoleApp;

public class TelaChamado
{
    Chamado[] chamados = new Chamado[100];
    int contadorChamados = 0;

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
        Console.WriteLine("5 - Gestão de Equipamentos");
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

        Console.Write("Digite o título do chamado: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Digite a descrição do chamado: ");
        string descricao = Console.ReadLine()!;

        Console.Write("Digite o id do equipamento: ");
        int idEquipamento = Convert.ToInt32(Console.ReadLine()!);

        DateTime dataAbertura = DateTime.Now;

        Equipamento equipamentoNovo;

        Chamado novoChamado;

        bool conseguiuCriar = false;

        for (int i = 0; i < TelaEquipamento.equipamentos.Length; i++)
        {
            equipamentoNovo = TelaEquipamento.equipamentos[i];

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
            "{0, -10} | {1, -15} | {2, -15} | {3, -10} | {4, -10} | {5, -10}",
            "Id", "Título", "Descrição", "Equipamento", "Data de Abertura", "Dias Abertos"
        );

        for (int i = 0; i < chamados.Length; i++)
        {
            Chamado e = chamados[i];

            if (e == null) continue;

            TimeSpan diferencaTempo = DateTime.Now - e.dataAbertura;
            int diasPassados = (int)diferencaTempo.TotalDays;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -10} | {4, -10} | {5, -10}",
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

        Console.Write("Digite o Id do registro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine()!);

        Console.Write("Digite o título do chamado: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Digite a descrição do chamado: ");
        string descricao = Console.ReadLine()!;

        Console.Write("Digite o id do equipamento: ");
        int idEquipamento = Convert.ToInt32(Console.ReadLine()!);

        Console.Write("Digite a data em que o chamado foi criado: ");
        DateTime dataAbertura = Convert.ToDateTime(Console.ReadLine()!);

        Equipamento equipamentoNovo;

        Chamado novoChamado;

        bool conseguiuEditar = false;

        for (int i = 0; i < chamados.Length; i++)
        {
            if (chamados[i] == null) continue;
            
            else if (chamados[i].id == idEquipamento)
            {
                chamados[i].titulo = titulo;
                chamados[i].descricao = descricao;
                chamados[i].dataAbertura = dataAbertura;
                for (int j = 0; j < TelaEquipamento.equipamentos.Length; j++)
                {
                    equipamentoNovo = TelaEquipamento.equipamentos[j];

                    if (equipamentoNovo == null) continue;

                    else if (equipamentoNovo.id == idEquipamento)
                    {
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

        Console.Write("Digite o Id do registro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine()!);

        bool conseguiuExcluir = false;

        for (int i = 0; i < chamados.Length; i++)
        {
            if (chamados[i] == null) continue;

            else if (chamados[i].id == idSelecionado)
            {
                conseguiuExcluir = true;
                chamados[i] = null;
            }
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
