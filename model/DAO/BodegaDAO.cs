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
    class BodegaDAO
    {

        private ConnectionDB conexion;

        public BodegaDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarBodega(Bodega bodega)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Bodega_Cafe ( id_bodega, nombre_bodega, descripcion_bodega,  ubicacion_bodega ,id_benficio_ubicacion_bodega)
                                    VALUES ( @id, @nombre, @descrip, @ubicacion, @iBeneficio)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", bodega.IdBodega);
                conexion.AgregarParametro("@nombre", bodega.NombreBodega);
                conexion.AgregarParametro("@descrip", bodega.DescripcionBodega);
                conexion.AgregarParametro("@ubicacion", bodega.UbicacionBodega);
                conexion.AgregarParametro("@iBeneficio", bodega.IdBenficioUbicacion);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción de la bodega en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<Bodega> ObtenerBodegas()
        {
            List<Bodega> listaBodega = new List<Bodega>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Bodega_Cafe";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Bodega bodegas = new Bodega()
                        {
                            IdBodega = Convert.ToInt32(reader["id_bodega"]),
                            NombreBodega = Convert.ToString(reader["nombre_bodega"]),
                            DescripcionBodega = (reader["descripcion_bodega"])is DBNull ? "" : Convert.ToString(reader["descripcion_bodega"]),
                            UbicacionBodega = Convert.ToString(reader["ubicacion_bodega"]),
                            IdBenficioUbicacion = Convert.ToInt32(reader["id_benficio_ubicacion_bodega"])
                        };

                        listaBodega.Add(bodegas);
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
            return listaBodega;
        }

        //obtener la Bodega en especifico mediante el id en la BD
        public Bodega ObtenerIdBodega(int idBodega)
        {
            Bodega bodega = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Bodega_Cafe WHERE id_bodega = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idBodega);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        bodega = new Bodega()
                        {
                            IdBodega = Convert.ToInt32(reader["id_bodega"]),
                            NombreBodega = Convert.ToString(reader["nombre_bodega"]),
                            DescripcionBodega = (reader["descripcion_bodega"]) is DBNull ? "" : Convert.ToString(reader["descripcion_bodega"]),
                            UbicacionBodega = Convert.ToString(reader["ubicacion_bodega"]),
                            IdBenficioUbicacion = Convert.ToInt32(reader["id_benficio_ubicacion_bodega"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Bodega: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return bodega;
        }


        //
        public Bodega CountBodega()
        {
            Bodega bodega = null;

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalBodega FROM Bodega_Cafe";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        bodega = new Bodega()
                        {
                            CountBodega = Convert.ToInt32(reader["TotalBodega"])
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
            return bodega;
        }


        //obtener la Bodega en especifico mediante el id del beneficio en la BD
        public Bodega ObtenerBodegaNombre(string nombreBodega)
        {
            Bodega bodega = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT b.id_bodega, b.nombre_bodega, b.descripcion_bodega, b.ubicacion_bodega, b.id_benficio_ubicacion_bodega, be.nombre_beneficio
                                        FROM Bodega_Cafe b
                                        INNER JOIN Beneficio be ON b.id_benficio_ubicacion_bodega = be.id_beneficio
                                        WHERE b.nombre_bodega = @nombreB";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreB", nombreBodega);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        bodega = new Bodega()
                        {
                            IdBodega = Convert.ToInt32(reader["id_bodega"]),
                            NombreBodega = Convert.ToString(reader["nombre_bodega"]),
                            DescripcionBodega = (reader["descripcion_bodega"]) is DBNull ? "" : Convert.ToString(reader["descripcion_bodega"]),
                            UbicacionBodega = Convert.ToString(reader["ubicacion_bodega"]),
                            IdBenficioUbicacion = Convert.ToInt32(reader["id_benficio_ubicacion_bodega"]),
                            NombreBenficioUbicacion = Convert.ToString(reader["nombre_beneficio"]),
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Bodega: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return bodega;
        }

        //obtener la Bodega y el nombre del beneficio en la BD
        public List<Bodega> ObtenerBodegaNombreBeneficio()
        {
            List<Bodega> listaBodega = new List<Bodega>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT b.id_bodega, b.nombre_bodega, b.descripcion_bodega, b.ubicacion_bodega, b.id_benficio_ubicacion_bodega, be.nombre_beneficio
                                        FROM Bodega_Cafe b
                                        INNER JOIN Beneficio be ON b.id_benficio_ubicacion_bodega = be.id_beneficio";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Bodega bodega = new Bodega()
                        {
                            IdBodega = Convert.ToInt32(reader["id_bodega"]),
                            NombreBodega = Convert.ToString(reader["nombre_bodega"]),
                            DescripcionBodega = (reader["descripcion_bodega"]) is DBNull ? "" : Convert.ToString(reader["descripcion_bodega"]),
                            UbicacionBodega = Convert.ToString(reader["ubicacion_bodega"]),
                            IdBenficioUbicacion = Convert.ToInt32(reader["id_benficio_ubicacion_bodega"]),
                            NombreBenficioUbicacion = Convert.ToString(reader["nombre_beneficio"]),
                        };
                        listaBodega.Add(bodega);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Bodega: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaBodega;
        }

        //
        public List<Bodega> BuscarBodega(string buscar)
        {
            List<Bodega> bodegas = new List<Bodega>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT b.id_bodega, b.nombre_bodega, b.descripcion_bodega, b.ubicacion_bodega, b.id_benficio_ubicacion_bodega, be.nombre_beneficio
                                        FROM Bodega_Cafe b
                                        INNER JOIN Beneficio be ON b.id_benficio_ubicacion_bodega = be.id_beneficio
                                        WHERE b.nombre_bodega LIKE CONCAT('%', @search, '%') OR be.nombre_beneficio LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Bodega bodega = new Bodega()
                        {
                            IdBodega = Convert.ToInt32(reader["id_bodega"]),
                            NombreBodega = Convert.ToString(reader["nombre_bodega"]),
                            DescripcionBodega = (reader["descripcion_bodega"]) is DBNull ? "" : Convert.ToString(reader["descripcion_bodega"]),
                            UbicacionBodega = Convert.ToString(reader["ubicacion_bodega"]),
                            IdBenficioUbicacion = Convert.ToInt32(reader["id_benficio_ubicacion_bodega"]),
                            NombreBenficioUbicacion = Convert.ToString(reader["nombre_beneficio"])
                        };

                        bodegas.Add(bodega);
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
            return bodegas;
        }

        //
        public bool ExisteBodega(string nombreBodega, int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM Bodega_Cafe WHERE nombre_bodega = @nombreBodega AND id_benficio_ubicacion_bodega = @benef";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreBodega", nombreBodega);
                conexion.AgregarParametro("@benef", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de la bodega: " + ex.Message);
                return false; 
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarBodega(int idBodega, string nombre, string descripcion, string ubicacion, int idBeneficio)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Bodega_Cafe SET nombre_bodega = @nombre, descripcion_bodega = @descrip, 
                                                        ubicacion_bodega = @ubicacion, id_benficio_ubicacion_bodega = @iBeneficio
                                    WHERE id_Bodega = @id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombre", nombre);
                conexion.AgregarParametro("@descrip", descripcion);
                conexion.AgregarParametro("@ubicacion", ubicacion);
                conexion.AgregarParametro("@iBeneficio", idBeneficio);
                conexion.AgregarParametro("@id", idBodega);

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
        public void EliminarBodega(int idBodega)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Bodega_Cafe WHERE id_Bodega = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idBodega);

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

        public Bodega ObtenerUltimoId()
        {
            Bodega alm = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_bodega) AS LastId FROM Bodega_Cafe";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        alm = new Bodega()
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
            return alm;
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM Bodega_Cafe WHERE id_bodega = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de la Bodega: " + ex.Message);
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
