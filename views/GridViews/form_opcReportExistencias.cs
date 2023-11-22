
using Microsoft.Reporting.WinForms;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_opcReportExistencias : Form
    {
        public string ReportPath;
        public ReportDataSource ReportDataSource; // Agregar un miembro para el ReportDataSource

        public form_opcReportExistencias(string reportPath, ReportDataSource reportDataSource = null)
        {
            InitializeComponent();
            ReportPath = reportPath;
            ReportDataSource = reportDataSource; // Asignar el ReportDataSource

            CargarInforme();
        }

        public void CargarInforme()
        {
            reportViewerDetallado.Reset();
            reportViewerDetallado.LocalReport.ReportPath = ReportPath;

            if (ReportDataSource != null)
            {
                reportViewerDetallado.LocalReport.DataSources.Add(ReportDataSource);
            }

            reportViewerDetallado.RefreshReport();
        }


        private void form_opcReportExistencias_Load(object sender, EventArgs e)
        {
      
            
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
