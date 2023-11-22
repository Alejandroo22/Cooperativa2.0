using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.HarvestController
{
    class LoteController
    {
        private LoteDAO loteDAO;

        public LoteController()
        {
            // Inicializa la instancia de la clase 
            loteDAO = new LoteDAO();
        }

        //
        public List<Lote> ObtenerLotes()
        {
            List<Lote> lotes = new List<Lote>();

            try
            {
                // Llamada al método del DAO para obtener los roles
                lotes = loteDAO.ObtenerLotes();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los roles: " + ex.Message);
            }

            return lotes;
        }
        
        //
        public List<Lote> ObtenerLotesNombreID()
        {
            List<Lote> lotes = new List<Lote>();

            try
            {
                // Llamada al método del DAO para obtener los roles
                lotes = loteDAO.ObtenerLotesNombreID();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los roles: " + ex.Message);
            }

            return lotes;
        }

        //
        public Lote ObtenerLoteNombre(string nombre)
        {
            Lote lote = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                lote = loteDAO.ObtenerLoteNombre(nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el rol: " + ex.Message);
            }

            return lote;
        }

        //
        public bool InsertarLote(Lote lote)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para insertar el rol
                exito = loteDAO.InsertarLote(lote);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción del rol: " + ex.Message);
            }

            return exito;
        }

        //
        public bool ActualizarLote(int id, string nombre, double cantidad, DateTime fecha, int idtipo, int idCalidad, int idCosecha, int idFinca)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para actualizar el rol
                exito = loteDAO.ActualizarLote(id, nombre, cantidad, fecha, idtipo, idCalidad, idCosecha, idFinca);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la actualización del Lote: " + ex.Message);
            }

            return exito;
        }

        //
        public void EliminarLote(int id)
        {
            try
            {
                // Llamada al método del DAO para eliminar el rol
                loteDAO.EliminarLote(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar el rol: " + ex.Message);
            }
        }

        //
        public Lote ObtenerILote(int id)
        {
            Lote lote = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                lote = loteDAO.ObtenerIdLote(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el rol: " + ex.Message);
            }

            return lote;
        }
    }
}
