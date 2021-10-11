namespace Actualizator
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnPrevisualizar = new System.Windows.Forms.Button();
            this.cmbBoxFiltros = new System.Windows.Forms.ComboBox();
            this.btnModificarFiltros = new System.Windows.Forms.Button();
            this.chkBoxFiltros = new System.Windows.Forms.CheckBox();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.textBackup = new System.Windows.Forms.TextBox();
            this.btnRutaBackup = new System.Windows.Forms.Button();
            this.textDestino = new System.Windows.Forms.TextBox();
            this.cmbConexiones = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnVerCarpetaDestino = new System.Windows.Forms.Button();
            this.btnVerCarpetaOrigen = new System.Windows.Forms.Button();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRutaDestino = new System.Windows.Forms.Button();
            this.btnRutaOrigen = new System.Windows.Forms.Button();
            this.textOrigen = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GuardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeViewOrigen = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblArchivosOrigen = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutDestino = new System.Windows.Forms.TableLayoutPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutDestino.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnPrevisualizar);
            this.splitContainer1.Panel1.Controls.Add(this.cmbBoxFiltros);
            this.splitContainer1.Panel1.Controls.Add(this.btnModificarFiltros);
            this.splitContainer1.Panel1.Controls.Add(this.chkBoxFiltros);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxBackup);
            this.splitContainer1.Panel1.Controls.Add(this.textBackup);
            this.splitContainer1.Panel1.Controls.Add(this.btnRutaBackup);
            this.splitContainer1.Panel1.Controls.Add(this.textDestino);
            this.splitContainer1.Panel1.Controls.Add(this.cmbConexiones);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnVerCarpetaDestino);
            this.splitContainer1.Panel1.Controls.Add(this.btnVerCarpetaOrigen);
            this.splitContainer1.Panel1.Controls.Add(this.btnSincronizar);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnRutaDestino);
            this.splitContainer1.Panel1.Controls.Add(this.btnRutaOrigen);
            this.splitContainer1.Panel1.Controls.Add(this.textOrigen);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            this.splitContainer1.Size = new System.Drawing.Size(1052, 665);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnPrevisualizar
            // 
            this.btnPrevisualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevisualizar.Location = new System.Drawing.Point(890, 34);
            this.btnPrevisualizar.Name = "btnPrevisualizar";
            this.btnPrevisualizar.Size = new System.Drawing.Size(137, 39);
            this.btnPrevisualizar.TabIndex = 27;
            this.btnPrevisualizar.Text = "Previsualizar";
            this.btnPrevisualizar.UseVisualStyleBackColor = true;
            // 
            // cmbBoxFiltros
            // 
            this.cmbBoxFiltros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxFiltros.FormattingEnabled = true;
            this.cmbBoxFiltros.Location = new System.Drawing.Point(144, 113);
            this.cmbBoxFiltros.Name = "cmbBoxFiltros";
            this.cmbBoxFiltros.Size = new System.Drawing.Size(176, 21);
            this.cmbBoxFiltros.TabIndex = 26;
            this.cmbBoxFiltros.Visible = false;
            // 
            // btnModificarFiltros
            // 
            this.btnModificarFiltros.Location = new System.Drawing.Point(326, 111);
            this.btnModificarFiltros.Name = "btnModificarFiltros";
            this.btnModificarFiltros.Size = new System.Drawing.Size(114, 24);
            this.btnModificarFiltros.TabIndex = 25;
            this.btnModificarFiltros.Text = "Configurar filtros";
            this.btnModificarFiltros.UseVisualStyleBackColor = true;
            this.btnModificarFiltros.Visible = false;
            this.btnModificarFiltros.Click += new System.EventHandler(this.btnAddFiltros_Click);
            // 
            // chkBoxFiltros
            // 
            this.chkBoxFiltros.AutoSize = true;
            this.chkBoxFiltros.Location = new System.Drawing.Point(24, 112);
            this.chkBoxFiltros.Name = "chkBoxFiltros";
            this.chkBoxFiltros.Size = new System.Drawing.Size(112, 17);
            this.chkBoxFiltros.TabIndex = 22;
            this.chkBoxFiltros.Text = "Filtros excluyentes";
            this.chkBoxFiltros.UseVisualStyleBackColor = true;
            this.chkBoxFiltros.CheckedChanged += new System.EventHandler(this.chkBoxFiltros_CheckedChanged);
            // 
            // checkBoxBackup
            // 
            this.checkBoxBackup.AutoSize = true;
            this.checkBoxBackup.Location = new System.Drawing.Point(24, 142);
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.checkBoxBackup.Size = new System.Drawing.Size(63, 17);
            this.checkBoxBackup.TabIndex = 21;
            this.checkBoxBackup.Text = "Backup";
            this.checkBoxBackup.UseVisualStyleBackColor = true;
            this.checkBoxBackup.CheckedChanged += new System.EventHandler(this.checkBoxBackup_CheckedChanged);
            // 
            // textBackup
            // 
            this.textBackup.Location = new System.Drawing.Point(144, 141);
            this.textBackup.Name = "textBackup";
            this.textBackup.ReadOnly = true;
            this.textBackup.Size = new System.Drawing.Size(444, 20);
            this.textBackup.TabIndex = 19;
            this.textBackup.Visible = false;
            // 
            // btnRutaBackup
            // 
            this.btnRutaBackup.Location = new System.Drawing.Point(594, 140);
            this.btnRutaBackup.Name = "btnRutaBackup";
            this.btnRutaBackup.Size = new System.Drawing.Size(33, 20);
            this.btnRutaBackup.TabIndex = 17;
            this.btnRutaBackup.Text = "...";
            this.btnRutaBackup.UseVisualStyleBackColor = true;
            this.btnRutaBackup.Visible = false;
            this.btnRutaBackup.Click += new System.EventHandler(this.btnRutaBackup_Click);
            // 
            // textDestino
            // 
            this.textDestino.Location = new System.Drawing.Point(144, 84);
            this.textDestino.Name = "textDestino";
            this.textDestino.ReadOnly = true;
            this.textDestino.Size = new System.Drawing.Size(444, 20);
            this.textDestino.TabIndex = 15;
            // 
            // cmbConexiones
            // 
            this.cmbConexiones.FormattingEnabled = true;
            this.cmbConexiones.Location = new System.Drawing.Point(144, 34);
            this.cmbConexiones.Name = "cmbConexiones";
            this.cmbConexiones.Size = new System.Drawing.Size(444, 21);
            this.cmbConexiones.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Conexión";
            // 
            // btnVerCarpetaDestino
            // 
            this.btnVerCarpetaDestino.Location = new System.Drawing.Point(633, 85);
            this.btnVerCarpetaDestino.Name = "btnVerCarpetaDestino";
            this.btnVerCarpetaDestino.Size = new System.Drawing.Size(137, 20);
            this.btnVerCarpetaDestino.TabIndex = 8;
            this.btnVerCarpetaDestino.Text = "Visualizar carpeta";
            this.btnVerCarpetaDestino.UseVisualStyleBackColor = true;
            this.btnVerCarpetaDestino.Visible = false;
            this.btnVerCarpetaDestino.Click += new System.EventHandler(this.btnVerCarpetaDestino_Click);
            // 
            // btnVerCarpetaOrigen
            // 
            this.btnVerCarpetaOrigen.Location = new System.Drawing.Point(633, 55);
            this.btnVerCarpetaOrigen.Name = "btnVerCarpetaOrigen";
            this.btnVerCarpetaOrigen.Size = new System.Drawing.Size(137, 20);
            this.btnVerCarpetaOrigen.TabIndex = 7;
            this.btnVerCarpetaOrigen.Text = "Visualizar carpeta";
            this.btnVerCarpetaOrigen.UseVisualStyleBackColor = true;
            this.btnVerCarpetaOrigen.Visible = false;
            this.btnVerCarpetaOrigen.Click += new System.EventHandler(this.btnVerCarpetaOrigen_Click);
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSincronizar.Location = new System.Drawing.Point(890, 120);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(137, 39);
            this.btnSincronizar.TabIndex = 6;
            this.btnSincronizar.Text = "Sincronizar";
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Origen";
            // 
            // btnRutaDestino
            // 
            this.btnRutaDestino.Location = new System.Drawing.Point(594, 83);
            this.btnRutaDestino.Name = "btnRutaDestino";
            this.btnRutaDestino.Size = new System.Drawing.Size(33, 20);
            this.btnRutaDestino.TabIndex = 3;
            this.btnRutaDestino.Text = "...";
            this.btnRutaDestino.UseVisualStyleBackColor = true;
            this.btnRutaDestino.Click += new System.EventHandler(this.btnSubirArchivo2_Click);
            // 
            // btnRutaOrigen
            // 
            this.btnRutaOrigen.Location = new System.Drawing.Point(594, 55);
            this.btnRutaOrigen.Name = "btnRutaOrigen";
            this.btnRutaOrigen.Size = new System.Drawing.Size(33, 20);
            this.btnRutaOrigen.TabIndex = 2;
            this.btnRutaOrigen.Text = "...";
            this.btnRutaOrigen.UseVisualStyleBackColor = true;
            this.btnRutaOrigen.Click += new System.EventHandler(this.btnSubirArchivo1_Click);
            // 
            // textOrigen
            // 
            this.textOrigen.Location = new System.Drawing.Point(144, 58);
            this.textOrigen.Name = "textOrigen";
            this.textOrigen.ReadOnly = true;
            this.textOrigen.Size = new System.Drawing.Size(444, 20);
            this.textOrigen.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1052, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GuardarToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.menuToolStripMenuItem.Text = "Opciones";
            // 
            // GuardarToolStripMenuItem
            // 
            this.GuardarToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.GuardarToolStripMenuItem.Name = "GuardarToolStripMenuItem";
            this.GuardarToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.GuardarToolStripMenuItem.Text = "Guardar Conexión";
            this.GuardarToolStripMenuItem.Click += new System.EventHandler(this.anadirToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutDestino);
            this.splitContainer2.Size = new System.Drawing.Size(1049, 483);
            this.splitContainer2.SplitterDistance = 518;
            this.splitContainer2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeViewOrigen, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 483);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeViewOrigen
            // 
            this.treeViewOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewOrigen.Location = new System.Drawing.Point(3, 53);
            this.treeViewOrigen.Name = "treeViewOrigen";
            this.treeViewOrigen.Size = new System.Drawing.Size(512, 427);
            this.treeViewOrigen.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblArchivosOrigen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 44);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Origen";
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // lblArchivosOrigen
            // 
            this.lblArchivosOrigen.AutoSize = true;
            this.lblArchivosOrigen.Location = new System.Drawing.Point(66, 16);
            this.lblArchivosOrigen.Name = "lblArchivosOrigen";
            this.lblArchivosOrigen.Size = new System.Drawing.Size(89, 13);
            this.lblArchivosOrigen.TabIndex = 1;
            this.lblArchivosOrigen.Text = "lblArchivosOrigen";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Archivos: ";
            // 
            // tableLayoutDestino
            // 
            this.tableLayoutDestino.AutoScroll = true;
            this.tableLayoutDestino.ColumnCount = 1;
            this.tableLayoutDestino.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDestino.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDestino.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutDestino.Name = "tableLayoutDestino";
            this.tableLayoutDestino.RowCount = 2;
            this.tableLayoutDestino.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutDestino.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDestino.Size = new System.Drawing.Size(527, 483);
            this.tableLayoutDestino.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 483);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // folderBrowserDlg
            // 
            this.folderBrowserDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(521, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destino";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 665);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Sincronizar carpetas";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutDestino.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textDestino;
        private System.Windows.Forms.ComboBox cmbConexiones;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnVerCarpetaDestino;
        private System.Windows.Forms.Button btnVerCarpetaOrigen;
        private System.Windows.Forms.Button btnSincronizar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRutaDestino;
        private System.Windows.Forms.Button btnRutaOrigen;
        private System.Windows.Forms.TextBox textOrigen;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GuardarToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDestino;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeViewOrigen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblArchivosOrigen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxBackup;
        private System.Windows.Forms.TextBox textBackup;
        private System.Windows.Forms.Button btnRutaBackup;
        private System.Windows.Forms.ComboBox cmbBoxFiltros;
        private System.Windows.Forms.Button btnModificarFiltros;
        private System.Windows.Forms.CheckBox chkBoxFiltros;
        private System.Windows.Forms.Button btnPrevisualizar;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

