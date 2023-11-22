using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.InfrastructureController
{
    class MaquinariaController
    {
        private MaquinariaDAO maquinariaDAO;

        public MaquinariaController()
        {
            // Inicializa la instancia de la clase 
            maquinariaDAO = new MaquinariaDAO();
        }

        //
        public List<Maquinaria> ObtenerMaquinariaNombreBeneficio()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Maquinaria
                return maquinariaDAO.ObtenerMaquinariaNombreBeneficio();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Maquinaria: " + ex.Message);
                return new List<Maquinaria>();
            }
        }

        //
        public Maquinaria ObtenerIdMaquinaria(int idMaquinaria)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Maquinaria
                return maquinariaDAO.ObtenerIdMaquinaria(idMaquinaria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Maquinaria: " + ex.Message);
                return null;
            }
        }

        //
        public Maquinaria ObtenerNombreMaquinaria(string nombodega)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Maquinaria
                return maquinariaDAO.ObtenerMaquinariaNombre(nombodega);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Maquinaria: " + ex.Message);
                return null;
            }
        }
        
        //
        public Maquinaria CountMaquinaria()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Maquinaria
                return maquinariaDAO.CountMaquinaria();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Maquinaria: " + ex.Message);
                return null;
            }
        }
        
        //
        public Maquinaria ObtenerUltimoId()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Maquinaria
                return maquinariaDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la Maquinaria: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarMaquinaria(Maquinaria Maquinaria)
        {
            try
            {
                // Llamada al método del DAO para insertar la Maquinaria
                return maquinariaDAO.InsertarMaquinaria(Maquinaria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de Maquinaria en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Maquinaria> ObtenerMaquinaria()
        {
            try
            {
                // Llamada al método del DAO para obtener las Maquinaria
                return maquinariaDAO.ObtenerMaquinarias();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Maquinaria: " + ex.Message);
                return new List<Maquinaria>();
            }
        }

        //
        public List<Maquinaria> BuscarMaquinaria(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Maquinaria
                return maquinariaDAO.BuscarMaquinaria(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Maquinaria: " + ex.Message);
                return new List<Maquinaria>();
            }
        }

        //
        public bool ActualizarMaquinaria(int idMaquinaria, string nombreMaquinaria, string numeroSerieMaquinaria,
                                 string modeloMaquinaria, double capacidadMax, string proveedorMaquinaria,
                                 string direccionProveedorMaquinaria, string telefonoProveedorMaquinaria,
                                 string contratoServicioMaquinaria, int idBeneficioMaquinaria)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Maquinaria
                return maquinariaDAO.ActualizarMaquinaria(idMaquinaria, nombreMaquinaria, numeroSerieMaquinaria, modeloMaquinaria, capacidadMax,
                                                            proveedorMaquinaria, direccionProveedorMaquinaria, telefonoProveedorMaquinaria, contratoServicioMaquinaria, idBeneficioMaquinaria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la Maquinaria: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarMaquinaria(int idMaquinaria)
        {
            try
            {
                // Llamada al método del DAO para eliminar la Maquinaria
                maquinariaDAO.EliminarMaquinaria(idMaquinaria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la Maquinaria: " + ex.Message);
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return maquinariaDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion de la Maquinaria en la base de datos: " + ex.Message);
                return false;
            }
        }

    }
}
