using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace tl2_tp10_2023_ezemrtz.Repositorios{
    public class TareaRepository : ITareaRepository{
        private readonly string cadenaConexion;
        public TareaRepository(string CadenaDeConexion){
            this.cadenaConexion = CadenaDeConexion;
        }
        public void Create(int idTablero, Tarea tarea){
            var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@idTablero, @nombre, @estado, @descripcion, @color, @idUser)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUser", tarea.IdUsuarioAsignado));

                var filas = command.ExecuteNonQuery();
                connection.Close();   

                if(filas == 0) throw new Exception("Hubo un problema al crear la tarea");
            }
        }
        public void Update(int id, Tarea tarea){
            var query = $"UPDATE Tarea SET id_tablero = @idTablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @idUser WHERE id = @id";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUser", tarea.IdUsuarioAsignado));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al modificar la tarea");
            }
        }
        public Tarea Get(int id){
            var queryString = "SELECT * FROM Tarea WHERE id = @id";

            Tarea tarea = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                    }
                }
                connection.Close();
            }
            if(tarea == null) throw new Exception("Tarea no encontrada");
            return (tarea);
        }
        public List<Tarea> GetByTablero(int idTablero){
            var queryString = "SELECT * FROM Tarea WHERE id_tablero = @idTablero";

            List<Tarea> tareas = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if(tareas == null) throw new Exception("Hubo un problema al buscar las tareas asociadas a un tablero");
            return tareas;
        }
        public List<Tarea> GetByEstado(int estado){
            var queryString = "SELECT * FROM Tarea WHERE estado = @estado";

            List<Tarea> tareas = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@estado", estado));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if(tareas == null) throw new Exception("Hubo un problema al buscar las tareas");
            return (tareas);
        }
        public List<Tarea> GetByUser(int idUsuario){
            var queryString = "SELECT * FROM Tarea WHERE id_usuario_asignado = @idUser";

            List<Tarea> tareas = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value ? Convert.ToInt32(reader["id_usuario_asignado"]) : (int?)null;
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if(tareas == null) throw new Exception("Hubo un problema al buscar las tareas");
            return (tareas);
        }
        public void Remove(int id){
            var queryString = "DELETE FROM Tarea WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                var filas = command.ExecuteNonQuery();
                connection.Close();
                if(filas == 0) throw new Exception("No existe la tarea a eliminar");
            }
        }
        public void AsignarAUsuario(int idUsuario, int idTarea){
            var query = $"UPDATE Tarea SET id_usuario_asignado = @idUser WHERE id = @idTarea";

            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al asignar un usuario a la tarea");
            }
        }
    }
}