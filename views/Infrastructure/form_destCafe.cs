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
    public partial class form_destCafe : Form
    {
        private Bodega bodegaSeleccionado;
        private int ibenef;
        private bool imagenClickeada = false;

        public form_destCafe()
        {
            InitializeComponent();

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_destCafe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //funcion para mostrar de inicio los datos en el dataGrid
            ShowBodegaGrid();

            dtg_destCafe.CellPainting += dataGrid_Bodega_CellPainting;

            txb_beneficio.ReadOnly = true;
            txb_beneficio.Enabled = false;
            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            //coloca nueva mente el contador en el txb del cdigo
            BodegaController cal = new BodegaController();
            var count = cal.CountBodega();
            txb_id.Text = Convert.ToString(count.CountBodega + 1);

            AsignarFuente();

        }

        private void btn_tBeneficio_Click(object sender, EventArgs e)
        {
            TablaSeleccionadabodega.ITable = 1;
            form_tablaBeneficio tablaBeneficio = new form_tablaBeneficio();
            if (tablaBeneficio.ShowDialog() == DialogResult.OK)
            {
                txb_beneficio.Text = BeneficioSeleccionado.NombreBeneficioSeleccionado;
            }
        }

        private void dataGrid_Bodega_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_destCafe;

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

        public void ShowBodegaGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            BodegaController bodegaController = new BodegaController();
            List<Bodega> datos = bodegaController.ObtenerBodegaNombreBeneficio();

            var datosPersonalizados = datos.Select(bodega => new
            {
                ID = bodega.IdBodega,
                Nombre = bodega.NombreBodega,
                Descripcion = bodega.DescripcionBodega,
                Ubicacion = bodega.UbicacionBodega,
                Beneficio = bodega.NombreBenficioUbicacion
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_destCafe.DataSource = datosPersonalizados;

            dtg_destCafe.RowHeadersVisible = false;
            dtg_destCafe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void dtg_Bodega_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_destCafe.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_destCafe.Rows[e.RowIndex];
                bodegaSeleccionado = new Bodega();

                // Obtener los valores de las celdas de la fila seleccionada
                bodegaSeleccionado.IdBodega = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                bodegaSeleccionado.NombreBodega = filaSeleccionada.Cells["Nombre"].Value.ToString();
                bodegaSeleccionado.DescripcionBodega = filaSeleccionada.Cells["Descripcion"].Value.ToString();
                bodegaSeleccionado.UbicacionBodega = filaSeleccionada.Cells["Ubicacion"].Value.ToString();
                bodegaSeleccionado.NombreBenficioUbicacion = filaSeleccionada.Cells["Beneficio"].Value.ToString();
                BeneficioSeleccionado.NombreBeneficioSeleccionado = bodegaSeleccionado.NombreBenficioUbicacion;
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_deleteDestino_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (bodegaSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();

                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro de la Bodega: " + bodegaSeleccionado.NombreBodega + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    BodegaController controller = new BodegaController();
                    controller.EliminarBodegas(bodegaSeleccionado.IdBodega);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato Bodega", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos de la Bodega " + bodegaSeleccionado.NombreBodega + " en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Bodega Eliminada correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowBodegaGrid();
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
            bodegaSeleccionado = null;
            this.Close();
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_nombre, txb_descripcion, txb_ubicacion, txb_beneficio };

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }
            //coloca nueva mente el contador en el txb del cdigo
            BodegaController cal = new BodegaController();
            var count = cal.CountBodega();
            txb_id.Text = Convert.ToString(count.CountBodega + 1);
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

        private void btn_modDestino_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (bodegaSeleccionado != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    BodegaController bodegaC = new BodegaController();
                    BeneficioController benefC = new BeneficioController();
                    var name = benefC.ObtenerBeneficioNombre(BeneficioSeleccionado.NombreBeneficioSeleccionado);
                    ibenef = name.IdBeneficio;
                    BeneficioSeleccionado.IdBeneficioSleccionado = ibenef;

                    txb_id.Text = Convert.ToString(bodegaSeleccionado.IdBodega);
                    txb_nombre.Text = bodegaSeleccionado.NombreBodega;
                    txb_descripcion.Text = bodegaSeleccionado.DescripcionBodega;
                    txb_ubicacion.Text = bodegaSeleccionado.UbicacionBodega;
                    txb_beneficio.Text = bodegaSeleccionado.NombreBenficioUbicacion;
                    txb_id.Enabled = true;
                    txb_id.ReadOnly = false;
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_SaveDestino_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_nombre.Text))
            {
                MessageBox.Show("El campo Nombre Destino, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txb_ubicacion.Text))
            {
                DialogResult result = MessageBox.Show("El campo Ubicacion, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.No)
                {
                    return;
                }
                txb_ubicacion.Text = ".";
            }
            
            if (string.IsNullOrWhiteSpace(txb_beneficio.Text))
            {
                MessageBox.Show("El campo Beneficio, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            BodegaController subController = new BodegaController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario);

            TextBox[] textBoxes = { txb_ubicacion };
            TextBox[] textBoxesM = { txb_nombre };
            TextBox[] textBoxesLetter = { txb_descripcion };
            ConvertFirstCharacter(textBoxes);
            ConvertFirstLetter(textBoxesLetter);
            ConvertAllUppercase(textBoxesM);

            //se obtiene los valores ingresados por el usuario
            string nameBodega = txb_nombre.Text;
            string ubicacion = txb_ubicacion.Text;
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

            //Se crea una instancia de la clase Bodega
            Bodega bodegaInsert = new Bodega()
            {
                IdBodega = Convert.ToInt32(txb_id.Text),
                NombreBodega = nameBodega,
                DescripcionBodega = description,
                UbicacionBodega = ubicacion,
                IdBenficioUbicacion = BeneficioSeleccionado.IdBeneficioSleccionado
            };

            if (!imagenClickeada)
            {
                //verifica si ya exite un nombre identico
                var existeB = subController.ExisteBodega(txb_nombre.Text, BeneficioSeleccionado.IdBeneficioSleccionado);

                if (existeB)
                {
                    MessageBox.Show("Ya existe una bodega con el mismo nombre en el beneficio ( " + BeneficioSeleccionado.NombreBeneficioSeleccionado + " ). Por favor, elija un nombre diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Código que se ejecutará si no se ha hecho clic en la imagen update
                // Llamar al controlador para insertar en la base de datos
                bool exito = subController.InsertarBodega(bodegaInsert);

                if (exito)
                {
                    MessageBox.Show("Bodega agregada correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        log.RegistrarLog(usuario.IdUsuario, "Registro una Bodega", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva Bodega a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el datagrid
                    ShowBodegaGrid();
                    //borra los datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al agregar la Bodega. Verifique los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //verifica si ya exite un nombre identico
                var existeB = subController.ExisteBodega(txb_nombre.Text, BeneficioSeleccionado.IdBeneficioSleccionado);

                if (existeB && Convert.ToInt32(txb_id.Text) != bodegaSeleccionado.IdBodega)
                {
                    MessageBox.Show("Ya existe una bodega con el mismo nombre en el beneficio ( " + BeneficioSeleccionado.NombreBeneficioSeleccionado + " ). Por favor, elija un nombre diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Código que se ejecutará si se ha hecho clic en la imagen update
                Console.WriteLine("id beneficio" + ibenef);
                bool exito = subController.ActualizarBodegas(bodegaSeleccionado.IdBodega, nameBodega, description, ubicacion, BeneficioSeleccionado.IdBeneficioSleccionado);

                if (exito)
                {
                    MessageBox.Show("Bodega actualizada correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verifica el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Actualizo una Bodega", ModuloActual.NombreModulo, "Actualizacion", "Actualizo datos con ID " + bodegaSeleccionado.IdBodega + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos
                    ShowBodegaGrid();

                    //funcion para limpiar las cajas de texto
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la Bodega, Verifique los datos ingresados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                imagenClickeada = false;
            }
            txb_id.Enabled = false;
            txb_id.ReadOnly = true;
        }

        private void txb_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 7;

            if (txb_id.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 98;

            if (txb_nombre.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 195;

            if (txb_descripcion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_ubicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 175;

            if (txb_ubicacion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5, label6,label7 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_beneficio, txb_descripcion, txb_id,txb_nombre,txb_ubicacion };
            Button[] buttons = { btn_SaveDestino, btn_Cancel };

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
