using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.AccesController
{
    class RoleController
    {
        private RoleDAO roleDAO;

        public RoleController()
        {
            // Inicializa la instancia de la clase 
            roleDAO = new RoleDAO();
        }

        //
        public List<Role> ObtenerRolCbx()
        {
            try
            {
                // Llamada al método del DAO para obtener los roles
                return roleDAO.ObtenerIdNombreRoles();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los roles: " + ex.Message);
                return null;
            }
        }

        //
        public List<Role> ObtenerRoles()
        {
            List<Role> roles = new List<Role>();

            try
            {
                // Llamada al método del DAO para obtener los roles
                roles = roleDAO.ObtenerRoles();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los roles: " + ex.Message);
            }

            return roles;
        }

        //
        public Role ObtenerRol(string nombreRol)
        {
            Role rol = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                rol = roleDAO.ObtenerRol(nombreRol);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el rol: " + ex.Message);
            }

            return rol;
        }

        //
        public bool InsertarRol(Role rol)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para insertar el rol
                exito = roleDAO.InsertarRol(rol);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción del rol: " + ex.Message);
            }

            return exito;
        }

        //
        public bool ActualizarRol(int id, string nombre, string descripcion, string nivel, string permisos)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para actualizar el rol
                exito = roleDAO.ActualizarRol(id, nombre, descripcion, nivel, permisos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la actualización del rol: " + ex.Message);
            }

            return exito;
        }

        //
        public void EliminarRol(int idRol)
        {
            try
            {
                // Llamada al método del DAO para eliminar el rol
                roleDAO.EliminarRol(idRol);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar el rol: " + ex.Message);
            }
        }

        //
        public Role ObtenerIRol(int id)
        {
            Role rol = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                rol = roleDAO.ObtenerIdRol(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el rol: " + ex.Message);
            }

            return rol;
        }

    }
}
