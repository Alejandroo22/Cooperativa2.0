using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class Traslado
    {
        public int Idtraslado_cafe { get; set; }
        public int NumTraslado { get; set; }
        public int IdCosecha { get; set; }
        public string NombreCosecha { get; set; }
        public int IdProcedencia { get; set; }
        public string NombreProcedencia { get; set; }
        public int IdProcedenciaDestino { get; set; }
        public string NombreProcedenciaDestino { get; set; }
        public int IdAlmacenProcedencia { get; set; }
        public string NombreAlmacenProcedencia { get; set; }
        public int IdBodegaProcedencia { get; set; }
        public string NombreBodegaProcedencia { get; set; }
        public int IdAlmacenDestino { get; set; }
        public string NombreAlmacenDestino { get; set; }
        public int IdBodegaDestino { get; set; }
        public string NombreBodegaDestino { get; set; }
        public int IdCalidadCafe { get; set; }
        public string NombreCalidadCafe { get; set; }
        public int IdSubProducto { get; set; }
        public string NombreSubProducto { get; set; }
        public double CantidadTrasladoQQs { get; set; }
        public double CantidadTrasladoSacos { get; set; }
        public DateTime FechaTrasladoCafe { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ObservacionTraslado { get; set; }
        public int CountTraslado { get; set; }
    }

    public static class TablaSeleccionadaTraslado
    {
        public static int ITable { get; set; }
    }

    public static class TrasladoSeleccionado
    {
        public static int ITraslado { get; set; }
        public static int NumTraslado { get; set; }
        public static bool clickImg { get; set; }
    }
    class ReporteTraslado
    {
        public int Idtraslado_cafe { get; set; }
        public int NumTraslado { get; set; }
        public int IdCosecha { get; set; }
        public string NombreCosecha { get; set; }
        public int IdProcedencia { get; set; }
        public string NombreProcedencia { get; set; }
        public int IdProcedenciaDestino { get; set; }
        public string NombreProcedenciaDestino { get; set; }
        public int IdAlmacenProcedencia { get; set; }
        public string NombreAlmacenProcedencia { get; set; }
        public int IdBodegaProcedencia { get; set; }
        public string NombreBodegaProcedencia { get; set; }
        public int IdAlmacenDestino { get; set; }
        public string NombreAlmacenDestino { get; set; }
        public int IdBodegaDestino { get; set; }
        public string NombreBodegaDestino { get; set; }
        public int IdCalidadCafe { get; set; }
        public string NombreCalidadCafe { get; set; }
        public int IdSubProducto { get; set; }
        public string NombreSubProducto { get; set; }
        public double CantidadTrasladoQQs { get; set; }
        public double CantidadTrasladoSacos { get; set; }
        public string FechaTrasladoCafe { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ObservacionTraslado { get; set; }
        public string nombre_persona { get; set; }
    }
}
