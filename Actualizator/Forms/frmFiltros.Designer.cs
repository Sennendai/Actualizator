namespace Actualizator
{
    partial class FormFiltros
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
            this.components = new System.ComponentModel.Container();
            this.cmbBoxFiltros = new System.Windows.Forms.ComboBox();
            this.txtBoxFiltro = new System.Windows.Forms.TextBox();
            this.dataGridFiltros = new System.Windows.Forms.DataGridView();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.btnAbrirOrigen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBorrarFiltro = new System.Windows.Forms.Button();
            this.btnAddFiltros = new System.Windows.Forms.Button();
            this.cmbBoxConfigs = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFiltros)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBoxFiltros
            // 
            this.cmbBoxFiltros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxFiltros.FormattingEnabled = true;
            this.cmbBoxFiltros.Location = new System.Drawing.Point(12, 12);
            this.cmbBoxFiltros.Name = "cmbBoxFiltros";
            this.cmbBoxFiltros.Size = new System.Drawing.Size(176, 21);
            this.cmbBoxFiltros.TabIndex = 28;
            // 
            // txtBoxFiltro
            // 
            this.txtBoxFiltro.AcceptsReturn = true;
            this.txtBoxFiltro.Location = new System.Drawing.Point(12, 44);
            this.txtBoxFiltro.Name = "txtBoxFiltro";
            this.txtBoxFiltro.Size = new System.Drawing.Size(176, 20);
            this.txtBoxFiltro.TabIndex = 29;
            this.txtBoxFiltro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckEnterKeyPress);
            // 
            // dataGridFiltros
            // 
            this.dataGridFiltros.AllowUserToAddRows = false;
            this.dataGridFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridFiltros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridFiltros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFiltros.Location = new System.Drawing.Point(0, 79);
            this.dataGridFiltros.Name = "dataGridFiltros";
            this.dataGridFiltros.Size = new System.Drawing.Size(462, 150);
            this.dataGridFiltros.TabIndex = 30;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAceptar.Location = new System.Drawing.Point(12, 278);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(110, 24);
            this.btnAceptar.TabIndex = 31;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(340, 278);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 24);
            this.btnCancelar.TabIndex = 32;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAbrirOrigen
            // 
            this.btnAbrirOrigen.Location = new System.Drawing.Point(262, 39);
            this.btnAbrirOrigen.Name = "btnAbrirOrigen";
            this.btnAbrirOrigen.Size = new System.Drawing.Size(33, 29);
            this.btnAbrirOrigen.TabIndex = 35;
            this.btnAbrirOrigen.Text = "...";
            this.toolTipControl.SetToolTip(this.btnAbrirOrigen, "Abrir carpeta origen");
            this.btnAbrirOrigen.UseVisualStyleBackColor = true;
            this.btnAbrirOrigen.Click += new System.EventHandler(this.btnAbrirOrigen_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Pulse la tecla \'Supr\' para borrar un registro de la tabla";
            // 
            // btnBorrarFiltro
            // 
            this.btnBorrarFiltro.BackgroundImage = global::Actualizator.Properties.Resources.delete;
            this.btnBorrarFiltro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBorrarFiltro.Location = new System.Drawing.Point(228, 39);
            this.btnBorrarFiltro.Name = "btnBorrarFiltro";
            this.btnBorrarFiltro.Size = new System.Drawing.Size(28, 29);
            this.btnBorrarFiltro.TabIndex = 34;
            this.btnBorrarFiltro.UseVisualStyleBackColor = true;
            this.btnBorrarFiltro.Click += new System.EventHandler(this.btnBorrarFiltro_Click);
            // 
            // btnAddFiltros
            // 
            this.btnAddFiltros.BackgroundImage = global::Actualizator.Properties.Resources.add;
            this.btnAddFiltros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddFiltros.Location = new System.Drawing.Point(194, 39);
            this.btnAddFiltros.Name = "btnAddFiltros";
            this.btnAddFiltros.Size = new System.Drawing.Size(28, 29);
            this.btnAddFiltros.TabIndex = 27;
            this.btnAddFiltros.UseVisualStyleBackColor = true;
            this.btnAddFiltros.Click += new System.EventHandler(this.btnAddFiltros_Click);
            // 
            // cmbBoxConfigs
            // 
            this.cmbBoxConfigs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxConfigs.FormattingEnabled = true;
            this.cmbBoxConfigs.Location = new System.Drawing.Point(300, 12);
            this.cmbBoxConfigs.Name = "cmbBoxConfigs";
            this.cmbBoxConfigs.Size = new System.Drawing.Size(150, 21);
            this.cmbBoxConfigs.TabIndex = 36;
            this.cmbBoxConfigs.Visible = false;
            this.cmbBoxConfigs.SelectedIndexChanged += new System.EventHandler(this.cmbBoxConfigs_SelectedIndexChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "archivos";
            this.openFileDialog.Multiselect = true;
            // 
            // FormFiltros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 314);
            this.ControlBox = false;
            this.Controls.Add(this.cmbBoxConfigs);
            this.Controls.Add(this.btnAbrirOrigen);
            this.Controls.Add(this.btnBorrarFiltro);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.dataGridFiltros);
            this.Controls.Add(this.txtBoxFiltro);
            this.Controls.Add(this.cmbBoxFiltros);
            this.Controls.Add(this.btnAddFiltros);
            this.MaximizeBox = false;
            this.Name = "FormFiltros";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filtros";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormFiltros_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFiltros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBoxFiltros;
        private System.Windows.Forms.Button btnAddFiltros;
        private System.Windows.Forms.TextBox txtBoxFiltro;
        private System.Windows.Forms.DataGridView dataGridFiltros;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBorrarFiltro;
        private System.Windows.Forms.Button btnAbrirOrigen;
        private System.Windows.Forms.ComboBox cmbBoxConfigs;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}