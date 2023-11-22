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
    class ProcedenciaDestinoDAO
    {
        private ConnectionDB conexion;

        public ProcedenciaDestinoDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        // Función para insertar un nuevo registro en la tabla Procedencia_Destino_Cafe
        public bool InsertarProcedenciaDestino(ProcedenciaDestino procedenciaDestino)
        {
            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para insertar
                string consulta = @"INSERT INTO Procedencia_Destino_Cafe SET
                                        id_procedencia = @iProce,
                                        nombre_procedencia = @nombreProcedencia,
                                        descripcion_procedencia = @descripcionProcedencia,";
                                            if (procedenciaDestino.IdBenficioUbicacion != 0){consulta += "id_benficio_ubicacion_procedencia = @idBeneficioUbicacion,";}
                                            if (procedenciaDestino.IdSocioProcedencia != 0){consulta += "id_socio_procedencia = @idSocio,";}
                                            if (procedenciaDestino.IdMaquinaria != 0){consulta += "id_maquinaria_procedencia = @idMaquinaria";}
                                            
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@iProce", procedenciaDestino.IdProcedencia);
                conexion.AgregarParametro("@nombreProcedencia", procedenciaDestino.NombreProcedencia);
                conexion.AgregarParametro("@descripcionProcedencia", procedenciaDestino.DescripcionProcedencia);
                conexion.AgregarParametro("@idBeneficioUbicacion", procedenciaDestino.IdBenficioUbicacion);
                conexion.AgregarParametro("@idSocio", procedenciaDestino.IdSocioProcedencia);
                conexion.AgregarParametro("@idMaquinaria", procedenciaDestino.IdMaquinaria);

                int filasAfectadas = conexion.EjecutarInstruccion();

                // Si se afecta una fila, se insertó correctamente
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de la Procedencia/Destino en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        // Función para actualizar un registro en la tabla Procedencia_Destino_Cafe
        public bool ActualizarProcedenciaDestino(ProcedenciaDestino procedenciaDestino)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para actualizar
                string consulta = @"UPDATE Procedencia_Destino_Cafe 
                            SET nombre_procedencia = @nombreProcedencia,
                                descripcion_procedencia = @descripcionProcedencia,";
                            if (procedenciaDestino.IdBenficioUbicacion != 0){consulta += "id_benficio_ubicacion_procedencia = @idBeneficioUbicacion,";}
                            if (procedenciaDestino.IdSocioProcedencia != 0){consulta += "id_socio_procedencia = @idSocio,";}
                            if (procedenciaDestino.IdMaquinaria != 0){consulta += "id_maquinaria_procedencia = @idMaquinaria";}
                            consulta += @" WHERE id_procedencia = @id";
                            
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombreProcedencia", procedenciaDestino.NombreProcedencia);
                conexion.AgregarParametro("@descripcionProcedencia", procedenciaDestino.DescripcionProcedencia);
                conexion.AgregarParametro("@idBeneficioUbicacion", procedenciaDestino.IdBenficioUbicacion);
                conexion.AgregarParametro("@idSocio", procedenciaDestino.IdSocioProcedencia);
                conexion.AgregarParametro("@idMaquinaria", procedenciaDestino.IdMaquinaria);
                conexion.AgregarParametro("@id", procedenciaDestino.IdProcedencia);

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

        // Función para eliminar un registro de la tabla Procedencia_Destino_Cafe
        public bool EliminarProcedenciaDestino(int idProcedencia)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para eliminar
                string consulta = @"DELETE FROM Procedencia_Destino_Cafe 
                            WHERE id_procedencia = @id";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", idProcedencia);

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

        // Función para obtener todos los registros de la tabla Procedencia_Destino_Cafe
        public List<ProcedenciaDestino> ObtenerProcedenciasDestino()
        {
            List<ProcedenciaDestino> listaProcedenciasDestino = new List<ProcedenciaDestino>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = "SELECT * FROM Procedencia_Destino_Cafe";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        ProcedenciaDestino procedenciaDestino = new ProcedenciaDestino()
                        {
                            IdProcedencia = Convert.ToInt32(reader["id_procedencia"]),
                            NombreProcedencia = Convert.ToString(reader["nombre_procedencia"]),
                            DescripcionProcedencia = Convert.ToString(reader["descripcion_procedencia"]),
                            IdBenficioUbicacion = (reader["id_benficio_ubicacion_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_benficio_ubicacion_procedencia"]),
                            IdSocioProcedencia = (reader["id_socio_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_socio_procedencia"]),
                            IdMaquinaria = (reader["id_maquinaria_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_maquinaria_procedencia"])
                        };

                        listaProcedenciasDestino.Add(procedenciaDestino);
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

            return listaProcedenciasDestino;
        }

        // Función para obtener todos los registros de la tabla Procedencia_Destino_Cafe
        public List<ProcedenciaDestino> ObtenerProcedenciasDestinoNombres()
        {
            List<ProcedenciaDestino> listaProcedenciasDestino = new List<ProcedenciaDestino>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT pd.*,
                                       b.nombre_beneficio,
                                       s.nombre_socio,
                                       f.nombre_finca,
                                       m.nombre_maquinaria
                                FROM Procedencia_Destino_Cafe pd
                                LEFT JOIN Beneficio b ON pd.id_benficio_ubicacion_procedencia = b.id_beneficio
                                LEFT JOIN Socio s ON pd.id_socio_procedencia = s.id_socio
                                LEFT JOIN Finca f ON s.id_finca_socio = f.id_finca
                                LEFT JOIN Maquinaria m ON pd.id_maquinaria_procedencia = m.id_maquinaria";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        ProcedenciaDestino procedenciaDestino = new ProcedenciaDestino()
                        {
                            IdProcedencia = Convert.ToInt32(reader["id_procedencia"]),
                            NombreProcedencia = Convert.ToString(reader["nombre_procedencia"]),
                            DescripcionProcedencia = Convert.ToString(reader["descripcion_procedencia"]),
                            NombreBenficioUbicacion = (reader["nombre_beneficio"]) is DBNull ? "" : Convert.ToString(reader["nombre_beneficio"]),
                            NombreSocioProcedencia = (reader["nombre_socio"]) is DBNull ? "" : Convert.ToString(reader["nombre_socio"]),
                            NombreFincaSocio = (reader["nombre_finca"]) is DBNull ? "" : Convert.ToString(reader["nombre_finca"]),
                            NombreMaquinaria = (reader["nombre_maquinaria"]) is DBNull ? "" : Convert.ToString(reader["nombre_maquinaria"]),
                            IdBenficioUbicacion = (reader["id_benficio_ubicacion_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_benficio_ubicacion_procedencia"]),
                            IdSocioProcedencia = (reader["id_socio_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_socio_procedencia"]),
                            IdMaquinaria = (reader["id_maquinaria_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_maquinaria_procedencia"])
                        };

                        listaProcedenciasDestino.Add(procedenciaDestino);
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

            return listaProcedenciasDestino;
        }

        // Función para obtener un registro de la tabla Procedencia_Destino_Cafe por ID
        public ProcedenciaDestino ObtenerProcedenciaDestinoPorId(int idProcedencia)
        {
            ProcedenciaDestino procedenciaDestino = null;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT pd.*,
                                           b.nombre_beneficio AS nombre_benficio_ubicacion,
                                           s.nombre_socio AS nombre_socio_procedencia,
                                           m.nombre_maquinaria AS nombre_maquinaria_procedencia
                                    FROM Procedencia_Destino_Cafe pd
                                    INNER JOIN Beneficio b ON pd.id_benficio_ubicacion_procedencia = b.id_beneficio
                                    INNER JOIN Socio s ON pd.id_socio_procedencia = s.id_socio
                                    INNER JOIN Maquinaria m ON pd.id_maquinaria_procedencia = m.id_maquinaria
                                    WHERE pd.id_procedencia = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idProcedencia);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        procedenciaDestino = new ProcedenciaDestino()
                        {
                            IdProcedencia = Convert.ToInt32(reader["id_procedencia"]),
                            NombreProcedencia = Convert.ToString(reader["nombre_procedencia"]),
                            DescripcionProcedencia = Convert.ToString(reader["descripcion_procedencia"]),
                            NombreBenficioUbicacion = (reader["nombre_benficio_ubicacion"]) is DBNull ? "" : Convert.ToString(reader["nombre_benficio_ubicacion"]),
                            NombreSocioProcedencia = (reader["nombre_socio_procedencia"]) is DBNull ? "" : Convert.ToString(reader["nombre_socio_procedencia"]),
                            NombreMaquinaria = (reader["nombre_maquinaria_procedencia"]) is DBNull ? "" : Convert.ToString(reader["nombre_maquinaria_procedencia"]),
                            
                        };
                        if (reader["id_socio_procedencia"] == DBNull.Value)
                        {
                            procedenciaDestino.IdSocioProcedencia = 0; // o cualquier valor predeterminado que desees
                        }
                        else
                        {
                            procedenciaDestino.IdSocioProcedencia = Convert.ToInt32(reader["id_socio_procedencia"]);
                        }
                        if (reader["id_maquinaria_procedencia"] == DBNull.Value)
                        {
                            procedenciaDestino.IdMaquinaria = 0; // o cualquier valor predeterminado que desees
                        }
                        else
                        {
                            procedenciaDestino.IdSocioProcedencia = Convert.ToInt32(reader["id_maquinaria_procedencia"]);
                        }
                        if (reader["id_benficio_ubicacion_procedencia"] == DBNull.Value)
                        {
                            procedenciaDestino.IdSocioProcedencia = 0; // o cualquier valor predeterminado que desees
                        }
                        else
                        {
                            procedenciaDestino.IdBenficioUbicacion = Convert.ToInt32(reader["id_benficio_ubicacion_procedencia"]);
                        }
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

            return procedenciaDestino;
        }

        //
        public List<ProcedenciaDestino> BuscarProcedenciaDestino(string buscar)
        {
            List<ProcedenciaDestino> procedencias = new List<ProcedenciaDestino>();

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para buscar procedencias
                string consulta = @"SELECT pd.*,
                                           b.nombre_beneficio AS nombre_benficio_ubicacion,
                                           s.nombre_socio AS nombre_socio_procedencia,
                                           m.nombre_maquinaria AS nombre_maquinaria_procedencia
                                    FROM Procedencia_Destino_Cafe pd
                                    INNER JOIN Beneficio b ON pd.id_benficio_ubicacion_procedencia = b.id_beneficio
                                    INNER JOIN Socio s ON pd.id_socio_procedencia = s.id_socio
                                    INNER JOIN Maquinaria m ON pd.id_maquinaria_procedencia = m.id_maquinaria
                                    WHERE pd.nombre_procedencia LIKE CONCAT('%', @search, '%')
                                       OR b.nombre_beneficio LIKE CONCAT('%', @search, '%')
                                       OR s.nombre_socio LIKE CONCAT('%', @search, '%')
                                       OR m.nombre_maquinaria LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", buscar);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        ProcedenciaDestino procedencia = new ProcedenciaDestino()
                        {
                            IdProcedencia = Convert.ToInt32(reader["id_procedencia"]),
                            NombreProcedencia = Convert.ToString(reader["nombre_procedencia"]),
                            DescripcionProcedencia = Convert.ToString(reader["descripcion_procedencia"]),
                            NombreBenficioUbicacion = (reader["nombre_benficio_ubicacion"]) is DBNull ? "" : Convert.ToString(reader["nombre_benficio_ubicacion"]),
                            NombreSocioProcedencia = (reader["nombre_socio_procedencia"]) is DBNull ? "" : Convert.ToString(reader["nombre_socio_procedencia"]),
                            NombreMaquinaria = (reader["nombre_maquinaria_procedencia"]) is DBNull ? "" : Convert.ToString(reader["nombre_maquinaria_procedencia"]),
                            IdBenficioUbicacion = (reader["id_benficio_ubicacion_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_benficio_ubicacion_procedencia"]),
                            IdSocioProcedencia = (reader["id_socio_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_socio_procedencia"]),
                            IdMaquinaria = (reader["id_maquinaria_procedencia"]) is DBNull ? 0 : Convert.ToInt32(reader["id_maquinaria_procedencia"])
                        };

                        procedencias.Add(procedencia);
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

            return procedencias;
        }

        //
        public ProcedenciaDestino CountProcedencia()
        {
            ProcedenciaDestino proce = null;
            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalProcedencia FROM Procedencia_Destino_Cafe";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            proce = new ProcedenciaDestino()
                            {
                                CountProcedencia = Convert.ToInt32(reader["TotalProcedencia"])
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
            return proce;
        }

        public ProcedenciaDestino ObtenerUltimoId()
        {
            ProcedenciaDestino pd = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_procedencia) AS LastId FROM Procedencia_Destino_Cafe";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        pd = new ProcedenciaDestino()
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
            return pd;
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para verificar si existe una bodega con el mismo nombre
                string consulta = "SELECT COUNT(*) FROM Procedencia_Destino_Cafe WHERE id_procedencia = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de la Procedencia_Destino_Cafe: " + ex.Message);
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
