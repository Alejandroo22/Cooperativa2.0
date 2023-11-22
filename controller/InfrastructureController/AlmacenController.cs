using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.InfrastructureController
{
    class AlmacenController
    {
        private AlmacenDAO almacenDAO;

        public AlmacenController()
        {
            // Inicializa la instancia de la clase 
            almacenDAO = new AlmacenDAO();
        }

        //
        public List<Almacen> ObtenerAlmacenNombreBodega()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacen
                return almacenDAO.ObtenerAlmacenNombreBodega();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Almacen>();
            }
        }
        
        //
        public List<Almacen> ObtenerAlmacenNombreCalidadBodega()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacen
                return almacenDAO.ObtenerAlmacenNombreCalidadBodega();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Almacen>();
            }
        }

        //
        public Almacen ObtenerIdAlmacen(int idAlmacen)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacens
                return almacenDAO.ObtenerIdAlmacen(idAlmacen);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Almacens: " + ex.Message);
                return null;
            }
        }

        //
        public Almacen ObtenerNombreAlmacen(string nombodega)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacens
                return almacenDAO.ObtenerAlmacenNombre(nombodega);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Almacens: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarAlmacen(Almacen almacen)
        {
            try
            {
                // Llamada al método del DAO para insertar la almacen
                return almacenDAO.InsertarAlmacen(almacen);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de Almacen en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Almacen> ObtenerAlmacens()
        {
            try
            {
                // Llamada al método del DAO para obtener las Almacens
                return almacenDAO.ObtenerAlmacenes();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Almacen>();
            }
        }

        //
        public List<Almacen> BuscarAlmacens(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Almacens
                return almacenDAO.BuscarAlmacen(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Almacen>();
            }
        }
        
        //
        public List<Almacen> BuscarIDBodegaAlmacens(int buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Almacens
                return almacenDAO.BuscarIDBodegaAlmacen(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Almacen>();
            }
        }

        //
        public bool ActualizarAlmacens(int idAlmacen, string nombre, string descripcion, double capacidad, string ubicacion, int idBodega)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Almacens
                return almacenDAO.ActualizarAlmacen(idAlmacen, nombre, descripcion, capacidad, ubicacion, idBodega);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Almacens: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarAlmacens(int idAlmacens)
        {
            try
            {
                // Llamada al método del DAO para eliminar la Almacens
                almacenDAO.EliminarAlmacen(idAlmacens);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la Almacens: " + ex.Message);
            }
        }
        
        //
        public bool ActualizarCantidadEntradaCafeAlmacen(int idAlmacen, double cantidad, double cantidadSaco, int iCalidad, int iSubProd)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Almacens
                return almacenDAO.ActualizarCantidadEntradaCafeAlmacen(idAlmacen, cantidad, cantidadSaco, iCalidad, iSubProd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar las Cantidades Almacens: " + ex.Message);
                return false;
            }
        }
        
        //
        public bool ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(int idAlmacen, double cantidadNu, double cantidadNuSaco, int iCalidad, int iSubPro)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Almacens
                return almacenDAO.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(idAlmacen, cantidadNu, cantidadNuSaco, iCalidad, iSubPro );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar las Cantidades Almacens: " + ex.Message);
                return false;
            }
        }
        
        //
        public bool ActualizarCalidadAlmacen(int iCalidad, int iAlmacen)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Almacens
                return almacenDAO.ActualizarCalidadAlmacen(iCalidad, iAlmacen);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Calidad Almacens: " + ex.Message);
                return false;
            }
        }

        //
        public Almacen ObtenerCantidadCafeAlmacen(int iAlmacen)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de las cantidades
                return almacenDAO.ObtenerCantidadCafeAlmacen(iAlmacen);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad: " + ex.Message);
                return null;
            }
        }
        
        //
        public Almacen ObtenerAlmacenNombreCalidad(int iAlmacen)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre del Almacens
                return almacenDAO.ObtenerAlmacenNombreCalidad(iAlmacen);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad: " + ex.Message);
                return null;
            }
        }

        //
        public List<Almacen> ObtenerPorIDAlmacenNombreCalidadBodega(int id)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacen
                return almacenDAO.ObtenerPorIDAlmacenNombreCalidadBodega(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Almacen>();
            }
        }

        //
        public List<Almacen> BuscarIDBodegaAlmacenCalidad(int buscar, int id)
        {
            try
            {
                // Llamada al método del DAO para obtener las Almacens
                return almacenDAO.BuscarIDBodegaAlmacenCalidad(buscar, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Almacens: " + ex.Message);
                return new List<Almacen>();
            }
        }

        public Almacen CountExistenceCofeeAlmacen(int id)
        {
            try
            {
                //se realiza el llamado al metodo DAO para obtener las existencias de cafe por calidad en los almacenes
                return almacenDAO.CountExistenceCofee(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener la lista total de Existencias de cafe por calidad: " + ex.Message);
                return null;
            }
        }

        public Almacen CountAlmacen()
        {
            try
            {
                //se realiza el llamado al metodo DAO para obtener las existencias de los almacenes
                return almacenDAO.CountAlmacen();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener la lista total de Existencias de almacen: " + ex.Message);
                return null;
            }
        }

        //
        public bool ExisteAlmacen(string almacen, int idB)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return almacenDAO.ExisteAlmacen(almacen, idB);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del Almacen en la base de datos: " + ex.Message);
                return false;
            }
        }

        public Almacen ObtenerUltimoId()
        {
            try
            {
                //se realiza el llamado al metodo DAO para obtener las existencias de los almacenes
                return almacenDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener el ultimo id de almacen: " + ex.Message);
                return null;
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return almacenDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del Almacen en la base de datos: " + ex.Message);
                return false;
            }
        }


    }
}
