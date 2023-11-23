using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping;

namespace sistema_modular_cafe_majada.controller.ProductController
{
    class CCafeController
    {

        private CalidadCafeDAO ccafeDAO;

        public CCafeController()
        {
            // Se inicia la instancia de la clase CalidadCafeDAO
            ccafeDAO = new CalidadCafeDAO();
        }

        public bool InsertarCalidad(CalidadCafe calidadCafe)
        {
            try
            {
                //Se realiza el llamado al metodo DAO para insertar
                return ccafeDAO.InsertarCalidadCafe(calidadCafe);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error durante la inserción de la calidad del café en la base de datos: " + ex.Message);
                return false;
            }
        }

        public List<CalidadCafe> ObtenerCalidades()
        {
            try
            {
                //se llama al metodo DAO para obtener las calidades
                return ccafeDAO.ObtenerCalidades();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener la lista de calidades de café: " + ex.Message);
                return new List<CalidadCafe>();
            }
        }
        
        //
        public List<CalidadCafe> BuscarCalidades(string buscar)
        {
            try
            {
                //se llama al metodo DAO para obtener las calidades
                return ccafeDAO.BuscarCalidades(buscar);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener la lista de calidades de café: " + ex.Message);
                return new List<CalidadCafe>();
            }
        }

        public CalidadCafe ObtenerNombreCalidad (string nomCalidad)
        {
            CalidadCafe calidad = new CalidadCafe();
            try
            {
                //llamada al metodo DAO para obtener los datos
                calidad = ccafeDAO.ObtenerNombreCalidad(nomCalidad);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los datos: " + ex.Message);
            }
            return calidad;
        }
        
        public CalidadCafe ObtenerIdCalidad (int idCalidad)
        {
            CalidadCafe calidad = new CalidadCafe();
            try
            {
                //llamada al metodo DAO para obtener los datos
                calidad = ccafeDAO.ObtenerIdCalidad(idCalidad);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los datos: " + ex.Message);
            }
            return calidad;
        }

        public CalidadCafe CountCalidad ()
        {
            CalidadCafe calidad = new CalidadCafe();
            try
            {
                //llamada al metodo DAO para obtener los datos
                calidad = ccafeDAO.CountCalidades();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los totales de datos: " + ex.Message);
            }
            return calidad;
        }
        
        public CalidadCafe ObtenerUltimoId()
        {
            CalidadCafe calidad = new CalidadCafe();
            try
            {
                //llamada al metodo DAO para obtener los datos
                calidad = ccafeDAO.ObtenerUltimoId();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio un error al obtener los ult id de calidades: " + ex.Message);
            }
            return calidad;
        }

        public void EliminarCalidades(int idCalidades)
        {
            try
            {
                // se realiza el llamado el metodo DAO para eliminar
                ccafeDAO.EliminarCalidad(idCalidades);
            }
            catch(Exception ex)
            {
                Console.WriteLine("OCurrio un error al eliminar una calidad de café: " + ex.Message);
            }
        }

        public bool ActualizarCalidades(int id,string calidad, string descrip)
        {
            try
            {
                //llamada al metodo DAO para actualizar
                return ccafeDAO.ActualizarCalidades(id, calidad, descrip);
            }
            catch(Exception ex)
            {
                Console.WriteLine("OCurrio un error al actualizar los datos: " + ex.Message);
                return false;
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return ccafeDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion de la Calidad Cafe en la base de datos: " + ex.Message);
                return false;
            }
        }

        public string ObtenerNombrePorCodigo(int codigo)
        {

            string nombre = "Código no encontrado";

            try
            {
                // Llamada al método del DAO para obtener el nombre por el código
                nombre = ccafeDAO.ObtenerNombrePorCodigo(codigo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la ejecución: " + ex.Message);
            }

            return nombre;
        }

    }
}
