using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Acces;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.AccesController
{
    class FallaController
    {
        private FallasDAO fallaDAO;

        public FallaController()
        {
            // Inicializa la instancia de la clase 
            fallaDAO = new FallasDAO();
        }

        //
        public List<Fallas> ObtenerFallaNombreMaquinaria()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Falla
                return fallaDAO.ObtenerFallaNombreMaquinaria();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Fallas: " + ex.Message);
                return new List<Fallas>();
            }
        }

        //
        public Fallas ObtenerIdFalla(int idFalla)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Fallas
                return fallaDAO.ObtenerIdFalla(idFalla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Fallas: " + ex.Message);
                return null;
            }
        }

        //
        public Fallas ObtenerNombreMaquinariaFalla(string nomMaquinaria)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Fallas
                return fallaDAO.ObtenerFallaNombreMaquinaria(nomMaquinaria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Fallas: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarFalla(Fallas Falla)
        {
            try
            {
                // Llamada al método del DAO para insertar la Falla
                return fallaDAO.InsertarFalla(Falla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de Falla en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Fallas> ObtenerFallas()
        {
            try
            {
                // Llamada al método del DAO para obtener las Fallas
                return fallaDAO.ObtenerFallas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Fallas: " + ex.Message);
                return new List<Fallas>();
            }
        }

        //
        public List<Fallas> BuscarFallas(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Fallas
                return fallaDAO.BuscarFalla(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Fallas: " + ex.Message);
                return new List<Fallas>();
            }
        }

        //
        public bool ActualizarFallas(int idfalla, string descripcion, string pieza, DateTime fecha, string accion, string observacion, int idMaquinaria)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Fallas
                return fallaDAO.Actualizarfalla(idfalla, descripcion, pieza, fecha, accion, observacion, idMaquinaria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Fallas: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarFallas(int idFallas)
        {
            try
            {
                // Llamada al método del DAO para eliminar la Fallas
                fallaDAO.EliminarFalla(idFallas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la Fallas: " + ex.Message);
            }
        }

        //
        public List<Maquinaria> ObtenerMaquinas()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Bodega
                return fallaDAO.ObtenerMaquina();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Bodegas: " + ex.Message);
                return new List<Maquinaria>();
            }
        }

    }
}
