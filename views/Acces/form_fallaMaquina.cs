using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using sistema_modular_cafe_majada.controller.AccesController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.UserData;
using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.Mapping.Acces;
using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_fallaMaquina : Form
    {

        private Fallas fallaSeleccionada;
        private bool imagenClickeada = false;
        public form_fallaMaquina()
        {
            InitializeComponent();

            dtg_fallas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ShowFallasGrid();

            CbxMaquinas();
            cbx_fallaMaquina.DropDownStyle = ComboBoxStyle.DropDownList;

            AsignarFuente();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowFallasGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            FallaController fallaController = new FallaController();
            List<Fallas> datos = fallaController.ObtenerFallaNombreMaquinaria();

            var datosPersonalizados = datos.Select(falla => new
            {
                ID = falla.IdFalla,
                Descripcion = falla.DescripcionFalla,
                Pieza = falla.PiezaReemplazada,
                fecha = falla.FechaFalla,
                Acciones = falla.AccionesTomadas,
                Maquinas = falla.NombreMaquinaria,
                observaciones = falla.ObservacionFalla

            }).ToList();

            // Asignar los datos al DataGridView
            dtg_fallas.DataSource = datosPersonalizados;

            dtg_fallas.Columns["ID"].HeaderText = "Identificador de Falla";
            dtg_fallas.Columns["Descripcion"].HeaderText = "Descripcion de Falla";
            dtg_fallas.Columns["Pieza"].HeaderText = "Pieza a reemplazar";
            dtg_fallas.Columns["fecha"].HeaderText = "Fecha";
            dtg_fallas.Columns["Acciones"].HeaderText = "Acciones a tomar";
            dtg_fallas.Columns["Maquinas"].HeaderText = "Maquina";
            dtg_fallas.Columns["observaciones"].HeaderText = "Observaciones";

            dtg_fallas.RowHeadersVisible = false;
            dtg_fallas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void CbxMaquinas()
        {
            FallaController falla = new FallaController();
            //List<Maquinaria> maquinarias=new List<Maquinaria>();
            List<Maquinaria> datoMaquina = falla.ObtenerMaquinas();

            cbx_fallaMaquina.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (Maquinaria maquina in datoMaquina)
            {
                int idMaquina = maquina.IdMaquinaria;
                string nombreMaquina = maquina.NombreMaquinaria;

                cbx_fallaMaquina.Items.Add(new KeyValuePair<int, string>(idMaquina, nombreMaquina));
            }
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_accionesFalla,txb_desFalla,txb_obsFalla,txb_piezaFalla };

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }
        }

        //
        public void ConvertFirstLetter(TextBox[] textBoxes)
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

                    // Recorrer cada palabra y convertir el primer carácter a mayúscula solo si es la primera palabra
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(words[i]))
                        {
                            if (i == 0) // Verificar si es la primera palabra
                            {
                                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                            }
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

        private void dtg_fallas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_fallas.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_fallas.Rows[e.RowIndex];
                fallaSeleccionada = new Fallas();

                // Obtener los valores de las celdas de la fila seleccionada
                fallaSeleccionada.IdFalla = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                fallaSeleccionada.DescripcionFalla = filaSeleccionada.Cells["Descripcion"].Value.ToString();
                fallaSeleccionada.PiezaReemplazada = filaSeleccionada.Cells["Pieza"].Value.ToString();
                fallaSeleccionada.FechaFalla = Convert.ToDateTime(filaSeleccionada.Cells["fecha"].Value);
                fallaSeleccionada.AccionesTomadas = filaSeleccionada.Cells["Acciones"].Value.ToString();
                fallaSeleccionada.NombreMaquinaria = filaSeleccionada.Cells["Maquinas"].Value.ToString();
                fallaSeleccionada.ObservacionFalla = filaSeleccionada.Cells["observaciones"].Value.ToString();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_SaveFalla_Click(object sender, EventArgs e)
        {
            //se valida si el textbox esta vacio
            if (string.IsNullOrWhiteSpace(txb_desFalla.Text))
            {
                MessageBox.Show("El campo Descripcion, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(dtp_fechaFalla.Text))
            {
                MessageBox.Show("El campo Fecha, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_accionesFalla.Text))
            {
                MessageBox.Show("El campo Acciones, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Verificar si se ha seleccionado una maquinaria
            if (cbx_fallaMaquina.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una maquinaria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FallaController subController = new FallaController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario);

            TextBox[] textBoxes = { txb_desFalla,txb_accionesFalla,txb_obsFalla,txb_piezaFalla};
            ConvertFirstCharacter(textBoxes);

            //se obtiene los valores ingresados por el usuario
            string descripcion = txb_desFalla.Text;
            string pieza = txb_piezaFalla.Text;
            DateTime fecha = dtp_fechaFalla.Value;
            string acciones = txb_accionesFalla.Text;
            string observaciones = txb_obsFalla.Text;

            try
            {
                // Obtener el valor numérico seleccionado
                KeyValuePair<int, string> selectedStatus = new KeyValuePair<int, string>();
                if (cbx_fallaMaquina.SelectedItem is KeyValuePair<int, string> keyValue)
                {
                    selectedStatus = keyValue;
                }
                else if (cbx_fallaMaquina.SelectedItem != null)
                {
                    selectedStatus = (KeyValuePair<int, string>)cbx_fallaMaquina.SelectedItem;
                }

                int selectedValue = selectedStatus.Key;

                //verifica si han clikeado el icono update
                if (!imagenClickeada)
                {
                    // Código que se ejecutará si no se ha hecho clic en la imagen update

                    // Crear una instancia de la clase Usuario con los valores obtenidos
                    Fallas fallaInsert = new Fallas()
                    {
                        DescripcionFalla = descripcion,
                        PiezaReemplazada = pieza,
                        FechaFalla = fecha,
                        AccionesTomadas = acciones,
                        IdMaquinaria=selectedValue,
                        ObservacionFalla = observaciones
                    };

                    Console.WriteLine("Depurador - insercion: " + fallaInsert.DescripcionFalla);
                    // Llamar al controlador para insertar la persona en la base de datos
                    bool exito = subController.InsertarFalla(fallaInsert);

                    if (!exito)
                    {
                        MessageBox.Show("Error al insertar la Falla. Verifica los datos e intenta nuevamente.");
                        return;
                    }

                    MessageBox.Show("Falla agregada correctamente.");

                    try
                    {
                        //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Registro dato Falla", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva falla a la base de datos");

                        //funcion para actualizar los datos en el dataGrid
                        ShowFallasGrid();

                        //borrar datos de los textbox
                        ClearDataTxb();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener la falla: " + ex.Message);
                    }
                }
                else
                {
                    // Código que se ejecutará
                    // si se ha hecho clic en la imagen update
                    bool exito = subController.ActualizarFallas(fallaSeleccionada.IdFalla,descripcion,pieza,fecha,acciones, observaciones,selectedValue);

                    if (!exito)
                    {
                        MessageBox.Show("Error al actualizar la falla. Verifica los datos e intenta nuevamente.", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Falla actualizada correctamente.");

                    try
                    {
                        //verificar el departamento del log
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Falla", ModuloActual.NombreModulo, "Actualizacion", "Actualizo los datos de la falla con ID " + fallaSeleccionada.IdFalla + " en la base de datos");

                        //funcion para actualizar los datos en el dataGrid
                        ShowFallasGrid();

                        ClearDataTxb();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }
                    imagenClickeada = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error - Se detecto un error al guardar los datos. " + ex.Message);
                MessageBox.Show("Se detecto un error al guardar los datos, De tipo " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            imagenClickeada = false;
            fallaSeleccionada = null;
            this.Close();
        }

        private void btn_modFalla_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (fallaSeleccionada == null)
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var nombreMaquina = new MaquinariaController();
                var maquinaria = nombreMaquina.ObtenerNombreMaquinaria(fallaSeleccionada.NombreMaquinaria);
                // El usuario seleccionó "Sí"
                imagenClickeada = true;

                // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                txb_desFalla.Text = fallaSeleccionada.DescripcionFalla;
                txb_piezaFalla.Text = fallaSeleccionada.PiezaReemplazada;
                dtp_fechaFalla.Value = fallaSeleccionada.FechaFalla;
                txb_accionesFalla.Text = fallaSeleccionada.AccionesTomadas;
                txb_obsFalla.Text = fallaSeleccionada.ObservacionFalla;

                cbx_fallaMaquina.Items.Clear();
                CbxMaquinas();
                int imaquina = maquinaria.IdMaquinaria - 1;
                cbx_fallaMaquina.SelectedIndex = imaquina;

            }
        }

        private void dtg_fallas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_fallas;

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

        private void txb_desFalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 250;

            if (txb_desFalla.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_piezaFalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 125;

            if (txb_piezaFalla.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_accionesFalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 245;

            if (txb_accionesFalla.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_obsFalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 245;

            if (txb_obsFalla.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2, label3, label6,label5,label7,label4 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_accionesFalla, txb_desFalla, txb_obsFalla,txb_piezaFalla };
            Button[] buttons = { btn_SaveFalla, btn_Cancel };
            ComboBox[] comboBoxes = { cbx_fallaMaquina };
            DateTimePicker[] dateTimePickers = { dtp_fechaFalla};

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna al label de titulo de formulario
            FontViews.LabelStyleTitle(labeltitle);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleGC(buttons);
            //se asigna a Combobox
            FontViews.ComboBoxStyle(comboBoxes);
            //se asigna a fechas
            FontViews.DateStyle(dateTimePickers);
        }
    }
}
