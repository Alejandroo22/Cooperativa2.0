using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_modular_cafe_majada.controller.ReportsController
{
    class ReportesController
    {
        private ReportesDAO ReportesDAO;
        public ReportesController()
        {
            ReportesDAO = new ReportesDAO();
        }
        public List<ReportesSubpartida> GetSubpartidaData(int cosecha, string fechaini, string fechafin)
        {
            List<ReportesSubpartida> reportesSubpartidas = new List<ReportesSubpartida>();

            try
            {
                reportesSubpartidas = ReportesDAO.GetSubpartidaData(cosecha, fechaini, fechafin);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de subpartida: " + ex.Message);
            }

            return reportesSubpartidas;
        }

        public List<ReportesBodegas> GetBodegaData(int cosecha)
        {
            List<ReportesBodegas> reportesBodegas = new List<ReportesBodegas>();

            try
            {
                reportesBodegas = ReportesDAO.GetBodegaData(cosecha);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de subpartida: " + ex.Message);
            }

            return reportesBodegas;
        }

        public List<ReportesCCaliadades> GetCCalidadData(int cosecha)
        {
            List<ReportesCCaliadades> reportesCCaliadades = new List<ReportesCCaliadades>();

            try
            {
                reportesCCaliadades = ReportesDAO.GetCCalidadData(cosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de calidades de cafe: " + ex.Message);
            }

            return reportesCCaliadades;
        }

        public List<ReportesCafeBodegas> GetCafeBodegaData(int cosecha)
        {
            List<ReportesCafeBodegas> reportesCafeBodegas = new List<ReportesCafeBodegas>();

            try
            {
                reportesCafeBodegas = ReportesDAO.GetCafeBodegaData(cosecha);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de subpartida: " + ex.Message);
            }

            return reportesCafeBodegas;
        }
    }

   
}
