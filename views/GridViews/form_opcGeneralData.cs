using sistema_modular_cafe_majada.controller;
using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using sistema_modular_cafe_majada.model.Mapping.Product;
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
    public partial class form_opcGeneralData : Form
    {
        // Agrega un campo privado para almacenar la referencia de form_main
        private form_panel_principal formularioPrincipal;

        public form_opcGeneralData(form_panel_principal principalForm)
        {
            InitializeComponent();

            formularioPrincipal = principalForm; // Almacena la referencia de form_main en el campo privado

            this.KeyPreview = true; // Habilita la captura de eventos de teclado para el formulario

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_multiple.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //
            SearchRegister(txb_buscarOpcGeneral);
            txb_buscarOpcGeneral.TextChanged += txb_buscarOpcGeneral_TextChanged;

            //esta es una llamada para funcion para pintar las filas del datagrid
            dtg_multiple.CellPainting += dtg_multiple_CellPainting;
        }

        //esta es una funcion para pintar las filas del datagrid
        private void dtg_multiple_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_multiple;

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            configDTG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            configDTG.BorderStyle = BorderStyle.None;
            configDTG.AllowUserToResizeColumns = false;
            configDTG.AllowUserToOrderColumns = false;
            configDTG.AllowUserToAddRows = false;
            configDTG.AllowUserToResizeRows = false;
            configDTG.MultiSelect = false;
            //configuracion de la fila de encabezado en el datagrid
            Font customFonten = new Font("Segoe UI", 10f, FontStyle.Bold);
            configDTG.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(184, 89, 89);
            configDTG.ColumnHeadersDefaultCellStyle.Font = customFonten;
            configDTG.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            configDTG.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(184, 89, 89);
            configDTG.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            configDTG.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //configuracion de las filas por defecto en el datagrid
            Font customFontdef = new Font("Segoe UI", 10f, FontStyle.Regular);

            configDTG.DefaultCellStyle.BackColor = Color.White;
            configDTG.DefaultCellStyle.Font = customFontdef;
            configDTG.DefaultCellStyle.ForeColor = Color.Black;
            configDTG.DefaultCellStyle.SelectionBackColor = Color.White;
            configDTG.DefaultCellStyle.SelectionForeColor = Color.Black;
            configDTG.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //configuracion de las filas que son seleccionadas
            configDTG.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 199, 199);
            configDTG.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }

        //
        public void ShowCalidadGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var calidadController = new CCafeController();
            List<CalidadCafe> datos = calidadController.ObtenerCalidades();

            var datosPersonalizados = datos.Select(calidad => new
            {
                ID = calidad.IdCalidad,
                Nombre = calidad.NombreCalidad,
                Descripcion = calidad.DescripcionCalidad
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_multiple.DataSource = datosPersonalizados;

            dtg_multiple.RowHeadersVisible = false;
            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void ShowSubProductoGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var subProductoController = new SubProductoController();
            List<SubProducto> datos = subProductoController.ObtenerNombreSubProductos();

            var datosPersonalizados = datos.Select(sub => new
            {
                ID = sub.IdSubProducto,
                Nombre = sub.NombreSubProducto,
                Descripcion = sub.DescripcionSubProducto,
                Calidad_Cafe = sub.NombreCalidadCafe
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_multiple.DataSource = datosPersonalizados;

            dtg_multiple.RowHeadersVisible = false;
            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void ShowCosechaGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var beneficioController = new BeneficioController();
            List<Beneficio> datos = beneficioController.ObtenerBeneficios();

            var datosPersonalizados = datos.Select(benef => new
            {
                ID = benef.IdBeneficio,
                Nombre = benef.NombreBeneficio,
                Ubicacion = benef.UbicacionBeneficio
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_multiple.DataSource = datosPersonalizados;

            dtg_multiple.RowHeadersVisible = false;
            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void ShowFincaGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var fincaController = new FincaController();
            List<Finca> datos = fincaController.ObtenerFincas();

            var datosPersonalizados = datos.Select(finca => new
            {
                ID = finca.IdFinca,
                Nombres = finca.nombreFinca,
                Ubicacion = finca.ubicacionFinca
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_multiple.DataSource = datosPersonalizados;

            dtg_multiple.RowHeadersVisible = false;
            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }
        
        //
        public void ShowTipoCafeGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var tipoCafeController = new TipoCafeController();
            List<TipoCafe> datos = tipoCafeController.ObtenerTipoCafes();

            var datosPersonalizados = datos.Select(tipoC => new
            {
                ID = tipoC.IdTipoCafe,
                Nombre = tipoC.NombreTipoCafe,
                Descripcion = tipoC.DescripcionTipoCafe
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_multiple.DataSource = datosPersonalizados;

            dtg_multiple.RowHeadersVisible = false;
            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void form_opcGeneralData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Cierra el formulario actual
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txb_buscarOpcGeneral_TextChanged(object sender, EventArgs e)
        {
            SearchRegister(txb_buscarOpcGeneral);
        }

        //
        public void SearchRegister(TextBox text)
        {
            int opc = formularioPrincipal.Itabla;

            switch (opc)
            {
                case 1:
                    //calidad
                    {
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowCalidadGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var calidadController = new CCafeController();
                            List<CalidadCafe> datos = calidadController.BuscarCalidades(text.Text);

                            var datosPersonalizados = datos.Select(calidad => new
                            {
                                ID = calidad.IdCalidad,
                                Nombre = calidad.NombreCalidad,
                                Descripcion = calidad.DescripcionCalidad
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_multiple.DataSource = datosPersonalizados;

                            dtg_multiple.RowHeadersVisible = false;
                            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }

                    break;
                case 2:
                    {
                        //tipo de cafe
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowSubProductoGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var subProductoController = new SubProductoController();
                            List<SubProducto> datos = subProductoController.ObtenerNombreSubProductos();

                            var datosPersonalizados = datos.Select(sub => new
                            {
                                ID = sub.IdSubProducto,
                                Nombre = sub.NombreSubProducto,
                                Descripcion = sub.DescripcionSubProducto,
                                Calidad_Cafe = sub.NombreCalidadCafe
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_multiple.DataSource = datosPersonalizados;

                            dtg_multiple.RowHeadersVisible = false;
                            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    break;
                case 3:
                    {
                        //tipo de cafe
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowTipoCafeGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var tipoCafeController = new TipoCafeController();
                            List<TipoCafe> datos = tipoCafeController.BuscadorTipoCafes(text.Text);

                            var datosPersonalizados = datos.Select(tipoC => new
                            {
                                ID = tipoC.IdTipoCafe,
                                Nombre = tipoC.NombreTipoCafe,
                                Descripcion = tipoC.DescripcionTipoCafe
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_multiple.DataSource = datosPersonalizados;

                            dtg_multiple.RowHeadersVisible = false;
                            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }

                    break;
                case 4:
                    {
                        //finca
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowFincaGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var fincaController = new FincaController();
                            List<Finca> datos = fincaController.BuscadorFinca(text.Text);

                            var datosPersonalizados = datos.Select(finca => new
                            {
                                ID = finca.IdFinca,
                                Nombres = finca.nombreFinca,
                                Ubicacion = finca.ubicacionFinca
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_multiple.DataSource = datosPersonalizados;

                            dtg_multiple.RowHeadersVisible = false;
                            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    break;
                case 5:
                    //beneficio
                    {
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowCosechaGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var beneficioController = new BeneficioController();
                            List<Beneficio> datos = beneficioController.BuscarBeneficio(text.Text);

                            var datosPersonalizados = datos.Select(benef => new
                            {
                                ID = benef.IdBeneficio,
                                Nombre = benef.NombreBeneficio,
                                Ubicacion = benef.UbicacionBeneficio
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_multiple.DataSource = datosPersonalizados;

                            dtg_multiple.RowHeadersVisible = false;
                            dtg_multiple.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Ocurrio un Error. La tabla que desea acceder no exite. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        //
        private void txb_buscarOpcGeneral_Enter(object sender, EventArgs e)
        {
            if (txb_buscarOpcGeneral.Text == "Buscar...")
            {
                txb_buscarOpcGeneral.Text = string.Empty;
                txb_buscarOpcGeneral.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txb_buscarOpcGeneral_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_buscarOpcGeneral.Text))
            {
                txb_buscarOpcGeneral.Text = "Buscar...";
                txb_buscarOpcGeneral.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
