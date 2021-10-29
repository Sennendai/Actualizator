using BackGroundWorked_ALG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Utilities.Clases.XML;

namespace Actualizator
{
    /// En el codigo se llama Proyecto a lo que en la interfaz y de cara al usuario se llama Configuracion

    public partial class FormPrincipal : Form
    {
        #region· - VARIABLES

        private int countArchivosOrigen = 0;
        public int CountArchivosOrigen
        {
            get { return countArchivosOrigen; }
            set
            {
                if(value!= countArchivosOrigen)
                {
                    countArchivosOrigen = value;
                    VisibilidadManipularArchivos();
                    treeViewOrigen.ExpandAll();
                }
            }
        }
        private string rutaBackup;
        private string lastRutaBackup;

        private List<string> destinos = new List<string>();
        private Proyecto actualProyecto;
        private BindingList<Proyecto> proyectos = new BindingList<Proyecto>();
        private BindingList<Filtro> filtros = new BindingList<Filtro>();

        private ArchivosTreeView archivosOrigenArbol = new ArchivosTreeView();
        private ArchivosTreeView archivosDestinoArbol = new ArchivosTreeView();
        private DirectoryInfo dirArbolOrigen;
        private TreeView treeviewDestino;

        #region· BOOLEANOS 
        private bool hayFiltros = false;
        private bool hacerBackup = false;
        private bool sobreescribir = false;
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
                    lblFiltrosCount.Visible = hayFiltros;
                    lblFiltrosCount.Text = StringResource.filtrosCount + filtros.Count().ToString();
                }
            }
        }

        private bool addProyecto = false;
        private bool modificandoProyecto = false;
        private bool destinoIntroducido = false;
        private bool iniciando;
        #endregion

        #endregion

        #region· - CONSTRUCTOR

        public FormPrincipal()
        {
            InitializeComponent();
            CargarDatos();
        }

        #endregion

        #region· - FUNCIONES

        private void CargarDatos()
        {
            iniciando = true;
            // ruta backup por defecto
            rutaBackup = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            CargarProyectos();

            lblArchivosOrigen.Text = CountArchivosOrigen.ToString();

            textBackup.Text = rutaBackup;
            cmbProyecto.DisplayMember = nameof(Proyecto.ProyectoName);
            lblLog.Text = StringResource.saludo;
            cmbProyecto.SelectedIndex = -1;
            iniciando = false;
        }

        private void ActualizarDatos()
        {
            try
            {
                lblArchivosOrigen.Text = CountArchivosOrigen.ToString();

                chkBoxFiltros.Checked = HayFiltros;
                checkBoxBackup.Checked = HacerBackup;

                lblFiltrosCount.Text = StringResource.filtrosCount + filtros.Count().ToString();                

                cmbProyecto.DataSource = proyectos;
                if (actualProyecto != null) cmbProyecto.SelectedItem = actualProyecto;
                //cmbProyecto.Refresh();
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        /// <summary>
        /// Muestra un BrowserDialog y rellena el texto de un textoBox con la ruta elegida, hace el boton visible si se pasa un boton
        /// </summary>
        private void ElegirRuta(TextBox txtBox, Button btn = null)
        {
            folderBrowserDlg.SelectedPath = txtBox.Text;
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtBox.Text = folderBrowserDlg.SelectedPath;
                if (btn != null) btn.Visible = true;
            }
        }

        #region· ARCHIVOS

        /// <summary>
        /// Dado un DirectoryInfo crea un objecto con la estructura de los directorios y subdiresctorios con sus respectivos archivos  
        /// </summary>
        /// <returns></returns>
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
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
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
        /// Copia los archivos en una carpeta nueva en la ruta indicada
        /// </summary>
        private void CrerBackup(List<DirectoryInfo> directoriesDestino, DirectoryInfo dirOrigen)
        {
            try
            {
                // Crear la ruta de carpeta
                string temporalRutaBackup = Path.Combine(rutaBackup, DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", ""));
                // Crear la carpeta
                Directory.CreateDirectory(temporalRutaBackup);
                lastRutaBackup = temporalRutaBackup;
                // Copiar todos los archivos
                foreach (DirectoryInfo dirDestino in directoriesDestino)
                {
                    string newDirDestino = Path.Combine(temporalRutaBackup, dirDestino.Name);
                    Directory.CreateDirectory(newDirDestino);

                    ArchivosTreeView archivosACopiar = GetArchivosTreeView(dirOrigen);
                    CopiarArchivos(archivosACopiar, newDirDestino, dirOrigen.FullName);
                }

                LocalUtilities.WriteTextLog(StringResource.mensajeBackup + temporalRutaBackup + StringResource.mensajeFecha + DateTime.Now.ToString(), lblLog);
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        /// <summary>
        /// Copia los archivos de origen en la ruta indicada, crea las subcarpetas si no existen
        /// </summary>
        private void CopiarArchivos(ArchivosTreeView archivosOrigen, string rutaDestino, string rutaOrigen, bool root = true)
        {
            try
            {
                if (HayFiltros) archivosOrigen.Archivos = FiltrarStringArchivos(archivosOrigen.Archivos);

                string directoryRoot;
                if (root)
                {
                    directoryRoot = rutaDestino;
                }
                else
                {
                    directoryRoot = Path.Combine(rutaDestino, archivosOrigen.DirName);
                    Directory.CreateDirectory(directoryRoot);
                }

                // Copia nivel raiz
                foreach (string archivo in archivosOrigen.Archivos)
                {
                    File.Copy(Path.Combine(rutaOrigen, archivo), Path.Combine(directoryRoot, archivo));
                }

                // Rellena las subcarpetas
                foreach (ArchivosTreeView directory in archivosOrigen.Subdir)
                {
                    CopiarArchivos(directory, directoryRoot, Path.Combine(rutaOrigen, directory.DirName), false);
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
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

        private List<DirectoryInfo> GetAllDestinos()
        {
            List<DirectoryInfo> allDestinos = new List<DirectoryInfo>();

            foreach (var destino in destinos)
            {
                DirectoryInfo dirDestino = new DirectoryInfo(destino);
                if (dirDestino.Exists) allDestinos.Add(dirDestino);
                else
                {
                    LocalUtilities.MensajeInfo(StringResource.comprobarDestino);
                    return null;
                }
            }

            return allDestinos;
        }

        #endregion

        #region· ARBOL

        /// <summary>
        /// Devuelve archivos modificados dadas dos rutas, busca dentro de todas als subcarpetas
        /// </summary>
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
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }

            return archivosTree;
        }

        /// <summary>
        /// Rellena un TreeView dado un objecto que contiene la estructura de los archivos
        /// </summary>
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

        private void SetIconForNode(TreeNode node, int imageindex, Color color)
        {
            node.ImageIndex = imageindex;
            node.SelectedImageIndex = imageindex;
            node.ForeColor = color;

            if (node.Nodes.Count != 0)
            {
                foreach (TreeNode tn in node.Nodes)
                    SetIconForNode(tn, imageindex, color);
            }
        }

        private void PopulateArbolOrigen()
        {
            treeViewOrigen = PopulateArchivoTreeView(archivosOrigenArbol, null, treeViewOrigen);
            CountArchivosOrigen = archivosOrigenArbol.Archivos.Count;
        }

        private void PopulateArbolDestino()
        {
            treeviewDestino = PopulateArchivoTreeView(archivosDestinoArbol, null, treeviewDestino);
        }

        /// <summary>
        /// Introduce en un treeView la ruta de carpeta especificada
        /// </summary>
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
                            backWork.TextoMostrar = StringResource.extrayendoInfo;
                            backWork.BackColor = Color.White;
                            backWork.Enabled = true;
                            backWork.Visible = true;
                            backWork.TicsNum = 5;
                            backWork.Height = 75;
                            backWork.Width = 200;
                            backWork.CuadroColorRelleno = Color.CornflowerBlue;
                            backWork.Efecto = MyProcessControl.eEfecto.Fiu;

                            if (origen)
                            {
                                backWork.Proceso(GetArchivosOrigenArbol, PopulateArbolOrigen);
                                backWork.Visible = false;
                            }
                            else
                            {
                                archivosDestinoArbol = GetArchivosTreeView(dirArbolOrigen);
                                treeviewDestino = PopulateArchivoTreeView(archivosDestinoArbol, null, treeviewDestino);
                                //backWork.Proceso(GetArchivoDestinoArbol, PopulateArbolDestino);
                                backWork.Visible = false;
                                destinoIntroducido = true;
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
                            LocalUtilities.MensajeInfo(StringResource.accessDenied);
                        }
                    }
                    else
                    {
                        if (origen)
                        {
                            LocalUtilities.MensajeInfo(StringResource.comprobarOrigen);
                        }
                        else
                        {
                            LocalUtilities.MensajeInfo(StringResource.comprobarDestino);
                            destinoIntroducido = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }

            return contador;
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

        #endregion

        #region· BOTONES              

        private void GuardarProyecto(bool borrando = false)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbProyecto.Text))
                {
                    cmbProyecto.Text = StringResource.nuevoProyecto + "_" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", "");
                }
                
                if ((ComprobarNombreProyecto() || !addProyecto) || borrando)
                {
                    LimpiarLayoutDestino();
                    ActualizarDestino();

                    if (actualProyecto == null)
                    {
                        Guid guid = Guid.NewGuid();
                        actualProyecto = new Proyecto(guid);
                    }

                    actualProyecto.ProyectoName = cmbProyecto.Text;
                    actualProyecto.PathOrigen = textOrigen.Text;
                    actualProyecto.PathDestino = destinos;
                    actualProyecto.HacerBackup = HacerBackup;
                    actualProyecto.PathBackup = rutaBackup;
                    actualProyecto.FicherosExcluidos = filtros;
                    actualProyecto.Filtrar = HayFiltros;
                    actualProyecto.LastPathBackup = lastRutaBackup;

                    bool modificando = false;
                    if (proyectos.Any(x => x.Identifier == actualProyecto.Identifier))
                    {
                        modificando = true;
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

                    if (!addProyecto && !modificando)
                    {
                        proyectos.Add(actualProyecto);
                        VisibilidadBotonesControl();
                    }
                    string proyectoXML = SerializerXML.getObjectSerialized(proyectos);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(proyectoXML);
                    doc.Save(StringResource.archivoProyecto);

                    if (ExisteProyecto())
                    {
                        addProyecto = false;
                        btnAddProyecto.Image = Properties.Resources.add;
                        cmbProyecto.Enabled = true;
                        cmbProyecto.Visible = true;
                        txtProyecto.Visible = false;
                        txtProyecto.Text = string.Empty;
                        LocalUtilities.WriteTextLog(StringResource.guardadoXML + DateTime.Now.ToString(), lblLog);
                    }
                }
                else
                {
                    LocalUtilities.MensajeInfo(StringResource.mismoNombre);
                }

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        /// <summary>
        /// Al asociar el DataSource del comboBox de los proyectos se llama a la funcion de IndexChanged la cual actualiza el proyecto
        /// </summary>
        private void CargarProyectos()
        {
            try
            {
                if (ExisteProyecto())
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Path.Combine(Environment.CurrentDirectory, StringResource.archivoProyecto));

                    string resultado = doc.InnerXml;

                    if (resultado != null)
                    {
                        proyectos = SerializerXML.Deserialize_Opcion1<BindingList<Proyecto>>(resultado);
                        if (proyectos.Count() != 0)
                        {
                            cmbProyecto.DataSource = proyectos;
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
                LocalUtilities.MensajeError(StringResource.xmlCorrupto + LocalUtilities.getErrorException(ex));
            }
        }

        private void RecargarProyecto()
        {
            try
            {
                if (addProyecto && string.IsNullOrEmpty(actualProyecto?.ProyectoName))
                {
                    NuevoProyecto();
                    VisibilidadesTodas();
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
                        ActualizarTreeView(textOrigen, treeViewOrigen, true);
                    }
                    else
                    {
                        CountArchivosOrigen = 0;
                    }

                    LimpiarLayoutDestino();
                    if (actualProyecto?.PathDestino?.Count() != 0)
                    {
                        textDestino.Text = actualProyecto?.PathDestino.FirstOrDefault();
                    }
                    else
                    {
                        textDestino.Text = string.Empty;
                    }
                    if (actualProyecto?.PathDestino != null)
                    {
                        foreach (string destino in actualProyecto?.PathDestino)
                        {
                            AddDestinoControl(destino);
                        }

                    }
                    HacerBackup = actualProyecto.HacerBackup;
                    rutaBackup = actualProyecto?.PathBackup;
                    textBackup.Text = actualProyecto?.PathBackup;
                    lastRutaBackup = actualProyecto?.LastPathBackup;

                    VisibilidadesTodas();
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        private void ActualizarProyecto()
        {

            if (actualProyecto != null)
            {
                actualProyecto.PathDestino = destinos;
                actualProyecto.Filtrar = HayFiltros;
                actualProyecto.ProyectoName = cmbProyecto.Text;
                actualProyecto.PathOrigen = textOrigen.Text;
                actualProyecto.HacerBackup = HacerBackup;
                actualProyecto.PathBackup = rutaBackup;
            }

            treeViewOrigen.Nodes.Clear();
            if (!string.IsNullOrEmpty(textOrigen.Text))
            {
                ActualizarTreeView(textOrigen, treeViewOrigen, true);
            }
            else
            {
                CountArchivosOrigen = 0;
            }

            LimpiarLayoutDestino();
            ActualizarDestino();

            foreach (string destino in destinos)
            {
                AddDestinoControl(destino, false);
            }


        }

        private void NuevoProyecto()
        {
            textOrigen.Text = string.Empty;
            textDestino.Text = string.Empty;
            lastRutaBackup = string.Empty;
            CountArchivosOrigen = 0;
            HacerBackup = false;
            HayFiltros = false;
            filtros = new BindingList<Filtro>();

            rutaBackup = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            textBackup.Text = rutaBackup;

            destinos.Clear();
            treeViewOrigen.Nodes.Clear();

            LimpiarLayoutDestino();
        }

        /// <summary>
        /// Añade un control en el tableLayout de Destino
        /// </summary>
        /// <param name="addDestino">Indica si se añade la ruta a la lista de destinos</param>
        private void AddDestinoControl(string text, bool addDestino = true)
        {
            cDestino destinoControl = new cDestino();
            treeviewDestino = destinoControl.TreeViewDestino;
            destinoControl.LabelContador = ActualizarTreeView(textDestino, treeviewDestino, false, text);
            if (destinoIntroducido)
            {
                destinoControl.TreeViewDestino = treeviewDestino;
                destinoControl.RutaDestino = text;

                tableLayoutDestino.Controls.Add(destinoControl);
                if (addDestino) destinos.Add(text);
            }
        }

        private void LimpiarLayoutDestino()
        {
            tableLayoutDestino.Controls.Clear();
        }

        public void ActualizarDestino()
        {
            destinos = new List<string>();
            if (!string.IsNullOrEmpty(textDestino.Text) && ComprobarDestino())
            {
                destinos.Add(textDestino.Text);
            }
            foreach (cDestino destino in tableLayoutDestino.Controls)
            {
                destinos.Add(destino.RutaDestino);
            }
        }

        private void RestaurarBackup()
        {
            if (!string.IsNullOrEmpty(lastRutaBackup))
            {
                DialogResult dialogResult = MessageBox.Show(StringResource.mensajeRestaurarBackup, StringResource.restaurar, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DirectoryInfo dirOrigen = new DirectoryInfo(lastRutaBackup);
                    List<DirectoryInfo> directoriesDestino = GetAllDestinos();
                    if (directoriesDestino != null && directoriesDestino.Count() != 0)
                    {
                        foreach (DirectoryInfo dirDestino in directoriesDestino)
                        {
                            if (dirOrigen.Exists)
                            {

                                {
                                    CopiarArchivos(GetArchivosTreeView(dirOrigen), dirDestino.FullName, dirOrigen.FullName);
                                }
                            }
                            else
                            {
                                LocalUtilities.MensajeInfo(StringResource.noLastBackup);
                            }
                        }

                        GuardarProyecto();
                        LocalUtilities.WriteTextLog(StringResource.restaurarBackup + DateTime.Now.ToString(), lblLog);
                    }
                    else
                    {
                        LocalUtilities.MensajeInfo(StringResource.comprobarDestino);
                    }
                }
            }
            else
            {
                LocalUtilities.MensajeInfo(StringResource.noLastBackup);
            }
        }

        private void CancelarProyecto()
        {
            try
            {
                addProyecto = false;
                BorrarProyecto();
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        private void BorrarProyecto()
        {
            try
            {
                IReadOnlyList<Proyecto> proyectoABorrar = proyectos.Where(x => x.Identifier == actualProyecto.Identifier).ToList();

                foreach (var proyecto in proyectoABorrar)
                {
                    proyectos.Remove(proyecto);
                    LocalUtilities.WriteTextLog(StringResource.proyecto + proyecto.ProyectoName + StringResource.borrado + DateTime.Now.ToString(), lblLog);
                }

                cmbProyecto.DataSource = proyectos;
                actualProyecto = (Proyecto)cmbProyecto.SelectedItem;
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        #endregion

        #region· COMPROBACIONES

        private bool ComprobarNombreProyecto()
        {
            foreach (var proyecto in proyectos)
            {
                if (proyecto.ProyectoName != null)
                {
                    if (addProyecto)
                    {
                        if (proyecto.ProyectoName.Equals(actualProyecto?.ProyectoName) ||
                            proyecto.ProyectoName.Equals(txtProyecto.Text))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (proyecto.ProyectoName.Equals(actualProyecto?.ProyectoName) ||
                            proyecto.ProyectoName.Equals(cmbProyecto.Text))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool ExisteProyecto()
        {
            return File.Exists(Path.Combine(Environment.CurrentDirectory, StringResource.archivoProyecto)); ;
        }

        private bool ComprobarDestino()
        {
            foreach (string destino in destinos)
            {
                if (destino.Equals(textDestino.Text))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Comprueba si se tiene permiso para modificar la ruta especificada
        /// </summary>
        private bool ComprobarAcceso(DirectoryInfo dirInfo)
        {
            bool access = true;

            try
            {
                var accessControl = dirInfo.GetAccessControl();

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

        #endregion

        #region· VISIBILIDAD

        private void VisibilidadFiltro()
        {
            btnModificarFiltros.Visible = chkBoxFiltros.Checked;
            HayFiltros = chkBoxFiltros.Checked;
        }

        private void VisibilidadBackup()
        {
            textBackup.Visible = checkBoxBackup.Checked;
            btnRutaBackup.Visible = checkBoxBackup.Checked;
            HacerBackup = checkBoxBackup.Checked;
        }

        private void VisibilidadBotonesControl()
        {
            if (actualProyecto != null && !addProyecto)
            {
                btnBorrar.Visible = true;
                btnRecargar.Visible = true;
                btnActualizar.Visible = true;
                btnAddProyecto.Visible = true;
            }
            else
            {
                btnBorrar.Visible = false;
                btnRecargar.Visible = false;
                btnActualizar.Visible = false;
                if (!addProyecto) btnAddProyecto.Visible = false;
            }
        }

        private void VisibilidadActualizar()
        {
            if (!string.IsNullOrEmpty(textOrigen.Text))
            {
                btnActualizar.Visible = true;
            }
            else
            {
                btnActualizar.Visible = false;
            }
        }

        private void VisibilidadManipularArchivos()
        {
            if (treeViewOrigen.Nodes.Count != 0 && destinos.Count() != 0)
            {
                btnSincronizar.Visible = true;
                btnPrevisualizar.Visible = true;
                chkBoxSobreescribir.Visible = true;
            }
            else
            {
                btnSincronizar.Visible = false;
                btnPrevisualizar.Visible = false;
                chkBoxSobreescribir.Visible = false;
            }
        }

        private void VisibilidadRestaurarBackup()
        {
            if (!string.IsNullOrEmpty(actualProyecto.LastPathBackup))
            {
                btnRestaurarBackup.Visible = true;
            }
            else
            {
                btnRestaurarBackup.Visible = false;
            }
        }

        private void VisibilidadesTodas()
        {
            VisibilidadBackup();
            VisibilidadBotonesControl();
            VisibilidadFiltro();
            VisibilidadManipularArchivos();
            VisibilidadRestaurarBackup();
        }
        #endregion

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

                    if (HacerBackup) CrerBackup(directoriesDestino, dirOrigen);

                    foreach (DirectoryInfo dirDestino in directoriesDestino)
                    {
                        ArchivosTreeView archivosModificados = GetArchivosModificadosTreeView(dirOrigen, dirDestino);
                        CopiarArchivos(archivosModificados, dirDestino.FullName, dirOrigen.FullName);
                    }

                    GuardarProyecto();
                    LocalUtilities.WriteTextLog(StringResource.sincronizarCarpetas + DateTime.Now.ToString(), lblLog);
                }
                else
                {
                    LocalUtilities.MensajeInfo(StringResource.comprobarOrigen);
                }

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex));
            }
        }

        #endregion

        #region· - EVENTOS

        private void btnRutaOrigen_Click(object sender, EventArgs e)
        {
            ElegirRuta(textOrigen);
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
            else
            {
                LocalUtilities.MensajeInfo(StringResource.noDestino);
            }
        }

        private void btnVerCarpetaOrigen_Click(object sender, EventArgs e)
        {
            ActualizarTreeView(textOrigen, treeViewOrigen, true);
            ActualizarDatos();
        }

        private void btnVerCarpetaDestino_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textDestino.Text) && ComprobarDestino())
            {
                AddDestinoControl(textDestino.Text);
            }

            VisibilidadManipularArchivos();
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
                formFiltros = new FormFiltros(textOrigen.Text, filtros);
            }
            else
            {
                formFiltros = new FormFiltros(textOrigen.Text);
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

        // TODO: No actualiza la coleccion al añadir por textbox
        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!addProyecto || modificandoProyecto)
            {
                if (!iniciando)
                {
                    LimpiarLayoutDestino();

                    actualProyecto = (Proyecto)cmbProyecto.SelectedItem;
                    RecargarProyecto();
                    ActualizarDatos();
                }
                else
                {
                    ActualizarDatos();
                }
            }
            else
            {
                LocalUtilities.MensajeInfo(StringResource.alertaGuardarProyecto);
            }
        }

        private void btnAddProyecto_Click(object sender, EventArgs e)
        {
            if (actualProyecto != null && !string.IsNullOrEmpty(actualProyecto.ProyectoName))
            {
                Guid guid = Guid.NewGuid();
                actualProyecto = new Proyecto(guid);
                addProyecto = true;
                modificandoProyecto = true;

                proyectos.Add(actualProyecto);
                cmbProyecto.DataSource = proyectos;


                btnAddProyecto.Image = Properties.Resources.delete;
                modificandoProyecto = false;

                RecargarProyecto();

                cmbProyecto.Enabled = false;
                cmbProyecto.Visible = false;
                txtProyecto.Visible = true;
            }
            else if (addProyecto)
            {
                CancelarProyecto();
                btnAddProyecto.Image = Properties.Resources.add;

                RecargarProyecto();
                cmbProyecto.Enabled = true;
                cmbProyecto.Visible = true;
                txtProyecto.Visible = false;
            }
            else
            {
                LocalUtilities.MensajeInfo(StringResource.alertaGuardarProyecto);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (actualProyecto != null || !string.IsNullOrEmpty(textOrigen.Text)) ActualizarProyecto();
        }

        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            //RestaurarBackup();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProyecto.Text)) cmbProyecto.Text = txtProyecto.Text;
            GuardarProyecto();
            ActualizarDatos();
            VisibilidadesTodas();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            RecargarProyecto();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(StringResource.mensajeBorrarProyecto, StringResource.borrar, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                addProyecto = true;
                modificandoProyecto = true;

                BorrarProyecto();
                GuardarProyecto(true);
                VisibilidadesTodas();

                addProyecto = false;
                modificandoProyecto = false;

                if (proyectos.Count() == 0)
                {
                    btnBorrar.Visible = false;
                    btnRecargar.Visible = false;
                    btnActualizar.Visible = false;
                    if (!addProyecto) btnAddProyecto.Visible = false;
                }
            }
        }

        private void textOrigen_TextChanged(object sender, EventArgs e)
        {
            VisibilidadActualizar();
        }

        private void chkBoxSobreescribir_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxSobreescribir.Checked)
            {
                sobreescribir = true;
                warningImage.Visible = true;
            }
            else
            {
                sobreescribir = false;
                warningImage.Visible = false;
            }
            
        }
        #endregion

        
    }
}