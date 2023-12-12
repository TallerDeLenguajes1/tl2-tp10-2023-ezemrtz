using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ListarUsuariosViewModel
    {
        public Usuario UsuarioLogueado {get; set;}
        public List<Usuario> Usuarios {get;set;}
        public ListarUsuariosViewModel(Usuario logueado, List<Usuario> listaUsuarios){
            UsuarioLogueado = logueado;
            Usuarios = listaUsuarios;
        }
    }
}