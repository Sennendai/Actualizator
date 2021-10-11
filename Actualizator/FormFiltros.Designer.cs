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
            this.cmbBoxFiltros = new System.Windows.Forms.ComboBox();
            this.btnAddFiltros = new System.Windows.Forms.Button();
            this.txtBoxFiltro = new System.Windows.Forms.TextBox();
            this.dataGridFiltros = new System.Windows.Forms.DataGridView();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFiltros)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBoxFiltros
            // 
            this.cmbBoxFiltros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxFiltros.FormattingEnabled = true;
            this.cmbBoxFiltros.Location = new System.Drawing.Point(12, 51);
            this.cmbBoxFiltros.Name = "cmbBoxFiltros";
            this.cmbBoxFiltros.Size = new System.Drawing.Size(176, 21);
            this.cmbBoxFiltros.TabIndex = 28;
            // 
            // btnAddFiltros
            // 
            this.btnAddFiltros.Location = new System.Drawing.Point(194, 83);
            this.btnAddFiltros.Name = "btnAddFiltros";
            this.btnAddFiltros.Size = new System.Drawing.Size(33, 21);
            this.btnAddFiltros.TabIndex = 27;
            this.btnAddFiltros.Text = "+";
            this.btnAddFiltros.UseVisualStyleBackColor = true;
            this.btnAddFiltros.Click += new System.EventHandler(this.btnAddFiltros_Click);
            // 
            // txtBoxFiltro
            // 
            this.txtBoxFiltro.AcceptsReturn = true;
            this.txtBoxFiltro.Location = new System.Drawing.Point(12, 83);
            this.txtBoxFiltro.Name = "txtBoxFiltro";
            this.txtBoxFiltro.Size = new System.Drawing.Size(176, 20);
            this.txtBoxFiltro.TabIndex = 29;
            //add the handler to the textbox
            this.txtBoxFiltro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
            // 
            // dataGridFiltros
            // 
            this.dataGridFiltros.AllowUserToAddRows = false;
            this.dataGridFiltros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFiltros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridFiltros.Location = new System.Drawing.Point(0, 118);
            this.dataGridFiltros.Name = "dataGridFiltros";
            this.dataGridFiltros.Size = new System.Drawing.Size(299, 150);
            this.dataGridFiltros.TabIndex = 30;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(12, 12);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(110, 24);
            this.btnAceptar.TabIndex = 31;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(177, 12);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 24);
            this.btnCancelar.TabIndex = 32;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormFiltros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 268);
            this.ControlBox = false;
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
    }
}