using sistema_modular_cafe_majada.controller.HarvestController;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
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
    public partial class form_seleccionCosecha : Form
    {
        // Agrega un campo privado para almacenar la referencia de form_main
        private form_main formularioMain;

        public form_seleccionCosecha(form_main mainForm)
        {
            InitializeComponent();
            formularioMain = mainForm; // Almacena la referencia de form_main en el campo privado

            this.KeyPreview = true; // Habilita la captura de eventos de teclado para el formulario

            CbxCosecha();
            cbx_cosecha.DropDownStyle = ComboBoxStyle.DropDownList;

            AsignarFuente();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CbxCosecha()
        {
            CosechaController cosecha = new CosechaController();
            List<Cosecha> datoCosecha = cosecha.ObtenerCosechaDESC();

            cbx_cosecha.Items.Clear();

            // Asignar los valores al ComboBox
            foreach (Cosecha cosch in datoCosecha)
            {
                int iCosecha = cosch.IdCosecha;
                string nombreCosecha = cosch.NombreCosecha;

                // Agregar el objeto Cosecha directamente al ComboBox
                cbx_cosecha.Items.Add(new KeyValuePair<int, string>(iCosecha, nombreCosecha));

                // Asignar el DisplayMember y ValueMember para mostrar solo el nombre y mantener el ID asociado internamente
                /*cbx_cosecha.DisplayMember = "NombreCosecha";
                cbx_cosecha.ValueMember = "IdCosecha";*/
            }
            
            cbx_cosecha.SelectedIndex = 0;

        }

        private void btn_aplicar_Click(object sender, EventArgs e)
        {
            // Obtener el valor numérico seleccionado
            KeyValuePair<int, string> selectedStatus = new KeyValuePair<int, string>();
            if (cbx_cosecha.SelectedItem is KeyValuePair<int, string> keyValue)
            {
                selectedStatus = keyValue;
            }
            else if (cbx_cosecha.SelectedItem != null)
            {
                selectedStatus = (KeyValuePair<int, string>)cbx_cosecha.SelectedItem;
            }

            int selectedValue = selectedStatus.Key;
            string select = selectedStatus.Value;

            CosechaActual.ICosechaActual = selectedValue;
            CosechaActual.NombreCosechaActual = select;
            
            //se trae la variable instanciada del formulario main para actualizar el lbl del nombre usuario 
            formularioMain.NombreCosecha = CosechaActual.NombreCosechaActual;
            formularioMain.Refresh();

            this.Close();
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2, label1, };
            Button[] buttons = { btn_aplicar };
            ComboBox[] comboBoxes = { cbx_cosecha};

            //se asigna a los label de encaebzado
            FontViews.LabelStylePanelEncabezado(labels);
            //se asigna al combobox
            FontViews.ComboBoxStyle(comboBoxes);
            //se asigna a botones
            FontViews.ButtonStyleLogin(buttons);
        }

        private void form_seleccionCosecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Cierra el formulario actual
            }
        }
    }
}
