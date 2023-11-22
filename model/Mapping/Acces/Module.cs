using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Acces
{
    class Module
    {
        public int IdModule { get; set; }
        public string NombreModulo { get; set; }
    }

    public static class ModuloActual
    {
        public static string NombreModulo { get; set; }
    }
}
