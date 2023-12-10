using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ModificarUsuarioViewModel
    {  
        public int Id {get;set;}        
        public string Nombre {get;set;}        
        public string Contrasenia {get;set;}        
        public NivelAcceso Rol {get;set;}

        public ModificarUsuarioViewModel(Usuario usuario){
            this.Id = usuario.Id;
            this.Nombre = usuario.NombreDeUsuario;
            this.Contrasenia = usuario.Contrasenia;
            this.Rol = usuario.Rol;
        }
        public ModificarUsuarioViewModel(){}
    }
}