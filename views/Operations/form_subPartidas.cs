using Microsoft.Reporting.WinForms;
using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using sistema_modular_cafe_majada.model.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_subPartidas : Form
    {
        private List<TextBox> txbRestrict;
        public bool imagenClickeada = false;
        public int SubSPart;
        public string SubSPartCosecha;
        private bool imgClickCalidad = false;
        private bool imgClickBodega = false;
        SubPartidaController countSP = null;
        private SubPartidaController reportesController = new SubPartidaController();
        private string fechaActual;
        UserController userC = new UserController();
        public double cantidaQQsUpdate = 0.00;
        public double cantidaQQsActUpdate = 0.00;
        public double cantidaSacoUpdate = 0.00;
        public double cantidaSacoActUpdate = 0.00;
        private CCafeController cCafe = new CCafeController();
        private PersonalController pPersonal = new PersonalController();
        //variable para refrescar el formulario cad cierto tiempo
        private System.Timers.Timer refreshTimer;

        private int iProcedencia;
        private int icosechaCambio;
        private int isubProducto;
        private int iCatador;
        private int iPesador;
        private int iSecador;
        private int iBodega;
        private int iAlmacen;
        private int iCalidad;
        private int iCalidadNoUpd;
        private int iSubPartida;

        public form_subPartidas()
        {
            InitializeComponent();

            RolUser();

            // Configura el formato del DateTimePicker como corto.
            dtp_fechaSecado.Format = DateTimePickerFormat.Short;
            dtp_fechaSalidaSecad.Format = DateTimePickerFormat.Short;
            dtp_fechaPesa.Format = DateTimePickerFormat.Short;
            dtp_fechaInicioSecad.Format = DateTimePickerFormat.Short;
            dtp_fechaCatacion.Format = DateTimePickerFormat.Short;

            ClearDataTxb();
            // Configurar el temporizador para que se dispare cada cierto intervalo (por ejemplo, cada 5 segundos).
            refreshTimer = new System.Timers.Timer();
            refreshTimer.Interval = 5000; // Intervalo en milisegundos (5 segundos en este caso).
            refreshTimer.Elapsed += RefreshTimer_Elapsed;
            refreshTimer.Start();

            txbRestrict = new List<TextBox> { txb_pdasSemana1, txb_pdasSemana2, txb_pdasSemana3,
                                                txb_humedad, txb_rendimiento, txb_cantidadQQs, txb_CantidadSaco, txb_horaSalida, txb_horaInicio,
                                             txb_tiempoSecad, txb_codProcedencia, txb_codCalidad, txb_codPuntero, txb_codCatador, txb_codUbicacion,
                                                txb_codSilo, txb_codPesador};


            RestrictTextBoxNum(txbRestrict);

            CbxSubProducto();

            //
            CountNumSubPartida();

            txb_nombreCatador.Enabled = false;
            txb_nombreCatador.ReadOnly = true;
            txb_nombrePesador.Enabled = false;
            txb_nombrePesador.ReadOnly = true;
            txb_nombrePuntero.Enabled = false;
            txb_nombrePuntero.ReadOnly = true;
            txb_cosecha.Enabled = false;
            txb_cosecha.ReadOnly = true;
            txb_cosecha.Text = CosechaActual.NombreCosechaActual;
            txb_procedencia.Enabled = false;
            txb_procedencia.ReadOnly = true;
            txb_calidad.Enabled = false;
            txb_calidad.ReadOnly = true;
            txb_ubicadoBodega.Enabled = false;
            txb_ubicadoBodega.ReadOnly = true;
            txb_almacenSiloPiña.Enabled = false;
            txb_almacenSiloPiña.ReadOnly = true;
            cbx_subProducto.DropDownStyle = ComboBoxStyle.DropDownList;

            AsignarFuente();

        }

        //
        private void RefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Utilizar Invoke para actualizar los controles de la interfaz de usuario desde el hilo del temporizador.
            if (!this.IsDisposed && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    // Aquí se puede escribir la lógica para refrescar el formulario, actualizar los datos.
                    // Por ejemplo, si queremos actualizar datos desde una base de datos o un servicio, lo haríamos aquí.
                    if (!this.IsDisposed)
                    {
                        if (icosechaCambio != CosechaActual.ICosechaActual || string.IsNullOrWhiteSpace(txb_subPartida.Text))
                        {
                            icosechaCambio = CosechaActual.ICosechaActual;
                        }
                        txb_cosecha.Text = CosechaActual.NombreCosechaActual;
                    }
                }));
            }
        }

        //
        private void RolUser()
        {

            switch (UsuarioActual.RolUsuario)
            {
                case 1:
                    {
                        //administrador
                        //sin restricciones 
                    }
                    break;
                case 2:
                    {
                        //consultor
                        BlockEventsComponents();
                        txb_subPartida.Enabled = true;
                        btn_sPartida.Click += btn_sPartida_Click;
                    }
                    break;
                case 3:
                    {
                        //Digitador
                        btn_deleteSPartida.Click -= btn_deleteSPartida_Click;
                    }
                    break;
                case 4:
                    {
                        //Invitado
                        BlockEventsComponents();
                    }
                    break;
                default:
                    {
                        MessageBox.Show("Su rol actual no tiene autoridad para acceder a ciertas funciones en el sistema. Por favor, póngase en contacto con el administrador para obtener más información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BlockEventsComponents();
                        btn_pdfSPartida.Click -= btn_pdfSPartida_Click;
                    }
                    break;

            }
        }

        //
        private void BlockEventsComponents()
        {
            cbx_subProducto.Enabled = false;
            txb_subPartida.Enabled = false;
            txb_pdasSemana1.Enabled = false;
            txb_pdasSemana2.Enabled = false;
            txb_pdasSemana3.Enabled = false;
            txb_diasPdas1.Enabled = false;
            txb_diasPdas2.Enabled = false;
            txb_diasPdas3.Enabled = false;
            txb_fechaPartd1.Enabled = false;
            txb_fechaPartd2.Enabled = false;
            txb_fechaPartd3.Enabled = false;
            txb_observacionCafe.Enabled = false;
            txb_resultadoCatacion.Enabled = false;
            txb_observacionCatador.Enabled = false;
            txb_CantidadSaco.Enabled = false;
            txb_cantidadQQs.Enabled = false;
            txb_doctoAlmacen.Enabled = false;
            txb_observacionPesa.Enabled = false;
            txb_horaInicio.Enabled = false;
            txb_horaSalida.Enabled = false;
            txb_tiempoSecad.Enabled = false;
            txb_humedad.Enabled = false;
            txb_rendimiento.Enabled = false;
            txb_observacionSecad.Enabled = false;
            dtp_fechaSecado.Enabled = false;
            dtp_fechaInicioSecad.Enabled = false;
            dtp_fechaSalidaSecad.Enabled = false;
            dtp_fechaPesa.Enabled = false;
            dtp_fechaCatacion.Enabled = false;
            btn_deleteSPartida.Click -= btn_deleteSPartida_Click;
            btn_sPartida.Click -= btn_sPartida_Click;
            btn_SaveUser.Click -= btn_SaveUser_Click;
            btn_puntero.Click -= btn_puntero_Click;
            btn_pesador.Click -= btn_pesador_Click;
            btn_catador.Click -= btn_catador_Click;
            btn_ubicacionCafe.Click -= btn_ubicacionCafe_Click;
            btn_ubiFisicaCafe.Click -= btn_ubiFisicaCafe_Click;
            btn_CCafe.Click -= btn_CCafe_Click;
            btn_prodCafe.Click -= btn_prodCafe_Click;
        }

        //
        public void CbxSubProducto()
        {
            SubProductoController subPro = new SubProductoController();
            List<SubProducto> datoSubPro;

            if (imgClickCalidad)
            {
                datoSubPro = subPro.ObtenerSubProductoPorIdCalidad(CalidadSeleccionada.ICalidadSeleccionada);
            }
            else
            {
                datoSubPro = subPro.ObtenerSubProductos();
            }

            cbx_subProducto.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (SubProducto subP in datoSubPro)
            {
                int idSubP = subP.IdSubProducto;
                string nombreSubP = subP.NombreSubProducto;

                cbx_subProducto.Items.Add(new KeyValuePair<int, string>(idSubP, nombreSubP));
            }
        }

        //
        public static string ConvertILimitTimeTxb(string textTime)
        {
            string horaCompleta = textTime;

            // Dividir la cadena en partes utilizando el carácter ':'
            string[] partesHora = horaCompleta.Split(':');

            if (partesHora.Length == 1 && int.TryParse(partesHora[0], out int horas))
            {
                // Si hay solo una parte y se pudo convertir a entero, asumimos que es el número de horas.

                // Ajuste para las horas: Si la parte de las horas tiene un solo dígito y es menor a 10, se añade un cero a la izquierda.
                horaCompleta = horas < 10 ? "0" + horas : horas.ToString();

                // Añadimos los minutos y segundos con valor 00.
                horaCompleta += ":00";
            }
            else if (partesHora.Length == 2 && int.TryParse(partesHora[0], out int horas2) && int.TryParse(partesHora[1], out int minutos))
            {
                // Si hay dos partes y ambas se pudieron convertir a enteros, asumimos que es el número de horas y minutos.

                // Ajuste para las horas: Si la parte de las horas tiene un solo dígito y es menor a 10, se añade un cero a la izquierda.
                horaCompleta = horas2 < 10 ? "0" + horas2 : horas2.ToString();

                // Ajuste para los minutos: Si la parte de los minutos tiene un solo dígito y es menor a 10, se añade un cero a la izquierda.
                horaCompleta += ":" + (minutos < 10 ? "0" + minutos : minutos.ToString());

            }
            else if (partesHora.Length == 3 && int.TryParse(partesHora[0], out int horas3) && int.TryParse(partesHora[1], out int minutos3) && int.TryParse(partesHora[2], out int segundos))
            {
                // Si hay tres partes y todas se pudieron convertir a enteros, asumimos que es el número de horas, minutos y segundos.

                // Ajuste para las horas: Si la parte de las horas tiene un solo dígito y es menor a 10, se añade un cero a la izquierda.
                horaCompleta = horas3 < 10 ? "0" + horas3 : horas3.ToString();

                // Ajuste para los minutos: Si la parte de los minutos tiene un solo dígito y es menor a 10, se añade un cero a la izquierda.
                horaCompleta += ":" + (minutos3 < 10 ? "0" + minutos3 : minutos3.ToString());

            }
            else
            {
                // Si no se cumple ninguna de las condiciones, el formato ingresado no es válido.
                Console.WriteLine("Formato de hora no válido.");
                return string.Empty; // O puedes lanzar una excepción si es necesario.
            }

            // Devolvemos el resultado formateado.
            return horaCompleta;
        }

        //
        public void RestrictTextBoxNum(List<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.KeyPress += (sender, e) =>
                {
                    char decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != decimalSeparator && e.KeyChar != '.')
                    {
                        e.Handled = true; // Cancela el evento KeyPress si no es un dígito, el punto o la coma
                    }

                    // Permite solo un punto o una coma en el TextBox
                    if ((e.KeyChar == decimalSeparator || e.KeyChar == '.') && (textBox.Text.Contains(decimalSeparator.ToString()) || textBox.Text.Contains('.')))
                    {
                        e.Handled = true; // Cancela el evento KeyPress si ya hay un punto o una coma en el TextBox
                    }
                };
            }
        }

        //
        public static string ConvertToHHMMSS(string tiempoOriginalStr)
        {
            TimeSpan tiempoOriginal;

            // Parsear el valor original a un TimeSpan
            if (TimeSpan.TryParse(tiempoOriginalStr, out tiempoOriginal))
            {
                // Calcular el total de horas (días * 24 + horas)
                int totalHoras = tiempoOriginal.Days * 24 + tiempoOriginal.Hours;
                int min = tiempoOriginal.Minutes;

                // Usar string.Format() para formatear los minutos con ceros a la izquierda
                string tiempoNuevo = string.Format("{0:D2}:{1:D2}", totalHoras, min);

                return tiempoNuevo;
            }
            else
            {
                // Devolver el valor original en caso de formato inválido
                Console.WriteLine("Error de Formato en fecha convert");
                return tiempoOriginalStr;
            }
        }

        //validar que la hora este en los formatos valdios
        static bool ValidarHora(string horaStr)
        {
            try
            {
                if (horaStr.Length < 3)
                {
                    // La cadena no tiene el formato adecuado
                    return false;
                }

                int horas = int.Parse(horaStr.Substring(0, 2));
                if (horas >= 0 && horas <= 23)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
        
        //validacion de tiempo, verifica que el valor minutos este en el rango de los formatos
        static bool ValidarMinuto(string minStr)
        {
            try
            {
                if (minStr.Length < 5)
                {
                    // La cadena no tiene el formato adecuado
                    return false;
                }

                int minutos = int.Parse(minStr.Substring(3, 2));
                if (minutos >= 0 && minutos <= 59)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }

        //
        public void ShowSubPartidaView(TextBox txb)
        {
            var subPC = new SubPartidaController();
            SubProductoController subProC = new SubProductoController();
            
            var sub = new SubPartida();

            if (SubPartidaSeleccionado.ISubPartida != 0)
            {
                //Console.WriteLine("Depuracion - numSubpartida " + txb_subPartida.Text + " SubPartidaSeleccionada " + SubPartidaSeleccionado.NumSubPartida);
                sub = subPC.ObtenerSubPartidaPorID(SubPartidaSeleccionado.ISubPartida);
                txb_subPartida.Text = Convert.ToString(SubPartidaSeleccionado.NumSubPartida);
                iSubPartida = SubPartidaSeleccionado.ISubPartida;
            }
            else
            {
                //Console.WriteLine("Depuracion - numSubpartida " + txb_subPartida.Text + " SubPartidaSeleccionada " + SubPartidaSeleccionado.NumSubPartida);
                sub = subPC.ObtenerSubPartidasPorNombreAndCosecha(txb.Text, CosechaActual.NombreCosechaActual);
                txb_subPartida.Text = Convert.ToString(txb.Text);
                iSubPartida = sub.IdSubpartida;
                SubPartidaSeleccionado.NumSubPartida = sub.NumeroSubpartida;
                SubPartidaSeleccionado.NombreSubParti = Convert.ToString(sub.NumeroSubpartida);
                SubPartidaSeleccionado.ISubPartida = sub.IdSubpartida;
            }

            var name = subProC.ObtenerSubProductoPorNombre(sub.NombreSubProducto);
            isubProducto = name.IdSubProducto;

            AlmacenController almCtrl = new AlmacenController();
            var calidad = almCtrl.ObtenerAlmacenNombreCalidad(sub.IdCalidadCafe);
            CalidadSeleccionada.ICalidadSeleccionada = (int)calidad.IdCalidadCafe;
            CalidadSeleccionada.NombreCalidadSeleccionada = calidad.NombreCalidadCafe;

            string tiempoSec = ConvertToHHMMSS(sub.TiempoSecado.ToString());
            //cbx
            cbx_subProducto.Items.Clear();
            CbxSubProducto();

            int isP = name.IdSubProducto - 1;
            // Obtener la fecha y la hora por separado
            DateTime fechaInicio = sub.InicioSecado.Date;
            TimeSpan horaInicio = sub.InicioSecado.TimeOfDay;
            DateTime fechaSalida = sub.SalidaSecado.Date;
            TimeSpan horaSalida = sub.SalidaSecado.TimeOfDay;

            txb_procedencia.Text = sub.NombreProcedencia;
            iProcedencia = sub.IdProcedencia;
            txb_calidad.Text = sub.NombreCalidadCafe;
            iCalidad = sub.IdCalidadCafe;
            iCalidadNoUpd = sub.IdCalidadCafe;
            cbx_subProducto.SelectedIndex = isP;
            txb_pdasSemana1.Text = Convert.ToString(sub.Num1Semana);
            txb_pdasSemana2.Text = Convert.ToString(sub.Num2Semana);
            txb_pdasSemana3.Text = Convert.ToString(sub.Num3Semana);
            txb_diasPdas1.Text = Convert.ToString(sub.Dias1SubPartida);
            txb_diasPdas2.Text = Convert.ToString(sub.Dias2SubPartida);
            txb_diasPdas3.Text = Convert.ToString(sub.Dias3SubPartida);
            txb_fechaPartd1.Text = sub.Fecha1SubPartida;
            txb_fechaPartd2.Text = sub.Fecha2SubPartida;
            txb_fechaPartd3.Text = sub.Fecha3SubPartida;
            txb_observacionCafe.Text = sub.ObservacionIdentificacionCafe;
            dtp_fechaSecado.Value = sub.FechaSecado;
            dtp_fechaInicioSecad.Value = fechaInicio;
            dtp_fechaSalidaSecad.Value = fechaSalida;
            txb_horaInicio.Text = Convert.ToString(horaInicio);
            txb_horaSalida.Text = Convert.ToString(horaSalida);
            txb_tiempoSecad.Text = tiempoSec;
            txb_humedad.Text = sub.HumedadSecado.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
            txb_rendimiento.Text = sub.Rendimiento.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
            txb_nombrePuntero.Text = sub.NombrePunteroSecador;
            iSecador = sub.IdPunteroSecador;
            txb_observacionSecad.Text = sub.ObservacionSecado;
            txb_resultadoCatacion.Text = sub.ResultadoCatador;
            dtp_fechaCatacion.Value = sub.FechaCatacion;
            txb_nombreCatador.Text = sub.NombreCatador;
            iCatador = sub.IdCatador;
            txb_observacionCatador.Text = sub.ObservacionCatador;
            dtp_fechaPesa.Value = sub.FechaPesado;
            txb_CantidadSaco.Text = sub.PesaSaco.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
            txb_cantidadQQs.Text = sub.PesaQQs.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
            cantidaQQsUpdate = sub.PesaQQs;
            cantidaSacoUpdate = sub.PesaSaco;
            txb_ubicadoBodega.Text = sub.NombreBodega;
            iBodega = sub.IdBodega;
            txb_almacenSiloPiña.Text = sub.NombreAlmacen;
            iAlmacen = sub.IdAlmacen;
            txb_nombrePesador.Text = sub.NombrePunteroPesador;
            iPesador = sub.IdPesador;
            txb_doctoAlmacen.Text = sub.DoctoAlmacen;
            txb_observacionPesa.Text = sub.ObservacionPesador;

        }

        //
        private void btn_sPartida_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 1;
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                ShowSubPartidaView(txb_subPartida);
            }
        }

        //
        private void btn_prodCafe_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 2;
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                iProcedencia = ProcedenciaSeleccionada.IProcedencia;
                txb_procedencia.Text = ProcedenciaSeleccionada.NombreProcedencia;
            }
        }

        //
        private void btn_CCafe_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 3;
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                iCalidad = CalidadSeleccionada.ICalidadSeleccionada;
                txb_calidad.Text = CalidadSeleccionada.NombreCalidadSeleccionada;
                imgClickCalidad = true;
                CbxSubProducto();
            }
        }

        //
        private void btn_puntero_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 4;
            PersonalSeleccionado.TipoPersonal = "eca";
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                iSecador = PersonalSeleccionado.IPersonalPuntero;
                txb_nombrePuntero.Text = PersonalSeleccionado.NombrePersonalPuntero;
            }
        }

        //
        private void btn_catador_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 5;
            PersonalSeleccionado.TipoPersonal = "ata";
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                iCatador = PersonalSeleccionado.IPersonalCatador;
                txb_nombreCatador.Text = PersonalSeleccionado.NombrePersonalCatador;
            }
        }

        //
        private void btn_ubicacionCafe_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 6;
            imgClickBodega = true;
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                iBodega = BodegaSeleccionada.IdBodega;
                
                /*if(iBodega != AlmacenBodegaClick.IBodega)
                {
                    imgClickAlmacen = false;
                }
                if (!imgClickAlmacen)
                {
                }*/

                AlmacenBodegaClick.IBodega = iBodega;
                txb_ubicadoBodega.Text = BodegaSeleccionada.NombreBodega;
                txb_almacenSiloPiña.Text = null;
                iAlmacen = 0;
                AlmacenSeleccionado.NombreAlmacen = null;
                AlmacenSeleccionado.IAlmacen = 0;
            }
        }
        
        //
        private void btn_ubiFisicaCafe_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 7;
            //imgClickAlmacen = true;
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                // Llamar al método para obtener los datos de la base de datos
                AlmacenController almacenController = new AlmacenController();
                Almacen datoA = almacenController.ObtenerIdAlmacen(AlmacenSeleccionado.IAlmacen);
                BodegaController bodegaController = new BodegaController();
                Bodega datoB = bodegaController.ObtenerIdBodega(datoA.IdBodegaUbicacion);

                txb_ubicadoBodega.Text = datoB.NombreBodega;
                iBodega = datoB.IdBodega;
                imgClickBodega = false;
                Console.WriteLine("depuracion - id almacen nombre " + AlmacenSeleccionado.IAlmacen + AlmacenSeleccionado.NombreAlmacen);
                
                iAlmacen = AlmacenSeleccionado.IAlmacen;
                txb_almacenSiloPiña.Text = AlmacenSeleccionado.NombreAlmacen;
                Console.WriteLine("depuracion - id almacen nombre " + AlmacenSeleccionado.IAlmacen + AlmacenSeleccionado.NombreAlmacen);
            }
        }

        //
        private void btn_pesador_Click(object sender, EventArgs e)
        {
            TablaSeleccionadasubPartd.ITable = 8;
            PersonalSeleccionado.TipoPersonal = "esa";
            form_opcSubPartida form_OpcSub = new form_opcSubPartida();
            if (form_OpcSub.ShowDialog() == DialogResult.OK)
            {
                iPesador = PersonalSeleccionado.IPersonalPesador;
                txb_nombrePesador.Text = PersonalSeleccionado.NombrePersonalPesador;
            }
        }

        public void SaveSubPartida() 
        {

            //SubPartida subParti = new SubPartida();
            bool verific = VerificarCamposObligatorios();
            if(verific == true)
            {
                var almacenC = new AlmacenController();
                var cantSP = almacenC.ObtenerCantidadCafeAlmacen(iAlmacen);
                double cantMax = cantSP.CapacidadAlmacen;
                double cantAct = cantSP.CantidadActualAlmacen;
                double cantRest = cantMax - cantAct;
                double cantActSaco = cantSP.CantidadActualSacoAlmacen;
                double cantRestSaco = cantMax - cantActSaco;

                // Obtener los valores de los controles (txb, dtp, cbx)
                int subPartida = Convert.ToInt32(txb_subPartida.Text);
                string procedenciaNombre = txb_procedencia.Text;
                string calidadNombre = txb_calidad.Text;
                
                // Obtener el valor numérico seleccionado
                KeyValuePair<int, string> selectedStatus = new KeyValuePair<int, string>();
                if (cbx_subProducto.SelectedItem is KeyValuePair<int, string> keyValue)
                {
                    selectedStatus = keyValue;
                }
                else if (cbx_subProducto.SelectedItem != null)
                {
                    selectedStatus = (KeyValuePair<int, string>)cbx_subProducto.SelectedItem;
                }

                int selectedValue = selectedStatus.Key;
                string selectedValueName = selectedStatus.Value;

                int num1Semana = string.IsNullOrWhiteSpace(txb_pdasSemana1.Text) ? 0 : Convert.ToInt32(txb_pdasSemana1.Text);
                int num2Semana = string.IsNullOrWhiteSpace(txb_pdasSemana2.Text) ? 0 : Convert.ToInt32(txb_pdasSemana2.Text);
                int num3Semana = string.IsNullOrWhiteSpace(txb_pdasSemana3.Text) ? 0 : Convert.ToInt32(txb_pdasSemana3.Text);
                string dias1SubPartida = string.IsNullOrWhiteSpace(txb_diasPdas1.Text) ? "" : Convert.ToString(txb_diasPdas1.Text);
                string dias2SubPartida = string.IsNullOrWhiteSpace(txb_diasPdas2.Text) ? "" : Convert.ToString(txb_diasPdas2.Text);
                string dias3SubPartida = string.IsNullOrWhiteSpace(txb_diasPdas3.Text) ? "" : Convert.ToString(txb_diasPdas3.Text);
                
                string fecha1SubPartida = txb_fechaPartd1.Text;
                string fecha2SubPartida = txb_fechaPartd2.Text;
                string fecha3SubPartida = txb_fechaPartd3.Text;
                
                string observacionCafe = txb_observacionCafe.Text;
                DateTime fechaSecado = dtp_fechaSecado.Value.Date;

                //se verifica el formato de la hora 
                if (!ValidarHora(txb_horaInicio.Text))
                {
                    MessageBox.Show("El valor ingresado en el campo Inicio Secado no tiene un formato de hora válido. El formato es de 00 a 23 horas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!ValidarHora(txb_horaSalida.Text))
                {
                    MessageBox.Show("El valor ingresado en el campo Salida Secado no tiene un formato de hora válido. El formato es de 00 a 23 horas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //se verifica el formato de los minutos 
                if (!ValidarMinuto(txb_horaInicio.Text))
                {
                    MessageBox.Show("El valor ingresado en el campo Inicio Secado no tiene un formato de minuto válido. El formato es de 00 a 59 minuto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!ValidarMinuto(txb_horaSalida.Text))
                {
                    MessageBox.Show("El valor ingresado en el campo Salida Secado no tiene un formato de minuto válido. El formato es de 00 a 59 minuto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!ValidarMinuto(txb_tiempoSecad.Text))
                {
                    MessageBox.Show("El valor ingresado en el campo Tiempo de Secado no tiene un formato de minuto válido. El formato es de 00 a 59 minuto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Llamamos a la función ConvertTimeTxb y almacenamos el resultado formateado en la variable.
                string hhend = txb_horaSalida.Text;
                string hhstart = txb_horaInicio.Text;
                string hhsec = txb_tiempoSecad.Text;
                string inicioSecado = ConvertILimitTimeTxb(hhstart);
                string salidaSecado = ConvertILimitTimeTxb(hhend);

                // Convertir "tiempo secado" a un TimeSpan
                string horaFormateada = ConvertILimitTimeTxb(hhsec);
                string[] partes = horaFormateada.Split(':');
                TimeSpan tiempoSecado = new TimeSpan();
                if (partes.Length == 2)
                {
                    int horas = int.Parse(partes[0]);
                    int minutos = int.Parse(partes[1]);
                    tiempoSecado = new TimeSpan(horas, minutos, 0);
                }

                DateTime fechaInicioSecado = dtp_fechaInicioSecad.Value.Date + TimeSpan.Parse(inicioSecado);
                DateTime fechaSalidaSecado = dtp_fechaSalidaSecad.Value.Date + TimeSpan.Parse(salidaSecado);

                double humedadSecado;
                if (double.TryParse(txb_humedad.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out humedadSecado)) { }
                else
                {
                    MessageBox.Show("El valor ingresado en el campo Humedad no es un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                double rendimiento;
                if (double.TryParse(txb_rendimiento.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out rendimiento)){}
                else
                {
                    MessageBox.Show("El valor ingresado en el campo Rendimiento no es un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombrePunteroSecador = txb_nombrePuntero.Text;
                string observacionSecado = txb_observacionSecad.Text;

                string resultadoCatador = txb_resultadoCatacion.Text;
                DateTime fechaCatacion = dtp_fechaCatacion.Value;
                string observacionCatador = txb_observacionCatador.Text;
                DateTime fechaPesado = dtp_fechaPesa.Value;

                string nombreBodega = txb_ubicadoBodega.Text;
                string nombreAlmacen = txb_almacenSiloPiña.Text;
                string doctoAlmacen = txb_doctoAlmacen.Text;
                string nombrePunteroPesador = txb_nombrePesador.Text;
                string observacionPesador = txb_observacionPesa.Text;

                double pesoSaco;
                if (double.TryParse(txb_CantidadSaco.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out pesoSaco)){ cantidaSacoActUpdate = pesoSaco; }
                else
                {
                    MessageBox.Show("El valor ingresado en el campo Cantidad Saco no es un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                double pesoQQs;
                if (double.TryParse(txb_cantidadQQs.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out pesoQQs)){ cantidaQQsActUpdate = pesoQQs; }
                else
                {
                    MessageBox.Show("El valor ingresado en el campo Cantidad QQs no es un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear una instancia de la clase SubPartida con los valores obtenidos
                SubPartida subPart = new SubPartida()
                {
                    // Asignar los valores a las propiedades de la instancia
                    IdSubpartida = iSubPartida,
                    NumeroSubpartida = subPartida,
                    NombreProcedencia = procedenciaNombre,
                    IdProcedencia = iProcedencia,
                    NombreCalidadCafe = calidadNombre,
                    IdCalidadCafe = iCalidad,
                    IdSubProducto = selectedValue,
                    IdCosecha = CosechaActual.ICosechaActual,
                    NombreCosecha = CosechaActual.NombreCosechaActual,

                    Num1Semana = num1Semana,
                    Num2Semana = num2Semana,
                    Num3Semana = num3Semana,

                    Dias1SubPartida = dias1SubPartida,
                    Dias2SubPartida = dias2SubPartida,
                    Dias3SubPartida = dias3SubPartida,

                    Fecha1SubPartida = fecha1SubPartida,
                    Fecha2SubPartida = fecha2SubPartida,
                    Fecha3SubPartida = fecha3SubPartida,

                    ObservacionIdentificacionCafe = observacionCafe,
                    FechaSecado = fechaSecado,
                    InicioSecado = fechaInicioSecado,
                    SalidaSecado = fechaSalidaSecado,

                    TiempoSecado = tiempoSecado,
                    HumedadSecado = humedadSecado,
                    Rendimiento = rendimiento,
                    IdPunteroSecador = iSecador,
                    NombrePunteroSecador = nombrePunteroSecador,
                    ObservacionSecado = observacionSecado,
                    IdCatador = iCatador,
                    NombreCatador = resultadoCatador,
                    FechaCatacion = fechaCatacion,
                    ObservacionCatador = observacionCatador,
                    FechaPesado = fechaPesado,
                    PesaSaco = pesoSaco,
                    PesaQQs = pesoQQs,
                    IdBodega = (iBodega == 0) ? BodegaSeleccionada.IdBodega : iBodega,
                    NombreBodega = nombreBodega,
                    IdAlmacen = iAlmacen,
                    NombreAlmacen = nombreAlmacen,
                    DoctoAlmacen = doctoAlmacen,
                    IdPesador = iPesador,
                    NombrePunteroPesador = nombrePunteroPesador,
                    ObservacionPesador = observacionPesador
                };

                // Llamar al controlador para insertar la SubPartida en la base de datos
                LogController log = new LogController();
                var userControl = new UserController();
                var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                var subPartController = new SubPartidaController();

                //
                var almCM = almacenC.ObtenerCantidadCafeAlmacen(iAlmacen);
                var almNCM = almacenC.ObtenerAlmacenNombreCalidad(iAlmacen);
                double actcantidad = almCM.CantidadActualAlmacen;
                double actcantidadSaco = almCM.CantidadActualSacoAlmacen;
                
                if (!SubPartidaSeleccionado.clickImg)
                {
                    bool verificexisten = subPartController.VerificarExistenciaSubPartida(CosechaActual.ICosechaActual, Convert.ToInt32(txb_subPartida.Text));
                    
                    if (!verificexisten)
                    {
                        if (cantRest < pesoQQs || cantAct == cantMax)
                        {
                            MessageBox.Show("Error, la cantidad QQs de cafe que desea Agregar al almacen excede sus limite. Desea Agregar la cantidad de " + pesoQQs + " en el contenido disponible " + cantAct, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (cantRestSaco < pesoSaco || cantActSaco == cantMax)
                        {
                            MessageBox.Show("Error, la cantidad en Saco de cafe que desea Agregar al almacen excede sus limite. Desea Agregar la cantidad de " + pesoSaco + " en el contenido disponible " + cantActSaco, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        /*if (almNCM.IdCalidadCafe != CalidadSeleccionada.ICalidadSeleccionada && (cantAct != 0 && cantActSaco != 0))
                        {
                            MessageBox.Show("La Calidad Cafe que se a seleccionado en el formulario no es compatible, La calidad a Agregar al almacen es " + almNCM.NombreCalidadCafe + " y a seleccionado la calidad "
                                + CalidadSeleccionada.NombreCalidadSeleccionada + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txb_calidad.Text = null;
                            CalidadSeleccionada.ICalidadSeleccionada = 0;
                            CalidadSeleccionada.NombreCalidadSeleccionada = "";
                            return;
                        }
                        */
                       /* if (almNCM.IdSubProducto != selectedValue && (cantAct != 0 && cantActSaco != 0))
                        {
                            MessageBox.Show("El SubProducto Cafe que se a seleccionado en el formulario no es compatible, El SubProducto a Agregar es " + almNCM.NombreSubProducto + " y a seleccionado el SubProducto "
                                + selectedValueName + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                       */
                        var cantidadCafeC = new CantidadSiloPiñaController();

                        CantidadSiloPiña cantidad = new CantidadSiloPiña()
                        {
                            FechaMovimiento = fechaPesado,
                            IdCosechaCantidad = CosechaActual.ICosechaActual,
                            CantidadCafe = pesoQQs,
                            CantidadCafeSaco = pesoSaco,
                            TipoMovimiento = "Entrada Cafe No.SubPartida " + subPartida,
                            IdAlmacenSiloPiña = iAlmacen
                        };
                        
                        bool exito = subPartController.InsertarSubPartida(subPart);

                        if (exito)
                        {
                            MessageBox.Show("SubPartida agregada correctamente.", "Insercion Satifactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            bool exitoregistroCantidad = cantidadCafeC.InsertarCantidadCafeSiloPiña(cantidad);
                            if (!exitoregistroCantidad)
                            {
                                MessageBox.Show("Error, Ocurrio un problema en la insercion de la cantidad de cafe verifique los campos QQs ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            double resultCa = actcantidad + pesoQQs; 
                            double resultCaSaco = actcantidadSaco + pesoSaco; 
                            almacenC.ActualizarCantidadEntradaCafeAlmacen(iAlmacen, resultCa, resultCaSaco, iCalidad, selectedValue);
                            
                            try
                            {
                                //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                                //verificar el departamento
                                log.RegistrarLog(usuario.IdUsuario, "Registro dato SubPartida", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva SubPartida a la base de datos");
                                SubPartidaSeleccionado.clickImg = false;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                            }

                            //borrar datos de los textbox
                            ClearDataTxb();
                            CountNumSubPartida();
                        }
                        else
                        {
                            MessageBox.Show("Error al agregar la SubPartida. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar la SubPartida. El numero de SubPartida ya existe en la cosecha actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                else
                {

                    var cantidadCafeC = new CantidadSiloPiñaController();
                    string search = "No.SubPartida " + SubPartidaSeleccionado.NombreSubParti;
                    var cantUpd = cantidadCafeC.BuscarCantidadSiloPiñaSub(search);
                    double cantObtQQs = cantUpd.CantidadCafe;
                    double cantObtSaco = cantUpd.CantidadCafeSaco;

                    CantidadSiloPiña cantidad = new CantidadSiloPiña()
                    {
                        IdCantidadCafe = cantUpd.IdCantidadCafe,
                        FechaMovimiento = fechaPesado,
                        IdCosechaCantidad = CosechaActual.ICosechaActual,
                        CantidadCafe = cantidaQQsActUpdate,
                        CantidadCafeSaco = cantidaSacoActUpdate,
                        IdAlmacenSiloPiña = cantUpd.IdAlmacenSiloPiña,
                        TipoMovimiento = "Entrada Cafe No.SubPartida " + Convert.ToInt32(txb_subPartida.Text)
                    };

                   /* if (iCalidadNoUpd != CalidadSeleccionada.ICalidadSeleccionada && CalidadSeleccionada.ICalidadSeleccionada != 0)
                    {
                        if (cantObtQQs != actcantidad && cantObtSaco != actcantidadSaco)
                        {
                            MessageBox.Show("La calidad cafe que desea actualizar en el almacen no es permitido ya que contiene otra calidad. Calidad Actual "
                                +almNCM.NombreCalidadCafe+ " Calidad a actualizar "+txb_calidad.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txb_calidad.Text = null;
                            CalidadSeleccionada.ICalidadSeleccionada = 0;
                            CalidadSeleccionada.NombreCalidadSeleccionada = "";
                            return;
                        }
                    }
                   */
                    if (cantidaQQsUpdate != cantidaQQsActUpdate || Convert.ToInt32(txb_subPartida.Text) != SubPartidaSeleccionado.NumSubPartida)
                    {
                        if (cantRest < pesoQQs)
                        {
                            MessageBox.Show("Error, la cantidad QQs de cafe que desea agregar al almacen excede sus limite. Desea Agregar la cantidad de " + pesoQQs + " en el espacio disponible " + cantRest + " de " + cantMax, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        bool exitoregistroCantidad = cantidadCafeC.ActualizarCantidadCafeSiloPiña(cantidad);
                        if (!exitoregistroCantidad)
                        {
                            MessageBox.Show("Error, Ocurrio un problema en la actualizacion de la cantidad de cafe verifique los campos QQs ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    bool exito = subPartController.ActualizarSubPartida(subPart);

                    if (exito)
                    {
                        if (cantidaQQsUpdate != cantidaQQsActUpdate)
                        {
                            double resultCaUpd = actcantidad + cantidaQQsActUpdate - cantidaQQsUpdate;
                            double resultCaNoUpd = actcantidad - cantidaQQsUpdate;
                            double resultCaUpdSaco = actcantidadSaco + cantidaSacoActUpdate - cantidaSacoUpdate;
                            double resultCaNoUpdSaco = actcantidadSaco - cantidaSacoUpdate;
                            
                            if(cantUpd.IdAlmacenSiloPiña != iAlmacen)
                            {
                                //no actualiza los id unicamnete la cantidad restara ya que detecto que el almacen es diferente 
                                almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(cantUpd.IdAlmacenSiloPiña, resultCaNoUpd, resultCaNoUpdSaco, iCalidad, selectedValue);
                                //cambia los nuevos datos ya que detecto que el almacen cambio 
                                almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(iAlmacen, resultCaUpd, resultCaUpdSaco, iCalidad, selectedValue);

                            }
                            else
                            {
                                almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(cantUpd.IdAlmacenSiloPiña, resultCaUpd, resultCaUpdSaco, iCalidad, selectedValue);

                            }
                        }

                        if (iCalidadNoUpd != CalidadSeleccionada.ICalidadSeleccionada && CalidadSeleccionada.ICalidadSeleccionada != 0)
                        {
                            if (cantObtQQs == actcantidad && cantObtSaco == actcantidadSaco)
                            {
                                almacenC.ActualizarCalidadAlmacen(iCalidad, iAlmacen);
                            }
                        }

                        MessageBox.Show("SubPartida Actualizada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                            //verificar el departamento
                            log.RegistrarLog(usuario.IdUsuario, "Actualizacion dato SubPartida", ModuloActual.NombreModulo, "Actualizacion", "Actualizo los datos de la SubPartida con id ( " + subPart.IdSubpartida + " ) en la base de datos");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                        }

                        //borrar datos de los textbox
                        ClearDataTxb();
                        CountNumSubPartida();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la SubPartida. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public bool VerificarCamposObligatorios()
        {
            // Verificar campo num_subpartida
            if (string.IsNullOrWhiteSpace(txb_subPartida.Text))
            {
                MessageBox.Show("El campo Subpartida está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_procedencia_subpartida
            if (string.IsNullOrWhiteSpace(txb_procedencia.Text))
            {
                MessageBox.Show("El campo Procedencia está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_calidad_cafe_subpartida
            if (string.IsNullOrWhiteSpace(txb_calidad.Text))
            {
                MessageBox.Show("El campo Calidad_Cafe está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo num1_semana_subpartida
            if (string.IsNullOrWhiteSpace(txb_pdasSemana1.Text))
            {
                MessageBox.Show("El campo Num1_Semana está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo dias1_subpartida
            if (string.IsNullOrWhiteSpace(txb_diasPdas1.Text))
            {
                MessageBox.Show("El campo Dias1_Subpartida está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo fecha1_subpartida
            if (string.IsNullOrWhiteSpace(txb_fechaPartd1.Text))
            {
                MessageBox.Show("La fechaPdas 1 está vacio y es obligatoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo fecha_carga_secado_subpartida
            if (dtp_fechaSecado.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("La fecha de carga de secado está sin seleccionar y es obligatoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo inicio_secado_subpartida
            if (dtp_fechaInicioSecad.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("La fecha de inicio de secado está sin seleccionar y es obligatoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo salida_punto_secado_subpartida
            if (dtp_fechaSalidaSecad.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("La fecha de salida de secado está sin seleccionar y es obligatoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo tiempo_secado_subpartida
            if (string.IsNullOrWhiteSpace(txb_tiempoSecad.Text))
            {
                MessageBox.Show("El campo Tiempo_Secado está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo humedad_secado_subpartida
            if (string.IsNullOrWhiteSpace(txb_humedad.Text))
            {
                MessageBox.Show("El campo Humedad_Secado está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo rendimiento_subpartida
            if (string.IsNullOrWhiteSpace(txb_rendimiento.Text))
            {
                MessageBox.Show("El campo Rendimiento está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_puntero_secado_subpartida
            if (string.IsNullOrWhiteSpace(txb_nombrePuntero.Text))
            {
                MessageBox.Show("El campo Puntero_Secado está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_catador_subpartida
            if (string.IsNullOrWhiteSpace(txb_nombreCatador.Text))
            {
                MessageBox.Show("El campo nombre Catador está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo fecha_catacion_subpartida
            if (dtp_fechaCatacion.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("La fecha de catación está sin seleccionar y es obligatoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo fecha_pesado_subpartida
            if (dtp_fechaPesa.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("La fecha de pesado está sin seleccionar y es obligatoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo peso_saco_subpartida
            if (string.IsNullOrWhiteSpace(txb_CantidadSaco.Text))
            {
                MessageBox.Show("El campo cantidad_Saco está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo peso_qqs_subpartida
            if (string.IsNullOrWhiteSpace(txb_cantidadQQs.Text))
            {
                MessageBox.Show("El campo cantidad_QQs está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_almacen_subpartida
            if (string.IsNullOrWhiteSpace(txb_almacenSiloPiña.Text))
            {
                MessageBox.Show("El campo Almacen está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_bodega_subpartida
            if (string.IsNullOrWhiteSpace(txb_ubicadoBodega.Text))
            {
                MessageBox.Show("El campo Bodega está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_pesador_subpartida
            if (string.IsNullOrWhiteSpace(txb_nombrePesador.Text))
            {
                MessageBox.Show("El campo nombre Pesador está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            // Verificar campo id_pesador_subpartida
            if (string.IsNullOrWhiteSpace(txb_doctoAlmacen.Text))
            {
                MessageBox.Show("El campo Documento de Almacen está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true; // Si todos los campos obligatorios están completos, retornamos true
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> {txb_subPartida,txb_procedencia,txb_calidad,
                                                    txb_pdasSemana1,txb_pdasSemana2,txb_pdasSemana3,txb_diasPdas1,txb_diasPdas2,txb_diasPdas3,
                                                    txb_observacionCafe,txb_horaInicio,txb_horaSalida,txb_tiempoSecad,txb_humedad,txb_rendimiento,
                                                    txb_nombrePuntero,txb_observacionSecad,txb_resultadoCatacion,
                                                    txb_nombreCatador,txb_observacionCatador,txb_CantidadSaco,txb_cantidadQQs,
                                                    txb_ubicadoBodega,txb_almacenSiloPiña,txb_nombrePesador,txb_doctoAlmacen,txb_observacionPesa,
                                                    txb_fechaPartd1,txb_fechaPartd2,txb_fechaPartd3};

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }

            cbx_subProducto.SelectedIndex = -1;
            SubPartidaSeleccionado.clickImg = false;
            SubPartidaSeleccionado.NumSubPartida = 0;
            SubPartidaSeleccionado.ISubPartida = 0;

            imgClickBodega = false;
            imagenClickeada = false;
            imgClickCalidad = false;

            AlmacenSeleccionado.IAlmacen = 0;
            BodegaSeleccionada.IdBodega = 0;
            AlmacenBodegaClick.IBodega = 0;
            CalidadSeleccionada.ICalidadSeleccionada = 0;
            CalidadSeleccionada.NombreCalidadSeleccionada = "";
            ProcedenciaSeleccionada.IProcedencia = 0;
            iSubPartida = 0;
            iBodega = 0;
            dtp_fechaSecado.Value = DateTime.Now;
            dtp_fechaInicioSecad.Value = DateTime.Now;
            dtp_fechaSalidaSecad.Value = DateTime.Now;
            dtp_fechaCatacion.Value = DateTime.Now;
            dtp_fechaPesa.Value = DateTime.Now;

        }

        //
        private void CountNumSubPartida()
        {
            countSP = new SubPartidaController();
            var tril = countSP.CountSubPartida(CosechaActual.ICosechaActual);
            //
            txb_subPartida.Text = Convert.ToString(tril.CountSubPartida + 1);
        }

        //
        private void btn_SaveUser_Click(object sender, EventArgs e)
        {
            SaveSubPartida();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            this.Close();
        }

        private void btn_deleteSPartida_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (SubPartidaSeleccionado.NumSubPartida != 0 || Convert.ToInt32(txb_subPartida.Text) != 0 || !string.IsNullOrEmpty(txb_subPartida.Text))
            {
                countSP = new SubPartidaController();
                bool verificexisten = countSP.VerificarExistenciaSubPartida(CosechaActual.ICosechaActual, Convert.ToInt32(txb_subPartida.Text));

                if (verificexisten)
                {
                    LogController log = new LogController();
                    UserController userControl = new UserController();

                    if (SubPartidaSeleccionado.ISubPartida == 0)
                    {
                        SubPartidaSeleccionado.NumSubPartida = Convert.ToInt32(txb_subPartida.Text);
                        SubPartidaSeleccionado.ISubPartida = iSubPartida;
                    }

                    Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro SubPartida No: " + SubPartidaSeleccionado.NumSubPartida + ", de la cosecha: " + CosechaActual.NombreCosechaActual + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Obtener el valor numérico seleccionado
                        KeyValuePair<int, string> selectedStatus = new KeyValuePair<int, string>();
                        if (cbx_subProducto.SelectedItem is KeyValuePair<int, string> keyValue)
                        {
                            selectedStatus = keyValue;
                        }
                        else if (cbx_subProducto.SelectedItem != null)
                        {
                            selectedStatus = (KeyValuePair<int, string>)cbx_subProducto.SelectedItem;
                        }

                        int selectedValue = selectedStatus.Key;

                        //se llama la funcion delete del controlador para eliminar el registro
                        SubPartidaController controller = new SubPartidaController();
                        controller.EliminarSubPartida(SubPartidaSeleccionado.ISubPartida);

                        Console.WriteLine("numero de subpartida " + SubPartidaSeleccionado.NumSubPartida);
                        var cantidadCafeC = new CantidadSiloPiñaController();
                        string search = "No.SubPartida " + SubPartidaSeleccionado.NumSubPartida;
                        var cantUpd = cantidadCafeC.BuscarCantidadSiloPiñaSub(search);
                        Console.WriteLine("cantidad silo/piña a eliminar es " + search + " del numero de subpartida " + SubPartidaSeleccionado.NumSubPartida);
                        var almacenC = new AlmacenController();
                        var almCM = almacenC.ObtenerCantidadCafeAlmacen(iAlmacen);
                        double actcantidad = almCM.CantidadActualAlmacen;
                        double actcantidadSaco = almCM.CantidadActualSacoAlmacen;

                        double resultCaUpd = actcantidad - cantidaQQsUpdate;
                        double resultCaUpdSaco = actcantidadSaco - cantidaSacoUpdate;
                        almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(iAlmacen, resultCaUpd, resultCaUpdSaco, iCalidad, selectedValue);

                        cantidadCafeC.EliminarCantidadSiloPiña(cantUpd.IdCantidadCafe);

                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato SubPartida", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos de la SubPartida No: " + SubPartidaSeleccionado.NumSubPartida + " en la base de datos");

                        if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                        {
                            MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("SubPartida Eliminada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        //se actualiza la tabla
                        ClearDataTxb();
                        cbx_subProducto.SelectedIndex = -1;
                        CountNumSubPartida();
                    }
                    else
                    {
                        ClearDataTxb();
                        CountNumSubPartida();
                        return;
                    }
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_pdfSPartida_Click(object sender, EventArgs e)
        {
            var Nombre_Usuario = userC.ObtenerUsuariosNombresID(UsuarioActual.IUsuario);
            if (SubPartidaSeleccionado.ISubPartida != 0)
            {
            string reportPath = "../../views/Reports/report_numsubpartida.rdlc";
            List<ReportSubPartida> data = reportesController.ObtenerSubPartida(SubPartidaSeleccionado.ISubPartida);
                foreach (ReportSubPartida reporte in data)
                {
                    reporte.nombre_persona = Nombre_Usuario.ApellidoPersonaUsuario;

                }
                ReportDataSource reportDataSource = new ReportDataSource("repor_numsubpartida", data);
            form_opcReportExistencias reportSPartida = new form_opcReportExistencias(reportPath, reportDataSource);
            reportSPartida.ShowDialog();
             }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        public void RestrictTextBoxNumTime(List<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.KeyPress += (sender, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
                    {
                        e.Handled = true; // Cancela el evento KeyPress si no es un dígito o el carácter '/'
                    }

                    // Permite solo un '/' en el TextBox
                    if (e.KeyChar == '/' && (textBox.Text.Contains("/")))
                    {
                        e.Handled = true; // Cancela el evento KeyPress si ya hay un '/' en el TextBox
                    }
                };
            }
        }

        private void txb_horaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 5;

            if (txb_horaInicio.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
            else if (char.IsDigit(e.KeyChar))
            {
                // Agregar el dígito a la caja de texto
                txb_horaInicio.Text += e.KeyChar;

                // Verificar si ya existe un ':' en la cadena
                bool colonExists = txb_horaInicio.Text.Contains(":");

                // Agregar automáticamente un ':' si no existe y la longitud es par
                if (!colonExists && txb_horaInicio.Text.Length == 2)
                {
                    txb_horaInicio.Text += ":";
                    txb_horaInicio.SelectionStart = txb_horaInicio.Text.Length; // Mover el cursor al final
                }

                e.Handled = true; // Manejar el evento KeyPress
            }
        }

        private void txb_horaSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 5;

            if (txb_horaSalida.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
            else if (char.IsDigit(e.KeyChar))
            {
                // Agregar el dígito a la caja de texto
                txb_horaSalida.Text += e.KeyChar;

                // Verificar si ya existe un ':' en la cadena
                bool colonExists = txb_horaSalida.Text.Contains(":");

                // Agregar automáticamente un ':' si no existe y la longitud es par
                if (!colonExists && txb_horaSalida.Text.Length == 2)
                {
                    txb_horaSalida.Text += ":";
                    txb_horaSalida.SelectionStart = txb_horaSalida.Text.Length; // Mover el cursor al final
                }

                e.Handled = true; // Manejar el evento KeyPress
            }
        }

        private void txb_tiempoSecad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 5;

            if (txb_tiempoSecad.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
            else if (char.IsDigit(e.KeyChar))
            {
                // Agregar el dígito a la caja de texto
                txb_tiempoSecad.Text += e.KeyChar;

                // Verificar si ya existe un ':' en la cadena
                bool colonExists = txb_tiempoSecad.Text.Contains(":");

                // Agregar automáticamente un ':' si no existe y la longitud es par
                if (!colonExists && txb_tiempoSecad.Text.Length == 2)
                {
                    txb_tiempoSecad.Text += ":";
                    txb_tiempoSecad.SelectionStart = txb_tiempoSecad.Text.Length; // Mover el cursor al final
                }

                e.Handled = true; // Manejar el evento KeyPress
            }
        }

        private void txb_humedad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 4;

            if (txb_humedad.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_rendimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 4;

            if (txb_rendimiento.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_CantidadSaco_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 8;

            if (txb_CantidadSaco.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_doctoAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 200;

            if (txb_doctoAlmacen.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_pdasSemana1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 2;

            if (txb_pdasSemana1.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_diasPdas1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 10;

            if (txb_diasPdas1.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }

        }

        private void txb_fechaPartd1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 16;
            string currentText = txb_fechaPartd1.Text;

            if (txb_fechaPartd1.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
            else if (e.KeyChar == '/' ||
             e.KeyChar == 'a' && !currentText.Contains("a") ||
             e.KeyChar == 'l' && !currentText.Contains("l") ||
             char.IsDigit(e.KeyChar) ||
             e.KeyChar == ' ' ||
             e.KeyChar == (char)Keys.Back)
            {
                // Verificar si se está intentando ingresar un segundo espacio
                if (e.KeyChar == ' ' && currentText.EndsWith(" "))
                {
                    e.Handled = true; // Cancelar entrada si se intenta ingresar un segundo espacio
                    return;
                }

                // Permitir entrada de dígitos, diagonales, 'a', 'l', espacios y tecla de retroceso
                e.Handled = false;
            }
            else
            {
                e.Handled = true; // Cancelar entrada de otros caracteres
            }
        }

        private void txb_subPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 4;

            if (txb_subPartida.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_cantidadQQs_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 8;

            if (txb_cantidadQQs.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_fechaPartd2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 16;
            string currentText = txb_fechaPartd2.Text;

            if (txb_fechaPartd2.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
            else if (e.KeyChar == '/' ||
             e.KeyChar == 'a' && !currentText.Contains("a") ||
             e.KeyChar == 'l' && !currentText.Contains("l") ||
             char.IsDigit(e.KeyChar) ||
             e.KeyChar == ' ' ||
             e.KeyChar == (char)Keys.Back)
            {
                // Verificar si se está intentando ingresar un segundo espacio
                if (e.KeyChar == ' ' && currentText.EndsWith(" "))
                {
                    e.Handled = true; // Cancelar entrada si se intenta ingresar un segundo espacio
                    return;
                }

                // Permitir entrada de dígitos, diagonales, 'a', 'l', espacios y tecla de retroceso
                e.Handled = false;
            }
            else
            {
                e.Handled = true; // Cancelar entrada de otros caracteres
            }
        }

        private void txb_fechaPartd3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 16;
            string currentText = txb_fechaPartd3.Text;

            if (txb_fechaPartd3.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
            else if (e.KeyChar == '/' ||
             e.KeyChar == 'a' && !currentText.Contains("a") ||
             e.KeyChar == 'l' && !currentText.Contains("l") ||
             char.IsDigit(e.KeyChar) ||
             e.KeyChar == ' ' ||
             e.KeyChar == (char)Keys.Back)
            {
                // Verificar si se está intentando ingresar un segundo espacio
                if (e.KeyChar == ' ' && currentText.EndsWith(" "))
                {
                    e.Handled = true; // Cancelar entrada si se intenta ingresar un segundo espacio
                    return;
                }

                // Permitir entrada de dígitos, diagonales, 'a', 'l', espacios y tecla de retroceso
                e.Handled = false;
            }
            else
            {
                e.Handled = true; // Cancelar entrada de otros caracteres
            }
        }

        private void txb_diasPdas2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 50;

            if (txb_diasPdas2.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }


        }

        private void txb_diasPdas3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 50;

            if (txb_diasPdas3.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }

        }

        private void txb_pdasSemana2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 2;

            if (txb_pdasSemana2.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_pdasSemana3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 2;

            if (txb_pdasSemana3.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_horaInicio_Enter(object sender, EventArgs e)
        {
            if (txb_horaInicio.Text == "00:00")
            {
                txb_horaInicio.Text = string.Empty;
            }
        }

        private void txb_horaInicio_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_horaInicio.Text))
            {
                txb_horaInicio.Text = "00:00";
            }
        }

        private void txb_horaSalida_Enter(object sender, EventArgs e)
        {
            if (txb_horaSalida.Text == "00:00")
            {
                txb_horaSalida.Text = string.Empty;
            }
        }

        private void txb_horaSalida_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_horaSalida.Text))
            {
                txb_horaSalida.Text = "00:00";
            }
        }

        private void txb_tiempoSecad_Enter(object sender, EventArgs e)
        {
            if (txb_tiempoSecad.Text == "00:00")
            {
                txb_tiempoSecad.Text = string.Empty;
            }
        }

        private void txb_tiempoSecad_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_tiempoSecad.Text))
            {
                txb_tiempoSecad.Text = "00:00";
            }
        }

        private void txb_observacionCafe_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 250;

            if (txb_observacionCafe.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_observacionSecad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 250;

            if (txb_observacionSecad.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_resultadoCatacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 250;

            if (txb_resultadoCatacion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_observacionCatador_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 250;

            if (txb_observacionCatador.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_observacionPesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 250;

            if (txb_observacionPesa.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_fechaPartd1_Validating(object sender, CancelEventArgs e)
        {
            string fechaTexto = txb_fechaPartd1.Text.Trim();

            if (!string.IsNullOrEmpty(txb_fechaPartd1.Text))
            {
                // Intenta convertir el texto a una fecha
                if (fechaTexto.Contains(" al "))
                {
                    string[] partesFecha = fechaTexto.Split(new string[] { " al " }, StringSplitOptions.None);
                    if (partesFecha.Length == 2 &&
                        DateTime.TryParseExact(partesFecha[1].Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaFin))
                    {
                        // Verifica si el primer valor antes de "al" es un día válido (entre 01 y 31)
                        if (int.TryParse(partesFecha[0], out int diaInicio) && diaInicio >= 1 && diaInicio <= 31)
                        {
                            // Verifica si el día de inicio es válido para el mes y año proporcionados
                            int añoActual = DateTime.Now.Year;
                            int mesActual = DateTime.Now.Month;

                            if (diaInicio <= DateTime.DaysInMonth(fechaFin.Year, fechaFin.Month) &&
                                fechaFin.Year <= añoActual && fechaFin.Month <= mesActual)
                            {
                                // Crea una nueva fecha con el día corregido y valida si es futura
                                DateTime fechaInicio = new DateTime(fechaFin.Year, fechaFin.Month, diaInicio);
                                if (fechaInicio > DateTime.Now)
                                {
                                    e.Cancel = true;
                                    MessageBox.Show("La fecha de inicio no puede ser futura.");
                                }
                            }
                            else
                            {
                                e.Cancel = true;
                                MessageBox.Show("La fecha de inicio no es válida para el mes y año proporcionados.");
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            MessageBox.Show("El día de inicio debe ser un número válido entre 01 y 31.");
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        MessageBox.Show("Formato de fecha incorrecto. Debe ser dd/MM/yyyy o dd al dd/MM/yyyy.");
                    }
                }
                else
                {
                    if (!DateTime.TryParseExact(fechaTexto, new string[] { "dd/MM/yyyy", "dd al dd/MM/yyyy" },
                                                null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Formato de fecha incorrecto. Debe ser dd/MM/yyyy o dd al dd/MM/yyyy.");
                    }
                    else if (fecha > DateTime.Now)
                    {
                        e.Cancel = true;
                        MessageBox.Show("La fecha no puede ser futura.");
                    }
                    else if (fecha.Day < 1 || fecha.Day > DateTime.DaysInMonth(fecha.Year, fecha.Month))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Día no válido para el mes seleccionado.");
                    }
                    else if (fecha.Month < 1 || fecha.Month > 12)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Mes no válido. Debe estar entre 01 y 12.");
                    }
                }

            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label1, label2, label4, label5,label6,label7, label8,label9,label10,label11,
                                label12,label13,label14,label15,label16,label17,label18,label19,label20,label21,
                                label22,label23,label24,label25,label26,label27,label28,label29,label30,label31,
                                label32,label33,label34,label35,label36,label37,label38};
            TextBox[] textBoxes = { txb_almacenSiloPiña,txb_calidad,txb_cantidadQQs,txb_CantidadSaco,txb_cosecha,txb_diasPdas1,
                                    txb_diasPdas2,txb_diasPdas3,txb_doctoAlmacen,txb_fechaPartd1,txb_fechaPartd2,txb_fechaPartd3,
                                    txb_horaInicio,txb_horaSalida,txb_humedad,txb_nombreCatador,txb_nombrePesador,txb_nombrePuntero,
                                    txb_observacionCafe,txb_observacionCatador,txb_observacionPesa,txb_observacionSecad,txb_pdasSemana1,
                                    txb_pdasSemana2,txb_pdasSemana3,txb_procedencia,txb_rendimiento,txb_resultadoCatacion,txb_subPartida,txb_tiempoSecad,
                                    txb_ubicadoBodega,txb_codCalidad,txb_codProcedencia,txb_codPuntero,txb_codCatador,txb_codPesador,txb_codUbicacion,txb_codSilo};
            Button[] buttons = { btn_SaveUser, btn_Cancel };
            ComboBox[] comboBoxes = {cbx_subProducto };
            DateTimePicker[] dateTimePickers = {dtp_fechaCatacion,dtp_fechaInicioSecad,dtp_fechaPesa,dtp_fechaSalidaSecad,
                                                dtp_fechaSecado};

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleGC(buttons);
            //se asigna a combobox
            FontViews.ComboBoxStyle(comboBoxes);
            //se asigna a Fechas
            FontViews.DateStyle(dateTimePickers);
        }

        private void txb_fechaPartd2_Validating(object sender, CancelEventArgs e)
        {
            string fechaTexto = txb_fechaPartd2.Text.Trim();

            if (!string.IsNullOrEmpty(txb_fechaPartd2.Text))
            {
                // Intenta convertir el texto a una fecha
                if (fechaTexto.Contains(" al "))
                {
                    string[] partesFecha = fechaTexto.Split(new string[] { " al " }, StringSplitOptions.None);
                    if (partesFecha.Length == 2 &&
                        DateTime.TryParseExact(partesFecha[1].Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaFin))
                    {
                        // Verifica si el primer valor antes de "al" es un día válido (entre 01 y 31)
                        if (int.TryParse(partesFecha[0], out int diaInicio) && diaInicio >= 1 && diaInicio <= 31)
                        {
                            // Verifica si el día de inicio es válido para el mes y año proporcionados
                            int añoActual = DateTime.Now.Year;
                            int mesActual = DateTime.Now.Month;

                            if (diaInicio <= DateTime.DaysInMonth(fechaFin.Year, fechaFin.Month) &&
                                fechaFin.Year <= añoActual && fechaFin.Month <= mesActual)
                            {
                                // Crea una nueva fecha con el día corregido y valida si es futura
                                DateTime fechaInicio = new DateTime(fechaFin.Year, fechaFin.Month, diaInicio);
                                if (fechaInicio > DateTime.Now)
                                {
                                    e.Cancel = true;
                                    MessageBox.Show("La fecha de inicio no puede ser futura.");
                                }
                            }
                            else
                            {
                                e.Cancel = true;
                                MessageBox.Show("La fecha de inicio no es válida para el mes y año proporcionados.");
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            MessageBox.Show("El día de inicio debe ser un número válido entre 01 y 31.");
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        MessageBox.Show("Formato de fecha incorrecto. Debe ser dd/MM/yyyy o dd al dd/MM/yyyy.");
                    }
                }
                else
                {
                    if (!DateTime.TryParseExact(fechaTexto, new string[] { "dd/MM/yyyy", "dd al dd/MM/yyyy" },
                                                null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Formato de fecha incorrecto. Debe ser dd/MM/yyyy o dd al dd/MM/yyyy.");
                    }
                    else if (fecha > DateTime.Now)
                    {
                        e.Cancel = true;
                        MessageBox.Show("La fecha no puede ser futura.");
                    }
                    else if (fecha.Day < 1 || fecha.Day > DateTime.DaysInMonth(fecha.Year, fecha.Month))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Día no válido para el mes seleccionado.");
                    }
                    else if (fecha.Month < 1 || fecha.Month > 12)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Mes no válido. Debe estar entre 01 y 12.");
                    }
                }

            }
        }

        private void txb_fechaPartd3_Validating(object sender, CancelEventArgs e)
        {
            string fechaTexto = txb_fechaPartd3.Text.Trim();

            if (!string.IsNullOrEmpty(txb_fechaPartd3.Text))
            {
                // Intenta convertir el texto a una fecha
                if (fechaTexto.Contains(" al "))
                {
                    string[] partesFecha = fechaTexto.Split(new string[] { " al " }, StringSplitOptions.None);
                    if (partesFecha.Length == 2 &&
                        DateTime.TryParseExact(partesFecha[1].Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaFin))
                    {
                        // Verifica si el primer valor antes de "al" es un día válido (entre 01 y 31)
                        if (int.TryParse(partesFecha[0], out int diaInicio) && diaInicio >= 1 && diaInicio <= 31)
                        {
                            // Verifica si el día de inicio es válido para el mes y año proporcionados
                            int añoActual = DateTime.Now.Year;
                            int mesActual = DateTime.Now.Month;

                            if (diaInicio <= DateTime.DaysInMonth(fechaFin.Year, fechaFin.Month) &&
                                fechaFin.Year <= añoActual && fechaFin.Month <= mesActual)
                            {
                                // Crea una nueva fecha con el día corregido y valida si es futura
                                DateTime fechaInicio = new DateTime(fechaFin.Year, fechaFin.Month, diaInicio);
                                if (fechaInicio > DateTime.Now)
                                {
                                    e.Cancel = true;
                                    MessageBox.Show("La fecha de inicio no puede ser futura.");
                                }
                            }
                            else
                            {
                                e.Cancel = true;
                                MessageBox.Show("La fecha de inicio no es válida para el mes y año proporcionados.");
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            MessageBox.Show("El día de inicio debe ser un número válido entre 01 y 31.");
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        MessageBox.Show("Formato de fecha incorrecto. Debe ser dd/MM/yyyy o dd al dd/MM/yyyy.");
                    }
                }
                else
                {
                    if (!DateTime.TryParseExact(fechaTexto, new string[] { "dd/MM/yyyy", "dd al dd/MM/yyyy" },
                                                null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Formato de fecha incorrecto. Debe ser dd/MM/yyyy o dd al dd/MM/yyyy.");
                    }
                    else if (fecha > DateTime.Now)
                    {
                        e.Cancel = true;
                        MessageBox.Show("La fecha no puede ser futura.");
                    }
                    else if (fecha.Day < 1 || fecha.Day > DateTime.DaysInMonth(fecha.Year, fecha.Month))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Día no válido para el mes seleccionado.");
                    }
                    else if (fecha.Month < 1 || fecha.Month > 12)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Mes no válido. Debe estar entre 01 y 12.");
                    }
                }

            }
        }

        private void ResponsiveConfig()
        {
            this.Size = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 490);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
        }

        private void txb_subPartida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Evitar que se genere el "ding" de sonido de Windows

                int numS = Convert.ToInt32(txb_subPartida.Text);
                ClearDataTxb();
                txb_subPartida.Text = Convert.ToString(numS);
                countSP = new SubPartidaController();
                bool verificexisten = countSP.VerificarExistenciaSubPartida(CosechaActual.ICosechaActual, Convert.ToInt32(txb_subPartida.Text));

                if (verificexisten)
                {
                    ShowSubPartidaView(txb_subPartida); // Llamar a la función de búsqueda que desees
                    SubPartidaSeleccionado.clickImg = true;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Desea Agregar una nueva SubPartida", "¿Agregar SubPartida?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        ClearDataTxb();
                        txb_subPartida.Text = Convert.ToString(numS);
                    }
                }
            }
        }


        private void txb_codCalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string codigo = txb_codCalidad.Text.Trim();
                    int codigoInt;

                    if (int.TryParse(codigo, out codigoInt))
                    {
                        // Asegurarte de manejar el caso en el que ObtenerNombrePorCodigo devuelve null
                        CalidadCafe calidad = cCafe.ObtenerNombrePorCodigo(codigoInt);

                        if (calidad != null)
                        {
                            // Asignar el nombre al TextBox para mostrar el nombre
                            txb_calidad.Text = calidad.NombreCalidad;
                            CalidadSeleccionada.ICalidadSeleccionada = calidad.IdCalidad;
                            CalidadSeleccionada.NombreCalidadSeleccionada = calidad.NombreCalidad;
                            imgClickCalidad = true;
                            CbxSubProducto();
                        }
                        else
                        {
                            MessageBox.Show("ID no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que el texto no sea un número entero válido
                        MessageBox.Show("Por favor, ingrese un número entero válido como código.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el nombre: " + ex.Message);
                MessageBox.Show("Se produjo un error al obtener el nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txb_codPuntero_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string codigo = txb_codPuntero.Text.Trim();
                    int codigoInt;

                    if (int.TryParse(codigo, out codigoInt))
                    {
                        // Asegurarte de manejar el caso en el que ObtenerNombrePorCodigo devuelve null
                        Personal personal = pPersonal.ObtenerNombrePorCodigo(codigoInt);
                        
                        if (personal != null)
                        {
                            PersonalSeleccionado.TipoPersonal = "eca";
                            // Asignar el nombre al TextBox para mostrar el nombre
                            txb_nombrePuntero.Text = personal.NombrePersona;
                            PersonalSeleccionado.IPersonalPuntero = personal.IdPersonal;
                            PersonalSeleccionado.NombrePersonalPuntero = personal.NombrePersona;
                            imgClickCalidad = true;
                        }
                        else
                        {
                            MessageBox.Show("ID no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que el texto no sea un número entero válido
                        MessageBox.Show("Por favor, ingrese un número entero válido como código.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el nombre: " + ex.Message);
                MessageBox.Show("Se produjo un error al obtener el nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txb_codCatador_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string codigo = txb_codCatador.Text.Trim();
                    int codigoInt;

                    if (int.TryParse(codigo, out codigoInt))
                    {
                        // Asegurarte de manejar el caso en el que ObtenerNombrePorCodigo devuelve null
                        Personal personal = pPersonal.ObtenerNombrePorCodigo(codigoInt);

                        if (personal != null)
                        {
                            PersonalSeleccionado.TipoPersonal = "ata";
                            // Asignar el nombre al TextBox para mostrar el nombre
                            txb_nombreCatador.Text = personal.NombrePersona;
                            PersonalSeleccionado.IPersonalCatador = personal.IdPersonal;
                            PersonalSeleccionado.NombrePersonalCatador = personal.NombrePersonal;
                            imgClickCalidad = true;
                        }
                        else
                        {
                            MessageBox.Show("ID no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que el texto no sea un número entero válido
                        MessageBox.Show("Por favor, ingrese un número entero válido como código.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el nombre: " + ex.Message);
                MessageBox.Show("Se produjo un error al obtener el nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txb_codPesador_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string codigo = txb_codPesador.Text.Trim();
                    int codigoInt;

                    if (int.TryParse(codigo, out codigoInt))
                    {
                        // Asegurarte de manejar el caso en el que ObtenerNombrePorCodigo devuelve null
                        Personal personal = pPersonal.ObtenerNombrePorCodigo(codigoInt);

                        if (personal != null)
                        {
                            PersonalSeleccionado.TipoPersonal = "esa";
                            // Asignar el nombre al TextBox para mostrar el nombre
                            txb_nombrePesador.Text = personal.NombrePersona;
                            PersonalSeleccionado.IPersonalPesador = personal.IdPersonal;
                            PersonalSeleccionado.NombrePersonalPesador = personal.NombrePersonal;
                            imgClickCalidad = true;
                        }
                        else
                        {
                            MessageBox.Show("ID no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que el texto no sea un número entero válido
                        MessageBox.Show("Por favor, ingrese un número entero válido como código.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el nombre: " + ex.Message);
                MessageBox.Show("Se produjo un error al obtener el nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
