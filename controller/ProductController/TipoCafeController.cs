using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.ProductController
{
    class TipoCafeController
    {
        private TipoCafeDAO tipoCafeDAO;

        public TipoCafeController()
        {
            // Inicializa la instancia de la clase 
            tipoCafeDAO = new TipoCafeDAO();
        }

        //
        public List<TipoCafe> ObtenerTipoCafes()
        {
            List<TipoCafe> tipoCafes = new List<TipoCafe>();

            try
            {
                // Llamada al método del DAO para obtener los roles
                tipoCafes = tipoCafeDAO.ObtenerTipoCafes();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Tipos de Cafe: " + ex.Message);
            }

            return tipoCafes;
        }
        
        //
        public List<TipoCafe> BuscadorTipoCafes(string buscar)
        {
            List<TipoCafe> tipoCafes = new List<TipoCafe>();

            try
            {
                // Llamada al método del DAO para obtener los roles
                tipoCafes = tipoCafeDAO.BuscadorTipoCafes(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Tipos de Cafe: " + ex.Message);
            }

            return tipoCafes;
        }

        //
        public TipoCafe ObtenerTipoCafeNombre(string nombre)
        {
            TipoCafe tipoCafe = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                tipoCafe = tipoCafeDAO.ObtenerTipoCafeNombre(nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el Tipo de Cafe: " + ex.Message);
            }

            return tipoCafe;
        }

        //
        public TipoCafe CountTipoCafe()
        {
            TipoCafe tipoCafe = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                tipoCafe = tipoCafeDAO.CountTipoCafe();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el total de Tipo de Cafe: " + ex.Message);
            }

            return tipoCafe;
        }
        
        //
        public TipoCafe ObtenerUltimoId()
        {
            TipoCafe tipoCafe = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                tipoCafe = tipoCafeDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el ult id de Tipo de Cafe: " + ex.Message);
            }

            return tipoCafe;
        }

        //
        public bool InsertarTipoCafe(TipoCafe tipoCafe)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para insertar el rol
                exito = tipoCafeDAO.InsertarTipoCafe(tipoCafe);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción del Tipo de Cafe: " + ex.Message);
            }

            return exito;
        }

        //
        public bool ActualizarTipoCafe(int id, string nombre, string ubicacion)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para actualizar el rol
                exito = tipoCafeDAO.ActualizarTipoCafe(id, nombre, ubicacion);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la actualización del tipoCafe: " + ex.Message);
            }

            return exito;
        }

        //
        public void EliminarTipoCafe(int id)
        {
            try
            {
                // Llamada al método del DAO para eliminar el rol
                tipoCafeDAO.EliminarTipoCafe(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar el Tipo de Cafe: " + ex.Message);
            }
        }

        //
        public TipoCafe ObtenerITipoCafe(int id)
        {
            TipoCafe tipoCafe = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                tipoCafe = tipoCafeDAO.ObtenerIdTipoCafe(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el Tipo de Cafe: " + ex.Message);
            }

            return tipoCafe;
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return tipoCafeDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del Tipo Cafe en la base de datos: " + ex.Message);
                return false;
            }
        }

    }
}
