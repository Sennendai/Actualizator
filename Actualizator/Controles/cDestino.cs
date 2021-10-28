using System;
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

        #endregion

        #region· CONSTRUCTOR
        public cDestino()
        {
            InitializeComponent();
            CargarDatos();
        }
        #endregion

        private void CargarDatos()
        {
            this.Margin = new Padding(1, 3, 0, 3);
            this.ResizeRedraw = true;
            this.Dock = DockStyle.Fill;
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
            }
            else
            {
                splitContainer.Dock = DockStyle.Top;
                lblExpandir.Text = StringResource.clickExpandir;
            }
        }

        private void BorrarDestino()
        {
            DialogResult dialogResult = MessageBox.Show(StringResource.mensajeBorrarDestino, StringResource.borrar, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                TableLayoutPanel tableLayout = (TableLayoutPanel)this.Parent;
                tableLayout.Controls.Remove(this);
            }
        }

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

        #endregion
        
        // Para permitir que se pueda cambiar el tamaño del control
        // No funciona correctamente, hay algo que lo bloquea?
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var cp = base.CreateParams;
        //        cp.Style |= 0x00040000;  // Turn on WS_BORDER + WS_THICKFRAME
        //        return cp;
        //    }
        //}
    }
}
