using sistema_modular_cafe_majada.controller;
using sistema_modular_cafe_majada.controller.HarvestController;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Mapping.Operations;
using sistema_modular_cafe_majada.model.Mapping.Product;
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
    public partial class form_socios : Form
    {
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        //instancia de la clase mapeo Socio para capturar los datos seleccionado por el usuario
        private Socio socioSeleccionado;
        
        form_opcLote form_Opc;

        public form_socios()
        {
            InitializeComponent();

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_socios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowSociosGrid();

            dtg_socios.CellPainting += dtgv_socios_CellPainting;

            //
            txb_nombreFinca.ReadOnly = true;
            txb_nombreFinca.Enabled = false;
            txb_nombrePersona.ReadOnly = true;
            txb_nombrePersona.Enabled = false;
            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            //coloca nueva mente el contador en el txb del cdigo
            SocioController cal = new SocioController();
            var count = cal.CountSocio();
            txb_id.Text = Convert.ToString(count.CountSocio + 1);

            AsignarFuente();

        }

        private void dtgv_socios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_socios;

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

        public void ShowSociosGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var socioController = new SocioController();
            List<Socio> datos = socioController.ObtenerSocioNombrePersona();

            var datosPersonalizados = datos.Select(soc => new
            {
                ID = soc.IdSocio,
                Nombre = soc.NombreSocio,
                Descripcion = soc.DescripcionSocio,
                Ubicacion = soc.UbicacionSocio,
                Nombre_Persona = soc.NombrePersonaResp,
                Nombre_Finca = soc.NombreFinca
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_socios.DataSource = datosPersonalizados;

            dtg_socios.RowHeadersVisible = false;
            dtg_socios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //
        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_nombre, txb_descripcion, txb_nombrePersona, txb_ubicacion, txb_nombreFinca  };

            foreach (TextBox textBox in txb)
            {
                textBox.Text = "";
            }

            //coloca nueva mente el contador en el txb del cdigo
            SocioController cal = new SocioController();
            var count = cal.CountSocio();
            txb_id.Text = Convert.ToString(count.CountSocio + 1);
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
        private void btn_updateLote_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (socioSeleccionado != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    txb_id.Text = Convert.ToString(socioSeleccionado.IdSocio);
                    txb_nombre.Text = socioSeleccionado.NombreSocio;
                    txb_descripcion.Text = socioSeleccionado.DescripcionSocio;
                    txb_ubicacion.Text = socioSeleccionado.UbicacionSocio;
                    txb_nombrePersona.Text = socioSeleccionado.NombrePersonaResp;
                    txb_nombreFinca.Text = socioSeleccionado.NombreFinca;
                    Console.WriteLine("id finca " + FincaSeleccionada.IFincaSeleccionada);
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
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        private void btn_deleteLote_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (socioSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar los datos registrado del Socio: (" + socioSeleccionado.NombreSocio + ") ?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    SocioController controller = new SocioController();
                    controller.EliminarSocio(socioSeleccionado.IdSocio);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminado los datos Socio", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos del Socio (" + socioSeleccionado.NombreSocio + ") en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Socio Eliminado correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowSociosGrid();
                    socioSeleccionado = null;
                    ClearDataTxb();
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas del Lote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            imagenClickeada = false;
            this.Close();
        }

        private void btn_tFinca_Click(object sender, EventArgs e)
        {
            TablaSeleccionada.ITable = 1;
            form_Opc = new form_opcLote();

            if (form_Opc.ShowDialog() == DialogResult.OK)
            {
                txb_nombreFinca.Text = FincaSeleccionada.NombreFincaSeleccionada;
            }
        }

        private void btn_tPersona_Click(object sender, EventArgs e)
        {
            TablaSeleccionada.ITable = 2;
            form_Opc = new form_opcLote();

            if (form_Opc.ShowDialog() == DialogResult.OK)
            {
                txb_nombrePersona.Text = PersonSelect.NamePerson;
            }
        }

        private void dtg_lotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_socios.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_socios.Rows[e.RowIndex];
                socioSeleccionado = new Socio();

                // Obtener los valores de las celdas de la fila seleccionada
                socioSeleccionado.IdSocio = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                socioSeleccionado.NombreSocio = filaSeleccionada.Cells["Nombre"].Value.ToString();
                socioSeleccionado.DescripcionSocio = filaSeleccionada.Cells["Descripcion"].Value.ToString();
                socioSeleccionado.UbicacionSocio = filaSeleccionada.Cells["Ubicacion"].Value.ToString();
                socioSeleccionado.NombrePersonaResp = filaSeleccionada.Cells["Nombre_Persona"].Value.ToString();
                socioSeleccionado.NombreFinca = filaSeleccionada.Cells["Nombre_Finca"].Value.ToString();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_SaveLote_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_nombre.Text))
            {
                MessageBox.Show("El campo Nombre, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txb_ubicacion.Text))
            {
                DialogResult result = MessageBox.Show("El campo Ubicacion, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            
            if (string.IsNullOrWhiteSpace(txb_nombrePersona.Text))
            {
                MessageBox.Show("El campo Nombre Socio, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (string.IsNullOrWhiteSpace(txb_nombreFinca.Text))
            {
                DialogResult result = MessageBox.Show("Verifique si el Socio no tiene una Finca asociada a él. Si no tienen finca asociada precione NO y continue con la inserción", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    return;
                }
            }

            SocioController socioController = new SocioController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario

            TextBox[] textBoxes = { txb_ubicacion, txb_nombre };
            ConvertFirstCharacter(textBoxes);
            //TextBox[] textBoxesM = {  };
            //ConvertAllUppercase(textBoxesM);
            TextBox[] textBoxesLetter = { txb_descripcion };
            ConvertFirstLetter(textBoxesLetter);

            try
            {
                string name = txb_nombre.Text;
                string descripcion = txb_descripcion.Text;
                string ubicacion = txb_ubicacion.Text;
                string finca = txb_nombreFinca.Text;
                string persona = txb_nombrePersona.Text;

                var lastId = socioController.ObtenerUltimoId();
                bool existe = socioController.ExisteId(Convert.ToInt32(txb_id.Text));
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

                // Crear una instancia de la clase Socio con los valores obtenidos
                Socio socioInsert = new Socio()
                {
                    NombreSocio = name,
                    DescripcionSocio = descripcion,
                    UbicacionSocio = ubicacion,
                    IdPersonaRespSocio = PersonSelect.IdPerson,
                    IdFincaSocio = FincaSeleccionada.IFincaSeleccionada
                };

                if (!imagenClickeada)
                {
                    // Código que se ejecutará si no se ha hecho clic en la imagen update
                    // Llamar al controlador para insertar el Socio en la base de datos
                    bool exito = socioController.InsertarSocio(socioInsert);

                    if (!exito)
                    {
                        MessageBox.Show("Error al agregar el Socio. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Socio agregado correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        log.RegistrarLog(usuario.IdUsuario, "Registro de caracteristicas del Socio", ModuloActual.NombreModulo, "Insercion", "Inserto un nuevo Socio a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowSociosGrid();

                    //borrar datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    var personaC = new PersonController();
                    FincaController fincaC = new FincaController();
                    
                    Persona pers = new Persona();
                    Finca fin = new Finca();
                    pers = personaC.ObtenerPersona(persona);

                    if (!string.IsNullOrEmpty(finca))
                    {
                        fin = fincaC.ObtenerNombreFincas(finca);
                        // Código que se ejecutará si se ha hecho clic en la imagen update
                        bool exito= socioController.ActualizarSocio(socioSeleccionado.IdSocio, name, descripcion, ubicacion,pers.IdPersona, fin.IdFinca);
                        if (!exito)
                        {
                            MessageBox.Show("Error al actualizar los datos del Socio. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Código que se ejecutará si se ha hecho clic en la imagen update
                        bool exito = socioController.ActualizarSocio(socioSeleccionado.IdSocio, name, descripcion, ubicacion, pers.IdPersona, 0);
                        if (!exito)
                        {
                            MessageBox.Show("Error al actualizar los datos del Socio. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
  

                    MessageBox.Show("Socio actualizado correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verificar el departamento 
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Socio", ModuloActual.NombreModulo, "Actualizacion", "Actualizo las caracteristicas del Socio con ID " + socioSeleccionado.IdSocio + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowSociosGrid();

                    ClearDataTxb();

                    imagenClickeada = false;
                    socioSeleccionado = null;

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

        private void txb_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 95;

            if (txb_nombre.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
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
            TextBox[] textBoxes = { txb_descripcion, txb_nombre, txb_id,txb_nombreFinca,txb_nombrePersona,txb_ubicacion };
            Button[] buttons = { btn_SaveLote, btn_Cancel };

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
