
namespace sistema_modular_cafe_majada.views
{
    partial class form_trillaCafe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_trillaCafe));
            this.btn_tTrillas = new System.Windows.Forms.Button();
            this.txb_numTrilla = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_cosecha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_subProducto = new System.Windows.Forms.RadioButton();
            this.rb_cafeTrilla = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbx_subProducto = new System.Windows.Forms.ComboBox();
            this.btn_tCCafe = new System.Windows.Forms.Button();
            this.txb_pesoQQs = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txb_pesoSaco = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txb_calidadCafe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_tProcedencia = new System.Windows.Forms.Button();
            this.btn_tUbicacion = new System.Windows.Forms.Button();
            this.btn_tAlmacen = new System.Windows.Forms.Button();
            this.txb_bodega = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txb_finca = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txb_almacen = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_tPesador = new System.Windows.Forms.Button();
            this.txb_observacion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txb_personal = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtp_fechaTrilla = new System.Windows.Forms.DateTimePicker();
            this.btn_pdfTrilla = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_SaveTrilla = new System.Windows.Forms.Button();
            this.btn_deleteTrilla = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_tTrillas
            // 
            this.btn_tTrillas.FlatAppearance.BorderSize = 0;
            this.btn_tTrillas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tTrillas.Image = global::sistema_modular_cafe_majada.Properties.Resources.editar_24px;
            this.btn_tTrillas.Location = new System.Drawing.Point(211, 27);
            this.btn_tTrillas.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tTrillas.Name = "btn_tTrillas";
            this.btn_tTrillas.Size = new System.Drawing.Size(26, 28);
            this.btn_tTrillas.TabIndex = 2;
            this.btn_tTrillas.UseVisualStyleBackColor = true;
            this.btn_tTrillas.Click += new System.EventHandler(this.btn_tTrillas_Click);
            // 
            // txb_numTrilla
            // 
            this.txb_numTrilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_numTrilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_numTrilla.Location = new System.Drawing.Point(118, 32);
            this.txb_numTrilla.Margin = new System.Windows.Forms.Padding(2);
            this.txb_numTrilla.Name = "txb_numTrilla";
            this.txb_numTrilla.Size = new System.Drawing.Size(88, 21);
            this.txb_numTrilla.TabIndex = 1;
            this.txb_numTrilla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_numTrilla_KeyDown);
            this.txb_numTrilla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_numTrilla_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Oswald", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(115, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.TabIndex = 21;
            this.label2.Text = "N° de Trillado";
            // 
            // txb_cosecha
            // 
            this.txb_cosecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_cosecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_cosecha.Location = new System.Drawing.Point(13, 32);
            this.txb_cosecha.Margin = new System.Windows.Forms.Padding(2);
            this.txb_cosecha.Name = "txb_cosecha";
            this.txb_cosecha.Size = new System.Drawing.Size(88, 21);
            this.txb_cosecha.TabIndex = 0;
            this.txb_cosecha.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Oswald", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "Cosecha";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_subProducto);
            this.groupBox1.Controls.Add(this.rb_cafeTrilla);
            this.groupBox1.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 72);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(458, 77);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Movimiento";
            // 
            // rb_subProducto
            // 
            this.rb_subProducto.AutoSize = true;
            this.rb_subProducto.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_subProducto.Location = new System.Drawing.Point(217, 33);
            this.rb_subProducto.Margin = new System.Windows.Forms.Padding(2);
            this.rb_subProducto.Name = "rb_subProducto";
            this.rb_subProducto.Size = new System.Drawing.Size(140, 28);
            this.rb_subProducto.TabIndex = 7;
            this.rb_subProducto.TabStop = true;
            this.rb_subProducto.Text = "SubProductos de Trilla";
            this.rb_subProducto.UseVisualStyleBackColor = true;
            // 
            // rb_cafeTrilla
            // 
            this.rb_cafeTrilla.AutoSize = true;
            this.rb_cafeTrilla.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_cafeTrilla.Location = new System.Drawing.Point(106, 33);
            this.rb_cafeTrilla.Margin = new System.Windows.Forms.Padding(2);
            this.rb_cafeTrilla.Name = "rb_cafeTrilla";
            this.rb_cafeTrilla.Size = new System.Drawing.Size(92, 28);
            this.rb_cafeTrilla.TabIndex = 6;
            this.rb_cafeTrilla.TabStop = true;
            this.rb_cafeTrilla.Text = "Café a Trillar";
            this.rb_cafeTrilla.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbx_subProducto);
            this.groupBox2.Controls.Add(this.btn_tCCafe);
            this.groupBox2.Controls.Add(this.txb_pesoQQs);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txb_pesoSaco);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txb_calidadCafe);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(9, 162);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(458, 148);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos del Producto";
            // 
            // cbx_subProducto
            // 
            this.cbx_subProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_subProducto.FormattingEnabled = true;
            this.cbx_subProducto.Location = new System.Drawing.Point(315, 35);
            this.cbx_subProducto.Margin = new System.Windows.Forms.Padding(2);
            this.cbx_subProducto.Name = "cbx_subProducto";
            this.cbx_subProducto.Size = new System.Drawing.Size(129, 23);
            this.cbx_subProducto.TabIndex = 10;
            // 
            // btn_tCCafe
            // 
            this.btn_tCCafe.FlatAppearance.BorderSize = 0;
            this.btn_tCCafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tCCafe.Image = ((System.Drawing.Image)(resources.GetObject("btn_tCCafe.Image")));
            this.btn_tCCafe.Location = new System.Drawing.Point(208, 35);
            this.btn_tCCafe.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tCCafe.Name = "btn_tCCafe";
            this.btn_tCCafe.Size = new System.Drawing.Size(26, 28);
            this.btn_tCCafe.TabIndex = 9;
            this.btn_tCCafe.UseVisualStyleBackColor = true;
            this.btn_tCCafe.Click += new System.EventHandler(this.btn_tCCafe_Click);
            // 
            // txb_pesoQQs
            // 
            this.txb_pesoQQs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_pesoQQs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_pesoQQs.Location = new System.Drawing.Point(315, 100);
            this.txb_pesoQQs.Margin = new System.Windows.Forms.Padding(2);
            this.txb_pesoQQs.Name = "txb_pesoQQs";
            this.txb_pesoQQs.Size = new System.Drawing.Size(129, 23);
            this.txb_pesoQQs.TabIndex = 12;
            this.txb_pesoQQs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_pesoQQs_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(277, 102);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 24);
            this.label7.TabIndex = 6;
            this.label7.Text = "Q.Q.S";
            // 
            // txb_pesoSaco
            // 
            this.txb_pesoSaco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_pesoSaco.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_pesoSaco.Location = new System.Drawing.Point(98, 101);
            this.txb_pesoSaco.Margin = new System.Windows.Forms.Padding(2);
            this.txb_pesoSaco.Name = "txb_pesoSaco";
            this.txb_pesoSaco.Size = new System.Drawing.Size(129, 23);
            this.txb_pesoSaco.TabIndex = 11;
            this.txb_pesoSaco.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_pesoSaco_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 24);
            this.label6.TabIndex = 4;
            this.label6.Text = "Sacos/Pesas";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(243, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "SubProducto";
            // 
            // txb_calidadCafe
            // 
            this.txb_calidadCafe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_calidadCafe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_calidadCafe.Location = new System.Drawing.Point(98, 35);
            this.txb_calidadCafe.Margin = new System.Windows.Forms.Padding(2);
            this.txb_calidadCafe.Name = "txb_calidadCafe";
            this.txb_calidadCafe.Size = new System.Drawing.Size(106, 23);
            this.txb_calidadCafe.TabIndex = 0;
            this.txb_calidadCafe.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "Calidad de Café";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btn_tProcedencia);
            this.groupBox3.Controls.Add(this.btn_tUbicacion);
            this.groupBox3.Controls.Add(this.btn_tAlmacen);
            this.groupBox3.Controls.Add(this.txb_bodega);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txb_finca);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txb_almacen);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(494, 72);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(458, 238);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Procedencia del Café";
            // 
            // btn_tProcedencia
            // 
            this.btn_tProcedencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_tProcedencia.FlatAppearance.BorderSize = 0;
            this.btn_tProcedencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tProcedencia.Image = ((System.Drawing.Image)(resources.GetObject("btn_tProcedencia.Image")));
            this.btn_tProcedencia.Location = new System.Drawing.Point(427, 165);
            this.btn_tProcedencia.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tProcedencia.Name = "btn_tProcedencia";
            this.btn_tProcedencia.Size = new System.Drawing.Size(26, 28);
            this.btn_tProcedencia.TabIndex = 16;
            this.btn_tProcedencia.UseVisualStyleBackColor = true;
            this.btn_tProcedencia.Click += new System.EventHandler(this.btn_tProcedencia_Click);
            // 
            // btn_tUbicacion
            // 
            this.btn_tUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_tUbicacion.FlatAppearance.BorderSize = 0;
            this.btn_tUbicacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tUbicacion.Image = ((System.Drawing.Image)(resources.GetObject("btn_tUbicacion.Image")));
            this.btn_tUbicacion.Location = new System.Drawing.Point(427, 98);
            this.btn_tUbicacion.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tUbicacion.Name = "btn_tUbicacion";
            this.btn_tUbicacion.Size = new System.Drawing.Size(26, 28);
            this.btn_tUbicacion.TabIndex = 15;
            this.btn_tUbicacion.UseVisualStyleBackColor = true;
            this.btn_tUbicacion.Click += new System.EventHandler(this.btn_tUbicacion_Click);
            // 
            // btn_tAlmacen
            // 
            this.btn_tAlmacen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_tAlmacen.FlatAppearance.BorderSize = 0;
            this.btn_tAlmacen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tAlmacen.Image = ((System.Drawing.Image)(resources.GetObject("btn_tAlmacen.Image")));
            this.btn_tAlmacen.Location = new System.Drawing.Point(427, 33);
            this.btn_tAlmacen.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tAlmacen.Name = "btn_tAlmacen";
            this.btn_tAlmacen.Size = new System.Drawing.Size(26, 28);
            this.btn_tAlmacen.TabIndex = 14;
            this.btn_tAlmacen.UseVisualStyleBackColor = true;
            this.btn_tAlmacen.Click += new System.EventHandler(this.btn_tAlmacen_Click);
            // 
            // txb_bodega
            // 
            this.txb_bodega.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_bodega.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_bodega.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_bodega.Location = new System.Drawing.Point(181, 100);
            this.txb_bodega.Margin = new System.Windows.Forms.Padding(2);
            this.txb_bodega.Name = "txb_bodega";
            this.txb_bodega.Size = new System.Drawing.Size(236, 23);
            this.txb_bodega.TabIndex = 0;
            this.txb_bodega.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(18, 104);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 24);
            this.label9.TabIndex = 4;
            this.label9.Text = "Ubicación Fisica en Bodega";
            // 
            // txb_finca
            // 
            this.txb_finca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_finca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_finca.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_finca.Location = new System.Drawing.Point(181, 168);
            this.txb_finca.Margin = new System.Windows.Forms.Padding(2);
            this.txb_finca.Name = "txb_finca";
            this.txb_finca.Size = new System.Drawing.Size(236, 23);
            this.txb_finca.TabIndex = 0;
            this.txb_finca.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(46, 172);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 24);
            this.label10.TabIndex = 2;
            this.label10.Text = "Finca de Procedencia";
            // 
            // txb_almacen
            // 
            this.txb_almacen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_almacen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_almacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_almacen.Location = new System.Drawing.Point(181, 35);
            this.txb_almacen.Margin = new System.Windows.Forms.Padding(2);
            this.txb_almacen.Name = "txb_almacen";
            this.txb_almacen.Size = new System.Drawing.Size(236, 23);
            this.txb_almacen.TabIndex = 0;
            this.txb_almacen.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 38);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(163, 24);
            this.label11.TabIndex = 0;
            this.label11.Text = "Ubicación de Almacenamiento";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btn_tPesador);
            this.groupBox4.Controls.Add(this.txb_observacion);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txb_personal);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(9, 322);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(942, 254);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Observaciones Generales";
            // 
            // btn_tPesador
            // 
            this.btn_tPesador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_tPesador.FlatAppearance.BorderSize = 0;
            this.btn_tPesador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tPesador.Image = ((System.Drawing.Image)(resources.GetObject("btn_tPesador.Image")));
            this.btn_tPesador.Location = new System.Drawing.Point(911, 32);
            this.btn_tPesador.Margin = new System.Windows.Forms.Padding(2);
            this.btn_tPesador.Name = "btn_tPesador";
            this.btn_tPesador.Size = new System.Drawing.Size(26, 28);
            this.btn_tPesador.TabIndex = 18;
            this.btn_tPesador.UseVisualStyleBackColor = true;
            this.btn_tPesador.Click += new System.EventHandler(this.btn_tPesador_Click);
            // 
            // txb_observacion
            // 
            this.txb_observacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_observacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_observacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_observacion.Location = new System.Drawing.Point(202, 100);
            this.txb_observacion.Margin = new System.Windows.Forms.Padding(2);
            this.txb_observacion.Multiline = true;
            this.txb_observacion.Name = "txb_observacion";
            this.txb_observacion.Size = new System.Drawing.Size(699, 149);
            this.txb_observacion.TabIndex = 19;
            this.txb_observacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_observacion_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(83, 103);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 24);
            this.label8.TabIndex = 4;
            this.label8.Text = "Observaciones";
            // 
            // txb_personal
            // 
            this.txb_personal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_personal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_personal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_personal.Location = new System.Drawing.Point(202, 35);
            this.txb_personal.Margin = new System.Windows.Forms.Padding(2);
            this.txb_personal.Name = "txb_personal";
            this.txb_personal.Size = new System.Drawing.Size(699, 23);
            this.txb_personal.TabIndex = 0;
            this.txb_personal.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(56, 37);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 24);
            this.label13.TabIndex = 0;
            this.label13.Text = "Nombre del Pesador";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Oswald", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(299, 7);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 21);
            this.label12.TabIndex = 28;
            this.label12.Text = "Fecha";
            // 
            // dtp_fechaTrilla
            // 
            this.dtp_fechaTrilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fechaTrilla.Location = new System.Drawing.Point(302, 32);
            this.dtp_fechaTrilla.Margin = new System.Windows.Forms.Padding(2);
            this.dtp_fechaTrilla.Name = "dtp_fechaTrilla";
            this.dtp_fechaTrilla.Size = new System.Drawing.Size(151, 21);
            this.dtp_fechaTrilla.TabIndex = 4;
            // 
            // btn_pdfTrilla
            // 
            this.btn_pdfTrilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_pdfTrilla.BackColor = System.Drawing.Color.White;
            this.btn_pdfTrilla.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btn_pdfTrilla.FlatAppearance.BorderSize = 2;
            this.btn_pdfTrilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pdfTrilla.Font = new System.Drawing.Font("Oswald", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pdfTrilla.ForeColor = System.Drawing.Color.Black;
            this.btn_pdfTrilla.Image = global::sistema_modular_cafe_majada.Properties.Resources.pdf_24px;
            this.btn_pdfTrilla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_pdfTrilla.Location = new System.Drawing.Point(790, 7);
            this.btn_pdfTrilla.Margin = new System.Windows.Forms.Padding(2);
            this.btn_pdfTrilla.Name = "btn_pdfTrilla";
            this.btn_pdfTrilla.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.btn_pdfTrilla.Size = new System.Drawing.Size(162, 32);
            this.btn_pdfTrilla.TabIndex = 0;
            this.btn_pdfTrilla.TabStop = false;
            this.btn_pdfTrilla.Text = "Generar PDF";
            this.btn_pdfTrilla.UseVisualStyleBackColor = false;
            this.btn_pdfTrilla.Click += new System.EventHandler(this.btn_pdfTrilla_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(45)))), ((int)(((byte)(59)))));
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("Oswald", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Image = global::sistema_modular_cafe_majada.Properties.Resources.btn_eliminar;
            this.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Cancel.Location = new System.Drawing.Point(873, 42);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.btn_Cancel.Size = new System.Drawing.Size(79, 32);
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.TabStop = false;
            this.btn_Cancel.Text = "Cancelar";
            this.btn_Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_SaveTrilla
            // 
            this.btn_SaveTrilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveTrilla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.btn_SaveTrilla.FlatAppearance.BorderSize = 0;
            this.btn_SaveTrilla.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(115)))), ((int)(((byte)(71)))));
            this.btn_SaveTrilla.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(115)))), ((int)(((byte)(71)))));
            this.btn_SaveTrilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveTrilla.Font = new System.Drawing.Font("Oswald", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SaveTrilla.ForeColor = System.Drawing.Color.White;
            this.btn_SaveTrilla.Image = global::sistema_modular_cafe_majada.Properties.Resources.btn_guardar;
            this.btn_SaveTrilla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_SaveTrilla.Location = new System.Drawing.Point(782, 42);
            this.btn_SaveTrilla.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SaveTrilla.Name = "btn_SaveTrilla";
            this.btn_SaveTrilla.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.btn_SaveTrilla.Size = new System.Drawing.Size(79, 32);
            this.btn_SaveTrilla.TabIndex = 20;
            this.btn_SaveTrilla.Text = "Guardar";
            this.btn_SaveTrilla.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_SaveTrilla.UseVisualStyleBackColor = false;
            this.btn_SaveTrilla.Click += new System.EventHandler(this.btn_SaveTrilla_Click);
            // 
            // btn_deleteTrilla
            // 
            this.btn_deleteTrilla.FlatAppearance.BorderSize = 0;
            this.btn_deleteTrilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_deleteTrilla.Image = global::sistema_modular_cafe_majada.Properties.Resources.btn_eliminar_24px;
            this.btn_deleteTrilla.Location = new System.Drawing.Point(241, 26);
            this.btn_deleteTrilla.Margin = new System.Windows.Forms.Padding(2);
            this.btn_deleteTrilla.Name = "btn_deleteTrilla";
            this.btn_deleteTrilla.Size = new System.Drawing.Size(26, 28);
            this.btn_deleteTrilla.TabIndex = 3;
            this.btn_deleteTrilla.UseVisualStyleBackColor = true;
            this.btn_deleteTrilla.Click += new System.EventHandler(this.btn_deleteTrilla_Click);
            // 
            // form_trillaCafe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(960, 585);
            this.Controls.Add(this.btn_deleteTrilla);
            this.Controls.Add(this.btn_pdfTrilla);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_SaveTrilla);
            this.Controls.Add(this.dtp_fechaTrilla);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_tTrillas);
            this.Controls.Add(this.txb_numTrilla);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_cosecha);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(1350, 821);
            this.MinimumSize = new System.Drawing.Size(960, 585);
            this.Name = "form_trillaCafe";
            this.Text = "form_trillaCafe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_tTrillas;
        private System.Windows.Forms.TextBox txb_numTrilla;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_cosecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_subProducto;
        private System.Windows.Forms.RadioButton rb_cafeTrilla;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txb_pesoQQs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txb_pesoSaco;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_calidadCafe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txb_bodega;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txb_finca;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txb_almacen;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txb_observacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txb_personal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtp_fechaTrilla;
        private System.Windows.Forms.Button btn_tCCafe;
        private System.Windows.Forms.Button btn_tUbicacion;
        private System.Windows.Forms.Button btn_tAlmacen;
        private System.Windows.Forms.Button btn_tPesador;
        private System.Windows.Forms.ComboBox cbx_subProducto;
        private System.Windows.Forms.Button btn_pdfTrilla;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_SaveTrilla;
        private System.Windows.Forms.Button btn_deleteTrilla;
        private System.Windows.Forms.Button btn_tProcedencia;
    }
}