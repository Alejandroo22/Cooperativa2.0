using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Product;
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
    public partial class form_claseUva : Form
    {
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        //instancia de la clase mapeo Tipo de Cafe para capturar los datos seleccionado por el usuario
        private TipoCafe tipoCafeSeleccionado;

        public form_claseUva()
        {
            InitializeComponent();

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_claseUva.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowTipoCafeGrid();

            dtg_claseUva.CellPainting += dtgv_claseUva_CellPainting;

            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            //coloca nueva mente el contador en el txb del cdigo
            TipoCafeController cal = new TipoCafeController();
            var count = cal.CountTipoCafe();
            txb_id.Text = Convert.ToString(count.CountTipoCafe + 1);

            AsignarFuente();

        }

        private void dtgv_claseUva_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_claseUva;

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

        public void ShowTipoCafeGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var tipoCafeController = new TipoCafeController();
            List<TipoCafe> datos = tipoCafeController.ObtenerTipoCafes();

            var datosPersonalizados = datos.Select(tipo => new
            {
                ID = tipo.IdTipoCafe,
                Nombre = tipo.NombreTipoCafe,
                Descripcion = tipo.DescripcionTipoCafe
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_claseUva.DataSource = datosPersonalizados;

            dtg_claseUva.RowHeadersVisible = false;
            dtg_claseUva.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_claseUva, txb_descripcion };

            foreach (TextBox textBox in txb)
            {
                textBox.Text = "";
            }

            //coloca nueva mente el contador en el txb del cdigo
            TipoCafeController cal = new TipoCafeController();
            var count = cal.CountTipoCafe();
            txb_id.Text = Convert.ToString(count.CountTipoCafe + 1);
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

        private void dtg_claseUva_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_claseUva.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_claseUva.Rows[e.RowIndex];
                tipoCafeSeleccionado = new TipoCafe();

                // Obtener los valores de las celdas de la fila seleccionada
                tipoCafeSeleccionado.IdTipoCafe = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                tipoCafeSeleccionado.NombreTipoCafe = filaSeleccionada.Cells["Nombre"].Value.ToString();
                tipoCafeSeleccionado.DescripcionTipoCafe = filaSeleccionada.Cells["Descripcion"].Value.ToString();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_modUva_Click(object sender, EventArgs e)
        {
            //condicicion para verificar si los datos seleccionados son nulos
            if (tipoCafeSeleccionado != null)
            {
                DialogResult result = MessageBox.Show("¿Desea actualizar el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //si el usuario selecciono "SI"
                    imagenClickeada = true;

                    //se asignanlos registros a los cuadros de texto
                    txb_id.Text = Convert.ToString(tipoCafeSeleccionado.IdTipoCafe);
                    txb_claseUva.Text = tipoCafeSeleccionado.NombreTipoCafe;
                    txb_descripcion.Text = tipoCafeSeleccionado.DescripcionTipoCafe;
                    txb_id.Enabled = true;
                    txb_id.ReadOnly = false;
                }
            }
            else
            {
                //muestra un mensaje de error o excepción
                MessageBox.Show("No se ha seleccionado conrrectamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_deleteUva_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (tipoCafeSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar los datos registrado del Tipo de Cafe: (" + tipoCafeSeleccionado.NombreTipoCafe + ") ?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    TipoCafeController controller = new TipoCafeController();
                    controller.EliminarTipoCafe(tipoCafeSeleccionado.IdTipoCafe);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminado los datos del Tipo de Cafe", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del Tipo de Cafe (" + tipoCafeSeleccionado.NombreTipoCafe + ") en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Tipo de Cafe Eliminado correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowTipoCafeGrid();
                    tipoCafeSeleccionado = null;
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas del Tipo de Cafe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClearDataTxb();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            imagenClickeada = false;
            this.Close();
        }

        private void btn_SaveUva_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_claseUva.Text))
            {
                MessageBox.Show("El campo Clase Uva, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TipoCafeController tipoController = new TipoCafeController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario

            TextBox[] textBoxes = { txb_descripcion };
            ConvertFirstCharacter(textBoxes);
            TextBox[] textBoxesM = { txb_claseUva };
            ConvertFirstCharacter(textBoxesM);

            if (string.IsNullOrWhiteSpace(txb_descripcion.Text))
            {
                DialogResult result = MessageBox.Show("¿Desea dejar el campo descripcion vacio? Llenar dicho campo permitirá dar una informacion extra a futuros usuarios", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                string name = txb_claseUva.Text;
                string description = txb_descripcion.Text;

                var lastId = tipoController.ObtenerUltimoId();
                Console.WriteLine("ID " + lastId.LastId);
                bool existe = tipoController.ExisteId(Convert.ToInt32(txb_id.Text));
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

                // Crear una instancia de la clase Tipo de Cafe con los valores obtenidos
                TipoCafe tipoInsert = new TipoCafe()
                {
                    IdTipoCafe = Convert.ToInt32(txb_id.Text),
                    NombreTipoCafe = name,
                    DescripcionTipoCafe = description
                };

                if (!imagenClickeada)
                {
                    // Código que se ejecutará si no se ha hecho clic en la imagen update
                    // Llamar al controlador para insertar el Tipo de Cafe en la base de datos
                    bool exito = tipoController.InsertarTipoCafe(tipoInsert);

                    if (!exito)
                    {
                        MessageBox.Show("Error al agregar el Tipo de Cafe. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Tipo de Cafe agregado correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        log.RegistrarLog(usuario.IdUsuario, "Registro de caracteristicas del Tipo de Cafe", ModuloActual.NombreModulo, "Insercion", "Inserto un nuevo Tipo de Cafe a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowTipoCafeGrid();

                    //borrar datos de los textbox
                    ClearDataTxb();
                }
                else
                {

                    // Código que se ejecutará si se ha hecho clic en la imagen update
                    bool exito = tipoController.ActualizarTipoCafe(tipoCafeSeleccionado.IdTipoCafe, name, description);
                    
                    if (!exito)
                    {
                        MessageBox.Show("Error al actualizar los datos del Tipo de Cafe. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Tipo de Cafe actualizado correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verificar el departamento 
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Tipo de Cafe", ModuloActual.NombreModulo, "Actualizacion", "Actualizo las caracteristicas del Tipo de Cafe con ID " + tipoCafeSeleccionado.IdTipoCafe + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowTipoCafeGrid();

                    ClearDataTxb();

                    imagenClickeada = false;
                    tipoCafeSeleccionado = null;

                }
                txb_id.Enabled = false;
                txb_id.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
                MessageBox.Show("Error de tipo (" + ex.Message + "), verifique los datos he intenta nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txb_claseUva_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 72;

            if (txb_claseUva.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
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
            Label[] labels = {label2,label3,label4, label5 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_claseUva, txb_descripcion, txb_id };
            Button[] buttons = { btn_SaveUva, btn_Cancel };

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
