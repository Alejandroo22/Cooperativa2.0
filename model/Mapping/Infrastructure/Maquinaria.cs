using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Infrastructure
{
    class Maquinaria
    {
        public int IdMaquinaria { get; set; }
        public string NombreMaquinaria { get; set; }
        public string NumeroSerieMaquinaria { get; set; }
        public string ModeloMaquinaria { get; set; }
        public double CapacidadMaxMaquinaria { get; set; }
        public string ProveedorMaquinaria { get; set; }
        public string DireccionProveedorMaquinaria { get; set; }
        public string TelefonoProveedorMaquinaria { get; set; }
        public string ContratoServicioMaquinaria { get; set; }
        public int IdBeneficio { get; set; }
        public string NombreBeneficio { get; set; }
        public int CountMaquina{ get; set; }
        public int LastId{ get; set; }
    }

}
