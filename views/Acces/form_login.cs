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
using sistema_modular_cafe_majada.model.UserData;
using sistema_modular_cafe_majada.controller;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.controller.AccesController;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.controller.HarvestController;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using sistema_modular_cafe_majada.views;

namespace sistema_modular_cafe_majada
{
    public partial class form_login : Form
    {
        public form_login()
        {
            InitializeComponent();

            txb_username.TextChanged += txb_username_TextChanged;
            cb_modulos.DropDownStyle = ComboBoxStyle.DropDownList;

            AsignarFuente();

        }

        //variable de tipo global para manejo de intentos fallidos en el login
        public static int contador = 0;

        //Codigo para mover el formulario en cualquier lugar de la pantalla

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwmd, int wmsg, int wparam, int lparam);

        //Evento para cuando el cursor del mouse este dentro del textbox
        private void txb_username_Enter(object sender, EventArgs e)
        {
            
            if (txb_username.Text == "Usuario")
            {
                txb_username.Text = "";
                txb_username.ForeColor = Color.LightGray;
            }
        }

        //Evento para cuando el curso del mouse sale del TextBox
        private void txb_username_Leave(object sender, EventArgs e)
        {
            CbxDepartamento();
            if (txb_username.Text == "")
            {
                LimpiarComboBox();
                txb_username.Text = "Usuario";
                txb_username.ForeColor = Color.DimGray;
            }
        }

        private void txb_password_Enter(object sender, EventArgs e)
        {
            if (txb_password.Text == "Contraseña")
            {
                txb_password.Text = "";
                txb_password.ForeColor = Color.LightGray;
                txb_password.UseSystemPasswordChar = true;
            }
        }

