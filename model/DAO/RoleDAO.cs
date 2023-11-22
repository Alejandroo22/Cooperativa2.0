using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class RoleDAO
    {
        private ConnectionDB conexion;

        public RoleDAO()
        {
            // Inicializa la instancia de la clase ConexionBD
            conexion = new ConnectionDB();
        }

        //obtener todos los roles y id del rol de la BD
        public List<Role> ObtenerIdNombreRoles()
        {
            List<Role> roles = new List<Role>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                string consulta = "SELECT id_rol, nombre_rol FROM Rol";

                // Crear un comando con la consulta
                conexion.CrearComando(consulta);

                using (var reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Role role = new Role();
                        role.IdRol = Convert.ToInt32(reader["id_rol"]);
                        role.NombreRol = Convert.ToString(reader["nombre_rol"]);
                        roles.Add(role);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los roles: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return roles;
        }

        //obtener todos los roles de la BD
        public List<Role> ObtenerRoles()
        {
            List<Role> roles = new List<Role>();

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener los roles
                string consulta = "SELECT * FROM Rol";
                conexion.CrearComando(consulta);

                // Ejecutar la consulta y leer los resultados
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Role rol = new Role()
                        {
                            IdRol = Convert.ToInt32(reader["id_rol"]),
                            NombreRol = Convert.ToString(reader["nombre_rol"]),
                            DescripcionRol = Convert.ToString(reader["descripcion_rol"]),
                            NivelAccesoRol = Convert.ToString(reader["nivel_acceso_rol"]),
                            PermisosRol = Convert.ToString(reader["permisos_rol"]),
                            FechaCreacionRol = Convert.ToDateTime(reader["fecha_creacion_rol"])
                        };

                        // Verifica si el campo es nulo antes de asignarlo
                        if (reader["ult_fecha_mod_rol"] != DBNull.Value)
                        {
                            rol.UltFechaModRol = Convert.ToDateTime(reader["ult_fecha_mod_rol"]);
                        }

                        roles.Add(rol);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de roles: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return roles;
        }

        //obtener un rol en especifico de la BD
        public Role ObtenerRol(string nombreRol)
        {
            Role rol = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Rol WHERE nombre_rol = @NombreRol";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@NombreRol", nombreRol);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        rol = new Role()
                        {
                            IdRol = Convert.ToInt32(reader["id_rol"]),
                            NombreRol = Convert.ToString(reader["nombre_rol"]),
                            DescripcionRol = Convert.ToString(reader["descripcion_rol"]),
                            NivelAccesoRol = Convert.ToString(reader["nivel_acceso_rol"]),
                            PermisosRol = Convert.ToString(reader["permisos_rol"]),
                            FechaCreacionRol = Convert.ToDateTime(reader["fecha_creacion_rol"])
                        };

                        // Verifica si el campo es nulo antes de asignarlo
                        if (!Convert.IsDBNull(reader["ult_fecha_mod_rol"]))
                        {
                            rol.UltFechaModRol = Convert.ToDateTime(reader["ult_fecha_mod_rol"]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el rol: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return rol;
        }

        //
        public bool InsertarRol(Role rol)
        {
            bool exito = false;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para insertar el rol
                string consulta = "INSERT INTO Rol (nombre_rol, descripcion_rol, nivel_acceso_rol, permisos_rol, fecha_creacion_rol)" +
                                  "VALUES (@NombreRol, @DescripcionRol, @NivelAccesoRol, @PermisosRol, @FechaCreacion)";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@NombreRol", rol.NombreRol);
                conexion.AgregarParametro("@DescripcionRol", rol.DescripcionRol);
                conexion.AgregarParametro("@NivelAccesoRol", rol.NivelAccesoRol);
                conexion.AgregarParametro("@PermisosRol", rol.PermisosRol);
                conexion.AgregarParametro("@FechaCreacion", DateTime.Now);

                int filasAfectadas = conexion.EjecutarInstruccion();

                if (filasAfectadas > 0)
                {
                    exito = true; // Inserción exitosa
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el rol en la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return exito;
        }

        //
        public bool ActualizarRol(int id, string nombre, string descripcion, string nivel, string permisos)
        {
            bool exito = false;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para actualizar el rol
                string consulta = "UPDATE Rol SET nombre_rol = @NombreRol, descripcion_rol = @DescripcionRol, " +
                                  "nivel_acceso_rol = @NivelAccesoRol, permisos_rol = @PermisosRol, " +
                                  "ult_fecha_mod_rol = @FechaModificacion WHERE id_rol = @Id";
                conexion.CrearComando(consulta);

                conexion.AgregarParametro("@NombreRol", nombre);
                conexion.AgregarParametro("@DescripcionRol", descripcion);
                conexion.AgregarParametro("@NivelAccesoRol", nivel);
                conexion.AgregarParametro("@PermisosRol", permisos);
                conexion.AgregarParametro("@FechaModificacion", DateTime.Now);
                conexion.AgregarParametro("@Id", id);

                int filasAfectadas = conexion.EjecutarInstruccion();

                if (filasAfectadas > 0)
                {
                    exito = true; // Actualización exitosa
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el rol en la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return exito;
        }

        //
        public void EliminarRol(int idRol)
        {
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para eliminar el rol
                string consulta = "DELETE FROM Rol WHERE id_rol = @IdRol";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@IdRol", idRol);

                // Ejecutar la instrucción de eliminación
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
                Console.WriteLine("Error al eliminar el rol de la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        //obtener el rol en especifico mediante el id en la BD
        public Role ObtenerIdRol(int id)
        {
            Role rol = null;

            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Crear la consulta SQL para obtener el rol
                string consulta = "SELECT * FROM Rol WHERE id_rol = @IdRol";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@IdRol", id);

                // Ejecutar la consulta y leer el resultado
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        rol = new Role()
                        {
                            IdRol = Convert.ToInt32(reader["id_rol"]),
                            NombreRol = Convert.ToString(reader["nombre_rol"]),
                            DescripcionRol = Convert.ToString(reader["descripcion_rol"]),
                            NivelAccesoRol = Convert.ToString(reader["nivel_acceso_rol"]),
                            PermisosRol = Convert.ToString(reader["permisos_rol"]),
                            FechaCreacionRol = Convert.ToDateTime(reader["fecha_creacion_rol"])
                        };

                        // Verifica si el campo es nulo antes de asignarlo
                        if (!Convert.IsDBNull(reader["ult_fecha_mod_rol"]))
                        {
                            rol.UltFechaModRol = Convert.ToDateTime(reader["ult_fecha_mod_rol"]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el rol: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return rol;
        }

        
    }
}
