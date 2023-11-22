using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class TrillaController
    {
        private TrillaDAO trillaDAO;

        public TrillaController()
        {
            // Inicializa la instancia de la clase 
            trillaDAO = new TrillaDAO();
        }

        //
        public List<Trilla> ObtenerTrillas()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de las Trilla
                return trillaDAO.ObtenerTrillas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Trilla: " + ex.Message);
                return new List<Trilla>();
            }
        }

        //
        public Trilla ObtenerTrillasPorID(int idTrilla)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Trilla
                return trillaDAO.ObtenerTrillasPorID(idTrilla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Trilla: " + ex.Message);
                return null;
            }
        }
        public List<ReportesTrilla> ObtenerTrillasReports(int idTrilla)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de las Trilla
                return trillaDAO.ObtenerTrillasReports(idTrilla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Trilla: " + ex.Message);
                return new List<ReportesTrilla>();
            }
        }
        
        //
        public Trilla ObtenerTrillasPorIDNombre(int idTrilla)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Trilla
                return trillaDAO.ObtenerTrillasPorIDNombre(idTrilla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Trilla: " + ex.Message);
                return null;
            }
        }
        
        //
        public Trilla ObtenerTrillasPorCosechaIDNombre(int numTrilla, int icosecha)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Trilla
                return trillaDAO.ObtenerTrillasPorCosechaIDNombre(numTrilla, icosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Trilla: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarTrilla(Trilla trilla)
        {
            try
            {
                // Llamada al método del DAO para insertar la Trilla
                return trillaDAO.InsertarTrilla(trilla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de Almacen en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Trilla> ObtenerTrillasNombre()
        {
            try
            {
                // Llamada al método del DAO para obtener las Trilla
                return trillaDAO.ObtenerTrillasNombre();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Trilla: " + ex.Message);
                return new List<Trilla>();
            }
        }
        
        //
        public List<Trilla> ObtenerTrillasPorCosecha(int iCosecha)
        {
            try
            {
                // Llamada al método del DAO para obtener las Trilla
                return trillaDAO.ObtenerTrillasPorCosecha(iCosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Trilla: " + ex.Message);
                return new List<Trilla>();
            }
        }

        //
        public List<Trilla> BuscarTrilla(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Trilla
                return trillaDAO.BuscarTrilla(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Trilla: " + ex.Message);
                return new List<Trilla>();
            }
        }

        //
        public bool ActualizarTrilla(Trilla trilla)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Trilla
                return trillaDAO.ActualizarTrilla(trilla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Trilla: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarTrilla(int idTrilla)
        {
            try
            {
                // Llamada al método del DAO para eliminar la Trilla
                trillaDAO.EliminarTrilla(idTrilla);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la Trilla: " + ex.Message);
            }
        }


        //
        public Trilla CountTrilla(int idCosecha)
        {
            try
            {
                // Llamada al método del DAO para contar la trilla
                return trillaDAO.CountTrilla(idCosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la totalizacion de trilla en la base de datos: " + ex.Message);
                return null;
            }
        }

        //
        public bool VerificarExistenciaTrilla(int idCosecha, int numSubpartida)
        {
            try
            {
                // Llamada al método del DAO para verificar la SubPartida
                return trillaDAO.VerificarExistenciaTrilla(idCosecha, numSubpartida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de las Trillas: " + ex.Message);
                return false;
            }
        }


    }
}
