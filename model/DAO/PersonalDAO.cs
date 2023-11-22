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
    class PersonalDAO
    {
        private ConnectionDB conexion;

        public PersonalDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }

        //funcion para insertar un nuevo registro en la base de datos
        public bool InsertarPersonal(Personal personal)
        {
            try
            {
                Console.WriteLine("Depurador - insercion DAO: " + personal.NombrePersonal + " " + personal.Descripcion + " " + personal.ICargo + " " + personal.IdPersona);

                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL para insertar
                string consulta = @"INSERT INTO Personal ( nombre_personal, id_cargo_personal, descripcion_personal, id_persona_personal)
                                    VALUES ( @nombre, @cargo, @descripcion, @idPersona)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombre", personal.NombrePersonal);
                conexion.AgregarParametro("@cargo", personal.ICargo);
                conexion.AgregarParametro("@descripcion", personal.Descripcion);
                conexion.AgregarParametro("@idPersona", personal.IdPersona);

                int filasAfectadas = conexion.EjecutarInstruccion();

                //si la fila se afecta, se inserto correctamente
                return filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción del Personal en la base de datos: " + ex.Message);
                return false;
            }
            finally
            {
                //Se cierra la conexion a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para mostrar todos los registros
        public List<Personal> ObtenerPersonales()
        {
            List<Personal> listaPersonal = new List<Personal>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT * FROM Personal";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Personal personales = new Personal()
                        {
                            IdPersonal = Convert.ToInt32(reader["id_personal"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ICargo = Convert.ToInt32(reader["id_cargo_personal"]),
                            Descripcion = (reader["descripcion_personal"]) is DBNull ? "" : Convert.ToString(reader["descripcion_personal"]),
                            IdPersona = Convert.ToInt32(reader["id_persona_personal"])
                        };

                        listaPersonal.Add(personales);
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
            return listaPersonal;
        }

        //funcion para mostrar todos los registros y traer el nombre del cargo
        public List<Personal> ObtenerPersonalesNombreCargo()
        {
            List<Personal> listaPersonal = new List<Personal>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT id_personal, nombre_personal, c.nombre_cargo, descripcion_personal, p.nombres_persona, id_persona_personal
                                            FROM Personal pl
                                                INNER JOIN Cargo c ON pl.id_cargo_personal = c.id_cargo
                                                INNER JOIN Persona p ON pl.id_persona_personal = p.id_persona";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Personal personales = new Personal()
                        {
                            IdPersonal = Convert.ToInt32(reader["id_personal"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            NombreCargo = Convert.ToString(reader["nombre_cargo"]),
                            Descripcion = (reader["descripcion_personal"]) is DBNull ? "" : Convert.ToString(reader["descripcion_personal"]),
                            IdPersona = Convert.ToInt32(reader["id_persona_personal"]),
                            NombrePersona = Convert.ToString(reader["nombres_persona"])
                        };

                        listaPersonal.Add(personales);
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
            return listaPersonal;
        }
        
        //funcion para mostrar todos los registros y traer el nombre del cargo
        public List<Personal> ObtenerPersonalEspecificoNombreCargo(string nombre)
        {
            List<Personal> listaPersonal = new List<Personal>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT id_personal, nombre_personal, c.nombre_cargo, descripcion_personal, p.nombres_persona, id_persona_personal
                                            FROM Personal pl
                                                INNER JOIN Cargo c ON pl.id_cargo_personal = c.id_cargo
                                                INNER JOIN Persona p ON pl.id_persona_personal = p.id_persona
                                            WHERE pl.nombre_cargo = @nombre";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombre", nombre);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Personal personales = new Personal()
                        {
                            IdPersonal = Convert.ToInt32(reader["id_personal"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            NombreCargo = Convert.ToString(reader["nombre_cargo"]),
                            Descripcion = (reader["descripcion_personal"]) is DBNull ? "" : Convert.ToString(reader["descripcion_personal"]),
                            IdPersona = Convert.ToInt32(reader["id_persona_personal"]),
                            NombrePersona = Convert.ToString(reader["nombres_persona"])
                        };

                        listaPersonal.Add(personales);
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
            return listaPersonal;
        }
        
        //funcion para mostrar todos los registros y traer el nombre del cargo
        public List<Personal> BuscarPersonal(string buscar)
        {
            List<Personal> listaPersonal = new List<Personal>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT pl.id_personal, pl.nombre_personal, c.nombre_cargo, pl.descripcion_personal, p.nombres_persona, pl.id_persona_personal
                                            FROM Personal pl
                                                INNER JOIN Cargo c ON pl.id_cargo_personal = c.id_cargo
                                                INNER JOIN Persona p ON pl.id_persona_personal = p.id_persona
                                            WHERE c.nombre_cargo LIKE CONCAT('%', @search, '%') OR pl.nombre_personal LIKE CONCAT('%', @search, '%') ";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", buscar);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Personal personales = new Personal()
                        {
                            IdPersonal = Convert.ToInt32(reader["id_personal"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            NombreCargo = Convert.ToString(reader["nombre_cargo"]),
                            Descripcion = (reader["descripcion_personal"]) is DBNull ? "" : Convert.ToString(reader["descripcion_personal"]),
                            IdPersona = Convert.ToInt32(reader["id_persona_personal"]),
                            NombrePersona = Convert.ToString(reader["nombres_persona"])
                        };

                        listaPersonal.Add(personales);
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
            return listaPersonal;
        }
        
        //funcion para mostrar todos los registros y traer el nombre del cargo
        public List<Personal> BuscarPersonalCargo(string buscar)
        {
            List<Personal> listaPersonal = new List<Personal>();

            try
            {
                //Se conecta con la base de datos
                conexion.Conectar();

                string consulta = @"SELECT pl.id_personal, pl.nombre_personal, c.nombre_cargo, pl.descripcion_personal, p.nombres_persona, pl.id_persona_personal
                                            FROM Personal pl
                                                INNER JOIN Cargo c ON pl.id_cargo_personal = c.id_cargo
                                                INNER JOIN Persona p ON pl.id_persona_personal = p.id_persona
                                            WHERE c.nombre_cargo LIKE CONCAT('%', @search, '%')";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@search", buscar);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Personal personales = new Personal()
                        {
                            IdPersonal = Convert.ToInt32(reader["id_personal"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            NombreCargo = Convert.ToString(reader["nombre_cargo"]),
                            Descripcion = (reader["descripcion_personal"]) is DBNull ? "" : Convert.ToString(reader["descripcion_personal"]),
                            IdPersona = Convert.ToInt32(reader["id_persona_personal"]),
                            NombrePersona = Convert.ToString(reader["nombres_persona"])
                        };

                        listaPersonal.Add(personales);
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
            return listaPersonal;
        }

        //obtener el Personal en especifico mediante el id en la BD
        public Personal ObtenerIdPersonal(int id)
        {
            Personal personal = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Personal WHERE id_personal = @Id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id", id);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        personal = new Personal()
                        {
                            IdPersonal = Convert.ToInt32(reader["id_personal"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ICargo = Convert.ToInt32(reader["id_cargo_personal"]),
                            Descripcion = (reader["descripcion_personal"]) is DBNull ? "" : Convert.ToString(reader["descripcion_personal"]),
                            IdPersona = Convert.ToInt32(reader["id_persona_personal"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Personal: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return personal;
        }

        //obtener el Personal en especifico mediante el id en la BD
        public Personal ObtenerPersonalNombre(string nombre)
        {
            Personal personal = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Personal WHERE nombre_personal = @nombrePersonal";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombrePersonal", nombre);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        personal = new Personal()
                        {
                            IdPersonal = Convert.ToInt32(reader["id_personal"]),
                            NombrePersonal = Convert.ToString(reader["nombre_personal"]),
                            ICargo = Convert.ToInt32(reader["id_cargo_personal"]),
                            Descripcion = (reader["descripcion_personal"]) is DBNull ? "" : Convert.ToString(reader["descripcion_personal"]),
                            IdPersona = Convert.ToInt32(reader["id_persona_personal"])
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Personal: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return personal;
        }

        //funcion para actualizar un registro en la base de datos
        public bool ActualizarPersonal(int id, string nombreBen, int icargo, string descripcion, int idPersona)
        {
            bool exito = false;

            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea el script SQL 
                string consulta = @"UPDATE Personal SET nombre_personal = @nombre, id_cargo_personal = @cargo, 
                                        descripcion_personal = @descripcion, id_persona_personal = @idPersona
                                        WHERE id_personal = @id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@nombre", nombreBen);
                conexion.AgregarParametro("@cargo", icargo);
                conexion.AgregarParametro("@descripcion", descripcion);
                conexion.AgregarParametro("@idPersona", idPersona);
                conexion.AgregarParametro("@id", id);

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
        public void EliminarPersonal(int id)
        {
            try
            {
                //conexion a la base de datos
                conexion.Conectar();

                //se crea script SQL
                string consulta = @"DELETE FROM Personal WHERE id_personal = @id";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id", id);

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
