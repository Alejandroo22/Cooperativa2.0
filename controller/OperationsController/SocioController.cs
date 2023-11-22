using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class SocioController
    {
        private SocioDAO socioDAO;

        public SocioController()
        {
            // Inicializa la instancia de la clase 
            socioDAO = new SocioDAO();
        }

        //
        public List<Socio> ObtenerSocios()
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacen
                return socioDAO.ObtenerSocios();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Socios: " + ex.Message);
                return new List<Socio>();
            }
        }

        //
        public Socio ObtenerSocioPorId(int idSocio)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacens
                return socioDAO.ObtenerSocioPorId(idSocio);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Socio: " + ex.Message);
                return null;
            }
        }

        //
        public Socio ObtenerSocioNombre(string nombre)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la Almacens
                return socioDAO.ObtenerSocioNombre(nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Socio: " + ex.Message);
                return null;
            }
        }
        
        //
        public Socio CountSocio()
        {
            try
            {
                // Llamada al método del DAO para obtener la cantidad socio
                return socioDAO.CountSocio();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el Socio: " + ex.Message);
                return null;
            }
        }
        
        //
        public Socio ObtenerUltimoId()
        {
            try
            {
                // Llamada al método del DAO para obtener la cantidad socio
                return socioDAO.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el ult id Socio: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarSocio(Socio socio)
        {
            try
            {
                // Llamada al método del DAO para insertar la almacen
                return socioDAO.InsertarSocio(socio);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción del Socio en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Socio> ObtenerSocioNombrePersona()
        {
            try
            {
                // Llamada al método del DAO para obtener las Almacens
                return socioDAO.ObtenerSocioNombrePersona();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Socios: " + ex.Message);
                return new List<Socio>();
            }
        }

        //
        public List<Socio> BuscarSocio(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las Almacens
                return socioDAO.BuscarSocio(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de Socios: " + ex.Message);
                return new List<Socio>();
            }
        }

        //
        public bool ActualizarSocio(int idSocio, string nombre, string descripcion, string ubicacion, int idPersonaResp, int iFinca)
        {
            try
            {
                // Llamada al método del DAO para actualizar la Almacens
                return socioDAO.ActualizarSocio(idSocio, nombre, descripcion, ubicacion, idPersonaResp, iFinca);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar el Socio: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarSocio(int idSocio)
        {
            try
            {
                // Llamada al método del DAO para eliminar la Almacens
                socioDAO.EliminarSocio(idSocio);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar el Socio: " + ex.Message);
            }
        }

        //
        public bool ExisteId(int id)
        {
            try
            {
                // Llamada al método del DAO para insertar la Bodega
                return socioDAO.ExisteId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del Socio en la base de datos: " + ex.Message);
                return false;
            }
        }

    }
}
