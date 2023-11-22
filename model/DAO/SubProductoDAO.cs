using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class SubProductoDAO
    {
        private ConnectionDB conexion;

        public SubProductoDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //
        public bool InsertarSubProducto(SubProducto subproducto)
        {
            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea script SQL para insertar
                string consulta = @"INSERT INTO SubProducto (nombre_subproducto, descripcion, id_calidad_supproducto)
                            VALUES (@nombreSubProducto, @descripcion, @idCalidadCafe)";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombreSubProducto", subproducto.NombreSubProducto);
                conexion.AgregarParametro("@descripcion", subproducto.DescripcionSubProducto);
                conexion.AgregarParametro("@idCalidadCafe", subproducto.IdCalidadCafe);

                int filasAfectadas = conexion.EjecutarInstruccion();

                // Si la fila se afecta, se insertó correctamente
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        //
        public List<SubProducto> ObtenerSubProductos()
        {
            List<SubProducto> listaSubProductos = new List<SubProducto>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM SubProducto";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        SubProducto subproducto = new SubProducto()
                        {
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            DescripcionSubProducto = (reader["descripcion"]) is DBNull ? "" : Convert.ToString(reader["descripcion"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_supproducto"])
                        };

                        listaSubProductos.Add(subproducto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
            return listaSubProductos;
        }

        //obtener el SubProducto en especifico mediante el id en la BD
        public SubProducto ObtenerSubProductoPorId(int idSubProducto)
        {
            SubProducto subproducto = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el subproducto
                string consulta = "SELECT * FROM SubProducto WHERE id_subproducto = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idSubProducto);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        subproducto = new SubProducto()
                        {
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            DescripcionSubProducto = (reader["descripcion"]) is DBNull ? "" : Convert.ToString(reader["descripcion"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_supproducto"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el SubProducto: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return subproducto;
        }
        
        //obtener el SubProducto en especifico mediante el id en la BD
        public List<SubProducto> ObtenerSubProductoPorIdCalidad(int idCalidad)
        {
            List<SubProducto> listaSubProducto = new List<SubProducto>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el subproducto
                string consulta = "SELECT * FROM SubProducto WHERE id_calidad_supproducto = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idCalidad);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        SubProducto subproducto = new SubProducto()
                        {
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            DescripcionSubProducto = (reader["descripcion"]) is DBNull ? "" : Convert.ToString(reader["descripcion"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_supproducto"])
                        };
                        listaSubProducto.Add(subproducto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el SubProducto: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaSubProducto;
        }

        //obtener el subproducto en especifico mediante el nombre en la BD
        public SubProducto ObtenerSubProductoPorNombre(string nombreSubProducto)
        {
            SubProducto subProducto = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el subproducto por nombre
                string consulta = @"SELECT sp.id_subproducto, sp.nombre_subproducto, sp.descripcion, sp.id_calidad_supproducto, cc.nombre_calidad
                    FROM SubProducto sp
                    INNER JOIN Calidad_Cafe cc ON sp.id_calidad_supproducto = cc.id_calidad
                    WHERE sp.nombre_subproducto = @nombreSubProducto";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreSubProducto", nombreSubProducto);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        subProducto = new SubProducto()
                        {
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            DescripcionSubProducto = (reader["descripcion"]) is DBNull ? "" : Convert.ToString(reader["descripcion"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_supproducto"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el subproducto: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return subProducto;
        }

        //
        public SubProducto CountSubProducto()
        {
            SubProducto subP = null;

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalSubProducto FROM SubProducto";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        subP = new SubProducto()
                        {
                            CountSubProducto = Convert.ToInt32(reader["TotalSubProducto"])
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
            return subP;
        }

        //obtener la Cantidad y el nombre almacen en la BD
        public List<SubProducto> ObtenerNombreSubProductos()
        {
            List<SubProducto> listaSubProducto = new List<SubProducto>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener la cantidad y el nombre del almacén
                string consulta = @"SELECT sp.id_subproducto, sp.nombre_subproducto, sp.descripcion, sp.id_calidad_supproducto, cc.nombre_calidad
                                    FROM SubProducto sp
                                    INNER JOIN Calidad_Cafe cc ON sp.id_calidad_supproducto = cc.id_calidad";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        SubProducto sub = new SubProducto()
                        {
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            DescripcionSubProducto = (reader["descripcion"]) is DBNull ? "" : Convert.ToString(reader["descripcion"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_supproducto"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"])
                        };
                        listaSubProducto.Add(sub);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el SubProducto: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaSubProducto;
        }

        public SubProducto ObtenerUltimoId()
        {
            SubProducto sp = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_subproducto) AS LastId FROM SubProducto";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        sp = new SubProducto()
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
            return sp;
        }
        //
        public List<SubProducto> BuscarSubProducto(string buscar)
        {
            List<SubProducto> listaSubProducto = new List<SubProducto>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT sp.id_subproducto, sp.nombre_subproducto, sp.descripcion, sp.id_calidad_supproducto, cc.nombre_calidad_cafe
                                FROM SubProducto sp
                                INNER JOIN Calidad_Cafe cc ON sp.id_calidad_supproducto = cc.id_calidad
                                WHERE cc.nombre_calidad_cafe LIKE CONCAT('%', @search, '%') OR sp.nombre_subproducto LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        SubProducto sub = new SubProducto()
                        {
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            DescripcionSubProducto = (reader["descripcion"]) is DBNull ? "" : Convert.ToString(reader["descripcion"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_supproducto"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad_cafe"])
                        };

                        listaSubProducto.Add(sub);
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
            return listaSubProducto;
        }

        //
        public bool ActualizarSubProducto(SubProducto subProducto)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL 
                string consulta = @"UPDATE SubProducto 
                    SET nombre_subproducto = @nombreSubProducto, descripcion = @descripcionSubProducto, id_calidad_supproducto = @idCalidadCafe
                    WHERE id_subproducto = @idSubProducto";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombreSubProducto", subProducto.NombreSubProducto);
                conexion.AgregarParametro("@descripcionSubProducto", subProducto.DescripcionSubProducto);
                conexion.AgregarParametro("@idCalidadCafe", subProducto.IdCalidadCafe);
                conexion.AgregarParametro("@idSubProducto", subProducto.IdSubProducto);

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
                Console.WriteLine("Ocurrió un error al actualizar los datos: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión con la base de datos
                conexion.Desconectar();
            }

            return exito;
        }

        //funcion para eliminar un registro de la base de datos
        public void EliminarSubProducto(int idSubProducto)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM SubProducto WHERE id_subproducto = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idSubProducto);

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
        public bool ExisteSubProducto(string nombre, int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM SubProducto WHERE nombre_subproducto = @nombre AND id_calidad_supproducto = @idc";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombre", nombre);
                conexion.AgregarParametro("@idc", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia del SubProducto: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
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
                string consulta = "SELECT COUNT(*) FROM SubProducto WHERE id_subproducto = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia del SubProducto: " + ex.Message);
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
