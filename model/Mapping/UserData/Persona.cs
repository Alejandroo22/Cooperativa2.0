using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.UserData
{
    class Persona
    {
        public int IdPersona { get; set; }
        public string NombresPersona { get; set; }
        public string ApellidosPersona { get; set; }
        public string DireccionPersona { get; set; }
        public DateTime FechaNacimientoPersona { get; set; }
        public string NitPersona { get; set; }
        public string DuiPersona { get; set; }
        public string Telefono1Persona { get; set; }
        public string Telefono2Persona { get; set; }
    }

    public static class PersonSelect
    {
        public static int IdPerson { get; set; }
        public static string NamePerson { get; set; }
    }
}
