using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Acces;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class FallasDAO
    {
        private ConnectionDB conexion;

        public FallasDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarFalla(Fallas falla)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Falla ( descripcion_falla, pieza_reemplazada_falla, fecha_falla,  acciones_tomadas_falla, observaciones_falla, id_maquinaria_falla)
                                    VALUES ( @descripcion, @pieza, @fecha, @acciones, @observacion, @iMaquinaria)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@descripcion", falla.DescripcionFalla);
                conexion.AgregarParametro("@pieza", falla.PiezaReemplazada);
                conexion.AgregarParametro("@fecha", falla.FechaFalla);
                conexion.AgregarParametro("@acciones", falla.AccionesTomadas);
                conexion.AgregarParametro("@observacion", falla.ObservacionFalla);
                conexion.AgregarParametro("@iMaquinaria", falla.IdMaquinaria);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción de la Falla en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<Fallas> ObtenerFallas()
        {
            List<Fallas> listaFalla = new List<Fallas>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Falla";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Fallas fallas = new Fallas()
                        {
                            IdFalla = Convert.ToInt32(reader["id_falla"]),
                            DescripcionFalla = Convert.ToString(reader["descripcion_falla"]),
                            FechaFalla = Convert.ToDateTime(reader["fecha_falla"]),
                            AccionesTomadas = Convert.ToString(reader["acciones_tomadas_falla"]),
                            PiezaReemplazada = (reader["pieza_reemplazada_falla"]) is DBNull ? "" : Convert.ToString(reader["pieza_reemplazada_falla"]),
                            ObservacionFalla = (reader["observaciones_falla"]) is DBNull ? "" : Convert.ToString(reader["observaciones_falla"]),
                            IdMaquinaria = Convert.ToInt32(reader["id_maquinaria_falla"])
                        };

                        listaFalla.Add(fallas);
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
            return listaFalla;
        }

        //obtener la Falla en especifico mediante el id en la BD
        public Fallas ObtenerIdFalla(int idfalla)
        {
            Fallas falla = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT * FROM Falla WHERE id_falla = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idfalla);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        falla = new Fallas()
                        {
                            IdFalla = Convert.ToInt32(reader["id_falla"]),
                            DescripcionFalla = Convert.ToString(reader["descripcion_falla"]),
                            PiezaReemplazada = (reader["pieza_reemplazada_falla"]) is DBNull ? "" : Convert.ToString(reader["pieza_reemplazada_falla"]),
                            ObservacionFalla = (reader["observaciones_falla"]) is DBNull ? "" : Convert.ToString(reader["observaciones_falla"]),
                            FechaFalla = Convert.ToDateTime(reader["fecha_falla"]),
                            AccionesTomadas = Convert.ToString(reader["acciones_tomadas_falla"]),
                            IdMaquinaria = Convert.ToInt32(reader["id_maquinaria_falla"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el falla: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return falla;
        }

        //obtener la Falla en especifico mediante el id de la maquinaria en la BD
        public Fallas ObtenerFallaNombreMaquinaria(string nombreMaquinaria)
        {
            Fallas falla = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT f.id_falla, f.descripcion_falla, f.pieza_reemplazada_falla, f.fecha_falla, f.acciones_tomadas_falla, 
                                            f.observaciones_falla, f.id_maquinaria_falla, m.nombre_maquinaria
                                        FROM Falla f
                                        INNER JOIN Maquinaria m ON f.id_maquinaria_falla = m.id_maquinaria
                                        WHERE m.nombre_maquinaria = @nombreM";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreM", nombreMaquinaria);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        falla = new Fallas()
                        {
                            IdFalla = Convert.ToInt32(reader["id_falla"]),
                            DescripcionFalla = Convert.ToString(reader["descripcion_falla"]),
                            PiezaReemplazada = (reader["pieza_reemplazada_falla"]) is DBNull ? "" : Convert.ToString(reader["pieza_reemplazada_falla"]),
                            ObservacionFalla = (reader["observaciones_falla"]) is DBNull ? "" : Convert.ToString(reader["observaciones_falla"]),
                            FechaFalla = Convert.ToDateTime(reader["fecha_falla"]),
                            AccionesTomadas = Convert.ToString(reader["acciones_tomadas_falla"]),
                            IdMaquinaria = Convert.ToInt32(reader["id_maquinaria_falla"]),
                            NombreMaquinaria = Convert.ToString(reader["nombre_maquinaria"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el falla: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return falla;
        }

        //se obtiene el nombre de la maquina
        public List<Maquinaria> ObtenerMaquina()
        {
            List<Maquinaria> listaMaquina = new List<Maquinaria>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM maquinaria";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Maquinaria maquina = new Maquinaria()
                        {
                            IdMaquinaria = Convert.ToInt32(reader["id_maquinaria"]),
                            NombreMaquinaria = Convert.ToString(reader["nombre_maquinaria"]),
                            NumeroSerieMaquinaria = Convert.ToString(reader["numero_serie_maquinaria"]),
                            ModeloMaquinaria = Convert.ToString(reader["modelo_maquinaria"]),
                            CapacidadMaxMaquinaria = (reader["capacidad_max_maquinaria"] is DBNull ? 0 : Convert.ToDouble(reader["capacidad_max_maquinaria"])),
                            ProveedorMaquinaria = Convert.ToString(reader["proveedor_maquinaria"]),
                            DireccionProveedorMaquinaria = Convert.ToString(reader["direccion_proveedor_maquinaria"]),
                            TelefonoProveedorMaquinaria = Convert.ToString(reader["telefono_proveedor_maquinaria"]),
                            ContratoServicioMaquinaria = Convert.ToString(reader["contrato_servicio_maquinaria"]),
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio_maquinaria"])
                        };

                        listaMaquina.Add(maquina);
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
            return listaMaquina;
        }

        //obtener la Falla y el nombre maquinaria en la BD
        public List<Fallas> ObtenerFallaNombreMaquinaria()
        {
            List<Fallas> listaFalla = new List<Fallas>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT f.id_falla, f.descripcion_falla, f.pieza_reemplazada_falla, f.fecha_falla, f.acciones_tomadas_falla, 
                                            f.observaciones_falla, f.id_maquinaria_falla, m.nombre_maquinaria
                                        FROM Falla f
                                        INNER JOIN Maquinaria m ON f.id_maquinaria_falla = m.id_maquinaria";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Fallas falla = new Fallas()
                        {
                            IdFalla = Convert.ToInt32(reader["id_falla"]),
                            DescripcionFalla = Convert.ToString(reader["descripcion_falla"]),
                            PiezaReemplazada = (reader["pieza_reemplazada_falla"]) is DBNull ? "" : Convert.ToString(reader["pieza_reemplazada_falla"]),
                            ObservacionFalla = (reader["observaciones_falla"]) is DBNull ? "" : Convert.ToString(reader["observaciones_falla"]),
                            FechaFalla = Convert.ToDateTime(reader["fecha_falla"]),
                            AccionesTomadas = Convert.ToString(reader["acciones_tomadas_falla"]),
                            IdMaquinaria = Convert.ToInt32(reader["id_maquinaria_falla"]),
                            NombreMaquinaria = Convert.ToString(reader["nombre_maquinaria"])
                        };
                        listaFalla.Add(falla);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el falla: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaFalla;
        }

        //
        public List<Fallas> BuscarFalla(string buscar)
        {
            List<Fallas> fallas = new List<Fallas>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT f.id_falla, f.descripcion_falla, f.pieza_reemplazada_falla, f.fecha_falla, f.acciones_tomadas_falla, 
                                                f.observaciones_falla, f.id_maquinaria_falla, m.nombre_maquinaria
                                            FROM Falla f
                                            INNER JOIN Maquinaria m ON f.id_maquinaria_falla = m.id_maquinaria
                                            WHERE m.nombre_maquinaria LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Fallas fallaef = new Fallas()
                        {
                            IdFalla = Convert.ToInt32(reader["id_falla"]),
                            DescripcionFalla = Convert.ToString(reader["descripcion_falla"]),
                            FechaFalla = Convert.ToDateTime(reader["fecha_falla"]),
                            AccionesTomadas = Convert.ToString(reader["acciones_tomadas_falla"]),
                            PiezaReemplazada = (reader["pieza_reemplazada_falla"]) is DBNull ? "" : Convert.ToString(reader["pieza_reemplazada_falla"]),
                            ObservacionFalla = (reader["observaciones_falla"]) is DBNull ? "" : Convert.ToString(reader["observaciones_falla"]),
                            IdMaquinaria = Convert.ToInt32(reader["id_maquinaria_falla"]),
                            NombreMaquinaria = Convert.ToString(reader["nombre_maquinaria"])
                        };

                        fallas.Add(fallaef);
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
            return fallas;
        }

        //funcion para actualizar un registro en la base de datos
        public bool Actualizarfalla(int idfalla, string descripcion, string pieza, DateTime fecha, string accion, string observacion, int idMaquinaria)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Falla SET descripcion_falla = @descrip, pieza_reemplazada_falla = @pieza, fecha_falla = @fechaF, acciones_tomadas_falla = @accionT, 
                                       observaciones_falla = @observacion, id_maquinaria_falla = @iMaquinaria
                                    WHERE id_falla = @id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@descrip", descripcion);
                conexion.AgregarParametro("@pieza", pieza);
                conexion.AgregarParametro("@fechaF", fecha);
                conexion.AgregarParametro("@accionT", accion);
                conexion.AgregarParametro("@observacion", observacion);
                conexion.AgregarParametro("@iMaquinaria", idMaquinaria);
                conexion.AgregarParametro("@id", idfalla);

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
        public void EliminarFalla(int idfalla)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Falla WHERE id_falla = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idfalla);

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

    }
}
