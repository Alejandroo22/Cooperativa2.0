using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;

namespace sistema_modular_cafe_majada.model.DAO
{
    class FincaDAO
    {
        private ConnectionDB conexion;

        public FincaDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarFinca(Finca finca)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO finca (id_finca, nombre_finca,ubicacion_finca)
                                    VALUES (@idFinca,@nomFinca,@ubiFinca)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@idFinca", finca.IdFinca);
                conexion.AgregarParametro("@nomFinca", finca.nombreFinca);
                conexion.AgregarParametro("@ubiFinca", finca.ubicacionFinca);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0; 

            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción de la Finca en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar los registros
        public List<Finca> ObtenerFincas()
        {
            List<Finca> listaFincas = new List<Finca>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM finca";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Finca fincas = new Finca()
                        {
                            IdFinca = Convert.ToInt32(reader["id_finca"]),
                            nombreFinca = Convert.ToString(reader["nombre_finca"]),
                            ubicacionFinca = Convert.ToString(reader["ubicacion_finca"])
                        };

                        listaFincas.Add(fincas);
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
            return listaFincas;
        }

        //funcion para mostrar los registros
        public List<Finca> BuscarFincas(string buscar)
        {
            List<Finca> listaFincas = new List<Finca>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM finca WHERE nombre_finca LIKE CONCAT('%', @search, '%') ";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Finca fincas = new Finca()
                        {
                            IdFinca = Convert.ToInt32(reader["id_finca"]),
                            nombreFinca = Convert.ToString(reader["nombre_finca"]),
                            ubicacionFinca = Convert.ToString(reader["ubicacion_finca"])
                        };

                        listaFincas.Add(fincas);
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
            return listaFincas;
        }

        //
        public Finca CountFinca()
        {
            Finca finca = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalFinca FROM Finca";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            finca = new Finca()
                            {
                                CountFinca = Convert.ToInt32(reader["TotalFinca"])
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
            return finca;
        }

        public Finca ObtenerUltimoId()
        {
            Finca fm = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_finca) AS LastId FROM Finca";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        fm = new Finca()
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
            return fm;
        }

        //
        public Finca ObtenerNombreFinca(string nombre)
        {
            Finca finca = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Finca WHERE nombre_finca = @nombreF";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreF", nombre);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            finca = new Finca()
                            {
                                IdFinca = Convert.ToInt32(reader["id_finca"]),
                                nombreFinca = Convert.ToString(reader["nombre_finca"]),
                                ubicacionFinca = Convert.ToString(reader["ubicacion_finca"])
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
            return finca;
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarFinca(int idf,string nomFinca, string ubiFinca)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE finca SET nombre_finca=@nFinca,ubicacion_finca=@ufinca
                                    WHERE id_finca=@id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idf);
                conexion.AgregarParametro("@nFinca", nomFinca);
                conexion.AgregarParametro("@uFinca", ubiFinca);

                int filasAfectadas = conexion.EjecutarInstruccion();

                if(filasAfectadas>0)
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
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al actualizar los datos: "+ex.Message);
            }
            finally
            {
                //se cierra la conexion con la base de datos{
                conexion.Desconectar();
            }

            return exito;
        }

        //funcion para eliminar un registro de la base de datos
        public void EliminarFinca (int id)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM finca WHERE id_finca=@idFinca";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@idFinca", id);

                int filasAfectadas = conexion.EjecutarInstruccion();

                if(filasAfectadas>0)
                {
                    Console.WriteLine("El registro de elimino correctamente");
                }
                else
                {
                    Console.WriteLine("No se encontró el registro a eliminar");
                }
            }
            catch(Exception ex)
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
        public bool ExisteId(int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM Finca WHERE id_finca = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de la Finca: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

    }
}
