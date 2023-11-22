using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Helpers;
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
    public partial class form_personas : Form
    {
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        //instancia de la clase mapeo persona para capturar los datos seleccionado por el usuario
        private Persona personaSeleccionada;

        public form_personas()
        {
            InitializeComponent();

            dtp_FechaNac.Format = DateTimePickerFormat.Short;

            //funcion que restringe el uso de caracteres en los textbox necesarios
            List<TextBox> textBoxListN = new List<TextBox> { txb_Tel2, txb_Nit, txb_Dui };
            List<TextBox> textBoxListC = new List<TextBox> { txb_Nombre, txb_Apellido };

            //funcion para restringir cual quier caracter y solo acepta unicamente num
            RestrictTextBoxNum(textBoxListN);
            RestrictTextBoxCharacter(textBoxListC);

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dataGrid_PersonView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //funcion para mostrar de inicio los datos en el dataGrid
            ShowPersonGrid();

            dataGrid_PersonView.CellPainting += dataGrid_PersonView_CellPainting;

            AsignarFuente();
        }

        // Función para verificar si un TextBox está vacío
        private bool IsTextBoxEmpty(TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text);
        }

        private void form_personas_Load(object sender, EventArgs e)
        {
            // Verificar si ya se agregaron las columnas al DataGridView
            if (dataGrid_PersonView.Columns.Count == 0)
            {
                // Crea una nueva columna para cada propiedad en la clase mapeada y asigna los nombres deseados
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "ID",
                    HeaderText = "ID Persona",
                    DisplayIndex = 0
                });
                
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Nombres",
                    HeaderText = "Nombres",
                    DisplayIndex = 1
                });
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Apellidos",
                    HeaderText = "Apellidos",
                    DisplayIndex = 2
                });
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Dirección",
                    HeaderText = "Dirección",
                    DisplayIndex = 3
                });
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Fecha_Nacimiento",
                    HeaderText = "Fecha de Nacimiento",
                    DisplayIndex = 4
                });
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "NIT",
                    HeaderText = "NIT",
                    DisplayIndex = 5
                });
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "DUI",
                    HeaderText = "DUI",
                    DisplayIndex = 6
                });
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Teléfono",
                    HeaderText = "Teléfono",
                    DisplayIndex = 7
                });
                dataGrid_PersonView.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Teléfono_Secundario",
                    HeaderText = "Teléfono Secundario",
                    DisplayIndex = 8
                });
            }
        }

        // Evento CellPainting para personalizar el encabezado del DataGridView
        private void dataGrid_PersonView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dataGrid_PersonView;

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

        private void SavePerson_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_Nombre.Text))
            {
                MessageBox.Show("El campo Nombre, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_Apellido.Text))
            {
                MessageBox.Show("El campo Apellido, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_Direccion.Text))
            {
                MessageBox.Show("El campo Direccion, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_Dui.Text))
            {
                MessageBox.Show("El campo DUI, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_Tel1.Text))
            {
                MessageBox.Show("El campo Telefono 1, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PersonController personaController = new PersonController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
            
            TextBox[] textBoxes = { txb_Nombre, txb_Apellido, txb_Direccion };
            ConvertFirstCharacter(textBoxes);

            // Obtener los valores ingresados por el usuario
            string namePerson = txb_Nombre.Text;
            string lastNamePerson = txb_Apellido.Text;
            string location = txb_Direccion.Text;
            string dui = txb_Dui.Text;
            string nit = txb_Nit.Text;
            string phone1 = txb_Tel1.Text;
            string phone2 = txb_Tel2.Text;
            DateTime fechaSeleccionada = dtp_FechaNac.Value;

            // Crear una instancia de la clase Persona con los valores obtenidos
            Persona persona = new Persona()
            {
                NombresPersona = namePerson,
                ApellidosPersona = lastNamePerson,
                DireccionPersona = location,
                DuiPersona = dui,
                NitPersona = nit,
                Telefono1Persona = phone1,
                Telefono2Persona = phone2,
                FechaNacimientoPersona = fechaSeleccionada
            };

            if (!imagenClickeada)
            {
                // Código que se ejecutará si no se ha hecho clic en la imagen update
                // Llamar al controlador para insertar la persona en la base de datos
                bool exito = personaController.InsertarPersona(persona);

                if (exito)
                {
                    MessageBox.Show("Persona agregada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                        //verificar el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Registro dato Persona", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva persona a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowPersonGrid();

                    //borrar datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al agregar la persona. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Código que se ejecutará si se ha hecho clic en la imagen update
                bool exito = personaController.ActualizarPersona(personaSeleccionada.IdPersona, namePerson, lastNamePerson, location, fechaSeleccionada, nit, dui, phone1, phone2);

                if (exito)
                {
                    MessageBox.Show("Persona actualizada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        //verificar el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion de dato Persona", ModuloActual.NombreModulo, "Actualizacion", "Actualizo los datos de la persona con ID " + personaSeleccionada.IdPersona + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowPersonGrid();

                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la persona. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                imagenClickeada = false;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            imagenClickeada = false;
            this.Close();
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_Nombre, txb_Apellido, txb_Direccion, txb_Dui, txb_Nit,
                                    txb_Tel1, txb_Tel2};

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }

            dtp_FechaNac.Value = DateTime.Now;
        }

        public void ShowPersonGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var personController = new PersonController();
            List<Persona> datos = personController.ObtenerPersonas();

            var datosPersonalizados = datos.Select(persona => new
            {
                ID = persona.IdPersona,
                Nombres = persona.NombresPersona,
                Apellidos = persona.ApellidosPersona,
                Dirección = persona.DireccionPersona,
                Fecha_Nacimiento = persona.FechaNacimientoPersona,
                NIT = persona.NitPersona,
                DUI = persona.DuiPersona,
                Teléfono = persona.Telefono1Persona,
                Teléfono_Secundario = persona.Telefono2Persona
            }).ToList();

            // Asignar los datos al DataGridView
            dataGrid_PersonView.DataSource = datosPersonalizados;

            dataGrid_PersonView.RowHeadersVisible = false;
            dataGrid_PersonView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

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

        private void txb_Tel1_Leave(object sender, EventArgs e)
        {
            int cantidaDigitos = txb_Tel1.Text.Length;
            if (cantidaDigitos == 8)
            {
                FormatPhoneNumber(txb_Tel1);
            }
        }

        private void FormatPhoneNumber(TextBox textBox)
        {
            string input = textBox.Text;
            string codigoPais = "(+503)";
            string prefijo = input.Substring(0, 4);
            string sufijo = input.Substring(4);
            bool isNumber = VerificNumber(input);

            if(isNumber)
            {
                // Verificar si hay suficientes dígitos para el número de teléfono
                if (input.Length == 8)
                {
                    // Formatear el número de teléfono completo
                    string numeroTelefono = $"{codigoPais} {prefijo} - {sufijo}";

                    // Establecer el texto formateado en el TextBox
                    textBox.Text = numeroTelefono;
                }
                else if(input.Length != 0 && input.Length <8)
                {
                    // Si no hay suficientes dígitos, establecer el texto ingresado sin formato y lanzar un mensaje
                    MessageBox.Show("Verifique el campo Telefono, no tiene la longitud recomendada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Text = input;
                }
                else
                {
                    // Si no hay suficientes dígitos, establecer el texto ingresado sin formato 
                    textBox.Text = input;
                }
            }
            else
            {
                // Formatear el número de teléfono completo
                string numeroTelefono = $"{prefijo} - {sufijo}";

                // Establecer el texto formateado en el TextBox
                textBox.Text = numeroTelefono;
            }
            
        }

        private void FormatDui(TextBox textBox)
        {
            string input = textBox.Text;

            // Verificar si hay suficientes dígitos para el número de teléfono
            if (input.Length >= 9)
            {
                string prefijo = input.Substring(0, 8);
                string sufijo = input.Substring(8);

                // Formatear el número de teléfono completo
                string numeroTelefono = $"{prefijo}-{sufijo}";

                // Establecer el texto formateado en el TextBox
                textBox.Text = numeroTelefono;
            }
            else
            {
                // Si no hay suficientes dígitos, establecer el texto ingresado sin formato
                textBox.Text = input;
            }
        }

        public void LimitDigits(TextBox textBox, int maxLength)
        {
            textBox.KeyPress += (sender, e) =>
            {
                // Verificar si el carácter ingresado es un dígito y si la longitud máxima se ha alcanzado
                if (char.IsDigit(e.KeyChar) && textBox.Text.Length >= maxLength)
                {
                    e.Handled = true; // Cancela el evento KeyPress para evitar que se ingrese el dígito
                }
            };
        }

        private bool VerificNumber(string text)
        {
            foreach (char caracter in text)
            {
                if (!char.IsDigit(caracter))
                {
                    return false;
                }
            }

            return true;
        }

        public void RestrictTextBoxNum(List<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.KeyPress += (sender, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true; // Cancela el evento KeyPress si no es un dígito o una tecla de control
                    }
                };
            }
        }

        public void RestrictTextBoxCharacter(List<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.TextChanged += (sender, e) =>
                {
                    TextBox currentTextBox = (TextBox)sender;
                    string text = currentTextBox.Text;

                    // Eliminar caracteres no alfabéticos, manteniendo espacios y tildes
                    string filteredText = new string(text.Where(c => char.IsLetter(c) || c == ' ' || c.ToString() == "á" || c.ToString() == "é" || c.ToString() == "í" || c.ToString() == "ó" || c.ToString() == "ú").ToArray());

                    // Actualizar el texto en el TextBox
                    currentTextBox.Text = filteredText;
                };
            }
        }

        private void txb_Tel2_Leave(object sender, EventArgs e)
        {
            int cantidaDigitos = txb_Tel2.Text.Length;
            if (cantidaDigitos == 8)
            {
                FormatPhoneNumber(txb_Tel2);
            }
        }

        private void txb_Dui_Leave(object sender, EventArgs e)
        {
            FormatDui(txb_Dui);
        }

        private void dataGrid_PersonView_SelectionChanged(object sender, EventArgs e)
        {
            /*
            //recorre todos los nombres de la columna del dataGrid y los muestra en consola
            foreach (DataGridViewColumn column in dataGrid_PersonView.Columns)
            {
                Console.WriteLine(column.Name);
            }*/
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (personaSeleccionada != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    txb_Nombre.Text = personaSeleccionada.NombresPersona;
                    txb_Apellido.Text = personaSeleccionada.ApellidosPersona;
                    txb_Direccion.Text = personaSeleccionada.DireccionPersona;
                    dtp_FechaNac.Value = personaSeleccionada.FechaNacimientoPersona;
                    txb_Dui.Text = personaSeleccionada.DuiPersona;
                    txb_Nit.Text = personaSeleccionada.NitPersona ?? ""; // Asignar cadena vacía si nit es nulo
                    txb_Tel1.Text = personaSeleccionada.Telefono1Persona;
                    txb_Tel2.Text = personaSeleccionada.Telefono2Persona ?? ""; // Asignar cadena vacía si tel2 es nulo

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
            if (personaSeleccionada != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro de la persona: " + personaSeleccionada.NombresPersona + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    PersonController controller = new PersonController();
                    controller.EliminarPersona(personaSeleccionada.IdPersona);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato Persona", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos de la persona " + personaSeleccionada.NombresPersona + " en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Persona Eliminada correctamente.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowPersonGrid();
                    personaSeleccionada = null;
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGrid_PersonView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dataGrid_PersonView.Rows.Count)
            {
                Console.WriteLine("depurador - evento click img update: " + imagenClickeada);
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dataGrid_PersonView.Rows[e.RowIndex];
                personaSeleccionada = new Persona();

                // Obtener los valores de las celdas de la fila seleccionada
                personaSeleccionada.IdPersona = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                personaSeleccionada.NombresPersona = filaSeleccionada.Cells["Nombres"].Value.ToString();
                personaSeleccionada.ApellidosPersona = filaSeleccionada.Cells["Apellidos"].Value.ToString();
                personaSeleccionada.DireccionPersona = filaSeleccionada.Cells["Dirección"].Value.ToString();
                personaSeleccionada.FechaNacimientoPersona = Convert.ToDateTime(filaSeleccionada.Cells["Fecha_Nacimiento"].Value);
                personaSeleccionada.DuiPersona = filaSeleccionada.Cells["DUI"].Value.ToString();
                personaSeleccionada.NitPersona = filaSeleccionada.Cells["NIT"].Value != null ? filaSeleccionada.Cells["NIT"].Value.ToString() : null;
                personaSeleccionada.Telefono1Persona = filaSeleccionada.Cells["Teléfono"].Value.ToString();
                personaSeleccionada.Telefono2Persona = filaSeleccionada.Cells["Teléfono_Secundario"].Value != null ? filaSeleccionada.Cells["Teléfono_Secundario"].Value.ToString() : null;

                Console.WriteLine("depuracion - capturar datos dobleClick campo; nombre persona: " + personaSeleccionada.NombresPersona);
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txb_Nit_Leave(object sender, EventArgs e)
        {
            FormatDui(txb_Nit);
        }

        private void txb_Tel1_Enter(object sender, EventArgs e)
        {
            VerificRestrict(txb_Tel1);
        }

        private void txb_Tel2_Enter(object sender, EventArgs e)
        {
            VerificRestrict(txb_Tel2);
        }

        public void VerificRestrict(TextBox txb)
        {
            int tamañoActual = txb.Text.Length;
            List<TextBox> txbL = new List<TextBox> { txb };
            bool isNumber = VerificNumber(txb.Text);

            if (isNumber)
            {
                Console.WriteLine("Es numero");
                if (tamañoActual == 0)
                {
                    RestrictTextBoxNum(txbL);
                    LimitDigits(txb, 8);
                }
            }
            else
            {
                Console.WriteLine("No es numero");
                LimitDigits(txb, 18);
            }
        }

        private void txb_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 24;

            if (txb_Nombre.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_Apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 24;

            if (txb_Apellido.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_Direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 72;

            if (txb_Direccion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_Dui_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 9;

            if (txb_Dui.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_Nit_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 9;

            if (txb_Nit.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {

            this.FormBorderStyle = FormBorderStyle.None;
            Label[] labels = { label2,label3,label4, label5,label6,label7, label8,label10,label11 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_Apellido, txb_Direccion, txb_Dui,txb_Nit,txb_Nombre,txb_Tel1,txb_Tel2};
            Button[] buttons = { btn_SavePerson, btn_Cancel };
            DateTimePicker[] dateTimePickers = { dtp_FechaNac };

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna al label de titulo de formulario
            FontViews.LabelStyleTitle(labeltitle);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleGC(buttons);
            //se asigna a botones
            FontViews.DateStyle(dateTimePickers);
        }
    }
}
