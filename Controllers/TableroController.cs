using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_ezemrtz.Models;
using tl2_tp10_2023_ezemrtz.Repositorios;

namespace tl2_tp10_2023_ezemrtz.Controllers;


public class TableroController : Controller
{
    private TableroRepository tableroRepository;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    [HttpPost("api/CrearTablero")]
    public IActionResult CreateTablero(Tablero tablero){
        tableroRepository.Create(tablero);
        return RedirectToAction("Index");
    }
    [HttpGet("api/GetTableros")]
    public IActionResult GetAllTableros(){
        var tableros = tableroRepository.GetAll();
        return View(tableros);
    }
    [HttpPut("api/EditarTablero")]
    public IActionResult Update(int id,Tablero tablero){
        tableroRepository.Update(id,tablero);
        var tableroModificado = tableroRepository.Get(id);
        return View(tableroModificado);
    }
    [HttpDelete("api/EliminarTablero")]
    public IActionResult Delete(int id){
        tableroRepository.Remove(id);
        return RedirectToAction("Index");
    }

}