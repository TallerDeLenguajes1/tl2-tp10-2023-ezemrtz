using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ModificarTareaViewModel
    {  
        public int Id { get; set; }
        public int IdTablero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public EstadoTarea Estado { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public ModificarTareaViewModel(Tarea tarea, List<Usuario> usuarios){
            this.Id = tarea.Id;
            this.IdTablero = tarea.IdTablero;
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.Estado = tarea.Estado;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            this.Usuarios = usuarios;
        }
        public ModificarTareaViewModel(){}
    }
}