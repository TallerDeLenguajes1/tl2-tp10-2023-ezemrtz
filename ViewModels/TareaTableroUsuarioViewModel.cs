using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class TareaTableroUsuarioViewModel
    {
        public int IdTarea { get; set; }
        public string NombreTablero { get; set; }
        public string NombreTarea { get; set; }
        public string? Descripcion { get; set; }
        public string? Color { get; set; }
        public EstadoTarea Estado { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public string? NombreUsuarioAsignado { get; set; }

        public TareaTableroUsuarioViewModel(Tarea tarea, Tablero tablero, Usuario? usuario){
            IdTarea = tarea.Id;
            NombreTablero = tablero.Nombre;
            NombreTarea = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            if (usuario != null)  NombreUsuarioAsignado = usuario.NombreDeUsuario;
        }
    }
}