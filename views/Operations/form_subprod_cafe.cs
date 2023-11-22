using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping;
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
    public partial class form_subprod_cafe : Form
    {
        private SubProducto subProdcSeleccionado;
        private int iccafe;
        private bool imagenClickeada = false;

        public form_subprod_cafe()
        {
            InitializeComponent();

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_subProdCafe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //funcion para mostrar de inicio los datos en el dataGrid
            ShowSubProductGrid();

            dtg_subProdCafe.CellPainting += dataGrid_SubProduct_CellPainting;
            
            txb_calidadCafe.ReadOnly = true;
            txb_calidadCafe.Enabled = false;
            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            //coloca nueva mente el contador en el txb del cdigo
            SubProductoController sp = new SubProductoController();
            var count = sp.CountSubProducto();
            txb_id.Text = Convert.ToString(count.CountSubProducto + 1);

            AsignarFuente();

        }

        private void dataGrid_SubProduct_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_subProdCafe;

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

        public void ShowSubProductGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            SubProductoController subController = new SubProductoController();
            List<SubProducto> datos = subController.ObtenerNombreSubProductos();

            var datosPersonalizados = datos.Select(sub => new
            {
                ID = sub.IdSubProducto,
                Nombre = sub.NombreSubProducto,
                Descripcion = sub.DescripcionSubProducto,
                Calidad_Cafe = sub.NombreCalidadCafe
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_subProdCafe.DataSource = datosPersonalizados;

            dtg_subProdCafe.RowHeadersVisible = false;
            dtg_subProdCafe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void btn_tableCalidades_Click(object sender, EventArgs e)
        {
            form_tablaCCafe tablaCCafe = new form_tablaCCafe();
            if (tablaCCafe.ShowDialog() == DialogResult.OK)
            {
                txb_calidadCafe.Text = CalidadSeleccionada.NombreCalidadSeleccionada;
            }
        }

        private void dtg_subProdCafe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_subProdCafe.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_subProdCafe.Rows[e.RowIndex];
                subProdcSeleccionado = new SubProducto();

                // Obtener los valores de las celdas de la fila seleccionada
                subProdcSeleccionado.IdSubProducto = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                subProdcSeleccionado.NombreSubProducto = filaSeleccionada.Cells["Nombre"].Value.ToString();
                subProdcSeleccionado.DescripcionSubProducto = filaSeleccionada.Cells["Descripcion"].Value.ToString();
                subProdcSeleccionado.NombreCalidadCafe = filaSeleccionada.Cells["Calidad_Cafe"].Value.ToString();
                CalidadSeleccionada.NombreCalidadSeleccionada = subProdcSeleccionado.NombreCalidadCafe;
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_modSubProdCafe_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (subProdcSeleccionado != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    SubProductoController subC = new SubProductoController();
                    CCafeController cCafe = new CCafeController();
                    var name = cCafe.ObtenerNombreCalidad(CalidadSeleccionada.NombreCalidadSeleccionada);
                    iccafe = name.IdCalidad;
                    CalidadSeleccionada.ICalidadSeleccionada = iccafe;

                    txb_id.Text = Convert.ToString(subProdcSeleccionado.IdSubProducto);
                    txb_subProdCafe.Text = subProdcSeleccionado.NombreSubProducto;
                    txb_descripcion.Text = subProdcSeleccionado.DescripcionSubProducto;
                    txb_calidadCafe.Text = subProdcSeleccionado.NombreCalidadCafe;
                    
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_deleteSubProdCafe_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (subProdcSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();

                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro del usuario: " + subProdcSeleccionado.NombreSubProducto + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    SubProductoController controller = new SubProductoController();
                    controller.EliminarSubProducto(subProdcSeleccionado.IdSubProducto);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato SubProducto", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del SubProducto " + subProdcSeleccionado.NombreSubProducto + " en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("SubProducto Eliminada correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowSubProductGrid();
                    ClearDataTxb();
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
            imagenClickeada = false;
            subProdcSeleccionado = null;
            this.Close();
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_subProdCafe, txb_descripcion, txb_calidadCafe};

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }
            //coloca nueva mente el contador en el txb del cdigo
            SubProductoController sp = new SubProductoController();
            var count = sp.CountSubProducto();
            txb_id.Text = Convert.ToString(count.CountSubProducto + 1);
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

        private void btn_SaveSubProdCafe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_subProdCafe.Text))
            {
                MessageBox.Show("El campo Sub Producto Cafe, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txb_calidadCafe.Text))
            {
                MessageBox.Show("El campo Calidad Cafe, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SubProductoController subController = new SubProductoController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario);

            TextBox[] textBoxes = { txb_subProdCafe };
            TextBox[] textBoxesLetter = { txb_descripcion };
            ConvertAllUppercase(textBoxes);
            ConvertFirstLetter(textBoxesLetter);

            //se obtiene los valores ingresados por el usuario
            string nameSubProducto = txb_subProdCafe.Text;
            string description = txb_descripcion.Text;

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

            //Se crea una instancia de la clase SubProducto
            SubProducto subPro = new SubProducto()
            {
                IdSubProducto = Convert.ToInt32(txb_id.Text),
                NombreSubProducto = nameSubProducto,
                DescripcionSubProducto = description,
                IdCalidadCafe = CalidadSeleccionada.ICalidadSeleccionada
            };

            if (string.IsNullOrWhiteSpace(txb_descripcion.Text))
            {
                DialogResult result = MessageBox.Show("¿Desea dejar el campo descripcion vacio? Llenar dicho campo permitirá dar una informacion extra a futuros usuarios", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (!imagenClickeada)
            {
                // Código que se ejecutará si no se ha hecho clic en la imagen update

                //verifica si ya exite un nombre identico
                var existeSP = subController.ExisteSubProducto(txb_subProdCafe.Text, CalidadSeleccionada.ICalidadSeleccionada);

                if (existeSP)
                {
                    MessageBox.Show("Ya existe un SubProducto con el mismo nombre de la Calidad Cafe ( " + CalidadSeleccionada.NombreCalidadSeleccionada + " ). Por favor, elija un nombre diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Llamar al controlador para insertar en la base de datos
                bool exito = subController.InsertarSubProducto(subPro);

                if (exito)
                {
                    MessageBox.Show("SubProducto agregada correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        log.RegistrarLog(usuario.IdUsuario, "Registro un SubProducto", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva SubProducto a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el datagrid
                    ShowSubProductGrid();
                    //borra los datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al agregar el SubProducto. Verifique los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Se crea una instancia de la clase SubProducto
                SubProducto subProUpd = new SubProducto()
                {
                    IdSubProducto = subProdcSeleccionado.IdSubProducto,
                    NombreSubProducto = nameSubProducto,
                    DescripcionSubProducto = description,
                    IdCalidadCafe = CalidadSeleccionada.ICalidadSeleccionada
                };

                //verifica si ya exite un nombre identico
                var existeSP = subController.ExisteSubProducto(txb_subProdCafe.Text, CalidadSeleccionada.ICalidadSeleccionada);

                if (existeSP && Convert.ToInt32(txb_id.Text) != subProdcSeleccionado.IdSubProducto)
                {
                    MessageBox.Show("Ya existe un SubProducto con el mismo nombre de la Calidad Cafe ( " + CalidadSeleccionada.NombreCalidadSeleccionada + " ). Por favor, elija un nombre diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Código que se ejecutará si se ha hecho clic en la imagen update
                bool exito = subController.ActualizarSubProducto(subProUpd);

                if (exito)
                {
                    MessageBox.Show("SubProducto actualizada correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verifica el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Actualizo un SubProducto", ModuloActual.NombreModulo, "Actualizacion", "Actualizo datos con ID " + subProdcSeleccionado.IdSubProducto + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos
                    ShowSubProductGrid();

                    //funcion para limpiar las cajas de texto
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el SubProducto, Verifique los datos ingresados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                imagenClickeada = false;
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

        private void txb_subProdCafe_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 70;

            if (txb_subProdCafe.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 120;

            if (txb_descripcion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5, label6 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_calidadCafe, txb_descripcion, txb_id,txb_subProdCafe };
            Button[] buttons = { btn_SaveSubProdCafe, btn_Cancel };

            //se asigna a los label de encaebzado
            FontViews.LabelStyle(labels);
            //se asigna al label de titulo de formulario
            FontViews.LabelStyleTitle(labeltitle);
            //se asigna a textbox
            FontViews.TextBoxStyle(textBoxes);
            //se asigna a botones
            FontViews.ButtonStyleGC(buttons);
        }
    }
}
