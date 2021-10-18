using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Utilities.Clases.XML;

namespace Actualizator
{
    public partial class FormPrincipal : Form
    {
        #region· VARIABLES

        private int countArchivosDestino = 0;
        private int countArchivosOrigen = 0;
        private string rutaBackup;
        private string lastRutaBackup;

        private bool hayFiltros = false;
        private bool hacerBackup = false;

        public bool HacerBackup
        {
            get => hacerBackup;
            set
            {
                if (value != hacerBackup)
                {
                    hacerBackup = value;
                    checkBoxBackup.Checked = hacerBackup;
                    textBackup.Visible = hacerBackup;
                    btnRutaBackup.Visible = hacerBackup;
                }
            }
        }
        public bool HayFiltros
        {
            get => hayFiltros;
            set
            {
                if (value != hayFiltros)
                {
                    hayFiltros = value;
                    chkBoxFiltros.Checked = hayFiltros;
                    btnModificarFiltros.Visible = hayFiltros;
                    cmbBoxFiltros.Visible = hayFiltros;
                }
            }
        }

        private bool addProyecto = false;

        private List<string> destinos = new List<string>();
        private Proyecto actualProyecto;
        private BindingList<Proyecto> proyectos = new BindingList<Proyecto>();
        private BindingList<Filtro> filtros = new BindingList<Filtro>();

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

            textBackup.Text = rutaBackup;
            cmbProyecto.DisplayMember = nameof(Proyecto.ProyectoName);
            lblLog.Text = Resource.saludo;

            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            if (!string.IsNullOrEmpty(textOrigen.Text)) btnVerCarpetaOrigen.Visible = true;

            lblArchivosOrigen.Text = countArchivosOrigen.ToString();

            chkBoxFiltros.Checked = HayFiltros;
            checkBoxBackup.Checked = HacerBackup;

            BindingSource bSource = new BindingSource { DataSource = filtros };
            cmbBoxFiltros.DataSource = bSource;

            cmbProyecto.DataSource = proyectos;
            cmbProyecto.Refresh();
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

