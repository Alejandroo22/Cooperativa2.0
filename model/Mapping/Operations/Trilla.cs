using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class Trilla
    {
        public int IdTrilla_cafe { get; set; }
        public int IdCosecha { get; set; }
        public int NumTrilla { get; set; }
        public string NombreCosecha { get; set; }

        public int IdProcedencia { get; set; }
        public string NombreProcedencia { get; set; }
        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
        public int IdBodega { get; set; }
        public string NombreBodega { get; set; }

        public int IdCalidadCafe { get; set; }
        public string NombreCalidadCafe { get; set; }
        public int IdSubProducto { get; set; }
        public string NombreSubProducto { get; set; }
        public string TipoMovimientoTrilla { get; set; }
        public double CantidadTrillaQQs { get; set; }
        public double CantidadTrillaSacos { get; set; }
        public DateTime FechaTrillaCafe { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ObservacionTrilla { get; set; }
        public int CountTrilla { get; set; }
    }

    public static class TablaSeleccionadaTrilla
    {
        public static int ITable { get; set; }
    }

    public static class TrillaSeleccionado
    {
        public static int ITrilla { get; set; }
        public static int NumTrilla { get; set; }
        public static bool clickImg { get; set; }
    }
    class ReportesTrilla
    {
        public int IdTrilla_cafe { get; set; }
        public int IdCosecha { get; set; }
        public int NumTrilla { get; set; }
        public string NombreCosecha { get; set; }

        public int IdProcedencia { get; set; }
        public string NombreProcedencia { get; set; }
        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
        public int IdBodega { get; set; }
        public string NombreBodega { get; set; }

        public int IdCalidadCafe { get; set; }
        public string NombreCalidadCafe { get; set; }
        public int IdSubProducto { get; set; }
        public string NombreSubProducto { get; set; }
        public string TipoMovimientoTrilla { get; set; }
        public double CantidadTrillaQQs { get; set; }
        public double CantidadTrillaSacos { get; set; }
        public string FechaTrillaCafe { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ObservacionTrilla { get; set; }
        public string nombre_persona { get; set; }
    }
}
