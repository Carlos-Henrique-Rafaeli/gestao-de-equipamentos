using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System.Net.Mail;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class Fabricante
{
    public int id;
    public string nome;
    public string email;
    public string telefone;

    public Fabricante(string nome, string email, string telefone)
    {
        this.nome = nome;
        this.email = email;
        this.telefone = telefone;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(nome)) erros += "O campo 'Nome' é obrigatório.\n";

        else if (nome.Length < 3) erros += "O campo 'Nome' precisa conter ao menos 3 caracteres.\n";

        if (string.IsNullOrWhiteSpace(email)) erros += "O campo 'Email' é obrigatório.\n";

        else if (!MailAddress.TryCreate(email, out _)) erros += "O campo 'Email' deve estar em um formato válido.\n";

        if (string.IsNullOrWhiteSpace(telefone)) erros += "O campo 'Telefone' é obrigatório.\n";

        if (telefone.Length < 12) erros += "O campo 'Telefone' deve seguir o formato 00 0000-0000.";

        return erros;
    }
}
