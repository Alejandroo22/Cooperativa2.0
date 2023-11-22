using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class CantidadSiloPiña
    {
        public int IdCantidadCafe { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public double CantidadCafe { get; set; }
        public double CantidadCafeSaco { get; set; }
        public string TipoMovimiento { get; set; }
        public int IdAlmacenSiloPiña { get; set; }
        public string NombreAlmacen { get; set; }
        public int IdCosechaCantidad { get; set; }
        public string NombreCosechaCantidad { get; set; }
    }
}
