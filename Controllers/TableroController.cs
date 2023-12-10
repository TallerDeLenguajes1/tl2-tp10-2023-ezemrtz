using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;
using tl2_tp10_2023_ezemrtz.ViewModels;

namespace tl2_tp10_2023_ezemrtz.Controllers;


public class TableroController : Controller
{
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index(){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(esAdmin()){
            var tableros = _tableroRepository.GetAll();
            return View(new ListarTablerosViewModel(tableros));
        }else{
            return View(new ListarTablerosViewModel(_tableroRepository.GetByUser((int)HttpContext.Session.GetInt32("id"))));
        }
    }

    [HttpGet]
    public IActionResult CreateTablero(){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        return View(new CrearTableroViewModel{IdUsuarioPropietario = (int)HttpContext.Session.GetInt32("id")});
    }

    [HttpPost]
    public IActionResult CreateTablero(CrearTableroViewModel tablero){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(!ModelState.IsValid) return RedirectToAction("Index");

        _tableroRepository.Create(new Tablero(tablero));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateTablero(int id){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        return View(new ModificarTableroViewModel(_tableroRepository.Get(id)));
    }
    [HttpPost]
    public IActionResult UpdateTablero(ModificarTableroViewModel tablero){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(!ModelState.IsValid) return RedirectToAction("Index");
        _tableroRepository.Update(tablero.Id, new Tablero(tablero));
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTablero(int id){
        if(!logueado()) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if(!esAdmin()){
            var tablero = _tableroRepository.Get(id);
            if((int)HttpContext.Session.GetInt32("id") != tablero.IdUsuarioPropietario) return RedirectToAction("Index");
        }
        var tareas = _tareaRepository.GetByTablero(id);
        foreach (var tarea in tareas)
        {
            _tareaRepository.Remove(tarea.Id);
        }
        _tableroRepository.Remove(id);
        return RedirectToAction("Index");
    }

     private bool logueado(){
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin(){
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("rol") == NivelAcceso.administrador.ToString();
    }

}