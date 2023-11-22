using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.controller.ReportsController;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using sistema_modular_cafe_majada.model.Mapping.Reports;
using sistema_modular_cafe_majada.model.UserData;
using System.Timers;

namespace sistema_modular_cafe_majada.views
{
    
    public partial class form_reportes : Form
    {
        private string fechaActual;
        private System.Timers.Timer refreshTimer;
        int id_Cosecha = CosechaActual.ICosechaActual;
        UserController userC = new UserController();
        string Nombre_cosecha = CosechaActual.NombreCosechaActual;
        private ReportesController reportesController = new ReportesController();
        readonly string RutaReportSubpda = "../../views/Reports/repor_subpartida.rdlc";
        readonly string RutaReportBodega = "../../views/Reports/repor_bodega.rdlc";
        readonly string RutaReportCCalidad = "../../views/Reports/repor_ccalidad.rdlc";
        readonly string RutaReportCafeBodega = "../../views/Reports/repor_cafebodega.rdlc";
        readonly string RutaReportGraficas = "../../views/Reports/repor_grafico.rdlc";

        public form_reportes()
        {
            InitializeComponent();
            dtFechaInicial.Value = new DateTime(2023, 1, 1);

            FormConfig();
            AsignarFuente();

        }


        private void form_reportes_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        private void btn_rptSubpartidas_Click(object sender, EventArgs e)
        {
            try
            {
                // Se obtiene el nombre de usuario
                var Nombre_Usuario = userC.ObtenerUsuariosNombresID(UsuarioActual.IUsuario);

                // Se obtiene la fecha inicial seleccionada en el control dtFechaInicial y se formatea como una cadena con el formato "dd/MM/yyyy".
                string fechaInicial = dtFechaInicial.Value.Date.ToString("dd/MM/yyyy");

                // Se obtiene la fecha final seleccionada en el control dtFechaFinal y se formatea como una cadena con el formato "dd/MM/yyyy".
                string fechaFinal = dtFechaFinal.Value.Date.ToString("dd/MM/yyyy");

                // Se llama al método GetSubpartidaData del controlador de reportes reportesController,
                // pasando como argumentos el id de cosecha (id_Cosecha), la fecha inicial y la fecha final obtenidas anteriormente.
                // Esto devuelve una lista de objetos ReportesSubpartida, que se almacena en la variable data.
                List<ReportesSubpartida> data = reportesController.GetSubpartidaData(CosechaActual.ICosechaActual, fechaInicial, fechaFinal);

                // Se recorre la lista data mediante un bucle foreach.
                foreach (ReportesSubpartida reporte in data)
                {
                    // En cada iteración, se establece la fecha inicial y la fecha final en los campos FechaIni y FechaFin del objeto ReportesSubpartida,
                    // y se establece el nombre de la cosecha en el campo NombreCosecha.
                    reporte.FechaIni = fechaInicial;
                    reporte.FechaFin = fechaFinal;
                    reporte.NombreCosecha = CosechaActual.NombreCosechaActual;
                    reporte.nombre_persona = Nombre_Usuario.ApellidoPersonaUsuario;
                }

                // Se crea una nueva fuente de datos para el informe utilizando la lista data y se le asigna el nombre de "repor_subpartida".
                ReportDataSource reportDataSource = new ReportDataSource("repor_subpartida", data);

                // Se crea un nuevo informe local (LocalReport) y se establece la ruta del archivo de definición del informe utilizando la variable RutaReportSubpda.
                LocalReport reportSubpartida = new LocalReport();
                reportSubpartida.ReportPath = RutaReportSubpda;

                // Se agrega la fuente de datos al informe local.
                reportSubpartida.DataSources.Add(reportDataSource);

                // Finalmente, se muestra el informe en el visor utilizando el método ShowReportInViewer.
                ShowReportInViewer(reportSubpartida, "repor_subpartida");
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción durante la ejecución del bloque de código dentro del bloque try,
                // esta será capturada y se mostrará en la consola (ignorando la excepción sin tomar ninguna acción).
                Console.WriteLine("Error al generar el informe: " + ex.Message);
            }

        }
        private void btn_rptCafeAcumulado_Click(object sender, EventArgs e)
        {
            var Nombre_Usuario = userC.ObtenerUsuariosNombresID(UsuarioActual.IUsuario);
            // Obtener la fecha actual y asignarla a la variable global
            fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            List<ReportesBodegas> data = reportesController.GetBodegaData(CosechaActual.ICosechaActual);
            foreach (ReportesBodegas reporte in data)
            {
                reporte.nombre_cosecha = Nombre_cosecha;
                reporte.fecha = fechaActual;
                reporte.nombre_persona = Nombre_Usuario.ApellidoPersonaUsuario;
            }
            ReportDataSource reportDataSource = new ReportDataSource("repor_bodega", data);
            LocalReport reportBodega = new LocalReport();
            reportBodega.ReportPath = RutaReportBodega;
            reportBodega.DataSources.Add(reportDataSource);
            ShowReportInViewer(reportBodega, "repor_bodega");
        }

