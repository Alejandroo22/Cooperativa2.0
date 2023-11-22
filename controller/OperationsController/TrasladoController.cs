using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class TrasladoController
    {
        private TrasladoDAO trasladoDAO;

        public TrasladoController()
        {
            // Inicializa la instancia de la clase 
            trasladoDAO = new TrasladoDAO();
        }

        //
        public List<Traslado> ObtenerTrasladosCafe()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de los Traslado
                return trasladoDAO.ObtenerTrasladosCafe();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Traslado: " + ex.Message);
                return new List<Traslado>();
            }
        }

        //
        public Traslado ObtenerTrasladoCafePorID(int idTraslado)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de Traslado
                return trasladoDAO.ObtenerTrasladoCafePorID(idTraslado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Trilla: " + ex.Message);
                return null;
            }
        }
        //
        public List<ReporteTraslado> ObtenerTrasladosReports(int idTaslado)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de las Trilla
                return trasladoDAO.ObtenerTrasladoReport(idTaslado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Traslado: " + ex.Message);
                return new List<ReporteTraslado>();
            }
        }
        //
        public bool InsertarTrasladoCafe(Traslado traslado)
        {
            try
            {
                // Llamada al método del DAO para insertar la Traslado
                return trasladoDAO.InsertarTrasladoCafe(traslado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de Traslado en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Traslado> ObtenerTrasladosCafeNombres()
        {
            try
            {
                // Llamada al método del DAO para obtener las Traslado
                return trasladoDAO.ObtenerTrasladosCafeNombres();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Traslado: " + ex.Message);
                return new List<Traslado>();
            }
        }

        //
        public List<Traslado> BuscarTrasladoCafe(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Traslado
                return trasladoDAO.BuscarTrasladoCafe(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Traslado: " + ex.Message);
                return new List<Traslado>();
            }
        }

        //
        public bool ActualizarTrasladoCafe(Traslado traslado)
        {
            try
            {
                // Llamada al método del DAO para actualizar el Traslado
                return trasladoDAO.ActualizarTrasladoCafe(traslado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Traslado: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarTraslado(int idTraslado)
        {
            try
            {
                // Llamada al método del DAO para eliminar el Traslado
                trasladoDAO.EliminarTraslado(idTraslado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar el Traslado: " + ex.Message);
            }
        }

        //
        public Traslado ObtenerTrasladoPorIDNombre(int idTraslado)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre del Traslado
                return trasladoDAO.ObtenerTrasladoPorIDNombre(idTraslado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Traslado: " + ex.Message);
                return null;
            }
        }
        
        //
        public Traslado ObtenerTrasladoPorCosechaIDNombre(int numTraslado, int iCosecha)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre del Traslado
                return trasladoDAO.ObtenerTrasladoPorCosechaIDNombre(numTraslado, iCosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Traslado: " + ex.Message);
                return null;
            }
        }

        //
        public List<Traslado> ObtenerTrasladoPorCosecha(int iCosecha)
        {
            try
            {
                // Llamada al método del DAO para obtener las Trilla
                return trasladoDAO.ObtenerTrasladoPorCosecha(iCosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Trasalados de Cafe: " + ex.Message);
                return new List<Traslado>();
            }
        }

        //
        public bool VerificarExistenciaTraslado(int idCosecha, int numTraslado)
        {
            try
            {
                // Llamada al método del DAO para verificar los Traslado
                return trasladoDAO.VerificarExistenciaTraslado(idCosecha, numTraslado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de los Traslados: " + ex.Message);
                return false;
            }
        }

        //
        public Traslado CountTraslado(int idCosecha)
        {
            try
            {
                // Llamada al método del DAO para contar la Salida
                return trasladoDAO.CountTraslado(idCosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la totalizacion de los Traslado en la base de datos: " + ex.Message);
                return null;
            }
        }

    }
}
