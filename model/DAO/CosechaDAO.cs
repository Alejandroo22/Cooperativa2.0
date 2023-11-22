using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class CosechaDAO
    {
        private ConnectionDB conexion;

        public CosechaDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarCosecha(Cosecha cosecha)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Cosecha (id_cosecha, nombre_cosecha, fecha_cosecha)
                                    VALUES (@id, @nombre, @fecha)";
                conexion.CrearComando(consulta);

                DateTime fecha = DateTime.Today;

                conexion.AgregarParametro("@id", cosecha.IdCosecha);
                conexion.AgregarParametro("@nombre", cosecha.NombreCosecha);
                conexion.AgregarParametro("@fecha", fecha);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción de la Cosecha en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<Cosecha> ObtenerCosecha()
        {
            List<Cosecha> listaCosecha = new List<Cosecha>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Cosecha";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Cosecha cosecha = new Cosecha()
                        {
                            IdCosecha = Convert.ToInt32(reader["id_cosecha"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            FechaCosecha = Convert.ToDateTime(reader["fecha_cosecha"])
                        };
                        
                        listaCosecha.Add(cosecha);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
            return listaCosecha;
        }

        //funcion para mostrar todos los registros
        public List<Cosecha> ObtenerCosechaDESC()
        {
            List<Cosecha> listaCosecha = new List<Cosecha>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Cosecha ORDER BY fecha_cosecha DESC";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Cosecha cosecha = new Cosecha()
                        {
                            IdCosecha = Convert.ToInt32(reader["id_cosecha"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            FechaCosecha = Convert.ToDateTime(reader["fecha_cosecha"])
                        };
                        
                        listaCosecha.Add(cosecha);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
            return listaCosecha;
        }
        
        //funcion para mostrar todos los registros
        public Cosecha ObtenerCosechaUltima()
        {
            Cosecha cosecha = null;

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Cosecha ORDER BY fecha_cosecha DESC LIMIT 1";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cosecha = new Cosecha()
                        {
                            IdCosecha = Convert.ToInt32(reader["id_cosecha"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            FechaCosecha = Convert.ToDateTime(reader["fecha_cosecha"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
            return cosecha;
        }

        //funcion para mostrar todos los registros
        public List<Cosecha> BuscarCosecha(string buscar)
        {
            List<Cosecha> listaCosecha = new List<Cosecha>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Cosecha WHERE nombre_cosecha LIKE CONCAT('%', @search, '%') ";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Cosecha cosecha = new Cosecha()
                        {
                            IdCosecha = Convert.ToInt32(reader["id_cosecha"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            FechaCosecha = Convert.ToDateTime(reader["fecha_cosecha"])
                        };
                        
                        listaCosecha.Add(cosecha);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
            return listaCosecha;
        }

        //obtener la cosecha en especifico mediante el id en la BD
        public Cosecha ObtenerIdCosecha(int id)
        {
            Cosecha cosecha = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Cosecha WHERE id_cosecha = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", id);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cosecha = new Cosecha()
                        {
                            IdCosecha = Convert.ToInt32(reader["id_cosecha"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            FechaCosecha = Convert.ToDateTime(reader["fecha_cosecha"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Cosecha: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return cosecha;
        }

        //obtener la cosecha en especifico mediante el nombre en la BD
        public Cosecha ObtenerNombreCosecha(string nombre)
        {
            Cosecha cosecha = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Cosecha WHERE nombre_cosecha = @Nombre";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Nombre", nombre);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cosecha = new Cosecha()
                        {
                            IdCosecha = Convert.ToInt32(reader["id_cosecha"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            FechaCosecha = Convert.ToDateTime(reader["fecha_cosecha"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Cosecha: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return cosecha;
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarCosecha(int id, string nombre)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Cosecha SET nombre_cosecha = @Nombre
                                    WHERE id_cosecha = @id";
                conexion.CrearComando(consulta);
                
                conexion.AgregarParametro("@Nombre", nombre);
                conexion.AgregarParametro("@id", id);

                int filasAfectadas = conexion.EjecutarInstruccion();

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("La actualización se realizó correctamente");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la actualización");
                    exito = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al actualizar los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion con la base de datos{
                conexion.Desconectar();
            }

            return exito;
        }

        //funcion para eliminar un registro de la base de datos
        public void EliminarCosecha(int id)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Cosecha WHERE id_cosecha = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int filasAfectadas = conexion.EjecutarInstruccion();

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("El registro se elimino correctamente");
                }
                else
                {
                    Console.WriteLine("No se encontró el registro a eliminar");
                }
            }
            catch (Exception ex)
            {
                if (ex is MySqlException sqlException)
                {
                    SqlExceptionHelper.NumberException = sqlException.Number;
                    SqlExceptionHelper.MessageExceptionSql = SqlExceptionHelper.HandleSqlException(ex);
                }

                Console.WriteLine("Error al eliminar el registro" + ex.Message);
            }
            finally
            {
                //se cierra la conexion con la base de datos
                conexion.Desconectar();
            }
        }

        //
        public Cosecha CountCosecha()
        {
            Cosecha cosec = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalCosecha FROM Cosecha";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            cosec = new Cosecha()
                            {
                                CountCosecha = Convert.ToInt32(reader["TotalCosecha"])
                            };
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
            return cosec;
        }

        public Cosecha ObtenerUltimoId()
        {
            Cosecha cs = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_cosecha) AS LastId FROM Cosecha";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cs = new Cosecha()
                        {
                            LastId = (reader["LastId"]) is DBNull ? 0 : Convert.ToInt32(reader["LastId"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los datos: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
            return cs;
        }
    }
}
