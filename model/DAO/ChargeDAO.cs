using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Acces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class ChargeDAO
    {
        private ConnectionDB conexion;

        public ChargeDAO()
        {
            //Inicializa la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        public bool InsertarCargo(Charge cargo)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //Se crea la consulta para insertar las Cargoes de cafe
                string consulta = @"INSERT INTO Cargo (id_cargo, nombre_cargo, descripcion_cargo)
                                  VALUES (@IdCargo, @Cargo, @Comentario)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@IdCargo", cargo.IdCargo);
                conexion.AgregarParametro("@Cargo", cargo.NombreCargo);
                conexion.AgregarParametro("@Comentario", cargo.DescripcionCargo);

                int filasAfectadas = conexion.EjecutarInstruccion();

                return filasAfectadas > 0; //si la fila se afecta, inserto la Cargo con exito

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción del Cargo del personal en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

        }

        public List<Charge> ObtenerCargos()
        {
            List<Charge> cargos = new List<Charge>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Cargo";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Charge cargo = new Charge()
                        {
                            IdCargo = Convert.ToInt32(reader["id_cargo"]),
                            NombreCargo = Convert.ToString(reader["nombre_cargo"]),
                            DescripcionCargo = (reader["descripcion_cargo"]) is DBNull ? "" : Convert.ToString(reader["descripcion_cargo"])
                        };

                        cargos.Add(cargo);
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
            return cargos;
        }

        public bool ActualizarCargos(int id, string cargo, string descrip)
        {
            bool exito = false;

            try
            {
                //conexion a base de datos
                conexion.Conectar();

                string consulta = @"UPDATE Cargo SET nombre_cargo = @nomCargo, descripcion_cargo = @dCargo
                                  WHERE id_cargo = @id";

                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nomCargo", cargo);
                conexion.AgregarParametro("@dCargo", descrip);
                conexion.AgregarParametro("@id", id);

                int filasAfectadas = conexion.EjecutarInstruccion();

                // Puedes realizar alguna acción adicional en función del resultado de la actualización
                if (filasAfectadas > 0)
                {
                    Console.WriteLine("La actualización se realizó correctamente.");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la actualización.");
                    exito = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al actualizar los datos: " + ex.Message);
            }
            finally
            {
                //se cierra la conexion con la base de datos
                conexion.Desconectar();
            }

            return exito;
        }

        //
        public Charge ObtenerNombreCargo(string nomCargo)
        {
            Charge Cargo = null;

            try
            {
                conexion.Conectar();

                string consulta = @"SELECT * FROM Cargo WHERE nombre_cargo = @nomCargo";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nomCargo", nomCargo);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        Cargo = new Charge()
                        {
                            IdCargo = Convert.ToInt32(reader["id_cargo"]),
                            NombreCargo = Convert.ToString(reader["nombre_cargo"]),
                            DescripcionCargo = (reader["descripcion_cargo"]) is DBNull ? "" : Convert.ToString(reader["descripcion_cargo"])
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
                conexion.Desconectar();
            }
            return Cargo;
        }


        //
        public Charge CountCargo()
        {
            Charge cargo = null;

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT COUNT(*) AS TotalCargo FROM Cargo";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.Read())
                    {
                        cargo = new Charge()
                        {
                            CountCargo = Convert.ToInt32(reader["TotalCargo"])
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
            return cargo;
        }

        public void EliminarCargo(int idCargo)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el comando SQL 
                string consulta = @"DELETE FROM Cargo WHERE id_Cargo = @idCargo";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@idCargo", idCargo);

                //se ejecuta la isntruccion
                int filasAfectadas = conexion.EjecutarInstruccion();

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("El registro se eliminó correctamente");
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

        public Charge ObtenerUltimoId()
        {
            Charge alm = null;
            try
            {
                // Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT MAX(id_cargo) AS LastId FROM Cargo";

                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        alm = new Charge()
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
                string consulta = "SELECT COUNT(*) FROM Cargo WHERE id_cargo = @id";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia del Cargo Personal: " + ex.Message);
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
