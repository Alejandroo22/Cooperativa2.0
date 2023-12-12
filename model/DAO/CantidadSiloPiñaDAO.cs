using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class CantidadSiloPiñaDAO
    {
        private ConnectionDB conexion;

        public CantidadSiloPiñaDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        public bool InsertarCantidadCafeSiloPiña(CantidadSiloPiña cantidad)
        {
            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea script SQL para insertar
                string consulta = @"INSERT INTO CantidadCafe_Silo_Piña (fecha_movimiento_cantidad_cafe, id_cosecha_cantidad, tipo_movimiento_cantidad_cafe, 
                                                                cantidad_qqs_cafe ,cantidad_saco_cafe ,id_almacen_silo_piña, id_subproducto_cafe)
                                    VALUES (@fecha, @idC, @tipo, @cantidad, @cantidadSaco, @idAlmacenSiloPiña, @idSubProd)";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@fecha", cantidad.FechaMovimiento);
                conexion.AgregarParametro("@idC", cantidad.IdCosechaCantidad);
                conexion.AgregarParametro("@tipo", cantidad.TipoMovimiento);
                conexion.AgregarParametro("@cantidad", cantidad.CantidadCafe);
                conexion.AgregarParametro("@cantidadSaco", cantidad.CantidadCafeSaco);
                conexion.AgregarParametro("@idAlmacenSiloPiña", cantidad.IdAlmacenSiloPiña);
                conexion.AgregarParametro("@idSubProd", cantidad.IdSubProducto);

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

        //funcion para mostrar todos los registros
        public List<CantidadSiloPiña> ObtenerCantidadesSiloPiña()
        {
            List<CantidadSiloPiña> listaCantidad = new List<CantidadSiloPiña>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM CantidadCafe_Silo_Piña";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        CantidadSiloPiña Almacens = new CantidadSiloPiña()
                        {
                            IdCantidadCafe = Convert.ToInt32(reader["id_cantidad_cafe"]),
                            FechaMovimiento = Convert.ToDateTime(reader["fecha_movimiento_cantidad_cafe"]),
                            CantidadCafe = Convert.ToDouble(reader["cantidad_qqs_cafe"]),
                            CantidadCafeSaco = Convert.ToDouble(reader["cantidad_saco_cafe"]),
                            TipoMovimiento = Convert.ToString(reader["tipo_movimiento_cantidad_cafe"]),
                            IdAlmacenSiloPiña = Convert.ToInt32(reader["id_almacen_silo_piña"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_cafe"])
                        };

                        listaCantidad.Add(Almacens);
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
            return listaCantidad;
        }

        //obtener la Almacen en especifico mediante el id en la BD
        public CantidadSiloPiña ObtenerIdCantidadSiloPiña(int idCantidad)
        {
            CantidadSiloPiña cantidad = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el historial de las cantidades de cafe
                string consulta = "SELECT * FROM CantidadCafe_Silo_Piña WHERE id_cantidad_cafe = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idCantidad);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cantidad = new CantidadSiloPiña()
                        {
                            IdCantidadCafe = Convert.ToInt32(reader["id_cantidad_cafe"]),
                            FechaMovimiento = Convert.ToDateTime(reader["fecha_movimiento_cantidad_cafe"]),
                            CantidadCafe = Convert.ToDouble(reader["cantidad_qqs_cafe"]),
                            CantidadCafeSaco = Convert.ToDouble(reader["cantidad_saco_cafe"]),
                            TipoMovimiento = Convert.ToString(reader["tipo_movimiento_cantidad_cafe"]),
                            IdAlmacenSiloPiña = Convert.ToInt32(reader["id_almacen_silo_piña"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_cafe"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Cantidad de los Silos/Piña: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return cantidad;
        }

        //obtener la Cantidad en especifico mediante el id del silo/Piña en la BD
        public CantidadSiloPiña ObtenerCantidadSiloPiña(string nombreSiloPiña)
        {
            CantidadSiloPiña cantidad = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener los registros 
                string consulta = @"SELECT c.id_cantidad_cafe, c.fecha_movimiento_cantidad_cafe,
                                  c.cantidad_qqs_cafe, c.cantidad_saco_cafe, c.tipo_movimiento_cantidad_cafe,
                                  c.id_almacen_silo_piña, a.nombre_almacen, c.id_subproducto_cafe, sp.nombre_subproducto
                            FROM CantidadCafe_Silo_Piña c
                            INNER JOIN Almacen a ON c.id_almacen_silo_piña = a.id_almacen
                            INNER JOIN SubProducto sp ON c.id_subproducto_cafe = sp.id_subproducto
                            WHERE a.nombre_almacen = @nombreA";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreA", nombreSiloPiña);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cantidad = new CantidadSiloPiña()
                        {
                            IdCantidadCafe = Convert.ToInt32(reader["id_cantidad_cafe"]),
                            FechaMovimiento = Convert.ToDateTime(reader["fecha_movimiento_cantidad_cafe"]),
                            CantidadCafe = Convert.ToDouble(reader["cantidad_qqs_cafe"]),
                            CantidadCafeSaco = Convert.ToDouble(reader["cantidad_saco_cafe"]),
                            TipoMovimiento = Convert.ToString(reader["tipo_movimiento_cantidad_cafe"]),
                            IdAlmacenSiloPiña = Convert.ToInt32(reader["id_almacen_silo_piña"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_cafe"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad el silo/piña: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return cantidad;
        }

        //obtener la Cantidad y el nombre almacen en la BD
        public List<CantidadSiloPiña> ObtenerCantidadNombreSiloPiña()
        {
            List<CantidadSiloPiña> listaCantidad = new List<CantidadSiloPiña>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener los registros
                string consulta = @"SELECT c.id_cantidad_cafe, c.fecha_movimiento_cantidad_cafe,
                                  c.cantidad_qqs_cafe, c.cantidad_saco_cafe, c.tipo_movimiento_cantidad_cafe,
                                  c.id_almacen_silo_piña, a.nombre_almacen, c.id_subproducto_cafe, sp.nombre_subproducto
                            FROM CantidadCafe_Silo_Piña c
                            INNER JOIN SubProducto sp ON c.id_subproducto_cafe = sp.id_subproducto
                            INNER JOIN Almacen a ON c.id_almacen_silo_piña = a.id_almacen";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        CantidadSiloPiña cantidad = new CantidadSiloPiña()
                        {
                            IdCantidadCafe = Convert.ToInt32(reader["id_cantidad_cafe"]),
                            FechaMovimiento = Convert.ToDateTime(reader["fecha_movimiento_cantidad_cafe"]),
                            CantidadCafe = Convert.ToDouble(reader["cantidad_qqs_cafe"]),
                            CantidadCafeSaco = Convert.ToDouble(reader["cantidad_saco_cafe"]),
                            TipoMovimiento = Convert.ToString(reader["tipo_movimiento_cantidad_cafe"]),
                            IdAlmacenSiloPiña = Convert.ToInt32(reader["id_almacen_silo_piña"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_cafe"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"])
                        };
                        listaCantidad.Add(cantidad);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad y almacen: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaCantidad;
        }

        //
        public List<CantidadSiloPiña> BuscarCantidadSiloPiña(string buscar)
        {
            List<CantidadSiloPiña> cantidads = new List<CantidadSiloPiña>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener los registros
                string consulta = @"SELECT c.id_cantidad_cafe, c.fecha_movimiento_cantidad_cafe,
                                  c.cantidad_qqs_cafe, c.cantidad_saco_cafe, c.tipo_movimiento_cantidad_cafe,
                                  c.id_almacen_silo_piña, a.nombre_almacen, c.id_subproducto_cafe, sp.nombre_subproducto
                            FROM CantidadCafe_Silo_Piña c
                            INNER JOIN Almacen a ON c.id_almacen_silo_piña = a.id_almacen
                            INNER JOIN SubProducto sp ON c.id_subproducto_cafe = sp.id_subproducto
                            WHERE a.nombre_almacen = @nombreA OR 
                                a.nombre_almacen LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        CantidadSiloPiña cantidad = new CantidadSiloPiña()
                        {
                            IdCantidadCafe = Convert.ToInt32(reader["id_cantidad_cafe"]),
                            FechaMovimiento = Convert.ToDateTime(reader["fecha_movimiento_cantidad_cafe"]),
                            CantidadCafe = Convert.ToDouble(reader["cantidad_qqs_cafe"]),
                            CantidadCafeSaco = Convert.ToDouble(reader["cantidad_saco_cafe"]),
                            TipoMovimiento = Convert.ToString(reader["tipo_movimiento_cantidad_cafe"]),
                            IdAlmacenSiloPiña = Convert.ToInt32(reader["id_almacen_silo_piña"]),
                            NombreAlmacen = Convert.ToString(reader["nombre_almacen"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_cafe"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"])
                        };

                        cantidads.Add(cantidad);
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
            return cantidads;
        }


        //
        public CantidadSiloPiña BuscarCantidadSiloPiñaSub(string buscar)
        {
            CantidadSiloPiña cantidads = null;

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener los registros
                string consulta = @"SELECT c.*, sp.nombre_subproducto
                                    FROM CantidadCafe_Silo_Piña c
                                    INNER JOIN SubProducto sp ON c.id_subproducto_cafe = sp.id_subproducto
                                    WHERE tipo_movimiento_cantidad_cafe LIKE CONCAT('%', @search)";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cantidads = new CantidadSiloPiña()
                        {
                            IdCantidadCafe = Convert.ToInt32(reader["id_cantidad_cafe"]),
                            IdCosechaCantidad = Convert.ToInt32(reader["id_cosecha_cantidad"]),
                            FechaMovimiento = Convert.ToDateTime(reader["fecha_movimiento_cantidad_cafe"]),
                            CantidadCafe = Convert.ToDouble(reader["cantidad_qqs_cafe"]),
                            CantidadCafeSaco = Convert.ToDouble(reader["cantidad_saco_cafe"]),
                            TipoMovimiento = Convert.ToString(reader["tipo_movimiento_cantidad_cafe"]),
                            IdAlmacenSiloPiña = Convert.ToInt32(reader["id_almacen_silo_piña"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_cafe"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"])
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
            return cantidads;
        }

        //
        //obtener la Cantidad de subproducto en especifico mediante el id del silo/Piña en la BD
        public CantidadSiloPiña ObtenerCantidadSubProductoSiloPiña(int iCosecha, int iAlmacen, int iSubProducto)
        {
            CantidadSiloPiña cantidad = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener los registros 
                string consulta = @"SELECT a.id_calidad_cafe, sp.nombre_subproducto, a.nombre_almacen, ccsp.id_subproducto_cafe,
                                        (
                                            COALESCE(SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Entrada%' THEN ccsp.cantidad_saco_cafe ELSE 0 END), 0) 
                                            + COALESCE((SELECT SUM(ccsp2.cantidad_saco_cafe)
                                                        FROM CantidadCafe_Silo_Piña ccsp2
                                                        WHERE ccsp2.id_almacen_silo_piña = @iAlmacen
                                                        AND ccsp2.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Destino%'), 0)
                                        )
                                        - COALESCE(SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Salida%' THEN ccsp.cantidad_saco_cafe ELSE 0 END), 0)
                                        - COALESCE(SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Procedencia%' THEN ccsp.cantidad_saco_cafe ELSE 0 END), 0) 
                                        AS existencias_cantidad_sacos,
                                        (
                                            COALESCE(SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Entrada%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END), 0) 
                                            + COALESCE((SELECT SUM(ccsp2.cantidad_qqs_cafe)
                                                        FROM CantidadCafe_Silo_Piña ccsp2
                                                        WHERE ccsp2.id_almacen_silo_piña = @iAlmacen
                                                        AND ccsp2.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Destino%'), 0)
                                        )
                                        - COALESCE(SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Salida%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END), 0)
                                        - COALESCE(SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Procedencia%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END) , 0) 
                                        AS existencias_cantidad_qqs
                                    FROM
                                        Almacen a
                                    JOIN
                                        Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad
                                    JOIN
                                        Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                                    LEFT JOIN
                                        CantidadCafe_Silo_Piña ccsp ON ccsp.id_almacen_silo_piña = a.id_almacen
                                    JOIN
                                        SubProducto sp ON ccsp.id_subproducto_cafe = sp.id_subproducto
                                    WHERE
                                        ccsp.id_cosecha_cantidad = @iCosecha and ccsp.id_subproducto_cafe =  @iSubProd
                                    GROUP BY
                                        a.id_calidad_cafe,
                                        sp.nombre_subproducto,
                                        a.nombre_almacen,
                                        a.id_almacen
                                    ORDER BY
                                        a.id_calidad_cafe,
                                        sp.id_subproducto,
                                        a.nombre_almacen";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@iCosecha", iCosecha);
                conexion.AgregarParametro("@iAlmacen", iAlmacen);
                conexion.AgregarParametro("@iSubProd", iSubProducto);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        cantidad = new CantidadSiloPiña()
                        {
                            CantidadCafe = Convert.ToDouble(reader["existencias_cantidad_qqs"]),
                            CantidadCafeSaco = Convert.ToDouble(reader["existencias_cantidad_sacos"]),
                            IdSubProducto = Convert.ToInt32(reader["id_subproducto_cafe"]),
                            NombreSubProducto = Convert.ToString(reader["nombre_subproducto"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad el silo/piña: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return cantidad;
        }

        //Funcion para actualizar los datos del historial 
        public bool ActualizarCantidadCafeSiloPiña(CantidadSiloPiña cantidad)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL 
                string consulta = @"UPDATE CantidadCafe_Silo_Piña 
                            SET fecha_movimiento_cantidad_cafe = @fecha, id_cosecha_cantidad = @idC, cantidad_qqs_cafe = @cantidad, 
                                cantidad_saco_cafe = @cantidadSaco, id_almacen_silo_piña = @idAlmacenSiloPiña, 
                                tipo_movimiento_cantidad_cafe = @tipo, id_subproducto_cafe = @idSubProd
                            WHERE id_cantidad_cafe = @idCantidadCafe";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@fecha", cantidad.FechaMovimiento);
                conexion.AgregarParametro("@idC", cantidad.IdCosechaCantidad);
                conexion.AgregarParametro("@cantidad", cantidad.CantidadCafe);
                conexion.AgregarParametro("@cantidadSaco", cantidad.CantidadCafeSaco);
                conexion.AgregarParametro("@idAlmacenSiloPiña", cantidad.IdAlmacenSiloPiña);
                conexion.AgregarParametro("@tipo", cantidad.TipoMovimiento);
                conexion.AgregarParametro("@idCantidadCafe", cantidad.IdCantidadCafe);
                conexion.AgregarParametro("@idSubProd", cantidad.IdSubProducto);

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
        public void EliminarCantidadSiloPiña(int idCantidad)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM CantidadCafe_Silo_Piña WHERE id_cantidad_cafe = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idCantidad);

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
                Console.WriteLine("Error al eliminar el registro" + ex.Message);
            }
            finally
            {
                //se cierra la conexion con la base de datos
                conexion.Desconectar();
            }
        }

        //obtener la Cantidad en especifico mediante el id del silo/Piña en la BD
        public List<CantidadSiloPiña> ObtenerListaSubProductosSiloPiña(int idAlmacen)
        {
            List<CantidadSiloPiña> resultados = new List<CantidadSiloPiña>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener los resultados
                string consulta = @"SELECT id_subproducto_cafe, MAX(nombre_subproducto) as nombre_subproducto
                                FROM CantidadCafe_Silo_Piña ccp
                                JOIN SubProducto sp ON ccp.id_subproducto_cafe = sp.id_subproducto
                                WHERE ccp.id_almacen_silo_piña = @idAlmacen
                                GROUP BY id_subproducto_cafe";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@idAlmacen", idAlmacen);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader != null && reader.Read())
                    {
                        CantidadSiloPiña resultado = new CantidadSiloPiña()
                        {
                            IdSubProducto = reader["id_subproducto_cafe"] is DBNull ? 0 : Convert.ToInt32(reader["id_subproducto_cafe"]),
                            NombreSubProducto = reader["nombre_subproducto"] is DBNull ? string.Empty : Convert.ToString(reader["nombre_subproducto"])
                        };

                        resultados.Add(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de subproductos en el silo/piña: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return resultados;
        }

        public bool VerificarRegistroExistente(int idAlmacen, int selectedSubproducto)
        {
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar registros existentes
                string consulta = "SELECT COUNT(*) FROM CantidadCafe_Silo_Piña WHERE id_almacen_silo_piña = @idAlmacen";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@idAlmacen", idAlmacen);

                // Ejecutar la consulta y obtener el resultado
                int cantidadRegistros = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                // Verificar las condiciones
                if (cantidadRegistros == 0)
                {
                    // No hay registros, puedes registrar uno nuevo
                    return true;
                }
                else
                {
                    // Hay registros, puedes verificar si cumplen con las condiciones
                    // Por ejemplo, verificar si el subproducto es el mismo
                    string consultaSubproducto = "SELECT DISTINCT id_subproducto_cafe FROM CantidadCafe_Silo_Piña WHERE id_almacen_silo_piña = @idAlmacen";
                    conexion.CrearComando(consultaSubproducto);
                    conexion.AgregarParametro("@idAlmacen", idAlmacen);

                    object subproductoExistente = conexion.EjecutarConsultaEscalar();

                    if (subproductoExistente != null && subproductoExistente != DBNull.Value)
                    {
                        int subproductoExistenteId = Convert.ToInt32(subproductoExistente);

                        if (subproductoExistenteId == selectedSubproducto)
                        {
                            // El subproducto es el mismo, puedes seguir
                            return true;
                        }
                        else
                        {
                            // El subproducto es diferente, muestra un mensaje de error
                            Console.WriteLine("El subproducto del almacen ya contiene registros y es diferente al seleccionado. Error");
                            return false;
                        }
                    }
                    else
                    {
                        // No hay registros de subproducto, puedes manejar esto según tus necesidades
                        Console.WriteLine("No hay registros de subproducto en el almacen. Información");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar registros existentes: " + ex.Message);
                return false;
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
        }

    }
}
