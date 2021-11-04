using Actualizator.Clases;
using Actualizator.Forms;
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

    public partial class frmPrincipal : Form
    {
        #region· - VARIABLES

        private int countArchivosOrigen = 0;
        public int CountArchivosOrigen
        {
            get { return countArchivosOrigen; }
            set
            {
                if (value != countArchivosOrigen || !addProyecto)
                {
                    countArchivosOrigen = value;
                    lblArchivosOrigen.Text = countArchivosOrigen.ToString();
                    VisibilidadManipularArchivos();
                    treeViewOrigen.ExpandAll();
                }
            }
        }
        private string rutaBackup;
        private string lastRutaBackup;
        private string dateTimeFormateado
        {
            get
            {
                return DateTime.Now.ToString().Replace("/", "-").Replace(" ", "_").Replace(":", "");
            }
        }

        private List<string> _rutasDestinos;
        public List<string> RutasDestinos 
        {
            get { return _rutasDestinos; }
            set
            {
                if (value!= _rutasDestinos)
                {
                    _rutasDestinos = value;                    
                }
            }
        }
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

        public frmPrincipal()
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
            RutasDestinos = new List<string>();

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
                chkBoxFiltros.Checked = HayFiltros;
                checkBoxBackup.Checked = HacerBackup;

                lblFiltrosCount.Text = StringResource.filtrosCount + filtros.Count().ToString();

                cmbProyecto.DataSource = proyectos;
                if (actualProyecto != null) cmbProyecto.SelectedItem = actualProyecto;
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
                if (btn != null) { btn.Visible = true; }
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
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
                // Crea la carpeta del proyecto
                string carpetaRutaBackup = Path.Combine(rutaBackup, actualProyecto.ProyectoName);
                Directory.CreateDirectory(carpetaRutaBackup);
                // Si ya hay backups, conserva los 3 mas nuevos y borra el resto
                ComprobarBackUpViejos(carpetaRutaBackup);
                // Crea la carpeta temporal
                string temporalRutaBackup = Path.Combine(carpetaRutaBackup, dateTimeFormateado);
                Directory.CreateDirectory(temporalRutaBackup);
                lastRutaBackup = temporalRutaBackup;
                // Copia todos los archivos que se vayan a sincronizar
                foreach (DirectoryInfo dirDestino in directoriesDestino)
                {
                    // Crea la carpeta del destino
                    string newDirDestino = Path.Combine(temporalRutaBackup, dirDestino.Name);
                    Directory.CreateDirectory(newDirDestino);

                    ArchivosTreeView archivosACopiar = Archivos.GetArchivosModificadosTreeView(dirOrigen, dirDestino, actualProyecto);
                    CopiarArchivos(archivosACopiar, newDirDestino, dirOrigen.FullName);
                }

                LocalUtilities.WriteTextLog(StringResource.mensajeBackup + temporalRutaBackup + StringResource.mensajeFecha + DateTime.Now.ToString(),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }

        /// <summary>
        /// Conserva las 3 carpetas mas nuevas de la ruta especificada y borra el resto
        /// </summary>
        private void ComprobarBackUpViejos(string rutaBackup)
        {
            DirectoryInfo dirBackUp = new DirectoryInfo(rutaBackup);
            var directoriesBackUp = dirBackUp.GetDirectories();
            if (directoriesBackUp.Count() > Int32.Parse(StringResource.numberLastBackUp))
            {
                directoriesBackUp.OrderByDescending(x => x.LastWriteTimeUtc).ToList();
                for (int i = 1; i < directoriesBackUp.Length + 1; i++)
                {
                    if (i > Int32.Parse(StringResource.numberLastBackUp))
                    {
                        // borra recursivamente el directorio
                        directoriesBackUp[i - 1].Delete(true);
                    }
                }                
            }
        }

        /// <summary>
        /// Copia los archivos de origen en la ruta indicada, crea las subcarpetas si no existen
        /// </summary>
        private void CopiarArchivos(ArchivosTreeView archivosOrigen, string rutaDestino, string rutaOrigen, bool root = true)
        {
            try
            {
                if (HayFiltros) 
                {
                    Arbol.Filtros = filtros;
                    archivosOrigen.Archivos = Arbol.FiltrarStringArchivos(archivosOrigen.Archivos); 
                }

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
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
        
        #endregion

        #region· ARBOL

        private void PopulateArbolOrigen()
        {
            treeViewOrigen = Arbol.PopulateArchivoTreeView(archivosOrigenArbol, null, HayFiltros, treeViewOrigen);
            CountArchivosOrigen = archivosOrigenArbol.GetTotalArchivos();
        }

        private void PopulateArbolDestino()
        {
            treeviewDestino = Arbol.PopulateArchivoTreeView(archivosDestinoArbol, null, HayFiltros, treeviewDestino);
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
                            Arbol.Filtros = filtros;

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
                                treeviewDestino = Arbol.PopulateArchivoTreeView(archivosDestinoArbol, null, HayFiltros, treeviewDestino);
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
                            LocalUtilities.MensajeInfo(StringResource.accessDenied, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                        }
                    }
                    else
                    {
                        if (origen)
                        {
                            LocalUtilities.MensajeInfo(StringResource.comprobarOrigen, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                        }
                        else
                        {
                            LocalUtilities.MensajeInfo(StringResource.comprobarDestino, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                            destinoIntroducido = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }

            return contador;
        }

        private void ActualizarTreeOrigen()
        {
            treeViewOrigen.Nodes.Clear();
            if (!string.IsNullOrEmpty(textOrigen.Text))
            {
                ActualizarTreeView(textOrigen, treeViewOrigen, true);
            }
            else
            {
                CountArchivosOrigen = 0;
            }
        }

        #endregion

        #region· PROYECTO              

        private void GuardarProyecto(bool borrando = false)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbProyecto.Text))
                {
                    cmbProyecto.Text = StringResource.nuevoProyecto + "_" + dateTimeFormateado;
                }

                if ((ComprobarNombreProyecto() || !addProyecto) || borrando)
                {
                    ActualizarDestino(true);                   

                    if (actualProyecto == null)
                    {
                        Guid guid = Guid.NewGuid();
                        actualProyecto = new Proyecto(guid);
                    }

                    actualProyecto.ProyectoName = cmbProyecto.Text;
                    actualProyecto.PathOrigen = textOrigen.Text;
                    actualProyecto.PathDestino = RutasDestinos;
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
                        LocalUtilities.WriteTextLog(StringResource.guardadoXML + DateTime.Now.ToString(),
                            actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
                    }
                }
                else
                {
                    LocalUtilities.MensajeInfo(StringResource.mismoNombre, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                }

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
                LocalUtilities.MensajeError(StringResource.xmlCorrupto + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
                    RutasDestinos = new List<string>();

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
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }

        private void ActualizarProyecto()
        {

            if (actualProyecto != null)
            {
                actualProyecto.Filtrar = HayFiltros;
                actualProyecto.ProyectoName = cmbProyecto.Text;
                actualProyecto.PathOrigen = textOrigen.Text;
                actualProyecto.HacerBackup = HacerBackup;
                actualProyecto.PathBackup = rutaBackup;
            }

            ActualizarTreeOrigen();

            LimpiarLayoutDestino();
            foreach (string destino in actualProyecto.PathDestino)
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

            RutasDestinos.Clear();
            treeViewOrigen.Nodes.Clear();

            LimpiarLayoutDestino();
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
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
                    LocalUtilities.WriteTextLog(StringResource.proyecto + proyecto.ProyectoName + StringResource.borrado + DateTime.Now.ToString(),
                        actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
                }

                cmbProyecto.DataSource = proyectos;
                actualProyecto = (Proyecto)cmbProyecto.SelectedItem;
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
            foreach (string destino in RutasDestinos)
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
            if (treeViewOrigen.Nodes.Count != 0 && RutasDestinos.Count() != 0)
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
            if (!string.IsNullOrEmpty(actualProyecto?.LastPathBackup))
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

        #region· CONTROL DESTINO

        /// <summary>
        /// Añade un control en el tableLayout de Destino
        /// </summary>
        /// <param name="addDestino">Indica si se añade la ruta a la lista de destinos</param>
        private void AddDestinoControl(string rutaDestino, bool addDestino = true)
        {
            cDestino destinoControl = new cDestino(RutasDestinos);
            treeviewDestino = destinoControl.TreeViewDestino;
            destinoControl.LabelContador = ActualizarTreeView(textDestino, treeviewDestino, false, rutaDestino);
            if (destinoIntroducido)
            {
                destinoControl.TreeViewDestino = treeviewDestino;
                destinoControl.RutaDestino = rutaDestino;

                tableLayoutDestino.Controls.Add(destinoControl);
                if (addDestino) RutasDestinos.Add(rutaDestino);
            }
        }

        public void ActualizarCountDestino()
        {
            lblCountDestinos.Text = RutasDestinos.Count().ToString();
        }

        private void LimpiarLayoutDestino()
        {
            tableLayoutDestino.Controls.Clear();
        }

        /// <summary>
        /// Actualiza la lista de los destinos
        /// </summary>
        /// <param name="incluirTodos">Si se desea incluir todos los destinos aunque no este seleccionados</param>
        /// <param name="desdeControl">Si se llama desde los eventos del tableLayout</param>
        public void ActualizarDestino(bool incluirTodos, bool desdeControl = false)
        {
            RutasDestinos = new List<string>();
            
            foreach (cDestino destino in tableLayoutDestino.Controls)
            {
                if(destino.CheckDestino || incluirTodos) RutasDestinos.Add(destino.RutaDestino);
            }

            if (!string.IsNullOrEmpty(textDestino.Text) && ComprobarDestino() && !desdeControl)
            {
                RutasDestinos.Add(textDestino.Text);
            }

            ActualizarCountDestino();
        }

        private Size ResizeTlpControl()
        {
            int height = tableLayoutDestino.Size.Height;
            int width = tableLayoutDestino.Size.Width;

            if (tableLayoutDestino.Controls.Count != 0)
            {
                height = height / tableLayoutDestino.Controls.Count;
                width = width / tableLayoutDestino.Controls.Count;
            }

            Size size = new Size(width, height);

            return size;
        }

        #endregion

        private void SincronizarCarpetas()
        {
            try
            {
                ActualizarDestino(false);
                DirectoryInfo dirOrigen = new DirectoryInfo(textOrigen.Text);
                List<DirectoryInfo> directoriesDestino = Archivos.GetAllDestinos(RutasDestinos, actualProyecto);
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
                        ArchivosTreeView archivosModificados = Archivos.GetArchivosModificadosTreeView(dirOrigen, dirDestino, actualProyecto);
                        CopiarArchivos(archivosModificados, dirDestino.FullName, dirOrigen.FullName);
                    }

                    GuardarProyecto();
                    LocalUtilities.WriteTextLog(StringResource.sincronizarCarpetas + DateTime.Now.ToString(),
                        actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
                }
                else
                {
                    LocalUtilities.MensajeInfo(StringResource.comprobarOrigen, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                }

            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }

        private void RestaurarBackup()
        {
            try
            {
                if (!string.IsNullOrEmpty(lastRutaBackup))
                {
                    DialogResult dialogResult = MessageBox.Show(StringResource.mensajeRestaurarBackup, StringResource.restaurar, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ActualizarDestino(false);
                        DirectoryInfo dirOrigen = new DirectoryInfo(lastRutaBackup);
                        List<DirectoryInfo> directoriesDestino = Archivos.GetAllDestinos(RutasDestinos, actualProyecto);
                        if (directoriesDestino != null && directoriesDestino.Count() != 0)
                        {
                            foreach (DirectoryInfo dirDestino in directoriesDestino)
                            {
                                if (dirOrigen.Exists)
                                {
                                    string dirOrigenProyecto = Path.Combine(dirOrigen.FullName, dirDestino.Name);
                                    DirectoryInfo proyectoDirDestino = new DirectoryInfo(dirOrigenProyecto);

                                    CopiarArchivos(GetArchivosTreeView(proyectoDirDestino), dirDestino.FullName, dirOrigenProyecto);
                                }
                                else
                                {
                                    LocalUtilities.MensajeInfo(StringResource.noLastBackup, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                                }
                            }

                            GuardarProyecto();
                            LocalUtilities.WriteTextLog(StringResource.restaurarBackup + DateTime.Now.ToString(),
                                actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
                        }
                    }
                }
                else
                {
                    LocalUtilities.MensajeInfo(StringResource.noLastBackup, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }

        #endregion

        #region· - EVENTOS

        #region· RUTAS
        private void btnRutaOrigen_Click(object sender, EventArgs e)
        {
            modificandoProyecto = true;
            ElegirRuta(textOrigen);
            ActualizarTreeOrigen();
            modificandoProyecto = false;
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
                LocalUtilities.MensajeInfo(StringResource.noDestino, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }

        #endregion

        #region· TOOLTIP HOVER

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

        #region· CHECKS

        private void checkBoxBackup_CheckedChanged(object sender, EventArgs e)
        {
            VisibilidadBackup();
        }

        private void chkBoxFiltros_CheckedChanged(object sender, EventArgs e)
        {
            VisibilidadFiltro();
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

        #region· CHANGED

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
                LocalUtilities.MensajeInfo(StringResource.alertaGuardarProyecto, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }

        private void textOrigen_TextChanged(object sender, EventArgs e)
        {
            VisibilidadActualizar();
        }

        private void tableLayoutDestino_ControlRemoved(object sender, ControlEventArgs e)
        {
            ActualizarDestino(true, true);
            VisibilidadesTodas();
            ResizeTlpControl();
        }

        private void tableLayoutDestino_ControlAdded(object sender, ControlEventArgs e)
        {
            ActualizarDestino(true, true);
            ResizeTlpControl();
        }

        #endregion

        #region· CLICKS

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

        private void btnAddFiltros_Click(object sender, EventArgs e)
        {
            FormFiltros formFiltros;
            if (filtros?.Count() > 0)
            {
                formFiltros = new FormFiltros(textOrigen.Text, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, filtros);
            }
            else
            {
                formFiltros = new FormFiltros(textOrigen.Text, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }

            formFiltros.ShowDialog();
            if (formFiltros.DialogResult == DialogResult.OK)
            {
                filtros = formFiltros.FiltrosADevolver;
                ActualizarDatos();
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
                LocalUtilities.MensajeInfo(StringResource.alertaGuardarProyecto, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (actualProyecto != null || !string.IsNullOrEmpty(textOrigen.Text)) ActualizarProyecto();
        }

        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            RestaurarBackup();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProyecto.Text)) cmbProyecto.Text = txtProyecto.Text;
            GuardarProyecto();

            RecargarProyecto();
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

        private void btnPrevisualizar_Click(object sender, EventArgs e)
        {
            Arbol.Filtros = filtros;
            ActualizarDestino(false, true);

            frmPrevisualizar formPrevisualizar;
            formPrevisualizar = new frmPrevisualizar(archivosOrigenArbol,textOrigen.Text, RutasDestinos, HayFiltros, actualProyecto, sobreescribir);
            formPrevisualizar.ShowDialog();
        }


        #endregion

        #endregion

    }
}