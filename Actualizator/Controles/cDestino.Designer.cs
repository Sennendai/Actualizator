namespace Actualizator
{
    partial class cDestino
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.contextMenuDestino = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.borrarDestinoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblExpandir = new System.Windows.Forms.Label();
            this.chkBoxDestino = new System.Windows.Forms.CheckBox();
            this.btnBorrarDestino = new System.Windows.Forms.Button();
            this.lblRutaDestino = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblArchivosDestino = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.treeViewDestino = new System.Windows.Forms.TreeView();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuDestino.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.ContextMenuStrip = this.contextMenuDestino;
            this.splitContainer.Panel1.Controls.Add(this.lblExpandir);
            this.splitContainer.Panel1.Controls.Add(this.chkBoxDestino);
            this.splitContainer.Panel1.Controls.Add(this.btnBorrarDestino);
            this.splitContainer.Panel1.Controls.Add(this.lblRutaDestino);
            this.splitContainer.Panel1.Controls.Add(this.label2);
            this.splitContainer.Panel1.Controls.Add(this.lblArchivosDestino);
            this.splitContainer.Panel1.Controls.Add(this.label3);
            this.splitContainer.Panel1.Click += new System.EventHandler(this.splitContainer1_Panel1_Click);
            this.splitContainer.Panel1MinSize = 80;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.treeViewDestino);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Size = new System.Drawing.Size(318, 117);
            this.splitContainer.SplitterDistance = 80;
            this.splitContainer.TabIndex = 0;
            // 
            // contextMenuDestino
            // 
            this.contextMenuDestino.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borrarDestinoToolStripMenuItem});
            this.contextMenuDestino.Name = "contextMenuDestino";
            this.contextMenuDestino.Size = new System.Drawing.Size(149, 26);
            // 
            // borrarDestinoToolStripMenuItem
            // 
            this.borrarDestinoToolStripMenuItem.Name = "borrarDestinoToolStripMenuItem";
            this.borrarDestinoToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.borrarDestinoToolStripMenuItem.Text = "Borrar destino";
            this.borrarDestinoToolStripMenuItem.Click += new System.EventHandler(this.borrarDestinoToolStripMenuItem_Click);
            // 
            // lblExpandir
            // 
            this.lblExpandir.AutoSize = true;
            this.lblExpandir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpandir.Location = new System.Drawing.Point(115, 74);
            this.lblExpandir.Name = "lblExpandir";
            this.lblExpandir.Size = new System.Drawing.Size(58, 13);
            this.lblExpandir.TabIndex = 40;
            this.lblExpandir.Text = "lblExpandir";
            this.lblExpandir.Click += new System.EventHandler(this.lblExpandir_Click);
            // 
            // chkBoxDestino
            // 
            this.chkBoxDestino.AutoSize = true;
            this.chkBoxDestino.Checked = true;
            this.chkBoxDestino.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxDestino.Location = new System.Drawing.Point(3, 3);
            this.chkBoxDestino.Name = "chkBoxDestino";
            this.chkBoxDestino.Size = new System.Drawing.Size(15, 14);
            this.chkBoxDestino.TabIndex = 39;
            this.chkBoxDestino.ThreeState = true;
            this.chkBoxDestino.UseVisualStyleBackColor = true;
            // 
            // btnBorrarDestino
            // 
            this.btnBorrarDestino.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrarDestino.BackColor = System.Drawing.SystemColors.Control;
            this.btnBorrarDestino.Image = global::Actualizator.Properties.Resources.delete;
            this.btnBorrarDestino.Location = new System.Drawing.Point(284, 3);
            this.btnBorrarDestino.Name = "btnBorrarDestino";
            this.btnBorrarDestino.Size = new System.Drawing.Size(30, 25);
            this.btnBorrarDestino.TabIndex = 38;
            this.toolTipControl.SetToolTip(this.btnBorrarDestino, "Borrar Destino");
            this.btnBorrarDestino.UseVisualStyleBackColor = false;
            this.btnBorrarDestino.Click += new System.EventHandler(this.btnBorrarDestino_Click);
            // 
            // lblRutaDestino
            // 
            this.lblRutaDestino.AutoSize = true;
            this.lblRutaDestino.Location = new System.Drawing.Point(78, 33);
            this.lblRutaDestino.Name = "lblRutaDestino";
            this.lblRutaDestino.Size = new System.Drawing.Size(76, 13);
            this.lblRutaDestino.TabIndex = 5;
            this.lblRutaDestino.Text = "lblRutaDestino";
            this.lblRutaDestino.MouseHover += new System.EventHandler(this.lblRutaDestino_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destino: ";
            // 
            // lblArchivosDestino
            // 
            this.lblArchivosDestino.AutoSize = true;
            this.lblArchivosDestino.Location = new System.Drawing.Point(78, 62);
            this.lblArchivosDestino.Name = "lblArchivosDestino";
            this.lblArchivosDestino.Size = new System.Drawing.Size(13, 13);
            this.lblArchivosDestino.TabIndex = 3;
            this.lblArchivosDestino.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Archivos: ";
            // 
            // treeViewDestino
            // 
            this.treeViewDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDestino.Location = new System.Drawing.Point(0, 0);
            this.treeViewDestino.Name = "treeViewDestino";
            this.treeViewDestino.Size = new System.Drawing.Size(150, 46);
            this.treeViewDestino.TabIndex = 0;
            // 
            // cDestino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer);
            this.Name = "cDestino";
            this.Size = new System.Drawing.Size(317, 152);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuDestino.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblArchivosDestino;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeViewDestino;
        private System.Windows.Forms.Label lblRutaDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.ToolStripMenuItem borrarDestinoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuDestino;
        private System.Windows.Forms.Button btnBorrarDestino;
        private System.Windows.Forms.CheckBox chkBoxDestino;
        private System.Windows.Forms.Label lblExpandir;
    }
}
