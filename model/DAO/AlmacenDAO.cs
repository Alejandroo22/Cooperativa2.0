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
    class AlmacenDAO
    {
        private ConnectionDB conexion;

        public AlmacenDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarAlmacen(Almacen almacen)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Almacen ( id_almacen, nombre_almacen, descripcion_almacen, capacidad_almacen, cantidad_actual_almacen, cantidad_actual_saco_almacen,  
                                            ubicacion_almacen, id_bodega_ubicacion_almacen)
                                    VALUES ( @id, @nombre, @descrip, @capacidad, @capacidadAct, @capacidadSacoAct, @ubicacion, @iBodega)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", almacen.IdAlmacen);
                conexion.AgregarParametro("@nombre", almacen.NombreAlmacen);
                conexion.AgregarParametro("@descrip", almacen.DescripcionAlmacen);
                conexion.AgregarParametro("@capacidad", almacen.CapacidadAlmacen);
                conexion.AgregarParametro("@capacidadAct", 0.0);
                conexion.AgregarParametro("@capacidadSacoAct", 0.0);
                conexion.AgregarParametro("@ubicacion", almacen.UbicacionAlmacen);
                conexion.AgregarParametro("@iBodega", almacen.IdBodegaUbicacion);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción de la Almacen en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<Almacen> ObtenerAlmacenes()
        {
            List<Almacen> listaAlmacen = new List<Almacen>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Almacen";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Almacen Almacens = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"])is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"])
                        };

                        listaAlmacen.Add(Almacens);
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
            return listaAlmacen;
        }

        //obtener la Almacen en especifico mediante el id en la BD
        public Almacen ObtenerIdAlmacen(int idAlmacen)
        {
            Almacen Almacen = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.*, ccp.id_subproducto_cafe 
                                    FROM Almacen a 
                                    LEFT JOIN CantidadCafe_Silo_Piña ccp ON a.id_almacen = ccp.id_almacen_silo_piña 
                                    WHERE id_almacen = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idAlmacen);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        Almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"])is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            IdSubProducto = (reader["id_subproducto_cafe"]) is DBNull ? 0 : Convert.ToInt32(reader["id_subproducto_cafe"]),
                            IdCalidadCafe = (reader["id_calidad_cafe"]) is DBNull ? 0 : Convert.ToInt32(reader["id_calidad_cafe"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Almacen: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return Almacen;
        }

        //obtener la Almacen en especifico mediante el id de la maquinaria en la BD
        public Almacen ObtenerAlmacenNombre(string nombreAlmacen)
        {
            Almacen Almacen = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.id_almacen, a.nombre_almacen, a.descripcion_almacen, a.capacidad_almacen, a.cantidad_actual_saco_almacen, a.ubicacion_almacen, a.id_bodega_ubicacion_almacen, b.nombre_bodega
                                        FROM Almacen a
                                        INNER JOIN Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                                        WHERE b.nombre_bodega = @nombreM";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreM", nombreAlmacen);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        Almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"]) is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            NombreBodegaUbicacion = Convert.ToString(reader["nombre_bodega"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Almacen: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return Almacen;
        }

        //obtener la Almacen y el nombre maquinaria en la BD
        public List<Almacen> ObtenerAlmacenNombreCalidadBodega()
        {
            List<Almacen> listaAlmacen = new List<Almacen>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.id_almacen, a.nombre_almacen, a.descripcion_almacen, a.capacidad_almacen, a.ubicacion_almacen, a.id_bodega_ubicacion_almacen, 
                                            b.nombre_bodega, a.id_calidad_cafe, c.nombre_calidad, a.cantidad_actual_almacen, a.cantidad_actual_saco_almacen
                                        FROM Almacen a
                                        LEFT JOIN Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                                        LEFT JOIN Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Almacen almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"]) is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            NombreBodegaUbicacion = Convert.ToString(reader["nombre_bodega"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            IdCalidadCafe = reader["id_calidad_cafe"] is DBNull ? (int?)null : Convert.ToInt32(reader["id_calidad_cafe"]),
                            NombreCalidadCafe = reader["nombre_calidad"] is DBNull ? null : Convert.ToString(reader["nombre_calidad"])
                        };
                        listaAlmacen.Add(almacen);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Almacen: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaAlmacen;
        }
        
        //obtener la Almacen y el nombre maquinaria en la BD
        public List<Almacen> ObtenerAlmacenNombreBodega()
        {
            List<Almacen> listaAlmacen = new List<Almacen>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.id_almacen, a.nombre_almacen, a.descripcion_almacen, a.capacidad_almacen, a.ubicacion_almacen, a.id_bodega_ubicacion_almacen, b.nombre_bodega,
                                        a.cantidad_actual_saco_almacen, a.cantidad_actual_almacen, cc.nombre_calidad
                                        FROM Almacen a
                                        LEFT JOIN Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                                        LEFT JOIN Calidad_Cafe cc ON a.id_calidad_cafe = cc.id_calidad";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Almacen almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"]) is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            NombreBodegaUbicacion = Convert.ToString(reader["nombre_bodega"])
                        };
                        listaAlmacen.Add(almacen);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Almacen: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaAlmacen;
        }

        //
        public List<Almacen> BuscarAlmacen(string buscar)
        {
            List<Almacen> almacens = new List<Almacen>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.id_almacen, a.nombre_almacen, a.descripcion_almacen, a.capacidad_almacen, a.ubicacion_almacen, a.id_bodega_ubicacion_almacen, 
                                            b.nombre_bodega, a.id_calidad_cafe, c.nombre_calidad, a.cantidad_actual_almacen, a.cantidad_actual_saco_almacen
                                        FROM Almacen a
                                        LEFT JOIN Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                                        LEFT JOIN Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad
                                        WHERE b.nombre_bodega LIKE CONCAT('%', @search, '%') OR a.nombre_almacen LIKE CONCAT('%', @search, '%') 
                                                OR a.id_bodega_ubicacion_almacen LIKE CONCAT('%', @search, '%') OR c.nombre_calidad LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Almacen almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"]) is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            NombreBodegaUbicacion = Convert.ToString(reader["nombre_bodega"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            IdCalidadCafe = reader["id_calidad_cafe"] is DBNull ? (int?)null : Convert.ToInt32(reader["id_calidad_cafe"]),
                            NombreCalidadCafe = reader["nombre_calidad"] is DBNull ? null : Convert.ToString(reader["nombre_calidad"])
                        };

                        almacens.Add(almacen);
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
            return almacens;
        }
        
        //
        public List<Almacen> BuscarIDBodegaAlmacen(int buscar)
        {
            List<Almacen> almacens = new List<Almacen>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.id_almacen, a.nombre_almacen, a.descripcion_almacen, a.capacidad_almacen, a.ubicacion_almacen, a.id_bodega_ubicacion_almacen, 
                                            b.nombre_bodega, a.id_calidad_cafe, c.nombre_calidad, a.cantidad_actual_almacen, a.cantidad_actual_saco_almacen
                                        FROM Almacen a
                                        LEFT JOIN Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                                        LEFT JOIN Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad
                                        WHERE a.id_bodega_ubicacion_almacen LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Almacen almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"]) is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            NombreBodegaUbicacion = Convert.ToString(reader["nombre_bodega"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            IdCalidadCafe = reader["id_calidad_cafe"] is DBNull ? (int?)null : Convert.ToInt32(reader["id_calidad_cafe"]),
                            NombreCalidadCafe = reader["nombre_calidad"] is DBNull ? null : Convert.ToString(reader["nombre_calidad"])
                        };

                        almacens.Add(almacen);
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
            return almacens;
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarAlmacen(int idAlmacen, string nombre, string descripcion, double capacidad, string ubicacion, int idBodega)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Almacen SET nombre_almacen = @nombre, descripcion_almacen = @descrip, capacidad_almacen = @capacidad, 
                                                        ubicacion_almacen = @ubicacion, id_bodega_ubicacion_almacen = @iBodega
                                    WHERE id_almacen = @id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombre", nombre);
                conexion.AgregarParametro("@descrip", descripcion);
                conexion.AgregarParametro("@capacidad", capacidad);
                conexion.AgregarParametro("@ubicacion", ubicacion);
                conexion.AgregarParametro("@iBodega", idBodega);
                conexion.AgregarParametro("@id", idAlmacen);

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
        public void EliminarAlmacen(int idAlmacen)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Almacen WHERE id_almacen = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idAlmacen);

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
        public Almacen ObtenerCantidadCafeAlmacen(int iAlmacen)
        {
            Almacen almacen = null;
            try
            {
                conexion.Conectar();

                string consulta = @"SELECT * FROM Almacen WHERE id_almacen = @id";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", iAlmacen);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        almacen = new Almacen()
                        {
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"]))
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener las cantidades: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return almacen;
        }
        
        //
        public Almacen ObtenerAlmacenNombreCalidad(int iAlmacen)
        {
            Almacen almacen = null;
            try
            {
                conexion.Conectar();

                string consulta = @"SELECT a.*, cc.nombre_calidad "  //, sp.nombre_subproducto
                                    +@"FROM Almacen a 
                                    LEFT JOIN Calidad_Cafe cc ON a.id_calidad_cafe = cc.id_calidad "
                                    //LEFT JOIN SubProducto sp ON a.id_subproducto_cafe = sp.id_subproducto
                                    +@"WHERE id_almacen = @id";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", iAlmacen);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            IdCalidadCafe = (reader["id_calidad_cafe"]) is DBNull ? 0 : Convert.ToInt32(reader["id_calidad_cafe"]),
                            NombreCalidadCafe = (reader["nombre_calidad"]) is DBNull ? "" : Convert.ToString(reader["nombre_calidad"]),
                            //IdSubProducto = (reader["id_subproducto_cafe"]) is DBNull ? 0 : Convert.ToInt32(reader["id_subproducto_cafe"]),
                            //NombreSubProducto = (reader["nombre_subproducto"]) is DBNull ? "" : Convert.ToString(reader["nombre_subproducto"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"]))
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener las cantidades: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return almacen;
        }

        //funcion para actualizar el registro de catidades en la base de datos
        public bool ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(int idAlmacen, double cantidadNueva, double cantidadNuevaSaco, int idCalidad)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Almacen SET cantidad_actual_almacen = @cantidadNu, cantidad_actual_saco_almacen = @cantidadNuSaco, id_calidad_cafe = @iCalidad
                                    WHERE id_almacen = @id";
                //id_subproducto_cafe = @iSubProd
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@cantidadNu", cantidadNueva);
                conexion.AgregarParametro("@cantidadNuSaco", cantidadNuevaSaco);
                conexion.AgregarParametro("@iCalidad", idCalidad);
                conexion.AgregarParametro("@id", idAlmacen);

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
        
        //funcion para actualizar el registro de catidades en la base de datos
        public bool ActualizarCalidadAlmacen(int idCalidad, int idAlmacen)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Almacen SET id_calidad_cafe = @iCalidad
                                    WHERE id_almacen = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@iCalidad", idCalidad);
                conexion.AgregarParametro("@id", idAlmacen);

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
        
        //funcion para actualizar el registro de catidades en la base de datos
        public bool ActualizarCantidadEntradaCafeAlmacen(int idAlmacen, double cantidad, double cantidadSaco, int idCalidad)
        {
            bool exito = false;
            Console.WriteLine("Depuracion - cantidad obtenida a actualizar " + cantidad);
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Almacen SET cantidad_actual_almacen = @cantidad, cantidad_actual_saco_almacen = @cantidadSaco, id_calidad_cafe = @iCalidad
                                    WHERE id_almacen = @id";
                //, id_subproducto_cafe = @iSubProd
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@cantidad", cantidad);
                conexion.AgregarParametro("@cantidadSaco", cantidadSaco);
                conexion.AgregarParametro("@iCalidad", idCalidad);
                //conexion.AgregarParametro("@iSubProd", iSubProd);
                conexion.AgregarParametro("@id", idAlmacen);

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

        //obtener la Almacen y el nombre maquinaria en la BD
        public List<Almacen> ObtenerPorIDAlmacenNombreCalidadBodega(int id)
        {
            List<Almacen> listaAlmacen = new List<Almacen>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.*, b.nombre_bodega, c.nombre_calidad "
                                    + @"FROM Almacen a 
                                        LEFT JOIN Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega 
                                        LEFT JOIN Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad "
                                    + @"WHERE ((id_calidad_cafe = @id or id_calidad_cafe IS NULL) AND 
                                        (cantidad_actual_almacen IS NULL OR cantidad_actual_almacen < capacidad_almacen or 0.0))
                                        OR (id_calidad_cafe <> @id AND (cantidad_actual_almacen = 0.0 or cantidad_actual_almacen IS NULL))";


                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Almacen almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"]) is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            NombreBodegaUbicacion = Convert.ToString(reader["nombre_bodega"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            IdCalidadCafe = reader["id_calidad_cafe"] is DBNull ? (int?)null : Convert.ToInt32(reader["id_calidad_cafe"]),
                            NombreCalidadCafe = reader["nombre_calidad"] is DBNull ? null : Convert.ToString(reader["nombre_calidad"])
                        };
                        listaAlmacen.Add(almacen);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Almacen: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaAlmacen;
        }

        //
        public List<Almacen> BuscarIDBodegaAlmacenCalidad(int buscar, int id)
        {
            List<Almacen> almacens = new List<Almacen>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT a.*, b.nombre_bodega, c.nombre_calidad " // , sp.nombre_subproducto
                                        +@" FROM Almacen a 
                                        LEFT JOIN Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega 
                                        LEFT JOIN Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad "
                                        //LEFT JOIN SubProducto sp ON a.id_subproducto_cafe = sp.id_subproducto
                                        +@"WHERE a.id_bodega_ubicacion_almacen LIKE CONCAT('%', @search, '%')";
                /*AND ((id_calidad_cafe = @id or id_calidad_cafe IS NULL) AND (cantidad_actual_almacen IS NULL OR cantidad_actual_almacen < capacidad_almacen or 0.0))
                OR (id_calidad_cafe <> @id AND (cantidad_actual_almacen = 0.0 or cantidad_actual_almacen IS NULL))";*/

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");
                conexion.AgregarParametro("@id", id);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Almacen almacen = new Almacen()
                        {
                            IdAlmacen = Convert.ToInt32(reader["id_almacen"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            DescripcionAlmacen = (reader["descripcion_almacen"]) is DBNull ? "" : Convert.ToString(reader["descripcion_almacen"]),
                            CapacidadAlmacen = Convert.ToDouble(reader["capacidad_almacen"]),
                            UbicacionAlmacen = Convert.ToString(reader["ubicacion_almacen"]),
                            IdBodegaUbicacion = Convert.ToInt32(reader["id_bodega_ubicacion_almacen"]),
                            NombreBodegaUbicacion = Convert.ToString(reader["nombre_bodega"]),
                            //IdSubProducto = (reader["id_subproducto_cafe"]) is DBNull ? 0 : Convert.ToInt32(reader["id_subproducto_cafe"]),
                            //NombreSubProducto = (reader["nombre_subproducto"]) is DBNull ? "" : Convert.ToString(reader["nombre_subproducto"]),
                            CantidadActualAlmacen = (reader["cantidad_actual_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_almacen"])),
                            CantidadActualSacoAlmacen = (reader["cantidad_actual_saco_almacen"] is DBNull ? 0.0 : Convert.ToDouble(reader["cantidad_actual_saco_almacen"])),
                            IdCalidadCafe = reader["id_calidad_cafe"] is DBNull ? (int?)null : Convert.ToInt32(reader["id_calidad_cafe"]),
                            NombreCalidadCafe = reader["nombre_calidad"] is DBNull ? null : Convert.ToString(reader["nombre_calidad"])
                        };

                        almacens.Add(almacen);
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
            return almacens;
        }

        //
        public Almacen CountExistenceCofee(int id)
        {
            Almacen almacen = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT SUM(a.cantidad_actual_almacen) AS TotalExistencia, cc.nombre_calidad 
                                    FROM Almacen a
                                    INNER JOIN Calidad_Cafe cc ON a.id_calidad_cafe = cc.id_calidad
                                    WHERE cc.id_calidad = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            almacen = new Almacen()
                            {
                                NombreCalidadCafe = (reader["nombre_calidad"]) is DBNull ? "" : Convert.ToString(reader["nombre_calidad"]),
                                CountExistenceCoffe = (reader["TotalExistencia"] is DBNull ? 0 : Convert.ToInt32(reader["TotalExistencia"]))
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
            return almacen;
        }
        
        //
        public Almacen CountAlmacen()
        {
            Almacen almacen = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalExistencia 
                                    FROM Almacen ";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            almacen = new Almacen()
                            {
                                CountAlmacen = (reader["TotalExistencia"] is DBNull ? 0 : Convert.ToInt32(reader["TotalExistencia"]))
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
            return almacen;
        }

        //
        public bool ExisteAlmacen(string nombre, int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM Almacen WHERE nombre_almacen = @nombre AND id_bodega_ubicacion_almacen = @ib";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombre", nombre);
                conexion.AgregarParametro("@ib", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia del Almacen: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        public Almacen ObtenerUltimoId()
        {
            Almacen alm = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_almacen) AS LastId FROM Almacen";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        alm = new Almacen()
                        {
                            LastId = (reader["LastId"]) is DBNull ? 0: Convert.ToInt32(reader["LastId"])
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
                string consulta = "SELECT COUNT(*) FROM Almacen WHERE id_almacen = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia del Almacen: " + ex.Message);
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
