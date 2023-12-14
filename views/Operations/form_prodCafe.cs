using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.Helpers;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
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
    public partial class form_prodCafe : Form
    {
        //variable global para verificar el estado del boton actualizar
        private bool imagenClickeada = false;
        //instancia de la clase mapeo beneficio para capturar los datos seleccionado por el usuario
        private ProcedenciaDestino proceSeleccionado;

        int ibeneficio;
        int imaquina;
        int isocio;

        public form_prodCafe()
        {
            InitializeComponent();
            
            cbx_beneficio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_maquinaria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_socio.DropDownStyle = ComboBoxStyle.DropDownList;
            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            ProcedenciaDestinoController proceC = new ProcedenciaDestinoController();
            var count = proceC.CountProcedencia();
            txb_id.Text = Convert.ToString(count.CountProcedencia + 1);

            //
            CbxBeneficio();
            CbxSocio();
            CbxMaquinaria();

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_proceCafe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowProcedenciaGrid();

            AsignarFuente();
        }

        private void dtg_proceCafe_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_proceCafe;

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

        public void ShowProcedenciaGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var proceController = new ProcedenciaDestinoController();
            List<ProcedenciaDestino> datos = proceController.ObtenerProcedenciasDestinoNombres();

            var datosPersonalizados = datos.Select(pro => new
            {
                ID = pro.IdProcedencia,
                Nombre = pro.NombreProcedencia,
                Descripcion = pro.DescripcionProcedencia,
                NombreBeneficio = pro.NombreBenficioUbicacion,
                NombreSocio = pro.NombreSocioProcedencia,
                Maquinaria = pro.NombreMaquinaria
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_proceCafe.DataSource = datosPersonalizados;

            dtg_proceCafe.RowHeadersVisible = false;
            dtg_proceCafe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void CbxBeneficio()
        {
            BeneficioController beneficio = new BeneficioController();
            List<Beneficio> datoBeneficio = beneficio.ObtenerBeneficios();

            cbx_beneficio.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (Beneficio benef in datoBeneficio)
            {
                int idBeneficio = benef.IdBeneficio;
                string nombreBeneficio = benef.NombreBeneficio;

                cbx_beneficio.Items.Add(new KeyValuePair<int, string>(idBeneficio, nombreBeneficio));
            }
        }

        public void CbxSocio()
        {
            SocioController socioC = new SocioController();
            List<Socio> dato = socioC.ObtenerSocios();

            cbx_socio.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (Socio soc in dato)
            {
                int idSocio = soc.IdSocio;
                string nombreSocio = soc.NombreSocio;

                cbx_socio.Items.Add(new KeyValuePair<int, string>(idSocio, nombreSocio));
            }
        }
        
        public void CbxMaquinaria()
        {
            MaquinariaController maquina = new MaquinariaController();
            List<Maquinaria> dato = maquina.ObtenerMaquinaria();

            cbx_maquinaria.Items.Clear();

            // Asignar los valores numéricos a los elementos del ComboBox
            foreach (Maquinaria maq  in dato)
            {
                int idMaquina = maq.IdMaquinaria;
                string nombreMaquina = maq.NombreMaquinaria;

                cbx_maquinaria.Items.Add(new KeyValuePair<int, string>(idMaquina, nombreMaquina));
            }
        }

        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_descripcion, txb_procedCafe };

            foreach (TextBox textBox in txb)
            {
                textBox.Text = "";
            }

            proceSeleccionado = null;
            imagenClickeada = false;

            cbx_beneficio.SelectedIndex = -1;
            cbx_maquinaria.SelectedIndex = -1;
            cbx_socio.SelectedIndex = -1;

            //coloca nueva mente el contador en el txb del cdigo
            ProcedenciaDestinoController proceC = new ProcedenciaDestinoController();
            var count = proceC.CountProcedencia();
            txb_id.Text = Convert.ToString(count.CountProcedencia + 1);
            
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

        private void btn_updateProcedencia_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (proceSeleccionado != null)
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Console.WriteLine("id prcedencia " + ProcedenciaSeleccionada.IProcedencia);
                    // El usuario seleccionó "Sí"
                    imagenClickeada = true;
                    ProcedenciaDestinoController proceCt = new ProcedenciaDestinoController();

                    var name = proceCt.ObtenerProcedenciaDestinoPorId(ProcedenciaSeleccionada.IProcedencia);

                    // Asignar los valores a los cuadros de texto solo si no se ha hecho clic en la imagen
                    txb_id.Text = Convert.ToString(proceSeleccionado.IdProcedencia);
                    txb_procedCafe.Text = proceSeleccionado.NombreProcedencia;
                    txb_descripcion.Text = proceSeleccionado.DescripcionProcedencia;

                    txb_id.Enabled = true;
                    txb_id.ReadOnly = false;

                    //cbxBeneficio
                    cbx_beneficio.Items.Clear();
                    CbxBeneficio();

                    //cbxMaquinaria
                    cbx_maquinaria.Items.Clear();
                    CbxMaquinaria();

                    //cbxSocio
                    cbx_socio.Items.Clear();
                    CbxSocio();
                
                    if (!string.IsNullOrWhiteSpace(proceSeleccionado.NombreSocioProcedencia))
                    {
                        SocioController socC = new SocioController();
                        var soc = socC.ObtenerSocioNombre(proceSeleccionado.NombreSocioProcedencia);
                        isocio = soc.IdSocio;
                        int iso = isocio - 1;
                        
                        cbx_socio.SelectedIndex = iso;
                    }
                    if (!string.IsNullOrWhiteSpace(proceSeleccionado.NombreMaquinaria))
                    {
                        MaquinariaController mac = new MaquinariaController();
                        var ma = mac.ObtenerNombreMaquinaria(proceSeleccionado.NombreMaquinaria);
                        imaquina = Convert.ToInt32(ma.IdMaquinaria);
                        int ima = imaquina - 1;
                        cbx_maquinaria.SelectedIndex = ima;
                    }
                    if (!string.IsNullOrWhiteSpace(proceSeleccionado.NombreBenficioUbicacion))
                    {
                        BeneficioController benC = new BeneficioController();
                        var ben = benC.ObtenerBeneficioNombre(proceSeleccionado.NombreBenficioUbicacion);
                        ibeneficio = ben.IdBeneficio;
                        int ibg = ibeneficio - 1;
                        cbx_beneficio.SelectedIndex = ibg;
                    }

                }
                else
                {
                    // El usuario seleccionó "No" o cerró el cuadro de diálogo
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas de la Procedencia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dtg_proceCafe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_proceCafe.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_proceCafe.Rows[e.RowIndex];
                proceSeleccionado = new ProcedenciaDestino();

                // Obtener los valores de las celdas de la fila seleccionada
                proceSeleccionado.IdProcedencia = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                ProcedenciaSeleccionada.IProcedencia = proceSeleccionado.IdProcedencia;
                proceSeleccionado.NombreProcedencia = filaSeleccionada.Cells["Nombre"].Value.ToString();
                proceSeleccionado.DescripcionProcedencia = filaSeleccionada.Cells["Descripcion"].Value.ToString();
                proceSeleccionado.NombreBenficioUbicacion = filaSeleccionada.Cells["NombreBeneficio"].Value.ToString();
                proceSeleccionado.NombreSocioProcedencia = filaSeleccionada.Cells["NombreSocio"].Value.ToString();
                Console.WriteLine("nombre socio " + proceSeleccionado.NombreSocioProcedencia);
                proceSeleccionado.NombreMaquinaria = filaSeleccionada.Cells["Maquinaria"].Value.ToString();
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

        private void btn_deletePorceCafe_Click(object sender, EventArgs e)
        {
            //condicion para verificar si los datos seleccionados van nulos, para evitar error
            if (proceSeleccionado != null)
            {
                LogController log = new LogController();
                UserController userControl = new UserController();
                Usuario usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar los datos registrado de la Procedencia: (" + proceSeleccionado.NombreProcedencia + ") ?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador para eliminar el registro
                    ProcedenciaDestinoController controller = new ProcedenciaDestinoController();
                    controller.EliminarProcedenciaDestino(proceSeleccionado.IdProcedencia);

                    //verificar el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de los datos Procedencia", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos de la Procedencia (" + proceSeleccionado.NombreProcedencia + ") en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Procedencia Eliminada correctamente.", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //se actualiza la tabla
                    ShowProcedenciaGrid();
                    ClearDataTxb();
                }
            }
            else
            {
                // Mostrar un mensaje de error o lanzar una excepción
                MessageBox.Show("No se ha seleccionado correctamente las caracteristicas de la Procedencia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_SaveProceCafe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_procedCafe.Text))
            {
                MessageBox.Show("El campo Procedencia, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ProcedenciaDestinoController proceController = new ProcedenciaDestinoController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario); // Asignar el resultado de ObtenerUsuario

            TextBox[] textBoxes = { txb_descripcion };
            TextBox[] textBoxesM = { txb_procedCafe };
            ConvertFirstCharacter(textBoxes);
            ConvertAllUppercase(textBoxesM);

            try
            {
                string name = txb_procedCafe.Text;
                string descripcion = txb_descripcion.Text;

                // Obtener el valor numérico seleccionado
                //beneficio
                KeyValuePair<int, string> selectedStatusB = new KeyValuePair<int, string>();
                if (cbx_beneficio.SelectedItem is KeyValuePair<int, string> keyValue)
                {
                    selectedStatusB = keyValue;
                }
                else if (cbx_beneficio.SelectedItem != null)
                {
                    selectedStatusB = (KeyValuePair<int, string>)cbx_beneficio.SelectedItem;
                }
                //maquinaria
                KeyValuePair<int, string> selectedStatusM = new KeyValuePair<int, string>();
                if (cbx_maquinaria.SelectedItem is KeyValuePair<int, string> keyValueM)
                {
                    selectedStatusM = keyValueM;
                }
                else if (cbx_maquinaria.SelectedItem != null)
                {
                    selectedStatusM = (KeyValuePair<int, string>)cbx_maquinaria.SelectedItem;
                }
                //socio
                KeyValuePair<int, string> selectedStatusS = new KeyValuePair<int, string>();
                if (cbx_socio.SelectedItem is KeyValuePair<int, string> keyValueS)
                {
                    selectedStatusS = keyValueS;
                }
                else if (cbx_socio.SelectedItem != null)
                {
                    selectedStatusS = (KeyValuePair<int, string>)cbx_socio.SelectedItem;
                }

                int selectedValueB = selectedStatusB.Key;
                int selectedValueM = selectedStatusM.Key;
                int selectedValueS = selectedStatusS.Key;

                var lastId = proceController.ObtenerUltimoId();
                bool existe = proceController.ExisteId(Convert.ToInt32(txb_id.Text));
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
                ProcedenciaDestino ProcedenciaInsert = new ProcedenciaDestino()
                {
                    IdProcedencia = Convert.ToInt32(txb_id.Text),
                    NombreProcedencia = name,
                    DescripcionProcedencia = descripcion,
                    IdBenficioUbicacion = selectedValueB,
                    IdMaquinaria = selectedValueM,
                    IdSocioProcedencia = selectedValueS,
                };

                if (!imagenClickeada)
                {
                    // Código que se ejecutará si no se ha hecho clic en la imagen update
                    // Llamar al controlador para insertar la Procedencia en la base de datos
                    bool exito = proceController.InsertarProcedenciaDestino(ProcedenciaInsert);

                    if (!exito)
                    {
                        MessageBox.Show("Error al agregar la Procedencia. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Procedencia agregado correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verificar el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Registro de caracteristicas de la Procedencia", ModuloActual.NombreModulo, "Insercion", "Inserto una nueva Procedencia a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowProcedenciaGrid();

                    //borrar datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    // Crear una instancia de la clase Beneficio con los valores obtenidos
                    ProcedenciaDestino ProcedenciaUpdate = new ProcedenciaDestino()
                    {
                        IdProcedencia = proceSeleccionado.IdProcedencia,
                        NombreProcedencia = name,
                        DescripcionProcedencia = descripcion,
                        IdBenficioUbicacion = selectedValueB,
                        IdMaquinaria = selectedValueM,
                        IdSocioProcedencia = selectedValueS,
                    };

                    // Código que se ejecutará si se ha hecho clic en la imagen update
                    bool exito = proceController.ActualizarProcedenciaDestino(ProcedenciaUpdate);

                    if (!exito)
                    {
                        MessageBox.Show("Error al actualizar los datos de la Procedencia. Verifica los datos e intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Procedencia actualizado correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verificar el departamento 
                        log.RegistrarLog(usuario.IdUsuario, "Actualizacion del dato Procedencia", ModuloActual.NombreModulo, "Actualizacion", "Actualizo las caracteristicas de la Procedencia con ID " + proceSeleccionado.IdProcedencia + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el dataGrid
                    ShowProcedenciaGrid();

                    ClearDataTxb();

                    imagenClickeada = false;
                    proceSeleccionado = null;

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

        private void txb_procedCafe_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 95;

            if (txb_procedCafe.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
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

        private void AsignarFuente()
        {
            Label[] labels = { label2,label3,label4, label5,label6,label7, label8 };
            Label[] labeltitle = { label1 };
            TextBox[] textBoxes = { txb_descripcion, txb_procedCafe, txb_id };
            Button[] buttons = { btn_SaveProceCafe, btn_Cancel };
            ComboBox[] comboBoxes = { cbx_beneficio,cbx_maquinaria,cbx_socio };

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
