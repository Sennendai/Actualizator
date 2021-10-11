using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Actualizator
{
    public partial class Form1 : Form
    {
        #region· VARIABLES

        private int CountArchivosDestino = 0;
        private int CountArchivosOrigen = 0;
        private string RutaBackup;
        private BindingList<Filtro> Filtros;
        private bool HayFiltros = false;
        private bool HacerBackup = false;

        #endregion

        #region· CONSTRUCTOR

        public Form1()
        {
            InitializeComponent();
            CargarDatos();
        }

        #endregion

        #region· FUNCIONES

        private void CargarDatos()
        {
            RutaBackup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            Filtros = new BindingList<Filtro>();
            BindingSource bSource = new BindingSource { DataSource = Filtros };
            cmbBoxFiltros.DataSource = bSource;

            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            textBackup.Text = RutaBackup;
            lblArchivosOrigen.Text = CountArchivosOrigen.ToString();
            //lblArchivosDestino.Text = CountArchivosDestino.ToString();

            BindingSource bSource = new BindingSource { DataSource = Filtros };
            cmbBoxFiltros.DataSource = bSource;
        }

        private void ElegirRuta(TextBox txtBox, Button btn = null)
        {
            folderBrowserDlg.SelectedPath = txtBox.Text;
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtBox.Text = folderBrowserDlg.SelectedPath;
                if (btn != null) btn.Visible = true;
            }
        }

        /// <summary>
        /// Rellena un TreeView dada una ruta de carpetas
        /// </summary>
        /// <param name="dirInfo">Ruta de la carpeta</param>
        /// <param name="treeNode">Nodo del arbol</param>
        /// <param name="treeView"></param>
        private void PopulateTreeView(DirectoryInfo dirInfo, TreeNode treeNode, TreeView treeView = null)
        {
            // Rellena a nivel raiz
            if (treeNode == null)
            {
                TreeNode directoryNodeRoot = new TreeNode
                {
                    Text = dirInfo.Name
                };

                treeView.Nodes.Add(directoryNodeRoot);
                AddFilesNode(dirInfo, ref directoryNodeRoot);
            }

            // Rellena las subcarpetas
            foreach (DirectoryInfo directory in dirInfo.GetDirectories())
            {
                TreeNode directoryNode = new TreeNode
                {
                    Text = directory.Name
                };

                if (treeNode == null)
                {
                    treeView.Nodes.Add(directoryNode);
                }
                else
                {
                    treeNode.Nodes.Add(directoryNode);
                }

                AddFilesNode(directory, ref directoryNode);
                PopulateTreeView(directory, directoryNode);
            }
        }

        private void AddFilesNode(DirectoryInfo directory, ref TreeNode directoryNode)
        {
            FileInfo[] archivos = directory.GetFiles();

            if (HayFiltros)
            {
                archivos = FiltrarArchivos(archivos);
            }
            
            foreach (FileInfo file in archivos)
            {
                TreeNode fileNode = new TreeNode
                {
                    Text = file.Name
                };

                directoryNode.Nodes.Add(fileNode);
            }            
        }

        private int ActualizarTreeView(TextBox txtBox, TreeView treeView)
        {
            int contador = 0;

            try
            {
                if (txtBox.Text != null)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(txtBox.Text);

                    if (dirInfo.Exists)
                    {
                        // Limpiar el treeView
                        treeView.Nodes.Clear();
                        // Poblar el TreeView, recursivamente
                        PopulateTreeView(dirInfo, null, treeView);

                        var contadorTodos = dirInfo.GetFiles("*", SearchOption.AllDirectories);
                        if (HayFiltros)
                        {
                            contadorTodos = FiltrarArchivos(contadorTodos);
                        }
                        contador = contadorTodos.Count();
                        ActualizarDatos();
                    }
                }                
            }
            catch (Exception ex)
            {
                Utilities.MensajeError(Utilities.getErrorException(ex));
            }

            return contador;
        }

        private void CrerBackup(FileInfo[] archivosOrigen)
        {
            try
            {
                // Crear la ruta de carpeta
                RutaBackup = Path.Combine(RutaBackup, DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", ""));
                // Crear la carpeta
                Directory.CreateDirectory(RutaBackup);
                // Copiar todos los archivos
                foreach (FileInfo archivoOrigen in archivosOrigen)
                {
                    File.Copy(archivoOrigen.FullName, Path.Combine(RutaBackup, archivoOrigen.Name));
                }
            }
            catch (Exception ex)
            {
                Utilities.MensajeError(Utilities.getErrorException(ex));
            }
        }

        private void SincronizarCarpetas()
        {
            try
            {
                DirectoryInfo dirOrigen = new DirectoryInfo(textOrigen.Text);
                DirectoryInfo dirDestino = new DirectoryInfo(textDestino.Text);

                if (dirOrigen.Exists && dirDestino.Exists)
                {
                    FileInfo[] archivosOrigen = dirOrigen.GetFiles("*", SearchOption.AllDirectories);
                    FileInfo[] archivosDestino = dirDestino.GetFiles("*", SearchOption.AllDirectories);

                    if (HayFiltros)  archivosOrigen = FiltrarArchivos(archivosOrigen); 

                    if (HacerBackup) CrerBackup(archivosOrigen);

                    foreach (FileInfo archivoOrigen in archivosOrigen)
                    {
                        var archivoDestino = archivosDestino.Where(x => x.Name == archivoOrigen.Name).FirstOrDefault();
                        // Copia al ser un archivo nuevo
                        if (archivoDestino == null)
                        {
                            File.Copy(archivoOrigen.FullName, Path.Combine(dirDestino.FullName, archivoOrigen.Name));
                        }
                        // Reemplaza si ya existe y esta modificado
                        else if (archivoDestino.LastWriteTimeUtc < archivoOrigen.LastWriteTimeUtc)
                        {
                            File.Copy(archivoOrigen.FullName, archivoDestino.FullName, true);
                        }
                    }
                }
                else
                {
                    Utilities.MensajeError("¡Compruebe las rutas de las carpetas!");
                }

            }
            catch (Exception ex)
            {
                Utilities.MensajeError(Utilities.getErrorException(ex));
            }
        }

        private FileInfo[] FiltrarArchivos(FileInfo[] archivos)
        {
            foreach (Filtro filtro in Filtros)
            {
                switch (filtro.cabecera)
                {
                    case Filtrado.TerminaPor:
                        archivos = archivos.Where(x => !x.Name.ToLower().EndsWith(filtro.filtro.ToLower())).ToArray();
                        break;
                    case Filtrado.Completo:
                        archivos = archivos.Where(x => !x.Name.ToLower().Equals(filtro.filtro.ToLower())).ToArray();
                        break;
                }
            }

            return archivos;
        }

        #endregion

        #region· INTERACCIONES

        #endregion

        private void btnSubirArchivo1_Click(object sender, EventArgs e)
        {
            ElegirRuta(textOrigen, btnVerCarpetaOrigen);
        }

        private void btnSubirArchivo2_Click(object sender, EventArgs e)
        {
            ElegirRuta(textDestino, btnVerCarpetaDestino);
        }

        private void btnRutaBackup_Click(object sender, EventArgs e)
        {
            ElegirRuta(textBackup);
        }

        private void btnVerCarpetaOrigen_Click(object sender, EventArgs e)
        {
            CountArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen);
        }

        private void btnVerCarpetaDestino_Click(object sender, EventArgs e)
        {
            if (textDestino.Text != null)
            {
                Destino destino = new Destino();
                destino.LabelContador = ActualizarTreeView(textDestino, destino.TreeViewDestino);
                destino.RutaDestino = textDestino.Text;

                tableLayoutDestino.Controls.Add(destino);
            }
        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            SincronizarCarpetas();
        }

        private void rutaBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void anadirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxBackup_CheckedChanged(object sender, EventArgs e)
        {
            textBackup.Visible = checkBoxBackup.Checked;
            btnRutaBackup.Visible = checkBoxBackup.Checked;
            HacerBackup = checkBoxBackup.Checked;
        }

        private void chkBoxFiltros_CheckedChanged(object sender, EventArgs e)
        {
            btnModificarFiltros.Visible = chkBoxFiltros.Checked;
            cmbBoxFiltros.Visible = chkBoxFiltros.Checked;
            HayFiltros = chkBoxFiltros.Checked;
        }

        private void btnAddFiltros_Click(object sender, EventArgs e)
        {
            FormFiltros formFiltros;
            if (Filtros?.Count() > 0)
            {
                formFiltros = new FormFiltros(Filtros);
            }
            else
            {
                formFiltros = new FormFiltros();
            }

            formFiltros.ShowDialog();
            if (formFiltros.DialogResult == DialogResult.OK)
            {
                Filtros = formFiltros.FiltrosADevolver;
                ActualizarDatos();
            }
        }
    }
}