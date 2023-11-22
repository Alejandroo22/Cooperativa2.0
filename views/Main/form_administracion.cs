using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistema_modular_cafe_majada.views;

namespace sistema_modular_cafe_majada
{
    public partial class form_administracion : Form
    {
        //DICCIONARIO PARA ALMACENAR EL COLOR ORIGINAL DEL BOTON
        private Dictionary<Button, Color> originalColors;

        public form_administracion()
        {
            InitializeComponent();

            //SE INICIALIZA EL DICCIONARIO DE COLORES
            originalColors = new Dictionary<Button, Color>();

            //SE LLAMA LA FUNCION ASIGNAR COLOR ORIGINAL
            AsignarColorOriginal(btn_calidades_cafe);
            AsignarColorOriginal(btn_beneficios);
            AsignarColorOriginal(btn_clase_cafeuva);
            AsignarColorOriginal(btn_cosecha);
            AsignarColorOriginal(btn_dest_cafe);
            AsignarColorOriginal(btn_fincas);
            AsignarColorOriginal(btn_maquinas);
            AsignarColorOriginal(btn_persona);
            AsignarColorOriginal(btn_personal);
            AsignarColorOriginal(btn_proce_cafe);
            AsignarColorOriginal(btn_rol);
            AsignarColorOriginal(btn_socios);
            AsignarColorOriginal(btn_subprod_cafe);
            AsignarColorOriginal(btn_ubicacion);
            AsignarColorOriginal(btn_usuarios);

            FormConfig();
            AsignarFuente();
        }

        //FUNCION PARA IR AGREGANDO Y REMOVIENDO FORMULARIOS
        public void AddFormulario(Form fp)
        {
            if (this.panel_container_admin.Controls.Count > 0)
            {
                this.panel_container_admin.Controls.RemoveAt(0);
            }


            fp.TopLevel = false;
            this.panel_container_admin.Controls.Add(fp);
            fp.Dock = DockStyle.Fill;
            fp.Show();
        }

        private void btn_cosecha_Click(object sender, EventArgs e)
        {
            form_cosecha cos = new form_cosecha();
            AddFormulario(cos);
        }

        private void btn_rol_Click(object sender, EventArgs e)
        {
            form_rol frol = new form_rol();
            AddFormulario(frol);
        }

        private void btn_persona_Click(object sender, EventArgs e)
        {
            form_personas fper = new form_personas();
            AddFormulario(fper);
        }

        private void btn_usuarios_Click(object sender, EventArgs e)
        {
            form_usuarios fusers = new form_usuarios();
            AddFormulario(fusers);
        }

        private void btn_calidades_cafe_Click(object sender, EventArgs e)
        {
            form_calidades_cafe fcal_cafe = new form_calidades_cafe();
            AddFormulario(fcal_cafe);
        }

        private void btn_subprod_cafe_Click(object sender, EventArgs e)
        {
            form_subprod_cafe fsubprod_cafe = new form_subprod_cafe();
            AddFormulario(fsubprod_cafe);
        }

        private void btn_fincas_Click(object sender, EventArgs e)
        {
            form_finca fFinca = new form_finca();
            AddFormulario(fFinca);
        }

        private void btn_beneficios_Click(object sender, EventArgs e)
        {
            form_beneficios fBeneficio = new form_beneficios();
            AddFormulario(fBeneficio);
        }

        private void btn_lote_Click(object sender, EventArgs e)
        {
            form_socios fLotes = new form_socios();
            AddFormulario(fLotes);
        }
        
        private void btn_proce_cafe_Click(object sender, EventArgs e)
        {
            form_prodCafe fProceCafe = new form_prodCafe();
            AddFormulario(fProceCafe);
        }

        private void btn_dest_cafe_Click(object sender, EventArgs e)
        {
            form_destCafe fDestCafe = new form_destCafe();
            AddFormulario(fDestCafe);
        }

        private void btn_ubicacion_Click(object sender, EventArgs e)
        {
            form_ubicacion fDestino = new form_ubicacion();
             AddFormulario(fDestino);
        }

        private void btn_maquinas_Click(object sender, EventArgs e)
        {
            form_maquinas fMaquinas = new form_maquinas();
            AddFormulario(fMaquinas);
        }

        private void btn_clase_cafeuva_Click(object sender, EventArgs e)
        {
            form_claseUva fClaseUva = new form_claseUva();
            AddFormulario(fClaseUva);
        }

        private void btn_personal_Click(object sender, EventArgs e)
        {
            form_personal fPersonal = new form_personal();
            AddFormulario(fPersonal);
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
            button.FlatAppearance.BorderColor = Color.FromArgb(26,134,216);
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
            //Hacemos un array de botones para luego llamarlos en la funcion que se encuentra en la clase FontViews
            Button[] buttons = {btn_beneficios,btn_calidades_cafe,btn_clase_cafeuva,btn_cosecha,btn_dest_cafe,btn_fincas,
                                btn_maquinas,btn_persona,btn_personal,btn_proce_cafe,btn_rol,btn_socios,btn_subprod_cafe,
                                btn_ubicacion,btn_usuarios};

            FontViews.ButtonStyle(buttons);
        }

        private void FormConfig()
        {
            //al inciar el formulario estara sin bordes
            this.FormBorderStyle = FormBorderStyle.None;
        }
    }
}
