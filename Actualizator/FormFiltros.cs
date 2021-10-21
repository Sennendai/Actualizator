using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Actualizator
{
    public partial class FormFiltros : Form
    {
        public BindingList<Filtro> FiltrosADevolver = new BindingList<Filtro>();

        #region CONSTRUCTOR             

        public FormFiltros()
        {
            InitializeComponent();
            CargarDatos();
        }

        public FormFiltros(BindingList<Filtro> Filtros)
        {
            InitializeComponent();
            
            FiltrosADevolver = Filtros;
            CargarDatos();
        }

        #endregion

        private void CargarDatos()
        {
            cmbBoxFiltros.DataSource = Enum.GetValues(typeof(Filtrado));
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            BindingSource source = new BindingSource();
            source.DataSource = FiltrosADevolver;

            dataGridFiltros.DataSource = source;
        }

        private void AddFiltro()
        {
            if (txtBoxFiltro.Text != null)
            {
                Filtro filtro = new Filtro()
                {
                    cabecera = (Filtrado)cmbBoxFiltros.SelectedItem,
                    filtro = txtBoxFiltro.Text
                };

                FiltrosADevolver.Add(filtro);

                ActualizarDatos();
            }
            else
            {
                LocalUtilities.MensajeError(Resource.mensajeRellenarFiltro);
            }
        }

        private void btnAddFiltros_Click(object sender, EventArgs e)
        {
            AddFiltro();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                AddFiltro();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void FormFiltros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
