using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.HarvestController
{
    class CosechaController
    {
        private CosechaDAO cosechaDAO;

        public CosechaController()
        {
            // Inicializa la instancia de la clase 
            cosechaDAO = new CosechaDAO();
        }

        //
        public List<Cosecha> ObtenerCosecha()
        {
            List<Cosecha> cosechas = new List<Cosecha>();

            try
            {
                // Llamada al método del DAO para obtener los Cosecha
                cosechas = cosechaDAO.ObtenerCosecha();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Cosecha: " + ex.Message);
            }

            return cosechas;
        }
        
        //
        public List<Cosecha> ObtenerCosechaDESC()
        {
            List<Cosecha> cosechas = new List<Cosecha>();

            try
            {
                // Llamada al método del DAO para obtener los Cosecha
                cosechas = cosechaDAO.ObtenerCosechaDESC();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener las Cosecha: " + ex.Message);
            }

            return cosechas;
        }
        
        //
        public Cosecha ObtenerCosechaUltima()
        {
            Cosecha cosechas = null;

            try
            {
                // Llamada al método del DAO para obtener los Cosecha
                cosechas = cosechaDAO.ObtenerCosechaUltima();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener las Cosecha: " + ex.Message);
            }

            return cosechas;
        }

        //
        public List<Cosecha> BuscarCosecha(string buscar)
        {
            List<Cosecha> cosechas = new List<Cosecha>();

            try
            {
                // Llamada al método del DAO para obtener los Cosecha
                cosechas = cosechaDAO.BuscarCosecha(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Cosecha: " + ex.Message);
            }

            return cosechas;
        }

        //
        public Cosecha ObtenerNombreCosecha(string nombre)
        {
            Cosecha cosecha = null;

            try
            {
                // Llamada al método del DAO para obtener el Cosecha
                cosecha = cosechaDAO.ObtenerNombreCosecha(nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la cosecha: " + ex.Message);
            }

            return cosecha;
        }

        //
        public bool InsertarCosecha(Cosecha cosecha)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para insertar el Cosecha
                exito = cosechaDAO.InsertarCosecha(cosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de la cosecha: " + ex.Message);
            }

            return exito;
        }

        //
        public bool ActualizarCosecha(int id, string nombre)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para actualizar el Cosecha
                exito = cosechaDAO.ActualizarCosecha(id, nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la actualización de la cosecha: " + ex.Message);
            }

            return exito;
        }

        //
        public void EliminarCosecha(int id)
        {
            try
            {
                // Llamada al método del DAO para eliminar el Cosecha
                cosechaDAO.EliminarCosecha(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la cosecha: " + ex.Message);
            }
        }

        //
        public Cosecha ObtenerICosecha(int id)
        {
            Cosecha cosecha = null;

            try
            {
                // Llamada al método del DAO para obtener el Cosecha
                cosecha = cosechaDAO.ObtenerIdCosecha(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el Cosecha: " + ex.Message);
            }

            return cosecha;
        }
        
        //
        public Cosecha CountCosecha()
        {
            Cosecha cosecha = null;

            try
            {
                // Llamada al método del DAO para obtener el Cosecha
                cosecha = cosechaDAO.CountCosecha();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el Cosecha: " + ex.Message);
            }

            return cosecha;
        }
        
        //
        public Cosecha ObtenerUltimoId()
        {
            Cosecha cosecha = null;

            try
            {
                // Llamada al método del DAO para obtener el Cosecha
                cosecha = cosechaDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el Cosecha: " + ex.Message);
            }

            return cosecha;
        }

    }
}
