﻿namespace Actualizator
{
    partial class FormPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnRutaOrigen = new System.Windows.Forms.Button();
            this.cmbBoxFiltros = new System.Windows.Forms.ComboBox();
            this.chkBoxFiltros = new System.Windows.Forms.CheckBox();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.textBackup = new System.Windows.Forms.TextBox();
            this.btnRutaBackup = new System.Windows.Forms.Button();
            this.textDestino = new System.Windows.Forms.TextBox();
            this.cmbProyecto = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRutaDestino = new System.Windows.Forms.Button();
            this.textOrigen = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeViewOrigen = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblArchivosOrigen = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutDestino = new System.Windows.Forms.TableLayoutPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.btnPrevisualizar = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnRecargar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnRestaurarBackup = new System.Windows.Forms.Button();
            this.btnAddProyecto = new System.Windows.Forms.Button();
            this.btnModificarFiltros = new System.Windows.Forms.Button();
            this.btnVerCarpetaDestino = new System.Windows.Forms.Button();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.txtProyecto = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtProyecto);
            this.splitContainer1.Panel1.Controls.Add(this.btnPrevisualizar);
            this.splitContainer1.Panel1.Controls.Add(this.lblLog);
            this.splitContainer1.Panel1.Controls.Add(this.btnBorrar);
            this.splitContainer1.Panel1.Controls.Add(this.btnRecargar);
            this.splitContainer1.Panel1.Controls.Add(this.btnGuardar);
            this.splitContainer1.Panel1.Controls.Add(this.btnRutaOrigen);
            this.splitContainer1.Panel1.Controls.Add(this.btnActualizar);
            this.splitContainer1.Panel1.Controls.Add(this.btnRestaurarBackup);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddProyecto);
            this.splitContainer1.Panel1.Controls.Add(this.cmbBoxFiltros);
            this.splitContainer1.Panel1.Controls.Add(this.btnModificarFiltros);
            this.splitContainer1.Panel1.Controls.Add(this.chkBoxFiltros);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxBackup);
            this.splitContainer1.Panel1.Controls.Add(this.textBackup);
            this.splitContainer1.Panel1.Controls.Add(this.btnRutaBackup);
            this.splitContainer1.Panel1.Controls.Add(this.textDestino);
            this.splitContainer1.Panel1.Controls.Add(this.cmbProyecto);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnVerCarpetaDestino);
            this.splitContainer1.Panel1.Controls.Add(this.btnSincronizar);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnRutaDestino);
            this.splitContainer1.Panel1.Controls.Add(this.textOrigen);
            this.toolTipControl.SetToolTip(this.splitContainer1.Panel1, "Recargar configuración");
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            this.splitContainer1.Size = new System.Drawing.Size(834, 633);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 1;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(132, 9);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(35, 13);
            this.lblLog.TabIndex = 0;
            this.lblLog.Text = "lblLog";
            // 
            // btnRutaOrigen
            // 
            this.btnRutaOrigen.Location = new System.Drawing.Point(585, 68);
            this.btnRutaOrigen.Name = "btnRutaOrigen";
            this.btnRutaOrigen.Size = new System.Drawing.Size(33, 20);
            this.btnRutaOrigen.TabIndex = 3;
            this.btnRutaOrigen.Text = "...";
            this.btnRutaOrigen.UseVisualStyleBackColor = true;
            this.btnRutaOrigen.Click += new System.EventHandler(this.btnRutaOrigen_Click);
            // 
            // cmbBoxFiltros
            // 
            this.cmbBoxFiltros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxFiltros.FormattingEnabled = true;
            this.cmbBoxFiltros.Location = new System.Drawing.Point(135, 153);
            this.cmbBoxFiltros.Name = "cmbBoxFiltros";
            this.cmbBoxFiltros.Size = new System.Drawing.Size(176, 21);
            this.cmbBoxFiltros.TabIndex = 0;
            this.cmbBoxFiltros.Visible = false;
            // 
            // chkBoxFiltros
            // 
            this.chkBoxFiltros.AutoSize = true;
            this.chkBoxFiltros.Location = new System.Drawing.Point(15, 150);
            this.chkBoxFiltros.Name = "chkBoxFiltros";
            this.chkBoxFiltros.Size = new System.Drawing.Size(112, 17);
            this.chkBoxFiltros.TabIndex = 8;
            this.chkBoxFiltros.Text = "Filtros excluyentes";
            this.chkBoxFiltros.UseVisualStyleBackColor = true;
            this.chkBoxFiltros.CheckedChanged += new System.EventHandler(this.chkBoxFiltros_CheckedChanged);
            // 
            // checkBoxBackup
            // 
            this.checkBoxBackup.AutoSize = true;
            this.checkBoxBackup.Location = new System.Drawing.Point(15, 127);
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.checkBoxBackup.Size = new System.Drawing.Size(63, 17);
            this.checkBoxBackup.TabIndex = 7;
            this.checkBoxBackup.Text = "Backup";
            this.checkBoxBackup.UseVisualStyleBackColor = true;
            this.checkBoxBackup.CheckedChanged += new System.EventHandler(this.checkBoxBackup_CheckedChanged);
            // 
            // textBackup
            // 
            this.textBackup.Location = new System.Drawing.Point(135, 125);
            this.textBackup.Name = "textBackup";
            this.textBackup.ReadOnly = true;
            this.textBackup.Size = new System.Drawing.Size(444, 20);
            this.textBackup.TabIndex = 0;
            this.textBackup.Visible = false;
            this.textBackup.MouseHover += new System.EventHandler(this.textBackup_MouseHover);
            // 
            // btnRutaBackup
            // 
            this.btnRutaBackup.Location = new System.Drawing.Point(585, 124);
            this.btnRutaBackup.Name = "btnRutaBackup";
            this.btnRutaBackup.Size = new System.Drawing.Size(33, 21);
            this.btnRutaBackup.TabIndex = 0;
            this.btnRutaBackup.Text = "...";
            this.btnRutaBackup.UseVisualStyleBackColor = true;
            this.btnRutaBackup.Visible = false;
            this.btnRutaBackup.Click += new System.EventHandler(this.btnRutaBackup_Click);
            // 
            // textDestino
            // 
            this.textDestino.Location = new System.Drawing.Point(135, 92);
            this.textDestino.Name = "textDestino";
            this.textDestino.Size = new System.Drawing.Size(444, 20);
            this.textDestino.TabIndex = 4;
            this.textDestino.MouseHover += new System.EventHandler(this.textDestino_MouseHover);
            // 
            // cmbProyecto
            // 
            this.cmbProyecto.FormattingEnabled = true;
            this.cmbProyecto.Location = new System.Drawing.Point(135, 37);
            this.cmbProyecto.Name = "cmbProyecto";
            this.cmbProyecto.Size = new System.Drawing.Size(444, 21);
            this.cmbProyecto.TabIndex = 1;
            this.cmbProyecto.SelectedIndexChanged += new System.EventHandler(this.cmbProyecto_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Configuración";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Origen";
            // 
            // btnRutaDestino
            // 
            this.btnRutaDestino.Location = new System.Drawing.Point(585, 91);
            this.btnRutaDestino.Name = "btnRutaDestino";
            this.btnRutaDestino.Size = new System.Drawing.Size(33, 21);
            this.btnRutaDestino.TabIndex = 5;
            this.btnRutaDestino.Text = "...";
            this.btnRutaDestino.UseVisualStyleBackColor = true;
            this.btnRutaDestino.Click += new System.EventHandler(this.btnSubirArchivo2_Click);
            // 
            // textOrigen
            // 
            this.textOrigen.Location = new System.Drawing.Point(135, 66);
            this.textOrigen.Name = "textOrigen";
            this.textOrigen.Size = new System.Drawing.Size(444, 20);
            this.textOrigen.TabIndex = 2;
            this.textOrigen.MouseHover += new System.EventHandler(this.textOrigen_MouseHover);
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
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(831, 428);
            this.splitContainer2.SplitterDistance = 396;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(396, 428);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeViewOrigen
            // 
            this.treeViewOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewOrigen.Location = new System.Drawing.Point(3, 53);
            this.treeViewOrigen.Name = "treeViewOrigen";
            this.treeViewOrigen.Size = new System.Drawing.Size(390, 372);
            this.treeViewOrigen.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblArchivosOrigen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 44);
            this.groupBox1.TabIndex = 0;
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
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutDestino);
            this.splitContainer3.Size = new System.Drawing.Size(431, 428);
            this.splitContainer3.SplitterDistance = 48;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 48);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destino";
            // 
            // tableLayoutDestino
            // 
            this.tableLayoutDestino.AutoScroll = true;
            this.tableLayoutDestino.ColumnCount = 1;
            this.tableLayoutDestino.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDestino.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutDestino.Name = "tableLayoutDestino";
            this.tableLayoutDestino.RowCount = 1;
            this.tableLayoutDestino.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutDestino.Size = new System.Drawing.Size(431, 376);
            this.tableLayoutDestino.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 428);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // folderBrowserDlg
            // 
            this.folderBrowserDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnPrevisualizar
            // 
            this.btnPrevisualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevisualizar.BackgroundImage = global::Actualizator.Properties.Resources.view;
            this.btnPrevisualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrevisualizar.Location = new System.Drawing.Point(671, 76);
            this.btnPrevisualizar.Name = "btnPrevisualizar";
            this.btnPrevisualizar.Size = new System.Drawing.Size(151, 36);
            this.btnPrevisualizar.TabIndex = 0;
            this.btnPrevisualizar.Text = "Previsualizar     sincronización";
            this.btnPrevisualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrevisualizar.UseVisualStyleBackColor = true;
            this.btnPrevisualizar.Visible = false;
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.SystemColors.Control;
            this.btnBorrar.Image = global::Actualizator.Properties.Resources.delete;
            this.btnBorrar.Location = new System.Drawing.Point(80, 3);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(30, 32);
            this.btnBorrar.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnBorrar, "Borrar configuración");
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.Visible = false;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnRecargar
            // 
            this.btnRecargar.BackColor = System.Drawing.SystemColors.Control;
            this.btnRecargar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnRecargar.Image = global::Actualizator.Properties.Resources.document_refresh;
            this.btnRecargar.Location = new System.Drawing.Point(41, 3);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Size = new System.Drawing.Size(33, 32);
            this.btnRecargar.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnRecargar, "Recargar Configuración");
            this.btnRecargar.UseVisualStyleBackColor = false;
            this.btnRecargar.Visible = false;
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.Image = global::Actualizator.Properties.Resources.disk_blue;
            this.btnGuardar.Location = new System.Drawing.Point(6, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(29, 32);
            this.btnGuardar.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnGuardar, "Guardar configuración");
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.BackgroundImage = global::Actualizator.Properties.Resources.refresh;
            this.btnActualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnActualizar.Location = new System.Drawing.Point(673, 12);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(151, 23);
            this.btnActualizar.TabIndex = 9;
            this.btnActualizar.Text = "Actualizar configuración";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnRestaurarBackup
            // 
            this.btnRestaurarBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurarBackup.BackgroundImage = global::Actualizator.Properties.Resources.recycle;
            this.btnRestaurarBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRestaurarBackup.Location = new System.Drawing.Point(673, 121);
            this.btnRestaurarBackup.Name = "btnRestaurarBackup";
            this.btnRestaurarBackup.Size = new System.Drawing.Size(151, 26);
            this.btnRestaurarBackup.TabIndex = 0;
            this.btnRestaurarBackup.Text = "Restaurar backup";
            this.btnRestaurarBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestaurarBackup.UseVisualStyleBackColor = true;
            this.btnRestaurarBackup.Visible = false;
            this.btnRestaurarBackup.Click += new System.EventHandler(this.btnRestaurarBackup_Click);
            // 
            // btnAddProyecto
            // 
            this.btnAddProyecto.Image = global::Actualizator.Properties.Resources.add;
            this.btnAddProyecto.Location = new System.Drawing.Point(585, 37);
            this.btnAddProyecto.Name = "btnAddProyecto";
            this.btnAddProyecto.Size = new System.Drawing.Size(33, 25);
            this.btnAddProyecto.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnAddProyecto, "Nueva Configuración");
            this.btnAddProyecto.UseVisualStyleBackColor = true;
            this.btnAddProyecto.Visible = false;
            this.btnAddProyecto.Click += new System.EventHandler(this.btnAddProyecto_Click);
            // 
            // btnModificarFiltros
            // 
            this.btnModificarFiltros.BackgroundImage = global::Actualizator.Properties.Resources.gear;
            this.btnModificarFiltros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnModificarFiltros.Location = new System.Drawing.Point(317, 151);
            this.btnModificarFiltros.Name = "btnModificarFiltros";
            this.btnModificarFiltros.Size = new System.Drawing.Size(131, 24);
            this.btnModificarFiltros.TabIndex = 0;
            this.btnModificarFiltros.Text = "Configurar filtros";
            this.btnModificarFiltros.UseVisualStyleBackColor = true;
            this.btnModificarFiltros.Visible = false;
            this.btnModificarFiltros.Click += new System.EventHandler(this.btnAddFiltros_Click);
            // 
            // btnVerCarpetaDestino
            // 
            this.btnVerCarpetaDestino.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVerCarpetaDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerCarpetaDestino.Image = global::Actualizator.Properties.Resources.document_into;
            this.btnVerCarpetaDestino.Location = new System.Drawing.Point(624, 90);
            this.btnVerCarpetaDestino.Name = "btnVerCarpetaDestino";
            this.btnVerCarpetaDestino.Size = new System.Drawing.Size(27, 22);
            this.btnVerCarpetaDestino.TabIndex = 6;
            this.toolTipControl.SetToolTip(this.btnVerCarpetaDestino, "Agregar destino");
            this.btnVerCarpetaDestino.UseVisualStyleBackColor = true;
            this.btnVerCarpetaDestino.Click += new System.EventHandler(this.btnVerCarpetaDestino_Click);
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSincronizar.BackgroundImage = global::Actualizator.Properties.Resources.document_exchange;
            this.btnSincronizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSincronizar.Location = new System.Drawing.Point(673, 42);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(151, 28);
            this.btnSincronizar.TabIndex = 0;
            this.btnSincronizar.Text = "Sincronizar carpetas";
            this.btnSincronizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Visible = false;
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
            // 
            // txtProyecto
            // 
            this.txtProyecto.Location = new System.Drawing.Point(135, 38);
            this.txtProyecto.Name = "txtProyecto";
            this.txtProyecto.Size = new System.Drawing.Size(420, 20);
            this.txtProyecto.TabIndex = 10;
            this.txtProyecto.Visible = false;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 633);
            this.Controls.Add(this.splitContainer1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPrincipal";
            this.Text = "Sincronizar carpetas";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textDestino;
        private System.Windows.Forms.ComboBox cmbProyecto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnVerCarpetaDestino;
        private System.Windows.Forms.Button btnSincronizar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRutaDestino;
        private System.Windows.Forms.TextBox textOrigen;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.SplitContainer splitContainer2;
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
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDestino;
        private System.Windows.Forms.Button btnAddProyecto;
        private System.Windows.Forms.Button btnRestaurarBackup;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnRutaOrigen;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnRecargar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnPrevisualizar;
        private System.Windows.Forms.TextBox txtProyecto;
    }
}

