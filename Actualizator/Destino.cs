using System;
using System.Drawing;
using System.Windows.Forms;

namespace Actualizator
{
    public partial class Destino : UserControl
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

        public Destino()
        {
            InitializeComponent();
            // para ajustar a la pagina los sucesivos controles
            //this.Dock = DockStyle.Fill;
            this.Margin = new Padding(1,3,0,3);
        }

        private void splitContainer1_Panel1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            if (!splitContainer1.Panel2Collapsed)
            {
                //this.AutoSize = true;
               this.Size = new Size(525, 400);
            }
            else
            {
                this.Size = new Size(525, 100);
            }
        }

        private void lblRutaDestino_MouseHover(object sender, EventArgs e)
        {
            toolTipControl.SetToolTip(lblRutaDestino, lblRutaDestino.Text);
        }

        private void borrarDestinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Quieres borrar el destino?", "Borrar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                TableLayoutPanel tableLayout = (TableLayoutPanel)this.Parent;
                tableLayout.Controls.Remove(this);
            }
        }
    }
}
