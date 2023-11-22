using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class SubProductoController
    {
        private SubProductoDAO sproductoDAO;

        public SubProductoController()
        {
            sproductoDAO = new SubProductoDAO();
        }

        // Función para obtener todos los nombres de los subproductos
        public List<SubProducto> ObtenerNombreSubProductos()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de los subproductos
                return sproductoDAO.ObtenerNombreSubProductos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de subproductos: " + ex.Message);
                return new List<SubProducto>();
            }
        }

        // Función para obtener total de los subproductos
        public SubProducto CountSubProducto()
        {
            try
            {
                // Llamada al método del DAO para obtener el total de los subproductos
                return sproductoDAO.CountSubProducto();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el total de subproductos: " + ex.Message);
                return new SubProducto();
            }
        }
        
        // Función para obtener total de los subproductos
        public SubProducto ObtenerUltimoId()
        {
            try
            {
                // Llamada al método del DAO para obtener el total de los subproductos
                return sproductoDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el ult id de subproductos: " + ex.Message);
                return new SubProducto();
            }
        }

        // Función para insertar un nuevo subproducto
        public bool InsertarSubProducto(SubProducto subproducto)
        {
            try
            {
                // Llamada al método del DAO para insertar el subproducto
                return sproductoDAO.InsertarSubProducto(subproducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción en la base de datos: " + ex.Message);
                return false;
            }
        }

        // Función para obtener todos los subproductos
        public List<SubProducto> ObtenerSubProductos()
        {
            try
            {
                // Llamada al método del DAO para obtener todos los subproductos
                return sproductoDAO.ObtenerSubProductos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los datos: " + ex.Message);
                return new List<SubProducto>();
            }
        }

        // Función para obtener un subproducto por su ID
        public SubProducto ObtenerSubProductoPorId(int idSubProducto)
        {
            try
            {
                // Llamada al método del DAO para obtener el subproducto por ID
                return sproductoDAO.ObtenerSubProductoPorId(idSubProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el subproducto: " + ex.Message);
                return null;
            }
        }
        
        // Función para obtener un subproducto por su ID
        public List<SubProducto> ObtenerSubProductoPorIdCalidad(int idCalidad)
        {
            try
            {
                // Llamada al método del DAO para obtener el subproducto por ID
                return sproductoDAO.ObtenerSubProductoPorIdCalidad(idCalidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el subproducto: " + ex.Message);
                return new List<SubProducto>();
            }
        }

        // Función para obtener un subproducto por su nombre
        public SubProducto ObtenerSubProductoPorNombre(string nombreSubProducto)
        {
            try
            {
                // Llamada al método del DAO para obtener el subproducto por nombre
                return sproductoDAO.ObtenerSubProductoPorNombre(nombreSubProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el subproducto: " + ex.Message);
                return null;
            }
        }

        // Función para buscar subproductos por su nombre o el nombre de su calidad de café
        public List<SubProducto> BuscarSubProducto(string buscar)
        {
            try
            {
                // Llamada al método del DAO para buscar subproductos por nombre o calidad de café
                return sproductoDAO.BuscarSubProducto(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al buscar los subproductos: " + ex.Message);
                return new List<SubProducto>();
            }
        }

        // Función para actualizar un subproducto
        public bool ActualizarSubProducto(SubProducto subProducto)
        {
            try
            {
                // Llamada al método del DAO para actualizar el subproducto
                return sproductoDAO.ActualizarSubProducto(subProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar los datos: " + ex.Message);
                return false;
            }
        }

        // Función para eliminar un subproducto por su ID
        public void EliminarSubProducto(int idSubProducto)
        {
            try
            {
                // Llamada al método del DAO para eliminar el subproducto por ID
                sproductoDAO.EliminarSubProducto(idSubProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el registro: " + ex.Message);
            }
        }

        //
        public bool ExisteSubProducto(string nombre, int idC)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return sproductoDAO.ExisteSubProducto(nombre, idC);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del SubProducto en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return sproductoDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del SubProducto en la base de datos: " + ex.Message);
                return false;
            }
        }

    }

}
