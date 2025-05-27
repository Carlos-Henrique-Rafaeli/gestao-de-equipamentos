using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Extensions;
using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers;

[Route("chamados")]
public class ControladorChamado : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioChamado repositorioChamado;
    private IRepositorioEquipamento repositorioEquipamento;

    public ControladorChamado()
    {
        contextoDados = new ContextoDados(true);
        repositorioChamado = new RepositorioChamado(contextoDados);
        repositorioEquipamento = new RepositorioEquipamento(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();

        var cadastrarVM = new CadastrarChamadoViewModel(equipamentos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastar(CadastrarChamadoViewModel cadastrarVM)
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();
        
        var novoChamado = cadastrarVM.ParaEntidade(equipamentos);

        repositorioChamado.CadastrarRegistro(novoChamado);

        var notificacaoVM = new NotificacaoViewModel(
            "Chamado Cadastrado!",
            $"O registro \"{novoChamado.Titulo}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id)
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();

        var chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(id);

        var editarVM = new EditarChamadoViewModel(
            id,
            chamadoSelecionado.Titulo,
            chamadoSelecionado.Descricao,
            chamadoSelecionado.Equipamento.Id,
            equipamentos
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id, EditarChamadoViewModel editarVM)
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();

        var chamadoSelecionado = editarVM.ParaEntidade(equipamentos);

        repositorioChamado.EditarRegistro(id, chamadoSelecionado);

        var notificacaoVM = new NotificacaoViewModel(
            "Chamado Editado!",
            $"O registro \"{chamadoSelecionado.Titulo}\" foi editado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult Excluir([FromRoute] int id)
    {
        var chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirChamadoViewModel(
            chamadoSelecionado.Id,
            chamadoSelecionado.Titulo
        );

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirConfirmado([FromRoute] int id)
    {
        repositorioChamado.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Chamado Excluído!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var chamados = repositorioChamado.SelecionarRegistros();

        var visualizarVM = new VisualizarChamadosViewModel(chamados);

        return View(visualizarVM);
    }
}
