using sistema_modular_cafe_majada.controller;
using sistema_modular_cafe_majada.controller.HarvestController;
using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using sistema_modular_cafe_majada.model.Mapping.Product;
using sistema_modular_cafe_majada.model.UserData;
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
    public partial class form_opcLote : Form
    {
        public form_opcLote()
        {
            InitializeComponent();

            this.KeyPreview = true; // Habilita la captura de eventos de teclado para el formulario

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_tableOpc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //
            SearchRegister(txb_buscarOpc);
            txb_buscarOpc.TextChanged += txb_buscarOpc_TextChanged;
            
            //esta es una llamada para funcion para pintar las filas del datagrid
            dtg_tableOpc.CellPainting += dtg_tableOpc_CellPainting;
        }
         
        //esta es una funcion para pintar las filas del datagrid
        private void dtg_tableOpc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_tableOpc;

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
            dtg_tableOpc.DataSource = datosPersonalizados;

            dtg_tableOpc.RowHeadersVisible = false;
            dtg_tableOpc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void ShowPersonGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var personaController = new PersonController();
            List<Persona> datos = personaController.ObtenerPersonas();

            var datosPersonalizados = datos.Select(persona => new
            {
                ID = persona.IdPersona,
                Nombres = persona.NombresPersona,
                Apellidos = persona.ApellidosPersona,
                Dirección = persona.DireccionPersona,
                DUI = persona.DuiPersona,
                Teléfono = persona.Telefono1Persona,
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_tableOpc.DataSource = datosPersonalizados;

            dtg_tableOpc.RowHeadersVisible = false;
            dtg_tableOpc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //
        private void dtg_tableOpc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_tableOpc.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_tableOpc.Rows[e.RowIndex];

                int opc = TablaSeleccionada.ITable;

                switch (opc)
                {
                    case 1:
                        //finca
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            FincaSeleccionada.IFincaSeleccionada = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            FincaSeleccionada.NombreFincaSeleccionada = filaSeleccionada.Cells["Nombres"].Value.ToString();

                        }
                        break;
                    case 2:
                        //Persona
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            PersonSelect.IdPerson = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            PersonSelect.NamePerson = filaSeleccionada.Cells["Nombres"].Value.ToString();

                        }
                        break;
                    default:
                        MessageBox.Show("Ocurrio un Error. La tabla que desea acceder no exite. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje.
                MessageBox.Show("Seleccione una fila válida antes de hacer doble clic en el encabezado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void form_opcLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Cierra el formulario actual
            }
        }

        private void txb_buscarOpc_TextChanged(object sender, EventArgs e)
        {
            SearchRegister(txb_buscarOpc);
        }

        //
        public void SearchRegister(TextBox text)
        {
            int opc = TablaSeleccionada.ITable;

            switch (opc)
            {
                case 1:
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
                            dtg_tableOpc.DataSource = datosPersonalizados;

                            dtg_tableOpc.RowHeadersVisible = false;
                            dtg_tableOpc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                        
                    break;
                case 2:
                    {
                        //tipo de cafe
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowPersonGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var personController = new PersonController();
                            List<Persona> datos = personController.BuscarPersonas(text.Text);

                            var datosPersonalizados = datos.Select(persona => new
                            {
                                ID = persona.IdPersona,
                                Nombres = persona.NombresPersona,
                                Apellidos = persona.ApellidosPersona,
                                Dirección = persona.DireccionPersona,
                                DUI = persona.DuiPersona,
                                Teléfono = persona.Telefono1Persona,
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_tableOpc.DataSource = datosPersonalizados;

                            dtg_tableOpc.RowHeadersVisible = false;
                            dtg_tableOpc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Ocurrio un Error. La tabla que desea acceder no exite. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        //
        private void txb_buscarOpc_Enter(object sender, EventArgs e)
        {
            if (txb_buscarOpc.Text == "Buscar...")
            {
                txb_buscarOpc.Text = string.Empty;
                txb_buscarOpc.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txb_buscarOpc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_buscarOpc.Text))
            {
                txb_buscarOpc.Text = "Buscar...";
                txb_buscarOpc.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
