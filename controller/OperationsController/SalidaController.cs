using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class SalidaController
    {
        private SalidaDAO salidaDAO;

        public SalidaController()
        {
            // Inicializa la instancia de la clase 
            salidaDAO = new SalidaDAO();
        }

        //
        public List<Salida> ObtenerSalidasCafe()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de las Salidas de Cafe
                return salidaDAO.ObtenerSalidasCafe();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Salida: " + ex.Message);
                return new List<Salida>();
            }
        }

        //
        public Salida ObtenerSalidaCafePorId(int idSalida)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Salida
                return salidaDAO.ObtenerSalidaCafePorId(idSalida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Salida: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarSalidaCafe(Salida salida)
        {
            try
            {
                // Llamada al método del DAO para insertar la Salida
                return salidaDAO.InsertarSalidaCafe(salida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de Salida en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Salida> ObtenerSalidaCafeNombres()
        {
            try
            {
                // Llamada al método del DAO para obtener las Almacens
                return salidaDAO.ObtenerSalidaCafeNombres();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Salida>();
            }
        }

        //
        public List<Salida> BuscarSalidaCafe(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Salida
                return salidaDAO.BuscarSalidaCafe(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Salida: " + ex.Message);
                return new List<Salida>();
            }
        }

        //
        public bool ActualizarSalidaCafe(Salida salidaCafe)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Salida
                return salidaDAO.ActualizarSalidaCafe(salidaCafe);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Salida: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarSalidaCafe(int idSalida)
        {
            try
            {
                // Llamada al método del DAO para eliminar la Salida
                salidaDAO.EliminarSalidaCafe(idSalida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la Salida: " + ex.Message);
            }
        }

        //
        public List<Salida> ObtenerSalidasPorCosecha(int iCosecha)
        {
            try
            {
                // Llamada al método del DAO para obtener las Trilla
                return salidaDAO.ObtenerSalidasPorCosecha(iCosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Salidas de Cafe: " + ex.Message);
                return new List<Salida>();
            }
        }
        //
        public List<ReportSalida> ObtenerReporteSalida(int idSalida)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de las Trilla
                return salidaDAO.ObtenerReporteSalida(idSalida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la salida: " + ex.Message);
                return new List<ReportSalida>();
            }
        }

        //
        public Salida ObtenerSalidasPorIDNombre(int idSalida)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Salida
                return salidaDAO.ObtenerSalidasPorIDNombre(idSalida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Salida: " + ex.Message);
                return null;
            }
        }
        
        //
        public Salida ObtenerSalidasPorCosechaIDNombre(int numSalida, int icosecha)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Salida
                return salidaDAO.ObtenerSalidasPorCosechaIDNombre(numSalida, icosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Salida: " + ex.Message);
                return null;
            }
        }

        //
        public Salida CountSalida(int idCosecha)
        {
            try
            {
                // Llamada al método del DAO para contar la Salida
                return salidaDAO.CountSalida(idCosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la totalizacion de las Salidas en la base de datos: " + ex.Message);
                return null;
            }
        }

        //
        public bool VerificarExistenciaSalida(int idCosecha, int numSalida)
        {
            try
            {
                // Llamada al método del DAO para verificar la Salida
                return salidaDAO.VerificarExistenciaSalida(idCosecha, numSalida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al verificar la existencia de las Salidas: " + ex.Message);
                return false;
            }
        }

    }
}
