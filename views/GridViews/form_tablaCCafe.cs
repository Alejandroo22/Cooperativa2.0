using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.model.Mapping;
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
    public partial class form_tablaCCafe : Form
    {
        List<CalidadCafe> datos = new List<CalidadCafe>();

        public form_tablaCCafe()
        {
            InitializeComponent();
            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_tablaCCafe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //funcion para mostrar de inicio los datos en el dataGrid
            ShowDTGCCafeGrid(txb_buscarOpc);

            //esta es una llamada para funcion para pintar las filas del datagrid
            dtg_tablaCCafe.CellPainting += dtg_tableCCafe_CellPainting;
        }

        //
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //esta es una funcion para pintar las filas del datagrid
        private void dtg_tableCCafe_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_tablaCCafe;

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
        public void ShowDTGCCafeGrid(TextBox text)
        {
            var ccafeController = new CCafeController();

            if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
            {
                // Llamar al método para obtener los datos de la base de datos
                datos = ccafeController.ObtenerCalidades();
            }
            else
            {
                // Llamar al método para obtener los datos de la base de datos
                datos = ccafeController.BuscarCalidades(text.Text);

            }

            var datosPersonalizados = datos.Select(ccafe => new
            {
                ID = ccafe.IdCalidad,
                Nombre = ccafe.NombreCalidad,
                Descripcion = ccafe.DescripcionCalidad
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_tablaCCafe.DataSource = datosPersonalizados;

            dtg_tablaCCafe.RowHeadersVisible = false;
            dtg_tablaCCafe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void dtg_tablaCCafe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_tablaCCafe.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_tablaCCafe.Rows[e.RowIndex];

                // Obtener los valores de las celdas de la fila seleccionada
                CalidadSeleccionada.ICalidadSeleccionada = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                CalidadSeleccionada.NombreCalidadSeleccionada = filaSeleccionada.Cells["Nombre"].Value.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida antes de hacer doble clic en el encabezado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txb_buscarPer_Enter(object sender, EventArgs e)
        {
            if (txb_buscarOpc.Text == "Buscar...")
            {
                txb_buscarOpc.Text = "";
                txb_buscarOpc.ForeColor = Color.Black;
            }
        }

        private void txb_buscarPer_Leave(object sender, EventArgs e)
        {
            if (txb_buscarOpc.Text == "")
            {
                txb_buscarOpc.Text = "Buscar...";
                txb_buscarOpc.ForeColor = Color.DimGray;
            }
        }

        private void txb_buscarPer_TextChanged(object sender, EventArgs e)
        {
            ShowDTGCCafeGrid(txb_buscarOpc);
        }
    }
}
