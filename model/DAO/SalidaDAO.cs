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
    class SalidaDAO
    {
        private ConnectionDB conexion;

        public SalidaDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarSalidaCafe(Salida salidaCafe)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Salida_Cafe SET
                                        num_salida = @numSalida,
                                        id_cosecha_salida = @idCosecha,";
                                        if (salidaCafe.IdProcedencia != 0) { consulta += "id_procedencia_salida = @idProcedencia,"; }
                                        consulta += @"id_calidad_cafe_salida = @idCalidadCafe,
                                        id_subproducto_salida = @idSubProducto,
                                        tipo_salida = @tipoSalida,
                                        cantidad_salida_qqs_cafe = @cantidadSalidaQQs,
                                        cantidad_salida_sacos_cafe = @cantidadSalidaSacos,
                                        fecha_salidaCafe = @fechaSalidaCafe, 
                                        id_personal_salida = @idPersonal, 
                                        observacion_salida = @observacionSalida,
                                        id_almacen_salida = @iAlmacen,
                                        id_bodega_salida = @iBodega";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@numSalida", salidaCafe.NumSalida_cafe);
                conexion.AgregarParametro("@idCosecha", salidaCafe.IdCosecha);
                conexion.AgregarParametro("@idProcedencia", salidaCafe.IdProcedencia);
                conexion.AgregarParametro("@idCalidadCafe", salidaCafe.IdCalidadCafe);
                conexion.AgregarParametro("@idSubProducto", salidaCafe.IdSubProducto);
                conexion.AgregarParametro("@tipoSalida", salidaCafe.TipoSalida);
                conexion.AgregarParametro("@cantidadSalidaQQs", salidaCafe.CantidadSalidaQQs);
                conexion.AgregarParametro("@cantidadSalidaSacos", salidaCafe.CantidadSalidaSacos);
                conexion.AgregarParametro("@fechaSalidaCafe", salidaCafe.FechaSalidaCafe);
                conexion.AgregarParametro("@idPersonal", salidaCafe.IdPersonal);
                conexion.AgregarParametro("@observacionSalida", salidaCafe.ObservacionSalida);
                conexion.AgregarParametro("@iAlmacen", salidaCafe.IdAlmacen);
                conexion.AgregarParametro("@iBodega", salidaCafe.IdBodega);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se insertó correctamente
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de la Salida de Café en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        // Función para mostrar todas las salidas de café
        public List<Salida> ObtenerSalidasCafe()
        {
            List<Salida> listaSalidasCafe = new List<Salida>();

            try
            {
                // Conexión a la base de datos 
                conexion.Conectar();

                string consulta = @"SELECT * FROM Salida_Cafe";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Salida salidaCafe = new Salida()
                        {
                            IdSalida_cafe = Convert.ToInt32(reader["id_salida_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_salida"]),
                            IdProcedencia = Convert.ToInt32(reader["id_procedencia_salida"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_salida"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_salida"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            FechaSalidaCafe = Convert.ToDateTime(reader["fecha_salidaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_salida"]),
                            ObservacionSalida = Convert.ToString(reader["observacion_salida"])
                        };

                        listaSalidasCafe.Add(salidaCafe);
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
            return listaSalidasCafe;
        }

        // Función para obtener una salida de café por su ID
        public Salida ObtenerSalidaCafePorId(int idSalidaCafe)
        {
            Salida salidaCafe = null;

            try
            {
                // Conexión a la base de datos 
                conexion.Conectar();

                string consulta = "SELECT * FROM Salida_Cafe WHERE id_salida_cafe = @Id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idSalidaCafe);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        salidaCafe = new Salida()
                        {
                            IdSalida_cafe = Convert.ToInt32(reader["id_salida_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_salida"]),
                            IdProcedencia = Convert.ToInt32(reader["id_procedencia_salida"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_salida"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_salida"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            FechaSalidaCafe = Convert.ToDateTime(reader["fecha_salidaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_salida"]),
                            ObservacionSalida = Convert.ToString(reader["observacion_salida"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Salida de Café: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return salidaCafe;
        }

        // Función para obtener una salida de café por su ID
        public List<Salida> ObtenerSalidaCafeNombres()
        {
            List<Salida> salidaCafes = new List<Salida>();

            try
            {
                // Conexión a la base de datos 
                conexion.Conectar();

                string consulta = @"SELECT s.*, c.nombre_cosecha, pd.nombre_procedencia, cc.nombre_calidad, spo.nombre_subproducto pl.nombre_personal
                                    FROM Salida_Cafe s 
                                    INNER JOIN Cosecha c ON s.id_cosecha_salida = c.id_cosecha
                                    INNER JOIN Procedencia_Destino_Cafe pd ON s.id_procedencia_salida = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON s.id_calidad_cafe_salida = cc.id_calidad
                                    INNER JOIN SubProducto spo ON s.id_subproducto_salida = spo.id_subproducto
                                    INNER JOIN Personal pl ON s.id_personal_salida = pl.id_personal";
                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Salida salidaCafe = new Salida()
                        {
                            IdSalida_cafe = Convert.ToInt32(reader["id_salida_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_salida"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            IdProcedencia = Convert.ToInt32(reader["id_procedencia_salida"]),
                            NombreProcedencia = Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_salida"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad_cafe"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_salida"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            FechaSalidaCafe = Convert.ToDateTime(reader["fecha_salidaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_salida"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionSalida = Convert.ToString(reader["observacion_salida"])
                        };
                        salidaCafes.Add(salidaCafe);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Salida de Café: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return salidaCafes;
        }

        // Función para actualizar un registro en la base de datos
        public bool ActualizarSalidaCafe(Salida salidaCafe)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos 
                conexion.Conectar();

                // Se crea el script SQL para actualizar
                string consulta = @"
                UPDATE Salida_Cafe SET
                    id_cosecha_salida = @idCosecha,
                    num_salida = @numSalida,";
                    if (salidaCafe.IdProcedencia != 0) { consulta += "id_procedencia_salida = @idProcedencia,"; }
                    consulta += @"id_bodega_salida = @iBodega,
                    id_almacen_salida = @iAlmacen,
                    id_calidad_cafe_salida = @idCalidadCafe,
                    id_subproducto_salida = @idSubProducto,
                    tipo_salida = @tipoSalida,
                    cantidad_salida_qqs_cafe = @cantidadSalidaQQs,
                    cantidad_salida_sacos_cafe = @cantidadSalidaSacos,
                    fecha_salidaCafe = @fechaSalidaCafe,
                    id_personal_salida = @idPersonal,
                    observacion_salida = @observacionSalida
                WHERE id_salida_cafe = @id";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@idCosecha", salidaCafe.IdCosecha);
                conexion.AgregarParametro("@numSalida", salidaCafe.NumSalida_cafe);
                conexion.AgregarParametro("@idProcedencia", salidaCafe.IdProcedencia);
                conexion.AgregarParametro("@iBodega", salidaCafe.IdBodega);
                conexion.AgregarParametro("@iAlmacen", salidaCafe.IdAlmacen);
                conexion.AgregarParametro("@idCalidadCafe", salidaCafe.IdCalidadCafe);
                conexion.AgregarParametro("@idSubProducto", salidaCafe.IdSubProducto);
                conexion.AgregarParametro("@tipoSalida", salidaCafe.TipoSalida);
                conexion.AgregarParametro("@cantidadSalidaQQs", salidaCafe.CantidadSalidaQQs);
                conexion.AgregarParametro("@cantidadSalidaSacos", salidaCafe.CantidadSalidaSacos);
                conexion.AgregarParametro("@fechaSalidaCafe", salidaCafe.FechaSalidaCafe);
                conexion.AgregarParametro("@idPersonal", salidaCafe.IdPersonal);
                conexion.AgregarParametro("@observacionSalida", salidaCafe.ObservacionSalida);
                conexion.AgregarParametro("@id", salidaCafe.IdSalida_cafe);

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
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return exito;
        }

        // Función para obtener todos los registros de Salida en la base de datos
        public List<Salida> ObtenerSalidasPorCosecha(int iCosecha)
        {
            List<Salida> listaSalidas = new List<Salida>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT s.*,
                                            c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Salida_Cafe s
                                    INNER JOIN Cosecha c ON s.id_cosecha_salida = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON s.id_procedencia_salida = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON s.id_calidad_cafe_salida = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON s.id_subproducto_salida = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON s.id_almacen_salida = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON s.id_bodega_salida = b.id_bodega
                                    INNER JOIN Personal p ON s.id_personal_salida = p.id_personal
                                    WHERE id_cosecha_salida = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", iCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Salida salida = new Salida()
                        {
                            IdSalida_cafe = Convert.ToInt32(reader["id_salida_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_salida"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumSalida_cafe = Convert.ToInt32(reader["num_salida"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_salida"]) ? 0 : Convert.ToInt32(reader["id_procedencia_salida"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_salida"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_salida"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_salida"]) ? 0 : Convert.ToInt32(reader["id_almacen_salida"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_salida"]) ? 0 : Convert.ToInt32(reader["id_bodega_salida"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            FechaSalidaCafe = Convert.ToDateTime(reader["fecha_salidaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_salida"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionSalida = Convert.IsDBNull(reader["observacion_salida"]) ? null : Convert.ToString(reader["observacion_salida"])
                        };

                        listaSalidas.Add(salida);
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

            return listaSalidas;
        }

        // Función para eliminar un registro en la base de datos
        public bool EliminarSalidaCafe(int idSalidaCafe)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos 
                conexion.Conectar();

                // Se crea el script SQL para eliminar
                string consulta = "DELETE FROM Salida_Cafe WHERE id_salida_cafe = @id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", idSalidaCafe);

                int filasAfectadas = conexion.EjecutarInstruccion();

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("El registro se eliminó correctamente");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo eliminar el registro");
                    exito = false;
                }
            }
            catch (Exception ex)
            {
                if (ex is MySqlException sqlException)
                {
                    SqlExceptionHelper.NumberException = sqlException.Number;
                    SqlExceptionHelper.MessageExceptionSql = SqlExceptionHelper.HandleSqlException(ex);
                }
                Console.WriteLine("Ocurrió un error al eliminar el registro: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return exito;
        }

        // Función para buscar salidas de café
        public List<Salida> BuscarSalidaCafe(string buscar)
        {
            List<Salida> salidasCafe = new List<Salida>();

            try
            {
                // Conexión a la base de datos 
                conexion.Conectar();

                // Crear la consulta SQL para buscar salidas de café
                string consulta = @"
                SELECT s.*,
                       c.nombre_cosecha,
                       pd.nombre_procedencia,
                       cc.nombre_calidad_cafe,
                       a.nombre_almacen,
                       b.nombre_bodega,
                       sbp.nombre_subproducto,
                       p.nombre_personal
                FROM Salida_Cafe s
                INNER JOIN Cosecha c ON s.id_cosecha_salida = c.id_cosecha
                LEFT JOIN Procedencia_Destino_Cafe pd ON s.id_procedencia_salida = pd.id_procedencia
                INNER JOIN Calidad_Cafe cc ON s.id_calidad_cafe_salida = cc.id_calidad
                INNER JOIN SubProducto sbp ON s.id_subproducto_salida = sbp.id_subproducto
                LEFT JOIN Almacen a ON t.id_almacen_salida = a.id_almacen
                LEFT JOIN Bodega_Cafe b ON t.id_bodega_salida = b.id_bodega
                INNER JOIN Personal p ON s.id_personal_salida = p.id_personal
                WHERE c.nombre_cosecha LIKE CONCAT('%', @search, '%') OR
                      pd.nombre_procedencia LIKE CONCAT('%', @search, '%') OR
                      cc.nombre_calidad_cafe LIKE CONCAT('%', @search, '%') OR
                      sbp.nombre_subproducto LIKE CONCAT('%', @search, '%') OR
                      s.tipo_salida LIKE CONCAT('%', @search, '%') OR
                      b.nombre_bodega LIKE CONCAT('%', @search, '%') OR
                      p.nombre_personal LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", buscar);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Salida salidaCafe = new Salida()
                        {
                            IdSalida_cafe = Convert.ToInt32(reader["id_salida_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_salida"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumSalida_cafe = Convert.ToInt32(reader["num_salida"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_salida"]) ? 0 : Convert.ToInt32(reader["id_procedencia_salida"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_salida"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_salida"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_salida"]) ? 0 : Convert.ToInt32(reader["id_almacen_salida"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_salida"]) ? 0 : Convert.ToInt32(reader["id_bodega_salida"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            FechaSalidaCafe = Convert.ToDateTime(reader["fecha_salidaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_salida"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionSalida = Convert.IsDBNull(reader["observacion_salida"]) ? null : Convert.ToString(reader["observacion_salida"])
                        };

                        salidasCafe.Add(salidaCafe);
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
            return salidasCafe;
        }

        // Función para obtener todos los registros de la Salida en la base de datos
        public Salida ObtenerSalidasPorIDNombre(int idSalida)
        {
            Salida salida = null;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT s.*,
                                            c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Salida_Cafe s
                                    INNER JOIN Cosecha c ON s.id_cosecha_salida = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON s.id_procedencia_salida = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON s.id_calidad_cafe_salida = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON s.id_subproducto_salida = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON s.id_almacen_salida = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON s.id_bodega_salida = b.id_bodega
                                    INNER JOIN Personal p ON s.id_personal_salida = p.id_personal
                                    WHERE s.id_salida_cafe = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idSalida);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        salida = new Salida()
                        {
                            IdSalida_cafe = Convert.ToInt32(reader["id_salida_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_salida"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumSalida_cafe = Convert.ToInt32(reader["num_salida"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_salida"]) ? 0 : Convert.ToInt32(reader["id_procedencia_salida"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_salida"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_salida"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_salida"]) ? 0 : Convert.ToInt32(reader["id_almacen_salida"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_salida"]) ? 0 : Convert.ToInt32(reader["id_bodega_salida"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            FechaSalidaCafe = Convert.ToDateTime(reader["fecha_salidaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_salida"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionSalida = Convert.IsDBNull(reader["observacion_salida"]) ? null : Convert.ToString(reader["observacion_salida"])
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

            return salida;
        }
        
        // Función para obtener todos los registros de la Salida en la base de datos
        public Salida ObtenerSalidasPorCosechaIDNombre(int numSalida, int iCosecha)
        {
            Salida salida = null;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT s.*,
                                            c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Salida_Cafe s
                                    INNER JOIN Cosecha c ON s.id_cosecha_salida = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON s.id_procedencia_salida = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON s.id_calidad_cafe_salida = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON s.id_subproducto_salida = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON s.id_almacen_salida = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON s.id_bodega_salida = b.id_bodega
                                    INNER JOIN Personal p ON s.id_personal_salida = p.id_personal
                                    WHERE s.num_salida = @Id AND s.id_cosecha_salida = @ICosecha";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", numSalida);
                conexion.AgregarParametro("@ICosecha", iCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        salida = new Salida()
                        {
                            IdSalida_cafe = Convert.ToInt32(reader["id_salida_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_salida"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumSalida_cafe = Convert.ToInt32(reader["num_salida"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_salida"]) ? 0 : Convert.ToInt32(reader["id_procedencia_salida"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_salida"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_salida"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_salida"]) ? 0 : Convert.ToInt32(reader["id_almacen_salida"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_salida"]) ? 0 : Convert.ToInt32(reader["id_bodega_salida"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            FechaSalidaCafe = Convert.ToDateTime(reader["fecha_salidaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_salida"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionSalida = Convert.IsDBNull(reader["observacion_salida"]) ? null : Convert.ToString(reader["observacion_salida"])
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

            return salida;
        }

        //
        public List<ReportSalida> ObtenerReporteSalida(int idSalida)
        {
            List<ReportSalida> data = new List<ReportSalida>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT 
		                                    c.nombre_cosecha,
                                            s.num_salida,
                                            DATE_FORMAT(s.fecha_salidaCafe, '%d/%m/%Y') AS Fecha,
                                            s.tipo_salida,
                                            a.nombre_almacen,
                                            b.nombre_bodega,
	                                       pd.nombre_procedencia,
	                                       cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           s.cantidad_salida_sacos_cafe,
                                           s.cantidad_salida_qqs_cafe,
	                                       p.nombre_personal,
                                           s.observacion_salida
                                    FROM Salida_Cafe s
                                    INNER JOIN Cosecha c ON s.id_cosecha_salida = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON s.id_procedencia_salida = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON s.id_calidad_cafe_salida = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON s.id_subproducto_salida = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON s.id_almacen_salida = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON s.id_bodega_salida = b.id_bodega
                                    INNER JOIN Personal p ON s.id_personal_salida = p.id_personal
                                    WHERE s.id_salida_cafe = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idSalida);

                MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta);
                {
                    while (reader.Read())
                    {
                        ReportSalida reporteSalida = new ReportSalida()
                        {
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumSalida_cafe = Convert.ToInt32(reader["num_salida"]),
                            FechaSalidaCafe = Convert.ToString(reader["Fecha"]),
                            TipoSalida = Convert.ToString(reader["tipo_salida"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            CantidadSalidaQQs = Convert.ToDouble(reader["cantidad_salida_qqs_cafe"]),
                            CantidadSalidaSacos = Convert.ToDouble(reader["cantidad_salida_sacos_cafe"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionSalida = Convert.IsDBNull(reader["observacion_salida"]) ? null : Convert.ToString(reader["observacion_salida"])
                        };
                        data.Add(reporteSalida);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte del traslado: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return data;
        }
        //
        public Salida CountSalida(int idCosecha)
        {
            Salida salida = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalSalida FROM Salida_Cafe
                                    WHERE id_cosecha_salida = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            salida = new Salida()
                            {
                                CountSalida = Convert.ToInt32(reader["TotalSalida"])
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
            return salida;
        }

        //
        public bool VerificarExistenciaSalida(int idCosecha, int numSalida)
        {
            bool existeSalida = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Consulta SQL con la cláusula WHERE para verificar la existencia de la Salida
                string consulta = @"SELECT COUNT(*) FROM Salida_Cafe 
                            WHERE id_cosecha_salida = @idCosecha AND num_salida = @numSalida";

                conexion.CrearComando(consulta);

                // Agregar los parámetros
                conexion.AgregarParametro("@idCosecha", idCosecha);
                conexion.AgregarParametro("@numSalida", numSalida);

                // Ejecutar la consulta y obtener el resultado
                int resultado = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                // Si el resultado es mayor que 0, significa que existe una Salida con los valores proporcionados
                existeSalida = resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de las Salidas: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return existeSalida;
        }


    }
}
