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
    class TrillaDAO
    {
        private ConnectionDB conexion;

        public TrillaDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        // Función para insertar un nuevo registro en la tabla Trilla
        public bool InsertarTrilla(Trilla trilla)
        {
            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para insertar
                string consulta = @"
                INSERT INTO Trilla SET
                    id_cosecha_trilla = @idCosecha,
                    num_trilla = @numTrilla,
                    tipo_movimiento_trilla = @tipoMovimientoTrilla,
                    id_calidad_cafe_trilla = @idCalidadCafe,
                    id_subproducto_trilla = @idSubProducto,
                    cantidad_trilla_qqs_cafe = @cantidadTrillaQQs,
                    cantidad_trilla_sacos_cafe = @cantidadTrillaSacos,";
                    if (trilla.IdProcedencia != 0){ consulta += "id_procedencia_trilla = @idProcedencia,"; }
                    if (trilla.IdAlmacen != 0){ consulta += "id_almacen_trilla = @iAlmacen,"; }
                    if (trilla.IdBodega != 0){ consulta += "id_bodega_trilla = @iBodega,"; }
                consulta += @"fecha_trillaCafe = @fechaTrillaCafe,
                    id_personal_trilla = @idPersonal,
                    observacion_trilla = @observacionTrilla";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@idCosecha", trilla.IdCosecha);
                conexion.AgregarParametro("@numTrilla", trilla.NumTrilla);
                conexion.AgregarParametro("@tipoMovimientoTrilla", trilla.TipoMovimientoTrilla);
                conexion.AgregarParametro("@idCalidadCafe", trilla.IdCalidadCafe);
                conexion.AgregarParametro("@idSubProducto", trilla.IdSubProducto);
                conexion.AgregarParametro("@cantidadTrillaQQs", trilla.CantidadTrillaQQs);
                conexion.AgregarParametro("@cantidadTrillaSacos", trilla.CantidadTrillaSacos);
                conexion.AgregarParametro("@idProcedencia", trilla.IdProcedencia);
                conexion.AgregarParametro("@iAlmacen", trilla.IdAlmacen);
                conexion.AgregarParametro("@iBodega", trilla.IdBodega);
                conexion.AgregarParametro("@fechaTrillaCafe", trilla.FechaTrillaCafe);
                conexion.AgregarParametro("@idPersonal", trilla.IdPersonal);
                conexion.AgregarParametro("@observacionTrilla", trilla.ObservacionTrilla);

                int filasAfectadas = conexion.EjecutarInstruccion();

                // Si se afecta una fila, se insertó correctamente
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de la Trilla en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        // Función para Obtener trillas
        public List<Trilla> ObtenerTrillasNombre()
        {
            List<Trilla> trillas = new List<Trilla>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para buscar trillas
                string consulta = @" SELECT t.*,
                                           c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Trilla t
                                    INNER JOIN Cosecha c ON t.id_cosecha_trilla = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON t.id_procedencia_trilla = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_trilla = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_trilla = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON t.id_almacen_trilla = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON t.id_bodega_trilla = b.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_trilla = p.id_personal";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Trilla trilla = new Trilla()
                        {
                            IdTrilla_cafe = Convert.ToInt32(reader["id_trilla"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_trilla"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_trilla"]) ? 0 : Convert.ToInt32(reader["id_procedencia_trilla"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_trilla"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_trilla"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_trilla"]) ? 0 : Convert.ToInt32(reader["id_almacen_trilla"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_trilla"]) ? 0 : Convert.ToInt32(reader["id_bodega_trilla"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            FechaTrillaCafe = Convert.ToDateTime(reader["fecha_trillaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_trilla"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
                        };

                        trillas.Add(trilla);
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
            return trillas;
        }

        // Función para buscar trillas
        public List<Trilla> BuscarTrilla(string buscar)
        {
            List<Trilla> trillas = new List<Trilla>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para buscar trillas
                string consulta = @" SELECT t.*,
                                           c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Trilla t
                                    INNER JOIN Cosecha c ON t.id_cosecha_trilla = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON t.id_procedencia_trilla = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_trilla = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_trilla = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON t.id_almacen_trilla = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON t.id_bodega_trilla = b.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_trilla = p.id_personal
                                    WHERE c.nombre_cosecha LIKE CONCAT('%', @search, '%') OR
                                          pd.nombre_procedencia LIKE CONCAT('%', @search, '%') OR
                                          cc.nombre_calidad_cafe LIKE CONCAT('%', @search, '%') OR
                                          sbp.nombre_subproducto LIKE CONCAT('%', @search, '%') OR
                                          t.tipo_movimiento_trilla LIKE CONCAT('%', @search, '%') OR
                                          p.nombre_personal LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", buscar);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Trilla trilla = new Trilla()
                        {
                            IdTrilla_cafe = Convert.ToInt32(reader["id_trilla"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_trilla"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_trilla"]) ? 0 : Convert.ToInt32(reader["id_procedencia_trilla"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_trilla"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_trilla"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_trilla"]) ? 0 : Convert.ToInt32(reader["id_almacen_trilla"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_trilla"]) ? 0 : Convert.ToInt32(reader["id_bodega_trilla"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            FechaTrillaCafe = Convert.ToDateTime(reader["fecha_trillaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_trilla"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
                        };

                        trillas.Add(trilla);
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
            return trillas;
        }

        // Función para actualizar un registro de Trilla en la base de datos
        public bool ActualizarTrilla(Trilla trilla)
        {
            bool exito = false;
            
            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para actualizar
                string consulta = @"UPDATE Trilla 
                                SET id_cosecha_trilla = @idCosecha,
                                    num_trilla = @numTrilla,
                                    tipo_movimiento_trilla = @tipoMovimientoTrilla,
                                    id_calidad_cafe_trilla = @idCalidadCafe,
                                    id_subproducto_trilla = @idSubProducto,
                                    cantidad_trilla_qqs_cafe = @cantidadTrillaQQs,
                                    cantidad_trilla_sacos_cafe = @cantidadTrillaSacos,";
                                    if (trilla.IdProcedencia != 0){ consulta += "id_procedencia_trilla = @idProcedencia,"; }
                                    if (trilla.IdAlmacen != 0){ consulta += "id_almacen_trilla = @iAlmacen,"; }
                                    if (trilla.IdBodega != 0){ consulta += "id_bodega_trilla = @iBodega,"; }
                                    consulta += @"fecha_trillaCafe = @fechaTrillaCafe,
                                    id_personal_trilla = @idPersonal,
                                    observacion_trilla = @observacionTrilla
                                    WHERE id_trilla = @id";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@idCosecha", trilla.IdCosecha);
                conexion.AgregarParametro("@numTrilla", trilla.NumTrilla);
                conexion.AgregarParametro("@tipoMovimientoTrilla", trilla.TipoMovimientoTrilla);
                conexion.AgregarParametro("@idCalidadCafe", trilla.IdCalidadCafe);
                conexion.AgregarParametro("@idSubProducto", trilla.IdSubProducto);
                conexion.AgregarParametro("@cantidadTrillaQQs", trilla.CantidadTrillaQQs);
                conexion.AgregarParametro("@cantidadTrillaSacos", trilla.CantidadTrillaSacos);
                conexion.AgregarParametro("@idProcedencia", trilla.IdProcedencia);
                conexion.AgregarParametro("@iAlmacen", trilla.IdAlmacen);
                conexion.AgregarParametro("@iBodega", trilla.IdBodega);
                conexion.AgregarParametro("@fechaTrillaCafe", trilla.FechaTrillaCafe);
                conexion.AgregarParametro("@idPersonal", trilla.IdPersonal);
                conexion.AgregarParametro("@observacionTrilla", trilla.ObservacionTrilla);
                conexion.AgregarParametro("@id", trilla.IdTrilla_cafe);

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

        //funcion para eliminar un registro de la base de datos
        public void EliminarTrilla(int idTrilla)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Trilla WHERE id_trilla = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idTrilla);

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

        // Función para obtener todos los registros de Trilla en la base de datos
        public List<Trilla> ObtenerTrillas()
        {
            List<Trilla> listaTrillas = new List<Trilla>();

            try
            {
                // Conexión a la base de datos (asegúrate de tener la clase "conexion" y los métodos correspondientes)
                conexion.Conectar();

                string consulta = "SELECT * FROM Trilla";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Trilla trilla = new Trilla()
                        {
                            IdTrilla_cafe = Convert.ToInt32(reader["id_trilla"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_trilla"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_trilla"]) ? 0 : Convert.ToInt32(reader["id_procedencia_trilla"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_trilla"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad_cafe"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_trilla"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_trilla"]) ? 0 : Convert.ToInt32(reader["id_almacen_trilla"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_trilla"]) ? 0 : Convert.ToInt32(reader["id_bodega_trilla"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            FechaTrillaCafe = Convert.ToDateTime(reader["fecha_trillaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_trilla"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
                        };

                        listaTrillas.Add(trilla);
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

            return listaTrillas;
        }
        
        // Función para obtener todos los registros de Trilla en la base de datos
        public List<Trilla> ObtenerTrillasPorCosecha(int iCosecha)
        {
            List<Trilla> listaTrillas = new List<Trilla>();

            try
            {
                // Conexión a la base de datos (asegúrate de tener la clase "conexion" y los métodos correspondientes)
                conexion.Conectar();

                string consulta = @"SELECT t.*,
                                            c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Trilla t
                                    INNER JOIN Cosecha c ON t.id_cosecha_trilla = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON t.id_procedencia_trilla = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_trilla = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_trilla = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON t.id_almacen_trilla = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON t.id_bodega_trilla = b.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_trilla = p.id_personal
                                    WHERE id_cosecha_trilla = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", iCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Trilla trilla = new Trilla()
                        {
                            IdTrilla_cafe = Convert.ToInt32(reader["id_trilla"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_trilla"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_trilla"]) ? 0 : Convert.ToInt32(reader["id_procedencia_trilla"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_trilla"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_trilla"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_trilla"]) ? 0 : Convert.ToInt32(reader["id_almacen_trilla"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_trilla"]) ? 0 : Convert.ToInt32(reader["id_bodega_trilla"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            FechaTrillaCafe = Convert.ToDateTime(reader["fecha_trillaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_trilla"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
                        };

                        listaTrillas.Add(trilla);
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

            return listaTrillas;
        }

        // Función para obtener todos los registros de Trilla en la base de datos
        public Trilla ObtenerTrillasPorID(int idTrilla)
        {
            Trilla trilla = null;

            try
            {
                // Conexión a la base de datos (asegúrate de tener la clase "conexion" y los métodos correspondientes)
                conexion.Conectar();

                string consulta = "SELECT * FROM Trilla WHERE id_trilla = @Id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idTrilla);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        trilla = new Trilla()
                        {
                            IdTrilla_cafe = Convert.ToInt32(reader["id_trilla"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_trilla"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_trilla"]) ? 0 : Convert.ToInt32(reader["id_procedencia_trilla"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_trilla"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad_cafe"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_trilla"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_trilla"]) ? 0 : Convert.ToInt32(reader["id_almacen_trilla"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_trilla"]) ? 0 : Convert.ToInt32(reader["id_bodega_trilla"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            FechaTrillaCafe = Convert.ToDateTime(reader["fecha_trillaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_trilla"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
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

            return trilla;
        }

        public List<ReportesTrilla> ObtenerTrillasReports(int idTrilla)
        {
            List<ReportesTrilla> data = new List<ReportesTrilla>();

            try
            {
                // Conexión a la base de datos (asegúrate de tener la clase "conexion" y los métodos correspondientes)
                conexion.Conectar();

                string consulta = @"SELECT 
	                                    c.nombre_cosecha,
                                        t.num_trilla,
                                        DATE_FORMAT(t.fecha_trillaCafe, '%d/%m/%Y') AS Fecha,
                                        t.tipo_movimiento_trilla,
                                       pd.nombre_procedencia,
                                       cc.nombre_calidad,
                                       sbp.nombre_subproducto,
                                       t.cantidad_trilla_sacos_cafe,
                                       t.cantidad_trilla_qqs_cafe,
                                       a.nombre_almacen,
                                       b.nombre_bodega,
                                       p.nombre_personal,
                                       t.observacion_trilla
                                    FROM Trilla t
                                    INNER JOIN Cosecha c ON t.id_cosecha_trilla = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON t.id_procedencia_trilla = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_trilla = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_trilla = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON t.id_almacen_trilla = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON t.id_bodega_trilla = b.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_trilla = p.id_personal
                                    WHERE id_trilla = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idTrilla);

                MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta);
                {
                    while (reader.Read())
                    {
                        ReportesTrilla reportesTrillado = new ReportesTrilla()
                        {
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            FechaTrillaCafe = Convert.ToString(reader["Fecha"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
                        };
                        data.Add(reportesTrillado);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte del trillado: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return data;
        }

        // Función para obtener todos los registros de Trilla en la base de datos
        public Trilla ObtenerTrillasPorIDNombre(int idTrilla)
        {
            Trilla trilla = null;

            try
            {
                // Conexión a la base de datos (asegúrate de tener la clase "conexion" y los métodos correspondientes)
                conexion.Conectar();

                string consulta = @"SELECT t.*,
                                            c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Trilla t
                                    INNER JOIN Cosecha c ON t.id_cosecha_trilla = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON t.id_procedencia_trilla = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_trilla = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_trilla = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON t.id_almacen_trilla = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON t.id_bodega_trilla = b.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_trilla = p.id_personal
                                    WHERE id_trilla = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idTrilla);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        trilla = new Trilla()
                        {
                            IdTrilla_cafe = Convert.ToInt32(reader["id_trilla"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_trilla"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_trilla"]) ? 0 : Convert.ToInt32(reader["id_procedencia_trilla"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_trilla"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_trilla"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_trilla"]) ? 0 : Convert.ToInt32(reader["id_almacen_trilla"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_trilla"]) ? 0 : Convert.ToInt32(reader["id_bodega_trilla"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            FechaTrillaCafe = Convert.ToDateTime(reader["fecha_trillaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_trilla"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
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

            return trilla;
        }
        
        // Función para obtener todos los registros de Trilla en la base de datos
        public Trilla ObtenerTrillasPorCosechaIDNombre(int numTrilla, int iCosecha)
        {
            Trilla trilla = null;

            try
            {
                // Conexión a la base de datos (asegúrate de tener la clase "conexion" y los métodos correspondientes)
                conexion.Conectar();

                string consulta = @"SELECT t.*,
                                            c.nombre_cosecha,
                                           pd.nombre_procedencia,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           a.nombre_almacen,
                                           b.nombre_bodega,
                                           p.nombre_personal
                                    FROM Trilla t
                                    INNER JOIN Cosecha c ON t.id_cosecha_trilla = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON t.id_procedencia_trilla = pd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_trilla = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_trilla = sbp.id_subproducto
                                    LEFT JOIN Almacen a ON t.id_almacen_trilla = a.id_almacen
                                    LEFT JOIN Bodega_Cafe b ON t.id_bodega_trilla = b.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_trilla = p.id_personal
                                    WHERE t.num_trilla = @Id AND t.id_cosecha_trilla = @ICosecha";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", numTrilla);
                conexion.AgregarParametro("@ICosecha", iCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        trilla = new Trilla()
                        {
                            IdTrilla_cafe = Convert.ToInt32(reader["id_trilla"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_trilla"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTrilla = Convert.ToInt32(reader["num_trilla"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_trilla"]) ? 0 : Convert.ToInt32(reader["id_procedencia_trilla"]),
                            NombreProcedencia = Convert.IsDBNull(reader["nombre_procedencia"]) ? null : Convert.ToString(reader["nombre_procedencia"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_trilla"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_trilla"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacen = Convert.IsDBNull(reader["id_almacen_trilla"]) ? 0 : Convert.ToInt32(reader["id_almacen_trilla"]),
                            NombreAlmacen = Convert.IsDBNull(reader["nombre_almacen"]) ? null : Convert.ToString(reader["nombre_almacen"]),
                            IdBodega = Convert.IsDBNull(reader["id_bodega_trilla"]) ? 0 : Convert.ToInt32(reader["id_bodega_trilla"]),
                            NombreBodega = Convert.IsDBNull(reader["nombre_bodega"]) ? null : Convert.ToString(reader["nombre_bodega"]),
                            TipoMovimientoTrilla = Convert.ToString(reader["tipo_movimiento_trilla"]),
                            CantidadTrillaQQs = Convert.ToDouble(reader["cantidad_trilla_qqs_cafe"]),
                            CantidadTrillaSacos = Convert.ToDouble(reader["cantidad_trilla_sacos_cafe"]),
                            FechaTrillaCafe = Convert.ToDateTime(reader["fecha_trillaCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_trilla"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTrilla = Convert.IsDBNull(reader["observacion_trilla"]) ? null : Convert.ToString(reader["observacion_trilla"])
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

            return trilla;
        }

        //
        public Trilla CountTrilla(int idCosecha)
        {
            Trilla trilla = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalTrilla FROM Trilla
                                    WHERE id_cosecha_trilla = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            trilla = new Trilla()
                            {
                                CountTrilla = Convert.ToInt32(reader["TotalTrilla"])
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
            return trilla;
        }

        //
        public bool VerificarExistenciaTrilla(int idCosecha, int numSubpartida)
        {
            bool existeSubPartida = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Consulta SQL con la cláusula WHERE para verificar la existencia de la subpartida
                string consulta = @"SELECT COUNT(*) FROM Trilla 
                            WHERE id_cosecha_trilla = @idCosecha AND num_trilla = @numTrilla";

                conexion.CrearComando(consulta);

                // Agregar los parámetros para evitar posibles ataques de inyección SQL
                conexion.AgregarParametro("@idCosecha", idCosecha);
                conexion.AgregarParametro("@numTrilla", numSubpartida);

                // Ejecutar la consulta y obtener el resultado
                int resultado = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                // Si el resultado es mayor que 0, significa que existe una subpartida con los valores proporcionados
                existeSubPartida = resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de la subpartida: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return existeSubPartida;
        }



    }
}
