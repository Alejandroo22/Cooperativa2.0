using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class SubPartida
    {
        public int IdSubpartida { get; set; }
        public int NumeroSubpartida { get; set; }
        public int IdCosecha { get; set; }
        public string NombreCosecha { get; set; }
        public int IdProcedencia { get; set; } 
        public string NombreProcedencia { get; set; }
        public int IdCalidadCafe { get; set; } 
        public string NombreCalidadCafe { get; set; }
        public int IdSubProducto { get; set; } 
        public string NombreSubProducto { get; set; }
        public int Num1Semana { get; set; } 
        public int Num2Semana { get; set; } 
        public int Num3Semana { get; set; } 
        public int Dias1SubPartida { get; set; } 
        public int Dias2SubPartida { get; set; } 
        public int Dias3SubPartida { get; set; } 
        public string Fecha1SubPartida { get; set; }
        public string Fecha2SubPartida { get; set; } 
        public string Fecha3SubPartida { get; set; } 
        public string ObservacionIdentificacionCafe { get; set; }
        public DateTime FechaSecado { get; set; }
        public DateTime InicioSecado { get; set; }
        public DateTime SalidaSecado { get; set; }
        public TimeSpan TiempoSecado { get; set; }
        public double HumedadSecado { get; set; }
        public double Rendimiento { get; set; }
        public int IdPunteroSecador { get; set; } 
        public string NombrePunteroSecador { get; set; }
        public string ObservacionSecado { get; set; }
        public int IdCatador { get; set; } 
        public string NombreCatador { get; set; }
        public string ResultadoCatador { get; set; }
        public DateTime FechaCatacion { get; set; }
        public string ObservacionCatador { get; set; }
        //falta ver ubicacion
        public DateTime FechaPesado { get; set; }
        public double PesaSaco { get; set; }
        public double PesaQQs { get; set; }
        public int IdBodega { get; set; } 
        public string NombreBodega { get; set; }
        public int IdAlmacen { get; set; } 
        public string NombreAlmacen { get; set; }
        public string DoctoAlmacen { get; set; }
        public int IdPesador { get; set; } 
        public string NombrePunteroPesador { get; set; }
        public string ObservacionPesador { get; set; }
        public int CountSubPartida { get; set; }
    }

    class GraficSubPartida
    {
        public string Mes { get; set; }
        public string Calidad { get; set; }
        public double cantidad { get; set; }
    }

    public static class TablaSeleccionadasubPartd
    {
        public static int ITable { get; set; }
    }

    public static class SubPartidaSeleccionado
    {
        public static int ISubPartida { get; set; }
        public static int NumSubPartida { get; set; }
        public static string NombreSubParti { get; set; }
        public static bool clickImg { get; set; }
    }
    //
    class ReportSubPartida
    {
        public int NumeroSubpartida { get; set; }
        public string NombreCosecha { get; set; }
        public string NombreProcedencia { get; set; }
        public string NombreCalidadCafe { get; set; }
        public string NombreSubProducto { get; set; }
        public int Num1Semana { get; set; }
        public int Dias1SubPartida { get; set; }
        public string Fecha1SubPartida { get; set; }
        public string ObservacionIdentificacionCafe { get; set; }
        public string FechaSecado { get; set; }
        public string InicioSecado { get; set; }
        public TimeSpan TiempoIniSecado { get; set; }
        public string SalidaSecado { get; set; }
        public TimeSpan TiempoFinSecado { get; set; }
        public TimeSpan TiempoSecado { get; set; }
        public double HumedadSecado { get; set; }
        public double Rendimiento { get; set; }
        public string NombrePunteroSecador { get; set; }
        public string ObservacionSecado { get; set; }
        public string NombreCatador { get; set; }
        public string ResultadoCatador { get; set; }
        public string FechaCatacion { get; set; }
        public string ObservacionCatador { get; set; }
        //falta ver ubicacion
        public string FechaPesado { get; set; }
        public double PesaSaco { get; set; }
        public double PesaQQs { get; set; }
        public string NombreBodega { get; set; }
        public string NombreAlmacen { get; set; }
        public string DoctoAlmacen { get; set; }
        public string NombrePunteroPesador { get; set; }
        public string ObservacionPesador { get; set; }
        public int Num2Semana { get; set; }
        public int Num3Semana { get; set; }
        public int Dias2SubPartida { get; set; }
        public int Dias3SubPartida { get; set; }
        public string Fecha2SubPartida { get; set; }
        public string Fecha3SubPartida { get; set; }
        public string nombre_persona { get; set; }
    }
    //
}
