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
    /// TODO: Cambiar nomenclatura, en el codigo se llama Proyecto a lo que en la interfaz y de cara al usuario se llama Configuracion

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
        private string temporalRutaBackup;
        private List<string> _rutasDestinos;
        public List<string> RutasDestinos
        {
            get { return _rutasDestinos; }
            set
            {
                if (value != _rutasDestinos)
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
        private frmPrevisualizar formPrevisualizar;

        #region· BOOLEANOS 
        private bool hayFiltros = false;
        private bool hacerBackup = false;
        private bool sobreescribir = false;
        private bool borrarArchivosDestino = false;
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
            ActualizarCountDestino();

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
                if (actualProyecto != null && !addProyecto) cmbProyecto.SelectedItem = actualProyecto;
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

        private void GetArchivosOrigenArbol()
        {
            archivosOrigenArbol = ArchivosUtilities.GetArchivosTreeView(dirArbolOrigen, actualProyecto);
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
                ArchivosUtilities.ComprobarBackUpViejos(carpetaRutaBackup);
                // Crea la carpeta temporal
                temporalRutaBackup = Path.Combine(carpetaRutaBackup, dateTimeFormateado);
                Directory.CreateDirectory(temporalRutaBackup);
                lastRutaBackup = temporalRutaBackup;
                // Copia todos los archivos que se vayan a sincronizar
                foreach (DirectoryInfo dirDestino in directoriesDestino)
                {
                    // Crea la carpeta del destino
                    string newDirDestino = Path.Combine(temporalRutaBackup, dirDestino.Name);
                    Directory.CreateDirectory(newDirDestino);

                    // archivos a copiar, coge todo el origen si se quiere sobreescribir
                    ArchivosTreeView archivosACopiar;
                    if (sobreescribir) archivosACopiar = ArchivosUtilities.GetArchivosTreeView(dirOrigen, actualProyecto);
                    else archivosACopiar = ArchivosUtilities.GetArchivosModificadosTreeView(dirOrigen, dirDestino, actualProyecto);

                    CopiarArchivos(archivosACopiar, newDirDestino, dirOrigen.FullName);
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
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
                    ArchivosUtilities.Filtros = filtros;
                    archivosOrigen.Archivos.FiltrarStringArchivos();
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
                    File.Copy(Path.Combine(rutaOrigen, archivo), Path.Combine(directoryRoot, archivo), true);
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

        #endregion

        #region· ARBOL

        private void PopulateArbolOrigen()
        {
            treeViewOrigen.Nodes.Clear();
            treeViewOrigen = ArbolUtilities.PopulateArchivoTreeView(archivosOrigenArbol, null, HayFiltros, treeViewOrigen);
            CountArchivosOrigen = archivosOrigenArbol.GetTotalArchivos();
        }

        private void PopulateArbolDestino()
        {
            treeviewDestino = ArbolUtilities.PopulateArchivoTreeView(archivosDestinoArbol, null, HayFiltros, treeviewDestino);
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

                        if (LocalUtilities.ComprobarAcceso(dirInfo))
                        {
                            // Limpiar el treeView
                            treeView.Nodes.Clear();

                            // Poblar el TreeView
                            ArchivosUtilities.Filtros = filtros;

                            MyProcessControl backWork = new MyProcessControl(this);
                            backWork = LocalUtilities.ConfigurarProcessControl(backWork);

                            if (origen)
                            {
                                backWork.Proceso(GetArchivosOrigenArbol, PopulateArbolOrigen);
                                backWork.Visible = false;
                            }
                            else
                            {
                                archivosDestinoArbol = ArchivosUtilities.GetArchivosTreeView(dirArbolOrigen, actualProyecto);
                                treeviewDestino = ArbolUtilities.PopulateArchivoTreeView(archivosDestinoArbol, null, HayFiltros, treeviewDestino);
                                //backWork.Proceso(GetArchivoDestinoArbol, PopulateArbolDestino);
                                backWork.Visible = false;
                                destinoIntroducido = true;
                            }

                            var contadorTodos = dirInfo.GetFiles("*", SearchOption.AllDirectories);
                            if (HayFiltros)
                            {
                                ArchivosUtilities.Filtros = filtros;
                                contadorTodos = ArchivosUtilities.FiltrarFileArchivos(contadorTodos);
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
                if ((ComprobarNombreProyecto() || !addProyecto) || borrando)
                {
                    ActualizarDestino(true);

                    if (actualProyecto == null)
                    {
                        Guid guid = Guid.NewGuid();
                        actualProyecto = new Proyecto(guid);
                    }

                    if (!borrando)
                    {
                        actualProyecto.ProyectoName = cmbProyecto.Text;
                        actualProyecto.PathOrigen = textOrigen.Text;

                        // Si solo hay una ruta de destino se añade
                        if (RutasDestinos.Count() != 0) actualProyecto.PathDestino = RutasDestinos;
                        else if (!string.IsNullOrEmpty(textDestino.Text)) actualProyecto.PathDestino.Add(textDestino.Text);

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
                        textDestino.Text = actualProyecto?.PathDestino?.FirstOrDefault();
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
                    HacerBackup = (bool)(actualProyecto?.HacerBackup);
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
                ActualizarDestino(true);

                actualProyecto.Filtrar = HayFiltros;
                actualProyecto.ProyectoName = cmbProyecto.Text;
                actualProyecto.PathOrigen = textOrigen.Text;

                // Si solo hay una ruta de destino se añade
                if (RutasDestinos.Count() != 0) actualProyecto.PathDestino = RutasDestinos;
                else if (!string.IsNullOrEmpty(textDestino.Text)) actualProyecto.PathDestino.Add(textDestino.Text);

                actualProyecto.HacerBackup = HacerBackup;
                actualProyecto.PathBackup = rutaBackup;
            }

            ActualizarTreeOrigen();

            LimpiarLayoutDestino();
            if (actualProyecto != null)
            {
                foreach (string destino in actualProyecto?.PathDestino)
                {
                    AddDestinoControl(destino, false);
                }
            }
            else
            {
                ActualizarDestino(true);
                foreach (string destino in RutasDestinos)
                {
                    AddDestinoControl(destino, false);
                }
            }
        }

        private void NuevoProyecto()
        {
            cmbProyecto.Text = String.Empty;
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

        private void ActualizarComboProyecto()
        {
            cmbProyecto.DataSource = null;
            cmbProyecto.DataSource = proyectos;
            cmbProyecto.DisplayMember = nameof(Proyecto.ProyectoName);
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
            if (actualProyecto != null && proyectos.Count() != 0)
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

        private void VisibilidadAddDestino()
        {
            if (!string.IsNullOrEmpty(textDestino.Text))
            {
                btnVerCarpetaDestino.Visible = true;
            }
            else
            {
                btnVerCarpetaDestino.Visible = false;
            }
        }

        private void VisibilidadManipularArchivos()
        {
            if (treeViewOrigen.Nodes.Count != 0 && RutasDestinos.Count() != 0)
            {
                btnActualizar.Visible = true;
                btnSincronizar.Visible = true;
                btnPrevisualizar.Visible = true;
                chkBoxSobreescribir.Visible = true;
                chkBorrarDestino.Visible = true;
                chkCopiarArchivos.Visible = true;
                addDocumentImage.Visible = true;
            }
            else
            {
                btnActualizar.Visible = false;
                btnSincronizar.Visible = false;
                btnPrevisualizar.Visible = false;
                chkBoxSobreescribir.Visible = false;
                chkBorrarDestino.Visible = false;
                chkCopiarArchivos.Visible = false;
                addDocumentImage.Visible = false;
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
                if (destino.CheckDestino || incluirTodos) RutasDestinos.Add(destino.RutaDestino);
            }

            if (!string.IsNullOrEmpty(textDestino.Text) && ComprobarDestino() && !desdeControl)
            {
                RutasDestinos.Add(textDestino.Text);
            }

            ActualizarCountDestino();
        }

        #endregion

        private void SincronizarCarpetas()
        {
            try
            {
                ActualizarDestino(false);

                DirectoryInfo dirOrigen = new DirectoryInfo(textOrigen.Text);
                List<DirectoryInfo> directoriesDestino = ArchivosUtilities.GetAllDestinos(RutasDestinos, actualProyecto);
                if (directoriesDestino == null || directoriesDestino.Count() == 0)
                {
                    return;
                }

                if (dirOrigen.Exists)
                {
                    ArchivosTreeView archivosOrigen = ArchivosUtilities.GetArchivosTreeView(dirOrigen, actualProyecto);

                    if (HacerBackup) CrerBackup(directoriesDestino, dirOrigen);

                    if (borrarArchivosDestino) ArchivosUtilities.BorrarArchivos(directoriesDestino);

                    if (sobreescribir)
                    {
                        foreach (DirectoryInfo dirDestino in directoriesDestino)
                        {
                            ArchivosTreeView archivos = ArchivosUtilities.GetArchivosTreeView(dirOrigen, actualProyecto);
                            CopiarArchivos(archivos, dirDestino.FullName, dirOrigen.FullName);
                        }
                    }
                    else
                    {
                        foreach (DirectoryInfo dirDestino in directoriesDestino)
                        {
                            ArchivosTreeView archivosModificados = ArchivosUtilities.GetArchivosModificadosTreeView(dirOrigen, dirDestino, actualProyecto);
                            CopiarArchivos(archivosModificados, dirDestino.FullName, dirOrigen.FullName);
                        }
                    }
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

        private void DespuesDeSincro()
        {
            GuardarProyecto();
            ActualizarProyecto();
            if (HacerBackup) LocalUtilities.WriteTextLog(StringResource.mensajeBackup + temporalRutaBackup + StringResource.mensajeFecha + DateTime.Now.ToString(),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
            LocalUtilities.WriteTextLog(StringResource.sincronizarCarpetas + DateTime.Now.ToString(),
                actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
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
                        List<DirectoryInfo> directoriesDestino = ArchivosUtilities.GetAllDestinos(RutasDestinos, actualProyecto);
                        if (directoriesDestino != null && directoriesDestino.Count() != 0)
                        {
                            foreach (DirectoryInfo dirDestino in directoriesDestino)
                            {
                                if (dirOrigen.Exists)
                                {
                                    string dirOrigenProyecto = Path.Combine(dirOrigen.FullName, dirDestino.Name);
                                    DirectoryInfo proyectoDirDestino = new DirectoryInfo(dirOrigenProyecto);

                                    CopiarArchivos(ArchivosUtilities.GetArchivosTreeView(proyectoDirDestino, actualProyecto), dirDestino.FullName, dirOrigenProyecto);
                                }
                                else
                                {
                                    LocalUtilities.MensajeInfo(StringResource.noLastBackup, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                                }
                            }
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

        private void DespuesdeBackup()
        {
            GuardarProyecto();
            LocalUtilities.WriteTextLog(StringResource.restaurarBackup + DateTime.Now.ToString(),
                actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto, lblLog);
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
                chkCopiarArchivos.Checked = false;
                addDocumentImage.Visible = false;
                chkBorrarDestino.Checked = false;
                fatalWarningImage.Visible = false;
            }
            else
            {
                sobreescribir = false;
                warningImage.Visible = false;
                chkCopiarArchivos.Checked = true;
                addDocumentImage.Visible = true;
            }
        }

        private void chkBorrarDestino_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBorrarDestino.Checked)
            {
                borrarArchivosDestino = true;
                fatalWarningImage.Visible = true;
                chkBoxSobreescribir.Checked = false;
                warningImage.Visible = false;
                chkCopiarArchivos.Checked = false;
                addDocumentImage.Visible = false;
            }
            else
            {
                borrarArchivosDestino = false;
                fatalWarningImage.Visible = false;
                if (!chkBoxSobreescribir.Checked)
                {
                    chkCopiarArchivos.Checked = true;
                    addDocumentImage.Visible = true;
                }                
            }
        }

        #endregion

        #region· CHANGED

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

        private void textDestino_TextChanged(object sender, EventArgs e)
        {
            VisibilidadAddDestino();
        }

        private void tableLayoutDestino_ControlRemoved(object sender, ControlEventArgs e)
        {
            ActualizarDestino(true, true);
            VisibilidadesTodas();

            foreach (Control control in tableLayoutDestino.Controls)
            {
                control.Size = LocalUtilities.ResizeTlpControl(tableLayoutDestino);
            }
        }

        private void tableLayoutDestino_ControlAdded(object sender, ControlEventArgs e)
        {
            ActualizarDestino(true, true);
            VisibilidadesTodas();

            foreach (Control control in tableLayoutDestino.Controls)
            {
                control.Size = LocalUtilities.ResizeTlpControl(tableLayoutDestino);
            }
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
            MyProcessControl backWork = new MyProcessControl(this);
            backWork = LocalUtilities.ConfigurarProcessControl(backWork);

            backWork.Proceso(SincronizarCarpetas, DespuesDeSincro);
            backWork.Visible = false;
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
            filtros = formFiltros.FiltrosADevolver;
            if (formFiltros.DialogResult == DialogResult.OK)
            {
                ActualizarDatos();
            }
        }

        private void btnAddProyecto_Click(object sender, EventArgs e)
        {
            if (actualProyecto != null && !string.IsNullOrEmpty(actualProyecto.ProyectoName) && !addProyecto)
            {
                Guid guid = Guid.NewGuid();
                actualProyecto = new Proyecto(guid);
                addProyecto = true;
                modificandoProyecto = true;

                proyectos.Add(actualProyecto);

                btnAddProyecto.Image = Properties.Resources.delete;

                RecargarProyecto();
                modificandoProyecto = false;
                cmbProyecto.Enabled = false;
                cmbProyecto.Visible = false;
                txtProyecto.Visible = true;
            }
            else if (addProyecto)
            {
                CancelarProyecto();
                btnAddProyecto.Image = Properties.Resources.add;

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
            MyProcessControl backWork = new MyProcessControl(this);
            backWork = LocalUtilities.ConfigurarProcessControl(backWork);

            backWork.Proceso(RestaurarBackup, DespuesdeBackup);
            backWork.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProyecto.Text)) cmbProyecto.Text = txtProyecto.Text;
            else if (string.IsNullOrEmpty(cmbProyecto.Text)) cmbProyecto.Text = StringResource.nuevoProyecto + "_" + dateTimeFormateado;

            GuardarProyecto();

            //RecargarProyecto();
            ActualizarComboProyecto();
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
                RecargarProyecto();

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
            ArchivosUtilities.Filtros = filtros;
            ActualizarDestino(false, true);

            MyProcessControl backWork = new MyProcessControl(this);
            backWork = LocalUtilities.ConfigurarProcessControl(backWork);

            backWork.Proceso(CargarPrevisualizar, MostrarPrevisualizar);
            backWork.Visible = false;
        }

        private void CargarPrevisualizar()
        {
            formPrevisualizar = new frmPrevisualizar(archivosOrigenArbol, textOrigen.Text, RutasDestinos, HayFiltros, actualProyecto, sobreescribir || borrarArchivosDestino);
        }

        private void MostrarPrevisualizar()
        {
            formPrevisualizar.ShowDialog();
        }

        #endregion

        #endregion
               
    }
}