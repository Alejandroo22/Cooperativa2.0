using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class ModuleDAO
    {
        // funcion para obtener todos los usuarios
        private ConnectionDB conexion;

        public ModuleDAO()
        {
            // Inicializa la instancia de la clase ConexionBD
            conexion = new ConnectionDB();
        }

        //funcion para obtner los modulos de la bd utilizada en el login y
        public List<Module> ObtenerModulos()
        {
            List<Module> modulos = new List<Module>();

            try
            {
                // Abre la conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT DISTINCT id_modulo, nombre_modulo FROM Modulo";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Module modulo = new Module();
                        modulo.IdModule = Convert.ToInt32(reader["id_modulo"]);
                        modulo.NombreModulo = Convert.ToString(reader["nombre_modulo"]);
                        modulos.Add(modulo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los módulos: " + ex.Message);
            }
            finally
            {
                // Cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return modulos;
        }

        //obtener los modulos al cual pertenece el usuario
        public List<Module> ObtenerModulosDeUsuario(int idUsuario)
        {
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                string query = @"SELECT m.id_modulo, m.nombre_modulo
                            FROM Modulo m
                            INNER JOIN Usuario_Modulo um ON um.id_modulo = m.id_modulo
                            WHERE um.id_usuario = @idUsuario";

                // Crear un comando con la consulta
                conexion.CrearComando(query);
                conexion.AgregarParametro("@idUsuario", idUsuario);

                List<Module> modulos = new List<Module>();

                using (var reader = conexion.EjecutarConsultaReader(query))
                {
                    while (reader.Read())
                    {
                        Module modulo = new Module();
                        modulo.IdModule = Convert.ToInt32(reader["id_modulo"]);
                        modulo.NombreModulo = Convert.ToString(reader["nombre_modulo"]);
                        modulos.Add(modulo);
                    }
                }

                return modulos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los módulos del usuario: " + ex.Message);
                return null;
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        //
        public void InsertarUsuarioModulos(int idUsuario, List<int> modulosSeleccionados)
        {
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                foreach (int idModulo in modulosSeleccionados)
                {
                    string consulta = @"INSERT INTO Usuario_Modulo (id_usuario, id_modulo) VALUES (@idUsuario, @idModulo)";

                    conexion.CrearComando(consulta);

                    conexion.AgregarParametro("@idUsuario", idUsuario);
                    conexion.AgregarParametro("@idModulo", idModulo);

                    // Ejecutar la consulta
                    int filasAfectadas = conexion.EjecutarInstruccion();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("La inserción se realizó correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo realizar la inserción.");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al insertar los módulos del usuario: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

        }

        // Elimina los módulos asociados a un usuario
        public void EliminarModulosDelUsuario(int usuarioId, List<int> modulosEliminar)
        {
            try
            {
                conexion.Conectar();

                // Aquí se crea el comando SQL para eliminar los módulos del usuario
                string consulta = @"DELETE FROM Usuario_Modulo WHERE id_usuario = @IdUsuario AND id_modulo = @IdModulo";
                conexion.CrearComando(consulta);

                // Se agrega el parámetro del usuario una sola vez
                conexion.AgregarParametro("@IdUsuario", usuarioId);

                // Se agrega el parámetro del módulo una sola vez
                conexion.AgregarParametro("@IdModulo", 0); // Valor temporal

                foreach (int idModulo in modulosEliminar)
                {
                    // Se asigna el valor actual del módulo al parámetro
                    conexion.ObtenerParametro("@IdModulo").Value = idModulo;

                    // Se ejecuta la instrucción de eliminación
                    int filasAfectadas = conexion.EjecutarInstruccion();

                    // Aquí puedes agregar lógica adicional si lo deseas, por ejemplo, verificar el número de filas afectadas.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar los módulos del usuario: " + ex.Message);
                // Puedes agregar aquí cualquier manejo de excepciones adicional que necesites.
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        //
        public List<int> ObtenerModulosActualesDelUsuario(int usuarioId)
        {

            try
            {
                List<int> modulosActuales = new List<int>();
                
                conexion.Conectar();

                string query = @"SELECT id_modulo FROM Usuario_Modulo WHERE id_usuario = @UsuarioId";

                conexion.CrearComando(query);
                conexion.AgregarParametro("@UsuarioId", usuarioId);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(query))
                {
                    while (reader.Read())
                    {
                        int moduloId = Convert.ToInt32(reader["id_modulo"]);
                        modulosActuales.Add(moduloId);
                    }
                }
                return modulosActuales;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al Obtener los módulos del usuario: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Desconectar();
            }
        }


    }
}
