using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class BeneficioDAO
    {
        private ConnectionDB conexion;

        public BeneficioDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarBeneficio(Beneficio beneficio)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Beneficio ( id_beneficio, nombre_beneficio, ubicacion_beneficio)
                                    VALUES ( @id, @nombre, @ubicacion)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", beneficio.IdBeneficio);
                conexion.AgregarParametro("@nombre", beneficio.NombreBeneficio);
                conexion.AgregarParametro("@ubicacion", beneficio.UbicacionBeneficio);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción del Beneficio en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<Beneficio> ObtenerBeneficios()
        {
            List<Beneficio> listaBeneficio = new List<Beneficio>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Beneficio";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Beneficio beneficios = new Beneficio()
                        {
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio"]),
                            NombreBeneficio = Convert.ToString(reader["nombre_beneficio"]),
                            UbicacionBeneficio = Convert.ToString(reader["ubicacion_beneficio"])
                        };

                        listaBeneficio.Add(beneficios);
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
            return listaBeneficio;
        }

        //obtener el beneficio en especifico mediante el id en la BD
        public Beneficio ObtenerIdBeneficio(int idBen)
        {
            Beneficio beneficio = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Beneficio WHERE id_beneficio = @Id";
                
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idBen);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        beneficio = new Beneficio()
                        {
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio"]),
                            NombreBeneficio = Convert.ToString(reader["nombre_beneficio"]),
                            UbicacionBeneficio = Convert.ToString(reader["ubicacion_beneficio"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Beneficio: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return beneficio;
        }

        //obtener el total de beneficios en la BD
        public Beneficio CountBeneficio()
        {
            Beneficio beneficio = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT COUNT(*) AS TotalBeneficio FROM Beneficio";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        beneficio = new Beneficio()
                        {
                            CountBeneficio = Convert.ToInt32(reader["TotalBeneficio"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Beneficio: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return beneficio;
        }

        //obtener el beneficio en especifico mediante el id en la BD
        public Beneficio ObtenerBeneficioNombre(string nombre)
        {
            Beneficio beneficio = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Beneficio WHERE nombre_beneficio = @nombreBen";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreBen", nombre);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        beneficio = new Beneficio()
                        {
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio"]),
                            NombreBeneficio = Convert.ToString(reader["nombre_beneficio"]),
                            UbicacionBeneficio = Convert.ToString(reader["ubicacion_beneficio"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Beneficio: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return beneficio;
        }

        //
        public List<Beneficio> BuscarBeneficio(string buscar)
        {
            List<Beneficio> beneficios = new List<Beneficio>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Beneficio WHERE nombre_beneficio LIKE CONCAT('%', @search, '%') ";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Beneficio benef = new Beneficio()
                        {
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio"]),
                            NombreBeneficio = Convert.ToString(reader["nombre_beneficio"]),
                            UbicacionBeneficio = Convert.ToString(reader["ubicacion_beneficio"])
                        };

                        beneficios.Add(benef);
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
            return beneficios;
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarBeneficio(int idBen, string nombreBen, string ubicacionBen)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Beneficio SET nombre_beneficio = @nombre, ubicacion_beneficio = @ubicacion
                                    WHERE id_beneficio = @id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombre", nombreBen);
                conexion.AgregarParametro("@ubicacion", ubicacionBen);
                conexion.AgregarParametro("@id", idBen);

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
        public void EliminarBeneficio(int idBen)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Beneficio WHERE id_beneficio = @id";
                
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idBen);

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

        public Beneficio ObtenerUltimoId()
        {
            Beneficio bn = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_beneficio) AS LastId FROM Beneficio";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        bn = new Beneficio()
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
            return bn;
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM Beneficio WHERE id_beneficio = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia del Beneficio: " + ex.Message);
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
