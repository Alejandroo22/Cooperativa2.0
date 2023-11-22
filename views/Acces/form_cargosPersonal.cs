using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using sistema_modular_cafe_majada.controller.SecurityData;
using sistema_modular_cafe_majada.controller.UserDataController;
using sistema_modular_cafe_majada.model.Acces;
using sistema_modular_cafe_majada.model.DAO;
using sistema_modular_cafe_majada.model.UserData;
using sistema_modular_cafe_majada.controller.AccesController;
using sistema_modular_cafe_majada.model.Mapping.Acces;
using sistema_modular_cafe_majada.model.Helpers;

namespace sistema_modular_cafe_majada.views
{
    public partial class form_cargosPersonal : Form
    {
        //variable globar para verificar el estado del boton modificar
        private bool imagenClickeada = false;
        //instancia de la clase
        private Charge cargoSeleccionada;

        public form_cargosPersonal()
        {
            InitializeComponent();

            ShowCargoGrid();

            txb_id.ReadOnly = true;
            txb_id.Enabled = false;

            //coloca nueva mente el contador en el txb del cdigo
            ChargeController ben = new ChargeController();
            var count = ben.CountCargo();
            txb_id.Text = Convert.ToString(count.CountCargo + 1);

            //inicializamos el metodo para las fuentes
            AsignarFuente();

        }

        private void dtg_cargosPersonal_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_cargosPersonal;

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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void ClearDataTxb()
        {
            List<TextBox> txb = new List<TextBox> { txb_cargo, txb_descripCargo };

            foreach (TextBox textBox in txb)
            {
                textBox.Clear();
            }

            imagenClickeada = false;
            cargoSeleccionada = null;

            //coloca nueva mente el contador en el txb del cdigo
            ChargeController ben = new ChargeController();
            var count = ben.CountCargo();
            txb_id.Text = Convert.ToString(count.CountCargo + 1);
        }

