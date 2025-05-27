using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers;

[Route("/")]
public class ControladorIncial : Controller
{
    public IActionResult PaginaInicial()
    {
        return View("PaginaInicial");
    }
}
