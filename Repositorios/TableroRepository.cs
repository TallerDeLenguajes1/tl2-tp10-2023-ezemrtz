using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace tl2_tp10_2023_ezemrtz.Repositorios{
    public class TableroRepository : ITableroRepository{
        private readonly string cadenaConexion;
        public TableroRepository(string CadenaDeConexion){
            this.cadenaConexion = CadenaDeConexion;
        }
        public void Create(Tablero tablero){
            var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idUser, @nombre, @descripcion)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idUser", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                var filas = command.ExecuteNonQuery();
                connection.Close();  

                if(filas == 0) throw new Exception("Hubo un problema al crear el tablero");
            }
        }
        public void Update(int id, Tablero tablero){
            var query = $"UPDATE Tablero SET id_usuario_propietario = @idUser, nombre = @nombre, descripcion = @descripcion WHERE id = @id";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@id", id));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al modificar el tablero");
            }
        }
        public List<Tablero> GetAll(){
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    tableros = new List<Tablero>();
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            if(tableros == null) throw new Exception("Hubo un problema al buscar los tableros");
            return tableros;
        }
        public Tablero Get(int id){
            var queryString = "SELECT * FROM Tablero WHERE id = @idTablero";

            Tablero tablero = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
                connection.Close();
            }
            if(tablero == null) throw new Exception("No se encontro ningun tablero");
            return (tablero);
        }
        public List<Tablero> GetByUser(int idUsuario){
            var queryString = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUser";

            List<Tablero> tableros = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    tableros = new List<Tablero>();
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            if(tableros.Count == null) throw new Exception("No se encontro ningun tablero");
            return (tableros);
        }

        public List<Tablero> GetByAssignedTask(int idUsuario){
             var queryString = "SELECT * FROM Tablero INNER JOIN Tarea ON(Tablero.id = id_tablero) WHERE id_usuario_asignado = @idUser AND id_usuario_propietario <> @idUser";

            List<Tablero> tableros = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    tableros = new List<Tablero>();
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            if(tableros == null) throw new Exception("No se encontro ningun tablero");
            return (tableros);
        }
        public void Remove(int id){
            var queryString = "DELETE FROM Tablero WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al eliminar el tablero especificado");
            }
        }
    }
}