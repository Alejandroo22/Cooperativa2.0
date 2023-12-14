using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using sistema_modular_cafe_majada.views;
using sistema_modular_cafe_majada.model.UserData;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.controller.UserDataController;
using System.Runtime.InteropServices;

namespace sistema_modular_cafe_majada
{
    public partial class form_main : Form
    {
        private string _nombreUsuario;
        private string _nombreCosecha;
        private Usuario usuario;
        private LogController log;


        //DICCIONARIO PARA ALMACENAR EL COLOR ORIGINAL DEL BOTON
        //private Dictionary<Button, Color> originalColors;

        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set
            {
                _nombreUsuario = value;
                lbl_User.Text = "Usuario: "+_nombreUsuario;
            }
        }

        public string NombreCosecha
        {
            get { return _nombreCosecha; }
            set
            {
                _nombreCosecha = value;
                lbl_numCosecha.Text = _nombreCosecha;
            }
        }

        public form_main()
        {
            InitializeComponent();

            //codigo para maximizar a pantalla completa solamente en area de trabajo
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            this.FormBorderStyle = FormBorderStyle.None;

            // Código 
            this.Shown += form_main_Shown;

            //
            RolUser();

            AsignarFuente();
            
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
                        btn_admin_panel.Visible = false;
                    }
                    break;
                case 3:
                    {
                        //Digitador
                        btn_admin_panel.Visible = false;
                        btn_reportes.Visible = false;
                    }
                    break;
                case 4:
                    {
                        //Invitado
                        btn_admin_panel.Visible = false;
                        btn_reportes.Visible = false;
                    }
                    break;
                default:
                    {
                        MessageBox.Show("Su rol actual no tiene autoridad para acceder a ciertas funciones en el sistema. Por favor, póngase en contacto con el administrador para obtener más información.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btn_admin_panel.Enabled = false;
                        btn_existenciasCafe.Enabled = false;
                        btn_reportes.Enabled = false;
                    }
                    break;

            }
        }

        private void form_main_Load(object sender, EventArgs e)
        {
            form_panel_principal pre = new form_panel_principal();
            AddFormulario(pre);

            //SE INICIALIZA EL DICCIONARIO DE COLORES
            //originalColors = new Dictionary<Button, Color>();

            //SE LLAMA LA FUNCION ASIGNAR COLOR ORIGINAL
            /*AsignarColorOriginal(btn_principal);
            AsignarColorOriginal(btn_existenciasCafe);
            AsignarColorOriginal(btn_reportes);
            AsignarColorOriginal(btn_admin_panel);*/

        }

        //FUNCION PARA IR AGREGANDO Y REMOVIENDO FORMULARIOS
        public void AddFormulario(Form fp)
        {
            /*if (this.panel_container.Controls.Count > 0)
            {
                this.panel_container.Controls.RemoveAt(0);
            }

            fp.TopLevel = false;
            this.panel_container.Controls.Add(fp);
            fp.Dock = DockStyle.Fill;
            fp.Show();*/

            if (this.panel_container.Controls.Count > 0)
            {
                this.panel_container.Controls.RemoveAt(0);
            }

            fp.TopLevel = false;
            this.panel_container.Controls.Add(fp);

            // Establecer el tamaño del formulario según las dimensiones del panel
            fp.Size = this.panel_container.Size;
            fp.Location = new Point(0, 0);

            // Si deseas que el formulario se ajuste automáticamente al cambio de tamaño del panel:
            this.panel_container.Resize += (sender, args) =>
            {
                fp.Size = this.panel_container.Size;
            };

            fp.Show();
        }

        //funcion para cerrar la aplicacion por completo
        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //funcion para maximizar a pantalla completa o minimizar a un tamaño minimo
        //private void btn_max_Click(object sender, EventArgs e)
        //{
        //    if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
        //    else this.WindowState = FormWindowState.Normal;
        //}

        private void btn_principal_Click(object sender, EventArgs e)
        {
            form_panel_principal pre = new form_panel_principal();
            AddFormulario(pre);
        }

        private void btn_activos_Click(object sender, EventArgs e)
        {
            form_coffeeStocks fact = new form_coffeeStocks();
            AddFormulario(fact);
        }

        private void btn_admin_panel_Click(object sender, EventArgs e)
        {
            form_administracion fadmin = new form_administracion();
            AddFormulario(fadmin);
        }

        private void btn_reportes_Click(object sender, EventArgs e)
        {

            form_reportes frepor = new form_reportes();
            AddFormulario(frepor);
        }

        private void btn_CloseSection_Click(object sender, EventArgs e)
        {
            log = new LogController();
            UserController usuarioControl = new UserController();
            var usuario = usuarioControl.ObtenerUsuario(UsuarioActual.NombreUsuario);

            DialogResult result = MessageBox.Show("¿Estás seguro de cerrar sesion?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //verificar el departamento
                log.RegistrarLog(usuario.IdUsuario, "Salio del Sistema", ModuloActual.NombreModulo, "Cierre de sesion", "El Usuario: " + _nombreUsuario + " salio del sistema");

                this.Close();

            }
            else
            {
                // El usuario seleccionó "No" o cerró el cuadro de diálogo
            }
        }

        private void form_main_Shown(object sender, EventArgs e)
        {
            string name = "Usuario: " + NombreUsuario;
            lbl_User.Text = name;
            
            string nameCosecha = NombreCosecha;
            lbl_numCosecha.Text = nameCosecha;
            
            string modulo = ModuloActual.NombreModulo;
            lbl_nameModule.Text = modulo;
        }

        private void lbl_username_Click(object sender, EventArgs e)
        {
            form_userData fUserData = new form_userData(this);
            AddFormulario(fUserData);
        }

        private void form_main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Left)
                {
                    // Evita que el formulario se redimensione hacia la izquierda
                    e.Handled = true;
                }
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Right)
                {
                    // Evita que el formulario se redimensione hacia la derecha
                    e.Handled = true;
                }
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Up)
                {
                    // Evita que el formulario se redimensione hacia arriba
                    e.Handled = true;
                }
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Down)
                {
                    // Evita que el formulario se redimensione hacia abajo
                    e.Handled = true;
                }
            }
        }

        private void lbl_numCosecha_Click(object sender, EventArgs e)
        {
            if (UsuarioActual.RolUsuario >= 5 || UsuarioActual.RolUsuario < 0)
            {
                return;
            }
            form_seleccionCosecha seleccionCosecha = new form_seleccionCosecha(this);
            seleccionCosecha.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
            lbl_numCosecha_Click(sender, e);
        }

        private void AsignarFuente()
        {
            Label[] encabezado = { label1, lbl_nameModule };
            Label[] info = { label2,lbl_numCosecha,lbl_User };
            Button[] buttons = { btn_admin_panel, btn_CloseSection,btn_existenciasCafe,btn_principal,btn_reportes };

            //se asigna a los label de encaebzado
            FontViews.LabelStyleEncabezado(encabezado);
            //se asigna al label de titulo de formulario
            FontViews.LabelStyleInfo(info);
            //se asigna a botones
            FontViews.ButtonStyleMain(buttons);
        }

        private void btn_min_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //FUNCIONES PARA CAMBIAR DE COLOR LOS BOTONES AL HACER CLICK EN ELLOS
        /*private void AsignarColorOriginal(Button button)
        {
            // Guardar el color original del botón en el diccionario
            //originalColors.Add(button, button.BackColor);

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
            button.BackColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            button.ForeColor = Color.Black;
        }

        private void Button_Leave(object sender, EventArgs e)
        {
            // Obtener el botón actual
            Button button = (Button)sender;

            // Restaurar el color original del botón al perder el foco
            button.BackColor = Color.FromArgb(114, 26, 26);
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
        }*/
    }
}
