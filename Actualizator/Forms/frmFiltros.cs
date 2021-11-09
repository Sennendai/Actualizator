using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Actualizator
{
    public partial class FormFiltros : Form
    {
        #region VARIABLES

        public BindingList<Filtro> FiltrosADevolver = new BindingList<Filtro>();
        private List<Filtro> backupFiltros = new List<Filtro>();
        private string rutaOrigen;
        private string proyectoName;

        #endregion

        #region CONSTRUCTOR             

        public FormFiltros(string rutaOrigen, string proyectoName)
        {
            InitializeComponent();
            this.rutaOrigen = rutaOrigen;
            this.proyectoName = proyectoName;
            CargarDatos();
        }

        public FormFiltros(string rutaOrigen, string proyectoName, BindingList<Filtro> Filtros)
        {
            InitializeComponent();
            this.rutaOrigen = rutaOrigen;
            this.proyectoName = proyectoName;
            FiltrosADevolver = Filtros;
            backupFiltros = Filtros.ToList();

            CargarDatos();
        }

        #endregion

        #region· FUNCIONES

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

        private void AddFiltro(string archivo = null)
        {
            if (!string.IsNullOrEmpty(txtBoxFiltro.Text) || archivo != null)
            {
                Filtro filtro;

                if (archivo == null)
                {
                    filtro = new Filtro()
                    {
                        cabecera = (Filtrado)cmbBoxFiltros.SelectedItem,
                        filtro = txtBoxFiltro.Text
                    };
                }
                else
                {
                    filtro = new Filtro()
                    {
                        cabecera = Filtrado.Completo,
                        filtro = archivo
                    };
                }

                if (ComprobarFiltro(filtro)) FiltrosADevolver.Add(filtro);

                ActualizarDatos();
            }
        }

        private bool ComprobarFiltro(Filtro filtroNuevo)
        {
            foreach (var filtroOriginal in FiltrosADevolver)
            {
                if (filtroOriginal.filtro.Equals(filtroNuevo.filtro))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region· EVENTOS

        private void btnAddFiltros_Click(object sender, EventArgs e)
        {
            AddFiltro();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            FiltrosADevolver = new BindingList<Filtro>(backupFiltros);
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
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

        private void btnBorrarFiltro_Click(object sender, EventArgs e)
        {
            if (dataGridFiltros.SelectedRows?.Count != 0)
            {
                foreach (DataGridViewRow row in dataGridFiltros.SelectedRows)
                {
                    dataGridFiltros.Rows.RemoveAt(row.Index);
                }
            }
            else if (dataGridFiltros.SelectedCells?.Count != 0)
            {
                foreach (DataGridViewTextBoxCell cell in dataGridFiltros.SelectedCells)
                {
                    dataGridFiltros.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        private void btnAbrirOrigen_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rutaOrigen))
                {
                    openFileDialog.InitialDirectory = rutaOrigen;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in openFileDialog.SafeFileNames)
                        {
                            AddFiltro(file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex), proyectoName);
            }
        }

        #endregion
    }
}
