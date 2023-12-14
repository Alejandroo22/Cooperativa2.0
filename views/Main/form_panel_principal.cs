using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using sistema_modular_cafe_majada.controller;
using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using sistema_modular_cafe_majada.model.Mapping.Product;
using sistema_modular_cafe_majada.model.UserData;
using sistema_modular_cafe_majada.views;

namespace sistema_modular_cafe_majada
{
    public partial class form_panel_principal : Form
    {
        //variable para refrescar el formulario cad cierto tiempo
        private System.Timers.Timer refreshTimer;

        private int idExitCalidad = 0;

        private int iTabla;
        public int Itabla
        {
            get { return iTabla; }
            set { iTabla = value; }
        }

        public form_panel_principal()
        {
            InitializeComponent();

            ShowCountBDCard();
            ShowCountExistenciaBDCard();
            ConfigurarGrafico1();
            //ConfigurarGrafico2();
            ConfigurarGrafico3();

            // Configurar el temporizador para que se dispare cada cierto intervalo (por ejemplo, cada 5 segundos).
            refreshTimer = new System.Timers.Timer();
            refreshTimer.Interval = 2500; // Intervalo en milisegundos (5 segundos en este caso).
            refreshTimer.Elapsed += RefreshTimer_Elapsed;
            refreshTimer.Start();

            AsignarFuente();

        }

