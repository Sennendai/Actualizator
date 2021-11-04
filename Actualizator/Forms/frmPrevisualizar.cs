using Actualizator.Clases;
using Actualizator.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Proyecto actualProyecto;
        private bool sobreescribir;

        private TreeView treeviewDestino = new TreeView();

        #endregion

        #region CONSTRUCTOR
        public frmPrevisualizar()
        {
            InitializeComponent();
        }

        public frmPrevisualizar(ArchivosTreeView archivosOrigen, string origen, List<string> destinos, bool HayFiltros, Proyecto actualProyecto, bool sobreescribir)
        {
            InitializeComponent();
            this.origen = origen;
            this.archivosOrigen = archivosOrigen;
            this.destinos = destinos;
            this.HayFiltros = HayFiltros;
            this.actualProyecto = actualProyecto;
            this.sobreescribir = sobreescribir;

            CargarDatos();
        }

        #endregion
        
        private void CargarDatos()
        {
            Arbol.PopulateArchivoTreeView(archivosOrigen, null, HayFiltros, treeViewOrigen);
            treeViewOrigen.ExpandAll();

            DirectoryInfo dirOrigen = new DirectoryInfo(origen);
            dirDestinos = Archivos.GetAllDestinos(destinos, actualProyecto);

            foreach (DirectoryInfo dirDestino in dirDestinos)
            {
                ArchivosTreeView archivos;
                if (sobreescribir) archivos = Archivos.GetArchivosTreeView(dirOrigen, actualProyecto);
                else  archivos = Archivos.GetArchivosModificadosTreeView(dirOrigen, dirDestino, actualProyecto);

                AddDestinoControl(dirDestino.FullName, archivos);
            }
        }

        private void AddDestinoControl(string rutaDestino, ArchivosTreeView archivosModificados)
        {
            cPrevisualizarDestino destinoControl = new cPrevisualizarDestino();
            treeviewDestino = destinoControl.TreeViewDestino;
            treeviewDestino = Arbol.PopulateArchivoTreeView(archivosModificados, null, HayFiltros, treeviewDestino);

            destinoControl.TreeViewDestino = treeviewDestino;
            destinoControl.RutaDestino = rutaDestino;
            destinoControl.TotalArchivos = StringResource.totalArchivos + archivosModificados.GetTotalArchivos().ToString();

            tlpDestino.Controls.Add(destinoControl);
        }

        private Size ResizeTlpControl()
        {
            int height = tlpDestino.Size.Height;
            int width = tlpDestino.Size.Width;

            height = height / tlpDestino.Controls.Count;
            width = width / tlpDestino.Controls.Count;

            Size size = new Size(width, height);

            return size;
        }

        private void frmPrevisualizar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void tlpDestino_ControlAdded(object sender, ControlEventArgs e)
        {
            foreach(Control control in tlpDestino.Controls)
            {
                control.Size = ResizeTlpControl();
            }
        }
    }
}
