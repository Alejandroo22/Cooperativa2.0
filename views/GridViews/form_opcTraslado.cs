using sistema_modular_cafe_majada.controller.InfrastructureController;
using sistema_modular_cafe_majada.controller.OperationsController;
using sistema_modular_cafe_majada.controller.ProductController;
using sistema_modular_cafe_majada.model.Mapping;
using sistema_modular_cafe_majada.model.Mapping.Harvest;
using sistema_modular_cafe_majada.model.Mapping.Infrastructure;
using sistema_modular_cafe_majada.model.Mapping.Operations;
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
    public partial class form_opcTraslado : Form
    {
        public form_opcTraslado()
        {
            InitializeComponent();

            this.KeyPreview = true; // Habilita la captura de eventos de teclado para el formulario

            //auto ajustar el contenido de los datos al área establecido para el datagrid
            dtg_opcTraslado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //
            SearchRegister(txb_buscarOpc);
            txb_buscarOpc.TextChanged += txb_buscarOpc_TextChanged;

            //esta es una llamada para funcion para pintar las filas del datagrid
            dtg_opcTraslado.CellPainting += dtg_opcTraslado_CellPainting;

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //esta es una funcion para pintar las filas del datagrid
        private void dtg_opcTraslado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var configDTG = dtg_opcTraslado;

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

        private void txb_buscarOpc_TextChanged(object sender, EventArgs e)
        {
            SearchRegister(txb_buscarOpc);
        }

        //
        public void ShowProcedenciaGrid()
        {
            //se llama el metodo para obtener los datos de la base de datos
            var procedenciaController = new ProcedenciaDestinoController();
            List<ProcedenciaDestino> datosP = procedenciaController.ObtenerProcedenciasDestinoNombres();

            var datosProcedencia = datosP.Select(proced => new
            {
                ID = proced.IdProcedencia,
                Nombre = proced.NombreProcedencia,
                Descripcion = string.IsNullOrWhiteSpace(proced.DescripcionProcedencia) ? proced.DescripcionProcedencia = "" : proced.DescripcionProcedencia,
                Nombre_Socio = proced.NombreSocioProcedencia ?? "", // Verificar si es NULL y establecer cadena vacía en ese caso
                Nombre_Finca = proced.NombreFincaSocio ?? "",
                Nombre_Beneficio = proced.NombreBenficioUbicacion ?? "",
                Nombre_Maquinaria = proced.NombreMaquinaria ?? ""
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosProcedencia;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //
        public void ShowProcedenciaGrid(TextBox text)
        {
            //se llama el metodo para obtener los datos de la base de datos
            var procedenciaController = new ProcedenciaDestinoController();
            List<ProcedenciaDestino> datosP = procedenciaController.BuscarProcedenciaDestino(text.Text);

            var datosProcedencia = datosP.Select(proced => new
            {
                ID = proced.IdProcedencia,
                Nombre = proced.NombreProcedencia,
                Descripcion = string.IsNullOrWhiteSpace(proced.DescripcionProcedencia) ? proced.DescripcionProcedencia = "" : proced.DescripcionProcedencia,
                Nombre_Socio = proced.NombreSocioProcedencia ?? "", // Verificar si es NULL y establecer cadena vacía en ese caso
                Nombre_Finca = proced.NombreFincaSocio ?? "",
                Nombre_Beneficio = proced.NombreBenficioUbicacion ?? "",
                Nombre_Maquinaria = proced.NombreMaquinaria ?? ""
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosProcedencia;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //
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
            dtg_opcTraslado.DataSource = datosPersonalizados;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void ShowBodegaGrid(TextBox text)
        {
            // Llamar al método para obtener los datos de la base de datos
            BodegaController bodegaController = new BodegaController();
            List<Bodega> datos = bodegaController.BuscarBodegas(text.Text);

            var datosPersonalizados = datos.Select(bodega => new
            {
                ID = bodega.IdBodega,
                Nombre = bodega.NombreBodega,
                Descripcion = bodega.DescripcionBodega,
                Ubicacion = bodega.UbicacionBodega,
                Beneficio = bodega.NombreBenficioUbicacion
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosPersonalizados;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //
        public void ShowPersonalGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            var personalController = new PersonalController();
            List<Personal> datosP = null;

            // Verificar si se obtuvieron datos
            if (PersonalSeleccionado.TipoPersonal.Length != 0)
            {
                datosP = personalController.BuscarPersonalCargo(PersonalSeleccionado.TipoPersonal);
            }
            else
            {
                datosP = personalController.ObtenerPersonalesNombreCargo();
            }

            var datosPersonalizados = datosP.Select(personal => new
            {
                ID = personal.IdPersonal,
                Nombre = personal.NombrePersonal,
                Cargo = personal.NombreCargo,
                Descripcion = personal.Descripcion,
                ID_Persona = personal.IdPersona,
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosPersonalizados;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //
        public void ShowAlmacenGrid()
        {
            // Llamar al método para obtener los datos de la base de datos
            AlmacenController almacenController = new AlmacenController();
            List<Almacen> datos = null;

            Console.WriteLine("Depuracion - id Bodega" + AlmacenBodegaClick.IBodega);
            if(TablaSeleccionadaTraslado.ITable == 2)
            {
                if (AlmacenBodegaClick.IBodega != 0)
                {
                    datos = almacenController.BuscarIDBodegaAlmacenCalidad(AlmacenBodegaClick.IBodega, CalidadSeleccionada.ICalidadSeleccionada);
                }
                else
                {
                    datos = almacenController.ObtenerPorIDAlmacenNombreCalidadBodega(CalidadSeleccionada.ICalidadSeleccionada);
                }
            }
            else if (TablaSeleccionadaTraslado.ITable == 5)
            {
                if (AlmacenBodegaClick.IBodegaDestino != 0)
                {
                    datos = almacenController.BuscarIDBodegaAlmacenCalidad(AlmacenBodegaClick.IBodegaDestino, CalidadSeleccionada.ICalidadSeleccionada);
                }
                else
                {
                    datos = almacenController.ObtenerPorIDAlmacenNombreCalidadBodega(CalidadSeleccionada.ICalidadSeleccionada);
                }
            }
            
            var datosPersonalizados = datos.Select(almacen => new
            {
                ID = almacen.IdAlmacen,
                Nombre = almacen.NombreAlmacen,
                Descripcion = almacen.DescripcionAlmacen,
                Capacidad = almacen.CapacidadAlmacen,
                CantidadQQs = string.IsNullOrWhiteSpace(almacen.CantidadActualAlmacen.ToString()) ? 0.0 : almacen.CantidadActualAlmacen,
                CantidadSaco = string.IsNullOrWhiteSpace(almacen.CantidadActualSacoAlmacen.ToString()) ? 0.0 : almacen.CantidadActualSacoAlmacen,
                Ubicacion = almacen.UbicacionAlmacen,
                Calidad = almacen.NombreCalidadCafe ?? "",
                //SubProducto = almacen.NombreSubProducto ?? "",
                Bodega = almacen.NombreBodegaUbicacion
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosPersonalizados;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void ShowAlmacenGrid(TextBox text)
        {
            // Llamar al método para obtener los datos de la base de datos
            AlmacenController almacenController = new AlmacenController();
            List<Almacen> datos = almacenController.BuscarAlmacens(text.Text);

            var datosPersonalizados = datos.Select(almacen => new
            {
                ID = almacen.IdAlmacen,
                Nombre = almacen.NombreAlmacen,
                Descripcion = almacen.DescripcionAlmacen,
                Capacidad = almacen.CapacidadAlmacen,
                CantidadQQs = string.IsNullOrWhiteSpace(almacen.CantidadActualAlmacen.ToString()) ? 0.0 : almacen.CantidadActualAlmacen,
                CantidadSaco = string.IsNullOrWhiteSpace(almacen.CantidadActualSacoAlmacen.ToString()) ? 0.0 : almacen.CantidadActualSacoAlmacen,
                Ubicacion = almacen.UbicacionAlmacen,
                Calidad = almacen.NombreCalidadCafe ?? "",
                //SubProducto = almacen.NombreSubProducto ?? "",
                Bodega = almacen.NombreBodegaUbicacion
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosPersonalizados;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //
        public void ShowCalidadCGrid()
        {
            //se llama el metodo para obtener los datos de la base de datos
            var calidadesController = new CCafeController();
            List<CalidadCafe> datosCCafe = calidadesController.ObtenerCalidades();

            var datosCalidades = datosCCafe.Select(calidades => new
            {
                ID = calidades.IdCalidad,
                Calidad = calidades.NombreCalidad,
                Descripcion = calidades.DescripcionCalidad
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosCalidades;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void ShowTrasladoGrid()
        {
            //se llama el metodo para obtener los datos de la base de datos
            var trasladoController = new TrasladoController();
            List<Traslado> datosTrl = trasladoController.ObtenerTrasladoPorCosecha(CosechaActual.ICosechaActual);

            var datosTraslado = datosTrl.Select(sub => new
            {
                ID = sub.Idtraslado_cafe,
                Numero_TrasladoCafe = sub.NumTraslado,
                Fecha_Traslado = sub.FechaTrasladoCafe,
                Nombre_FincaProcedencia = sub.NombreProcedencia,
                Nombre_CalidadCafe = sub.NombreCalidadCafe,
                Cantidad_QQs = sub.CantidadTrasladoQQs,
                Nombre_AlmacenProcedencia = sub.NombreAlmacenProcedencia,
                Nombre_BodegaProcedencia = sub.NombreBodegaProcedencia,
                Nombre_AlmacenDestino = sub.NombreAlmacenDestino,
                Nombre_BodegaDestino = sub.NombreBodegaDestino,
                Personal_Secador = sub.NombrePersonal
            }).ToList();

            // Asignar los datos al DataGridView
            dtg_opcTraslado.DataSource = datosTraslado;

            dtg_opcTraslado.RowHeadersVisible = false;
            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        //
        public void SearchRegister(TextBox text)
        {
            int opc = TablaSeleccionadaTraslado.ITable;

            switch (opc)
            {
                case 1:
                    //Traslado
                    {
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowTrasladoGrid();
                        }
                        else
                        {
                            //se llama el metodo para obtener los datos de la base de datos
                            var trasladoController = new TrasladoController();
                            List<Traslado> datosTrl = trasladoController.BuscarTrasladoCafe(text.Text);
                            //se llama el metodo para obtener los datos de la base de datos

                            var datosTraslado = datosTrl.Select(sub => new
                            {
                                ID = sub.Idtraslado_cafe,
                                Numero_TrasladoCafe = sub.NumTraslado,
                                Fecha_Traslado = sub.FechaTrasladoCafe,
                                Nombre_FincaProcedencia = sub.NombreProcedencia,
                                Nombre_CalidadCafe = sub.NombreCalidadCafe,
                                Cantidad_QQs = sub.CantidadTrasladoQQs,
                                Nombre_AlmacenProcedencia = sub.NombreAlmacenProcedencia,
                                Nombre_BodegaProcedencia = sub.NombreBodegaProcedencia,
                                Nombre_AlmacenDestino = sub.NombreAlmacenDestino,
                                Nombre_BodegaDestino = sub.NombreBodegaDestino,
                                Personal_Secador = sub.NombrePersonal
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_opcTraslado.DataSource = datosTraslado;

                            dtg_opcTraslado.RowHeadersVisible = false;
                            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    break;
                case 2:
                    //Almacen Procedencia
                    {
                        //Procedencia
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowAlmacenGrid();
                        }
                        else
                        {
                            ShowAlmacenGrid(text);
                        }
                    }
                    break;
                case 3:
                    //Ubicacion Bodega Procedencia
                    {
                        //Procedencia
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowBodegaGrid();
                        }
                        else
                        {
                            ShowBodegaGrid(text);
                        }
                    }
                    break;
                case 4:
                    //finca Procedencia
                    {
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowProcedenciaGrid();
                        }
                        else
                        {
                            ShowProcedenciaGrid(text);
                        }
                    }
                    break;
                case 5:
                    //Almacen Destino
                    {
                        //Procedencia
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowAlmacenGrid();
                        }
                        else
                        {
                            ShowAlmacenGrid(text);
                        }
                    }
                    break;
                case 6:
                    //Ubicacion Bodega Destino
                    {
                        //Procedencia
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowBodegaGrid();
                        }
                        else
                        {
                            ShowBodegaGrid(text);
                        }
                    }
                    break;
                case 7:
                    //finca Destino
                    {
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowProcedenciaGrid();
                        }
                        else
                        {
                            ShowProcedenciaGrid(text);
                        }
                    }
                    break;
                case 8:
                    //Calidad Cafe
                    {
                        //Procedencia
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowCalidadCGrid();
                        }
                        else
                        {
                            //se llama el metodo para obtener los datos de la base de datos
                            var calidadesController = new CCafeController();
                            List<CalidadCafe> datosCCafe = calidadesController.BuscarCalidades(text.Text);

                            var datosCalidades = datosCCafe.Select(calidades => new
                            {
                                ID = calidades.IdCalidad,
                                Calidad = calidades.NombreCalidad,
                                Descripcion = calidades.DescripcionCalidad
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_opcTraslado.DataSource = datosCalidades;

                            dtg_opcTraslado.RowHeadersVisible = false;
                            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    break;
                case 9:
                    //Pesador
                    {
                        //Procedencia
                        if (string.IsNullOrWhiteSpace(text.Text) || text.Text == "Buscar...")
                        {
                            //funcion para mostrar de inicio los datos en el dataGrid
                            ShowPersonalGrid();
                        }
                        else
                        {
                            // Llamar al método para obtener los datos de la base de datos
                            var personalController = new PersonalController();
                            List<Personal> datos = personalController.BuscarPersonal(text.Text);

                            var datosPersonalizados = datos.Select(personal => new
                            {
                                ID = personal.IdPersonal,
                                Nombre = personal.NombrePersonal,
                                Cargo = personal.NombreCargo,
                                Descripcion = personal.Descripcion,
                                ID_Persona = personal.IdPersona,
                            }).ToList();

                            // Asignar los datos al DataGridView
                            dtg_opcTraslado.DataSource = datosPersonalizados;

                            dtg_opcTraslado.RowHeadersVisible = false;
                            dtg_opcTraslado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Ocurrio un Error. La tabla que desea acceder no exite. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        //
        private void txb_buscarOpc_Enter(object sender, EventArgs e)
        {
            if (txb_buscarOpc.Text == "Buscar...")
            {
                txb_buscarOpc.Text = string.Empty;
                txb_buscarOpc.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txb_buscarOpc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txb_buscarOpc.Text))
            {
                txb_buscarOpc.Text = "Buscar...";
                txb_buscarOpc.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void form_opcTraslado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Cierra el formulario actual
            }
        }

        private void dtg_opcTraslado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido (mayor o igual a 0 y dentro del rango de filas con datos)
            if (e.RowIndex >= 0 && e.RowIndex < dtg_opcTraslado.Rows.Count)
            {
                // Obtener la fila correspondiente a la celda en la que se hizo doble clic
                DataGridViewRow filaSeleccionada = dtg_opcTraslado.Rows[e.RowIndex];

                int opc = TablaSeleccionadaTraslado.ITable;

                switch (opc)
                {
                    case 1:
                        //Traslado
                        {
                            TrasladoSeleccionado.clickImg = true;
                            // Obtener los valores de las celdas de la fila seleccionada
                            TrasladoSeleccionado.ITraslado = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            TrasladoSeleccionado.NumTraslado = Convert.ToInt32(filaSeleccionada.Cells["Numero_TrasladoCafe"].Value);
                        }
                        break;
                    case 2:
                        //Almacen Procedencia
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            AlmacenSeleccionado.IAlmacen = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            AlmacenSeleccionado.NombreAlmacen = filaSeleccionada.Cells["Nombre"].Value.ToString();
                        }
                        break;
                    case 3:
                        //Ubicacion Bodega Procedencia
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            BodegaSeleccionada.IdBodega = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            BodegaSeleccionada.NombreBodega = filaSeleccionada.Cells["Nombre"].Value.ToString();
                        }
                        break;
                    case 4:
                        //Procedencia Procedencia
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            ProcedenciaSeleccionada.IProcedencia = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            ProcedenciaSeleccionada.NombreProcedencia = filaSeleccionada.Cells["Nombre"].Value.ToString();
                        }
                        break;
                    case 5:
                        //Almacen Destino
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            AlmacenSeleccionado.IAlmacenDestino = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            AlmacenSeleccionado.NombreAlmacenDestino = filaSeleccionada.Cells["Nombre"].Value.ToString();
                        }
                        break;
                    case 6:
                        //Ubicacion Bodega Destino
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            BodegaSeleccionada.IdBodegaDestino = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            BodegaSeleccionada.NombreBodegaDestino = filaSeleccionada.Cells["Nombre"].Value.ToString();
                        }
                        break;
                    case 7:
                        //Procedencia Destino
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            ProcedenciaSeleccionada.IProcedenciaDestino = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            ProcedenciaSeleccionada.NombreProcedenciaDestino = filaSeleccionada.Cells["Nombre"].Value.ToString();
                        }
                        break;
                    case 8:
                        //Calidad Cafe
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            CalidadSeleccionada.ICalidadSeleccionada = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            CalidadSeleccionada.NombreCalidadSeleccionada = filaSeleccionada.Cells["Calidad"].Value.ToString();
                        }
                        break;
                    case 9:
                        //Pesador
                        {
                            // Obtener los valores de las celdas de la fila seleccionada
                            PersonalSeleccionado.IPersonalPesador = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                            PersonalSeleccionado.NombrePersonalPesador = filaSeleccionada.Cells["Nombre"].Value.ToString();
                        }
                        break;
                    default:
                        MessageBox.Show("Ocurrio un Error. La tabla que desea acceder no exite. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // El índice de fila no es válido, se muestra un mensaje para evitar realizar la acción de error.
                MessageBox.Show("Seleccione una fila válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

}