        private void RefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Utilizar Invoke para actualizar los controles de la interfaz de usuario desde el hilo del temporizador.
            if (!this.IsDisposed && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    // Aquí se puede escribir la lógica para refrescar el formulario, actualizar los datos.
                    // Por ejemplo, si queremos actualizar datos desde una base de datos o un servicio, lo harías aquí.
                    if (!this.IsDisposed)
                    {
                        ShowCountBDCard();
                        ShowCountExistenciaBDCard();
                    }
                }));
            }
        }

        //funcion para mostrar los totales de registros de cada tarjeta en el panel principal
        public void ShowCountBDCard()
        {
            //para calidad de cafe
            var ccafe = new CCafeController();
            CalidadCafe totalccafe = ccafe.CountCalidad();
            lbl_calidad.Text = totalccafe.CountCalidad.ToString();

            //para calidad de cafe
            var subP = new SubProductoController();
            SubProducto totalsub = subP.CountSubProducto();
            lbl_subProduct.Text = totalsub.CountSubProducto.ToString();
            
            //para tipo de cafe
            var tipocafe = new TipoCafeController();
            TipoCafe totaltipo = tipocafe.CountTipoCafe();
            lbl_tipo.Text = totaltipo.CountTipoCafe.ToString();
            
            //para finca de cafe
            var finca = new FincaController();
            Finca totalFinca = finca.CountFincas();
            lbl_finca.Text = totalFinca.CountFinca.ToString();

            //para beneficio
            var beneficio = new BeneficioController();
            Beneficio totalBeneficio = beneficio.CountBeneficio();
            lbl_beneficio.Text = totalBeneficio.CountBeneficio.ToString();
        }

        //funcion para mostrar los totales de existencia de cafe para cada tarjeta en el panel principal de existencia
        public void ShowCountExistenciaBDCard()
        {
            var almacenExistenciaC = new AlmacenController();
            CCafeController ccafeC = new CCafeController();

            var calidad = ccafeC.ObtenerUltimoId();
            Almacen almacen = new Almacen();

            // Obtener el número total de registros en la base de datos
            int totalRegistros = almacenExistenciaC.ObtenerTotalRegistrosEnLaBD();

            // Incrementar el contador
            idExitCalidad++;

            // Si llegamos al último registro, reiniciar el contador
            if (idExitCalidad > totalRegistros)
            {
                idExitCalidad = 1;
            }

            // Cantidad SHG
            almacen = almacenExistenciaC.CountExistenceCofeeAlmacen(idExitCalidad);
            lbl_existCafe1.Text = almacen.NombreCalidadCafe;
            lbl_cafeSHG.Text = Convert.ToString(almacen.CountExistenceCoffe);

            // Cantidad HG
            idExitCalidad++;
            almacen = almacenExistenciaC.CountExistenceCofeeAlmacen(idExitCalidad);
            lbl_existCafe2.Text = almacen.NombreCalidadCafe;
            lbl_cafeHG.Text = Convert.ToString(almacen.CountExistenceCoffe);

            // Cantidad CS
            idExitCalidad++;
            almacen = almacenExistenciaC.CountExistenceCofeeAlmacen(idExitCalidad);
            lbl_existCafe3.Text = almacen.NombreCalidadCafe;
            lbl_cafeCS.Text = Convert.ToString(almacen.CountExistenceCoffe);
        }

        //Configuracion para la grafica 1
        private void ConfigurarGrafico1()
        {
            // Obtener los datos de la base de datos
            SubPartidaController spC = new SubPartidaController();
            List<GraficSubPartida> datos = spC.ObtenerCalidadQQsOro(CosechaActual.ICosechaActual);

            // Obtener el mes del primer resultado
           // string mes = datos.Count > 0 ? datos[0].Mes : string.Empty;

            // Configurar el tipo de gráfico (en este caso, será un gráfico de columnas, vertical)
            chart1.Series.Clear();
            // Título del gráfico con el mes
            chart1.Titles.Add($"Cafe QQs Oro Entrada ");

            // Crear una nueva serie para QQs Oro
            Series serieQQsOro = new Series("QQs Oro");
            serieQQsOro.ChartType = SeriesChartType.Column;

            // Asignar la propiedad de tooltip para la serie
            serieQQsOro.ToolTip = "#VALX: #VALY"; // Muestra el valor X (Calidad) y el valor Y (Cantidad)

            // Recorrer los datos y agregarlos a la serie
            foreach (GraficSubPartida sp in datos)
            {
                serieQQsOro.Points.AddXY(sp.Calidad, sp.cantidad);
              //  mes = sp.Mes;
            }

            // Agregar la serie al gráfico
            chart1.Series.Add(serieQQsOro);

            // Mostrar los valores en las barras
            chart1.Series["QQs Oro"].IsValueShownAsLabel = true;

            // Etiquetas de los ejes
            chart1.ChartAreas[0].AxisX.Title = "Calidad Cafe";
            chart1.ChartAreas[0].AxisY.Title = "QQs Oro";

            // Configurar la leyenda y su posición
            chart1.Legends.Clear();
            Legend legend = new Legend("MiLeyenda");
            chart1.Legends.Add(legend);
            chart1.Series["QQs Oro"].Legend = "MiLeyenda"; // Asociar la serie "Ventas" a la leyenda correcta
            chart1.Legends["MiLeyenda"].Docking = Docking.Bottom;

            // Refrescar la gráfica
            chart1.Invalidate();
        }

        //Configuracion para la grafica 2
        /*private void ConfigurarGrafico2()
        {
            // Obtener los datos de la base de datos
            SubPartidaController spC = new SubPartidaController();
            List<GraficSubPartida> datos = spC.ObtenerCalidadQQsOro(CosechaActual.ICosechaActual);

            // Obtener el mes del primer resultado
            string mes = datos.Count > 0 ? datos[0].Mes : string.Empty;

            // Configurar el tipo de gráfico (en este caso, será un gráfico de columnas, vertical)
            chart2.Series.Clear();
            // Título del gráfico con el mes
            chart2.Titles.Add($"Cafe QQs Oro Entrada del mes de {mes}");

            // Crear una nueva serie para QQs Oro
            Series serieQQsOro = new Series("QQs Oro");
            serieQQsOro.ChartType = SeriesChartType.Line;

            // Asignar la propiedad de tooltip para la serie
            serieQQsOro.ToolTip = "#VALX: #VALY"; // Muestra el valor X (Calidad) y el valor Y (Cantidad)

            // Recorrer los datos y agregarlos a la serie
            foreach (GraficSubPartida sp in datos)
            {
                serieQQsOro.Points.AddXY(sp.Calidad, sp.cantidad);
                mes = sp.Mes;
            }

            // Agregar la serie al gráfico
            chart2.Series.Add(serieQQsOro);

            // Etiquetas de los ejes
            chart2.ChartAreas[0].AxisX.Title = "Calidad Cafe";
            chart2.ChartAreas[0].AxisY.Title = "QQs Oro";

            // Configurar la leyenda y su posición
            chart2.Legends.Clear();
            Legend legend = new Legend("MiLeyenda");
            chart2.Legends.Add(legend);
            chart2.Series["QQs Oro"].Legend = "MiLeyenda"; // Asociar la serie "Ventas" a la leyenda correcta
            chart2.Legends["MiLeyenda"].Docking = Docking.Bottom;

            serieQQsOro.Color = Color.Blue;
            serieQQsOro.BorderWidth = 2;
            serieQQsOro.MarkerStyle = MarkerStyle.Circle;
            serieQQsOro.MarkerSize = 8;

            // Refrescar la gráfica
            chart2.Invalidate();

        }*/

        //Configuracion para la grafica 3
        private void ConfigurarGrafico3()
        {
            // Obtener los datos de la base de datos
            SubPartidaController spC = new SubPartidaController();
            List<GraficSubPartida> datos = spC.ObtenerCalidadQQsOro(CosechaActual.ICosechaActual);

            // Calcular la suma total de cantidades
            double sumaTotal = datos.Sum(sp => sp.cantidad);
            // Obtener el mes del primer resultado
         //   string mes = datos.Count > 0 ? datos[0].Mes : string.Empty;

            // Configurar el tipo de gráfico (en este caso, será un gráfico de columnas, vertical)
            chart3.Series.Clear();
            // Título del gráfico con el mes
            chart3.Titles.Add($"Cafe QQs Oro Entrada ");

            // Crear una nueva serie para QQs Oro
            Series serieQQsOro = new Series("QQs Oro");
            serieQQsOro.ChartType = SeriesChartType.Pie;

            // Recorrer los datos y agregarlos a la serie
            foreach (GraficSubPartida sp in datos)
            {
                DataPoint point = new DataPoint();
                point.AxisLabel = sp.Calidad; // Nombre de calidad en la leyenda
                double porcentaje = (sp.cantidad / sumaTotal) * 100;
                point.SetValueY(porcentaje); // Porcentaje como valor de cantidad
                point.ToolTip = $"{sp.Calidad}: {sp.cantidad}"; // Tooltip con el nombre de calidad y cantidad
                
                // Agregar el punto a la serie
                serieQQsOro.Points.Add(point);
            }

            // Agregar la serie al gráfico
            chart3.Series.Add(serieQQsOro);

            // Mostrar los valores en porcentaje
            chart3.Series["QQs Oro"].IsValueShownAsLabel = true;
            chart3.Series["QQs Oro"]["PieLabelStyle"] = "Outside"; // Mostrar los valores fuera del gráfico
            chart3.Series["QQs Oro"]["PieLineColor"] = "Black"; // Línea del borde del gráfico de pastel en negro
            serieQQsOro.LabelFormat = "#.##'%'"; // Formato de etiqueta para valores de porcentaje

            // Configurar la leyenda y su posición
            chart3.Legends.Clear();
            Legend legend = new Legend("MiLeyenda");
            chart3.Legends.Add(legend);
            chart3.Series["QQs Oro"].Legend = "MiLeyenda"; // Asociar la serie "Ventas" a la leyenda correcta
            chart3.Legends["MiLeyenda"].Docking = Docking.Bottom;

            // Refrescar la gráfica
            chart3.Invalidate();
        }

        private void pnl_calCafe_Click(object sender, EventArgs e)
        {
            if(UsuarioActual.RolUsuario == 1 || UsuarioActual.RolUsuario == 2)
            {
                iTabla = 1;
                form_opcGeneralData form_Opc = new form_opcGeneralData(this);
                form_Opc.ShowDialog();
            }
        }

        private void pnl_subProd_Click(object sender, EventArgs e)
        {
            if (UsuarioActual.RolUsuario == 1 || UsuarioActual.RolUsuario == 2)
            {
                iTabla = 2;
                form_opcGeneralData form_Opc = new form_opcGeneralData(this);
                form_Opc.ShowDialog();
            }
        }

        private void pnl_Uva_Click(object sender, EventArgs e)
        {
            if (UsuarioActual.RolUsuario == 1 || UsuarioActual.RolUsuario == 2)
            {
                iTabla = 3;
                form_opcGeneralData form_Opc = new form_opcGeneralData(this);
                form_Opc.ShowDialog();
            }
        }

        private void pnl_fincas_Click(object sender, EventArgs e)
        {
            if (UsuarioActual.RolUsuario == 1 || UsuarioActual.RolUsuario == 2)
            {
                iTabla = 4;
                form_opcGeneralData form_Opc = new form_opcGeneralData(this);
                form_Opc.ShowDialog();
            }
        }

        private void pnl_beneficios_Click(object sender, EventArgs e)
        {
            if (UsuarioActual.RolUsuario == 1 || UsuarioActual.RolUsuario == 2)
            {
                iTabla = 5;
                form_opcGeneralData form_Opc = new form_opcGeneralData(this);
                form_Opc.ShowDialog();
            }
        }

        private void form_panel_principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Se Detiene el temporizador antes de cerrar el formulario.
            refreshTimer.Stop();
        }

        private void AsignarFuente()
        {
            Label[] encabezados = { label1,label2, label5, label7, label9,label11,label16,lbl_cafeSHG,lbl_cafeHG,lbl_cafeCS};
            Label[] info = { lbl_beneficio,lbl_cafeCS,lbl_cafeHG,lbl_cafeSHG,lbl_calidad,lbl_finca,
                            lbl_subProduct,lbl_tipo };

            //se asigna a los label de encaebzado
            FontViews.LabelStylePanelEncabezado(encabezados);
            //se asigna al label de titulo de formulario
            FontViews.LabelStylePanelInfo(info);
        }
    }
}