        private int ActualizarTreeView(TextBox txtBox, TreeView treeView, string ruta = null)
        {
            int contador = 0;

            try
            {
                if (txtBox.Text != null)
                {
                    DirectoryInfo dirInfo;
                    if (ruta != null)
                    {
                        dirInfo = new DirectoryInfo(ruta);
                    }
                    else
                    {
                        dirInfo = new DirectoryInfo(txtBox.Text);
                    }

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
                LocalUtilities.MensajeError("Error: " + LocalUtilities.getErrorException(ex));
            }

            return contador;
        }

        private void CrerBackup(FileInfo[] archivosOrigen)
        {
            try
            {
                // Crear la ruta de carpeta
                string temporalRutaBackup = Path.Combine(rutaBackup, DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", ""));
                // Crear la carpeta
                Directory.CreateDirectory(temporalRutaBackup);
                lastRutaBackup = temporalRutaBackup;
                // Copiar todos los archivos
                foreach (FileInfo archivoOrigen in archivosOrigen)
                {
                    File.Copy(archivoOrigen.FullName, Path.Combine(temporalRutaBackup, archivoOrigen.Name));
                    LocalUtilities.WriteTextLog("Backup: " + temporalRutaBackup + " | Fecha: " + DateTime.Now.ToString());
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
                List<DirectoryInfo> directoriesDestino = GetAllDestinos();
                if (directoriesDestino == null || directoriesDestino.Count() == 0)
                {
                    LocalUtilities.MensajeError(Resource.comprobarDestino);
                    return;
                }

                if (dirOrigen.Exists)
                {
                    FileInfo[] archivosOrigen = dirOrigen.GetFiles("*", SearchOption.AllDirectories);

                    if (HayFiltros) archivosOrigen = FiltrarArchivos(archivosOrigen);

                    if (HacerBackup) CrerBackup(archivosOrigen);

                    foreach (DirectoryInfo dirDestino in directoriesDestino)
                    {
                        FileInfo[] archivosDestino = dirDestino.GetFiles("*", SearchOption.AllDirectories);

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

                    GuardarProyecto();
                }
                else
                {
                    LocalUtilities.MensajeError("¡Compruebe las rutas de las carpetas de origen!");
                }

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError("Error: " + LocalUtilities.getErrorException(ex));
            }
        }

        private List<DirectoryInfo> GetAllDestinos()
        {
            List<DirectoryInfo> allDestinos = new List<DirectoryInfo>();

            foreach (var destino in destinos)
            {
                DirectoryInfo dirDestino = new DirectoryInfo(destino);
                if (dirDestino.Exists) allDestinos.Add(dirDestino);
                else
                {
                    LocalUtilities.MensajeError(Resource.comprobarDestino);
                    return null;
                }
            }

            return allDestinos;
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
                if (!string.IsNullOrEmpty(cmbProyecto.Text.ToString()))
                {
                    ActualizarDestino();

                    if (actualProyecto == null)
                    {
                        Guid guid = Guid.NewGuid();
                        actualProyecto = new Proyecto(guid);
                    }

                    actualProyecto.ProyectoName = cmbProyecto.Text.ToString();
                    actualProyecto.PathOrigen = textOrigen.Text;
                    actualProyecto.PathDestino = destinos;
                    actualProyecto.HacerBackup = HacerBackup;
                    actualProyecto.PathBackup = rutaBackup;
                    actualProyecto.FicherosExcluidos = filtros;
                    actualProyecto.Filtrar = HayFiltros;
                    actualProyecto.LastPathBackup = lastRutaBackup;

                    if (proyectos.Any(x => x.Identifier == actualProyecto.Identifier))
                    {
                        foreach (var proyecto in proyectos.Where(proyecto => proyecto.Identifier == actualProyecto.Identifier))
                        {
                            proyecto.ProyectoName = actualProyecto.ProyectoName;
                            proyecto.PathOrigen = actualProyecto.PathOrigen;
                            proyecto.PathDestino = actualProyecto.PathDestino;
                            proyecto.HacerBackup = actualProyecto.HacerBackup;
                            proyecto.PathBackup = actualProyecto.PathBackup;
                            proyecto.FicherosExcluidos = actualProyecto.FicherosExcluidos;
                            proyecto.Filtrar = actualProyecto.Filtrar;
                            proyecto.LastPathBackup = actualProyecto.LastPathBackup;
                        }
                    }
                    else
                    {
                        proyectos.Add(actualProyecto);
                    }

                    string proyectoXML = SerializerXML.getObjectSerialized(proyectos);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(proyectoXML);
                    doc.Save(Resource.archivoProyecto);

                    if (ExisteProyecto())
                    {
                        addProyecto = false;
                        LocalUtilities.WriteTextLog("Guardado proyecto XML " + " | Fecha: " + DateTime.Now.ToString());
                        //MessageBox.Show("Guardado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    LocalUtilities.MensajeError("¡Indique un nombre para el proyecto!");
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError("Error: " + LocalUtilities.getErrorException(ex));
            }
        }

        private void CargarProyectos()
        {
            try
            {
                if (ExisteProyecto())
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Path.Combine(Environment.CurrentDirectory, Resource.archivoProyecto));

                    string resultado = doc.InnerXml;

                    if (resultado != null)
                    {
                        proyectos = SerializerXML.Deserialize_Opcion1<BindingList<Proyecto>>(resultado);
                        if (proyectos.Count() != 0)
                        {
                            cmbProyecto.DataSource = proyectos;
                            cmbProyecto.DisplayMember = nameof(Proyecto.ProyectoName);
                        }
                    }
                }
                else
                {
                    NuevoProyecto();
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError("¡Archivo XML corrupto! \n" + LocalUtilities.getErrorException(ex));
            }
        }

        private void RecargarProyecto()
        {
            if (string.IsNullOrEmpty(actualProyecto?.ProyectoName))
            {
                NuevoProyecto();
            }
            else
            {
                filtros = new BindingList<Filtro>();
                destinos = new List<string>();

                HayFiltros = actualProyecto.Filtrar;
                foreach (Filtro filtro in actualProyecto.FicherosExcluidos)
                {
                    filtros.Add(filtro);
                }

                cmbProyecto.Text = actualProyecto.ProyectoName;
                textOrigen.Text = actualProyecto.PathOrigen;
                treeViewOrigen.Nodes.Clear();
                if (!string.IsNullOrEmpty(textOrigen.Text))
                {
                    countArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen);
                }
                else
                {
                    countArchivosOrigen = 0;
                }

                LimpiarLayoutDestino();
                foreach (string destino in actualProyecto.PathDestino)
                {
                    AddDestinoControl(destino);
                }

                HacerBackup = actualProyecto.HacerBackup;
                rutaBackup = actualProyecto.PathBackup;
                textBackup.Text = actualProyecto.PathBackup;
                lastRutaBackup = actualProyecto.LastPathBackup;
            }
        }

        private void ActualizarProyecto()
        {
            //actualProyecto.FicherosExcluidos.Clear();
            actualProyecto.PathDestino = destinos;

            actualProyecto.Filtrar = HayFiltros;

            actualProyecto.ProyectoName = cmbProyecto.Text;
            actualProyecto.PathOrigen = textOrigen.Text;
            treeViewOrigen.Nodes.Clear();
            if (!string.IsNullOrEmpty(textOrigen.Text))
            {
                countArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen);
            }
            else
            {
                countArchivosOrigen = 0;
            }

            ActualizarDestino();
            LimpiarLayoutDestino();
            foreach (string destino in destinos)
            {
                AddDestinoControl(destino, false);
            }

            actualProyecto.HacerBackup = HacerBackup;
            actualProyecto.PathBackup = rutaBackup;
        }

        private void NuevoProyecto()
        {
            cmbProyecto.Text = string.Empty;
            textOrigen.Text = string.Empty;
            textDestino.Text = string.Empty;
            lastRutaBackup = string.Empty;
            countArchivosOrigen = 0;
            HacerBackup = false;
            HayFiltros = false;
            filtros = new BindingList<Filtro>();

            rutaBackup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            destinos.Clear();
            treeViewOrigen.Nodes.Clear();

            LimpiarLayoutDestino();
        }

        private bool ExisteProyecto()
        {
            return File.Exists(Path.Combine(Environment.CurrentDirectory, Resource.archivoProyecto)); ;
        }

        private void AddDestinoControl(string text, bool addDestino = true)
        {
            Destino destinoControl = new Destino();
            destinoControl.LabelContador = ActualizarTreeView(textDestino, destinoControl.TreeViewDestino, text);
            destinoControl.RutaDestino = text;

            tableLayoutDestino.Controls.Add(destinoControl);
            if (addDestino) destinos.Add(text);
        }

        private void VisibilidadFiltro()
        {
            btnModificarFiltros.Visible = chkBoxFiltros.Checked;
            cmbBoxFiltros.Visible = chkBoxFiltros.Checked;
            HayFiltros = chkBoxFiltros.Checked;
        }

        private void VisibilidadBackup()
        {
            textBackup.Visible = checkBoxBackup.Checked;
            btnRutaBackup.Visible = checkBoxBackup.Checked;
            HacerBackup = checkBoxBackup.Checked;
        }

        private void LimpiarLayoutDestino()
        {
            tableLayoutDestino.Controls.Clear();
        }

        public void ActualizarDestino()
        {
            destinos = new List<string>();
            foreach (Destino destino in tableLayoutDestino.Controls)
            {
                destinos.Add(destino.RutaDestino);
            }
        }

        private void RestaurarBackup()
        {
            if (!string.IsNullOrEmpty(lastRutaBackup))
            {

            }
            else
            {
                LocalUtilities.MensajeError(Resource.noLastBackup);
            }
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

            if (textBackup.Text != null)
            {
                rutaBackup = textBackup.Text;
            }
        }

        private void btnVerCarpetaOrigen_Click(object sender, EventArgs e)
        {
            countArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen);
            ActualizarDatos();
        }

        private void btnVerCarpetaDestino_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textDestino.Text))
            {
                AddDestinoControl(textDestino.Text);
            }
        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            SincronizarCarpetas();
        }

        private void anadirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarProyecto();
        }

        private void checkBoxBackup_CheckedChanged(object sender, EventArgs e)
        {
            VisibilidadBackup();
        }

        private void chkBoxFiltros_CheckedChanged(object sender, EventArgs e)
        {
            VisibilidadFiltro();
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

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!addProyecto)
            {
                LimpiarLayoutDestino();

                actualProyecto = (Proyecto)cmbProyecto.SelectedItem;
                RecargarProyecto();
                ActualizarDatos();
            }
            else
            {
                LocalUtilities.MensajeError("¡Guarde el proyecto actual!");
            }
        }

        private void btnAddProyecto_Click(object sender, EventArgs e)
        {
            if (actualProyecto != null && !string.IsNullOrEmpty(actualProyecto.ProyectoName))
            {
                Guid guid = Guid.NewGuid();
                actualProyecto = new Proyecto(guid);

                // no añadir el proyecto aqui si no antes de guardar?
                //proyectos.Add(proyecto);
                addProyecto = true;

                RecargarProyecto();
                ActualizarDatos();
            }
            else
            {
                LocalUtilities.MensajeError("¡Guarde el proyecto actual!");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarProyecto();
            ActualizarDatos();
        }

        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            RestaurarBackup();
        }

        private void btnPrevisualizar_Click(object sender, EventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarProyecto();
            ActualizarDatos();
        }


        #endregion

        private void recargarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecargarProyecto();
        }
    }
}