        private void txb_password_Leave(object sender, EventArgs e)
        {
            if (txb_password.Text == "")
            {
                txb_password.Text = "Contraseña";
                txb_password.ForeColor = Color.DimGray;
                txb_password.UseSystemPasswordChar = false;
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //funciones para mover el formulario a cualquier posicion en la pantalla
        private void form_login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            KeyValuePair<int, string>? departamentoSeleccionado = cb_modulos.SelectedItem as KeyValuePair<int, string>?;
            
            if (departamentoSeleccionado != null)
            {
                StartSeccionModuleCafe();
            }
            else
            {
                MessageBox.Show("La accion que esta intentando hacer no se encuentra en el sistema. Por favor seleccionar el Departamento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        //mostrar nuevamente los datos en el login
        private void Logout(object sender, FormClosedEventArgs e)
        {
            txb_username.Text = "Usuario";
            txb_username.ForeColor = Color.LightGray;
            txb_password.Text = "Contraseña";
            txb_password.ForeColor = Color.LightGray;
            txb_password.UseSystemPasswordChar = false;
            
            this.Show();
            ActiveControl = btn_login;
        }

        public void StartSeccionModuleCafe()
        {
            var cosechaC = new CosechaController();
            var userControl = new UserController();
            var log = new controller.SecurityData.LogController();

            string user = txb_username.Text;
            string password = txb_password.Text;
            
            KeyValuePair<int, string>? departamentoSeleccionado = cb_modulos.SelectedItem as KeyValuePair<int, string>?;
            string depto = departamentoSeleccionado.Value.Value;
            int idepto = departamentoSeleccionado.Value.Key;

            bool loginSuccessful = userControl.AutenticarUsuario(user, password);
            var usuario = userControl.ObtenerUsuario(user); // Asignar el resultado de ObtenerUsuario
            var cosecha = cosechaC.ObtenerCosechaUltima();

            contador++;
            if (loginSuccessful)
            {
                //se llama la funcion, la que verifica el estado del usuario
                bool exito = VerificStatusUserMessages();
                if (exito)
                {
                    bool verification = VerificUserModule(user, idepto);
                    if (verification)
                    {
                        ModuloActual.NombreModulo = depto;
                        try
                        {
                            //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                            log.RegistrarLog(usuario.IdUsuario, "Inicio sesion satisfactoriamente", ModuloActual.NombreModulo, "Inicio de Sesion", "Intentos realizados " + contador);

                            //funcion para abrir el formulario adecuado
                            // Inicio de sesión exitoso, realiza las acciones necesarias
                            // Navega a otra ventana, muestra un mensaje, etc.
                            // MessageBox.Show("Inicio de sesión exitoso");
                            UsuarioActual.NombreUsuario = user;
                            UsuarioActual.IUsuario = usuario.IdUsuario;
                            UsuarioActual.RolUsuario = usuario.IdRolUsuario;
                            CosechaActual.NombreCosechaActual = cosecha.NombreCosecha;
                            CosechaActual.ICosechaActual = cosecha.IdCosecha;

                            //administrador
                            form_main formPrin = new form_main();
                            formPrin.NombreUsuario = user;
                            formPrin.NombreCosecha = CosechaActual.NombreCosechaActual;

                            LimpiarComboBox();
                            formPrin.Show();

                            //buca en la vista main el evento close del form 
                            formPrin.FormClosed += Logout;
                            this.Hide();
                            
                            contador = 0;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                        }

                    }

                }

            }
            else
            {
                // Las credenciales son inválidas, muestra un mensaje de error
                MessageBox.Show("Credenciales incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CbxDepartamento()
        {
            ModuleController deptoControl = new ModuleController();
            List<Module> datoDepto = deptoControl.ObtenerModulos();

            cb_modulos.Items.Clear();
            cb_modulos.ForeColor = Color.WhiteSmoke; // Establece el color de la letra
            if (datoDepto.Count > 0)
            {
                // Asignar los valores numéricos a los elementos del ComboBox
                foreach (Module dept in datoDepto)
                {
                    int id = dept.IdModule;
                    string nombreDepto = dept.NombreModulo;
                    cb_modulos.Items.Add(new KeyValuePair<int, string>(id, nombreDepto));
                }
                cb_modulos.SelectedIndex = 0;
            }
            else
            {
                // No hay elementos en el departamento, puedes mostrar un mensaje o tomar una acción apropiada
            }
        }

        //verificar esta funcion para manejar varios modulos 
        private bool VerificUserModule(string nameUser, int depto)
        {
            bool autenticado;
            bool verific = true;         //aqui se manejara el contador para verificar cuantos intentos hace el usuario para acceder al sistema
            
            UserController login = new UserController();
            KeyValuePair<int, string>? departamentoSeleccionado = cb_modulos.SelectedItem as KeyValuePair<int, string>?;
            
            depto = departamentoSeleccionado.Value.Key;

            switch (depto)
            {
                case 1:
                    {
                        autenticado = login.VerificarUsuarioDepartamento(nameUser, depto);

                        if (!autenticado)
                        {
                            MessageBox.Show("El usuario no está autenticado en el modulo seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // Realizar acciones adicionales para usuarios no autenticados...
                            verific = false;
                        }

                    }
                    break;
                case 2:
                    {
                        autenticado = login.VerificarUsuarioDepartamento(nameUser, depto);
                        if (autenticado)
                        {
                            
                            MessageBox.Show("Este Modulo se Encuentra en Desarrollo", "Desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Realizar acciones adicionales para usuarios autenticados...
                            verific = false;
                        }
                        else
                        {
                            MessageBox.Show("El usuario no está autenticado en el modulo seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // Realizar acciones adicionales para usuarios no autenticados...
                            verific = false;
                        }

                    }
                    break;
                case 3:
                    {
                        autenticado = login.VerificarUsuarioDepartamento(nameUser, depto);
                        if (autenticado)
                        {

                            MessageBox.Show("Este Modulo se Encuentra en Desarrollo", "Desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Realizar acciones adicionales para usuarios autenticados...
                            verific = false;
                        }
                        else
                        {
                            MessageBox.Show("El usuario no está autenticado en el modulo seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // Realizar acciones adicionales para usuarios no autenticados...
                            verific = false;
                        }
                    }
                    break;
                case 4:
                    {
                        autenticado = login.VerificarUsuarioDepartamento(nameUser, depto);
                        if (autenticado)
                        {

                            MessageBox.Show("Este Modulo se Encuentra en Desarrollo", "Desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Realizar acciones adicionales para usuarios autenticados...
                            verific = false;
                        }
                        else
                        {
                            MessageBox.Show("El usuario no está autenticado en el modulo seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // Realizar acciones adicionales para usuarios no autenticados...
                            verific = false;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("El modulo seleccionado no se encuentra registrado en el sistema. Porfavor verifique el modulo al que pertenece.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    verific = false;
                    break;
            }
            return verific;
        }

        //funcion general para abrir formularios de cualquier tipo
        public void OpenFormName(Form nameForm)
        {
            // Inicio de sesión exitoso, realiza las acciones necesarias
            // Navega a otra ventana, muestra un mensaje, etc.
            // MessageBox.Show("Inicio de sesión exitoso");
            nameForm = new Form();
            UsuarioActual.NombreUsuario = txb_username.Text;

            LimpiarComboBox();
            nameForm.Show();

            //busca en la vista main el evento close del form 
            nameForm.FormClosed += Logout;
            this.Hide();
        }

        public bool VerificStatusUserMessages()
        {
            bool verific = false;
            UserController status = new UserController();
            var statusUser = status.ObtenerEstadoUsuario(txb_username.Text);
            string estado = statusUser.EstadoUsuario;

            switch (estado)
            {
                case "Activo":
                    {
                        verific = true;
                    }
                    break;
                case "Inactivo":
                    {
                        MessageBox.Show("Su cuenta está inactiva. Por favor, contacte al administrador del sistema para obtener más información.", "Mensaje de Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "Suspendido":
                    {
                        MessageBox.Show("Su cuenta está suspendida temporalmente. Por favor, comuníquese con el administrador del sistema para obtener más detalles.", "Mensaje de Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "Pendiente de activación":
                    {
                        MessageBox.Show("Su cuenta está en proceso de activación. Se le notificará cuando su cuenta esté lista para su uso.", "Mensaje de Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "Eliminado":
                    {
                        MessageBox.Show("Su cuenta ha sido eliminada. Si cree que ha sido un error, póngase en contacto con el soporte técnico para resolver este problema.", "Mensaje de Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default:
                    MessageBox.Show("El Estado de Usuario no se encuentra registrado en el sistema. Porfavor verifique el estado en el sistema.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

            return verific;
        }

        public void LimpiarComboBox()
        {
            cb_modulos.Items.Clear();
            cb_modulos.Text = ""; // Establece el texto del ComboBox en vacío
        }

        private void txb_username_TextChanged(object sender, EventArgs e)
        {
            //string nombreUsuario = txb_username.Text;
            CbxDepartamento();
        }

        private void txb_password_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                StartSeccionModuleCafe();
            }
        }

        private void cbx_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ComboBox comboBox = (ComboBox)sender;
                string itemText = comboBox.GetItemText(comboBox.Items[e.Index]);
                Brush itemBrush = (itemText == comboBox.Text) ? Brushes.WhiteSmoke : Brushes.White;

                // Dibujar fondo negro para el ComboBox
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds);

                // Dibujar el texto con color blanco
                e.Graphics.DrawString(itemText, e.Font, itemBrush, e.Bounds);
            }
        }

        private void cbx_DropDown(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.DrawItem += cbx_DrawItem;
        }

        private void txb_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartSeccionModuleCafe();
            }
        }

        private void cb_modulos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartSeccionModuleCafe();
            }
        }

        private void AsignarFuente()
        {
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_password, txb_username };
            Button[] buttons = { btn_login };
            ComboBox[] comboBoxes = { cb_modulos };

            //se asigna al label de titulo de formulario
            FontViews.LabelStyleTitle(labeltitle);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleLogin(buttons);
            //se asigna a combobox
            FontViews.ComboBoxStyle(comboBoxes);
        }

        private void form_login_Load(object sender, EventArgs e)
        {
            ActiveControl = btn_login;
        }
    }
}
