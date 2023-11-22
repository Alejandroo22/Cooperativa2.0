using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class SubProducto
    {
        public int IdSubProducto { get; set; }
        public string NombreSubProducto { get; set; }
        public string DescripcionSubProducto { get; set; }
        public int IdCalidadCafe { get; set; }
        public string NombreCalidadCafe { get; set; }
        public int CountSubProducto { get; set; }
        public int LastId { get; set; }
    }
}
