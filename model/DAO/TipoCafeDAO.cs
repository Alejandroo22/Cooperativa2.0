using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class TipoCafeDAO
    {
        private ConnectionDB conexion;

        public TipoCafeDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarTipoCafe(TipoCafe tipoCafe)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Tipo_Cafe (id_tipo_cafe, nombre_tipo_cafe, descripcion_tipo_cafe)
                                    VALUES ( @id, @nombre, @descripcion)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", tipoCafe.IdTipoCafe);
                conexion.AgregarParametro("@nombre", tipoCafe.NombreTipoCafe);
                conexion.AgregarParametro("@descripcion", tipoCafe.DescripcionTipoCafe);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción del TipoCafe en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<TipoCafe> ObtenerTipoCafes()
        {
            List<TipoCafe> listaTipoCafe = new List<TipoCafe>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Tipo_Cafe";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        TipoCafe TipoCafes = new TipoCafe()
                        {
                            IdTipoCafe = Convert.ToInt32(reader["id_tipo_cafe"]),
                            NombreTipoCafe = Convert.ToString(reader["nombre_tipo_cafe"]),
                            DescripcionTipoCafe = (reader["descripcion_tipo_cafe"]) is DBNull ? "" : Convert.ToString(reader["descripcion_tipo_cafe"])
                        };

                        listaTipoCafe.Add(TipoCafes);
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
            return listaTipoCafe;
        }

        //funcion para mostrar todos los registros
        public List<TipoCafe> BuscadorTipoCafes(string buscar)
        {
            List<TipoCafe> listaTipoCafe = new List<TipoCafe>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Tipo_Cafe WHERE nombre_tipo_cafe LIKE CONCAT('%', @search, '%') ";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        TipoCafe TipoCafes = new TipoCafe()
                        {
                            IdTipoCafe = Convert.ToInt32(reader["id_tipo_cafe"]),
                            NombreTipoCafe = Convert.ToString(reader["nombre_tipo_cafe"]),
                            DescripcionTipoCafe = (reader["descripcion_tipo_cafe"]) is DBNull ? "" : Convert.ToString(reader["descripcion_tipo_cafe"])
                        };

                        listaTipoCafe.Add(TipoCafes);
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
            return listaTipoCafe;
        }

        //obtener el total de TipoCafe de la BD
        public TipoCafe CountTipoCafe()
        {
            TipoCafe tipoCafe = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT COUNT(*) AS TotalTipoCafe FROM Tipo_Cafe";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        tipoCafe = new TipoCafe()
                        {
                            CountTipoCafe = Convert.ToInt32(reader["TotalTipoCafe"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el total TipoCafe: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return tipoCafe;
        }

        public TipoCafe ObtenerUltimoId()
        {
            TipoCafe tpc = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_tipo_cafe) AS LastId FROM Tipo_Cafe";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        tpc = new TipoCafe()
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
            return tpc;
        }
        //obtener el TipoCafe en especifico mediante el id en la BD
        public TipoCafe ObtenerIdTipoCafe(int idTip)
        {
            TipoCafe tipoCafe = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Tipo_Cafe WHERE id_tipo_cafe = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idTip);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        tipoCafe = new TipoCafe()
                        {
                            IdTipoCafe = Convert.ToInt32(reader["id_tipo_cafe"]),
                            NombreTipoCafe = Convert.ToString(reader["nombre_tipo_cafe"]),
                            DescripcionTipoCafe = (reader["descripcion_tipo_cafe"]) is DBNull ? "" : Convert.ToString(reader["descripcion_tipo_cafe"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el TipoCafe: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return tipoCafe;
        }

        //obtener el TipoCafe en especifico mediante el id en la BD
        public TipoCafe ObtenerTipoCafeNombre(string nombre)
        {
            TipoCafe tipoCafe = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Tipo_Cafe WHERE nombre_tipo_cafe = @nombreTip";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreTip", nombre);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        tipoCafe = new TipoCafe()
                        {
                            IdTipoCafe = Convert.ToInt32(reader["id_tipo_cafe"]),
                            NombreTipoCafe = Convert.ToString(reader["nombre_tipo_cafe"]),
                            DescripcionTipoCafe = (reader["descripcion_tipo_cafe"]) is DBNull ? "" : Convert.ToString(reader["descripcion_tipo_cafe"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el TipoCafe: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return tipoCafe;
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarTipoCafe(int idTipo, string nombreTipo, string descripcionTipo)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Tipo_Cafe SET nombre_tipo_cafe = @nombre, descripcion_tipo_cafe = @descripcion
                                    WHERE id_tipo_cafe = @id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombre", nombreTipo);
                conexion.AgregarParametro("@descripcion", descripcionTipo);
                conexion.AgregarParametro("@id", idTipo);

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
        public void EliminarTipoCafe(int idTip)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Tipo_Cafe WHERE id_tipo_cafe = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idTip);

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
        public bool ExisteId(int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM Tipo_Cafe WHERE id_tipo_cafe = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia del Tipo Cafe: " + ex.Message);
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
