using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class TareaTableroUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID")]
        public int IdTarea { get; set; }
        public string NombreTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de la tarea")]
        public string NombreTarea { get; set; }

        [StringLength(50)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        
        [StringLength(20)]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        [Display(Name = "Usuario asignado")]
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