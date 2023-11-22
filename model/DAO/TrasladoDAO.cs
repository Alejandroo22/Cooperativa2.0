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
    class TrasladoDAO
    {
        private ConnectionDB conexion;

        public TrasladoDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        // Función para insertar un nuevo registro en la base de datos
        public bool InsertarTrasladoCafe(Traslado traslado)
        {
            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para insertar
                string consulta = @"INSERT INTO Traslado_Cafe SET 
                                    id_cosecha_traslado = @idCosecha, 
                                    num_traslado = @numTraslado,";
                                    if (traslado.IdProcedencia != 0) {consulta += "id_procedencia_procedencia_traslado = @idProcedenciaP,"; }
                                    if (traslado.IdProcedenciaDestino != 0) {consulta += "id_procedencia_destino_traslado = @idProcedenciaD,"; }
                                    consulta += @"id_almacen_procedencia_traslado = @iAlmacenP,
                                    id_bodega_procedencia_traslado = @iBodegaP,
                                    id_almacen_destino_traslado = @iAlmacenD,
                                    id_bodega_destino_traslado = @iBodegaD,
                                    id_calidad_cafe_traslado = @idCalidadCafe, 
                                    id_subproducto_traslado = @idSubProducto,
                                    cantidad_traslado_qqs_cafe = @cantidadQQs,
                                    cantidad_traslado_sacos_cafe = @cantidadSacos,
                                    fecha_trasladoCafe = @fechaTraslado, 
                                    id_personal_traslado = @idPersonal,
                                    observacion_traslado = @observacion";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@idCosecha", traslado.IdCosecha);
                conexion.AgregarParametro("@numTraslado", traslado.NumTraslado);
                conexion.AgregarParametro("@idProcedenciaP", traslado.IdProcedencia);
                conexion.AgregarParametro("@idProcedenciaD", traslado.IdProcedenciaDestino);
                conexion.AgregarParametro("@iAlmacenP", traslado.IdAlmacenProcedencia);
                conexion.AgregarParametro("@iBodegaP", traslado.IdBodegaProcedencia);
                conexion.AgregarParametro("@iAlmacenD", traslado.IdAlmacenDestino);
                conexion.AgregarParametro("@iBodegaD", traslado.IdBodegaDestino);
                conexion.AgregarParametro("@idCalidadCafe", traslado.IdCalidadCafe);
                conexion.AgregarParametro("@idSubProducto", traslado.IdSubProducto);
                conexion.AgregarParametro("@cantidadQQs", traslado.CantidadTrasladoQQs);
                conexion.AgregarParametro("@cantidadSacos", traslado.CantidadTrasladoSacos);
                conexion.AgregarParametro("@fechaTraslado", traslado.FechaTrasladoCafe);
                conexion.AgregarParametro("@idPersonal", traslado.IdPersonal);
                conexion.AgregarParametro("@observacion", traslado.ObservacionTraslado);

                int filasAfectadas = conexion.EjecutarInstruccion();

                // Si la fila se afecta, se insertó correctamente
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción del TrasladoCafe en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        // Función para actualizar un registro en la base de datos
        public bool ActualizarTrasladoCafe(Traslado traslado)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para actualizar
                string consulta = @"UPDATE Traslado_Cafe 
                                SET id_cosecha_traslado = @idCosecha,
                                    num_traslado = @numTraslado,";
                                if (traslado.IdProcedencia != 0) { consulta += "id_procedencia_procedencia_traslado = @idProcedenciaP,"; }
                                if (traslado.IdProcedenciaDestino != 0) { consulta += "id_procedencia_destino_traslado = @idProcedenciaD,"; }
                                consulta += @"id_almacen_procedencia_traslado = @iAlmacenP,
                                    id_bodega_procedencia_traslado = @iBodegaP,
                                    id_almacen_destino_traslado = @iAlmacenD,
                                    id_bodega_destino_traslado = @iBodegaD,
                                    id_calidad_cafe_traslado = @idCalidadCafe,
                                    id_subproducto_traslado = @idSubProducto,
                                    cantidad_traslado_qqs_cafe = @cantidadTrasladoQQs,
                                    cantidad_traslado_sacos_cafe = @cantidadTrasladoSacos,
                                    fecha_trasladoCafe = @fechaTraslado,
                                    id_personal_traslado = @idPersonal,
                                    observacion_traslado = @observacionTraslado
                                WHERE id_traslado_cafe = @id";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@idCosecha", traslado.IdCosecha);
                conexion.AgregarParametro("@numTraslado", traslado.NumTraslado);
                conexion.AgregarParametro("@idProcedenciaP", traslado.IdProcedencia);
                conexion.AgregarParametro("@idProcedenciaD", traslado.IdProcedenciaDestino);
                conexion.AgregarParametro("@iAlmacenP", traslado.IdAlmacenProcedencia);
                conexion.AgregarParametro("@iBodegaP", traslado.IdBodegaProcedencia);
                conexion.AgregarParametro("@iAlmacenD", traslado.IdAlmacenDestino);
                conexion.AgregarParametro("@iBodegaD", traslado.IdBodegaDestino);
                conexion.AgregarParametro("@idCalidadCafe", traslado.IdCalidadCafe);
                conexion.AgregarParametro("@idSubProducto", traslado.IdSubProducto);
                conexion.AgregarParametro("@cantidadTrasladoQQs", traslado.CantidadTrasladoQQs);
                conexion.AgregarParametro("@cantidadTrasladoSacos", traslado.CantidadTrasladoSacos);
                conexion.AgregarParametro("@fechaTraslado", traslado.FechaTrasladoCafe);
                conexion.AgregarParametro("@idPersonal", traslado.IdPersonal);
                conexion.AgregarParametro("@observacionTraslado", traslado.ObservacionTraslado);
                conexion.AgregarParametro("@id", traslado.Idtraslado_cafe);

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
        public void EliminarTraslado(int idTraslado)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Traslado_Cafe WHERE id_traslado_cafe = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idTraslado);

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

        // Función para buscar trillas
        public List<Traslado> BuscarTrasladoCafe(string buscar)
        {
            List<Traslado> traslados = new List<Traslado>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para buscar trillas
                string consulta = @"SELECT t.*,
                                            c.nombre_cosecha,
                                           pdr.nombre_procedencia AS ProcedenciaP,
                                           pdd.nombre_procedencia AS ProcedenciaD,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           ar.nombre_almacen AS AlmacenP,
                                           br.nombre_bodega AS BodegaP,
                                           ad.nombre_almacen AS AlmacenD,
                                           bd.nombre_bodega AS BodegaD,
                                           p.nombre_personal
                                    FROM Traslado_Cafe t
                                    INNER JOIN Cosecha c ON t.id_cosecha_salida = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pdr ON t.id_procedencia_procedencia_traslado = pdr.id_procedencia
                                    LEFT JOIN Procedencia_Destino_Cafe pdd ON t.id_procedencia_destino_traslado = pdd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_salida = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_salida = sbp.id_subproducto
                                    LEFT JOIN Almacen ar ON t.id_almacen_procedencia_traslado = ar.id_almacen
                                    LEFT JOIN Bodega_Cafe br ON t.id_bodega_procedencia_traslado = br.id_bodega
                                    LEFT JOIN Almacen ad ON t.id_almacen_destino_traslado = ad.id_almacen
                                    LEFT JOIN Bodega_Cafe bd ON t.id_bodega_destino_traslado = bd.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_salida = p.id_personal
                                    WHERE c.nombre_cosecha LIKE CONCAT('%', @search, '%') OR
                                     pdr.nombre_procedencia LIKE CONCAT('%', @search, '%') OR
                                     pdd.nombre_procedencia LIKE CONCAT('%', @search, '%') OR
                                     cc.nombre_calidad_cafe LIKE CONCAT('%', @search, '%') OR
                                     sbp.nombre_subproducto LIKE CONCAT('%', @search, '%') OR
                                     p.nombre_personal LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", buscar);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Traslado traslado = new Traslado()
                        {
                            Idtraslado_cafe = Convert.ToInt32(reader["id_traslado_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_traslado"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTraslado = Convert.ToInt32(reader["num_traslado"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_procedencia_traslado"]),
                            NombreProcedencia = Convert.IsDBNull(reader["ProcedenciaP"]) ? null : Convert.ToString(reader["ProcedenciaP"]),
                            IdProcedenciaDestino = Convert.IsDBNull(reader["id_procedencia_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_destino_traslado"]),
                            NombreProcedenciaDestino = Convert.IsDBNull(reader["ProcedenciaD"]) ? null : Convert.ToString(reader["ProcedenciaD"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_traslado"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_traslado"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacenProcedencia = Convert.IsDBNull(reader["id_almacen_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_procedencia_traslado"]),
                            NombreAlmacenProcedencia = Convert.IsDBNull(reader["AlmacenP"]) ? null : Convert.ToString(reader["AlmacenP"]),
                            IdAlmacenDestino = Convert.IsDBNull(reader["id_almacen_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_destino_traslado"]),
                            NombreAlmacenDestino = Convert.IsDBNull(reader["AlmacenD"]) ? null : Convert.ToString(reader["AlmacenD"]),
                            IdBodegaProcedencia = Convert.IsDBNull(reader["id_bodega_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_procedencia_traslado"]),
                            NombreBodegaProcedencia = Convert.IsDBNull(reader["BodegaP"]) ? null : Convert.ToString(reader["BodegaP"]),
                            IdBodegaDestino = Convert.IsDBNull(reader["id_bodega_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_destino_traslado"]),
                            NombreBodegaDestino = Convert.IsDBNull(reader["BodegaD"]) ? null : Convert.ToString(reader["BodegaD"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            FechaTrasladoCafe = Convert.ToDateTime(reader["fecha_trasladoCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_traslado"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTraslado = Convert.IsDBNull(reader["observacion_traslado"]) ? null : Convert.ToString(reader["observacion_traslado"])
                        };

                        traslados.Add(traslado);
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

            return traslados;
        }

        // Función para obtener todos los registros de Traslado_Cafe en la base de datos
        public List<Traslado> ObtenerTrasladosCafe()
        {
            List<Traslado> listaTraslados = new List<Traslado>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = "SELECT * FROM Traslado_Cafe";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Traslado traslado = new Traslado()
                        {
                            Idtraslado_cafe = Convert.ToInt32(reader["id_traslado_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_traslado"]),
                            IdProcedencia = (reader["id_procedencia_traslado"]) is DBNull ? 0 : Convert.ToInt32(reader["id_procedencia_traslado"]),
                            IdProcedenciaDestino = Convert.IsDBNull(reader["id_procedencia_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_destino_traslado"]),
                            IdAlmacenProcedencia = Convert.IsDBNull(reader["id_almacen_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_procedencia_traslado"]),
                            IdAlmacenDestino = Convert.IsDBNull(reader["id_almacen_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_destino_traslado"]),
                            IdBodegaProcedencia = Convert.IsDBNull(reader["id_bodega_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_procedencia_traslado"]),
                            IdBodegaDestino = Convert.IsDBNull(reader["id_bodega_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_destino_traslado"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_traslado"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_traslado"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            FechaTrasladoCafe = Convert.ToDateTime(reader["fecha_trasladoCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_traslado"]),
                            ObservacionTraslado = Convert.ToString(reader["observacion_traslado"])
                        };

                        listaTraslados.Add(traslado);
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

            return listaTraslados;
        }

        // Función para obtener un registro de Traslado_Cafe por su ID
        public Traslado ObtenerTrasladoCafePorID(int idTraslado)
        {
            Traslado traslado = null;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = "SELECT * FROM Traslado_Cafe WHERE id_traslado_cafe = @Id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idTraslado);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        traslado = new Traslado()
                        {
                            Idtraslado_cafe = Convert.ToInt32(reader["id_traslado_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_traslado"]),
                            IdProcedencia = (reader["id_procedencia_traslado"]) is DBNull ? 0 : Convert.ToInt32(reader["id_procedencia_traslado"]),
                            IdProcedenciaDestino = Convert.IsDBNull(reader["id_procedencia_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_destino_traslado"]),
                            IdAlmacenProcedencia = Convert.IsDBNull(reader["id_almacen_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_procedencia_traslado"]),
                            IdAlmacenDestino = Convert.IsDBNull(reader["id_almacen_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_destino_traslado"]),
                            IdBodegaProcedencia = Convert.IsDBNull(reader["id_bodega_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_procedencia_traslado"]),
                            IdBodegaDestino = Convert.IsDBNull(reader["id_bodega_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_destino_traslado"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_traslado"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_traslado"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            FechaTrasladoCafe = Convert.ToDateTime(reader["fecha_trasladoCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_traslado"]),
                            ObservacionTraslado = Convert.ToString(reader["observacion_traslado"])
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

            return traslado;
        }

        // Función para obtener todos los registros de TrasladoCafe con sus respectivos nombres de referencias
        public List<Traslado> ObtenerTrasladosCafeNombres()
        {
            List<Traslado> listaTraslados = new List<Traslado>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT tc.*,
                                       c.nombre_cosecha,
                                       sp.nombre_subpartida,
                                       pd.nombre_procedencia,
                                       pd2.nombre_procedencia AS nombre_destino,
                                       cc.nombre_calidad_cafe,
                                       sbp.nombre_subproducto,
                                       p.nombre_personal
                                FROM Traslado_Cafe tc
                                INNER JOIN Cosecha c ON tc.id_cosecha_traslado = c.id_cosecha
                                INNER JOIN SubPartida sp ON tc.id_subpartida_traslado = sp.id_subpartida
                                INNER JOIN Procedencia_Destino_Cafe pd ON tc.id_procedencia_traslado = pd.id_procedencia
                                INNER JOIN Procedencia_Destino_Cafe pd2 ON tc.id_destino_traslado = pd2.id_procedencia
                                INNER JOIN Calidad_Cafe cc ON tc.id_calidad_cafe_traslado = cc.id_calidad
                                INNER JOIN SubProducto sbp ON tc.id_subproducto_traslado = sbp.id_subproducto
                                INNER JOIN Personal p ON tc.id_personal_traslado = p.id_personal";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Traslado traslado = new Traslado()
                        {
                            Idtraslado_cafe = Convert.ToInt32(reader["id_traslado_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_traslado"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            IdProcedencia = (reader["id_procedencia_traslado"]) is DBNull ? 0 : Convert.ToInt32(reader["id_procedencia_traslado"]),
                            IdProcedenciaDestino = Convert.IsDBNull(reader["id_procedencia_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_destino_traslado"]),
                            IdAlmacenProcedencia = Convert.IsDBNull(reader["id_almacen_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_procedencia_traslado"]),
                            IdAlmacenDestino = Convert.IsDBNull(reader["id_almacen_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_destino_traslado"]),
                            IdBodegaProcedencia = Convert.IsDBNull(reader["id_bodega_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_procedencia_traslado"]),
                            IdBodegaDestino = Convert.IsDBNull(reader["id_bodega_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_destino_traslado"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_traslado"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad_cafe"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_traslado"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            FechaTrasladoCafe = Convert.ToDateTime(reader["fecha_trasladoCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_traslado"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTraslado = Convert.ToString(reader["observacion_traslado"])
                        };

                        listaTraslados.Add(traslado);
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

            return listaTraslados;
        }

        // Función para obtener todos los registros de Traslado en la base de datos
        public List<Traslado> ObtenerTrasladoPorCosecha(int iCosecha)
        {
            List<Traslado> listaTraslados = new List<Traslado>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT t.*,
                                            c.nombre_cosecha,
                                           pdr.nombre_procedencia AS ProcedenciaP,
                                           pdd.nombre_procedencia AS ProcedenciaD,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           ar.nombre_almacen AS AlmacenP,
                                           br.nombre_bodega AS BodegaP,
                                           ad.nombre_almacen AS AlmacenD,
                                           bd.nombre_bodega AS BodegaD,
                                           p.nombre_personal
                                    FROM Traslado_Cafe t
                                    INNER JOIN Cosecha c ON t.id_cosecha_traslado = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pdr ON t.id_procedencia_procedencia_traslado = pdr.id_procedencia
                                    LEFT JOIN Procedencia_Destino_Cafe pdd ON t.id_procedencia_destino_traslado = pdd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_traslado = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_traslado = sbp.id_subproducto
                                    LEFT JOIN Almacen ar ON t.id_almacen_procedencia_traslado = ar.id_almacen
                                    LEFT JOIN Bodega_Cafe br ON t.id_bodega_procedencia_traslado = br.id_bodega
                                    LEFT JOIN Almacen ad ON t.id_almacen_destino_traslado = ad.id_almacen
                                    LEFT JOIN Bodega_Cafe bd ON t.id_bodega_destino_traslado = bd.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_traslado = p.id_personal
                                    WHERE t.id_cosecha_traslado = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", iCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Traslado traslado = new Traslado()
                        {
                            Idtraslado_cafe = Convert.ToInt32(reader["id_traslado_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_traslado"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTraslado = Convert.ToInt32(reader["num_traslado"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_procedencia_traslado"]),
                            NombreProcedencia = Convert.IsDBNull(reader["ProcedenciaP"]) ? null : Convert.ToString(reader["ProcedenciaP"]),
                            IdProcedenciaDestino = Convert.IsDBNull(reader["id_procedencia_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_destino_traslado"]),
                            NombreProcedenciaDestino = Convert.IsDBNull(reader["ProcedenciaD"]) ? null : Convert.ToString(reader["ProcedenciaD"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_traslado"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_traslado"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacenProcedencia = Convert.IsDBNull(reader["id_almacen_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_procedencia_traslado"]),
                            NombreAlmacenProcedencia = Convert.IsDBNull(reader["AlmacenP"]) ? null : Convert.ToString(reader["AlmacenP"]),
                            IdAlmacenDestino = Convert.IsDBNull(reader["id_almacen_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_destino_traslado"]),
                            NombreAlmacenDestino = Convert.IsDBNull(reader["AlmacenD"]) ? null : Convert.ToString(reader["AlmacenD"]),
                            IdBodegaProcedencia = Convert.IsDBNull(reader["id_bodega_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_procedencia_traslado"]),
                            NombreBodegaProcedencia = Convert.IsDBNull(reader["BodegaP"]) ? null : Convert.ToString(reader["BodegaP"]),
                            IdBodegaDestino = Convert.IsDBNull(reader["id_bodega_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_destino_traslado"]),
                            NombreBodegaDestino = Convert.IsDBNull(reader["BodegaD"]) ? null : Convert.ToString(reader["BodegaD"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            FechaTrasladoCafe = Convert.ToDateTime(reader["fecha_trasladoCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_traslado"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTraslado = Convert.IsDBNull(reader["observacion_traslado"]) ? null : Convert.ToString(reader["observacion_traslado"])
                        };

                        listaTraslados.Add(traslado);
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

            return listaTraslados;
        }

        // Función para obtener todos los registros del Traslado en la base de datos
        public Traslado ObtenerTrasladoPorIDNombre(int idTraslado)
        {
            Traslado traslado = null;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT t.*,
                                            c.nombre_cosecha,
                                           pdr.nombre_procedencia AS ProcedenciaP,
                                           pdd.nombre_procedencia AS ProcedenciaD,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           ar.nombre_almacen AS AlmacenP,
                                           br.nombre_bodega AS BodegaP,
                                           ad.nombre_almacen AS AlmacenD,
                                           bd.nombre_bodega AS BodegaD,
                                           p.nombre_personal
                                    FROM Traslado_Cafe t
                                    INNER JOIN Cosecha c ON t.id_cosecha_traslado = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pdr ON t.id_procedencia_procedencia_traslado = pdr.id_procedencia
                                    LEFT JOIN Procedencia_Destino_Cafe pdd ON t.id_procedencia_destino_traslado = pdd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_traslado = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_traslado = sbp.id_subproducto
                                    LEFT JOIN Almacen ar ON t.id_almacen_procedencia_traslado = ar.id_almacen
                                    LEFT JOIN Bodega_Cafe br ON t.id_bodega_procedencia_traslado = br.id_bodega
                                    LEFT JOIN Almacen ad ON t.id_almacen_destino_traslado = ad.id_almacen
                                    LEFT JOIN Bodega_Cafe bd ON t.id_bodega_destino_traslado = bd.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_traslado = p.id_personal
                                    WHERE t.id_traslado_cafe = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idTraslado);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        traslado = new Traslado()
                        {
                            Idtraslado_cafe = Convert.ToInt32(reader["id_traslado_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_traslado"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTraslado = Convert.ToInt32(reader["num_traslado"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_procedencia_traslado"]),
                            NombreProcedencia = Convert.IsDBNull(reader["ProcedenciaP"]) ? null : Convert.ToString(reader["ProcedenciaP"]),
                            IdProcedenciaDestino = Convert.IsDBNull(reader["id_procedencia_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_destino_traslado"]),
                            NombreProcedenciaDestino = Convert.IsDBNull(reader["ProcedenciaD"]) ? null : Convert.ToString(reader["ProcedenciaD"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_traslado"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_traslado"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacenProcedencia = Convert.IsDBNull(reader["id_almacen_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_procedencia_traslado"]),
                            NombreAlmacenProcedencia = Convert.IsDBNull(reader["AlmacenP"]) ? null : Convert.ToString(reader["AlmacenP"]),
                            IdAlmacenDestino = Convert.IsDBNull(reader["id_almacen_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_destino_traslado"]),
                            NombreAlmacenDestino = Convert.IsDBNull(reader["AlmacenD"]) ? null : Convert.ToString(reader["AlmacenD"]),
                            IdBodegaProcedencia = Convert.IsDBNull(reader["id_bodega_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_procedencia_traslado"]),
                            NombreBodegaProcedencia = Convert.IsDBNull(reader["BodegaP"]) ? null : Convert.ToString(reader["BodegaP"]),
                            IdBodegaDestino = Convert.IsDBNull(reader["id_bodega_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_destino_traslado"]),
                            NombreBodegaDestino = Convert.IsDBNull(reader["BodegaD"]) ? null : Convert.ToString(reader["BodegaD"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            FechaTrasladoCafe = Convert.ToDateTime(reader["fecha_trasladoCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_traslado"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTraslado = Convert.IsDBNull(reader["observacion_traslado"]) ? null : Convert.ToString(reader["observacion_traslado"])
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

            return traslado;
        }

        // Función para obtener todos los registros del Traslado en la base de datos
        public Traslado ObtenerTrasladoPorCosechaIDNombre(int numTraslado, int iCosecha)
        {
            Traslado traslado = null;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT t.*,
                                            c.nombre_cosecha,
                                           pdr.nombre_procedencia AS ProcedenciaP,
                                           pdd.nombre_procedencia AS ProcedenciaD,
                                           cc.nombre_calidad,
                                           sbp.nombre_subproducto,
                                           ar.nombre_almacen AS AlmacenP,
                                           br.nombre_bodega AS BodegaP,
                                           ad.nombre_almacen AS AlmacenD,
                                           bd.nombre_bodega AS BodegaD,
                                           p.nombre_personal
                                    FROM Traslado_Cafe t
                                    INNER JOIN Cosecha c ON t.id_cosecha_traslado = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pdr ON t.id_procedencia_procedencia_traslado = pdr.id_procedencia
                                    LEFT JOIN Procedencia_Destino_Cafe pdd ON t.id_procedencia_destino_traslado = pdd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_traslado = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_traslado = sbp.id_subproducto
                                    LEFT JOIN Almacen ar ON t.id_almacen_procedencia_traslado = ar.id_almacen
                                    LEFT JOIN Bodega_Cafe br ON t.id_bodega_procedencia_traslado = br.id_bodega
                                    LEFT JOIN Almacen ad ON t.id_almacen_destino_traslado = ad.id_almacen
                                    LEFT JOIN Bodega_Cafe bd ON t.id_bodega_destino_traslado = bd.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_traslado = p.id_personal
                                    WHERE t.num_traslado = @Id AND t.id_cosecha_traslado = @iCosecha";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", numTraslado);
                conexion.AgregarParametro("@iCosecha", iCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        traslado = new Traslado()
                        {
                            Idtraslado_cafe = Convert.ToInt32(reader["id_traslado_cafe"]),
                            IdCosecha = Convert.ToInt32(reader["id_cosecha_traslado"]),
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTraslado = Convert.ToInt32(reader["num_traslado"]),
                            IdProcedencia = Convert.IsDBNull(reader["id_procedencia_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_procedencia_traslado"]),
                            NombreProcedencia = Convert.IsDBNull(reader["ProcedenciaP"]) ? null : Convert.ToString(reader["ProcedenciaP"]),
                            IdProcedenciaDestino = Convert.IsDBNull(reader["id_procedencia_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_procedencia_destino_traslado"]),
                            NombreProcedenciaDestino = Convert.IsDBNull(reader["ProcedenciaD"]) ? null : Convert.ToString(reader["ProcedenciaD"]),
                            IdCalidadCafe = Convert.ToInt32(reader["id_calidad_cafe_traslado"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_traslado"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            IdAlmacenProcedencia = Convert.IsDBNull(reader["id_almacen_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_procedencia_traslado"]),
                            NombreAlmacenProcedencia = Convert.IsDBNull(reader["AlmacenP"]) ? null : Convert.ToString(reader["AlmacenP"]),
                            IdAlmacenDestino = Convert.IsDBNull(reader["id_almacen_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_almacen_destino_traslado"]),
                            NombreAlmacenDestino = Convert.IsDBNull(reader["AlmacenD"]) ? null : Convert.ToString(reader["AlmacenD"]),
                            IdBodegaProcedencia = Convert.IsDBNull(reader["id_bodega_procedencia_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_procedencia_traslado"]),
                            NombreBodegaProcedencia = Convert.IsDBNull(reader["BodegaP"]) ? null : Convert.ToString(reader["BodegaP"]),
                            IdBodegaDestino = Convert.IsDBNull(reader["id_bodega_destino_traslado"]) ? 0 : Convert.ToInt32(reader["id_bodega_destino_traslado"]),
                            NombreBodegaDestino = Convert.IsDBNull(reader["BodegaD"]) ? null : Convert.ToString(reader["BodegaD"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            FechaTrasladoCafe = Convert.ToDateTime(reader["fecha_trasladoCafe"]),
                            IdPersonal = Convert.ToInt32(reader["id_personal_traslado"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTraslado = Convert.IsDBNull(reader["observacion_traslado"]) ? null : Convert.ToString(reader["observacion_traslado"])
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

            return traslado;
        }

        //
        public List<ReporteTraslado> ObtenerTrasladoReport (int idTraslado)
        {
            List<ReporteTraslado> data = new List<ReporteTraslado>();

            try
            {
                // Conexión a la base de datos (asegúrate de tener la clase "conexion" y los métodos correspondientes)
                conexion.Conectar();

                string consulta = @"SELECT 
		                                    c.nombre_cosecha,
		                                    t.num_traslado,
		                                    DATE_FORMAT(t.fecha_trasladoCafe, '%d/%m/%Y') AS Fecha,
                                            ar.nombre_almacen AS AlmacenP,
                                           br.nombre_bodega AS BodegaP,
                                           pdr.nombre_procedencia AS ProcedenciaP,
	                                       ad.nombre_almacen AS AlmacenD,
	                                       bd.nombre_bodega AS BodegaD,      
	                                       pdd.nombre_procedencia AS ProcedenciaD,
	                                       cc.nombre_calidad,
	                                       sbp.nombre_subproducto,
	                                       t.cantidad_traslado_sacos_cafe,
                                           t.cantidad_traslado_qqs_cafe,
	                                       p.nombre_personal,
                                           t.observacion_traslado
                                    FROM Traslado_Cafe t
                                    INNER JOIN Cosecha c ON t.id_cosecha_traslado = c.id_cosecha
                                    LEFT JOIN Procedencia_Destino_Cafe pdr ON t.id_procedencia_procedencia_traslado = pdr.id_procedencia
                                    LEFT JOIN Procedencia_Destino_Cafe pdd ON t.id_procedencia_destino_traslado = pdd.id_procedencia
                                    INNER JOIN Calidad_Cafe cc ON t.id_calidad_cafe_traslado = cc.id_calidad
                                    INNER JOIN SubProducto sbp ON t.id_subproducto_traslado = sbp.id_subproducto
                                    LEFT JOIN Almacen ar ON t.id_almacen_procedencia_traslado = ar.id_almacen
                                    LEFT JOIN Bodega_Cafe br ON t.id_bodega_procedencia_traslado = br.id_bodega
                                    LEFT JOIN Almacen ad ON t.id_almacen_destino_traslado = ad.id_almacen
                                    LEFT JOIN Bodega_Cafe bd ON t.id_bodega_destino_traslado = bd.id_bodega
                                    INNER JOIN Personal p ON t.id_personal_traslado = p.id_personal
                                    WHERE t.id_traslado_cafe = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idTraslado);

                MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta);
                {
                    while (reader.Read())
                    {
                        ReporteTraslado reporteTraslado = new ReporteTraslado()
                        {
                            NombreCosecha = Convert.ToString(reader["nombre_cosecha"]),
                            NumTraslado = Convert.ToInt32(reader["num_traslado"]),
                            FechaTrasladoCafe = Convert.ToString(reader["Fecha"]),
                            NombreAlmacenProcedencia = Convert.ToString(reader["AlmacenP"]),
                            NombreBodegaProcedencia = Convert.ToString(reader["BodegaP"]),
                            NombreProcedencia = Convert.ToString(reader["ProcedenciaP"]),
                            NombreAlmacenDestino = Convert.ToString(reader["AlmacenD"]),
                            NombreBodegaDestino = Convert.ToString(reader["BodegaD"]),
                            NombreProcedenciaDestino = Convert.ToString(reader["ProcedenciaD"]),
                            NombreCalidadCafe = Convert.ToString(reader["nombre_calidad"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"]),
                            CantidadTrasladoSacos = Convert.ToDouble(reader["cantidad_traslado_sacos_cafe"]),
                            CantidadTrasladoQQs = Convert.ToDouble(reader["cantidad_traslado_qqs_cafe"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ObservacionTraslado = Convert.ToString(reader["observacion_traslado"])
                        };
                        data.Add(reporteTraslado);
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
        public Traslado CountTraslado(int idCosecha)
        {
            Traslado traslado = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalTraslado FROM Traslado_Cafe
                                    WHERE id_cosecha_traslado = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idCosecha);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            traslado = new Traslado()
                            {
                                CountTraslado = Convert.ToInt32(reader["TotalTraslado"])
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
            return traslado;
        }

        //
        public bool VerificarExistenciaTraslado(int idCosecha, int numTraslado)
        {
            bool existeTraslado = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Consulta SQL con la cláusula WHERE para verificar la existencia de la Traslado_Cafe
                string consulta = @"SELECT COUNT(*) FROM Traslado_Cafe 
                            WHERE id_cosecha_traslado = @idCosecha AND num_traslado = @numTraslado";

                conexion.CrearComando(consulta);

                // Agregar los parámetros
                conexion.AgregarParametro("@idCosecha", idCosecha);
                conexion.AgregarParametro("@numTraslado", numTraslado);

                // Ejecutar la consulta y obtener el resultado
                int resultado = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                // Si el resultado es mayor que 0, significa que existen Traslado con los valores proporcionados
                existeTraslado = resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de los Traslado: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return existeTraslado;
        }

    }
}
