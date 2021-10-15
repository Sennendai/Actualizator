namespace Actualizator
{
    partial class Destino
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblRutaDestino = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblArchivosDestino = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.treeViewDestino = new System.Windows.Forms.TreeView();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.borrarDestinoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDestino = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuDestino.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.ContextMenuStrip = this.contextMenuDestino;
            this.splitContainer1.Panel1.Controls.Add(this.lblRutaDestino);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lblArchivosDestino);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Click += new System.EventHandler(this.splitContainer1_Panel1_Click);
            this.splitContainer1.Panel1MinSize = 80;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeViewDestino);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(386, 100);
            this.splitContainer1.SplitterDistance = 75;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblRutaDestino
            // 
            this.lblRutaDestino.AutoSize = true;
            this.lblRutaDestino.Location = new System.Drawing.Point(83, 17);
            this.lblRutaDestino.Name = "lblRutaDestino";
            this.lblRutaDestino.Size = new System.Drawing.Size(76, 13);
            this.lblRutaDestino.TabIndex = 5;
            this.lblRutaDestino.Text = "lblRutaDestino";
            this.lblRutaDestino.MouseHover += new System.EventHandler(this.lblRutaDestino_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destino: ";
            // 
            // lblArchivosDestino
            // 
            this.lblArchivosDestino.AutoSize = true;
            this.lblArchivosDestino.Location = new System.Drawing.Point(83, 46);
            this.lblArchivosDestino.Name = "lblArchivosDestino";
            this.lblArchivosDestino.Size = new System.Drawing.Size(13, 13);
            this.lblArchivosDestino.TabIndex = 3;
            this.lblArchivosDestino.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 46);
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
            // borrarDestinoToolStripMenuItem
            // 
            this.borrarDestinoToolStripMenuItem.Name = "borrarDestinoToolStripMenuItem";
            this.borrarDestinoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.borrarDestinoToolStripMenuItem.Text = "Borrar destino";
            this.borrarDestinoToolStripMenuItem.Click += new System.EventHandler(this.borrarDestinoToolStripMenuItem_Click);
            // 
            // contextMenuDestino
            // 
            this.contextMenuDestino.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borrarDestinoToolStripMenuItem});
            this.contextMenuDestino.Name = "contextMenuDestino";
            this.contextMenuDestino.Size = new System.Drawing.Size(149, 26);
            // 
            // Destino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.MaximumSize = new System.Drawing.Size(1000, 350);
            this.Name = "Destino";
            this.Size = new System.Drawing.Size(386, 100);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuDestino.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblArchivosDestino;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeViewDestino;
        private System.Windows.Forms.Label lblRutaDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.ToolStripMenuItem borrarDestinoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuDestino;
    }
}
