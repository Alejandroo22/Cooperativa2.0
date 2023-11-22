using sistema_modular_cafe_majada.controller.AccesController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_usuarios : Form
    {
        //variable privada para esta clase para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        // Se Crea un diccionario para mapear nombres de módulos a sus IDs
        Dictionary<string, int> diccionarioModulos = new Dictionary<string, int>();

        //instancia de la clase mapeo persona para capturar los datos seleccionado por el usuario
        private Usuario usuarioSeleccionado;
        private int idPerson;
        private bool msjUpdatePass = true;
        public int contador = 0;

        public form_usuarios()
        {
            InitializeComponent();

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dataGrid_UserView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //funcion para mostrar de inicio los datos en el dataGrid
            ShowUserGrid();

            CbxRoles();

            dataGrid_UserView.CellPainting += dataGrid_UserView_CellPainting;
            StyleChekedListBox();

            txb_Name.ReadOnly = true;
            txb_Name.Enabled = false;
            cbx_role.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_userStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            AsignarFuente();

        }

        private void dataGrid_UserView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dataGrid_UserView;

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

        public void ShowUserGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            UserController userController = new UserController();
            List<Usuario> datos = userController.ObtenerTodosUsuariosNombresID();

            var datosPersonalizados = datos.Select(usuario => new
            {
                ID = usuario.IdUsuario,
                Usuario = usuario.NombreUsuario,
                Email = usuario.EmailUsuario,
                Clave_Usuario = usuario.ClaveUsuario,
                Estado = usuario.EstadoUsuario,
                Fecha_Creacion = usuario.FechaCreacionUsuario,
                Fecha_Baja = usuario.FechaBajaUsuario,
                Rol = usuario.NombreRol,
                Persona = usuario.NombrePersonaUsuario
            }).ToList();

            // Asignar los datos al DataGridView
            dataGrid_UserView.DataSource = datosPersonalizados;

            dataGrid_UserView.RowHeadersVisible = false;
            dataGrid_UserView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void dataGrid_UserView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dataGrid_UserView.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dataGrid_UserView.Rows[e.RowIndex];
                usuarioSeleccionado = new Usuario();

                // Obtener los valores de las celdas de la fila seleccionada
                usuarioSeleccionado.IdUsuario = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                usuarioSeleccionado.NombreUsuario = filaSeleccionada.Cells["Usuario"].Value.ToString();
                usuarioSeleccionado.EmailUsuario = filaSeleccionada.Cells["Email"].Value.ToString();
                usuarioSeleccionado.ClaveUsuario = filaSeleccionada.Cells["Clave_Usuario"].Value.ToString();
                usuarioSeleccionado.EstadoUsuario = filaSeleccionada.Cells["Estado"].Value.ToString();
                usuarioSeleccionado.FechaCreacionUsuario = Convert.ToDateTime(filaSeleccionada.Cells["Fecha_Creacion"].Value);
                // Obtener el valor de la celda
                object valorCelda = filaSeleccionada.Cells["Fecha_Baja"].Value;
                // Verificar si el valor de la celda es nulo y asignar el valor correspondiente a la propiedad
                usuarioSeleccionado.FechaBajaUsuario = valorCelda != null ? Convert.ToDateTime(valorCelda) : (DateTime?)null;
                usuarioSeleccionado.NombreRol = Convert.ToString(filaSeleccionada.Cells["Rol"].Value);
                usuarioSeleccionado.NombrePersonaUsuario = Convert.ToString(filaSeleccionada.Cells["Persona"].Value);
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (usuarioSeleccionado != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    PersonController personC = new PersonController();
                    RoleController rolC = new RoleController();
                    UserController userC = new UserController();
                    var name = personC.ObtenerPersona(usuarioSeleccionado.NombrePersonaUsuario);
                    idPerson = name.IdPersona;
                    
                    txb_Name.Text = usuarioSeleccionado.NombrePersonaUsuario;
                    txb_NameUser.Text = usuarioSeleccionado.NombreUsuario;
                    txb_Email.Text = usuarioSeleccionado.EmailUsuario;
                    txb_Password.Text = usuarioSeleccionado.ClaveUsuario;
                    txb_Password.PasswordChar = '\0';
                    txb_PassConfirm.Text = "";

                    var user = userC.ObtenerUsuario(usuarioSeleccionado.NombreUsuario);
                    int idUser = user.IdUsuario;

                    CargarModulosUsuarioCheckedListBox(idUser);

                    txb_Password.ReadOnly = true;
                    txb_PassConfirm.ReadOnly = true;

                    label10.Visible = true;
                    cbx_userStatus.Visible = true;

                    cbx_role.Items.Clear();
                    CbxRoles();

                    var rol = rolC.ObtenerRol(usuarioSeleccionado.NombreRol);
                    int irole = rol.IdRol - 1;
                    cbx_role.SelectedIndex = irole;
                    cbx_userStatus.Items.Clear();
                    cbx_userStatus.Items.Add("Activo");
                    cbx_userStatus.Items.Add("Inactivo");
                    cbx_userStatus.Items.Add("Suspendido");
                    cbx_userStatus.Items.Add("Pendiente de activación");
                    cbx_userStatus.SelectedItem = usuarioSeleccionado.EstadoUsuario;

                    DialogResult resultConfirme = MessageBox.Show("¿Deseas actualizar la contraseña del usuario al mismo tiempo?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultConfirme == DialogResult.Yes)
                    {
                        txb_Password.ReadOnly = true;
                        txb_PassConfirm.ReadOnly = false;
                        msjUpdatePass = false;
                    }

                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (usuarioSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                var statusUser = userControl.ObtenerEstadoUsuario(usuarioSeleccionado.NombreUsuario);

                //verifica si el usuario a eliminar ya fue eliminado anteriormente
                if (statusUser.EstadoUsuario == "Eliminado")
                {
                    MessageBox.Show("Lo sentimos, el usuario ya fue eliminado por lo caul no se puede eliminar nuevamente ya que este usuario está vinculado a otros elementos en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro del usuario: " + usuarioSeleccionado.NombreUsuario + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    UserController controller = new UserController();
                    controller.EliminarUsuario(usuarioSeleccionado.IdUsuario);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato Usuario", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del usuario " + usuarioSeleccionado.NombreUsuario + " en la base de datos");

                    MessageBox.Show("Usuario Eliminada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //se actualiza la tabla
                    ShowUserGrid();
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CbxRoles()
        {
            RoleController roles = new RoleController();
            List<Role> datoRol = roles.ObtenerRolCbx();
            
            cbx_role.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (Role role in datoRol)
            {
                int idRol = role.IdRol;
                string nombreRol = role.NombreRol;

                cbx_role.Items.Add(new KeyValuePair<int, string>(idRol, nombreRol));
            }
        }

        //Funcion para colocar mayusculas en la primera letra de la palabra y verificar que no vayan mayusculas intercaladas
        public void ConvertFirstCharacter(ComboBox[] comboBoxes)
        {
            foreach (ComboBox comboBox in comboBoxes)
            {
                string input = comboBox.Text; // Obtener el valor seleccionado por el usuario desde el ComboBox

                // Verificar si la cadena no está vacía
                if (!string.IsNullOrEmpty(input))
                {
                    // Convertir toda la cadena a minúsculas
                    string lowerCaseInput = input.ToLower();

                    // Dividir la cadena en palabras utilizando espacios como delimitadores
                    string[] words = lowerCaseInput.Split(' ');

                    // Recorrer cada palabra y convertir el primer carácter a mayúscula
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(words[i]))
                        {
                            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                        }
                    }

                    // Unir las palabras nuevamente en una sola cadena
                    string result = string.Join(" ", words);

                    // Asignar el valor modificado de vuelta al ComboBox
                    comboBox.Text = result;
                }
            }
        }

        private void btn_SaveUser_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerIUsuario(UsuarioActual.IUsuario); // Asignar el resultado de ObtenerUsuario

            string nameUser = txb_NameUser.Text;
            string pass = txb_Password.Text;
            string passConfirm = txb_PassConfirm.Text;
            string email = txb_Email.Text;

            // Las contraseñas se cifra
            string encryptedPassword = PasswordManager.EncryptPassword(pass);

            try
            {
                // Verificar si se ha seleccionado la persona qeu se asignara al usuario
                if (string.IsNullOrEmpty(txb_Name.Text))
                {
                    MessageBox.Show("Debe seleccionar la persona resposable del campo Nombre para el usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txb_NameUser.Text))
                {
                    MessageBox.Show("El campo Nombre de Usuario, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Verificar si se ha seleccionado un rol de usuario
                if (cbx_role.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un rol para el usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txb_Password.Text))
                {
                    MessageBox.Show("El campo Contraseña, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Obtener el valor numérico seleccionado
                KeyValuePair<int, string> selectedStatus = new KeyValuePair<int, string>();
                if (cbx_role.SelectedItem is KeyValuePair<int, string> keyValue)
                {
                    selectedStatus = keyValue;
                }
                else if (cbx_role.SelectedItem != null)
                {
                    selectedStatus = (KeyValuePair<int, string>)cbx_role.SelectedItem;
                }

                int selectedValue = selectedStatus.Key;

                // Código que se ejecutará si no se ha hecho clic en la imagen update
                // Verificar si el texto cumple con el formato de un correo electrónico válido
                bool esValido = ValidarFormatoCorreoElectronico(email);
                if (!esValido)
                {
                    // Mostrar un mensaje de error al usuario
                    MessageBox.Show("El correo electrónico ingresado no es válido. Verifique el formato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (clb_module.CheckedItems.Count == 0)
                {
                    // No se ha seleccionado ningún elemento en el CheckedListBox
                    MessageBox.Show("Debe seleccionar el Modulo al que pertenecera el usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //verifica si han clikeado el icono update
                if (!imagenClickeada)
                { 
                    string estado;

                    //verifica si la contraseña coinciden
                    if (pass != passConfirm)
                    {
                        // Las contraseñas no coinciden, mostrar un mensaje de error
                        MessageBox.Show("Las contraseñas no coinciden. Por favor, verifique he inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DialogResult result = MessageBox.Show("¿Desea dejar al usuario en estado pendiente de activación? Esto permitirá una revisión adicional antes de otorgarle acceso al sistema.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        estado = "Pendiente de Activacion";
                    }
                    else
                    {
                        estado = "Activo";
                    }

                    // Crear una instancia de la clase Usuario con los valores obtenidos
                    Usuario usuarioInsert = new Usuario()
                    {
                        NombreUsuario = nameUser,
                        EmailUsuario = email,
                        ClaveUsuario = encryptedPassword,
                        EstadoUsuario = estado,
                        IdRolUsuario = selectedValue,
                        IdPersonaUsuario = PersonSelect.IdPerson
                    };

                    // Llamar al controlador para insertar la persona en la base de datos
                    bool exito = userController.InsertarUsuario(usuarioInsert);

                    if (!exito)
                    {
                        MessageBox.Show("Error al insertar el usuario. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Usuario agregada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //
                    try
                    {
                        var userM = userController.ObtenerUsuario(nameUser);
                        int idUserM = userM.IdUsuario;

                        List<int> modulosSeleccionados = new List<int>();
                        foreach (object item in clb_module.CheckedItems)
                        {
                            string nombreModulo = item.ToString();

                            // Obtener el ID del módulo utilizando el diccionario
                            if (diccionarioModulos.ContainsKey(nombreModulo))
                            {
                                int idModulo = diccionarioModulos[nombreModulo];
                                modulosSeleccionados.Add(idModulo);
                            }
                        }

                        var moduleControl = new ModuleController();
                        moduleControl.InsertarUsuarioModulos(idUserM, modulosSeleccionados);

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                        return;
                    }

                    try
                    {
                        //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Registro dato Usuario", ModuloActual.NombreModulo, "Insercion", "Inserto un nuevo usuario a la base de datos");
                        
                        //funcion para actualizar los datos en el dataGrid
                        ShowUserGrid();

                        //borrar datos de los textbox
                        ClearDataTxb();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }
 
                }
                else
                {
                    // Código que se ejecutará
                    // si se ha hecho clic en la imagen update
                    bool baja = false;
                    object selectedItem = cbx_userStatus.SelectedItem; // Obtiene el objeto seleccionado
                    bool verificEncrypt = PasswordManager.VerifyPassword(passConfirm, txb_Password.Text);

                    if (selectedItem == null)
                    {
                        MessageBox.Show("Asigne el dato de estado de usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string valorSeleccionado = selectedItem.ToString(); // Convierte el objeto a string si es necesario
                    usuario.EstadoUsuario = valorSeleccionado;

                    if (selectedItem.Equals("Inactivo") || selectedItem.Equals("Suspendido") || selectedItem.Equals("Eliminado"))
                    {
                        baja = true;
                    }
                    
                    DateTime? fechaBaja = null;
                    
                    if (baja)
                    {
                        fechaBaja = DateTime.Today;
                    }

                    if (!msjUpdatePass)
                    {
                        if (!verificEncrypt)
                        {
                            // Las contraseñas no coinciden, mostrar un mensaje de error
                            MessageBox.Show("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            msjUpdatePass = true;
                            return;
                        }

                    }

                    bool exito = userController.ActualizarUsuario(usuarioSeleccionado.IdUsuario, nameUser, email, pass, fechaBaja, usuario.EstadoUsuario, selectedValue, idPerson);

                    if (!exito)
                    {
                        MessageBox.Show("Error al actualizar la persona. Verifica los datos e intenta nuevamente.", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Usuario actualizada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //
                    try
                    {
                        // Obtiene el usuario según el nombre de usuario
                        var usuarioController = new UserController();
                        Usuario userM = usuarioController.ObtenerUsuario(nameUser);
                        int idUserM = userM.IdUsuario;

                        // Intera sobre los elementos del control de lista de verificación
                        List<int> nuevosModulosSeleccionados = new List<int>();
                        foreach (object item in clb_module.CheckedItems)
                        {
                            string nombreModulo = item.ToString();

                            // Obtener el ID del módulo utilizando el diccionario
                            if (diccionarioModulos.ContainsKey(nombreModulo))
                            {
                                int idModulo = diccionarioModulos[nombreModulo];
                                nuevosModulosSeleccionados.Add(idModulo);
                            }
                        }

                        // Accede al controlador y ejecutar las acciones necesarias
                        var moduleController = new ModuleController();
                        List<int> modulosActuales = moduleController.ObtenerModulosActualesDelUsuario(idUserM);
                        
                        List<int> modulosAgregar = nuevosModulosSeleccionados.Except(modulosActuales).ToList();
                        List<int> modulosEliminar = modulosActuales.Except(nuevosModulosSeleccionados).ToList();

                        moduleController.InsertarUsuarioModulos(idUserM, modulosAgregar);
                        moduleController.EliminarModulosDelUsuario(idUserM, modulosEliminar);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al actualizar los modulos del usuario: " + ex.Message);
                    }

                    try
                    {
                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Usuario", ModuloActual.NombreModulo, "Actualizacion", "Actualizo los datos del usuario con ID " + usuarioSeleccionado.IdUsuario + " en la base de datos");

                        //funcion para actualizar los datos en el dataGrid
                        ShowUserGrid();

                        label10.Visible = false;
                        cbx_userStatus.Visible = false;
                        ClearDataTxb();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    txb_Password.ReadOnly = false;
                    txb_PassConfirm.ReadOnly = false;
                    imagenClickeada = false;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - Se detecto un error al guardar los datos. " + ex.Message);
                MessageBox.Show("Se detecto un error al guardar los datos, De tipo "+ ex.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ClearDataTxb()
        {

            PersonSelect.NamePerson = "";
            List<TextBox> txb = new List<TextBox> { txb_Name, txb_NameUser, txb_Password, txb_PassConfirm, txb_Email };

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }

            // Limpiar el CheckedListBox
            clb_module.Items.Clear();
            cbx_role.Items.Clear();
            cbx_userStatus.Items.Clear();
            cbx_role.Text = "";
            cbx_userStatus.Text = "";
        }

        private void btn_table_person_Click(object sender, EventArgs e)
        {
            form_tableperson ftperson = new form_tableperson();
            if (ftperson.ShowDialog() == DialogResult.OK)
            {
                txb_Name.Text = PersonSelect.NamePerson;
            }
        }

        private void CargarModulosCheckedListBox()
        {
            var moduleControl = new ModuleController();
            // Obtener los módulos del controlador
            List<Module> modulos = moduleControl.ObtenerModulos();

            // Limpiar el CheckedListBox
            diccionarioModulos.Clear(); // Limpiar el diccionario
            clb_module.Items.Clear();

            // Agregar los módulos al CheckedListBox
            foreach (Module modulo in modulos)
            {
                int idModulo = modulo.IdModule;
                string nombreModulo = modulo.NombreModulo;

                // Agregar el nombre del módulo y su ID al diccionario
                diccionarioModulos.Add(nombreModulo, idModulo);

                // Agregar el nombre del módulo al CheckedListBox
                clb_module.Items.Add(nombreModulo);
            }
        }

        //funcion para mostrar los modulos y al mismo tiempo seleccionar aquellos modulos ya asignados al usuario
        private void CargarModulosUsuarioCheckedListBox(int idUsuario)
        {
            var moduleControl = new ModuleController();
            // Obtener los módulos del controlador
            List<Module> modulos = moduleControl.ObtenerModulos();

            // Limpiar el CheckedListBox y el diccionario del modulo
            diccionarioModulos.Clear(); // Limpiar el diccionario
            clb_module.Items.Clear();

            // Obtener los módulos seleccionados del usuario
            List<Module> modulosSeleccionados = moduleControl.ObtenerModulosDeUsuario(idUsuario);

            // Agregar los módulos al CheckedListBox y seleccionar los módulos correspondientes
            foreach (Module modulo in modulos)
            {
                string nombreModule = modulo.NombreModulo;

                // Agregar el nombre del módulo y su ID al diccionario
                diccionarioModulos.Add(nombreModule, modulo.IdModule);

                // Verificar si el nombre del módulo está seleccionado para el usuario
                if (modulosSeleccionados.Any(m => m.NombreModulo == modulo.NombreModulo))
                {
                    clb_module.Items.Add(nombreModule, true);
                }
                else
                {
                    clb_module.Items.Add(nombreModule, false); // No marcar el módulo
                }
            }

        }

        //funcion para darle estilos a los chekedlistBox
        public void StyleChekedListBox()
        {
            clb_module.ItemHeight = 35; // Cambiar la altura de cada elemento
            clb_module.Padding = new Padding(10, 5, 10, 5); // Cambiar el relleno interno de cada elemento
            clb_module.Margin = new Padding(8); // Cambiar el margen externo del CheckedListBox
            clb_module.BorderStyle = BorderStyle.Fixed3D; // Establecer un borde sólido
            clb_module.Font = new Font("Oswald", 9, FontStyle.Regular); // Establecer la fuente y tamaño del texto
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            label10.Visible = false;
            cbx_userStatus.Visible = false;
            usuarioSeleccionado = null;
            txb_Password.ReadOnly = false;
            txb_PassConfirm.ReadOnly = false;
            CbxRoles();
            this.Close();
        }

        private void form_usuarios_Load(object sender, EventArgs e)
        {
            label10.Visible = false;
            cbx_userStatus.Visible = false;
        }

        public string FormatearCorreoElectronico(string correo)
        {
            // Verificar si el texto cumple con el formato de un correo electrónico válido
            bool esValido = ValidarFormatoCorreoElectronico(correo);

            if (!esValido)
            {
                // Mostrar un mensaje de error al usuario
                MessageBox.Show("El correo electrónico ingresado no es válido. Verifique el formato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Puedes lanzar una excepción si deseas manejar el error en otro lugar
                // throw new Exception("El correo electrónico ingresado no es válido. Verifique el formato.");
            }

            return correo;
        }

        public bool ValidarFormatoCorreoElectronico(string correo)
        {
            // Expresión regular para verificar el formato del correo electrónico
            string patron = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(patron);

            return regex.IsMatch(correo);
        }

        private void txb_NameUser_Enter(object sender, EventArgs e)
        {
            CbxRoles();
            if (imagenClickeada)
            {
                UserController userC = new UserController();
                var user = userC.ObtenerUsuario(usuarioSeleccionado.NombreUsuario);
                int idUser = user.IdUsuario;
                CargarModulosUsuarioCheckedListBox(idUser);
                return;
            }
            CargarModulosCheckedListBox();
        }

        private void txb_Password_Leave(object sender, EventArgs e)
        {
            if (!imagenClickeada)
            {
                txb_Password.PasswordChar = '*';
            }
            else
            {
                txb_Password.PasswordChar = '\0';
            }
        }

        private void txb_Password_Enter(object sender, EventArgs e)
        {
            if (!imagenClickeada)
            {
                txb_Password.PasswordChar = '*';
            }
            else
            {
                txb_Password.PasswordChar = '\0';
            }
        }

        private void txb_NameUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 24;

            if (txb_NameUser.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 72;

            if (txb_Email.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 64;

            if (txb_Password.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_PassConfirm_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 64;

            if (txb_PassConfirm.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5,label6,label7, label8,label9,label10 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_Email, txb_Name, txb_NameUser,txb_PassConfirm,txb_Password };
            Button[] buttons = { btn_SaveUser, btn_Cancel };
            ComboBox[] comboBoxes = { cbx_role, cbx_userStatus };

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna al label de titulo de formulario
            FontViews.LabelStyleTitle(labeltitle);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleGC(buttons);
            //se asogna a combox
            FontViews.ComboBoxStyle(comboBoxes);
        }
    }
}
