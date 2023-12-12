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
    public partial class form_trasladoCafe : Form
    {
        //variable para refrescar el formulario cad cierto tiempo
        private System.Timers.Timer refreshTimer;

        private List<TextBox> txbRestrict;
        private int icosechaCambio;
        TrasladoController countTrl = null;
        private TrasladoController reportesController = new TrasladoController();
        private string fechaActual;
        UserController userC = new UserController();
        public string rbSelect;
        public double cantidaQQsUpdate = 0.00;
        public double cantidaSacoUpdate = 0.00;
        public double cantidaQQsActUpdate = 0.00;
        public double cantidaSacoActUpdate = 0.00;
        private bool imgClickCalidad = false;
        private CCafeController cCafe = new CCafeController();
        private PersonalController pPersonal = new PersonalController();
        private int iTraslado;
        private int isubProducto;
        private int iPesador;
        private int iProcedencia;
        private int iBodegaProce;
        private int iAlmacenProce;
        private int iAlmacenProceUpd;
        private int iProcedenciaDest;
        private int iBodegaDest;
        private int iAlmacenDest;
        private int iCalidad;
        private int iCalidadNoUpd;

        public form_trasladoCafe()
        {
            InitializeComponent();

            //
            RolUser();
            ClearDataTxb();

            dtp_fechaTraslado.Format = DateTimePickerFormat.Short;

            // Configurar el temporizador para que se dispare cada cierto intervalo (por ejemplo, cada 5 segundos).
            refreshTimer = new System.Timers.Timer();
            refreshTimer.Interval = 5000; // Intervalo en milisegundos (5 segundos en este caso).
            refreshTimer.Elapsed += RefreshTimer_Elapsed;
            refreshTimer.Start();

            txbRestrict = new List<TextBox> { txb_pesoQQs, txb_pesoSaco };

            RestrictTextBoxNum(txbRestrict);

            CbxSubProducto();

            countTrl = new TrasladoController();
            var sald = countTrl.CountTraslado(CosechaActual.ICosechaActual);
            //
            TrasladoSeleccionado.ITraslado = 0;
            txb_numTraslado.Text = Convert.ToInt32(sald.CountTraslado + 1).ToString();
            txb_personal.Enabled = false;
            txb_personal.ReadOnly = true;
            txb_cosecha.Enabled = false;
            txb_cosecha.ReadOnly = true;
            txb_cosecha.Text = CosechaActual.NombreCosechaActual;
            txb_calidadCafe.Enabled = false;
            txb_calidadCafe.ReadOnly = true;
            txb_almacenPr.Enabled = false;
            txb_almacenPr.ReadOnly = true;
            txb_bodegaPr.Enabled = false;
            txb_bodegaPr.ReadOnly = true;
            txb_fincaPr.Enabled = false;
            txb_fincaPr.ReadOnly = true;
            txb_almacenDes.Enabled = false;
            txb_almacenDes.ReadOnly = true;
            txb_bodegaDes.Enabled = false;
            txb_bodegaDes.ReadOnly = true;
            txb_fincaDes.Enabled = false;
            txb_fincaDes.ReadOnly = true;
            cbx_subProducto.DropDownStyle = ComboBoxStyle.DropDownList;

            //
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
                        if (icosechaCambio != CosechaActual.ICosechaActual || string.IsNullOrWhiteSpace(txb_numTraslado.Text))
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
                        txb_numTraslado.Enabled = true;
                        btn_tTraslado.Click += btn_tTraslado_Click;
                        btn_pdfTraslado.Click += btn_pdfTraslado_Click;
                    }
                    break;
                case 3:
                    {
                        //Digitador
                        btn_deleteTraslado.Click -= btn_deleteTraslado_Click;
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
                    }
                    break;

            }
        }

        //
        private void BlockEventsComponents()
        {
            txb_numTraslado.Enabled = false;
            txb_pesoSaco.Enabled = false;
            txb_pesoQQs.Enabled = false;
            cbx_subProducto.Enabled = false;
            txb_observacion.Enabled = false;
            dtp_fechaTraslado.Enabled = false;
            btn_tTraslado.Click -= btn_tTraslado_Click;
            btn_deleteTraslado.Click -= btn_deleteTraslado_Click;
            btn_tAlmacenP.Click -= btn_tAlmacenP_Click;
            btn_tUbicacionP.Click -= btn_tUbicacionP_Click;
            btn_tFincaP.Click -= btn_tFincaP_Click;
            btn_tAlmacenD.Click -= btn_tAlmacenD_Click;
            btn_tUbicacionD.Click -= btn_tAlmacenD_Click;
            btn_tFincaD.Click -= btn_tFincaD_Click;
            btn_tCCafe.Click -= btn_tCCafe_Click;
            btn_tPesadores.Click -= btn_tPesadores_Click;
            btn_SaveTraslado.Click -= btn_SaveTraslado_Click;
            btn_pdfTraslado.Click -= btn_pdfTraslado_Click;
        }

        //
        public void ShowTrasladoView(TextBox txb)
        {
            var trl = new TrasladoController();
            SubProductoController subPro = new SubProductoController();
            
            var sub = new Traslado();

            if (TrasladoSeleccionado.ITraslado != 0)
            {
                sub = trl.ObtenerTrasladoPorIDNombre(TrasladoSeleccionado.ITraslado);
                txb_numTraslado.Text = Convert.ToString(TrasladoSeleccionado.NumTraslado);
                iTraslado = TrasladoSeleccionado.ITraslado;
            }
            else
            {
                sub = trl.ObtenerTrasladoPorCosechaIDNombre(Convert.ToInt32(txb.Text), CosechaActual.ICosechaActual);
                txb_numTraslado.Text = Convert.ToString(txb.Text);
                iTraslado = sub.Idtraslado_cafe;
                TrasladoSeleccionado.NumTraslado = sub.NumTraslado;
                TrasladoSeleccionado.ITraslado = sub.Idtraslado_cafe;
            }

            CbxSubProducto();
            var name = subPro.ObtenerSubProductoPorNombre(sub.NombreSubProducto);
            isubProducto = name.IdSubProducto;

            //cbx
            int isP = name.IdSubProducto - 1;

            // Obtener la fecha y la hora por separado
            DateTime fechaTraslado = sub.FechaTrasladoCafe.Date;

            AlmacenController almCtrl = new AlmacenController();
            var calidad = almCtrl.ObtenerAlmacenNombreCalidad(sub.IdCalidadCafe);
            CalidadSeleccionada.ICalidadSeleccionada = (int)calidad.IdCalidadCafe;
            CalidadSeleccionada.NombreCalidadSeleccionada = calidad.NombreCalidadCafe;

            txb_codCalidad.Text = Convert.ToString(CalidadSeleccionada.ICalidadSeleccionada);
            txb_codPesador.Text = Convert.ToString(sub.IdPersonal);
            dtp_fechaTraslado.Value = fechaTraslado;
            txb_calidadCafe.Text = sub.NombreCalidadCafe;
            iCalidad = sub.IdCalidadCafe;
            iCalidadNoUpd = sub.IdCalidadCafe;
            cbx_subProducto.SelectedIndex = isP;
            txb_observacion.Text = sub.ObservacionTraslado;
            txb_pesoSaco.Text = sub.CantidadTrasladoSacos.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
            txb_pesoQQs.Text = sub.CantidadTrasladoQQs.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
            cantidaQQsUpdate = sub.CantidadTrasladoQQs;
            cantidaSacoUpdate = sub.CantidadTrasladoSacos;
            txb_bodegaPr.Text = sub.NombreBodegaProcedencia;
            iBodegaProce = sub.IdBodegaProcedencia;
            txb_bodegaDes.Text = sub.NombreBodegaDestino;
            iBodegaDest = sub.IdBodegaDestino;
            txb_almacenPr.Text = sub.NombreAlmacenProcedencia;
            iAlmacenProce = sub.IdAlmacenProcedencia;
            iAlmacenProceUpd = sub.IdAlmacenProcedencia;
            txb_almacenDes.Text = sub.NombreAlmacenDestino;
            iAlmacenDest = sub.IdAlmacenDestino;
            txb_personal.Text = sub.NombrePersonal;
            iPesador = sub.IdPersonal;
            txb_fincaPr.Text = sub.NombreProcedencia;
            txb_fincaDes.Text = sub.NombreProcedenciaDestino;
            iProcedencia = sub.IdProcedencia;
            iProcedenciaDest = sub.IdProcedenciaDestino;

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
        public void CbxSubProducto()
        {
            SubProductoController subPro = new SubProductoController();
            List<SubProducto> datoSubPro;

            if (imgClickCalidad || CalidadSeleccionada.ICalidadSeleccionada != 0)
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
        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> {txb_almacenPr, txb_bodegaPr, txb_almacenDes, txb_bodegaDes, txb_calidadCafe, txb_fincaPr, txb_numTraslado,
                                                    txb_observacion, txb_fincaDes, txb_personal,txb_pesoQQs, txb_pesoSaco,txb_codCalidad,txb_codPesador};

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }

            cbx_subProducto.SelectedIndex = -1;
            TrasladoSeleccionado.clickImg = false;
            TrasladoSeleccionado.ITraslado = 0;
            TrasladoSeleccionado.NumTraslado = 0;
            AlmacenBodegaClick.IBodega = 0;
            dtp_fechaTraslado.Value = DateTime.Now;
            AlmacenSeleccionado.IAlmacen = 0;
            AlmacenSeleccionado.IAlmacenDestino = 0;
            AlmacenSeleccionado.NombreAlmacen = "";
            AlmacenSeleccionado.NombreAlmacenDestino = "";
            BodegaSeleccionada.IdBodega = 0;
            BodegaSeleccionada.IdBodegaDestino = 0;
            BodegaSeleccionada.NombreBodega = "";
            BodegaSeleccionada.NombreBodegaDestino = "";
            CalidadSeleccionada.ICalidadSeleccionada = 0;
            CalidadSeleccionada.NombreCalidadSeleccionada = "";
            imgClickCalidad = false;
            iTraslado = 0;
            iAlmacenDest = 0;
            iAlmacenProce = 0;

            countTrl = new TrasladoController();
            var sald = countTrl.CountTraslado(CosechaActual.ICosechaActual);
            //
            txb_numTraslado.Text = Convert.ToString(sald.CountTraslado + 1);
        }

        //
        public bool VerificarCamposObligatorios()
        {
            // Verificar campo num_subpartida
            if (string.IsNullOrWhiteSpace(txb_numTraslado.Text))
            {
                MessageBox.Show("El campo Numero de Traslado esta vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_calidad_cafe_subpartida
            if (string.IsNullOrWhiteSpace(txb_calidadCafe.Text))
            {
                MessageBox.Show("El campo Calidad_Cafe está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo fecha_carga_secado_subpartida
            if (dtp_fechaTraslado.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("La fecha de carga de Salida está sin seleccionar y es obligatoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo peso_saco_subpartida
            if (string.IsNullOrWhiteSpace(txb_pesoSaco.Text))
            {
                MessageBox.Show("El campo cantidad_Saco está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo peso_qqs_subpartida
            if (string.IsNullOrWhiteSpace(txb_pesoQQs.Text))
            {
                MessageBox.Show("El campo cantidad_QQs está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txb_fincaPr.Text) && (string.IsNullOrWhiteSpace(txb_almacenPr.Text) || string.IsNullOrWhiteSpace(txb_bodegaPr.Text)))
            {
                MessageBox.Show("El area de Procedencia del Cafe sus campos estan vacío y es necesario al menos llenar uno de ellos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txb_fincaDes.Text) && (string.IsNullOrWhiteSpace(txb_almacenDes.Text) || string.IsNullOrWhiteSpace(txb_bodegaDes.Text)))
            {
                MessageBox.Show("El area de Destino del Cafe sus campos estan vacío y es necesario al menos llenar uno de ellos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Verificar campo id_pesador_subpartida
            if (string.IsNullOrWhiteSpace(txb_personal.Text))
            {
                MessageBox.Show("El campo nombre Pesador está vacío y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txb_almacenPr.Text) || string.IsNullOrWhiteSpace(txb_bodegaPr.Text))
            {
                MessageBox.Show("El campo Almacen o el de Bodega en Procedencia esta vacío y es necesario llenar ambos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txb_almacenDes.Text) || string.IsNullOrWhiteSpace(txb_bodegaDes.Text))
            {
                MessageBox.Show("El campo Almacen o el de Bodega en Destino esta vacío y es necesario llenar ambos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true; // Si todos los campos obligatorios están completos, retornamos true
        }

        private void btn_tTraslado_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 1;
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                ShowTrasladoView(txb_numTraslado);
            }
        }

        private void btn_tAlmacenP_Click(object sender, EventArgs e)
        {
            try
            {
                TablaSeleccionadaTraslado.ITable = 2;
                form_opcTraslado opcTraslado = new form_opcTraslado();
                if (opcTraslado.ShowDialog() == DialogResult.OK)
                {
                    iAlmacenProce = AlmacenSeleccionado.IAlmacen;
                    if (AlmacenSeleccionado.IAlmacenDestino == AlmacenSeleccionado.IAlmacen || iAlmacenDest == iAlmacenProce)
                    {
                        MessageBox.Show("Error, no es permitido hacer un traslado al mismo Silo/Piña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        iAlmacenProce = 0;
                        AlmacenSeleccionado.IAlmacen = 0;
                        AlmacenSeleccionado.NombreAlmacen = "";
                        return;
                    }

                    // Llamar al método para obtener los datos de la base de datos
                    AlmacenController almacenController = new AlmacenController();
                    Almacen datoA = almacenController.ObtenerIdAlmacen(AlmacenSeleccionado.IAlmacen);
                    if (datoA.IdCalidadCafe == 0)
                    {
                        MessageBox.Show("El almacen seleccionado está vacio, no puede ser utilizado para traslado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    BodegaController bodegaController = new BodegaController();
                    Bodega datoB = bodegaController.ObtenerIdBodega(datoA.IdBodegaUbicacion);
                    CCafeController ccafeC = new CCafeController();
                    CalidadCafe ccafe = ccafeC.ObtenerIdCalidad((int)datoA.IdCalidadCafe);
                    //datos para subproducto
                    isubProducto = (int)datoA.IdSubProducto;

                    //cbx
                    cbx_subProducto.Items.Clear();
                    CbxSubProducto();
                    int isP = isubProducto - 1;
                    Console.WriteLine("Depurar - idSubpro: " + isP);

                    cbx_subProducto.SelectedIndex = isP;
                    txb_calidadCafe.Text = ccafe.NombreCalidad;
                    CalidadSeleccionada.ICalidadSeleccionada = ccafe.IdCalidad;
                    CalidadSeleccionada.NombreCalidadSeleccionada = ccafe.NombreCalidad;
                    txb_bodegaPr.Text = datoB.NombreBodega;
                    iBodegaProce = datoB.IdBodega;
                    BodegaSeleccionada.IdBodega = iBodegaProce;
                    BodegaSeleccionada.NombreBodega = datoB.NombreBodega;

                    txb_almacenPr.Text = AlmacenSeleccionado.NombreAlmacen;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de tipo " + ex.Message);
                MessageBox.Show("Error de tipo " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_tUbicacionP_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 3;
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                iBodegaProce = BodegaSeleccionada.IdBodega;

                AlmacenBodegaClick.IBodega = iBodegaProce;
                txb_bodegaPr.Text = BodegaSeleccionada.NombreBodega;
                txb_almacenPr.Text = null;
                iAlmacenProce = 0;
                AlmacenSeleccionado.NombreAlmacen = null;
                AlmacenSeleccionado.IAlmacen = 0;
            }
        }

        private void btn_tFincaP_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 4;
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                iProcedencia = ProcedenciaSeleccionada.IProcedencia;
                txb_fincaPr.Text = ProcedenciaSeleccionada.NombreProcedencia;
            }
        }

        private void btn_tCCafe_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 8;
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                iCalidad = CalidadSeleccionada.ICalidadSeleccionada;
                txb_calidadCafe.Text = CalidadSeleccionada.NombreCalidadSeleccionada;
                imgClickCalidad = true;
                CbxSubProducto();
            }
        }

        private void btn_tAlmacenD_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 5;
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                iAlmacenDest = AlmacenSeleccionado.IAlmacenDestino;
                if (AlmacenSeleccionado.IAlmacen == AlmacenSeleccionado.IAlmacenDestino || iAlmacenProce == iAlmacenDest)
                {
                    MessageBox.Show("Error, no es permitido hacer un traslado al mismo Silo/Piña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    iAlmacenDest = 0;
                    AlmacenSeleccionado.IAlmacenDestino = 0;
                    AlmacenSeleccionado.NombreAlmacenDestino = "";
                    return;
                }
                // Llamar al método para obtener los datos de la base de datos
                AlmacenController almacenController = new AlmacenController();
                Almacen datoA = almacenController.ObtenerIdAlmacen(AlmacenSeleccionado.IAlmacenDestino);
                BodegaController bodegaController = new BodegaController();
                Bodega datoB = bodegaController.ObtenerIdBodega(datoA.IdBodegaUbicacion);

                txb_bodegaDes.Text = datoB.NombreBodega;
                iBodegaDest = datoB.IdBodega;
                BodegaSeleccionada.IdBodegaDestino = iBodegaDest;
                BodegaSeleccionada.NombreBodegaDestino = datoB.NombreBodega;

                txb_almacenDes.Text = AlmacenSeleccionado.NombreAlmacenDestino;
            }
        }

        private void btn_tUbicacionD_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 6;
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                iBodegaDest = BodegaSeleccionada.IdBodegaDestino;

                AlmacenBodegaClick.IBodegaDestino = iBodegaDest;
                txb_bodegaDes.Text = BodegaSeleccionada.NombreBodegaDestino;

                txb_almacenDes.Text = null;
                iAlmacenDest = 0;
                AlmacenSeleccionado.NombreAlmacenDestino = null;
                AlmacenSeleccionado.IAlmacenDestino = 0;
            }
        }

        private void btn_tFincaD_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 7;
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                iProcedenciaDest = ProcedenciaSeleccionada.IProcedenciaDestino;
                txb_fincaDes.Text = ProcedenciaSeleccionada.NombreProcedenciaDestino;
            }
        }

        private void btn_tPesadores_Click(object sender, EventArgs e)
        {
            TablaSeleccionadaTraslado.ITable = 9;
            PersonalSeleccionado.TipoPersonal = "esa";
            form_opcTraslado opcTraslado = new form_opcTraslado();
            if (opcTraslado.ShowDialog() == DialogResult.OK)
            {
                iPesador = PersonalSeleccionado.IPersonalPesador;
                txb_personal.Text = PersonalSeleccionado.NombrePersonalPesador;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            this.Close();
        }

        //
        public void SaveTraslado()
        {
            bool verific = VerificarCamposObligatorios();
            if (!verific)
            {
                return;
            }

            var almacenC = new AlmacenController();
            //Procedencia
            var cantSPr = almacenC.ObtenerCantidadCafeAlmacen(AlmacenSeleccionado.IAlmacen);
            double cantMaxPr = cantSPr.CapacidadAlmacen;
            double cantActPr = cantSPr.CantidadActualAlmacen;
            double cantActSacoPr = cantSPr.CantidadActualSacoAlmacen;
            double cantRestPr = cantMaxPr - cantActPr;
            double cantRestSacoPr = cantMaxPr - cantActSacoPr;
            //Destino
            var cantSDes = almacenC.ObtenerCantidadCafeAlmacen(AlmacenSeleccionado.IAlmacenDestino);
            double cantMaxDes = cantSDes.CapacidadAlmacen;
            double cantActDes = cantSDes.CantidadActualAlmacen;
            double cantActSacoDes = cantSDes.CantidadActualSacoAlmacen;
            double cantRestDes = cantMaxDes - cantActDes;
            double cantRestSacoDes = cantMaxDes - cantActSacoDes;
            
            int numTraslado = Convert.ToInt32(txb_numTraslado.Text);
            string observacion = txb_observacion.Text;

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

            // Verificar si se ha seleccionado un rol de usuario
            if (cbx_subProducto.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un SubProducto en Datos del Producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double pesoSaco;
            if (double.TryParse(txb_pesoSaco.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out pesoSaco)) { cantidaSacoActUpdate = pesoSaco; }
            else
            {
                MessageBox.Show("El valor ingresado en el campo Cantidad Saco no es un número válido, verifique el separador decimal que sea punto y no coma. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double pesoQQs;
            if (double.TryParse(txb_pesoQQs.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out pesoQQs)) { cantidaQQsActUpdate = pesoQQs; }
            else
            {
                MessageBox.Show("El valor ingresado en el campo Cantidad QQs no es un número válido, verifique el separador decimal que sea punto y no coma. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime fechaTraslado = dtp_fechaTraslado.Value.Date;

            Traslado traslado = new Traslado()
            {
                NumTraslado = numTraslado,
                IdCosecha = CosechaActual.ICosechaActual,
                FechaTrasladoCafe = fechaTraslado,
                
                IdCalidadCafe = CalidadSeleccionada.ICalidadSeleccionada,
                IdSubProducto = selectedValue,
                CantidadTrasladoSacos = pesoSaco,
                CantidadTrasladoQQs = pesoQQs,

                IdPersonal = PersonalSeleccionado.IPersonalPesador,
                ObservacionTraslado = observacion,

                IdAlmacenProcedencia = AlmacenSeleccionado.IAlmacen,
                IdBodegaProcedencia = BodegaSeleccionada.IdBodega,
                IdProcedencia = ProcedenciaSeleccionada.IProcedencia,

                IdAlmacenDestino = AlmacenSeleccionado.IAlmacenDestino,
                IdBodegaDestino = BodegaSeleccionada.IdBodegaDestino,
                IdProcedenciaDestino = ProcedenciaSeleccionada.IProcedenciaDestino
            };

            //Se Llama e instancia la variable para obtenr los valores deseados de CantidadSiloPiña
            var historialSubProducProce = new CantidadSiloPiña();
            var historialSubProducDest = new CantidadSiloPiña();
            var histoialCantidad = new CantidadSiloPiñaController();
            historialSubProducProce = histoialCantidad.ObtenerCantidadSubProductoSiloPiña(CosechaActual.ICosechaActual, iAlmacenProce, selectedValue);
            historialSubProducDest = histoialCantidad.ObtenerCantidadSubProductoSiloPiña(CosechaActual.ICosechaActual, iAlmacenDest, selectedValue);
            
            List<CantidadSiloPiña> listaSubproductosProc = histoialCantidad.ObtenerSubProductoSiloPiña(iAlmacenProce);
            List<CantidadSiloPiña> listaSubproductosDest = histoialCantidad.ObtenerSubProductoSiloPiña(iAlmacenDest);

            // Llamar al controlador para insertar la SubPartida en la base de datos
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario

            var trasladoController = new TrasladoController();
            var verifyExistence = histoialCantidad.VerificarRegistroExistenteDestino(AlmacenSeleccionado.IAlmacenDestino, selectedValue);
            
            //Procedencia
            var almCM = almacenC.ObtenerCantidadCafeAlmacen(iAlmacenProce);
            var almNCM = almacenC.ObtenerAlmacenNombreCalidad(iAlmacenProce);
            double actcantidadPr = almCM.CantidadActualAlmacen;
            double actcantidadSacoPr = almCM.CantidadActualSacoAlmacen;
            //Destino
            var almCMDes = almacenC.ObtenerCantidadCafeAlmacen(iAlmacenDest);
            var almNCMDes = almacenC.ObtenerAlmacenNombreCalidad(iAlmacenDest);
            double actcantidadDes = almCMDes.CantidadActualAlmacen;
            double actcantidadSacoDes = almCMDes.CantidadActualSacoAlmacen;

            if (!TrasladoSeleccionado.clickImg)
            {
                bool verificexisten = trasladoController.VerificarExistenciaTraslado(CosechaActual.ICosechaActual, Convert.ToInt32(txb_numTraslado.Text));

                if (!verificexisten)
                {
                    if (historialSubProducProce.CantidadCafe < pesoQQs || historialSubProducProce.CantidadCafe == 0)
                    {
                        MessageBox.Show("Error, la cantidad QQs de cafe que desea Sacar en Procedencia del almacen excede sus limite. Desea Sacar la cantidad de " + pesoQQs + " en el contenido disponible " + historialSubProducProce.CantidadCafe
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (historialSubProducDest.CantidadCafe < pesoQQs || historialSubProducDest.CantidadCafe < 0)
                    {
                        MessageBox.Show("Error, la cantidad QQs de cafe que desea Agrega en Procedencia del almacen excede sus limite maximos. Desea Agregar la cantidad de " + pesoQQs + " en el contenido disponible " + historialSubProducDest.CantidadCafe
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    if (historialSubProducProce.CantidadCafeSaco < pesoSaco || historialSubProducProce.CantidadCafeSaco == 0)
                    {
                        MessageBox.Show("Error, la cantidad en Sacos de cafe que desea Sacar en Procedencia del almacen excede sus limite. Desea Sacar la cantidad de " + pesoSaco + " en el contenido disponible " + historialSubProducProce.CantidadCafeSaco
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (historialSubProducDest.CantidadCafeSaco < pesoSaco || historialSubProducDest.CantidadCafeSaco < 0)
                    {
                        MessageBox.Show("Error, la cantidad en Saco de cafe que desea Agrega en Procedencia del almacen excede sus limite maximos. Desea Agregar la cantidad de " + pesoSaco + " en el contenido disponible " + historialSubProducDest.CantidadCafeSaco
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int icalidadDesName = Convert.ToInt32(almNCMDes.IdCalidadCafe == null ? 0 : almNCMDes.IdCalidadCafe);
                    if (almNCM.IdCalidadCafe != icalidadDesName && (icalidadDesName != 0 && (actcantidadDes != 0 && actcantidadSacoDes != 0)))
                    {
                        MessageBox.Show("La Calidad Cafe que desea trasladar al Almacen de destino no es compatible, La calidad a trasladar es "+ almNCM.NombreCalidadCafe + " y el Almacen destino tiene la calidad actual " + almNCMDes.NombreCalidadCafe +".", 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if ((almNCM.IdCalidadCafe != CalidadSeleccionada.ICalidadSeleccionada || icalidadDesName != CalidadSeleccionada.ICalidadSeleccionada) && (icalidadDesName != 0 && (actcantidadDes != 0 && actcantidadSacoDes != 0)))
                    {
                        MessageBox.Show("La Calidad Cafe que se a seleccionado en el formulario no es compatible, La calidad a trasladar es "+ almNCM.NombreCalidadCafe + " y el Almacen destino tiene la calidad actual " + almNCMDes.NombreCalidadCafe +" y a seleccionado la calidad "
                            + CalidadSeleccionada.NombreCalidadSeleccionada +".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verificar si el selectedValue coincide con algún id de subproducto en la lista
                    if (!verifyExistence)
                    {
                        MessageBox.Show("El subproducto del almacen ya contiene registros y es diferente al seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    /*
                    int isubProductoDesName = Convert.ToInt32(almNCMDes.IdSubProducto == null ? 0 : almNCMDes.IdSubProducto);
                    if (almNCM.IdSubProducto != isubProductoDesName && isubProductoDesName != 0)
                    {
                        MessageBox.Show("El SubProducto Cafe que desea trasladar al Almacen de destino no es compatible, el SubProducto a trasladar es "+ almNCM.NombreSubProducto + " y el Almacen destino tiene el SubProducto actual " + almNCMDes.NombreSubProducto +".", 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    if ((almNCM.IdSubProducto != selectedValue || isubProductoDesName != selectedValue) && isubProductoDesName != 0)
                    {
                        MessageBox.Show("El SubProducto Cafe que se a seleccionado en el formulario no es compatible, El SubProducto a trasladar es "+ almNCM.NombreSubProducto + " y el Almacen destino tiene el SubProducto actual " + almNCMDes.NombreSubProducto +" y a seleccionado el SubProducto "
                            + selectedValueName +".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }*/

                    var cantidadCafeC = new CantidadSiloPiñaController();

                    CantidadSiloPiña cantidadPr = new CantidadSiloPiña()
                    {
                        FechaMovimiento = fechaTraslado,
                        IdCosechaCantidad = CosechaActual.ICosechaActual,
                        CantidadCafe = pesoQQs,
                        CantidadCafeSaco = pesoSaco,
                        TipoMovimiento = "Traslado Cafe - Procedencia No.TrasladoCafe " + numTraslado,
                        IdSubProducto = selectedValue,
                        IdAlmacenSiloPiña = iAlmacenProce
                    };
                    
                    CantidadSiloPiña cantidadDes = new CantidadSiloPiña()
                    {
                        FechaMovimiento = fechaTraslado,
                        IdCosechaCantidad = CosechaActual.ICosechaActual,
                        CantidadCafe = pesoQQs,
                        CantidadCafeSaco = pesoSaco,
                        TipoMovimiento = "Traslado Cafe - Destino No.TrasladoCafe " + numTraslado,
                        IdSubProducto = selectedValue,
                        IdAlmacenSiloPiña = iAlmacenDest
                        //imaquinaria
                    };

                    bool exitoregistroCantidadPr = cantidadCafeC.InsertarCantidadCafeSiloPiña(cantidadPr);
                    bool exitoregistroCantidadDest = cantidadCafeC.InsertarCantidadCafeSiloPiña(cantidadDes);
                    if (!exitoregistroCantidadPr && exitoregistroCantidadDest)
                    {
                        MessageBox.Show("Error, Ocurrio un problema en la insercion de la cantidad de cafe verifique los campos QQs ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool exito = trasladoController.InsertarTrasladoCafe(traslado);

                    if (exito)
                    {
                        MessageBox.Show("Traslado de Cafe agregada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Procedencia
                        double resultCaPr = actcantidadPr - pesoQQs;
                        double resultCaSacoPr = actcantidadSacoPr - pesoSaco;
                        almacenC.ActualizarCantidadEntradaCafeAlmacen(iAlmacenProce, resultCaPr, resultCaSacoPr, CalidadSeleccionada.ICalidadSeleccionada);
                        
                        //Destino
                        double resultCaDes = pesoQQs + actcantidadDes;
                        double resultCaSacoDes = pesoSaco + actcantidadSacoDes;
                        almacenC.ActualizarCantidadEntradaCafeAlmacen(iAlmacenDest, resultCaDes, resultCaSacoDes, CalidadSeleccionada.ICalidadSeleccionada);

                        try
                        {
                            log.RegistrarLog(usuario.IdUsuario, "Registro dato Traslado de Cafe", ModuloActual.NombreModulo, "Insercion", "Inserto un nuevo Traslado de Cafe a la base de datos");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                        }

                        //borrar datos de los textbox
                        ClearDataTxb();
                        
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar el Traslado de Cafe. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al agregar el Traslado de Cafe. El numero de Traslado de Cafe ya existe en la cosecha actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                Traslado trasladoUpd = new Traslado()
                {
                    Idtraslado_cafe = iTraslado,
                    NumTraslado = numTraslado,
                    IdCosecha = CosechaActual.ICosechaActual,
                    FechaTrasladoCafe = fechaTraslado,

                    IdCalidadCafe = iCalidad,
                    IdSubProducto = selectedValue,
                    CantidadTrasladoSacos = pesoSaco,
                    CantidadTrasladoQQs = pesoQQs,

                    IdPersonal = iPesador,
                    ObservacionTraslado = observacion,

                    IdAlmacenProcedencia = iAlmacenProce,
                    IdBodegaProcedencia = iBodegaProce,
                    IdProcedencia = iProcedencia,

                    IdAlmacenDestino = iAlmacenDest,
                    IdBodegaDestino = iBodegaDest,
                    IdProcedenciaDestino = iProcedenciaDest
                };

                var cantidadCafeC = new CantidadSiloPiñaController();
                string searchPr = "Procedencia No.TrasladoCafe " + TrasladoSeleccionado.NumTraslado;
                string searchDes = "Destino No.TrasladoCafe " + TrasladoSeleccionado.NumTraslado;
                //Procedencia
                var cantUpdPr = cantidadCafeC.BuscarCantidadSiloPiñaSub(searchPr);
                AlmacenController almC = new AlmacenController();
                var almProCant = almC.ObtenerCantidadCafeAlmacen(AlmacenSeleccionado.IAlmacen);
                double cantActProc = almProCant.CantidadActualAlmacen;
                double cantActSacoProc = almProCant.CantidadActualSacoAlmacen;
                //Destino
                var cantUpdDes = cantidadCafeC.BuscarCantidadSiloPiñaSub(searchDes);
                var almDestCant = almC.ObtenerCantidadCafeAlmacen(AlmacenSeleccionado.IAlmacenDestino);
                double cantRestDest = almDestCant.CapacidadAlmacen - almDestCant.CantidadActualAlmacen;
                double cantRestSacoDest = almDestCant.CapacidadAlmacen - almDestCant.CantidadActualSacoAlmacen;

                if (historialSubProducProce.CantidadCafe < pesoQQs || cantRestDest < pesoQQs)
                {
                    MessageBox.Show("Error, la cantidad QQs de cafe que desea Sacar en Procedencia del almacen excede sus limite. Desea Sacar la cantidad de " + pesoQQs + " en el contenido disponible " + historialSubProducProce.CantidadCafe
                        + " O, la cantidad QQs de cafe que desea Agregar en Destino del almacen excede sus limite. Desea Agregar la cantidad de " + pesoQQs + " en el contenido disponible " + cantRestDes, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cantRestDest < pesoQQs || historialSubProducProce.CantidadCafe < 0)
                {
                    MessageBox.Show("Error, la cantidad QQs de cafe que desea Agrega en Procedencia del almacen excede sus limite maximos. Desea Agregar la cantidad de " + pesoQQs + " en el contenido disponible " + cantRestDes
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (historialSubProducProce.CantidadCafeSaco < pesoSaco || cantRestSacoDest < pesoSaco)
                {
                    MessageBox.Show("Error, la cantidad en Saco de cafe que desea Sacar en Procedencia del almacen excede sus limite. Desea Sacar la cantidad de " + pesoSaco + " en el contenido disponible " + historialSubProducProce.CantidadCafeSaco
                        + " O, la cantidad QQs de cafe que desea Agregar en Destino del almacen excede sus limite. Desea Agregar la cantidad de " + pesoQQs + " en el contenido disponible " + cantRestDes, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cantRestSacoDest < pesoSaco || historialSubProducProce.CantidadCafeSaco < 0)
                {
                    MessageBox.Show("Error, la cantidad en Saco de cafe que desea Agrega en Procedencia del almacen excede sus limite maximos. Desea Agregar la cantidad de " + pesoSaco + " en el contenido disponible " + cantRestSacoDes
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int icalidadDesName = Convert.ToInt32(almNCMDes.IdCalidadCafe == null ? 0 : almNCMDes.IdCalidadCafe);
                if (almNCM.IdCalidadCafe != icalidadDesName && icalidadDesName != 0)
                {
                    MessageBox.Show("La Calidad Cafe que desea trasladar al Almacen de destino no es compatible, La calidad a trasladar es " + almNCM.NombreCalidadCafe + " y el Almacen destino tiene la calidad actual " + almNCMDes.NombreCalidadCafe + ".",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((almNCM.IdCalidadCafe != CalidadSeleccionada.ICalidadSeleccionada || icalidadDesName != CalidadSeleccionada.ICalidadSeleccionada) && icalidadDesName != 0)
                {
                    MessageBox.Show("La Calidad Cafe que se a seleccionado en el formulario no es compatible, La calidad a trasladar es " + almNCM.NombreCalidadCafe + " y el Almacen destino tiene la calidad actual " + almNCMDes.NombreCalidadCafe + " y a seleccionado la calidad "
                        + CalidadSeleccionada.NombreCalidadSeleccionada + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar si el selectedValue coincide con algún id de subproducto en la lista
                if (!verifyExistence)
                {
                    MessageBox.Show("El subproducto del almacen ya contiene registros y es diferente al seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                /*// Verificar si el selectedValue coincide con algún id de subproducto en la lista
                if (!(listaSubproductosDest.Any() && listaSubproductosDest.First().IdSubProducto != selectedValue) &&
                        (!(almDestCant.IdCalidadCafe == null) || !(almDestCant.IdCalidadCafe == 0)))
                {
                    MessageBox.Show("El SubProducto Cafe que se ha seleccionado en el formulario no es el mismo que contiene el almacen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }*/

                /*int isubProductoDesName = Convert.ToInt32(almNCMDes.IdSubProducto == null ? 0 : almNCMDes.IdSubProducto);
                if (almNCM.IdSubProducto != isubProductoDesName && isubProductoDesName != 0)
                {
                    MessageBox.Show("El SubProducto Cafe que desea trasladar al Almacen de destino no es compatible, el SubProducto a trasladar es " + almNCM.NombreSubProducto + " y el Almacen destino tiene el SubProducto actual " + almNCMDes.NombreSubProducto + ".",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((almNCM.IdSubProducto != selectedValue || isubProductoDesName != selectedValue) && isubProductoDesName != 0)
                {
                    MessageBox.Show("El SubProducto Cafe que se a seleccionado en el formulario no es compatible, El SubProducto a trasladar es " + almNCM.NombreSubProducto + " y el Almacen destino tiene el SubProducto actual " + almNCMDes.NombreSubProducto + " y a seleccionado el SubProducto "
                        + selectedValueName + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                */

                bool exito = trasladoController.ActualizarTrasladoCafe(trasladoUpd);

                if (!exito)
                {
                    MessageBox.Show("Error al actualizar el Traslado de Cafe. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Procedencia
                if (cantUpdPr.IdAlmacenSiloPiña != iAlmacenProce )
                {
                    //actual almacen
                    //no actualiza los id, unicamnete la cantidad sumara ya que detecto que el almacen es diferente 
                    var cantidadActC = almacenC.ObtenerCantidadCafeAlmacen(cantUpdPr.IdAlmacenSiloPiña);
                    double cantidAct = cantidadActC.CantidadActualAlmacen;
                    double resultCaNoUpd = cantidAct + cantidaQQsActUpdate;
                    double cantidActSaco = cantidadActC.CantidadActualSacoAlmacen;
                    double resultCaNoUpdSaco = cantidActSaco + cantidaSacoActUpdate;

                    almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(cantUpdPr.IdAlmacenSiloPiña, resultCaNoUpd, resultCaNoUpdSaco, CalidadSeleccionada.ICalidadSeleccionada);

                    //nuevo almacen
                    //cambia los nuevos datos ya que detecto que el almacen cambio 
                    var cantidadNewC = almacenC.ObtenerCantidadCafeAlmacen((AlmacenSeleccionado.IAlmacen == 0) ? iAlmacenProce : AlmacenSeleccionado.IAlmacen);
                    double cantidNew = cantidadNewC.CantidadActualAlmacen;
                    double resultCaUpd = cantidNew - cantidaQQsActUpdate;
                    double cantidNewSaco = cantidadNewC.CantidadActualSacoAlmacen;
                    double resultCaUpdSaco = cantidNewSaco - cantidaSacoActUpdate;

                    almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen((AlmacenSeleccionado.IAlmacen == 0) ? iAlmacenProce : AlmacenSeleccionado.IAlmacen, resultCaUpd, resultCaUpdSaco, CalidadSeleccionada.ICalidadSeleccionada);

                    CantidadSiloPiña cantidadUpd = new CantidadSiloPiña()
                    {
                        IdCantidadCafe = cantUpdPr.IdCantidadCafe,
                        FechaMovimiento = fechaTraslado,
                        IdCosechaCantidad = CosechaActual.ICosechaActual,
                        CantidadCafe = cantidaQQsActUpdate,
                        CantidadCafeSaco = cantidaSacoActUpdate,
                        IdAlmacenSiloPiña = AlmacenSeleccionado.IAlmacen,
                        IdSubProducto = selectedValue,
                        TipoMovimiento = "Traslado Cafe - Procedencia No.TrasladoCafe " + numTraslado
                    };

                    bool exitoUpdateCantidad = cantidadCafeC.ActualizarCantidadCafeSiloPiña(cantidadUpd);
                    if (!exitoUpdateCantidad)
                    {
                        MessageBox.Show("Error, Ocurrio un problema en la actualizacion de la cantidad de cafe verifique los campos QQs ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                //Destino
                if( cantUpdDes.IdAlmacenSiloPiña != iAlmacenDest )
                {
                    //actual almacen
                    //no actualiza los id, unicamnete la cantidad sumara ya que detecto que el almacen es diferente 
                    var cantidadActC = almacenC.ObtenerCantidadCafeAlmacen(cantUpdDes.IdAlmacenSiloPiña);
                    double cantidAct = cantidadActC.CantidadActualAlmacen;
                    double resultCaNoUpd = cantidAct - cantidaQQsActUpdate;
                    double cantidActSaco = cantidadActC.CantidadActualSacoAlmacen;
                    double resultCaNoUpdSaco = cantidActSaco - cantidaSacoActUpdate;

                    almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(cantUpdDes.IdAlmacenSiloPiña, resultCaNoUpd, resultCaNoUpdSaco, CalidadSeleccionada.ICalidadSeleccionada);

                    //nuevo almacen
                    //cambia los nuevos datos ya que detecto que el almacen cambio 
                    var cantidadNewC = almacenC.ObtenerCantidadCafeAlmacen((AlmacenSeleccionado.IAlmacenDestino == 0) ? iAlmacenDest : AlmacenSeleccionado.IAlmacenDestino);
                    double cantidNew = cantidadNewC.CantidadActualAlmacen;
                    double resultCaUpd = cantidNew + cantidaQQsActUpdate;
                    double cantidNewSaco = cantidadNewC.CantidadActualSacoAlmacen;
                    double resultCaUpdSaco = cantidNewSaco + cantidaSacoActUpdate;

                    almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen((AlmacenSeleccionado.IAlmacenDestino == 0) ? iAlmacenDest : AlmacenSeleccionado.IAlmacenDestino, resultCaUpd, resultCaUpdSaco, CalidadSeleccionada.ICalidadSeleccionada);

                    CantidadSiloPiña cantidadUpd = new CantidadSiloPiña()
                    {
                        IdCantidadCafe = cantUpdDes.IdCantidadCafe,
                        FechaMovimiento = fechaTraslado,
                        IdCosechaCantidad = CosechaActual.ICosechaActual,
                        CantidadCafe = cantidaQQsActUpdate,
                        CantidadCafeSaco = cantidaSacoActUpdate,
                        IdAlmacenSiloPiña = AlmacenSeleccionado.IAlmacenDestino,
                        IdSubProducto = selectedValue,
                        TipoMovimiento = "Traslado Cafe - Destino No.TrasladoCafe " + numTraslado
                    };

                    bool exitoUpdateCantidad = cantidadCafeC.ActualizarCantidadCafeSiloPiña(cantidadUpd);
                    if (!exitoUpdateCantidad)
                    {
                        MessageBox.Show("Error, Ocurrio un problema en la actualizacion de la cantidad de cafe verifique los campos QQs ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    //Procedencia
                    double resultCaUpdPr = actcantidadPr + cantidaQQsUpdate - cantidaQQsActUpdate;
                    double resultCaUpdSacoPr = actcantidadSacoPr + cantidaSacoUpdate - cantidaSacoActUpdate;
                    almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(cantUpdPr.IdAlmacenSiloPiña, resultCaUpdPr, resultCaUpdSacoPr, CalidadSeleccionada.ICalidadSeleccionada);
                    
                    CantidadSiloPiña cantidadPr = new CantidadSiloPiña()
                    {
                        IdCantidadCafe = cantUpdPr.IdCantidadCafe,
                        FechaMovimiento = fechaTraslado,
                        IdCosechaCantidad = CosechaActual.ICosechaActual,
                        CantidadCafe = cantidaQQsActUpdate,
                        CantidadCafeSaco = cantidaSacoActUpdate,
                        IdAlmacenSiloPiña = cantUpdPr.IdAlmacenSiloPiña,
                        IdSubProducto = selectedValue,
                        TipoMovimiento = "Traslado Cafe - Procedencia No.TrasladoCafe " + numTraslado
                    };
                    
                    //Destino
                    double resultCaUpdDes = actcantidadDes + cantidaQQsActUpdate - cantidaQQsUpdate;
                    double resultCaUpdSacoDes = actcantidadSacoDes + cantidaSacoActUpdate - cantidaSacoUpdate;
                    almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(cantUpdDes.IdAlmacenSiloPiña, resultCaUpdDes, resultCaUpdSacoDes, CalidadSeleccionada.ICalidadSeleccionada);

                    CantidadSiloPiña cantidadDes = new CantidadSiloPiña()
                    {
                        IdCantidadCafe = cantUpdDes.IdCantidadCafe,
                        FechaMovimiento = fechaTraslado,
                        IdCosechaCantidad = CosechaActual.ICosechaActual,
                        CantidadCafe = cantidaQQsActUpdate,
                        CantidadCafeSaco = cantidaSacoActUpdate,
                        IdAlmacenSiloPiña = cantUpdDes.IdAlmacenSiloPiña,
                        IdSubProducto = selectedValue,
                        TipoMovimiento = "Traslado Cafe - Destino No.TrasladoCafe " + numTraslado
                    };

                    bool exitoactualizarCantidadPr = cantidadCafeC.ActualizarCantidadCafeSiloPiña(cantidadPr);
                    bool exitoactualizarCantidadDes = cantidadCafeC.ActualizarCantidadCafeSiloPiña(cantidadDes);
                    if (!exitoactualizarCantidadPr && !exitoactualizarCantidadDes)
                    {
                        MessageBox.Show("Error, Ocurrio un problema en la actualizacion de la cantidad de cafe verifique los campos QQs ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

                MessageBox.Show("Traslado de Cafe Actualizada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    //verificar el departamento
                    log.RegistrarLog(usuario.IdUsuario, "Actualizacion dato Traslado de Cafe", ModuloActual.NombreModulo, "Actualizacion", "Actualizo los datos del Traslado de Cafe con id ( " + traslado.Idtraslado_cafe + " ) en la base de datos");
                    TrasladoSeleccionado.clickImg = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                }

                //borrar datos de los textbox
                ClearDataTxb();
            }
        }

        private void btn_SaveTraslado_Click(object sender, EventArgs e)
        {
            SaveTraslado();
        }

        private void btn_deleteTraslado_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (TrasladoSeleccionado.NumTraslado != 0 || Convert.ToInt32(txb_numTraslado.Text) != 0 || !string.IsNullOrEmpty(txb_numTraslado.Text))
            {
                var Sl = new TrasladoController();
                bool verificexisten = Sl.VerificarExistenciaTraslado(CosechaActual.ICosechaActual, Convert.ToInt32(txb_numTraslado.Text));

                if (verificexisten)
                {
                    LogController log = new LogController();
                    UserController userControl = new UserController();

                    if (TrasladoSeleccionado.ITraslado == 0)
                    {
                        TrasladoSeleccionado.NumTraslado = Convert.ToInt32(txb_numTraslado.Text);
                        TrasladoSeleccionado.ITraslado = iTraslado;
                    }

                    Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro Traslado No: " + TrasladoSeleccionado.NumTraslado + ", de la cosecha: " + CosechaActual.NombreCosechaActual + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        //se llama la funcion delete del controlador para eliminar el registro
                        TrasladoController controller = new TrasladoController();
                        controller.EliminarTraslado(TrasladoSeleccionado.ITraslado);

                        var cantidadCafeC = new CantidadSiloPiñaController();
                        string searchPr = "Procedencia No.TrasladoCafe " + TrasladoSeleccionado.NumTraslado;
                        string searchDes = "Destino No.TrasladoCafe " + TrasladoSeleccionado.NumTraslado;

                        //Procedencia
                        var cantUpdPr = cantidadCafeC.BuscarCantidadSiloPiñaSub(searchPr);
                        //Destino
                        var cantUpdDes = cantidadCafeC.BuscarCantidadSiloPiñaSub(searchDes);

                        var almacenC = new AlmacenController();

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

                        //Almacen Procedencia
                        var almCMP = almacenC.ObtenerCantidadCafeAlmacen(iAlmacenProce);
                        double actcantidadPr = almCMP.CantidadActualAlmacen;
                        double actcantidadSacoPr = almCMP.CantidadActualSacoAlmacen;

                        double resultCaUpdPr = actcantidadPr + cantidaQQsActUpdate;
                        double resultCaUpdSacoPr = actcantidadSacoPr + cantidaSacoActUpdate;
                        almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(iAlmacenProce, resultCaUpdPr, resultCaUpdSacoPr, CalidadSeleccionada.ICalidadSeleccionada);

                        //Almacen Destino
                        var almCMD = almacenC.ObtenerCantidadCafeAlmacen(iAlmacenDest);
                        double actcantidadDes = almCMD.CantidadActualAlmacen;
                        double actcantidadSacoDes = almCMD.CantidadActualSacoAlmacen;

                        double resultCaUpdDes = actcantidadDes - cantidaQQsActUpdate;
                        double resultCaUpdSacoDes = actcantidadSacoDes - cantidaSacoActUpdate;
                        almacenC.ActualizarCantidadEntradaCafeUpdateSubPartidaAlmacen(iAlmacenDest, resultCaUpdDes, resultCaUpdSacoDes, CalidadSeleccionada.ICalidadSeleccionada);

                        cantidadCafeC.EliminarCantidadSiloPiña(cantUpdPr.IdCantidadCafe);
                        cantidadCafeC.EliminarCantidadSiloPiña(cantUpdDes.IdCantidadCafe);

                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato Traslado Cafe", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del Traslado No: " + TrasladoSeleccionado.NumTraslado + " del ID en la BD: " + TrasladoSeleccionado.ITraslado + " en la base de datos");

                        if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                        {
                            MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Traslado de Cafe Eliminada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        //se actualiza la tabla
                        ClearDataTxb();

                    }
                    else
                    {
                        ClearDataTxb();
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

        private void btn_pdfTraslado_Click(object sender, EventArgs e)
        {
            var Nombre_Usuario = userC.ObtenerUsuariosNombresID(UsuarioActual.IUsuario);
            if (TrasladoSeleccionado.ITraslado != 0)
            {

                string reportPath = "../../views/Reports/repor_traslados.rdlc";

                List<ReporteTraslado> data = reportesController.ObtenerTrasladosReports(TrasladoSeleccionado.ITraslado);
                foreach (ReporteTraslado reporte in data)
                {
                    reporte.nombre_persona = Nombre_Usuario.ApellidoPersonaUsuario;

                }
                ReportDataSource reportDataSource = new ReportDataSource("repor_traslados", data);

                form_opcReportExistencias reportTraslado = new form_opcReportExistencias(reportPath, reportDataSource);
                reportTraslado.ShowDialog();
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void txb_numTraslado_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 4;

            if (txb_numTraslado.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_pesoSaco_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 8;

            if (txb_pesoSaco.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_pesoQQs_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 8;

            if (txb_pesoQQs.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_observacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 225;

            if (txb_observacion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label1, label2,label3,label4, label5,label6,label7, label8,label9,label10,
                                label11,label12,label13,label14,label15};
            TextBox[] textBoxes = { txb_almacenDes, txb_almacenPr, txb_calidadCafe,txb_cosecha,txb_bodegaDes,txb_bodegaPr,txb_observacion,
                                    txb_personal,txb_pesoQQs,txb_pesoSaco,txb_fincaDes,txb_fincaPr,txb_numTraslado,txb_codCalidad,txb_codPesador};
            Button[] buttons = { btn_SaveTraslado, btn_Cancel };
            DateTimePicker[] dateTimePickers = { dtp_fechaTraslado };
            ComboBox[] comboBoxes = { cbx_subProducto };

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna al combox
            FontViews.ComboBoxStyle(comboBoxes);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleGC(buttons);
            //se asigna a fechas
            FontViews.DateStyle(dateTimePickers);
        }

        private void txb_numTraslado_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txb_numTraslado.Text))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true; // Evitar que se genere el "ding" de sonido de Windows

                    int numS = Convert.ToInt32(txb_numTraslado.Text);
                    ClearDataTxb();
                    txb_numTraslado.Text = Convert.ToString(numS);
                    var Tr = new TrasladoController();
                    bool verificexisten = Tr.VerificarExistenciaTraslado(CosechaActual.ICosechaActual, Convert.ToInt32(txb_numTraslado.Text));

                    if (verificexisten)
                    {
                        ShowTrasladoView(txb_numTraslado); // Llamar a la función de búsqueda que desees
                        TrasladoSeleccionado.clickImg = true;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Desea Agregar un nuevo Traslado", "¿Agregar Traslado?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            ClearDataTxb();
                            txb_numTraslado.Text = Convert.ToString(numS);
                        }
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
                            txb_calidadCafe.Text = calidad.NombreCalidad;
                            CalidadSeleccionada.ICalidadSeleccionada = calidad.IdCalidad;
                            iCalidad = CalidadSeleccionada.ICalidadSeleccionada;
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
                            txb_personal.Text = personal.NombrePersona;
                            PersonalSeleccionado.IPersonalPesador = personal.IdPersonal;
                            iPesador = PersonalSeleccionado.IPersonalPesador;
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
