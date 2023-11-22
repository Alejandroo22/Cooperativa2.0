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
    public partial class form_coffeeStocks : Form
    {
        //DICCIONARIO PARA ALMACENAR EL COLOR ORIGINAL DEL BOTON
        private Dictionary<Button, Color> originalColors;
        public form_coffeeStocks()
        {
            InitializeComponent();

            //SE INICIALIZA EL DICCIONARIO DE COLORES
            originalColors = new Dictionary<Button, Color>();

            AsignarFuente();

            //SE LLAMA LA FUNCION ASIGNAR COLOR ORIGINAL

            AsignarColorOriginal(btn_subPartida);
            AsignarColorOriginal(btn_trillaCafe);
            AsignarColorOriginal(btn_entradaCafe);
            AsignarColorOriginal(btn_salidaCafe);

            
        }

        //FUNCION PARA IR AGREGANDO Y REMOVIENDO FORMULARIOS
        public void AddFormulario(Form fp)
        {
            if (this.pnl_opcStock.Controls.Count > 0)
            {
                this.pnl_opcStock.Controls.RemoveAt(0);
            }


            fp.TopLevel = false;
            this.pnl_opcStock.Controls.Add(fp);
            fp.Dock = DockStyle.Fill;
            fp.Show();
        }

        private void form_coffeeStocks_Load(object sender, EventArgs e)
        {

        }

        private void btn_trillaCafe_Click(object sender, EventArgs e)
        {
            form_trillaCafe form_Trilla = new form_trillaCafe();
            AddFormulario(form_Trilla);
        }

        private void btn_subPartida_Click(object sender, EventArgs e)
        {
            form_subPartidas form_SubPartidas = new form_subPartidas();
            AddFormulario(form_SubPartidas);
        }

        private void btn_entradaCafe_Click(object sender, EventArgs e)
        {
            form_trasladoCafe form_Entrada = new form_trasladoCafe();
            AddFormulario(form_Entrada);
        }

        private void btn_salidaCafe_Click(object sender, EventArgs e)
        {
            form_salidasCafe form_Salidas = new form_salidasCafe();
            AddFormulario(form_Salidas);
        }

        //FUNCIONES PARA CAMBIAR DE COLOR LOS BOTONES AL HACER CLICK EN ELLOS
        private void AsignarColorOriginal(Button button)
        {
            // Guardar el color original del botón en el diccionario
            originalColors.Add(button, button.BackColor);

            // Suscribir el evento Click al botón
            button.Click += Button_Click;

            // Suscribir el evento LostFocus al botón
            button.LostFocus += Button_Leave;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Obtener el botón actual
            Button button = (Button)sender;

            // Cambiar el color del botón al ser seleccionado
            button.BackColor = Color.FromArgb(224, 238, 249);
            button.FlatAppearance.BorderColor = Color.FromArgb(26, 134, 216);
            button.FlatAppearance.BorderSize = 1;
        }

        private void Button_Leave(object sender, EventArgs e)
        {
            // Obtener el botón actual
            Button button = (Button)sender;

            // Restaurar el color original del botón al perder el foco
            button.BackColor = originalColors[button];
            button.ForeColor = Color.Black;
            button.FlatAppearance.BorderColor = Color.FromArgb(218, 218, 218);
            button.FlatAppearance.BorderSize = 1;
        }

        private void AsignarFuente()
        {
          
            Button[] buttons = { btn_entradaCafe, btn_salidaCafe,btn_subPartida,btn_trillaCafe };

            //se asigna a botones
            FontViews.ButtonStyle(buttons);
        }
    }
}
