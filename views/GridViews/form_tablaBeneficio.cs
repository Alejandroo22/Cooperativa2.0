using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
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
    public partial class form_tablaBeneficio : Form
    {
        public form_tablaBeneficio()
        {
            InitializeComponent();

            this.KeyPreview = true; // Habilita la captura de eventos de teclado para el formulario

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_tOpc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //
            SearchRegister(txb_buscar);
            txb_buscar.TextChanged += txb_buscarOpc_TextChanged;

            //esta es una llamada para funcion para pintar las filas del datagrid
            dtg_tOpc.CellPainting += dtg_tableOpc_CellPainting;
        }

        //
        public void ShowBeneficioGrid()
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
            dtg_tOpc.DataSource = datosPersonalizados;

            dtg_tOpc.RowHeadersVisible = false;
            dtg_tOpc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //esta es una funcion para pintar las filas del datagrid
        private void dtg_tableOpc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_tOpc;

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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtg_tOpc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_tOpc.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_tOpc.Rows[e.RowIndex];

                int opc = TablaSeleccionadabodega.ITable;

                switch (opc)
                {
                    case 1:
                        //beneficio
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            BeneficioSeleccionado.IdBeneficioSleccionado = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            BeneficioSeleccionado.NombreBeneficioSeleccionado = filaSeleccionada.Cells["Nombre"].Value.ToString();

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
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida antes de hacer doble clic en el encabezado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txb_buscarOpc_TextChanged(object sender, EventArgs e)
        {
            SearchRegister(txb_buscar);
        }

        //
        public void SearchRegister(TextBox text)
        {
            int opc = TablaSeleccionadabodega.ITable;

            switch (opc)
            {
                case 1:
                    {
                        //Beneficio
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowBeneficioGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var benefController = new BeneficioController();
                            List<Beneficio> datos = benefController.BuscarBeneficio(text.Text);

                            var datosPersonalizados = datos.Select(benef => new
                            {
                                ID = benef.IdBeneficio,
                                Nombre = benef.NombreBeneficio,
                                Ubicacion = benef.UbicacionBeneficio
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_tOpc.DataSource = datosPersonalizados;

                            dtg_tOpc.RowHeadersVisible = false;
                            dtg_tOpc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            if (txb_buscar.Text == "Buscar...")
            {
                txb_buscar.Text = string.Empty;
                txb_buscar.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txb_buscarOpc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_buscar.Text))
            {
                txb_buscar.Text = "Buscar...";
                txb_buscar.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
