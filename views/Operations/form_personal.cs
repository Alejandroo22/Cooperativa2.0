using sistema_modular_cafe_majada.controller.AccesController;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Acces;
using sistema_modular_cafe_majada.model.Mapping.Operations;
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
    public partial class form_personal : Form
    {
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        //instancia de la clase mapeo personal para capturar los datos seleccionado por el usuario
        private Personal personalSeleccionado;

        public form_personal()
        {
            InitializeComponent();

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_personal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowPersonalGrid();
            CbxCargos();

            //restringir los txb para que no se puedan editar
            txb_namePersonal.ReadOnly = true;
            txb_namePersonal.Enabled = false;
            // Configura el ComboBox para que no permita la edición
            cbx_cargoPer.DropDownStyle = ComboBoxStyle.DropDownList;

            dtg_personal.CellPainting += dtgv_personal_CellPainting;

            AsignarFuente();
        }

        private void btn_tablePerson_Click(object sender, EventArgs e)
        {
            form_tableperson ftperson = new form_tableperson();
            if (ftperson.ShowDialog() == DialogResult.OK)
            {
                txb_namePersonal.Text = PersonSelect.NamePerson;
            }
        }

        //
        private void dtgv_personal_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_personal;

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
        public void ShowPersonalGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var personalController = new PersonalController();
            List<Personal> datos = personalController.ObtenerPersonalesNombreCargo();

            var datosPersonalizados = datos.Select(personal => new
            {
                ID = personal.IdPersonal,
                Nombre = personal.NombrePersonal,
                Cargo = personal. NombreCargo,
                Descripcion = personal.Descripcion,
                ID_Persona = personal.IdPersona,
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_personal.DataSource = datosPersonalizados;

            dtg_personal.RowHeadersVisible = false;
            dtg_personal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //
        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_namePersonal,txb_Descrip };

            foreach (TextBox textBox in txb)
            {
                textBox.Text = "";
            }

            cbx_cargoPer.Text = "";
        }

        //
        public void ConvertFirstCharacter(TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                string input = textBox.Text; // Obtener el valor ingresado por el usuario desde el TextBox

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

                    // Asignar el valor modificado de vuelta al TextBox
                    textBox.Text = result;
                }
            }
        }

        //
        private void dtg_personal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_personal.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_personal.Rows[e.RowIndex];
                personalSeleccionado = new Personal();
                var cargo = new ChargeController();

                // Obtener los valores de las celdas de la fila seleccionada
                personalSeleccionado.IdPersonal = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                personalSeleccionado.IdPersona = Convert.ToInt32(filaSeleccionada.Cells["ID_Persona"].Value);
                personalSeleccionado.NombrePersonal = filaSeleccionada.Cells["Nombre"].Value.ToString();
                personalSeleccionado.NombreCargo = filaSeleccionada.Cells["Cargo"].Value.ToString();
                personalSeleccionado.Descripcion = filaSeleccionada.Cells["Descripcion"].Value.ToString();

                Charge iCargo = cargo.ObtenerNombreCargo(personalSeleccionado.NombreCargo);
                personalSeleccionado.ICargo = iCargo.IdCargo;
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            this.Close();
        }

        //
        private void btn_deletePersonal_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (personalSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar los datos registrado del Personal: (" + personalSeleccionado.NombrePersonal + ") ?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    PersonalController controller = new PersonalController();
                    controller.EliminarPersonal(personalSeleccionado.IdPersonal);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de los datos Personal", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del Personal (" + personalSeleccionado.NombrePersonal + ") en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Personal Eliminado correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowPersonalGrid();
                    personalSeleccionado = null;
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas del Personal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_modPersonal_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (personalSeleccionado == null)
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                
            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // El usuario seleccionó "Sí"
                imagenClickeada = true;

                // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                txb_namePersonal.Text = personalSeleccionado.NombrePersonal;
                txb_Descrip.Text = personalSeleccionado.Descripcion;

                cbx_cargoPer.Items.Clear();
                CbxCargos();
                int icargo = personalSeleccionado.ICargo - 1;
                cbx_cargoPer.SelectedIndex = icargo;

            }
        }

        private void btn_SavePersonal_Click(object sender, EventArgs e)
        {
            PersonalController personalController = new PersonalController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario

            string name = txb_namePersonal.Text;
            string descrip = txb_Descrip.Text;

            try
            {
                // Verificar si se ha seleccionado la persona que se asignara al personal
                if (string.IsNullOrEmpty(txb_namePersonal.Text))
                {
                    MessageBox.Show("Debe seleccionar la persona resposable del campo Nombre para el personal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar si se ha seleccionado un cargo
                if (cbx_cargoPer.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un cargo para el personal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el valor numérico seleccionado
                KeyValuePair<int, string> selectedStatus = new KeyValuePair<int, string>();
                if (cbx_cargoPer.SelectedItem is KeyValuePair<int, string> keyValue)
                {
                    selectedStatus = keyValue;
                }
                else if (cbx_cargoPer.SelectedItem != null)
                {
                    selectedStatus = (KeyValuePair<int, string>)cbx_cargoPer.SelectedItem;
                }

                int selectedValue = selectedStatus.Key;

                //verifica si han clikeado el icono update
                if (!imagenClickeada)
                {
                    // Código que se ejecutará si no se ha hecho clic en la imagen update
                    
                    // Crear una instancia de la clase Usuario con los valores obtenidos
                    Personal personalInsert = new Personal()
                    {
                        NombrePersonal = name,
                        Descripcion = descrip,
                        ICargo = selectedValue,
                        IdPersona = PersonSelect.IdPerson
                    };

                    Console.WriteLine("Depurador - insercion: " + personalInsert.NombrePersonal + personalInsert.Descripcion + personalInsert.ICargo + PersonSelect.IdPerson);
                    // Llamar al controlador para insertar la persona en la base de datos
                    bool exito = personalController.InsertarPersonal(personalInsert);

                    if (!exito)
                    {
                        MessageBox.Show("Error al insertar el Personal. Verifica los datos e intenta nuevamente.");
                        return;
                    }

                    MessageBox.Show("Personal agregada correctamente.");

                    try
                    {
                        //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Registro dato Personal", ModuloActual.NombreModulo, "Insercion", "Inserto un nuevo personal a la base de datos");

                        //funcion para actualizar los datos en el dataGrid
                        ShowPersonalGrid();

                        //borrar datos de los textbox
                        ClearDataTxb();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el personal: " + ex.Message);
                    }
                }
                else
                {
                    // Código que se ejecutará
                    // si se ha hecho clic en la imagen update
                    bool exito = personalController.ActualizarPersonal(personalSeleccionado.IdPersonal, name,  selectedValue, descrip, PersonSelect.IdPerson);

                    if (!exito)
                    {
                        MessageBox.Show("Error al actualizar el personal. Verifica los datos e intenta nuevamente.", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Personal actualizada correctamente.");

                    try
                    {
                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Personal", ModuloActual.NombreModulo, "Actualizacion", "Actualizo los datos del Personal con ID " + personalSeleccionado.IdPersonal + " en la base de datos");

                        //funcion para actualizar los datos en el dataGrid
                        ShowPersonalGrid();

                        ClearDataTxb();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }
                    imagenClickeada = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - Se detecto un error al guardar los datos. " + ex.Message);
                MessageBox.Show("Se detecto un error al guardar los datos, De tipo " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //
        public void CbxCargos()
        {
            ChargeController cargo = new ChargeController();
            List<Charge> datoCargo = cargo.ObtenerCargos();

            cbx_cargoPer.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (Charge cargos in datoCargo)
            {
                int idRol = cargos.IdCargo;
                string nombreRol = cargos.NombreCargo;

                cbx_cargoPer.Items.Add(new KeyValuePair<int, string>(idRol, nombreRol));
            }
        }

        private void btn_cargosPersonal_Click(object sender, EventArgs e)
        {
            btn_cargosPersonal.FlatAppearance.BorderSize = 0;
            form_cargosPersonal form_Cargos = new form_cargosPersonal();
            form_Cargos.ShowDialog();
        }

        private void txb_Descrip_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 200;

            if (txb_Descrip.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_Descrip, txb_namePersonal };
            Button[] buttons = { btn_SavePersonal, btn_Cancel };
            ComboBox[] comboBoxes = { cbx_cargoPer };

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna al label de titulo de formulario
            FontViews.LabelStyleTitle(labeltitle);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleGC(buttons);
            //se asigna a combobox
            FontViews.ComboBoxStyle(comboBoxes);

        }
    }
}
