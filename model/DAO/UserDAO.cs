using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.DAO
{
    class UserDAO
    {
        private ConnectionDB conexion;

        public UserDAO()
        {
            // Inicializa la instancia de la clase ConexionBD
            conexion = new ConnectionDB();
        }

        //autentifica si el usuario que desea ingresar cumple con sus credenciales si cumple accede y guarda su id
        public bool AutenticarUsuario(string nombreUsuario, string contraseña)
        {
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                string query = "SELECT clave_usuario FROM Usuario WHERE nombre_usuario = @nombreUsuario";

                // Crear un comando con la consulta
                conexion.CrearComando(query);
                conexion.AgregarParametro("@nombreUsuario", nombreUsuario);

                string hashedPasswordFromDB = conexion.EjecutarConsultaEscalar() as string;

                bool isMatch = false;

                if (hashedPasswordFromDB != null)
                {
                    isMatch = controller.SecurityData.PasswordManager.VerifyPassword(contraseña, hashedPasswordFromDB);
                }

                return isMatch; // Devolver true si las credenciales son válidas, false en caso contrario
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al autenticar al usuario: " + ex.Message);
                return false;
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        //funcion para obtener el tipo de estado del usuario
        public Usuario ObtenerEstadoUsuario(string nombreUsuario)
        {
            Usuario estado = new Usuario();

            try
            {
                // Abre la conexión a la base de datos
                conexion.Conectar();

                string consulta = "SELECT estado_usuario FROM Usuario WHERE nombre_usuario = @NombreUsuario";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@NombreUsuario", nombreUsuario);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            estado.EstadoUsuario = Convert.ToString(reader["estado_usuario"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el estado del usuario: " + ex.Message);
            }
            finally
            {
                // Cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return estado;
        }

        //verificar el modulo al que pertenece el usuario
        public bool VerificarUsuarioDepartamento(string nombreUsuario, int iDepartamento)
        {
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Consulta para obtener el ID del usuario
                string usuarioQuery = "SELECT id_usuario FROM Usuario WHERE nombre_usuario = @nombreUsuario";
                conexion.CrearComando(usuarioQuery);
                conexion.AgregarParametro("@nombreUsuario", nombreUsuario);

                int idUsuario = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                // Consulta para verificar la existencia del usuario en el departamento seleccionado
                string verificacionQuery = @"SELECT COUNT(*) FROM Usuario_Modulo um
                                        INNER JOIN Modulo m ON um.id_modulo = m.id_modulo
                                        WHERE um.id_usuario = @idUsuario AND m.id_modulo = @iDepartamento";
                conexion.CrearComando(verificacionQuery);
                conexion.AgregarParametro("@idUsuario", idUsuario);
                conexion.AgregarParametro("@iDepartamento", iDepartamento);

                int count = Convert.ToInt32(conexion.EjecutarConsultaEscalar());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar el usuario en el departamento: " + ex.Message);
                return false;
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        //
        public List<Usuario> ObtenerTodosUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                // Abre la conexión a la base de datos
                conexion.Conectar();

                string consulta = "SELECT * FROM Usuario";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                            NombreUsuario = Convert.ToString(reader["nombre_usuario"]),
                            EmailUsuario = (reader["email_usuario"]) is DBNull ? "" : Convert.ToString(reader["email_usuario"]),
                            ClaveUsuario = Convert.ToString(reader["clave_usuario"]),
                            EstadoUsuario = Convert.ToString(reader["estado_usuario"]),
                            FechaCreacionUsuario = Convert.ToDateTime(reader["fecha_creacion_usuario"]),
                            FechaBajaUsuario = reader.IsDBNull(reader.GetOrdinal("fecha_baja_usuario")) ? null : (DateTime?)reader["fecha_baja_usuario"],
                            IdRolUsuario = Convert.ToInt32(reader["id_rol_usuario"]),
                            IdPersonaUsuario = Convert.ToInt32(reader["id_persona_usuario"])
                        };

                        usuarios.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }
            finally
            {
                // Cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return usuarios;
        }

        //
        public Usuario ObtenerUsuariosNombresID(int Id_Usuario)
        {
            Usuario usuario = null;

            try
            {
                // Abre la conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT CONCAT(
                                        SUBSTRING_INDEX(p.nombres_persona, ' ', 1), ' ', 
                                        SUBSTRING_INDEX(p.apellidos_persona, ' ', 1)
                                    ) AS Nombre_Completo  
                                    FROM Usuario u
                                    INNER JOIN Persona p ON u.id_persona_usuario = p.id_persona
                                    WHERE id_usuario = @Id_Usuario;
                                    ";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@Id_Usuario", Id_Usuario);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows && reader.Read())
                    {
                        usuario = new Usuario()
                        {

                            ApellidoPersonaUsuario = Convert.ToString(reader["Nombre_Completo"]),

                        };

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }
            finally
            {
                // Cierra la conexión a la base de datos
                conexion.Desconectar();
            }
            return usuario;
        }
        //
        public List<Usuario> ObtenerTodosUsuariosNombresID()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                // Abre la conexión a la base de datos
                conexion.Conectar();

                string consulta = @"SELECT u.id_usuario, u.nombre_usuario, u.email_usuario, u.clave_usuario, u.estado_usuario, u.fecha_creacion_usuario,
                                              u.fecha_baja_usuario, r.nombre_rol, p.nombres_persona   
                                    FROM Usuario u
                                    INNER JOIN Persona p ON u.id_persona_usuario = p.id_persona
                                    INNER JOIN Rol r ON u.id_rol_usuario = r.id_rol ";
                conexion.CrearComando(consulta);

                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                            NombreUsuario = Convert.ToString(reader["nombre_usuario"]),
                            EmailUsuario = (reader["email_usuario"]) is DBNull ? "" : Convert.ToString(reader["email_usuario"]),
                            ClaveUsuario = Convert.ToString(reader["clave_usuario"]),
                            EstadoUsuario = Convert.ToString(reader["estado_usuario"]),
                            FechaCreacionUsuario = Convert.ToDateTime(reader["fecha_creacion_usuario"]),
                            FechaBajaUsuario = reader.IsDBNull(reader.GetOrdinal("fecha_baja_usuario")) ? null : (DateTime?)reader["fecha_baja_usuario"],
                            NombreRol = Convert.ToString(reader["nombre_rol"]),
                            NombrePersonaUsuario = Convert.ToString(reader["nombres_persona"])
                        };

                        usuarios.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }
            finally
            {
                // Cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return usuarios;
        }

        //funcion para insertar unicamente el usuario
        public bool InsertarUsuario(Usuario user)
        {
            bool exito = false;
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                // Consulta SQL para insertar un nuevo usuario
                string consulta = @"INSERT INTO Usuario (nombre_usuario, email_usuario, clave_usuario, estado_usuario, 
                fecha_creacion_usuario, id_rol_usuario, id_persona_usuario) 
                VALUES (@nombreUsuario, @emailUsuario, @claveUsuario, @estadoUsuario, 
                @fechaCreacionUsuario, @idRolUsuario, @idPersonaUsuario)";

                // Crear un comando con la consulta
                conexion.CrearComando(consulta);

                // Agregar los parámetros al comando
                conexion.AgregarParametro("@nombreUsuario", user.NombreUsuario);
                conexion.AgregarParametro("@emailUsuario", user.EmailUsuario);
                conexion.AgregarParametro("@claveUsuario", user.ClaveUsuario);
                conexion.AgregarParametro("@estadoUsuario", user.EstadoUsuario);
                conexion.AgregarParametro("@fechaCreacionUsuario", DateTime.Today);
                conexion.AgregarParametro("@idRolUsuario", user.IdRolUsuario);
                conexion.AgregarParametro("@idPersonaUsuario", user.IdPersonaUsuario);

                // Ejecutar la consulta
                int filasAfectadas = conexion.EjecutarInstruccion();

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("La inserción se realizó correctamente.");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la inserción.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el usuario: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return exito;
        }

        //
        public void EliminarUsuario(int idUsuario)
        {
            try
            {
                conexion.Conectar();

                // Crear el comando SQL para eliminar el registro
                string consulta = "DELETE FROM Usuario WHERE id_usuario = @idUsuario";
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@idUsuario", idUsuario);

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
            catch (MySqlException ex)
            {
                // Verificar si la excepción es por restricción de clave externa
                if (ex.Number == 1451 || ex.Number == 1452)
                {
                    Console.WriteLine("No se puede eliminar el registro debido a una restricción de clave externa.");
                    ActualizarEstadoUsuario(idUsuario, "Eliminado");
                }
                else
                {
                    // Otra excepción, manejarla de acuerdo a tus necesidades
                    Console.WriteLine("Error al eliminar el registro: " + ex.Message);
                }
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
        }

        //
        public bool ActualizarUsuario(int id, string nombreUsuario, string email, string clave, DateTime? fechaBaja, string estado, int idRol, int idPersona)
        {
            bool exito = false;
            try
            {
                // Abrir la conexión a la base de datos
                conexion.Conectar();

                // Consulta SQL para actualizar el usuario
                string consulta = @"UPDATE Usuario SET nombre_usuario = @nombre, email_usuario = @email, 
                                clave_usuario = @clave, fecha_baja_usuario = @fechaBaja, estado_usuario = @estado, 
                                id_rol_usuario = @idRol, id_persona_usuario = @idPersona 
                                WHERE id_usuario = @id";

                // Crear el comando SQL
                conexion.CrearComando(consulta);

                // Agregar los parámetros al comando SQL
                conexion.AgregarParametro("@nombre", nombreUsuario);
                conexion.AgregarParametro("@email", email);
                conexion.AgregarParametro("@clave", clave);
                conexion.AgregarParametro("@fechaBaja", fechaBaja);
                conexion.AgregarParametro("@estado", estado);
                conexion.AgregarParametro("@idRol", idRol);
                conexion.AgregarParametro("@idPersona", idPersona);
                conexion.AgregarParametro("@id", id);

                // Ejecutar la instrucción de actualización
                int filasAfectadas = conexion.EjecutarInstruccion();

                // Verificar si la actualización fue exitosa
                if (filasAfectadas > 0)
                {
                    Console.WriteLine("La actualización se realizó correctamente.");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la actualización.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
            return exito;
        }

        //
        public bool ActualizarEstadoUsuario(int id, string estado)
        {
            bool exito = false;
            try
            {
                // Abrir la conexión a la base de datos
                conexion.Conectar();

                // Consulta SQL para actualizar el usuario
                string consulta = @"UPDATE Usuario SET estado_usuario = @estado, fecha_baja_usuario = @fechaBaja WHERE id_usuario = @id";

                // Crear el comando SQL
                conexion.CrearComando(consulta);

                // Agregar los parámetros al comando SQL
                conexion.AgregarParametro("@estado", estado);
                conexion.AgregarParametro("@fechaBaja", DateTime.Today);
                conexion.AgregarParametro("@id", id);

                // Ejecutar la instrucción de actualización
                int filasAfectadas = conexion.EjecutarInstruccion();

                // Verificar si la actualización fue exitosa
                if (filasAfectadas > 0)
                {
                    Console.WriteLine("La actualización se realizó correctamente.");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la actualización.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
            return exito;
        }

        //Actualiza unicamente el nombre de usuario y su email
        public bool ActualizarNombreEmailUsuario(int id, string nombreUsuario, string email)
        {
            bool exito = false;
            try
            {
                // Abrir la conexión a la base de datos
                conexion.Conectar();

                // Consulta SQL para actualizar el usuario
                string consulta = @"UPDATE Usuario SET nombre_usuario = @nombre, email_usuario = @email
                                WHERE id_usuario = @id";

                // Crear el comando SQL
                conexion.CrearComando(consulta);

                // Agregar los parámetros al comando SQL
                conexion.AgregarParametro("@nombre", nombreUsuario);
                conexion.AgregarParametro("@email", email);
                conexion.AgregarParametro("@id", id);

                // Ejecutar la instrucción de actualización
                int filasAfectadas = conexion.EjecutarInstruccion();

                // Verificar si la actualización fue exitosa
                if (filasAfectadas > 0)
                {
                    Console.WriteLine("La actualización se realizó correctamente.");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la actualización.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
            return exito;
        }

        //Actualiza unicamente la clave del usuario
        public bool ActualizarClaveUsuario(int id, string pass)
        {
            bool exito = false;
            try
            {
                // Abrir la conexión a la base de datos
                conexion.Conectar();

                // Consulta SQL para actualizar el usuario
                string consulta = @"UPDATE Usuario SET clave_usuario = @clave
                                WHERE id_usuario = @id";

                // Crear el comando SQL
                conexion.CrearComando(consulta);

                // Agregar los parámetros al comando SQL
                conexion.AgregarParametro("@clave", pass);
                conexion.AgregarParametro("@id", id);

                // Ejecutar la instrucción de actualización
                int filasAfectadas = conexion.EjecutarInstruccion();

                // Verificar si la actualización fue exitosa
                if (filasAfectadas > 0)
                {
                    Console.WriteLine("La actualización se realizó correctamente.");
                    exito = true;
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la actualización.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }
            return exito;
        }

        //funcion para obtener un usuario en especifico
        public Usuario ObtenerUsuario(string nombreUsuario)
        {
            Usuario usuario = null;
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                string consulta = "SELECT * FROM Usuario WHERE nombre_usuario = @nombreUsuario";

                // Crear un comando con la consulta
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@nombreUsuario", nombreUsuario);

                using (var reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                NombreUsuario = Convert.ToString(reader["nombre_usuario"]),
                                EmailUsuario = (reader["email_usuario"]) is DBNull ? "" : Convert.ToString(reader["email_usuario"]),
                                ClaveUsuario = Convert.ToString(reader["clave_usuario"]),
                                EstadoUsuario = Convert.ToString(reader["estado_usuario"]),
                                FechaCreacionUsuario = Convert.ToDateTime(reader["fecha_creacion_usuario"]),
                                IdRolUsuario = Convert.ToInt32(reader["id_rol_usuario"]),
                                IdPersonaUsuario = Convert.ToInt32(reader["id_persona_usuario"])
                            };

                            if (!reader.IsDBNull(reader.GetOrdinal("fecha_baja_usuario")))
                            {
                                usuario.FechaBajaUsuario = reader.GetDateTime("fecha_baja_usuario");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el usuario: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return usuario;
        }

        //
        public Usuario ObtenerIUsuario(int id)
        {
            Usuario usuario = null;
            try
            {
                // Conectar a la base de datos
                conexion.Conectar();

                string consulta = "SELECT * FROM Usuario WHERE id_usuario = @IUsuario";

                // Crear un comando con la consulta
                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@IUsuario", id);

                using (var reader = conexion.EjecutarConsultaReader(consulta))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                NombreUsuario = Convert.ToString(reader["nombre_usuario"]),
                                EmailUsuario = (reader["email_usuario"]) is DBNull ? "" : Convert.ToString(reader["email_usuario"]),
                                ClaveUsuario = Convert.ToString(reader["clave_usuario"]),
                                EstadoUsuario = Convert.ToString(reader["estado_usuario"]),
                                FechaCreacionUsuario = Convert.ToDateTime(reader["fecha_creacion_usuario"]),
                                IdRolUsuario = Convert.ToInt32(reader["id_rol_usuario"]),
                                IdPersonaUsuario = Convert.ToInt32(reader["id_persona_usuario"])
                            };

                            if (!reader.IsDBNull(reader.GetOrdinal("fecha_baja_usuario")))
                            {
                                usuario.FechaBajaUsuario = reader.GetDateTime("fecha_baja_usuario");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el usuario: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                conexion.Desconectar();
            }

            return usuario;
        }


        //obtener todos los usuarios y traer el nombre del rol en vez del id
        public List<Usuario> ObtenerUsuariosConRol()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                conexion.Conectar();

                string consulta = @"SELECT Usuario.id_usuario, Usuario.nombre_usuario, Usuario.email_usuario, Usuario.clave_usuario, 
                                Usuario.estado_usuario, Usuario.fecha_creacion_usuario, Usuario.fecha_baja_usuario, Rol.nombre_rol
                            FROM Usuario
                            JOIN Rol ON Usuario.id_rol_usuario = Rol.id_rol";

                conexion.CrearComando(consulta);
                using (MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta))
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        usuario.NombreUsuario = Convert.ToString(reader["nombre_usuario"]);
                        usuario.EmailUsuario = (reader["email_usuario"]) is DBNull ? "" : Convert.ToString(reader["email_usuario"]);
                        usuario.ClaveUsuario = Convert.ToString(reader["clave_usuario"]);
                        usuario.EstadoUsuario = Convert.ToString(reader["estado_usuario"]);
                        usuario.FechaCreacionUsuario = Convert.ToDateTime(reader["fecha_creacion_usuario"]);
                        usuario.FechaBajaUsuario = reader.IsDBNull(reader.GetOrdinal("fecha_baja_usuario")) ? null : (DateTime?)reader["fecha_baja_usuario"];
                        usuario.NombreRol = Convert.ToString(reader["nombre_rol"]);

                        usuarios.Add(usuario);
                    }
                }
                
            }
            catch (Exception ex)
            {
                // Manejar el error de conexión o consulta
                Console.WriteLine("Error al obtener el usuario con su rol: " + ex.Message);
            }
            finally
            {
                // Cierra la conexión a la base de datos
                conexion.Desconectar();
            }

            return usuarios;
        }


    }
}
