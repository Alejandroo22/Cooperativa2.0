using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Reports
{
    class ReportesSubpartida
    {
        public string NombreCosecha { get; set; }
        public int Subpartida { get; set; }
        public int Semana { get; set; }
        public string Partida { get; set; }
        public string Fecha { get; set; }
        public string CalidadCafe { get; set; }
        public string Procedencia { get; set; }
        public string AlmacenadoEn { get; set; }
        public double Sacos { get; set; }
        public double QqsPunto { get; set; }
        public string InicioSecado { get; set; }
        public string FinSecado { get; set; }
        public string Tiempo { get; set; }
        public double QqsOro { get; set; }
        public double Humedad { get; set; }
        public double Rendimiento { get; set; }
        public string Puntero { get; set; }
        public string FechaCatacion { get; set; }
        public string Catacion { get; set; }
        public string Almacen { get; set; }
        public string FechaSecado { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public string nombre_persona { get; set; }
    }

    class ReportesBodegas
    {
        public string nombre_cosecha { get; set; }
        public string nombre_bodega { get; set; }
        
        public string calidad_cafe { get; set; }

        public double total_sacos { get; set; }
        
        public double total_qqspunto { get; set; }
        public string fecha { get; set; }
        public string nombre_persona { get; set; }

    }
    class ReportesCCaliadades
    {
        public string nombre_cosecha { get; set; }
        public int id_nombre_calidad { get; set; }
        public string nombre_calidad { get; set; }
        public string nombre_subproducto { get; set; }
        public string almacenado_en { get; set; }
        public double total_sacosE { get; set; }
        public double total_qqspuntoE { get; set; }
        public double total_sacosS { get; set; }
        public double total_qqspuntoS { get; set; }
        public double total_sacosT { get; set; }
        public double total_qqspuntoT { get; set; }
        public string fecha { get; set; }
        public string nombre_persona { get; set; }

    }

    class ReportesCafeBodegas
    {
        public string nombre_cosecha { get; set; }
        public string nombre_bodega { get; set; }
        public string nombre_almacen { get; set; }
        public int id_calidad { get; set; }
        public string nombre_calidad { get; set; }
        public string nombre_subproducto { get; set; }
        public double total_sacosE { get; set; }
        public double total_qqspuntoE { get; set; }
        public double total_sacosS { get; set; }
        public double total_qqspuntoS { get; set; }
        public double total_sacosT { get; set; }
        public double total_qqspuntoT { get; set; }
        public string fecha { get; set; }
        public string nombre_persona { get; set; }

    }

}
