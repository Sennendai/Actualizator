namespace Actualizator.Forms
{
    partial class frmPrevisualizar
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeViewOrigen = new System.Windows.Forms.TreeView();
            this.tlpDestino = new System.Windows.Forms.TableLayoutPanel();
            this.lblOrigen = new System.Windows.Forms.Label();
            this.lblDestinos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Location = new System.Drawing.Point(0, 42);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeViewOrigen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tlpDestino);
            this.splitContainer.Size = new System.Drawing.Size(832, 476);
            this.splitContainer.SplitterDistance = 309;
            this.splitContainer.TabIndex = 0;
            // 
            // treeViewOrigen
            // 
            this.treeViewOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewOrigen.Location = new System.Drawing.Point(0, 0);
            this.treeViewOrigen.Name = "treeViewOrigen";
            this.treeViewOrigen.Size = new System.Drawing.Size(307, 474);
            this.treeViewOrigen.TabIndex = 0;
            // 
            // tlpDestino
            // 
            this.tlpDestino.AutoScroll = true;
            this.tlpDestino.ColumnCount = 1;
            this.tlpDestino.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDestino.Location = new System.Drawing.Point(0, 0);
            this.tlpDestino.Name = "tlpDestino";
            this.tlpDestino.RowCount = 1;
            this.tlpDestino.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDestino.Size = new System.Drawing.Size(517, 474);
            this.tlpDestino.TabIndex = 0;
            this.tlpDestino.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tlpDestino_ControlAdded);
            // 
            // lblOrigen
            // 
            this.lblOrigen.AutoSize = true;
            this.lblOrigen.Location = new System.Drawing.Point(12, 9);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(38, 13);
            this.lblOrigen.TabIndex = 1;
            this.lblOrigen.Text = "Origen";
            // 
            // lblDestinos
            // 
            this.lblDestinos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDestinos.AutoSize = true;
            this.lblDestinos.Location = new System.Drawing.Point(319, 9);
            this.lblDestinos.Name = "lblDestinos";
            this.lblDestinos.Size = new System.Drawing.Size(48, 13);
            this.lblDestinos.TabIndex = 2;
            this.lblDestinos.Text = "Destinos";
            // 
            // frmPrevisualizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 518);
            this.Controls.Add(this.lblDestinos);
            this.Controls.Add(this.lblOrigen);
            this.Controls.Add(this.splitContainer);
            this.Name = "frmPrevisualizar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPrevisualizar";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmPrevisualizar_KeyPress);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeViewOrigen;
        private System.Windows.Forms.TableLayoutPanel tlpDestino;
        private System.Windows.Forms.Label lblOrigen;
        private System.Windows.Forms.Label lblDestinos;
    }
}