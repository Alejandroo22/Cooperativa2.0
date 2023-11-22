using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Infrastructure
{
    class Almacen
    {
        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
        public string DescripcionAlmacen { get; set; }
        public double CapacidadAlmacen { get; set; }
        public double CantidadActualAlmacen { get; set; }
        public double CantidadActualSacoAlmacen { get; set; }
        public string UbicacionAlmacen { get; set; }
        public int IdBodegaUbicacion { get; set; }
        public string NombreBodegaUbicacion { get; set; }
        public int? IdCalidadCafe { get; set; }
        public string NombreCalidadCafe { get; set; }
        public int? IdSubProducto { get; set; }
        public string NombreSubProducto { get; set; }
        public int CountExistenceCoffe { get; set; }
        public int CountAlmacen { get; set; }
        public int LastId { get; set; }
    }

    public static class AlmacenSeleccionado
    {
        public static int IAlmacen { get; set; }
        public static string NombreAlmacen { get; set; }
        public static int IAlmacenDestino { get; set; }
        public static string NombreAlmacenDestino { get; set; }
    }

    public static class AlmacenBodegaClick
    {
        public static int IBodega { get; set; }
        public static int IBodegaDestino { get; set; }
        //public static string NombreAlmacen { get; set; }
    }
}
