using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class CantidadSiloPiñaController
    {
        private CantidadSiloPiñaDAO cantidadDAO;

        public CantidadSiloPiñaController()
        {
            // Inicializa la instancia de la clase DAO
            cantidadDAO = new CantidadSiloPiñaDAO();
        }

        public bool InsertarCantidadCafeSiloPiña(CantidadSiloPiña cantidad)
        {
            try
            {
                // Llamada al método del DAO para insertar la cantidad de café en el silo/piña
                return cantidadDAO.InsertarCantidadCafeSiloPiña(cantidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de la cantidad de café en el silo/piña: " + ex.Message);
                return false;
            }
        }

        public List<CantidadSiloPiña> ObtenerCantidadesSiloPiña()
        {
            try
            {
                // Llamada al método del DAO para obtener todas las cantidades de café en el silo/piña
                return cantidadDAO.ObtenerCantidadesSiloPiña();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de cantidades de café en el silo/piña: " + ex.Message);
                return new List<CantidadSiloPiña>();
            }
        }

        public CantidadSiloPiña ObtenerIdCantidadSiloPiña(int idCantidad)
        {
            try
            {
                // Llamada al método del DAO para obtener una cantidad específica de café en el silo/piña por su ID
                return cantidadDAO.ObtenerIdCantidadSiloPiña(idCantidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad de café en el silo/piña por su ID: " + ex.Message);
                return null;
            }
        }

        public CantidadSiloPiña ObtenerCantidadSiloPiña(string nombreSiloPiña)
        {
            try
            {
                // Llamada al método del DAO para obtener una cantidad específica de café en el silo/piña por su nombre de almacén
                return cantidadDAO.ObtenerCantidadSiloPiña(nombreSiloPiña);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad de café en el silo/piña por su nombre de almacén: " + ex.Message);
                return null;
            }
        }
        
        public List<CantidadSiloPiña> ObtenerSubProductoSiloPiña(int idAlmacen)
        {
            try
            {
                // Llamada al método del DAO para obtener una cantidad específica de café en el silo/piña por su nombre de almacén
                return cantidadDAO.ObtenerListaSubProductosSiloPiña(idAlmacen);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad de café en el silo/piña por su nombre de almacén: " + ex.Message);
                return new List<CantidadSiloPiña>();
            }
        }

        public CantidadSiloPiña ObtenerCantidadSubProductoSiloPiña(int iCosecha, int iAlmacen, int iSubProducto)
        {
            try
            {
                // Llamada al método del DAO para obtener una cantidad específica de café en el silo/piña por su nombre de almacén
                return cantidadDAO.ObtenerCantidadSubProductoSiloPiña(iCosecha, iAlmacen, iSubProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad de café en el silo/piña por su subProducto: " + ex.Message);
                return null;
            }
        }

        public List<CantidadSiloPiña> ObtenerCantidadNombreSiloPiña()
        {
            try
            {
                // Llamada al método del DAO para obtener todas las cantidades de café en el silo/piña con los nombres de calidad y almacén
                return cantidadDAO.ObtenerCantidadNombreSiloPiña();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de cantidades de café en el silo/piña con los nombres de calidad y almacén: " + ex.Message);
                return new List<CantidadSiloPiña>();
            }
        }

        public List<CantidadSiloPiña> BuscarCantidadSiloPiña(string buscar)
        {
            try
            {
                // Llamada al método del DAO para buscar cantidades de café en el silo/piña por nombre de calidad o nombre de almacén
                return cantidadDAO.BuscarCantidadSiloPiña(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al buscar cantidades de café en el silo/piña: " + ex.Message);
                return new List<CantidadSiloPiña>();
            }
        }
        
        public CantidadSiloPiña BuscarCantidadSiloPiñaSub(string buscar)
        {
            try
            {
                // Llamada al método del DAO para buscar cantidades de café en el silo/piña por nombre de calidad o nombre de almacén
                return cantidadDAO.BuscarCantidadSiloPiñaSub(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al buscar cantidades de café en el silo/piña: " + ex.Message);
                return null;
            }
        }

        public bool ActualizarCantidadCafeSiloPiña(CantidadSiloPiña cantidad)
        {
            try
            {
                // Llamada al método del DAO para actualizar una cantidad de café en el silo/piña
                return cantidadDAO.ActualizarCantidadCafeSiloPiña(cantidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la cantidad de café en el silo/piña: " + ex.Message);
                return false;
            }
        }

        public void EliminarCantidadSiloPiña(int idCantidad)
        {
            try
            {
                // Llamada al método del DAO para eliminar una cantidad de café en el silo/piña por su ID
                cantidadDAO.EliminarCantidadSiloPiña(idCantidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la cantidad de café en el silo/piña por su ID: " + ex.Message);
            }
        }

        public bool VerificarRegistroExistenteDestino(int idAlmacen, int selectedSubproducto)
        {
            try
            {
                // Llamada al método del DAO para actualizar una cantidad de café en el silo/piña
                return cantidadDAO.VerificarRegistroExistente(idAlmacen, selectedSubproducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar registros existentes: " + ex.Message);
                return false;
            }
        }

    }

}
