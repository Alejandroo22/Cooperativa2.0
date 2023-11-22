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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_beneficios : Form
    {
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        //instancia de la clase mapeo beneficio para capturar los datos seleccionado por el usuario
        private Beneficio beneficioSeleccionado;

        public form_beneficios()
        {
            InitializeComponent();

            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            //coloca nueva mente el contador en el txb del cdigo
            BeneficioController ben = new BeneficioController();
            var count = ben.CountBeneficio();
            txb_id.Text = Convert.ToString(count.CountBeneficio + 1);

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_beneficios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowBeneficioGrid();

            dtg_beneficios.CellPainting += dtgv_beneficios_CellPainting;

            AsignarFuente();
        }

        private void dtgv_beneficios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_beneficios;

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

        public void ShowBeneficioGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var beneficioController = new BeneficioController();
            List<Beneficio> datos = beneficioController.ObtenerBeneficios();

            var datosPersonalizados = datos.Select(rol => new
            {
                ID = rol.IdBeneficio,
                Nombre = rol.NombreBeneficio,
                Ubicacion = rol.UbicacionBeneficio
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_beneficios.DataSource = datosPersonalizados;

            dtg_beneficios.RowHeadersVisible = false;
            dtg_beneficios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_nombreBeneficio, txb_Ubicacion, txb_id };

            foreach (TextBox textBox in txb)
            {
                textBox.Text = "";
            }
            imagenClickeada = false;
            beneficioSeleccionado = null;

            //coloca nueva mente el contador en el txb del cdigo
            BeneficioController ben = new BeneficioController();
            var count = ben.CountBeneficio();
            txb_id.Text = Convert.ToString(count.CountBeneficio + 1);
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

        private void btn_updateBeneficio_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (beneficioSeleccionado != null)
            { 
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    txb_id.Text = Convert.ToString(beneficioSeleccionado.IdBeneficio);
                    txb_nombreBeneficio.Text = beneficioSeleccionado.NombreBeneficio;
                    txb_Ubicacion.Text = beneficioSeleccionado.UbicacionBeneficio;
                }
                else
                {
                    // El usuario seleccionó "No" o cerró el cuadro de diálogo
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas del Beneficio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_deleteBeneficio_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (beneficioSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar los datos registrado del Beneficio: (" + beneficioSeleccionado.NombreBeneficio + ") ?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    BeneficioController controller = new BeneficioController();
                    controller.EliminarBeneficio(beneficioSeleccionado.IdBeneficio);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de los datos Beneficio", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del Beneficio (" + beneficioSeleccionado.NombreBeneficio + ") en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Beneficio Eliminado correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowBeneficioGrid();
                    ClearDataTxb();
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas del Beneficio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //coloca nueva mente el contador en el txb del cdigo
            BeneficioController ben = new BeneficioController();
            var count = ben.CountBeneficio();
            txb_id.Text = Convert.ToString(count.CountBeneficio + 1);
        }

        private void dtg_beneficios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_beneficios.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_beneficios.Rows[e.RowIndex];
                beneficioSeleccionado = new Beneficio();

                // Obtener los valores de las celdas de la fila seleccionada
                beneficioSeleccionado.IdBeneficio = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                beneficioSeleccionado.NombreBeneficio = filaSeleccionada.Cells["Nombre"].Value.ToString();
                beneficioSeleccionado.UbicacionBeneficio = filaSeleccionada.Cells["Ubicacion"].Value.ToString();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            this.Close();
        }

        private void btn_SaveBeneficio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_nombreBeneficio.Text))
            {
                MessageBox.Show("El campo Nombre, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_Ubicacion.Text))
            {
                DialogResult result = MessageBox.Show("El campo Ubicacion, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No)
                {
                    return;
                }
                txb_Ubicacion.Text = ".";
            }

            BeneficioController beneficioController = new BeneficioController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario

            TextBox[] textBoxes = { txb_nombreBeneficio };
            TextBox[] textBoxesF = { txb_Ubicacion };
            ConvertFirstCharacter(textBoxesF);
            ConvertAllUppercase(textBoxes);

            try
            {
                string name = txb_nombreBeneficio.Text;
                string location = txb_Ubicacion.Text;

                var lastId = beneficioController.ObtenerUltimoId();
                bool existe = beneficioController.ExisteId(Convert.ToInt32(txb_id.Text));
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

                // Crear una instancia de la clase Beneficio con los valores obtenidos
                Beneficio beneficioInsert = new Beneficio()
                {
                    IdBeneficio = Convert.ToInt32(txb_id.Text),
                    NombreBeneficio = name,
                    UbicacionBeneficio = location
                };

                if (!imagenClickeada)
                {
                    // Código que se ejecutará si no se ha hecho clic en la imagen update
                    // Llamar al controlador para insertar el beneficio en la base de datos
                    bool exito = beneficioController.InsertarBeneficio(beneficioInsert);

                    if (!exito)
                    {
                        MessageBox.Show("Error al agregar el Beneficio. Verifica los datos e intenta nuevamente.");
                        return;
                    }

                    MessageBox.Show("Beneficio agregado correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //Console.WriteLine("el ID obtenido del usuario "+usuario.IdUsuario);
                        //verificar el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Registro de caracteristicas del Beneficio", ModuloActual.NombreModulo, "Insercion", "Inserto un nuevo Beneficio a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowBeneficioGrid();

                    //borrar datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    // Código que se ejecutará si se ha hecho clic en la imagen update
                    bool exito = beneficioController.ActualizarBeneficio(beneficioSeleccionado.IdBeneficio, name, location);

                    if (!exito)
                    {
                        MessageBox.Show("Error al actualizar los datos del Beneficio. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Beneficio actualizado correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verificar el departamento 
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Beneficio", ModuloActual.NombreModulo, "Actualizacion", "Actualizo las caracteristicas del Beneficio con ID " + beneficioSeleccionado.IdBeneficio + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowBeneficioGrid();

                    ClearDataTxb();

                }
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

        private void txb_nombreBeneficio_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 72;

            if (txb_nombreBeneficio.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_Ubicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 98;

            if (txb_Ubicacion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_nombreBeneficio, txb_Ubicacion, txb_id };
            Button[] buttons = { btn_SaveBeneficio, btn_Cancel };

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
