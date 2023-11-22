using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.AccesController
{
    class ModuleController
    {
        private ModuleDAO moduloDAO;

        public ModuleController()
        {
            moduloDAO = new ModuleDAO();
        }

        //funcion para obtner los modulos del DAO utilizada en el login y
        public List<Module> ObtenerModulos()
        {
            try
            {
                // Llamada al método del DAO para obtener los módulos
                return moduloDAO.ObtenerModulos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los módulos: " + ex.Message);
                return null;
            }
        }

        //obtener los modulos al cual pertenece el usuario
        public List<Module> ObtenerModulosDeUsuario(int idUsuario)
        {
            try
            {
                // Llamada al método del DAO para obtener los módulos del usuario
                return moduloDAO.ObtenerModulosDeUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los módulos del usuario: " + ex.Message);
                return null;
            }
        }

        //
        public void InsertarUsuarioModulos(int idUsuario, List<int> modulosSeleccionados)
        {
            try
            {
                // Llamada al método del DAO para insertar los módulos asignados al usuario
                moduloDAO.InsertarUsuarioModulos(idUsuario, modulosSeleccionados);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al insertar los módulos del usuario: " + ex.Message);
            }
        }

        //
        public void EliminarModulosDelUsuario(int usuarioId, List<int> modulosEliminar)
        {
            try
            {
                moduloDAO.EliminarModulosDelUsuario(usuarioId, modulosEliminar);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al eliminar los módulos del usuario: " + ex.Message);
            }
        }

        //
        public List<int> ObtenerModulosActualesDelUsuario(int usuarioId)
        {
            try
            {
                return moduloDAO.ObtenerModulosActualesDelUsuario(usuarioId);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al eliminar los módulos del usuario: " + ex.Message);
                return null;
            }
        }

    }
}
