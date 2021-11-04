namespace Actualizator.Controles
{
    partial class cPrevisualizarDestino
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
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.btnExpandir = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lblDestino = new System.Windows.Forms.Label();
            this.treeViewDestino = new System.Windows.Forms.TreeView();
            this.lblTotalArchivos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExpandir
            // 
            this.btnExpandir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpandir.Image = global::Actualizator.Properties.Resources.window_view;
            this.btnExpandir.Location = new System.Drawing.Point(356, 3);
            this.btnExpandir.Name = "btnExpandir";
            this.btnExpandir.Size = new System.Drawing.Size(25, 23);
            this.btnExpandir.TabIndex = 2;
            this.btnExpandir.UseVisualStyleBackColor = true;
            this.btnExpandir.Click += new System.EventHandler(this.btnExpandir_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lblTotalArchivos);
            this.splitContainer.Panel1.Controls.Add(this.lblDestino);
            this.splitContainer.Panel1.Controls.Add(this.btnExpandir);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.treeViewDestino);
            this.splitContainer.Size = new System.Drawing.Size(384, 91);
            this.splitContainer.SplitterDistance = 55;
            this.splitContainer.TabIndex = 3;
            // 
            // lblDestino
            // 
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(3, 8);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(53, 13);
            this.lblDestino.TabIndex = 3;
            this.lblDestino.Text = "lblDestino";
            // 
            // treeViewDestino
            // 
            this.treeViewDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDestino.Location = new System.Drawing.Point(0, 0);
            this.treeViewDestino.Name = "treeViewDestino";
            this.treeViewDestino.Size = new System.Drawing.Size(384, 32);
            this.treeViewDestino.TabIndex = 0;
            // 
            // lblTotalArchivos
            // 
            this.lblTotalArchivos.AutoSize = true;
            this.lblTotalArchivos.Location = new System.Drawing.Point(3, 32);
            this.lblTotalArchivos.Name = "lblTotalArchivos";
            this.lblTotalArchivos.Size = new System.Drawing.Size(82, 13);
            this.lblTotalArchivos.TabIndex = 4;
            this.lblTotalArchivos.Text = "lblTotalArchivos";
            // 
            // cPrevisualizarDestino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer);
            this.Name = "cPrevisualizarDestino";
            this.Size = new System.Drawing.Size(391, 101);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.Button btnExpandir;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.TreeView treeViewDestino;
        private System.Windows.Forms.Label lblTotalArchivos;
    }
}
