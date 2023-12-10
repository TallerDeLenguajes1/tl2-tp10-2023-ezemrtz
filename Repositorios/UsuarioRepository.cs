using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace tl2_tp10_2023_ezemrtz.Repositorios{
    public class UsuarioRepository : IUsuarioRepository{
        private string cadenaConexion = "Data Source=db/kanban.db;Cache=Shared";
        public void Create(Usuario usuario){
            var query = @"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@nombre, @password, @rol);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@password", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));

                command.ExecuteNonQuery();

                connection.Close();   
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
                command.ExecuteNonQuery();
                connection.Close();
            }
            
        }
        public List<Usuario> GetAll(){
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
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
            return usuarios;
        }
        public Usuario Get(int id){
            var queryString = "SELECT * FROM Usuario WHERE id = @idUser";

            var usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", id));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (NivelAcceso)Convert.ToInt32(reader["rol"]);
                    }
                }
                connection.Close();
            }

            return (usuario);
        }
        public void Remove(int id){
            var queryString = "DELETE FROM Usuario WHERE id = @idUser";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}