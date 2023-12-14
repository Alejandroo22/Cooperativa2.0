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
//using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using sistema_modular_cafe_majada.model.UserData;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_maquinas : Form
    {
        private Maquinaria maquinaSeleccionada;
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;

        private int iben;


        public form_maquinas()
        {
            InitializeComponent();

            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            //coloca nueva mente el contador en el txb del cdigo
            MaquinariaController ben = new MaquinariaController();
            var count = ben.CountMaquinaria();
            txb_id.Text = Convert.ToString(count.CountMaquina + 1);

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_maquina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowMaquinasGrid();

            AsignarFuente();
        }

        private void btn_fallaMaquina_Click(object sender, EventArgs e)
        {
            form_fallaMaquina fallaMaquina = new form_fallaMaquina();
            fallaMaquina.ShowDialog();
        }

        public void ShowMaquinasGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var maquinaController = new MaquinariaController();
            List<Maquinaria> datos = maquinaController.ObtenerMaquinariaNombreBeneficio();

            var datosPersonalizados = datos.Select(tipo => new
            {
                ID = tipo.IdMaquinaria,
                Nombre = tipo.NombreMaquinaria,
                NoSerie = tipo.NumeroSerieMaquinaria,
                Modelo = tipo.ModeloMaquinaria,
                CapMaxima = tipo.CapacidadMaxMaquinaria,
                Proveedor = tipo.ProveedorMaquinaria,
                Direccion = tipo.DireccionProveedorMaquinaria,
                Telefono = tipo.TelefonoProveedorMaquinaria,
                Nocontrato = tipo.ContratoServicioMaquinaria,
                Beneficio = tipo.NombreBeneficio
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_maquina.DataSource = datosPersonalizados;

            dtg_maquina.Columns["ID"].HeaderText = "Identificador de Maquina";
            dtg_maquina.Columns["Nombre"].HeaderText = "Nombre de la Maquina";
            dtg_maquina.Columns["NoSerie"].HeaderText = "Numero de Serie";
            dtg_maquina.Columns["Modelo"].HeaderText = "Modelo";
            dtg_maquina.Columns["CapMaxima"].HeaderText = "Capacidad Máxima";
            dtg_maquina.Columns["Proveedor"].HeaderText = "Proveedor";
            dtg_maquina.Columns["Direccion"].HeaderText = "Dirección del proveedor";
            dtg_maquina.Columns["Telefono"].HeaderText = "Telefono del proveedor";
            dtg_maquina.Columns["Nocontrato"].HeaderText = "Numero de Contrato";
            dtg_maquina.Columns["Beneficio"].HeaderText = "Beneficio de Trabajo";

            dtg_maquina.RowHeadersVisible = false;
            dtg_maquina.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dtg_maquina_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_maquina.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_maquina.Rows[e.RowIndex];
                maquinaSeleccionada = new Maquinaria();

                // Obtener los valores de las celdas de la fila seleccionada
                maquinaSeleccionada.IdMaquinaria = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                maquinaSeleccionada.NombreMaquinaria = filaSeleccionada.Cells["Nombre"].Value.ToString();
                maquinaSeleccionada.NumeroSerieMaquinaria = filaSeleccionada.Cells["NoSerie"].Value.ToString();
                maquinaSeleccionada.ModeloMaquinaria = filaSeleccionada.Cells["Modelo"].Value.ToString();
                maquinaSeleccionada.CapacidadMaxMaquinaria = Convert.ToDouble(filaSeleccionada.Cells["CapMaxima"].Value);
                maquinaSeleccionada.ProveedorMaquinaria = filaSeleccionada.Cells["Proveedor"].Value.ToString();
                maquinaSeleccionada.DireccionProveedorMaquinaria = filaSeleccionada.Cells["Direccion"].Value.ToString();
                maquinaSeleccionada.TelefonoProveedorMaquinaria = filaSeleccionada.Cells["Telefono"].Value.ToString();
                maquinaSeleccionada.ContratoServicioMaquinaria = filaSeleccionada.Cells["Nocontrato"].Value.ToString();
                maquinaSeleccionada.NombreBeneficio = filaSeleccionada.Cells["Beneficio"].Value.ToString();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_tBeneficioM_Click(object sender, EventArgs e)
        {
            TablaSeleccionadabodega.ITable = 1;
            form_tablaBeneficio tablaBeneficio = new form_tablaBeneficio();
            if (tablaBeneficio.ShowDialog() == DialogResult.OK)
            {
                txb_beneficio.Text = BeneficioSeleccionado.NombreBeneficioSeleccionado;
            }
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_maquina, txb_maxCapacidad, txb_modelMaquina, txb_beneficio,txb_numContrato,
                txb_numSerie,txb_proveedor,txb_proveedorDireccion,txb_proveedorTelefono };

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }

            //coloca nueva mente el contador en el txb del cdigo
            MaquinariaController ben = new MaquinariaController();
            var count = ben.CountMaquinaria();
            txb_id.Text = Convert.ToString(count.CountMaquina + 1);
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

        private void btn_SaveMaquina_Click(object sender, EventArgs e)
        {
            //se valida si el textbox esta vacio
            if (string.IsNullOrWhiteSpace(txb_maquina.Text))
            {
                MessageBox.Show("El campo Maquina, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_numSerie.Text))
            {
                MessageBox.Show("El campo Numero de Serie, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_modelMaquina.Text))
            {
                MessageBox.Show("El campo Modelo, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_proveedor.Text))
            {
                MessageBox.Show("El campo Numero de Serie, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_proveedorDireccion.Text))
            {
                MessageBox.Show("El campo Numero de Serie, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_proveedorTelefono.Text))
            {
                MessageBox.Show("El campo Numero de Serie, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(txb_beneficio.Text))
            {
                MessageBox.Show("El campo Numero de Serie, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txb_maxCapacidad.Text))
            {
                DialogResult result = MessageBox.Show("¿Desea dejar el campo Capacidad Máxima vacio? Llenar dicho campo permitirá dar una informacion extra a futuros usuarios", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // si los textbox que requieren datos obligatoriamente estan llenos

            MaquinariaController subController = new MaquinariaController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario);

            TextBox[] textBoxes = { txb_beneficio,txb_modelMaquina,txb_numContrato,txb_numSerie,txb_proveedor,txb_proveedorDireccion,txb_proveedorTelefono };
            TextBox[] textBoxesLetter = { txb_maquina };
            ConvertFirstCharacter(textBoxes);
            ConvertAllUppercase(textBoxesLetter);

            //se obtiene los valores ingresados por el usuario
            string nameMaquina = txb_maquina.Text;
            string numSerie = txb_numSerie.Text;
            string modelo = txb_modelMaquina.Text;
            double capMaxima;
            if (double.TryParse(txb_maxCapacidad.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out capMaxima)){}
            
            string proveedor = txb_proveedor.Text;
            string direccionProv = txb_proveedorDireccion.Text;
            string telProveedor = txb_proveedorTelefono.Text;
            string contrato = txb_numContrato.Text;
            string beneficio = txb_beneficio.Text;

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
            Maquinaria maquinaInsert = new Maquinaria()
            {
                IdMaquinaria = Convert.ToInt32(txb_id.Text),
                NombreMaquinaria = nameMaquina,
                NumeroSerieMaquinaria = numSerie,
                ModeloMaquinaria = modelo,
                CapacidadMaxMaquinaria = capMaxima,
                ProveedorMaquinaria = proveedor,
                DireccionProveedorMaquinaria = direccionProv,
                TelefonoProveedorMaquinaria = telProveedor,
                ContratoServicioMaquinaria = contrato,
                IdBeneficio = BeneficioSeleccionado.IdBeneficioSleccionado
            };

            if (!imagenClickeada)
            {
                // Código que se ejecutará si no se ha hecho clic en la imagen update
                // Llamar al controlador para insertar en la base de datos
                bool exito = subController.InsertarMaquinaria(maquinaInsert);

                if (exito)
                {
                    MessageBox.Show("Maquina agregada correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        log.RegistrarLog(usuario.IdUsuario, "Registro una Maquina", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva Maquina a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el datagrid
                    ShowMaquinasGrid();
                    //borra los datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al agregar la Maquina. Verifique los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Código que se ejecutará si se ha hecho clic en la imagen update
                bool exito = subController.ActualizarMaquinaria(maquinaSeleccionada.IdMaquinaria, nameMaquina, numSerie,modelo,capMaxima,
                    proveedor, direccionProv, telProveedor,contrato, iben);

                if (exito)
                {
                    MessageBox.Show("Maquina actualizada correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verifica el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Actualizo una Maquina", ModuloActual.NombreModulo, "Actualizacion", "Actualizo datos con ID " + maquinaSeleccionada.IdMaquinaria + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos
                    ShowMaquinasGrid();

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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            imagenClickeada = false;
            maquinaSeleccionada = null;
            this.Close();
        }

        private void btn_modMaquina_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (maquinaSeleccionada != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    MaquinariaController maquinariaC = new MaquinariaController();
                    BeneficioController benefC = new BeneficioController();
                    var name = benefC.ObtenerBeneficioNombre(maquinaSeleccionada.NombreBeneficio);
                    iben = name.IdBeneficio;

                    txb_id.Text = Convert.ToString(maquinaSeleccionada.IdMaquinaria);
                    txb_maquina.Text = maquinaSeleccionada.NombreMaquinaria;
                    txb_numSerie.Text=maquinaSeleccionada.NumeroSerieMaquinaria;
                    txb_modelMaquina.Text=maquinaSeleccionada.ModeloMaquinaria;
                    txb_maxCapacidad.Text=maquinaSeleccionada.CapacidadMaxMaquinaria.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
                    txb_proveedor.Text=maquinaSeleccionada.ProveedorMaquinaria;
                    txb_proveedorDireccion.Text=maquinaSeleccionada.DireccionProveedorMaquinaria;
                    txb_proveedorTelefono.Text=maquinaSeleccionada.TelefonoProveedorMaquinaria;
                    txb_numContrato.Text=maquinaSeleccionada.ContratoServicioMaquinaria;
                    txb_beneficio.Text=maquinaSeleccionada.NombreBeneficio;
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

        private void btn_deleteMaquina_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (maquinaSeleccionada != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();

                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro de la Bodega: " + maquinaSeleccionada.NombreMaquinaria + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    BodegaController controller = new BodegaController();
                    controller.EliminarBodegas(maquinaSeleccionada.IdMaquinaria);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de dato Maquina", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos de la Maquina " + maquinaSeleccionada.NombreMaquinaria + " en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Bodega Eliminada correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowMaquinasGrid();
                    ClearDataTxb();
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtg_maquina_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_maquina;

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

        private void txb_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 7;

            if (txb_id.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_maquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 120;

            if (txb_maquina.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_numSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 70;

            if (txb_numSerie.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_modelMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 70;

            if (txb_modelMaquina.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_maxCapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 8;

            if (txb_maxCapacidad.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 145;

            if (txb_proveedor.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_proveedorDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 145;

            if (txb_proveedorDireccion.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_proveedorTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 11;

            if (txb_proveedorTelefono.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_numContrato_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 200;

            if (txb_numContrato.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5, label6,label7,label8,label9,label10,label11,label12 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_beneficio, txb_maquina, txb_id,txb_maxCapacidad,txb_modelMaquina,txb_numContrato,
                                    txb_numSerie,txb_proveedor,txb_proveedorDireccion,txb_proveedorTelefono};
            Button[] buttons = { btn_SaveMaquina, btn_Cancel };

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
