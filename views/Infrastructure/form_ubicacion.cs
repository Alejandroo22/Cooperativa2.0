using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using sistema_modular_cafe_majada.model.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_ubicacion : Form
    {
        private Almacen almacenSeleccionado;
        private List<TextBox> txbRestrict;
        private int ibodega;
        private bool imagenClickeada = false;

        public form_ubicacion()
        {
            InitializeComponent();

            txbRestrict = new List<TextBox> { txb_capacidad };

            RestrictTextBoxNum(txbRestrict);

            txb_id.ReadOnly = true;
            txb_id.Enabled = false;
            cbx_bodega.DropDownStyle = ComboBoxStyle.DropDownList;

            //coloca nueva mente el contador en el txb del cdigo
            AlmacenController ben = new AlmacenController();
            var count = ben.CountAlmacen();
            txb_id.Text = Convert.ToString(count.CountAlmacen + 1);

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_ubicacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //funcion para mostrar de inicio los datos en el dataGrid
            ShowAlmacenGrid();

            //
            CbxBodega();

            dtg_ubicacion.CellPainting += dataGrid_Almacen_CellPainting;

            AsignarFuente();

        }

        private void dataGrid_Almacen_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_ubicacion;

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

        public void ShowAlmacenGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            AlmacenController almacenController = new AlmacenController();
            List<Almacen> datos = almacenController.ObtenerAlmacenNombreBodega();

            var datosPersonalizados = datos.Select(almacen => new
            {
                ID = almacen.IdAlmacen,
                Nombre = almacen.NombreAlmacen,
                Descripcion = almacen.DescripcionAlmacen,
                Capacidad = almacen.CapacidadAlmacen,
                Cantidad_Saco = almacen.CantidadActualSacoAlmacen,
                Cantidad_QQs = almacen.CantidadActualAlmacen,
                Calidad_Cafe = almacen.NombreCalidadCafe,
                Ubicacion = almacen.UbicacionAlmacen,
                Bodega = almacen.NombreBodegaUbicacion
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_ubicacion.DataSource = datosPersonalizados;

            dtg_ubicacion.RowHeadersVisible = false;
            dtg_ubicacion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void dtg_Almacen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_ubicacion.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_ubicacion.Rows[e.RowIndex];
                almacenSeleccionado = new Almacen();

                // Obtener los valores de las celdas de la fila seleccionada
                almacenSeleccionado.IdAlmacen = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                almacenSeleccionado.NombreAlmacen = filaSeleccionada.Cells["Nombre"].Value.ToString();
                almacenSeleccionado.DescripcionAlmacen = filaSeleccionada.Cells["Descripcion"].Value.ToString();
                almacenSeleccionado.CapacidadAlmacen = Convert.ToDouble(filaSeleccionada.Cells["Capacidad"].Value);
                almacenSeleccionado.UbicacionAlmacen = filaSeleccionada.Cells["Ubicacion"].Value.ToString();
                almacenSeleccionado.NombreBodegaUbicacion = filaSeleccionada.Cells["Bodega"].Value.ToString();
                BodegaSeleccionada.NombreBodega = almacenSeleccionado.NombreBodegaUbicacion;
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CbxBodega()
        {
            BodegaController bodega = new BodegaController();
            List<Bodega> datoBodega = bodega.ObtenerBodegas();

            cbx_bodega.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (Bodega bodeg in datoBodega)
            {
                int idBodega = bodeg.IdBodega;
                string nombreBodega = bodeg.NombreBodega;

                cbx_bodega.Items.Add(new KeyValuePair<int, string>(idBodega, nombreBodega));
            }
        }

        private void btn_deleteDestino_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (almacenSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();

                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro del Almacen: " + almacenSeleccionado.NombreAlmacen + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    AlmacenController controller = new AlmacenController();
                    controller.EliminarAlmacens(almacenSeleccionado.IdAlmacen);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato Almacen", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del Almacen " + almacenSeleccionado.NombreAlmacen + " en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Almacen Eliminada correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowAlmacenGrid();
                    ClearDataTxb();
                    cbx_bodega.SelectedIndex = -1;
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            this.Close();
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_nombreAlmacen, txb_descripcion, txb_ubicacion, txb_capacidad };

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }

            cbx_bodega.Items.Clear();
            cbx_bodega.SelectedIndex = -1;
            imagenClickeada = false;
            almacenSeleccionado = null;

            CbxBodega();
            //coloca nueva mente el contador en el txb del cdigo
            AlmacenController ben = new AlmacenController();
            var count = ben.CountAlmacen();
            txb_id.Text = Convert.ToString(count.CountAlmacen + 1);
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

        public void ConvertAllUppercase(TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                string input = textBox.Text; // Obtener el valor ingresado por el usuario desde el TextBox

                // Verificar si la cadena no está vacía
                if (!string.IsNullOrEmpty(input))
                {
                    // Convertir toda la cadena a mayúsculas
                    string upperCaseInput = input.ToUpper();

                    // Asignar el valor modificado de vuelta al TextBox
                    textBox.Text = upperCaseInput;
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

        private void btn_modDestino_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (almacenSeleccionado != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    BodegaController bodegaC = new BodegaController();
                    var name = bodegaC.ObtenerNombreBodega(BodegaSeleccionada.NombreBodega);
                    ibodega = name.IdBodega;

                    txb_id.Text = Convert.ToString(almacenSeleccionado.IdAlmacen);
                    txb_nombreAlmacen.Text = almacenSeleccionado.NombreAlmacen;
                    txb_descripcion.Text = almacenSeleccionado.DescripcionAlmacen;
                    txb_ubicacion.Text = almacenSeleccionado.UbicacionAlmacen;
                    txb_capacidad.Text = almacenSeleccionado.CapacidadAlmacen.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

                    //cbx
                    cbx_bodega.Items.Clear();
                    CbxBodega();
                    
                    int ibg = name.IdBodega - 1;
                    cbx_bodega.SelectedIndex = ibg;
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RestrictTextBoxNum(List<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.KeyPress += (sender, e) =>
                {
                    char decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != decimalSeparator && e.KeyChar != '.')
                    {
                        e.Handled = true; // Cancela el evento KeyPress si no es un dígito, el punto o la coma
                    }

                    // Permite solo un punto o una coma en el TextBox
                    if ((e.KeyChar == decimalSeparator || e.KeyChar == '.') && (textBox.Text.Contains(decimalSeparator.ToString()) || textBox.Text.Contains('.')))
                    {
                        e.Handled = true; // Cancela el evento KeyPress si ya hay un punto o una coma en el TextBox
                    }
                };
            }
        }

        private void btn_SaveDestino_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_nombreAlmacen.Text))
            {
                MessageBox.Show("El campo Nombre Almacen, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txb_ubicacion.Text))
            {
                DialogResult result = MessageBox.Show("El campo Ubicacion, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
                txb_ubicacion.Text = ".";
            }

            if (string.IsNullOrWhiteSpace(txb_capacidad.Text))
            {
                MessageBox.Show("El campo Capacidad, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txb_descripcion.Text))
            {
                DialogResult result = MessageBox.Show("¿Desea dejar el campo descripcion vacio? Llenar dicho campo permitirá dar una informacion extra a futuros usuarios", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // Verificar si se ha seleccionado una Bodega
            if (cbx_bodega.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una Bodega para el Almacen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AlmacenController subController = new AlmacenController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario);

            TextBox[] textBoxes = { txb_ubicacion };
            TextBox[] textBoxesM = { txb_nombreAlmacen };
            TextBox[] textBoxesLetter = { txb_descripcion };
            ConvertFirstCharacter(textBoxes);
            ConvertAllUppercase(textBoxesM);
            ConvertFirstLetter(textBoxesLetter);

            // Obtener el valor numérico seleccionado
            KeyValuePair<int, string> selectedStatus = new KeyValuePair<int, string>();
            if (cbx_bodega.SelectedItem is KeyValuePair<int, string> keyValue)
            {
                selectedStatus = keyValue;
            }
            else if (cbx_bodega.SelectedItem != null)
            {
                selectedStatus = (KeyValuePair<int, string>)cbx_bodega.SelectedItem;
            }

            int selectedValue = selectedStatus.Key;
            string selectedValueName = selectedStatus.Value;

            //se obtiene los valores ingresados por el usuario
            string nameAlmacen = txb_nombreAlmacen.Text;
            string ubicacion = txb_ubicacion.Text;
            string description = txb_descripcion.Text;

            double capacidad;
            if (double.TryParse(txb_capacidad.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out capacidad)){}
            else
            {
                MessageBox.Show("El valor ingresado en el campo Capacidad no es un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lastId = subController.ObtenerUltimoId();
            bool existe = subController.ExisteId(Convert.ToInt32(txb_id.Text));
            if (lastId.LastId == Convert.ToInt32(txb_id.Text) || existe)
            {
                if (!imagenClickeada)
                {
                    DialogResult result = MessageBox.Show("El Codigo ingresado ya existe, esto es debido a que se ha eliminado un registro ¿Desea agregar manualmente el codigo o seguir en el correlativo siguiente?. para cambiar el numero del campo codigo se encuentra en la parte superior derecha.", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.Yes)
                    {
                        txb_id.Enabled = true;
                        txb_id.ReadOnly = false;
                        return;
                    }
                    int idA = lastId.LastId + 1;
                    txb_id.Text = Convert.ToString(idA);
                }
            }

            //Se crea una instancia de la clase Almacen
            Almacen AlmacenInsert = new Almacen()
            {
                IdAlmacen = Convert.ToInt32(txb_id.Text),
                NombreAlmacen = nameAlmacen,
                DescripcionAlmacen = description,
                UbicacionAlmacen = ubicacion,
                CapacidadAlmacen = capacidad,
                IdBodegaUbicacion = selectedValue
            };

            if (!imagenClickeada)
            {
                //verifica si ya exite un nombre identico
                var existeA = subController.ExisteAlmacen(txb_nombreAlmacen.Text, selectedValue);

                if (existeA)
                {
                    MessageBox.Show("Ya existe un Almacen con el mismo nombre en la Bodega ( " + selectedValueName + " ). Por favor, elija un nombre diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Código que se ejecutará si no se ha hecho clic en la imagen update
                // Llamar al controlador para insertar en la base de datos
                bool exito = subController.InsertarAlmacen(AlmacenInsert);

                if (exito)
                {
                    MessageBox.Show("Almacen agregada correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        log.RegistrarLog(usuario.IdUsuario, "Registro una Almacen", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva Almacen a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el datagrid
                    ShowAlmacenGrid();
                    //borra los datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al agregar la Almacen. Verifique los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //verifica si ya exite un nombre identico
                var existeA = subController.ExisteAlmacen(txb_nombreAlmacen.Text, selectedValue);

                if (existeA && Convert.ToInt32(txb_id.Text) != almacenSeleccionado.IdAlmacen)
                {
                    MessageBox.Show("Ya existe un Almacen con el mismo nombre en la Bodega ( " + selectedValueName + " ). Por favor, elija un nombre diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Código que se ejecutará si se ha hecho clic en la imagen update
                bool exito = subController.ActualizarAlmacens(almacenSeleccionado.IdAlmacen, nameAlmacen, description, capacidad, ubicacion, selectedValue);

                if (exito)
                {
                    MessageBox.Show("Almacen actualizada correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verifica el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Actualizo una Almacen", ModuloActual.NombreModulo, "Actualizacion", "Actualizo datos con ID " + almacenSeleccionado.IdAlmacen + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos
                    ShowAlmacenGrid();

                    //funcion para limpiar las cajas de texto
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la Almacen, Verifique los datos ingresados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                imagenClickeada = false;
            }

        }

        private void txb_nombreAlmacen_Enter(object sender, EventArgs e)
        {
            //CbxBodega();
        }

        private void txb_descripcion_Enter(object sender, EventArgs e)
        {
            //CbxBodega();
        }

        private void txb_capacidad_Enter(object sender, EventArgs e)
        {
            //CbxBodega();
        }

        private void txb_capacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 8;

            if (txb_capacidad.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 7;

            if (txb_id.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_nombreAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 90;

            if (txb_nombreAlmacen.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 190;

            if (txb_descripcion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_ubicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 190;

            if (txb_ubicacion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5,label6,label7, label8 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_capacidad, txb_descripcion, txb_id, txb_nombreAlmacen,txb_ubicacion};
            Button[] buttons = { btn_SaveAlmacen, btn_Cancel };
            ComboBox[] comboBoxes = { cbx_bodega };

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
