using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class CrearTareaViewModel
    {  
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de la tarea")]
        public string Nombre { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [StringLength(20)]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }

        [Display(Name = "Usuario asignado")]
        public int? IdUsuarioAsignado { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public CrearTareaViewModel(Tarea tarea, List<Usuario> usuarios){
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.Estado = tarea.Estado;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            this.Usuarios = usuarios;
        }
        public CrearTareaViewModel(){}
    }
}