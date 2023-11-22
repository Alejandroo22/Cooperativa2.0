using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.controller.UserDataController
{
    class PersonController
    {
        private PersonDAO personaDAO;

        public PersonController()
        {
            // Inicializa la instancia de la clase 
            personaDAO = new PersonDAO();
        }

        //
        public Persona ObtenerNombrePersona(int idPersona)
        {
            try
            {
                // Llamada al método del DAO para obtener el nombre de la persona
                return personaDAO.ObtenerNombrePersona(idPersona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la persona: " + ex.Message);
                return null;
            }
        }

        //
        public bool InsertarPersona(Persona persona)
        {
            try
            {
                // Llamada al método del DAO para insertar la persona
                return personaDAO.InsertarPersona(persona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error durante la inserción de persona en la base de datos: " + ex.Message);
                return false;
            }
        }

        //
        public List<Persona> ObtenerPersonas()
        {
            try
            {
                // Llamada al método del DAO para obtener las personas
                return personaDAO.ObtenerPersonas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de personas: " + ex.Message);
                return new List<Persona>();
            }
        }

        //
        public List<Persona> BuscarPersonas(string buscar)
        {
            try
            {
                // Llamada al método del DAO para obtener las personas
                return personaDAO.BuscarPersonas(buscar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la lista de personas: " + ex.Message);
                return new List<Persona>();
            }
        }

        //
        public Persona ObtenerPersona(string nombrePersona)
        {
            try
            {
                // Llamada al método del DAO para obtener la persona
                return personaDAO.ObtenerPersona(nombrePersona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al obtener la persona: " + ex.Message);
                return null;
            }
        }

        //
        public bool ActualizarPersona(int id, string nombres, string apellidos, string direccion, DateTime fechaNacimiento, string nit, string dui, string tel1, string tel2)
        {
            try
            {
                // Llamada al método del DAO para actualizar la persona
                return personaDAO.ActualizarPersona(id, nombres, apellidos, direccion, fechaNacimiento, nit, dui, tel1, tel2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al actualizar la persona: " + ex.Message);
                return false;
            }
        }

        //
        public void EliminarPersona(int idPersona)
        {
            try
            {
                // Llamada al método del DAO para eliminar la persona
                personaDAO.EliminarPersona(idPersona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al eliminar la persona: " + ex.Message);
            }
        }

        //

    }
}
