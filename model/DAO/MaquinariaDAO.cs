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
    class MaquinariaDAO
    {
        private ConnectionDB conexion;

        public MaquinariaDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarMaquinaria(Maquinaria maquinaria)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                // Se crea el script SQL para insertar el registro en la tabla Maquinaria:
                string consulta = @"INSERT INTO Maquinaria (id_maquinaria,nombre_maquinaria, numero_serie_maquinaria, modelo_maquinaria, 
                                           capacidad_max_maquinaria, proveedor_maquinaria, direccion_proveedor_maquinaria, 
                                           telefono_proveedor_maquinaria, contrato_servicio_maquinaria, id_beneficio_maquinaria)
                                    VALUES (@id, @NombreMaquinaria, @NumeroSerieMaquinaria, @ModeloMaquinaria, 
                                            @CapacidadMaxima, @ProveedorMaquinaria, @DireccionProveedorMaquinaria, @TelefonoProveedorMaquinaria, 
                                            @ContratoServicioMaquinaria, @IdBeneficioMaquinaria)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@id", maquinaria.IdMaquinaria);
                conexion.AgregarParametro("@NombreMaquinaria", maquinaria.NombreMaquinaria);
                conexion.AgregarParametro("@NumeroSerieMaquinaria", maquinaria.NumeroSerieMaquinaria);
                conexion.AgregarParametro("@ModeloMaquinaria", maquinaria.ModeloMaquinaria);
                conexion.AgregarParametro("@CapacidadMaxima", maquinaria.CapacidadMaxMaquinaria);
                conexion.AgregarParametro("@ProveedorMaquinaria", maquinaria.ProveedorMaquinaria);
                conexion.AgregarParametro("@DireccionProveedorMaquinaria", maquinaria.DireccionProveedorMaquinaria);
                conexion.AgregarParametro("@TelefonoProveedorMaquinaria", maquinaria.TelefonoProveedorMaquinaria);
                conexion.AgregarParametro("@ContratoServicioMaquinaria", maquinaria.ContratoServicioMaquinaria);
                conexion.AgregarParametro("@IdBeneficioMaquinaria", maquinaria.IdBeneficio);


                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción de la maquinaria en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<Maquinaria> ObtenerMaquinarias()
        {
            List<Maquinaria> listaMaquinaria = new List<Maquinaria>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Maquinaria";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Maquinaria maquinarias = new Maquinaria()
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

                        listaMaquinaria.Add(maquinarias);
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
            return listaMaquinaria;
        }

        //obtener la maquinaria en especifico mediante el id en la BD
        public Maquinaria ObtenerIdMaquinaria(int idMaquinaria)
        {
            Maquinaria maquinaria = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT * FROM Maquinaria WHERE id_maquinaria = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", idMaquinaria);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        maquinaria = new Maquinaria()
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
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el maquinaria: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return maquinaria;
        }

        //obtener la maquinaria en especifico mediante el id de la maquinaria en la BD
        public Maquinaria ObtenerMaquinariaNombre(string nombreMaquinaria)
        {
            Maquinaria maquinaria = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT m.id_maquinaria, m.nombre_maquinaria, m.numero_serie_maquinaria, m.modelo_maquinaria,
                                           m.capacidad_max_maquinaria, m.proveedor_maquinaria, m.direccion_proveedor_maquinaria,
                                           m.telefono_proveedor_maquinaria, m.contrato_servicio_maquinaria, m.id_beneficio_maquinaria,
                                           b.nombre_beneficio
                                    FROM Maquinaria m
                                    INNER JOIN Beneficio b ON m.id_beneficio_maquinaria = b.id_beneficio
                                    WHERE m.nombre_maquinaria = @nombreM";


                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreM", nombreMaquinaria);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        maquinaria = new Maquinaria()
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
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio_maquinaria"]),
                            NombreBeneficio = Convert.ToString(reader["nombre_beneficio"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Maquinaria: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return maquinaria;
        }

        //obtener la Maquinaria y el nombre maquinaria en la BD
        public List<Maquinaria> ObtenerMaquinariaNombreBeneficio()
        {
            List<Maquinaria> listaMaquinaria = new List<Maquinaria>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT m.id_maquinaria, m.nombre_maquinaria, m.numero_serie_maquinaria, m.modelo_maquinaria,
                                           m.capacidad_max_maquinaria, m.proveedor_maquinaria, m.direccion_proveedor_maquinaria,
                                           m.telefono_proveedor_maquinaria, m.contrato_servicio_maquinaria, m.id_beneficio_maquinaria,
                                           b.nombre_beneficio
                                    FROM Maquinaria m
                                    INNER JOIN Beneficio b ON m.id_beneficio_maquinaria = b.id_beneficio";

                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Maquinaria Maquinaria = new Maquinaria()
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
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio_maquinaria"]),
                            NombreBeneficio = Convert.ToString(reader["nombre_beneficio"])
                        };
                        listaMaquinaria.Add(Maquinaria);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Maquinaria: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return listaMaquinaria;
        }

        //
        public List<Maquinaria> BuscarMaquinaria(string buscar)
        {
            List<Maquinaria> Maquinarias = new List<Maquinaria>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = @"SELECT m.id_maquinaria, m.nombre_maquinaria, m.numero_serie_maquinaria, m.modelo_maquinaria,
                                           m.capacidad_max_maquinaria, m.proveedor_maquinaria, m.direccion_proveedor_maquinaria,
                                           m.telefono_proveedor_maquinaria, m.contrato_servicio_maquinaria, m.id_beneficio_maquinaria,
                                           b.nombre_beneficio
                                    FROM Maquinaria m
                                    INNER JOIN Beneficio b ON m.id_beneficio_maquinaria = b.id_beneficio
                                    WHERE m.nombre_maquinaria LIKE CONCAT('%', @search, '%') OR b.nombre_beneficio LIKE CONCAT('%', @search, '%')
                                            OR m.numero_serie_maquinaria LIKE CONCAT('%', @search, '%') OR m.modelo_maquinaria LIKE CONCAT('%', @search, '%')";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", "%" + buscar + "%");

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Maquinaria Maquinaria = new Maquinaria()
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
                            IdBeneficio = Convert.ToInt32(reader["id_beneficio_maquinaria"]),
                            NombreBeneficio = Convert.ToString(reader["nombre_beneficio"])
                        };

                        Maquinarias.Add(Maquinaria);
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
            return Maquinarias;
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarMaquinaria(int idMaquinaria, string nombreMaquinaria, string numeroSerieMaquinaria,
                                 string modeloMaquinaria, double capacidadMax, string proveedorMaquinaria,
                                 string direccionProveedorMaquinaria, string telefonoProveedorMaquinaria,
                                 string contratoServicioMaquinaria, int idBeneficioMaquinaria)
        {
            bool exito = false;

            try
            {
                // Conexión a la base de datos
                conexion.Conectar();

                // Se crea el script SQL 
                string consulta = @"UPDATE Maquinaria SET nombre_maquinaria = @nombreMaquinaria,
                                                numero_serie_maquinaria = @numeroSerieMaquinaria,
                                                modelo_maquinaria = @modeloMaquinaria,
                                                capacidad_max_maquinaria = @capacidad,
                                                proveedor_maquinaria = @proveedorMaquinaria,
                                                direccion_proveedor_maquinaria = @direccionProveedorMaquinaria,
                                                telefono_proveedor_maquinaria = @telefonoProveedorMaquinaria,
                                                contrato_servicio_maquinaria = @contratoServicioMaquinaria,
                                                id_beneficio_maquinaria = @idBeneficioMaquinaria
                            WHERE id_maquinaria = @idMaquinaria";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombreMaquinaria", nombreMaquinaria);
                conexion.AgregarParametro("@numeroSerieMaquinaria", numeroSerieMaquinaria);
                conexion.AgregarParametro("@modeloMaquinaria", modeloMaquinaria);
                conexion.AgregarParametro("@capacidad", capacidadMax);
                conexion.AgregarParametro("@proveedorMaquinaria", proveedorMaquinaria);
                conexion.AgregarParametro("@direccionProveedorMaquinaria", direccionProveedorMaquinaria);
                conexion.AgregarParametro("@telefonoProveedorMaquinaria", telefonoProveedorMaquinaria);
                conexion.AgregarParametro("@contratoServicioMaquinaria", contratoServicioMaquinaria);
                conexion.AgregarParametro("@idBeneficioMaquinaria", idBeneficioMaquinaria);
                conexion.AgregarParametro("@idMaquinaria", idMaquinaria);

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
        public void EliminarMaquinaria(int idMaquinaria)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Maquinaria WHERE id_maquinaria = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", idMaquinaria);

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
        public Maquinaria CountMaquinaria()
        {
            Maquinaria maquina = null;

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalMaquinaria FROM Maquinaria";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        maquina = new Maquinaria()
                        {
                            CountMaquina = Convert.ToInt32(reader["TotalMaquinaria"])
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
            return maquina;
        }

        public Maquinaria ObtenerUltimoId()
        {
            Maquinaria alm = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_maquinaria) AS LastId FROM Maquinaria";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        alm = new Maquinaria()
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
                string consulta = "SELECT COUNT(*) FROM Maquinaria WHERE id_maquinaria = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de la Maquinaria: " + ex.Message);
                return false;
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
        }
        public Maquinaria ObtenerNombrePorCodigo(int codigo)
        {
            Maquinaria IdMaquinaria = null;
            try
            {
                conexion.Conectar();

                string consulta = @"SELECT id_maquinaria, nombre_maquinaria FROM Maquinaria WHERE id_maquinaria = @codigo;";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@codigo", codigo);



                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        IdMaquinaria = new Maquinaria()
                        {
                            IdMaquinaria = Convert.ToInt32(reader["id_maquinaria"]),
                            NombreMaquinaria = Convert.ToString(reader["nombre_maquinaria"]),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de la Maquinaria: " + ex.Message);
            }
            finally
            {
                // Se cierra la conexión a la base de datos
                conexion.Desconectar();
            }
            return IdMaquinaria;
        }

    }
}
