using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.UserDataController
{
    class UserController
    {
        private UserDAO usuarioDAO;

        public UserController()
        {
            usuarioDAO = new UserDAO();
        }

        //funcion utilizada en el login para verificar la autenticacion del usuario en la BD
        public bool AutenticarUsuario(string nombreUsuario, string contraseña)
        {
            try
            {
                // Llamada al método del DAO para autenticar al usuario
                return usuarioDAO.AutenticarUsuario(nombreUsuario, contraseña);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al autenticar al usuario: " + ex.Message);
                return false;
            }
        }

        //funcion utilizada en el login para Obtener los estados del usuario 
        public Usuario ObtenerEstadoUsuario(string nombreUsuario)
        {
            try
            {
                // Llamada al método del DAO para obtener el estado del usuario
                return usuarioDAO.ObtenerEstadoUsuario(nombreUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el estado del usuario: " + ex.Message);
                return null;
            }
        }

        //verificar el modulo al que pertenece el usuario
        public bool VerificarUsuarioDepartamento(string nombreUsuario, int iDepartamento)
        {
            try
            {
                // Llamada al método del DAO para verificar el usuario en el departamento
                return usuarioDAO.VerificarUsuarioDepartamento(nombreUsuario, iDepartamento);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar el usuario en el departamento: " + ex.Message);
                return false;
            }
        }

        //
        public List<Usuario> ObtenerTodosUsuarios()
        {
            try
            {
                // Llamada al método del DAO para obtener todos los usuarios
                return usuarioDAO.ObtenerTodosUsuarios();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
                return null;
            }
        }
        //
        public Usuario ObtenerUsuariosNombresID(int Id_Usuario)
        {
            try
            {
                return usuarioDAO.ObtenerUsuariosNombresID(Id_Usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener nombre del usuario: " + ex.Message);
                return null;
            }
        }
        //
        public List<Usuario> ObtenerTodosUsuariosNombresID()
        {
            try
            {
                // Llamada al método del DAO para obtener todos los usuarios
                return usuarioDAO.ObtenerTodosUsuariosNombresID();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarUsuario(Usuario user)
        {
            try
            {
                // Llamada al método del DAO para insertar el usuario
                return usuarioDAO.InsertarUsuario(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el usuario: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarUsuario(int idUsuario)
        {
            try
            {
                // Llamada al método del DAO para eliminar el usuario
                usuarioDAO.EliminarUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el usuario: " + ex.Message);
            }
        }

        //
        public bool ActualizarUsuario(int id, string nombreUsuario, string email, string clave, DateTime? fechaBaja, string estado, int idRol, int idPersona)
        {
            try
            {
                // Llamada al método del DAO para actualizar el usuario
                return usuarioDAO.ActualizarUsuario(id, nombreUsuario, email, clave, fechaBaja, estado, idRol, idPersona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
                return false;
            }
        }

        //Solo se actualiza el nombre de usuario y el email.
        public bool ActualizarNombreEmailUsuario(int id, string nombreUsuario, string email)
        {
            try
            {
                // Llamada al método del DAO para actualizar el usuario
                return usuarioDAO.ActualizarNombreEmailUsuario(id, nombreUsuario, email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
                return false;
            }
        }

        //Solo se actualiza el nombre de usuario y el email.
        public bool ActualizarClaveUsuario(int id, string pass)
        {
            try
            {
                // Llamada al método del DAO para actualizar el usuario
                return usuarioDAO.ActualizarClaveUsuario(id, pass);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
                return false;
            }
        }

        //
        public Usuario ObtenerUsuario(string nombreUsuario)
        {
            try
            {
                // Llamada al método del DAO para obtener el usuario
                return usuarioDAO.ObtenerUsuario(nombreUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                return null;
            }
        }

        //
        public Usuario ObtenerIUsuario(int id)
        {
            try
            {
                // Llamada al método del DAO para obtener el usuario
                return usuarioDAO.ObtenerIUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                return null;
            }
        }

        //
        public List<Usuario> ObtenerUsuariosConRol()
        {
            try
            {
                List<Usuario> usuarios = usuarioDAO.ObtenerUsuariosConRol();
                return usuarios;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al obtener el usuario con su rol: " + ex.Message);
                return null;
            }
        }
    }
}
