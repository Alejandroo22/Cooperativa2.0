using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Acces
{
    class Fallas
    {
        public int IdFalla { get; set; }
        public string DescripcionFalla { get; set; }
        public string PiezaReemplazada { get; set; }
        public DateTime FechaFalla { get; set; }
        public string AccionesTomadas { get; set; }
        public int IdMaquinaria { get; set; }
        public string NombreMaquinaria { get; set; }
        public string ObservacionFalla { get; set; }
    }
}