        private void btn_rptCCalidades_Click(object sender, EventArgs e)
        {
            var Nombre_Usuario = userC.ObtenerUsuariosNombresID(UsuarioActual.IUsuario);
            // Obtener la fecha actual y asignarla a la variable global fechaActual
            fechaActual = DateTime.Now.ToString("dd/MM/yyyy");

            // Obtener los datos de los reportes de calidad (ReportesCCaliadades) usando el controlador de reportes reportesController
            List<ReportesCCaliadades> data = reportesController.GetCCalidadData(CosechaActual.ICosechaActual);

            // Crear una fuente de datos para el informe usando la lista data y asignarle el nombre "repor_ccalidad"
            ReportDataSource reportDataSource = new ReportDataSource("repor_ccalidad", data);

            // Recorrer la lista data mediante un bucle foreach
            foreach (ReportesCCaliadades reporte in data)
            {
                // En cada iteración, se establece la fecha actual en el campo fecha del objeto ReportesCCaliadades
                reporte.fecha = fechaActual;

                // También se establece el nombre de la cosecha en el campo nombre_cosecha del objeto ReportesCCaliadades
                reporte.nombre_cosecha = Nombre_cosecha;
                reporte.nombre_persona = Nombre_Usuario.ApellidoPersonaUsuario;
            }

            // Se crea un nuevo informe local (LocalReport) y se establece la ruta del archivo de definición del informe utilizando la variable RutaReportCCalidad
            LocalReport reportCCalidad = new LocalReport();
            reportCCalidad.ReportPath = RutaReportCCalidad;

            // Se agrega la fuente de datos al informe local
            reportCCalidad.DataSources.Add(reportDataSource);

            // Finalmente, se muestra el informe en el visor utilizando el método ShowReportInViewer
            ShowReportInViewer(reportCCalidad, "repor_ccalidad");
        }

        private void btn_rptCafeBodegas_Click(object sender, EventArgs e)
        {
            var Nombre_Usuario = userC.ObtenerUsuariosNombresID(UsuarioActual.IUsuario);
            List<ReportesCafeBodegas> data = reportesController.GetCafeBodegaData(CosechaActual.ICosechaActual);
            foreach (ReportesCafeBodegas reporte in data)
            {
                // En cada iteración, se establece la fecha actual en el campo fecha del objeto ReportesCafeBodegas
                reporte.fecha = fechaActual;

                // También se establece el nombre de la cosecha en el campo nombre_cosecha del objeto ReportesCafeBodegas
                reporte.nombre_cosecha = Nombre_cosecha;
                reporte.nombre_persona = Nombre_Usuario.ApellidoPersonaUsuario;
            }
            ReportDataSource reportDataSource = new ReportDataSource("repor_cafebodega", data);
            LocalReport reportCCalidad = new LocalReport();
            reportCCalidad.ReportPath = RutaReportCafeBodega;
            reportCCalidad.DataSources.Add(reportDataSource);
            ShowReportInViewer(reportCCalidad, "repor_cafebodega");
        }

        private void btn_rptGrafica_Click(object sender, EventArgs e)
        {
            var Nombre_Usuario = userC.ObtenerUsuariosNombresID(UsuarioActual.IUsuario);
            // Obtener la fecha actual y asignarla a la variable global
            fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            List<ReportesBodegas> data = reportesController.GetBodegaData(CosechaActual.ICosechaActual);
            foreach (ReportesBodegas reporte in data)
            {
                reporte.nombre_cosecha = Nombre_cosecha;
                reporte.fecha = fechaActual;
                reporte.nombre_persona = Nombre_Usuario.ApellidoPersonaUsuario;
    
            }
            ReportDataSource reportDataSource = new ReportDataSource("repor_bodega", data);
            LocalReport reportGrafico = new LocalReport();
            reportGrafico.ReportPath = RutaReportGraficas;
            reportGrafico.DataSources.Add(reportDataSource);
            ShowReportInViewer(reportGrafico, "repor_grafico");
        }



        private void ShowReportInViewer(LocalReport report, string datasetName)
        {
            // Limpia cualquier fuente de datos que esté actualmente asignada al informe que se va a mostrar en el visor de informes reportViewer1.
            reportViewer1.LocalReport.DataSources.Clear();

            // Forzar la actualización de la configuración del visor de informes
            reportViewer1.Reset();
            // Establece la ruta del archivo de definición del informe que se va a mostrar en el visor de informes reportViewer1.
            // La variable report contiene el objeto LocalReport que se va a mostrar, y report.ReportPath contiene la ruta del archivo del informe que se cargará en el visor.
            reportViewer1.LocalReport.ReportPath = report.ReportPath;

            // Agrega una nueva fuente de datos al informe que se va a mostrar en el visor de informes.
            // El parámetro datasetName es el nombre del conjunto de datos que se utilizará en el informe para enlazar los campos.
            // report.DataSources.First().Value obtiene la primera fuente de datos del informe que se va a mostrar.
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(datasetName, report.DataSources.First().Value));

            // Refresca el visor de informes reportViewer1 para mostrar el informe con la nueva fuente de datos y el archivo de definición especificados.
            // Después de agregar o cambiar la fuente de datos y la definición del informe, esta línea es necesaria para mostrar los cambios en el visor.
            reportViewer1.RefreshReport();
           
        }

        private void AsignarFuente()
        {
            Label[] labels = {label1, label2,label3,label4, label5,label6,label7, label8 };
            DateTimePicker[] dateTimePickers = { dtFechaInicial,dtFechaFinal};

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna a textbox
            FontViews.DateStyle(dateTimePickers);
            //se asigna a botones
        }

        //funcion para quitar bordes de formularios
        private void FormConfig()
        {
            //al inciar el formulario estara sin bordes
            this.FormBorderStyle = FormBorderStyle.None;
        }


    }
}
