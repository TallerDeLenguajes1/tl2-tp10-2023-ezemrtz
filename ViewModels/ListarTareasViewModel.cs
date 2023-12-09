using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ListarTareasViewModel
    {
        public List<Tarea> Tareas {get;set;}
        public ListarTareasViewModel(List<Tarea> listaTareas){
            Tareas = listaTareas;
        }
    }
}