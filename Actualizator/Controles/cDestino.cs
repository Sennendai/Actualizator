using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Actualizator
{
    public partial class cDestino : UserControl
    {
        #region· VARIABLES

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

        public Button buttonBorrarDestino
        {
            get { return btnBorrarDestino; }
            set
            {
                if (value != btnBorrarDestino)
                {
                    btnBorrarDestino = value;
                }
            }
        }

        public string RutaDestino
        {
            get { return lblRutaDestino.Text; }
            set
            {
                if (value != lblRutaDestino.Text)
                {
                    lblRutaDestino.Text = value;
                }
            }
        }

        private int _labelContador;
        public int LabelContador
        {
            get { return _labelContador; }
            set
            {
                if (value != Convert.ToInt32(lblArchivosDestino.Text))
                {
                    _labelContador = value;
                    lblArchivosDestino.Text = _labelContador.ToString();
                }
            }
        }

        private List<string> rutasDestino;
        public List<string> RutasDestino
        {
            get => rutasDestino;
            set
            {
                if (value != rutasDestino)
                {
                    rutasDestino = value;

                }
            }
        }

        public bool CheckDestino
        {
            get { return chkBoxDestino.Checked; }
            set
            {
                if (value != chkBoxDestino.Checked)
                {
                    chkBoxDestino.Checked = value;
                }
            }
        }

        #endregion

        #region· CONSTRUCTOR

        public cDestino(List<string> rutasDestino)
        {
            InitializeComponent();
            this.rutasDestino = rutasDestino;

            CargarDatos();
        }

        #endregion

        #region· FUNCIONES

        private void CargarDatos()
        {
            this.Margin = new Padding(1, 3, 0, 3);
            this.ResizeRedraw = true;
            this.Dock = DockStyle.Fill;
            this.splitContainer.Panel2Collapsed = true;
            splitContainer.Dock = DockStyle.Fill;

            lblExpandir.Text = StringResource.clickExpandir;
        }

        private void ExpandirContraerPanel()
        {
            splitContainer.Panel2Collapsed = !splitContainer.Panel2Collapsed;
            if (!splitContainer.Panel2Collapsed)
            {
                splitContainer.Dock = DockStyle.Fill;
                lblExpandir.Text = string.Empty;
                this.treeViewDestino.ExpandAll();
                if (this.treeViewDestino.Nodes != null) this.treeViewDestino.SelectedNode = this.treeViewDestino.Nodes[0];
            }
            else
            {
                splitContainer.Dock = DockStyle.Top;
                lblExpandir.Text = StringResource.clickExpandir;
                this.treeViewDestino.CollapseAll();
            }
        }

        private void BorrarDestino()
        {
            DialogResult dialogResult = MessageBox.Show(StringResource.mensajeBorrarDestino, StringResource.borrar, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                TableLayoutPanel tableLayout = (TableLayoutPanel)this.Parent;
                tableLayout.Controls.Remove(this);
                rutasDestino.Remove(RutaDestino);
            }
        }

        #endregion

        #region· EVENTOS

        private void splitContainer1_Panel1_Click(object sender, EventArgs e)
        {
            ExpandirContraerPanel();
        }

        private void lblRutaDestino_MouseHover(object sender, EventArgs e)
        {
            toolTipControl.SetToolTip(lblRutaDestino, lblRutaDestino.Text);
        }

        private void borrarDestinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorrarDestino();
        }

        private void btnBorrarDestino_Click(object sender, EventArgs e)
        {
            BorrarDestino();
        }

        private void lblExpandir_Click(object sender, EventArgs e)
        {
            ExpandirContraerPanel();
        }

        private void chkBoxDestino_CheckedChanged(object sender, EventArgs e)
        {
            TableLayoutPanel tableLayout = (TableLayoutPanel)this.Parent;

            tableLayout.Update();
        }

        private void abrirDestinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblRutaDestino.Text))
            {
                Process.Start(StringResource.procesoExplorer, lblRutaDestino.Text);
            }
        }

        #endregion

    }
}
