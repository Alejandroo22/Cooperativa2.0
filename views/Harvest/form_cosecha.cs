using sistema_modular_cafe_majada.controller.HarvestController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
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
    public partial class form_cosecha : Form
    {
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        //instancia de la clase mapeo beneficio para capturar los datos seleccionado por el usuario
        private Cosecha cosechaSeleccionado;
        private List<TextBox> txbRestrict;

        public form_cosecha()
        {
            InitializeComponent();

            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            txbRestrict = new List<TextBox> { txb_nombre };

            RestrictTextBoxNum(txbRestrict);

            //coloca nueva mente el contador en el txb del cdigo
            CosechaController cosec = new CosechaController();
            var count = cosec.CountCosecha();
            txb_id.Text = Convert.ToString(count.CountCosecha + 1);

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_cosechas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowCosechaGrid();

            AsignarFuente();
        }

        private void dtgv_cosechas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_cosechas;

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

        public void ShowCosechaGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var cosechaController = new CosechaController();
            List<Cosecha> datos = cosechaController.ObtenerCosecha();

            var datosPersonalizados = datos.Select(cos => new
            {
                ID = cos.IdCosecha,
                Nombre = cos.NombreCosecha
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_cosechas.DataSource = datosPersonalizados;
            
            // Evitar que el usuario ajuste el tamaño de las filas y columnas manualmente
            dtg_cosechas.AllowUserToResizeRows = false;
            dtg_cosechas.AllowUserToResizeColumns = false;

            dtg_cosechas.RowHeadersVisible = false;
            dtg_cosechas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_id, txb_nombre };

            foreach (TextBox textBox in txb)
            {
                textBox.Text = "";
            }

            //coloca nueva mente el contador en el txb del cdigo
            CosechaController cosec = new CosechaController();
            var count = cosec.CountCosecha();
            txb_id.Text = Convert.ToString(count.CountCosecha + 1);


            imagenClickeada = false;
            cosechaSeleccionado = null;
        }

        //
        public void RestrictTextBoxNum(List<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.KeyPress += (sender, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
                    {
                        e.Handled = true; // Cancela el evento KeyPress si no es un dígito o el carácter '/'
                    }

                    // Permite solo un '/' en el TextBox
                    if (e.KeyChar == '/' && (textBox.Text.Contains("/")))
                    {
                        e.Handled = true; // Cancela el evento KeyPress si ya hay un '/' en el TextBox
                    }
                };
            }
        }

        private void dtg_cosechas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_cosechas.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_cosechas.Rows[e.RowIndex];
                cosechaSeleccionado = new Cosecha();

                // Obtener los valores de las celdas de la fila seleccionada
                cosechaSeleccionado.IdCosecha = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                cosechaSeleccionado.NombreCosecha = filaSeleccionada.Cells["Nombre"].Value.ToString();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_mod_cosecha_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (cosechaSeleccionado != null)
            { 
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    txb_id.Text = Convert.ToString(cosechaSeleccionado.IdCosecha);
                    txb_nombre.Text = cosechaSeleccionado.NombreCosecha;
                    txb_id.Enabled = true;
                    txb_id.ReadOnly = false;
                }
                else
                {
                    // El usuario seleccionó "No" o cerró el cuadro de diálogo
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas de la Cosecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_cosecha_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (cosechaSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar los datos registrado de la Cosecha: (" + cosechaSeleccionado.NombreCosecha + ") ?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    CosechaController controller = new CosechaController();
                    controller.EliminarCosecha(cosechaSeleccionado.IdCosecha);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de los datos Cosecha", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos de la Cosecha (" + cosechaSeleccionado.NombreCosecha + ") en la base de datos");

                    if(SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Cosecha Eliminada correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowCosechaGrid();
                    cosechaSeleccionado = null;
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas de la Procedencia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //coloca nueva mente el contador en el txb del cdigo
            CosechaController cosec = new CosechaController();
            var count = cosec.CountCosecha();
            txb_id.Text = Convert.ToString(count.CountCosecha + 1);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_nombre.Text))
            {
                MessageBox.Show("El campo Nombre, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CosechaController cosController = new CosechaController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario

            try
            {
                string name = txb_nombre.Text;

                var lastId = cosController.ObtenerUltimoId();
                if (lastId.LastId == Convert.ToInt32(txb_id.Text))
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

                // Crear una instancia de la clase Beneficio con los valores obtenidos
                Cosecha cosecha = new Cosecha()
                {
                    IdCosecha = Convert.ToInt32(txb_id.Text),
                    NombreCosecha = name
                };

                if (!imagenClickeada)
                {
                    // Código que se ejecutará si no se ha hecho clic en la imagen update
                    // Llamar al controlador para insertar la Procedencia en la base de datos
                    bool exito = cosController.InsertarCosecha(cosecha);

                    if (!exito)
                    {
                        MessageBox.Show("Error al agregar la Cosecha. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Cosecha agregado correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verificar el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Registro de caracteristicas de la Cosecha", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva Cosecha a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowCosechaGrid();

                    //borrar datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    // Código que se ejecutará si se ha hecho clic en la imagen update
                    bool exito = cosController.ActualizarCosecha(Convert.ToInt32(txb_id.Text), name);

                    if (!exito)
                    {
                        MessageBox.Show("Error al actualizar los datos de la Cosecha. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Cosecha actualizada correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verificar el departamento 
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Cosecha", ModuloActual.NombreModulo, "Actualizacion", "Actualizo las caracteristicas de la Cosecha con ID " + cosechaSeleccionado.IdCosecha + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowCosechaGrid();
                    ClearDataTxb();
                }

                txb_id.Enabled = false;
                txb_id.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
                MessageBox.Show("Error de tipo (" + ex.Message + "), verifique los datos he intenta nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //coloca nueva mente el contador en el txb del cdigo
            CosechaController cosec = new CosechaController();
            var count = cosec.CountCosecha();
            txb_id.Text = Convert.ToString(count.CountCosecha + 1);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            imagenClickeada = false;

            //coloca nueva mente el contador en el txb del cdigo
            CosechaController cosec = new CosechaController();
            var count = cosec.CountCosecha();
            txb_id.Text = Convert.ToString(count.CountCosecha + 1);
            this.Close();
        }

        private void txb_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 5;

            if (txb_nombre.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
            else if (char.IsDigit(e.KeyChar))
            {
                // Agregar el dígito a la caja de texto
                txb_nombre.Text += e.KeyChar;

                // Verificar si ya existe un '/' en la cadena
                bool colonExists = txb_nombre.Text.Contains("/");

                // Agregar automáticamente un '/' si no existe y la longitud es par
                if (!colonExists && txb_nombre.Text.Length == 2)
                {
                    txb_nombre.Text += "/";
                    txb_nombre.SelectionStart = txb_nombre.Text.Length; // Mover el cursor al final
                }

                e.Handled = true; // Manejar el evento KeyPress
            }
        }

        private void txb_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 6;

            if (txb_id.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2, label3 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_nombre, txb_id };
            Button[] buttons = { btn_save, btn_Cancel };

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
