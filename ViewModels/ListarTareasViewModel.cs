using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ListarTareasViewModel
    {
        public string? NombreTablero {get;set;}
        public List<TareaTableroUsuarioViewModel> Tareas {get;set;}
        public ListarTareasViewModel(List<Tarea> listaTareas, List<Tablero> listaTableros, List<Usuario> listaUsuarios){
            Tareas = new List<TareaTableroUsuarioViewModel>();
            foreach (var tarea in listaTareas)
            {
                var tablero = listaTableros.FirstOrDefault(t => t.Id == tarea.IdTablero);
                var usuario = listaUsuarios.FirstOrDefault(u => u.Id == tarea.IdUsuarioAsignado);
                var TTUViewModel = new TareaTableroUsuarioViewModel(tarea, tablero, usuario);
                NombreTablero = TTUViewModel.NombreTablero;
                Tareas.Add(TTUViewModel);
            }
        }
    }
}