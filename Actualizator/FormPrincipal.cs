using BackGroundWorked_ALG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
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
        private bool addingProyecto = false;

        private List<string> destinos = new List<string>();
        private Proyecto actualProyecto;
        private BindingList<Proyecto> proyectos = new BindingList<Proyecto>();
        private BindingList<Filtro> filtros = new BindingList<Filtro>();
        ArchivosTreeView archivosTreeView = new ArchivosTreeView();

        ArchivosTreeView archivosOrigenArbol = new ArchivosTreeView();
        ArchivosTreeView archivosDestinoArbol = new ArchivosTreeView();
        DirectoryInfo dirArbolOrigen;
        TreeView treeviewDestino;

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
            try
            {
                if (!string.IsNullOrEmpty(textOrigen.Text)) btnVerCarpetaOrigen.Visible = true;

                lblArchivosOrigen.Text = countArchivosOrigen.ToString();

                chkBoxFiltros.Checked = HayFiltros;
                checkBoxBackup.Checked = HacerBackup;

                BindingSource bSource = new BindingSource { DataSource = filtros };
                cmbBoxFiltros.DataSource = bSource;

                cmbProyecto.DataSource = proyectos;
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
            }
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

        private ArchivosTreeView GetArchivosTreeView(DirectoryInfo dirInfo)
        {
            ArchivosTreeView archivosTree = new ArchivosTreeView();
            // Rellena a nivel raiz            
            try
            {
                archivosTree.DirName = dirInfo.Name;
                FileInfo[] archivos = dirInfo.GetFiles();

                var test = dirInfo.GetAccessControl();

                foreach (FileInfo archivo in archivos)
                {
                    archivosTree.Archivos.Add(archivo.Name);
                }

                // Rellena las subcarpetas
                foreach (DirectoryInfo directory in dirInfo.GetDirectories())
                {
                    archivosTree.Subdir.Add(GetArchivosTreeView(directory));
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
            }

            return archivosTree;
        }

        private void GetArchivosOrigenArbol()
        {            
            archivosOrigenArbol = GetArchivosTreeView(dirArbolOrigen);
        }

        private void GetArchivoDestinoArbol()
        {
            archivosDestinoArbol = GetArchivosTreeView(dirArbolOrigen);
        }

        /// <summary>
        /// Devuelve archivos modificados dadas dos carpetas
        /// </summary>
        /// <param name="dirOrigen"></param>
        /// <param name="dirDestino"></param>
        /// <returns></returns>
        private ArchivosTreeView GetArchivosModificadosTreeView(DirectoryInfo dirOrigen, DirectoryInfo dirDestino)
        {
            var archivosDestino = dirDestino.GetFiles();
            ArchivosTreeView archivosTree = new ArchivosTreeView();

            try
            {
                // Rellena a nivel raiz            
                archivosTree.DirName = dirOrigen.Name;
                FileInfo[] archivos = dirOrigen.GetFiles();

                foreach (FileInfo archivo in archivos)
                {
                    FileInfo archivoDestino = archivosDestino.Where(x => x.Name == archivo.Name).FirstOrDefault();
                    if (archivoDestino != null)
                    {
                        // reemplaza si es un archivo modificado
                        if (archivo.LastWriteTimeUtc != archivoDestino.LastWriteTimeUtc)
                        {
                            archivosTree.Archivos.Add(archivo.Name);
                        }
                    }
                    else
                    {
                        // copia si es un archivo nuevo
                        archivosTree.Archivos.Add(archivo.Name);
                    }
                }

                // Rellena las subcarpetas
                foreach (DirectoryInfo directory in dirOrigen.GetDirectories())
                {
                    archivosTree.Subdir.Add(GetArchivosModificadosTreeView(directory, dirDestino));
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
            }

            return archivosTree;
        }

        private TreeView PopulateArchivoTreeView(ArchivosTreeView archivosTree, TreeNode treeNode, TreeView treeView = null, bool notRoot = false)
        {
            TreeNode directoryNodeRoot = new TreeNode
            {
                Text = archivosTree.DirName
            };

            // Rellena a nivel raiz
            if (treeNode == null)
            {
                treeView.Nodes.Add(directoryNodeRoot);

                AddFilesStringNode(archivosTree.Archivos, ref directoryNodeRoot);
            }

            // Rellena las subcarpetas
            foreach (var directory in archivosTree.Subdir)
            {
                TreeNode directoryNode = new TreeNode
                {
                    Text = directory.DirName
                };

                if (treeNode == null)
                {
                    directoryNodeRoot.Nodes.Add(directoryNode);
                }
                else
                {
                    treeNode.Nodes.Add(directoryNode);
                }

                AddFilesStringNode(directory.Archivos, ref directoryNode);
                PopulateArchivoTreeView(directory, directoryNode, null, true);
            }

            return treeView;
        }

        private void PopulateArbolOrigen()
        {
            treeViewOrigen = PopulateArchivoTreeView(archivosOrigenArbol, null, treeViewOrigen);
        }

        private void PopulateArbolDestino()
        {
            treeviewDestino = PopulateArchivoTreeView(archivosDestinoArbol, null, treeviewDestino);
        }

        private void AddFilesStringNode(List<string> archivos, ref TreeNode directoryNode)
        {
            if (HayFiltros)
            {
                archivos = FiltrarStringArchivos(archivos);
            }

            foreach (string file in archivos)
            {
                TreeNode fileNode = new TreeNode
                {
                    Text = file
                };

                directoryNode.Nodes.Add(fileNode);
            }
        }

        private List<string> FiltrarStringArchivos(List<string> archivos)
        {
            foreach (Filtro filtro in filtros)
            {
                switch (filtro.cabecera)
                {
                    case Filtrado.TerminaPor:
                        archivos = archivos.Where(x => !x.ToLower().EndsWith(filtro.filtro.ToLower())).ToList();
                        break;
                    case Filtrado.Completo:
                        archivos = archivos.Where(x => !x.ToLower().Equals(filtro.filtro.ToLower())).ToList();
                        break;
                }
            }

            return archivos;
        }

        private int ActualizarTreeView(TextBox txtBox, TreeView treeView, bool origen, string ruta = null)
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
                        dirArbolOrigen = dirInfo;

                        if (ComprobarAcceso(dirInfo))
                        {
                            // Limpiar el treeView
                            treeView.Nodes.Clear();

                            // Poblar el TreeView
                            MyProcessControl backWork = new MyProcessControl(this);
                            backWork.TextoMostrar = Resource.extrayendoInfo;
                            backWork.BackColor = Color.White;
                            backWork.Enabled = true;
                            backWork.Visible = true;
                            backWork.TicsNum = 5;
                            backWork.Height = 75;
                            backWork.Width = 200;
                            backWork.CuadroColorRelleno = Color.CornflowerBlue;
                            backWork.Efecto = MyProcessControl.eEfecto.Kid;

                            if (origen)
                            {
                                backWork.Proceso(GetArchivosOrigenArbol, PopulateArbolOrigen);
                                backWork.Visible = false;
                            }
                            else
                            {
                                backWork.Proceso(GetArchivoDestinoArbol, PopulateArbolDestino);
                                backWork.Visible = false;
                            }

                            var contadorTodos = dirInfo.GetFiles("*", SearchOption.AllDirectories);
                            if (HayFiltros)
                            {
                                contadorTodos = FiltrarArchivos(contadorTodos);
                            }
                            contador = contadorTodos.Count();
                            ActualizarDatos();
                        }
                        else
                        {
                            LocalUtilities.MensajeError(Resource.accessDenied);
                        }
                    }
                    else
                    {
                        if (origen)
                        {
                            LocalUtilities.MensajeError(Resource.comprobarOrigen);
                        }
                        else
                        {
                            LocalUtilities.MensajeError(Resource.comprobarDestino);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
            }

            return contador;
        }

        private bool ComprobarAcceso(DirectoryInfo dirInfo)
        {
            bool access = true;

            try
            {
                var test = dirInfo.GetAccessControl();                

                foreach (DirectoryInfo directory in dirInfo.GetDirectories())
                {
                    access = ComprobarAcceso(directory);
                    if (!access)
                        break;
                }
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

            return access;
        }

        private void CrerBackup(ArchivosTreeView archivosOrigen, string rutaOrigen)
        {
            try
            {
                // Crear la ruta de carpeta
                string temporalRutaBackup = Path.Combine(rutaBackup, DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", ""));
                // Crear la carpeta
                Directory.CreateDirectory(temporalRutaBackup);
                lastRutaBackup = temporalRutaBackup;
                // Copiar todos los archivos
                CopiarArchivos(archivosOrigen, temporalRutaBackup, rutaOrigen);

                LocalUtilities.WriteTextLog(Resource.mensajeBackup + temporalRutaBackup + Resource.mensajeFecha + DateTime.Now.ToString(), lblLog);
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="archivosOrigen"></param>
        /// <param name="rutaDestino"></param>
        /// <param name="rutaOrigen"></param>
        private void CopiarArchivos(ArchivosTreeView archivosOrigen, string rutaDestino, string rutaOrigen, bool notRoot = false)
        {
            try
            {
                if (HayFiltros) archivosOrigen.Archivos = FiltrarStringArchivos(archivosOrigen.Archivos);

                string directoryRoot;
                if (notRoot)
                {
                    directoryRoot = Path.Combine(rutaDestino, archivosOrigen.DirName);
                    Directory.CreateDirectory(directoryRoot);
                }
                else
                {
                    directoryRoot = rutaDestino;
                }

                // Copia nivel raiz
                foreach (string archivo in archivosOrigen.Archivos)
                {
                    File.Copy(Path.Combine(rutaOrigen, archivo), Path.Combine(directoryRoot, archivo));
                }

                // Rellena las subcarpetas
                foreach (ArchivosTreeView directory in archivosOrigen.Subdir)
                {
                    CopiarArchivos(directory, directoryRoot, Path.Combine(rutaOrigen, directory.DirName), true);
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        private void SincronizarCarpetas()
        {
            try
            {
                ActualizarDestino();
                DirectoryInfo dirOrigen = new DirectoryInfo(textOrigen.Text);
                List<DirectoryInfo> directoriesDestino = GetAllDestinos();
                if (directoriesDestino == null || directoriesDestino.Count() == 0)
                {
                    return;
                }

                if (dirOrigen.Exists)
                {
                    ArchivosTreeView archivosOrigen = GetArchivosTreeView(dirOrigen);

                    if (HacerBackup) CrerBackup(archivosOrigen, dirOrigen.FullName);

                    foreach (DirectoryInfo dirDestino in directoriesDestino)
                    {
                        ArchivosTreeView archivosModificados = GetArchivosModificadosTreeView(dirOrigen, dirDestino);
                        CopiarArchivos(archivosModificados, dirDestino.FullName, dirOrigen.FullName);
                    }

                    GuardarProyecto();
                    LocalUtilities.WriteTextLog(Resource.sincronizarCarpetas + DateTime.Now.ToString(), lblLog);
                }
                else
                {
                    LocalUtilities.MensajeError(Resource.comprobarOrigen);
                }

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
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

                    string proyectoXML = SerializerXML.getObjectSerialized(proyectos);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(proyectoXML);
                    doc.Save(Resource.archivoProyecto);

                    if (ExisteProyecto())
                    {
                        addProyecto = false;
                        btnCancelarAdd.Visible = false;
                        LocalUtilities.WriteTextLog(Resource.guardadoXML + DateTime.Now.ToString(), lblLog);
                    }
                }
                else
                {
                    LocalUtilities.MensajeError(Resource.nombreProyecto);
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
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
                LocalUtilities.MensajeError(Resource.xmlCorrupto + "\n" + LocalUtilities.getErrorException(ex));
            }
        }

        private void RecargarProyecto()
        {
            try
            {
                if (addProyecto && string.IsNullOrEmpty(actualProyecto?.ProyectoName))
                {
                    NuevoProyecto();
                }
                else if (actualProyecto != null)
                {
                    filtros = new BindingList<Filtro>();
                    destinos = new List<string>();

                    HayFiltros = actualProyecto.Filtrar;
                    if (actualProyecto?.FicherosExcluidos != null)
                    {
                        foreach (Filtro filtro in actualProyecto.FicherosExcluidos)
                        {
                            filtros.Add(filtro);
                        }
                    }
                    if (string.IsNullOrEmpty(cmbProyecto.Text)) cmbProyecto.Text = actualProyecto?.ProyectoName;
                    textOrigen.Text = actualProyecto?.PathOrigen;
                    treeViewOrigen.Nodes.Clear();
                    if (!string.IsNullOrEmpty(textOrigen.Text))
                    {
                        countArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen, true);
                    }
                    else
                    {
                        countArchivosOrigen = 0;
                    }

                    LimpiarLayoutDestino();
                    foreach (string destino in actualProyecto?.PathDestino)
                    {
                        AddDestinoControl(destino);
                    }

                    HacerBackup = actualProyecto.HacerBackup;
                    rutaBackup = actualProyecto?.PathBackup;
                    textBackup.Text = actualProyecto?.PathBackup;
                    lastRutaBackup = actualProyecto?.LastPathBackup;
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
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
                countArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen, true);
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
            cmbProyecto.Text = Resource.nuevoProyecto;
            textOrigen.Text = string.Empty;
            textDestino.Text = string.Empty;
            lastRutaBackup = string.Empty;
            countArchivosOrigen = 0;
            HacerBackup = false;
            HayFiltros = false;
            filtros = new BindingList<Filtro>();

            rutaBackup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            textBackup.Text = rutaBackup;

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
            treeviewDestino = destinoControl.TreeViewDestino;
            destinoControl.LabelContador = ActualizarTreeView(textDestino, treeviewDestino, false, text);
            destinoControl.TreeViewDestino = treeviewDestino;
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
                DialogResult dialogResult = MessageBox.Show(Resource.mensajeRestaurarBackup, Resource.restaurar, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DirectoryInfo dirOrigen = new DirectoryInfo(lastRutaBackup);
                    List<DirectoryInfo> directoriesDestino = GetAllDestinos();
                    if (directoriesDestino == null || directoriesDestino.Count() == 0)
                    {
                        LocalUtilities.MensajeError(Resource.comprobarDestino);
                        return;
                    }

                    if (dirOrigen.Exists)
                    {
                        foreach (DirectoryInfo dirDestino in directoriesDestino)
                        {
                            //ComprobarAcceso(dirOrigen);
                            CopiarArchivos(GetArchivosTreeView(dirOrigen), dirDestino.FullName, dirOrigen.FullName);
                        }

                        GuardarProyecto();
                        LocalUtilities.WriteTextLog(Resource.restaurarBackup + DateTime.Now.ToString(), lblLog);
                    }
                    else
                    {
                        LocalUtilities.MensajeError(Resource.noLastBackup);
                    }
                }
            }
            else
            {
                LocalUtilities.MensajeError(Resource.noLastBackup);
            }
        }

        private void CancelarProyecto()
        {
            try
            {
                addProyecto = false;
                IReadOnlyList<Proyecto> proyectoABorrar = proyectos.Where(x => x.Identifier == actualProyecto.Identifier).ToList();

                foreach (var proyecto in proyectoABorrar)
                {
                    proyectos.Remove(proyecto);
                }

                cmbProyecto.DataSource = proyectos;
                actualProyecto = (Proyecto)cmbProyecto.SelectedItem;
                RecargarProyecto();
                btnCancelarAdd.Visible = false;
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        private void BorrarProyecto()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(Resource.mensajeError + LocalUtilities.getErrorException(ex));
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
            countArchivosOrigen = ActualizarTreeView(textOrigen, treeViewOrigen, true);
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
            if (!addProyecto || addingProyecto)
            {
                LimpiarLayoutDestino();

                actualProyecto = (Proyecto)cmbProyecto.SelectedItem;
                RecargarProyecto();
                ActualizarDatos();
            }
            else
            {
                LocalUtilities.MensajeError(Resource.alertaGuardarProyecto);
            }
        }

        private void btnAddProyecto_Click(object sender, EventArgs e)
        {
            if (actualProyecto != null && !string.IsNullOrEmpty(actualProyecto.ProyectoName))
            {
                Guid guid = Guid.NewGuid();
                actualProyecto = new Proyecto(guid);
                addProyecto = true;
                addingProyecto = true;

                proyectos.Add(actualProyecto);
                cmbProyecto.DataSource = proyectos;
                if (actualProyecto != null) cmbProyecto.SelectedItem = actualProyecto;

                btnCancelarAdd.Visible = true;
                addingProyecto = false;
            }
            else
            {
                LocalUtilities.MensajeError(Resource.alertaGuardarProyecto);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarProyecto();
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

        private void recargarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecargarProyecto();
        }

        private void btnCancelarAdd_Click(object sender, EventArgs e)
        {
            CancelarProyecto();
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addProyecto)
            {
                CancelarProyecto();
            }
            else
            {
                BorrarProyecto();
            }
        }

        #endregion


    }
}