        public void ShowCargoGrid()
        {
            ChargeController cargoController = new ChargeController();
            List<Charge> datosCargo = cargoController.ObtenerCargos();

            var infoCargos = datosCargo.Select(cargos => new
            {
                ID = cargos.IdCargo,
                nombre = cargos.NombreCargo,
                descripcion = cargos.DescripcionCargo
            }).ToList();

            dtg_cargosPersonal.DataSource = infoCargos;

            dtg_cargosPersonal.Columns["ID"].HeaderText = "Código de Cargo";
            dtg_cargosPersonal.Columns["nombre"].HeaderText = "Nombre del Cargo";
            dtg_cargosPersonal.Columns["descripcion"].HeaderText = "Descripcion";

            dtg_cargosPersonal.RowHeadersVisible = false;
            dtg_cargosPersonal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dtg_cargosPersonal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_cargosPersonal.Rows.Count)
            {
                Console.WriteLine("depurador - evento click img update: " + imagenClickeada);

                //obtener la fila a la celda que se realizo el evento dobleClick
                DataGridViewRow filaSeleccionada = dtg_cargosPersonal.Rows[e.RowIndex];
                cargoSeleccionada = new Charge();

                cargoSeleccionada.IdCargo = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                cargoSeleccionada.NombreCargo = filaSeleccionada.Cells["nombre"].Value.ToString();
                cargoSeleccionada.DescripcionCargo = filaSeleccionada.Cells["descripcion"].Value.ToString();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_SaveCargo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_cargo.Text))
            {
                MessageBox.Show("El campo Cargo, esta vacio y es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ChargeController cargoController = new ChargeController();
            LogController log = new LogController();
            var userControl = new UserController();
            var usuario = userControl.ObtenerUsuario(UsuarioActual.NombreUsuario);

            /*TextBox[] textBoxes = {txb_nombreFinca };
            ConvertFirstCharacter(textBoxes);*/

            //se obtiene los valores ingresados por el usuario
            string namecargo = txb_cargo.Text;
            string descripcion = txb_descripCargo.Text;
            
            var lastId = cargoController.ObtenrUltimoId();
            bool existe = cargoController.ExisteId(Convert.ToInt32(txb_id.Text));
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

            //Se crea una instancia de la clase Calidades_cafe
            Charge cargo = new Charge()
            {
                IdCargo = Convert.ToInt32(txb_id.Text),
                NombreCargo = namecargo,
                DescripcionCargo = descripcion
            };

            if (!imagenClickeada)
            {
                // Código que se ejecutará si no se ha hecho clic en la imagen update
                // Llamar al controlador para insertar en la base de datos
                bool exito =cargoController.InsertarCargo(cargo);

                if (exito)
                {
                    MessageBox.Show("Cargo agregado correctamente.", "Insercion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        log.RegistrarLog(usuario.IdUsuario, "Registro un Cargo", ModuloActual.NombreModulo, "Insercion", "Inserto un nuevo cargo a la base de datos");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos en el datagrid
                    ShowCargoGrid();
                    //borra los datos de los textbox
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al agregar el cargo. Verifique los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Código que se ejecutará si se ha hecho clic en la imagen update
                bool exito = cargoController.ActualizarCargos(cargoSeleccionada.IdCargo, namecargo, descripcion);

                if (exito)
                {
                    MessageBox.Show("Cargo actualizado correctamente.", "Actualizacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        //verifica el departamento
                        log.RegistrarLog(usuario.IdUsuario, "Actualizo un cargo", ModuloActual.NombreModulo, "Actualizacion", "Actualizo datos con ID " + cargoSeleccionada.IdCargo + " en la base de datos");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                    }

                    //funcion para actualizar los datos
                    ShowCargoGrid();

                    //funcion para limpiar las cajas de texto
                    ClearDataTxb();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la finca, Verifique los datos ingresados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                imagenClickeada = false;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearDataTxb();
            imagenClickeada = false;
            this.Close();
        }

        private void btn_deleteCargo_Click(object sender, EventArgs e)
        {
            //condicion para verificar si lo datos seleccionados son nulos
            if (cargoSeleccionada != null)
            {
                LogController log = new LogController();
                UserController userController = new UserController();
                //asignar el resultado de obtenerusuario
                Usuario usuario = userController.ObtenerUsuario(UsuarioActual.NombreUsuario);
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar el registro seleccionado " + cargoSeleccionada.NombreCargo + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //se llama la funcion delete del controlador
                    ChargeController controller = new ChargeController();
                    controller.EliminarCargos(cargoSeleccionada.IdCargo);

                    //verifica el departamento del log
                    log.RegistrarLog(usuario.IdUsuario, "Eliminacion de Cargo", ModuloActual.NombreModulo, "Eliminacion", "Elimino los datos de cargo " + cargoSeleccionada.NombreCargo + " en la base de datos");

                    if (SqlExceptionHelper.NumberException == 1451 || SqlExceptionHelper.NumberException == 547)
                    {
                        MessageBox.Show(SqlExceptionHelper.MessageExceptionSql, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Cargo Eliminado correctamente", "Eliminacion Satisfactoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    ShowCargoGrid();
                    ClearDataTxb();
                    cargoSeleccionada = null;
                }
                else
                {
                    //muestra un mensaje de erro o excepcion
                    MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_modCargo_Click(object sender, EventArgs e)
        {
            if (cargoSeleccionada != null)
            {
                DialogResult result = MessageBox.Show("¿Desea actualizar el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //si el usuario selecciono "SI"
                    imagenClickeada = true;

                    //se asignanlos registros a los cuadros de texto
                    txb_id.Text = Convert.ToString(cargoSeleccionada.IdCargo);
                    txb_cargo.Text = cargoSeleccionada.NombreCargo;
                    txb_descripCargo.Text = cargoSeleccionada.DescripcionCargo;
                }
            }
            else
            {
                //muestra un mensaje de error o excepción
                MessageBox.Show("No se ha seleccionado correctamente el dato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txb_cargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 45;

            if (txb_cargo.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void txb_descripCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 220;

            if (txb_descripCargo.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada si se alcanza la longitud máxima
            }
        }

        private void AsignarFuente()
        {
            Label[] labels = { label1, label5, label8 };
            Label[] labeltitle = { label9 };
            TextBox[] textBoxes = { txb_cargo, txb_descripCargo, txb_id };
            Button[] buttons = { btn_SaveCargo, btn_Cancel };

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
