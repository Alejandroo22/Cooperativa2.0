using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.InfrastructureController
{
    class BodegaController
    {
        private BodegaDAO bodegaDAO;

        public BodegaController()
        {
            // Inicializa la instancia de la clase 
            bodegaDAO = new BodegaDAO();
        }

        //
        public List<Bodega> ObtenerBodegaNombreBeneficio()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Bodega
                return bodegaDAO.ObtenerBodegaNombreBeneficio();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Bodegas: " + ex.Message);
                return new List<Bodega>();
            }
        }

        //
        public List<Bodega> ObtenerBodegas()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Bodega
                return bodegaDAO.ObtenerBodegas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Bodegas: " + ex.Message);
                return new List<Bodega>();
            }
        }

        //
        public Bodega ObtenerIdBodega(int idBodega)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Bodegas
                return bodegaDAO.ObtenerIdBodega(idBodega);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Bodegas: " + ex.Message);
                return null;
            }
        }
        
        //
        public Bodega CountBodega()
        {
            try
            {
                // Llamada al método del DAO para obtener cantidad Bodegas
                return bodegaDAO.CountBodega();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Bodegas: " + ex.Message);
                return null;
            }
        }
        
        //
        public Bodega ObtenerUltimoId()
        {
            try
            {
                // Llamada al método del DAO para obtener cantidad Bodegas
                return bodegaDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el ultimo id de la Bodega: " + ex.Message);
                return null;
            }
        }

        //
        public Bodega ObtenerNombreBodega(string nombodega)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Bodegas
                return bodegaDAO.ObtenerBodegaNombre(nombodega);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Bodegas: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarBodega(Bodega bodega)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return bodegaDAO.InsertarBodega(bodega);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de Bodega en la base de datos: " + ex.Message);
                return false;
            }
        }
        
        //
        public bool ExisteBodega(string bodega, int idBenef)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return bodegaDAO.ExisteBodega(bodega, idBenef);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion de Bodega en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Bodega> BuscarBodegas(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Bodegas
                return bodegaDAO.BuscarBodega(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Bodegas: " + ex.Message);
                return new List<Bodega>();
            }
        }

        //
        public bool ActualizarBodegas(int idBodega, string nombre, string descripcion, string ubicacion, int idBeneficio)
        {
            Console.WriteLine(" " + idBodega);
            Console.WriteLine(" " + nombre);
            Console.WriteLine(" " + descripcion);
            Console.WriteLine(" " + ubicacion);
            Console.WriteLine(" " + idBeneficio);
            try
            {
                // Llamada al método del DAO para actualizar la Bodegas
                return bodegaDAO.ActualizarBodega(idBodega, nombre, descripcion, ubicacion, idBeneficio);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Bodegas: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarBodegas(int idBodegas)
        {
            try
            {
                // Llamada al método del DAO para eliminar la Bodegas
                bodegaDAO.EliminarBodega(idBodegas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la Bodegas: " + ex.Message);
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return bodegaDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion de la Bodega en la base de datos: " + ex.Message);
                return false;
            }
        }

    }
}
