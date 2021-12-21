namespace Actualizator
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.splitContainerPrincipal = new System.Windows.Forms.SplitContainer();
            this.chkCopiarArchivos = new System.Windows.Forms.CheckBox();
            this.btnCopiarProyecto = new System.Windows.Forms.Button();
            this.lblFiltrosIncluyentes = new System.Windows.Forms.Label();
            this.btnFiltrosIncluyentes = new System.Windows.Forms.Button();
            this.chkBoxFiltrosIncluyentes = new System.Windows.Forms.CheckBox();
            this.chkBorrarDestino = new System.Windows.Forms.CheckBox();
            this.lblFiltrosCount = new System.Windows.Forms.Label();
            this.chkBoxSobreescribir = new System.Windows.Forms.CheckBox();
            this.txtProyecto = new System.Windows.Forms.TextBox();
            this.btnPrevisualizar = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnRecargar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnRutaOrigen = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnRestaurarBackup = new System.Windows.Forms.Button();
            this.btnAddProyecto = new System.Windows.Forms.Button();
            this.btnModificarFiltros = new System.Windows.Forms.Button();
            this.chkBoxFiltros = new System.Windows.Forms.CheckBox();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.textBackup = new System.Windows.Forms.TextBox();
            this.btnRutaBackup = new System.Windows.Forms.Button();
            this.textDestino = new System.Windows.Forms.TextBox();
            this.cmbProyecto = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnVerCarpetaDestino = new System.Windows.Forms.Button();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRutaDestino = new System.Windows.Forms.Button();
            this.textOrigen = new System.Windows.Forms.TextBox();
            this.splitContainerOrigenDestino = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeViewOrigen = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirCarpetaOrigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxOrigen = new System.Windows.Forms.GroupBox();
            this.lblArchivosOrigen = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBoxDestino = new System.Windows.Forms.GroupBox();
            this.lblCountDestinos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutDestino = new System.Windows.Forms.TableLayoutPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainerForm = new System.Windows.Forms.SplitContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.flpBotonesControl = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).BeginInit();
            this.splitContainerPrincipal.Panel1.SuspendLayout();
            this.splitContainerPrincipal.Panel2.SuspendLayout();
            this.splitContainerPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOrigenDestino)).BeginInit();
            this.splitContainerOrigenDestino.Panel1.SuspendLayout();
            this.splitContainerOrigenDestino.Panel2.SuspendLayout();
            this.splitContainerOrigenDestino.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.groupBoxOrigen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBoxDestino.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).BeginInit();
            this.splitContainerForm.Panel1.SuspendLayout();
            this.splitContainerForm.Panel2.SuspendLayout();
            this.splitContainerForm.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.flpBotonesControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerPrincipal
            // 
            this.splitContainerPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerPrincipal.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPrincipal.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPrincipal.Name = "splitContainerPrincipal";
            this.splitContainerPrincipal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPrincipal.Panel1
            // 
            this.splitContainerPrincipal.Panel1.Controls.Add(this.flpBotonesControl);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.chkCopiarArchivos);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblFiltrosIncluyentes);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnFiltrosIncluyentes);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.chkBoxFiltrosIncluyentes);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.chkBorrarDestino);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblFiltrosCount);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.chkBoxSobreescribir);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtProyecto);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnPrevisualizar);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnRutaOrigen);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnActualizar);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnRestaurarBackup);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnModificarFiltros);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.chkBoxFiltros);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.checkBoxBackup);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.textBackup);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnRutaBackup);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.textDestino);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.cmbProyecto);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.label5);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnVerCarpetaDestino);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnSincronizar);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.label2);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.label1);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnRutaDestino);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.textOrigen);
            this.toolTipControl.SetToolTip(this.splitContainerPrincipal.Panel1, "Copiar configuración");
            this.splitContainerPrincipal.Panel1MinSize = 220;
            // 
            // splitContainerPrincipal.Panel2
            // 
            this.splitContainerPrincipal.Panel2.Controls.Add(this.splitContainerOrigenDestino);
            this.splitContainerPrincipal.Panel2.Controls.Add(this.splitter1);
            this.splitContainerPrincipal.Size = new System.Drawing.Size(859, 541);
            this.splitContainerPrincipal.SplitterDistance = 229;
            this.splitContainerPrincipal.TabIndex = 1;
            // 
            // chkCopiarArchivos
            // 
            this.chkCopiarArchivos.Checked = true;
            this.chkCopiarArchivos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopiarArchivos.Enabled = false;
            this.chkCopiarArchivos.Image = global::Actualizator.Properties.Resources.document_add1;
            this.chkCopiarArchivos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkCopiarArchivos.Location = new System.Drawing.Point(677, 105);
            this.chkCopiarArchivos.Name = "chkCopiarArchivos";
            this.chkCopiarArchivos.Size = new System.Drawing.Size(132, 26);
            this.chkCopiarArchivos.TabIndex = 25;
            this.chkCopiarArchivos.Text = "Sobreescribir nuevos";
            this.chkCopiarArchivos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkCopiarArchivos.UseVisualStyleBackColor = true;
            this.chkCopiarArchivos.CheckedChanged += new System.EventHandler(this.chkCopiarArchivos_CheckedChanged);
            // 
            // btnCopiarProyecto
            // 
            this.btnCopiarProyecto.BackColor = System.Drawing.SystemColors.Control;
            this.btnCopiarProyecto.Enabled = false;
            this.btnCopiarProyecto.Image = global::Actualizator.Properties.Resources.copy;
            this.btnCopiarProyecto.Location = new System.Drawing.Point(143, 3);
            this.btnCopiarProyecto.Name = "btnCopiarProyecto";
            this.btnCopiarProyecto.Size = new System.Drawing.Size(29, 32);
            this.btnCopiarProyecto.TabIndex = 24;
            this.toolTipControl.SetToolTip(this.btnCopiarProyecto, "Copiar configuración");
            this.btnCopiarProyecto.UseVisualStyleBackColor = true;
            this.btnCopiarProyecto.Click += new System.EventHandler(this.btnCopiarProyecto_Click);
            // 
            // lblFiltrosIncluyentes
            // 
            this.lblFiltrosIncluyentes.AutoSize = true;
            this.lblFiltrosIncluyentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltrosIncluyentes.Location = new System.Drawing.Point(250, 188);
            this.lblFiltrosIncluyentes.Name = "lblFiltrosIncluyentes";
            this.lblFiltrosIncluyentes.Size = new System.Drawing.Size(98, 13);
            this.lblFiltrosIncluyentes.TabIndex = 23;
            this.lblFiltrosIncluyentes.Text = "lblFiltrosIncluyentes";
            this.lblFiltrosIncluyentes.Visible = false;
            // 
            // btnFiltrosIncluyentes
            // 
            this.btnFiltrosIncluyentes.BackgroundImage = global::Actualizator.Properties.Resources.gear;
            this.btnFiltrosIncluyentes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFiltrosIncluyentes.Enabled = false;
            this.btnFiltrosIncluyentes.Location = new System.Drawing.Point(135, 182);
            this.btnFiltrosIncluyentes.Name = "btnFiltrosIncluyentes";
            this.btnFiltrosIncluyentes.Size = new System.Drawing.Size(109, 24);
            this.btnFiltrosIncluyentes.TabIndex = 21;
            this.btnFiltrosIncluyentes.Text = "Configurar filtros";
            this.btnFiltrosIncluyentes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFiltrosIncluyentes.UseVisualStyleBackColor = true;
            this.btnFiltrosIncluyentes.Click += new System.EventHandler(this.btnFiltrosIncluyentes_Click);
            // 
            // chkBoxFiltrosIncluyentes
            // 
            this.chkBoxFiltrosIncluyentes.AutoSize = true;
            this.chkBoxFiltrosIncluyentes.Location = new System.Drawing.Point(15, 181);
            this.chkBoxFiltrosIncluyentes.Name = "chkBoxFiltrosIncluyentes";
            this.chkBoxFiltrosIncluyentes.Size = new System.Drawing.Size(109, 17);
            this.chkBoxFiltrosIncluyentes.TabIndex = 22;
            this.chkBoxFiltrosIncluyentes.Text = "Filtros incluyentes";
            this.toolTipControl.SetToolTip(this.chkBoxFiltrosIncluyentes, "Para incluir solo estos archivos");
            this.chkBoxFiltrosIncluyentes.UseVisualStyleBackColor = true;
            this.chkBoxFiltrosIncluyentes.CheckedChanged += new System.EventHandler(this.chkBoxFiltrosIncluyentes_CheckedChanged);
            // 
            // chkBorrarDestino
            // 
            this.chkBorrarDestino.Enabled = false;
            this.chkBorrarDestino.Image = global::Actualizator.Properties.Resources.delete;
            this.chkBorrarDestino.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkBorrarDestino.Location = new System.Drawing.Point(675, 157);
            this.chkBorrarDestino.Name = "chkBorrarDestino";
            this.chkBorrarDestino.Size = new System.Drawing.Size(167, 26);
            this.chkBorrarDestino.TabIndex = 17;
            this.chkBorrarDestino.Text = "Borrar archivos del destino";
            this.chkBorrarDestino.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTipControl.SetToolTip(this.chkBorrarDestino, "Borrar todos los archivos y luego copiar");
            this.chkBorrarDestino.UseVisualStyleBackColor = true;
            this.chkBorrarDestino.CheckedChanged += new System.EventHandler(this.chkBorrarDestino_CheckedChanged);
            // 
            // lblFiltrosCount
            // 
            this.lblFiltrosCount.AutoSize = true;
            this.lblFiltrosCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltrosCount.Location = new System.Drawing.Point(250, 165);
            this.lblFiltrosCount.Name = "lblFiltrosCount";
            this.lblFiltrosCount.Size = new System.Drawing.Size(72, 13);
            this.lblFiltrosCount.TabIndex = 16;
            this.lblFiltrosCount.Text = "lblFiltrosCount";
            this.lblFiltrosCount.Visible = false;
            // 
            // chkBoxSobreescribir
            // 
            this.chkBoxSobreescribir.Enabled = false;
            this.chkBoxSobreescribir.Image = global::Actualizator.Properties.Resources.warning;
            this.chkBoxSobreescribir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkBoxSobreescribir.Location = new System.Drawing.Point(675, 130);
            this.chkBoxSobreescribir.Name = "chkBoxSobreescribir";
            this.chkBoxSobreescribir.Size = new System.Drawing.Size(132, 26);
            this.chkBoxSobreescribir.TabIndex = 14;
            this.chkBoxSobreescribir.Text = "Sobreescribir todos";
            this.chkBoxSobreescribir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkBoxSobreescribir.UseVisualStyleBackColor = true;
            this.chkBoxSobreescribir.CheckedChanged += new System.EventHandler(this.chkBoxSobreescribir_CheckedChanged);
            // 
            // txtProyecto
            // 
            this.txtProyecto.Location = new System.Drawing.Point(135, 46);
            this.txtProyecto.Name = "txtProyecto";
            this.txtProyecto.Size = new System.Drawing.Size(420, 20);
            this.txtProyecto.TabIndex = 10;
            this.txtProyecto.Visible = false;
            // 
            // btnPrevisualizar
            // 
            this.btnPrevisualizar.BackgroundImage = global::Actualizator.Properties.Resources.view;
            this.btnPrevisualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrevisualizar.Enabled = false;
            this.btnPrevisualizar.Location = new System.Drawing.Point(675, 76);
            this.btnPrevisualizar.Name = "btnPrevisualizar";
            this.btnPrevisualizar.Size = new System.Drawing.Size(167, 28);
            this.btnPrevisualizar.TabIndex = 0;
            this.btnPrevisualizar.Text = "Previsualizar sincronización";
            this.btnPrevisualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrevisualizar.UseVisualStyleBackColor = true;
            this.btnPrevisualizar.Click += new System.EventHandler(this.btnPrevisualizar_Click);
            // 
            // lblLog
            // 
            this.lblLog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(178, 12);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(35, 13);
            this.lblLog.TabIndex = 0;
            this.lblLog.Text = "lblLog";
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.SystemColors.Control;
            this.btnBorrar.Enabled = false;
            this.btnBorrar.Image = global::Actualizator.Properties.Resources.delete;
            this.btnBorrar.Location = new System.Drawing.Point(108, 3);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(29, 32);
            this.btnBorrar.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnBorrar, "Borrar configuración");
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnRecargar
            // 
            this.btnRecargar.BackColor = System.Drawing.SystemColors.Control;
            this.btnRecargar.Enabled = false;
            this.btnRecargar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnRecargar.Image = global::Actualizator.Properties.Resources.folder_refresh;
            this.btnRecargar.Location = new System.Drawing.Point(73, 3);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Size = new System.Drawing.Size(29, 32);
            this.btnRecargar.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnRecargar, "Recargar Configuración");
            this.btnRecargar.UseVisualStyleBackColor = true;
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.Image = global::Actualizator.Properties.Resources.disk_blue;
            this.btnGuardar.Location = new System.Drawing.Point(3, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(29, 32);
            this.btnGuardar.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnGuardar, "Guardar configuración");
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnRutaOrigen
            // 
            this.btnRutaOrigen.Location = new System.Drawing.Point(585, 76);
            this.btnRutaOrigen.Name = "btnRutaOrigen";
            this.btnRutaOrigen.Size = new System.Drawing.Size(33, 20);
            this.btnRutaOrigen.TabIndex = 3;
            this.btnRutaOrigen.Text = "...";
            this.btnRutaOrigen.UseVisualStyleBackColor = true;
            this.btnRutaOrigen.Click += new System.EventHandler(this.btnRutaOrigen_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackgroundImage = global::Actualizator.Properties.Resources.refresh;
            this.btnActualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnActualizar.Enabled = false;
            this.btnActualizar.Location = new System.Drawing.Point(677, 12);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(165, 23);
            this.btnActualizar.TabIndex = 9;
            this.btnActualizar.Text = "Actualizar configuración";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnRestaurarBackup
            // 
            this.btnRestaurarBackup.BackgroundImage = global::Actualizator.Properties.Resources.recycle;
            this.btnRestaurarBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRestaurarBackup.Enabled = false;
            this.btnRestaurarBackup.Location = new System.Drawing.Point(675, 189);
            this.btnRestaurarBackup.Name = "btnRestaurarBackup";
            this.btnRestaurarBackup.Size = new System.Drawing.Size(167, 26);
            this.btnRestaurarBackup.TabIndex = 0;
            this.btnRestaurarBackup.Text = "Restaurar backup";
            this.btnRestaurarBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestaurarBackup.UseVisualStyleBackColor = true;
            this.btnRestaurarBackup.Click += new System.EventHandler(this.btnRestaurarBackup_Click);
            // 
            // btnAddProyecto
            // 
            this.btnAddProyecto.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddProyecto.Enabled = false;
            this.btnAddProyecto.Image = global::Actualizator.Properties.Resources.add;
            this.btnAddProyecto.Location = new System.Drawing.Point(38, 3);
            this.btnAddProyecto.Name = "btnAddProyecto";
            this.btnAddProyecto.Size = new System.Drawing.Size(29, 32);
            this.btnAddProyecto.TabIndex = 0;
            this.toolTipControl.SetToolTip(this.btnAddProyecto, "Nueva Configuración");
            this.btnAddProyecto.UseVisualStyleBackColor = true;
            this.btnAddProyecto.Click += new System.EventHandler(this.btnAddProyecto_Click);
            // 
            // btnModificarFiltros
            // 
            this.btnModificarFiltros.BackgroundImage = global::Actualizator.Properties.Resources.gear;
            this.btnModificarFiltros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnModificarFiltros.Enabled = false;
            this.btnModificarFiltros.Location = new System.Drawing.Point(135, 159);
            this.btnModificarFiltros.Name = "btnModificarFiltros";
            this.btnModificarFiltros.Size = new System.Drawing.Size(109, 24);
            this.btnModificarFiltros.TabIndex = 0;
            this.btnModificarFiltros.Text = "Configurar filtros";
            this.btnModificarFiltros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModificarFiltros.UseVisualStyleBackColor = true;
            this.btnModificarFiltros.Click += new System.EventHandler(this.btnAddFiltros_Click);
            // 
            // chkBoxFiltros
            // 
            this.chkBoxFiltros.AutoSize = true;
            this.chkBoxFiltros.Location = new System.Drawing.Point(15, 158);
            this.chkBoxFiltros.Name = "chkBoxFiltros";
            this.chkBoxFiltros.Size = new System.Drawing.Size(112, 17);
            this.chkBoxFiltros.TabIndex = 8;
            this.chkBoxFiltros.Text = "Filtros excluyentes";
            this.toolTipControl.SetToolTip(this.chkBoxFiltros, "Para excluir archivos");
            this.chkBoxFiltros.UseVisualStyleBackColor = true;
            this.chkBoxFiltros.CheckedChanged += new System.EventHandler(this.chkBoxFiltros_CheckedChanged);
            // 
            // checkBoxBackup
            // 
            this.checkBoxBackup.AutoSize = true;
            this.checkBoxBackup.Location = new System.Drawing.Point(15, 135);
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.checkBoxBackup.Size = new System.Drawing.Size(63, 17);
            this.checkBoxBackup.TabIndex = 7;
            this.checkBoxBackup.Text = "Backup";
            this.checkBoxBackup.UseVisualStyleBackColor = true;
            this.checkBoxBackup.CheckedChanged += new System.EventHandler(this.checkBoxBackup_CheckedChanged);
            // 
            // textBackup
            // 
            this.textBackup.Enabled = false;
            this.textBackup.Location = new System.Drawing.Point(135, 133);
            this.textBackup.Name = "textBackup";
            this.textBackup.ReadOnly = true;
            this.textBackup.Size = new System.Drawing.Size(444, 20);
            this.textBackup.TabIndex = 0;
            this.textBackup.MouseHover += new System.EventHandler(this.textBackup_MouseHover);
            // 
            // btnRutaBackup
            // 
            this.btnRutaBackup.Enabled = false;
            this.btnRutaBackup.Location = new System.Drawing.Point(585, 132);
            this.btnRutaBackup.Name = "btnRutaBackup";
            this.btnRutaBackup.Size = new System.Drawing.Size(33, 21);
            this.btnRutaBackup.TabIndex = 0;
            this.btnRutaBackup.Text = "...";
            this.btnRutaBackup.UseVisualStyleBackColor = true;
            this.btnRutaBackup.Click += new System.EventHandler(this.btnRutaBackup_Click);
            // 
            // textDestino
            // 
            this.textDestino.Location = new System.Drawing.Point(135, 100);
            this.textDestino.Name = "textDestino";
            this.textDestino.Size = new System.Drawing.Size(444, 20);
            this.textDestino.TabIndex = 4;
            this.textDestino.TextChanged += new System.EventHandler(this.textDestino_TextChanged);
            this.textDestino.MouseHover += new System.EventHandler(this.textDestino_MouseHover);
            // 
            // cmbProyecto
            // 
            this.cmbProyecto.FormattingEnabled = true;
            this.cmbProyecto.Location = new System.Drawing.Point(135, 45);
            this.cmbProyecto.Name = "cmbProyecto";
            this.cmbProyecto.Size = new System.Drawing.Size(444, 21);
            this.cmbProyecto.TabIndex = 1;
            this.cmbProyecto.SelectedIndexChanged += new System.EventHandler(this.cmbProyecto_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Configuración";
            // 
            // btnVerCarpetaDestino
            // 
            this.btnVerCarpetaDestino.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVerCarpetaDestino.Enabled = false;
            this.btnVerCarpetaDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerCarpetaDestino.Image = global::Actualizator.Properties.Resources.down_plus;
            this.btnVerCarpetaDestino.Location = new System.Drawing.Point(624, 98);
            this.btnVerCarpetaDestino.Name = "btnVerCarpetaDestino";
            this.btnVerCarpetaDestino.Size = new System.Drawing.Size(27, 22);
            this.btnVerCarpetaDestino.TabIndex = 6;
            this.toolTipControl.SetToolTip(this.btnVerCarpetaDestino, "Agregar destino");
            this.btnVerCarpetaDestino.UseVisualStyleBackColor = true;
            this.btnVerCarpetaDestino.Click += new System.EventHandler(this.btnVerCarpetaDestino_Click);
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.BackgroundImage = global::Actualizator.Properties.Resources.folders;
            this.btnSincronizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSincronizar.Enabled = false;
            this.btnSincronizar.Location = new System.Drawing.Point(677, 42);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(165, 28);
            this.btnSincronizar.TabIndex = 0;
            this.btnSincronizar.Text = "Sincronizar carpetas";
            this.btnSincronizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Origen";
            // 
            // btnRutaDestino
            // 
            this.btnRutaDestino.Location = new System.Drawing.Point(585, 99);
            this.btnRutaDestino.Name = "btnRutaDestino";
            this.btnRutaDestino.Size = new System.Drawing.Size(33, 21);
            this.btnRutaDestino.TabIndex = 5;
            this.btnRutaDestino.Text = "...";
            this.btnRutaDestino.UseVisualStyleBackColor = true;
            this.btnRutaDestino.Click += new System.EventHandler(this.btnSubirArchivo2_Click);
            // 
            // textOrigen
            // 
            this.textOrigen.Location = new System.Drawing.Point(135, 74);
            this.textOrigen.Name = "textOrigen";
            this.textOrigen.Size = new System.Drawing.Size(444, 20);
            this.textOrigen.TabIndex = 2;
            this.textOrigen.TextChanged += new System.EventHandler(this.textOrigen_TextChanged);
            this.textOrigen.MouseHover += new System.EventHandler(this.textOrigen_MouseHover);
            // 
            // splitContainerOrigenDestino
            // 
            this.splitContainerOrigenDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOrigenDestino.Location = new System.Drawing.Point(3, 0);
            this.splitContainerOrigenDestino.MinimumSize = new System.Drawing.Size(0, 308);
            this.splitContainerOrigenDestino.Name = "splitContainerOrigenDestino";
            // 
            // splitContainerOrigenDestino.Panel1
            // 
            this.splitContainerOrigenDestino.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainerOrigenDestino.Panel2
            // 
            this.splitContainerOrigenDestino.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainerOrigenDestino.Size = new System.Drawing.Size(854, 308);
            this.splitContainerOrigenDestino.SplitterDistance = 393;
            this.splitContainerOrigenDestino.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeViewOrigen, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxOrigen, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(393, 308);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeViewOrigen
            // 
            this.treeViewOrigen.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewOrigen.Location = new System.Drawing.Point(3, 53);
            this.treeViewOrigen.Name = "treeViewOrigen";
            this.treeViewOrigen.Size = new System.Drawing.Size(387, 252);
            this.treeViewOrigen.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirCarpetaOrigenToolStripMenuItem});
            this.contextMenuStrip.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(180, 26);
            // 
            // abrirCarpetaOrigenToolStripMenuItem
            // 
            this.abrirCarpetaOrigenToolStripMenuItem.Name = "abrirCarpetaOrigenToolStripMenuItem";
            this.abrirCarpetaOrigenToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.abrirCarpetaOrigenToolStripMenuItem.Text = "Abrir carpeta origen";
            this.abrirCarpetaOrigenToolStripMenuItem.Click += new System.EventHandler(this.abrirCarpetaOrigenToolStripMenuItem_Click);
            // 
            // groupBoxOrigen
            // 
            this.groupBoxOrigen.Controls.Add(this.lblArchivosOrigen);
            this.groupBoxOrigen.Controls.Add(this.label3);
            this.groupBoxOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOrigen.Location = new System.Drawing.Point(3, 3);
            this.groupBoxOrigen.Name = "groupBoxOrigen";
            this.groupBoxOrigen.Size = new System.Drawing.Size(387, 44);
            this.groupBoxOrigen.TabIndex = 0;
            this.groupBoxOrigen.TabStop = false;
            this.groupBoxOrigen.Text = "Origen";
            this.groupBoxOrigen.UseCompatibleTextRendering = true;
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
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBoxDestino);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutDestino);
            this.splitContainer3.Size = new System.Drawing.Size(457, 308);
            this.splitContainer3.SplitterDistance = 47;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBoxDestino
            // 
            this.groupBoxDestino.Controls.Add(this.lblCountDestinos);
            this.groupBoxDestino.Controls.Add(this.label6);
            this.groupBoxDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDestino.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDestino.Name = "groupBoxDestino";
            this.groupBoxDestino.Size = new System.Drawing.Size(457, 47);
            this.groupBoxDestino.TabIndex = 0;
            this.groupBoxDestino.TabStop = false;
            this.groupBoxDestino.Text = "Destino";
            // 
            // lblCountDestinos
            // 
            this.lblCountDestinos.AutoSize = true;
            this.lblCountDestinos.Location = new System.Drawing.Point(137, 20);
            this.lblCountDestinos.Name = "lblCountDestinos";
            this.lblCountDestinos.Size = new System.Drawing.Size(86, 13);
            this.lblCountDestinos.TabIndex = 3;
            this.lblCountDestinos.Text = "lblCountDestinos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Destinos seleccionados: ";
            // 
            // tableLayoutDestino
            // 
            this.tableLayoutDestino.AutoScroll = true;
            this.tableLayoutDestino.ColumnCount = 1;
            this.tableLayoutDestino.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDestino.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutDestino.Name = "tableLayoutDestino";
            this.tableLayoutDestino.Padding = new System.Windows.Forms.Padding(1);
            this.tableLayoutDestino.RowCount = 1;
            this.tableLayoutDestino.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutDestino.Size = new System.Drawing.Size(457, 257);
            this.tableLayoutDestino.TabIndex = 0;
            this.tableLayoutDestino.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tableLayoutDestino_ControlAdded);
            this.tableLayoutDestino.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.tableLayoutDestino_ControlRemoved);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 306);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // splitContainerForm
            // 
            this.splitContainerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerForm.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerForm.Location = new System.Drawing.Point(0, 0);
            this.splitContainerForm.Name = "splitContainerForm";
            this.splitContainerForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerForm.Panel1
            // 
            this.splitContainerForm.Panel1.Controls.Add(this.splitContainerPrincipal);
            // 
            // splitContainerForm.Panel2
            // 
            this.splitContainerForm.Panel2.Controls.Add(this.statusStrip);
            this.splitContainerForm.Size = new System.Drawing.Size(858, 571);
            this.splitContainerForm.SplitterDistance = 541;
            this.splitContainerForm.TabIndex = 26;
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.MaximumSize = new System.Drawing.Size(0, 26);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(858, 26);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(112, 21);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // flpBotonesControl
            // 
            this.flpBotonesControl.Controls.Add(this.btnGuardar);
            this.flpBotonesControl.Controls.Add(this.btnAddProyecto);
            this.flpBotonesControl.Controls.Add(this.btnRecargar);
            this.flpBotonesControl.Controls.Add(this.btnBorrar);
            this.flpBotonesControl.Controls.Add(this.btnCopiarProyecto);
            this.flpBotonesControl.Controls.Add(this.lblLog);
            this.flpBotonesControl.Location = new System.Drawing.Point(6, 3);
            this.flpBotonesControl.Name = "flpBotonesControl";
            this.flpBotonesControl.Size = new System.Drawing.Size(573, 39);
            this.flpBotonesControl.TabIndex = 26;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 571);
            this.Controls.Add(this.splitContainerForm);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(874, 538);
            this.Name = "frmPrincipal";
            this.Text = "Sincronizar carpetas";
            this.splitContainerPrincipal.Panel1.ResumeLayout(false);
            this.splitContainerPrincipal.Panel1.PerformLayout();
            this.splitContainerPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).EndInit();
            this.splitContainerPrincipal.ResumeLayout(false);
            this.splitContainerOrigenDestino.Panel1.ResumeLayout(false);
            this.splitContainerOrigenDestino.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOrigenDestino)).EndInit();
            this.splitContainerOrigenDestino.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBoxOrigen.ResumeLayout(false);
            this.groupBoxOrigen.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBoxDestino.ResumeLayout(false);
            this.groupBoxDestino.PerformLayout();
            this.splitContainerForm.Panel1.ResumeLayout(false);
            this.splitContainerForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).EndInit();
            this.splitContainerForm.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.flpBotonesControl.ResumeLayout(false);
            this.flpBotonesControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
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
        private System.Windows.Forms.SplitContainer splitContainerOrigenDestino;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeViewOrigen;
        private System.Windows.Forms.GroupBox groupBoxOrigen;
        private System.Windows.Forms.Label lblArchivosOrigen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxBackup;
        private System.Windows.Forms.TextBox textBackup;
        private System.Windows.Forms.Button btnRutaBackup;
        private System.Windows.Forms.Button btnModificarFiltros;
        private System.Windows.Forms.CheckBox chkBoxFiltros;
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBoxDestino;
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
        private System.Windows.Forms.CheckBox chkBoxSobreescribir;
        private System.Windows.Forms.Label lblFiltrosCount;
        private System.Windows.Forms.Label lblCountDestinos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkBorrarDestino;
        private System.Windows.Forms.Label lblFiltrosIncluyentes;
        private System.Windows.Forms.Button btnFiltrosIncluyentes;
        private System.Windows.Forms.CheckBox chkBoxFiltrosIncluyentes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem abrirCarpetaOrigenToolStripMenuItem;
        private System.Windows.Forms.Button btnCopiarProyecto;
        private System.Windows.Forms.CheckBox chkCopiarArchivos;
        private System.Windows.Forms.SplitContainer splitContainerForm;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel flpBotonesControl;
    }
}

