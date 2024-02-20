using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class CrearUsuarioViewModel
    {  
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de usuario")]
        public string Nombre {get;set;}      

        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [StringLength(30)]
        [Display(Name = "Contraseña")]  
        public string Contrasenia {get;set;}   
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [StringLength(30)]
        [Compare("Contrasenia", ErrorMessage = "No coincide con la contraseña")]
        [Display(Name = "Confirmar Contraseña")]  
        public string Confirmacion {get;set;}   

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Rol")]     
        public NivelAcceso Rol {get;set;}

        public CrearUsuarioViewModel(){}
    }
}