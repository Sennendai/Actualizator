using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Actualizator
{
    public partial class FormFiltros : Form
    {
        #region VARIABLES

        public BindingList<Filtro> FiltrosADevolver = new BindingList<Filtro>();
        private Dictionary<string, BindingList<List<Filtro>>> FiltrosComunes = new Dictionary<string, BindingList<List<Filtro>>>();
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
            CargarFiltrosComunes();
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            BindingSource source = new BindingSource
            {
                DataSource = FiltrosADevolver
            };

            dataGridFiltros.DataSource = source;
            //dataGridFiltros.Columns[nameof(Filtro.NombreFiltro)].Visible = false;
        }

        private void CargarFiltrosComunes()
        {
            //FiltrosComunes.Add("Filtros comunes",new BindingList<List<Filtro>> { new List<Filtro> 
            //                    { new Filtro { descripcion = "Filtros comunes", filtro = string.Empty } } });
            //FiltrosComunes.Add(".dll, .pdb", new BindingList<List<Filtro>> {  new List<Filtro> 
            //                    { new Filtro { cabecera = Filtrado.TerminaPor, descripcion = ".dll, .pdb", filtro = ".dll" },
            //                     new Filtro { cabecera = Filtrado.TerminaPor, descripcion = ".dll, .pdb", filtro = ".pdb" }} });
            //FiltrosComunes.Add(".xml, .config", new BindingList<List<Filtro>> { new List<Filtro> 
            //                    { new Filtro { cabecera = Filtrado.TerminaPor, descripcion = ".xml, .config", filtro = ".xml" } } });

            List<Filtro> listaDeFiltros = new List<Filtro>();

            BindingSource source = new BindingSource
            {
                DataSource = listaDeFiltros
            };

            cmbBoxConfigs.DataSource = source;
            cmbBoxConfigs.DisplayMember = nameof(Filtro.Descripcion);
        }

        /// <summary>
        /// Filtrar una carpeta entera con todos sus archivos
        /// </summary>
        private void AddCarpetaFiltro(DirectoryInfo dirInfo)
        {
            Filtro filtro;

            filtro = new Filtro()
            {
                Descripcion = Filtrado.Ruta.ToString() + dirInfo.Name,
                cabecera = Filtrado.Ruta,
                NombreFiltro = dirInfo.Name,
                Filtros = new List<string>()
            };

            var archivosDir = dirInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (var archivoDir in archivosDir)
            {
                filtro.Filtros.Add(archivoDir.FullName);
            }

            if (ComprobarFiltro(filtro)) FiltrosADevolver.Add(filtro);
        }

        private void AddFiltro(string archivo = null)
        {
            if (!string.IsNullOrEmpty(txtBoxFiltro.Text) || archivo != null)
            {
                Filtro filtro;

                // añadido a mano
                if (archivo == null)
                {
                    filtro = new Filtro()
                    {
                        Descripcion = cmbBoxFiltros.SelectedItem.ToString() + " - " + txtBoxFiltro.Text,
                        cabecera = (Filtrado)cmbBoxFiltros.SelectedItem,
                        NombreFiltro = txtBoxFiltro.Text,
                        Filtros = new List<string> { txtBoxFiltro.Text }
                    };

                    if (ComprobarFiltro(filtro)) FiltrosADevolver.Add(filtro);
                }
                // archivo suelto   
                else
                {
                    filtro = new Filtro()
                    {
                        Descripcion = Filtrado.Ruta.ToString() + archivo,
                        cabecera = Filtrado.Ruta,
                        NombreFiltro = archivo,
                        Filtros = new List<string> { archivo }
                    };

                    if (ComprobarFiltro(filtro)) FiltrosADevolver.Add(filtro);
                }

                ActualizarDatos();
            }
        }

        private bool ComprobarFiltro(Filtro filtroNuevo)
        {
            foreach (var filtroOriginal in FiltrosADevolver)
            {
                switch (filtroOriginal.cabecera)
                {
                    case Filtrado.TerminaPor:
                    case Filtrado.Completo:
                        if (filtroOriginal.Filtros.Equals(filtroNuevo.Filtros))
                        {
                            return false;
                        }
                        break;
                    case Filtrado.Ruta:
                        foreach (var nuevoFiltro in filtroNuevo.Filtros)
                        {
                            if (filtroOriginal.Filtros.Any(x => x.Equals(nuevoFiltro))) return false;
                        }
                        break;
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
                txtBoxFiltro.Text = string.Empty;
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

        private void cmbBoxConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCarpeta_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rutaOrigen))
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog
                    {
                        InitialDirectory = rutaOrigen,
                        IsFolderPicker = true,
                        Multiselect = true
                    };
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        foreach (string filename in dialog.FileNames)
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(filename);
                            AddCarpetaFiltro(dirInfo); 
                        }
                        //var files = dirInfo.GetFiles("*", SearchOption.AllDirectories);
                        //foreach (var file in files)
                        //{
                        //    AddFiltro(file.Name);
                        //}
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
