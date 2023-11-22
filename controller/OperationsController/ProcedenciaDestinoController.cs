using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class ProcedenciaDestinoController
    {
        private ProcedenciaDestinoDAO prodesDAO;

        public ProcedenciaDestinoController()
        {
            // Inicializa la instancia de la clase 
            prodesDAO = new ProcedenciaDestinoDAO();
        }

        //
        public List<ProcedenciaDestino> ObtenerProcedenciasDestino()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de las ProcedenciaDestino
                return prodesDAO.ObtenerProcedenciasDestino();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de ProcedenciaDestino: " + ex.Message);
                return new List<ProcedenciaDestino>();
            }
        }

        //
        public ProcedenciaDestino ObtenerProcedenciaDestinoPorId(int idProDes)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la ProcedenciaDestino
                return prodesDAO.ObtenerProcedenciaDestinoPorId(idProDes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la ProcedenciaDestino: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarProcedenciaDestino(ProcedenciaDestino proDes)
        {
            try
            {
                // Llamada al método del DAO para insertar la ProcedenciaDestino
                return prodesDAO.InsertarProcedenciaDestino(proDes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de ProcedenciaDestino en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<ProcedenciaDestino> ObtenerProcedenciasDestinoNombres()
        {
            try
            {
                // Llamada al método del DAO para obtener las ProcedenciaDestino
                return prodesDAO.ObtenerProcedenciasDestinoNombres();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de ProcedenciaDestino: " + ex.Message);
                return new List<ProcedenciaDestino>();
            }
        }

        //
        public List<ProcedenciaDestino> BuscarProcedenciaDestino(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las ProcedenciaDestino
                return prodesDAO.BuscarProcedenciaDestino(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de ProcedenciaDestino: " + ex.Message);
                return new List<ProcedenciaDestino>();
            }
        }

        //
        public bool ActualizarProcedenciaDestino(ProcedenciaDestino proDes)
        {
            try
            {
                // Llamada al método del DAO para actualizar la ProcedenciaDestino
                return prodesDAO.ActualizarProcedenciaDestino(proDes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la ProcedenciaDestino: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarProcedenciaDestino(int idProDes)
        {
            try
            {
                // Llamada al método del DAO para eliminar la ProcedenciaDestino
                prodesDAO.EliminarProcedenciaDestino(idProDes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la ProcedenciaDestino: " + ex.Message);
            }
        }

        //
        public ProcedenciaDestino CountProcedencia()
        {
            try
            {
                // Llamada al método del DAO para obtener la cantidad de las ProcedenciaDestino
                return prodesDAO.CountProcedencia();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el numero de las ProcedenciaDestino: " + ex.Message);
                return null;
            }
        }
        
        //
        public ProcedenciaDestino ObtenerUltimoId()
        {
            try
            {
                // Llamada al método del DAO para obtener la cantidad de las ProcedenciaDestino
                return prodesDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el ult id de las ProcedenciaDestino: " + ex.Message);
                return null;
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return prodesDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion de la Procedencia en la base de datos: " + ex.Message);
                return false;
            }
        }

    }
}
