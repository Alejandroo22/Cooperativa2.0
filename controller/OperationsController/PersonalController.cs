using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.OperationsController
{
    class PersonalController
    {
        private PersonalDAO personalDAO;

        public PersonalController()
        {
            // Inicializa la instancia de la clase 
            personalDAO = new PersonalDAO();
        }

        //
        public List<Personal> ObtenerPersonals()
        {
            List<Personal> personals = new List<Personal>();

            try
            {
                // Llamada al método del DAO para obtener los Personales
                personals = personalDAO.ObtenerPersonales();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Personales: " + ex.Message);
            }

            return personals;
        }
        
        //
        public List<Personal> ObtenerPersonalesNombreCargo()
        {
            List<Personal> personals = new List<Personal>();

            try
            {
                // Llamada al método del DAO para obtener los Personales
                personals = personalDAO.ObtenerPersonalesNombreCargo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Personales: " + ex.Message);
            }

            return personals;
        }
        
        //
        public List<Personal> ObtenerPersonalEspecificoNombreCargo(string nombre)
        {
            List<Personal> personals = new List<Personal>();

            try
            {
                // Llamada al método del DAO para obtener los Personales
                personals = personalDAO.ObtenerPersonalEspecificoNombreCargo(nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Personales: " + ex.Message);
            }

            return personals;
        }
        
        //
        public List<Personal> BuscarPersonal(string buscar)
        {
            List<Personal> personals = new List<Personal>();

            try
            {
                // Llamada al método del DAO para obtener los Personales
                personals = personalDAO.BuscarPersonal(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Personales: " + ex.Message);
            }

            return personals;
        }
        
        //
        public List<Personal> BuscarPersonalCargo(string buscar)
        {
            List<Personal> personals = new List<Personal>();

            try
            {
                // Llamada al método del DAO para obtener los Personales
                personals = personalDAO.BuscarPersonalCargo(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener los Personales: " + ex.Message);
            }

            return personals;
        }

        //
        public Personal ObtenerPersonalNombre(string nombre)
        {
            Personal personal = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                personal = personalDAO.ObtenerPersonalNombre(nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el rol: " + ex.Message);
            }

            return personal;
        }

        //
        public bool InsertarPersonal(Personal personal)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para insertar el rol
                exito = personalDAO.InsertarPersonal(personal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción del rol: " + ex.Message);
            }

            return exito;
        }

        //
        public bool ActualizarPersonal(int id, string nombre, int icargo, string descripcion, int idPersona)
        {
            bool exito = false;

            try
            {
                // Llamada al método del DAO para actualizar el rol
                exito = personalDAO.ActualizarPersonal(id, nombre, icargo, descripcion, idPersona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la actualización del Personal: " + ex.Message);
            }

            return exito;
        }

        //
        public void EliminarPersonal(int id)
        {
            try
            {
                // Llamada al método del DAO para eliminar el rol
                personalDAO.EliminarPersonal(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar el rol: " + ex.Message);
            }
        }

        //
        public Personal ObtenerIPersonal(int id)
        {
            Personal personal = null;

            try
            {
                // Llamada al método del DAO para obtener el rol
                personal = personalDAO.ObtenerIdPersonal(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener el Personal: " + ex.Message);
            }

            return personal;
        }
        public Personal ObtenerNombrePorCodigo(int codigo)
        {
            Personal Idpersonal = new Personal();
            try
            {
                Idpersonal = personalDAO.ObtenerNombrePorCodigo(codigo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la verificacion del Personal en la base de datos: " + ex.Message);
            }
            return Idpersonal;
        }

    }
}
