using System;
using System.Windows.Forms;

namespace Actualizator.Controles
{
    public partial class cPrevisualizarDestino : UserControl
    {
        public TreeView TreeViewDestino
        {
            get { return treeViewDestino; }
            set
            {
                if (value != treeViewDestino)
                {
                    treeViewDestino = value;
                }
            }
        }

        public string RutaDestino
        {
            get { return lblDestino.Text; }
            set
            {
                if (value != lblDestino.Text)
                {
                    lblDestino.Text = value;
                }
            }
        }

        public string TotalArchivos
        {
            get { return lblTotalArchivos.Text; }
            set
            {
                if (value != lblTotalArchivos.Text)
                {
                    lblTotalArchivos.Text = value;
                }
            }
        }

        public cPrevisualizarDestino()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            this.Dock = DockStyle.Fill;
            this.splitContainer.Panel2Collapsed = true;
        }

        private void lblDestino_MouseHover(object sender, EventArgs e)
        {
            toolTipControl.SetToolTip(lblDestino, lblDestino.Text);
        }

        private void btnExpandir_Click(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = !splitContainer.Panel2Collapsed;
            if (!splitContainer.Panel2Collapsed)
            {
                splitContainer.Dock = DockStyle.Fill;
                this.treeViewDestino.ExpandAll();
                if (this.treeViewDestino.Nodes != null) this.treeViewDestino.SelectedNode = this.treeViewDestino.Nodes[0];
            }
            else
            {
                splitContainer.Dock = DockStyle.Top;
                this.treeViewDestino.CollapseAll();
            }
        }
    }
}
