using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Extensions;
using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers;

[Route("fabricantes")]
public class ControladorFabricante : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioFabricante repositorioFabricante;

    public ControladorFabricante()
    {
        contextoDados = new ContextoDados(true);
        repositorioFabricante = new RepositorioFabricante(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarFabricanteViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastar(CadastrarFabricanteViewModel cadastrarVM)
    {
        var novoFabricante = cadastrarVM.ParaEntidade();

        repositorioFabricante.CadastrarRegistro(novoFabricante);

        var notificacaoVM = new NotificacaoViewModel(
            "Fabricante Cadastrado!",
            $"O registro \"{novoFabricante.Nome}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id)
    {
        var fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        var editarVM = new EditarFabricanteViewModel(
            id,
            fabricanteSelecionado.Nome,
            fabricanteSelecionado.Email,
            fabricanteSelecionado.Telefone
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id, EditarFabricanteViewModel editarVM)
    {
        var fabricanteEditado = editarVM.ParaEntidade();

        repositorioFabricante.EditarRegistro(id, fabricanteEditado);

        var notificacaoVM = new NotificacaoViewModel(
            "Fabricante Editado!",
            $"O registro \"{fabricanteEditado.Nome}\" foi editado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        var fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirFabricanteViewModel(
            fabricanteSelecionado.Id,
            fabricanteSelecionado.Nome
        );

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirConfirmado([FromRoute] int id)
    {
        repositorioFabricante.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Fabricante Excluído!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var fabricantes = repositorioFabricante.SelecionarRegistros();

        var visualizarVM = new VisualizarFabricantesViewModel(fabricantes);

        return View(visualizarVM);
    }
}
