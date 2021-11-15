using Actualizator.Clases;
using Actualizator.Controles;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Actualizator.Forms
{
    public partial class frmPrevisualizar : Form
    {
        #region VARIABLES

        private List<string> destinos;
        private List<DirectoryInfo> dirDestinos;
        private string origen;
        private ArchivosTreeView archivosOrigen;
        private bool HayFiltros;
        private bool HayFiltrosIncluyentes;
        private Proyecto actualProyecto;
        private bool sobreescribir;
        private TreeView treeviewDestino = new TreeView();

        #endregion

        #region CONSTRUCTOR

        public frmPrevisualizar()
        {
            InitializeComponent();
        }

        public frmPrevisualizar(ArchivosTreeView archivosOrigen, string origen, List<string> destinos, bool HayFiltros, bool HayFiltrosIncluyentes,
            Proyecto actualProyecto, bool sobreescribir)
        {
            InitializeComponent();
            this.origen = origen;
            this.archivosOrigen = archivosOrigen;
            this.destinos = destinos;
            this.HayFiltros = HayFiltros;
            this.HayFiltrosIncluyentes = HayFiltrosIncluyentes;
            this.actualProyecto = actualProyecto;
            this.sobreescribir = sobreescribir;

            CargarDatos();
        }

        #endregion

        #region FUNCIONES

        private void CargarDatos()
        {
            ArbolUtilities.PopulateArchivoTreeView(archivosOrigen, null, HayFiltros, HayFiltrosIncluyentes, treeViewOrigen);
            treeViewOrigen.ExpandAll();

            DirectoryInfo dirOrigen = new DirectoryInfo(origen);
            dirDestinos = ArchivosUtilities.GetAllDestinos(destinos, actualProyecto);

            foreach (DirectoryInfo dirDestino in dirDestinos)
            {
                ArchivosTreeView archivos;
                if (sobreescribir) archivos = ArchivosUtilities.GetArchivosTreeView(dirOrigen, actualProyecto, false);
                else archivos = ArchivosUtilities.GetArchivosModificadosTreeView(dirOrigen, dirDestino, actualProyecto);

                AddDestinoControl(dirDestino.FullName, archivos);
            }
        }

        private void AddDestinoControl(string rutaDestino, ArchivosTreeView archivosModificados)
        {
            cPrevisualizarDestino destinoControl = new cPrevisualizarDestino();
            treeviewDestino = destinoControl.TreeViewDestino;
            treeviewDestino = ArbolUtilities.PopulateArchivoTreeView(archivosModificados, null, HayFiltros, HayFiltrosIncluyentes, treeviewDestino);

            destinoControl.TreeViewDestino = treeviewDestino;
            destinoControl.RutaDestino = rutaDestino;
            destinoControl.TotalArchivos = StringResource.totalArchivos + archivosModificados.GetTotalArchivos().ToString();

            tlpDestino.Controls.Add(destinoControl);
        }

        #endregion

        #region EVENTOS

        private void frmPrevisualizar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void tlpDestino_ControlAdded(object sender, ControlEventArgs e)
        {
            foreach (Control control in tlpDestino.Controls)
            {
                control.Size = LocalUtilities.ResizeTlpControl(tlpDestino);
            }
        }

        #endregion
    }
}
