using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Utilities.Clases.XML;

namespace Actualizator
{
    public partial class FormPrincipal : Form
    {
        #region· VARIABLES

        private int countArchivosDestino = 0;
        private int countArchivosOrigen = 0;
        private string rutaBackup;
        private BindingList<Filtro> filtros;
        private bool hayFiltros = false;
        private bool hacerBackup = false;
        private List<string> destinos = new List<string>();
        private Proyecto nuevoProyecto;
        private Proyecto actualProyecto;
        private List<Proyecto> proyectos = new List<Proyecto>();

        #endregion

        #region· CONSTRUCTOR

        public FormPrincipal()
        {
            InitializeComponent();
            CargarDatos();
        }

        #endregion

        #region· FUNCIONES

        private void CargarDatos()
        {
            rutaBackup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            CargarProyectos();

            filtros = new BindingList<Filtro>();
            BindingSource bSource = new BindingSource { DataSource = filtros };
            cmbBoxFiltros.DataSource = bSource;

            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            textBackup.Text = rutaBackup;
            lblArchivosOrigen.Text = countArchivosOrigen.ToString();
            //lblArchivosDestino.Text = CountArchivosDestino.ToString();

            BindingSource bSource = new BindingSource { DataSource = filtros };
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

            if (hayFiltros)
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
                        if (hayFiltros)
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
                LocalUtilities.MensajeError("Error: " + LocalUtilities.getErrorException(ex));
            }

            return contador;
        }

        private void CrerBackup(FileInfo[] archivosOrigen)
        {
            try
            {
                // Crear la ruta de carpeta
                rutaBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                    DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", ""));
                // Crear la carpeta
                Directory.CreateDirectory(rutaBackup);
                // Copiar todos los archivos
                foreach (FileInfo archivoOrigen in archivosOrigen)
                {
                    File.Copy(archivoOrigen.FullName, Path.Combine(rutaBackup, archivoOrigen.Name));
                    LocalUtilities.WriteTextLog("Backup: " + rutaBackup + " | Fecha: " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError("Error: " + LocalUtilities.getErrorException(ex));
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

                    if (hayFiltros)  archivosOrigen = FiltrarArchivos(archivosOrigen); 

                    if (hacerBackup) CrerBackup(archivosOrigen);

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
                    LocalUtilities.MensajeError("¡Compruebe las rutas de las carpetas!");
                }

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError("Error: " + LocalUtilities.getErrorException(ex));
            }
        }

        private FileInfo[] FiltrarArchivos(FileInfo[] archivos)
        {
            foreach (Filtro filtro in filtros)
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

        private void GuardarProyecto()
        {
            try
            {
                nuevoProyecto = new Proyecto(cmbProyecto.Text, textOrigen.Text, destinos, rutaBackup, filtros);
                proyectos.Add(nuevoProyecto);

                string proyectoXML = SerializerXML.getObjectSerialized(proyectos);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(proyectoXML);
                doc.Save("proyectos.xml");

                if (ExisteProyecto())
                {
                    LocalUtilities.WriteTextLog("Guardado proyecto XML " + " | Fecha: " + DateTime.Now.ToString());
                    MessageBox.Show("Guardado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError("Error: " + LocalUtilities.getErrorException(ex));
            }
        }

        private void CargarProyectos()
        {
            if (ExisteProyecto())
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(Environment.CurrentDirectory, "proyectos.xml"));

                string resultado = doc.InnerXml;

                if (resultado != null)
                {
                    var test = SerializerXML.Deserialize_Opcion1<List<Proyecto>>(resultado);
                }
            }
        }        

        private bool ExisteProyecto()
        {
            return File.Exists(Path.Combine(Environment.CurrentDirectory, "proyectos.xml")); ;
        }

        #endregion

        #region· INTERACCIONES

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
            countArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen);
        }

        private void btnVerCarpetaDestino_Click(object sender, EventArgs e)
        {
            if (textDestino.Text != null)
            {
                Destino destino = new Destino();
                destino.LabelContador = ActualizarTreeView(textDestino, destino.TreeViewDestino);
                destino.RutaDestino = textDestino.Text;

                tableLayoutDestino.Controls.Add(destino);
                destinos.Add(textDestino.Text);
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
            GuardarProyecto();
        }

        private void checkBoxBackup_CheckedChanged(object sender, EventArgs e)
        {
            textBackup.Visible = checkBoxBackup.Checked;
            btnRutaBackup.Visible = checkBoxBackup.Checked;
            hacerBackup = checkBoxBackup.Checked;
        }

        private void chkBoxFiltros_CheckedChanged(object sender, EventArgs e)
        {
            btnModificarFiltros.Visible = chkBoxFiltros.Checked;
            cmbBoxFiltros.Visible = chkBoxFiltros.Checked;
            hayFiltros = chkBoxFiltros.Checked;
        }

        private void btnAddFiltros_Click(object sender, EventArgs e)
        {
            FormFiltros formFiltros;
            if (filtros?.Count() > 0)
            {
                formFiltros = new FormFiltros(filtros);
            }
            else
            {
                formFiltros = new FormFiltros();
            }

            formFiltros.ShowDialog();
            if (formFiltros.DialogResult == DialogResult.OK)
            {
                filtros = formFiltros.FiltrosADevolver;
                ActualizarDatos();
            }
        }

        private void textOrigen_MouseHover(object sender, EventArgs e)
        {
            toolTipControl.SetToolTip(textOrigen, textOrigen.Text);
        }

        private void textDestino_MouseHover(object sender, EventArgs e)
        {
            toolTipControl.SetToolTip(textDestino, textDestino.Text);
        }

        private void textBackup_MouseHover(object sender, EventArgs e)
        {
            toolTipControl.SetToolTip(textBackup, textBackup.Text);
        }

        #endregion

    }
}