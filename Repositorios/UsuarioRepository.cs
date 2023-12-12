using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace tl2_tp10_2023_ezemrtz.Repositorios{
    public class UsuarioRepository : IUsuarioRepository{
        private readonly string cadenaConexion;
        public UsuarioRepository(string CadenaDeConexion){
            this.cadenaConexion = CadenaDeConexion;
        }
        public void Create(Usuario usuario){
            var query = @"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@nombre, @password, @rol);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@password", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));

                var filas = command.ExecuteNonQuery();
                connection.Close();   

                if(filas == 0) throw new Exception("Hubo un problema al crear el usuario");
            }
        }

        public void Update(int id, Usuario usuario){
            var query = $"UPDATE Usuario SET nombre_de_usuario = @name, contrasenia = @password, rol = @rol WHERE id = @id";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@password", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al modificar el usuario");
            }
            
        }
        public List<Usuario> GetAll(){
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    usuarios = new List<Usuario>();
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (NivelAcceso)Convert.ToInt32(reader["rol"]);
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            if(usuarios == null) throw new Exception("No se encontraron usuarios");

            return usuarios;
        }
        public Usuario Get(int id){
            var queryString = "SELECT * FROM Usuario WHERE id = @idUser";

            Usuario usuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", id));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (NivelAcceso)Convert.ToInt32(reader["rol"]);
                    }
                }
                connection.Close();
            }
            if(usuario == null) throw new Exception("Usuario no encontrado");

            return (usuario);
        }
        public Usuario GetByNamePassword(string nombre, string contrasenia){
            var queryString = "SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre AND contrasenia = @contrasenia";

            Usuario usuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", contrasenia));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (NivelAcceso)Convert.ToInt32(reader["rol"]);
                    }
                }
                connection.Close();
            }
            if(usuario == null) throw new Exception("No existe el usuario");

            return (usuario);
        }
        public void Remove(int id){
            var queryString = "DELETE FROM Usuario WHERE id = @idUser";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", id));
                var filas = command.ExecuteNonQuery();
                connection.Close();
                if(filas == 0) throw new Exception("Hubo un problema al eliminar el usuario");
            }
        }
    }
}