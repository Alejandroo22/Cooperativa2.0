
namespace sistema_modular_cafe_majada.views
{
    partial class form_coffeeStocks
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_subPartida = new System.Windows.Forms.Button();
            this.btn_trillaCafe = new System.Windows.Forms.Button();
            this.btn_entradaCafe = new System.Windows.Forms.Button();
            this.btn_salidaCafe = new System.Windows.Forms.Button();
            this.pnl_opcStock = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btn_subPartida, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_trillaCafe, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_entradaCafe, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_salidaCafe, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 14);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(837, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_subPartida
            // 
            this.btn_subPartida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_subPartida.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.btn_subPartida.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(238)))), ((int)(((byte)(249)))));
            this.btn_subPartida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_subPartida.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_subPartida.Image = global::sistema_modular_cafe_majada.Properties.Resources.SubPartida_45px;
            this.btn_subPartida.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_subPartida.Location = new System.Drawing.Point(3, 2);
            this.btn_subPartida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_subPartida.Name = "btn_subPartida";
            this.btn_subPartida.Size = new System.Drawing.Size(203, 96);
            this.btn_subPartida.TabIndex = 3;
            this.btn_subPartida.TabStop = false;
            this.btn_subPartida.Text = "Sub Partida";
            this.btn_subPartida.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_subPartida.UseVisualStyleBackColor = true;
            this.btn_subPartida.Click += new System.EventHandler(this.btn_subPartida_Click);
            // 
            // btn_trillaCafe
            // 
            this.btn_trillaCafe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_trillaCafe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.btn_trillaCafe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(238)))), ((int)(((byte)(249)))));
            this.btn_trillaCafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_trillaCafe.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_trillaCafe.Image = global::sistema_modular_cafe_majada.Properties.Resources.trilla_45px;
            this.btn_trillaCafe.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_trillaCafe.Location = new System.Drawing.Point(212, 2);
            this.btn_trillaCafe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_trillaCafe.Name = "btn_trillaCafe";
            this.btn_trillaCafe.Size = new System.Drawing.Size(203, 96);
            this.btn_trillaCafe.TabIndex = 2;
            this.btn_trillaCafe.TabStop = false;
            this.btn_trillaCafe.Text = "Trilla";
            this.btn_trillaCafe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_trillaCafe.UseVisualStyleBackColor = true;
            this.btn_trillaCafe.Click += new System.EventHandler(this.btn_trillaCafe_Click);
            // 
            // btn_entradaCafe
            // 
            this.btn_entradaCafe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_entradaCafe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.btn_entradaCafe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(238)))), ((int)(((byte)(249)))));
            this.btn_entradaCafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_entradaCafe.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_entradaCafe.Image = global::sistema_modular_cafe_majada.Properties.Resources.Entrada_45px;
            this.btn_entradaCafe.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_entradaCafe.Location = new System.Drawing.Point(421, 2);
            this.btn_entradaCafe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_entradaCafe.Name = "btn_entradaCafe";
            this.btn_entradaCafe.Size = new System.Drawing.Size(203, 96);
            this.btn_entradaCafe.TabIndex = 4;
            this.btn_entradaCafe.TabStop = false;
            this.btn_entradaCafe.Text = "Traslado";
            this.btn_entradaCafe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_entradaCafe.UseVisualStyleBackColor = true;
            this.btn_entradaCafe.Click += new System.EventHandler(this.btn_entradaCafe_Click);
            // 
            // btn_salidaCafe
            // 
            this.btn_salidaCafe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_salidaCafe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.btn_salidaCafe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(238)))), ((int)(((byte)(249)))));
            this.btn_salidaCafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_salidaCafe.Font = new System.Drawing.Font("Oswald", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_salidaCafe.Image = global::sistema_modular_cafe_majada.Properties.Resources.Salida_45px;
            this.btn_salidaCafe.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_salidaCafe.Location = new System.Drawing.Point(630, 2);
            this.btn_salidaCafe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_salidaCafe.Name = "btn_salidaCafe";
            this.btn_salidaCafe.Size = new System.Drawing.Size(204, 96);
            this.btn_salidaCafe.TabIndex = 5;
            this.btn_salidaCafe.TabStop = false;
            this.btn_salidaCafe.Text = "Salida";
            this.btn_salidaCafe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_salidaCafe.UseVisualStyleBackColor = true;
            this.btn_salidaCafe.Click += new System.EventHandler(this.btn_salidaCafe_Click);
            // 
            // pnl_opcStock
            // 
            this.pnl_opcStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_opcStock.BackColor = System.Drawing.Color.White;
            this.pnl_opcStock.BackgroundImage = global::sistema_modular_cafe_majada.Properties.Resources.servicio_al_cliente_1;
            this.pnl_opcStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_opcStock.Location = new System.Drawing.Point(13, 121);
            this.pnl_opcStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl_opcStock.Name = "pnl_opcStock";
            this.pnl_opcStock.Size = new System.Drawing.Size(1255, 588);
            this.pnl_opcStock.TabIndex = 1;
            // 
            // form_coffeeStocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnl_opcStock);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1800, 1010);
            this.Name = "form_coffeeStocks";
            this.Text = "form_subpatidas";
            this.Load += new System.EventHandler(this.form_coffeeStocks_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_opcStock;
        private System.Windows.Forms.Button btn_salidaCafe;
        private System.Windows.Forms.Button btn_entradaCafe;
        private System.Windows.Forms.Button btn_subPartida;
        private System.Windows.Forms.Button btn_trillaCafe;
    }
}