using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class Salida
    {
        public int IdSalida_cafe { get; set; }
        public int NumSalida_cafe { get; set; }
        public int IdCosecha { get; set; }
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
        public string TipoSalida { get; set; }
        public double CantidadSalidaQQs { get; set; }
        public double CantidadSalidaSacos { get; set; }
        public DateTime FechaSalidaCafe { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ObservacionSalida { get; set; }
        public int CountSalida { get; set; }
    }

    public static class TablaSeleccionadaSalida
    {
        public static int ITable { get; set; }
    }

    public static class SalidaSeleccionado
    {
        public static int ISalida { get; set; }
        public static int NumSalida { get; set; }
        public static bool clickImg { get; set; }
    }
    class ReportSalida
    {
        public int IdSalida_cafe { get; set; }
        public int NumSalida_cafe { get; set; }
        public int IdCosecha { get; set; }
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
        public string TipoSalida { get; set; }
        public double CantidadSalidaQQs { get; set; }
        public double CantidadSalidaSacos { get; set; }
        public string FechaSalidaCafe { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ObservacionSalida { get; set; }
        public string nombre_persona { get; set; }

    }
}
