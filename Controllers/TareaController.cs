using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;


namespace tl2_tp10_2023_ezemrtz.Controllers;

[ApiController]
[Route("[controller]")]
public class TareaController : Controller
{
    private TareaRepository tareaRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    [HttpPost("api/CrearTarea")]
    public IActionResult CreateTarea(int idTablero, Tarea tarea){
        tareaRepository.Create(idTablero,tarea);
        return RedirectToAction("Index");
    }

    [HttpGet("api/tarea/GetTareas")]
    public IActionResult GetTareaPorTablero(int id){
        var tareas = tareaRepository.GetByTablero(id);
        return View(tareas);
    }
    [HttpPut("api/ModificarTarea")]
    public IActionResult UpdateTarea(int id, string nombre){
        var tarea = tareaRepository.Get(id);
        tarea.Nombre = nombre;
        tareaRepository.Update(id,tarea);
        return View(tarea);
    }
    [HttpDelete("api/EliminarTarea")]
    public IActionResult DeleteTarea(int id){
        tareaRepository.Remove(id);
        return RedirectToAction("Index");
    }
}