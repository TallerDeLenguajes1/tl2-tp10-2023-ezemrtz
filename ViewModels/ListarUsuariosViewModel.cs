using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_ezemrtz.ViewModels
{
    public class ListarUsuariosViewModel
    {
        public List<Usuario> Usuarios {get;set;}
        public ListarUsuariosViewModel(List<Usuario> listaUsuarios){
            Usuarios = listaUsuarios;
        }
    }
}