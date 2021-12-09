using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Actualizator.Clases
{
    public static class ArchivosUtilities
    {
        private static BindingList<Filtro> filtros = new BindingList<Filtro>();
        public static BindingList<Filtro> Filtros
        {
            get { return filtros; }
            set
            {
                if (value != filtros)
                {
                    filtros = value;
                }
            }
        }
        private static BindingList<Filtro> filtrosIncluyentes = new BindingList<Filtro>();
        public static BindingList<Filtro> FiltrosIncluyentes
        {
            get { return filtrosIncluyentes; }
            set
            {
                if (value != filtrosIncluyentes)
                {
                    filtrosIncluyentes = value;
                }
            }
        }

        /// <summary>
        /// Devuelve archivos modificados O NUEVOS dadas dos rutas, busca dentro de todas las subcarpetas
        /// </summary>
        public static ArchivosTreeView GetArchivosModificadosTreeView(DirectoryInfo dirOrigen, DirectoryInfo dirDestino, Proyecto actualProyecto)
        {
            ArchivosTreeView archivosTree = new ArchivosTreeView();

            try
            {
                archivosTree.DirName = dirOrigen.Name;
                FileInfo[] archivos = dirOrigen.GetFiles();
                
                // Si no hay dirDestino siginifica que la estrucutra de carpetas es distinta en el destino, por lo tanto copia todo
                if (dirDestino != null)
                {
                    // Rellena a nivel raiz
                    DirectoryInfo[] directories = dirDestino.GetDirectories();

                    if (Filtros.Count != 0) archivos.FiltrarFileArchivos();
                    if (FiltrosIncluyentes.Count() != 0) archivos = archivos.FiltrarFileArchivosIncluyente();

                    foreach (FileInfo archivo in archivos)
                    {
                        //if (directories != null && (directories.Any(x => x.Name == dirOrigen.Name) || dirOrigen.Name.Equals(dirDestino.Name)))
                        
                        FileInfo[] nuevosArchivosDestino = dirDestino.GetFiles();
                        FileInfo archivoDestino = nuevosArchivosDestino.Where(x => x.Name == archivo.Name).FirstOrDefault();
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
                    foreach (DirectoryInfo directoryOrigen in dirOrigen.GetDirectories())
                    {
                        if (directories.Any(x => x.Name == directoryOrigen.Name))
                        {
                            DirectoryInfo directoryDestino = directories.Where(x => x.Name == directoryOrigen.Name).Single();
                            archivosTree.Subdir.Add(GetArchivosModificadosTreeView(directoryOrigen, directoryDestino, actualProyecto));
                        }
                        else
                        {
                            archivosTree.Subdir.Add(GetArchivosModificadosTreeView(directoryOrigen, null, actualProyecto));
                        }
                    } 
                }
                else
                {
                    if (Filtros.Count != 0) archivos = archivos.FiltrarFileArchivos();
                    if (FiltrosIncluyentes.Count() != 0) archivos = archivos.FiltrarFileArchivosIncluyente();

                    foreach (FileInfo archivo in archivos)
                    {       
                        archivosTree.Archivos.Add(archivo.Name);
                    }

                    // Rellena las subcarpetas
                    foreach (DirectoryInfo directoryOrigen in dirOrigen.GetDirectories())
                    {
                        archivosTree.Subdir.Add(GetArchivosModificadosTreeView(directoryOrigen, null, actualProyecto));
                    } 
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }

            return archivosTree;
        }

        /// <summary>
        /// Dado un DirectoryInfo crea un objecto con la estructura de los directorios y subdiresctorios con sus respectivos archivos  
        /// </summary>
        /// <param name="hayFiltrosIncluyentes">indica si hay filtro incluyente</param>
        public static ArchivosTreeView GetArchivosTreeView(DirectoryInfo dirInfo, Proyecto actualProyecto, bool hayFiltrosIncluyentes)
        {
            ArchivosTreeView archivosTree = new ArchivosTreeView();
            // Rellena a nivel raiz            
            try
            {
                archivosTree.DirName = dirInfo.Name;
                FileInfo[] archivos = dirInfo.GetFiles();

                if (hayFiltrosIncluyentes && FiltrosIncluyentes.Count()!=0) archivos = archivos.FiltrarFileArchivosIncluyente();

                var test = dirInfo.GetAccessControl();

                foreach (FileInfo archivo in archivos.FiltrarFileArchivos())
                {
                    archivosTree.Archivos.Add(archivo.Name);
                }

                // Rellena las subcarpetas
                foreach (DirectoryInfo directory in dirInfo.GetDirectories())
                {
                    archivosTree.Subdir.Add(GetArchivosTreeView(directory, actualProyecto, hayFiltrosIncluyentes));
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }

            return archivosTree;
        }

        /// <summary>
        /// Filtra un array de FileInfo
        /// </summary>
        /// <param name="archivos">archivos a devolver</param>
        public static FileInfo[] FiltrarFileArchivos(this FileInfo[] archivos)
        {
            foreach (Filtro filtroObjecto in Filtros)
            {
                switch (filtroObjecto.cabecera)
                {
                    case Filtrado.TerminaPor:
                        foreach (var filtro in filtroObjecto.Filtros)
                        {
                            archivos = archivos.Where(x => !x.Name.ToLower().EndsWith(filtro.ToLower())).ToArray();
                        }                        
                        break;
                    case Filtrado.Completo:
                        foreach (var filtro in filtroObjecto.Filtros)
                        {
                            archivos = archivos.Where(x => !x.Name.ToLower().Equals(filtro.ToLower())).ToArray();
                        }                            
                        break;
                    case Filtrado.Ruta:
                        foreach (var filtro in filtroObjecto.Filtros)
                        {
                            archivos = archivos.Where(x => !x.FullName.ToLower().Equals(filtro.ToLower())).ToArray();
                        }
                        break;
                }
            }
            return archivos;
        }

        /// <summary>
        /// Filtra un array de FileInfo de forma incluyente
        /// </summary>
        public static FileInfo[] FiltrarFileArchivosIncluyente(this FileInfo[] archivos)
        {
            List<FileInfo> archivosaDevolver = new List<FileInfo>();
            foreach (Filtro filtroObjecto in FiltrosIncluyentes)
            {
                switch (filtroObjecto.cabecera)
                {
                    case Filtrado.TerminaPor:
                        foreach (var filtro in filtroObjecto.Filtros)
                        {
                            var archivosFiltradosTermina = archivos.Where(x => x.Name.ToLower().EndsWith(filtro.ToLower())).ToList();
                            foreach (var archivo in archivosFiltradosTermina)
                            {
                                archivosaDevolver.Add(archivo);
                            } 
                        }                        
                        break;
                    case Filtrado.Completo:
                        foreach (var filtro in filtroObjecto.Filtros)
                        {
                            var archivosFiltradosCompleto = archivos.Where(x => x.Name.ToLower().Equals(filtro.ToLower())).ToArray();
                            foreach (var archivo in archivosFiltradosCompleto)
                            {
                                archivosaDevolver.Add(archivo);
                            } 
                        }
                        break;
                    case Filtrado.Ruta:
                        foreach (var filtro in filtroObjecto.Filtros)
                        {
                            var archivosFiltradoRuta = archivos.Where(x => x.FullName.ToLower().Equals(filtro.ToLower())).ToArray();
                            foreach (var archivo in archivosFiltradoRuta)
                            {
                                archivosaDevolver.Add(archivo);
                            }
                        }
                        break;
                }
            }
            return archivosaDevolver.ToArray();
        }

        /// <summary>
        /// Devuelve una lista de DirectoryInfo dada una lista de string con una ruta de carpeta
        /// </summary>
        public static List<DirectoryInfo> GetAllDestinos(List<string> RutasDestino, Proyecto actualProyecto)
        {
            List<DirectoryInfo> allDestinos = new List<DirectoryInfo>();

            foreach (var destino in RutasDestino)
            {
                DirectoryInfo dirDestino = new DirectoryInfo(destino);
                if (dirDestino.Exists) allDestinos.Add(dirDestino);
                else
                {
                    LocalUtilities.MensajeInfo(StringResource.comprobarDestino, actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
                    return null;
                }
            }

            return allDestinos;
        }

        /// <summary>
        /// Borra todo el contenido de las carpetas, incluidas subcarpetas
        /// </summary>
        /// <param name="directoriesDestino">Lista de carpetas</param>
        public static void BorrarArchivos(List<DirectoryInfo> directoriesDestino)
        {
            foreach (DirectoryInfo directory in directoriesDestino)
            {
                foreach(DirectoryInfo subdirectories in directory.GetDirectories())
                {
                    subdirectories.Delete(true);
                }
                foreach(var file in directory.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        /// <summary>
        /// Conserva las 3 carpetas mas nuevas de la ruta especificada y borra el resto
        /// </summary>
        public static void ComprobarBackUpViejos(string rutaBackup)
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
        /// Crea un archivo ZIP de los archivos proporcionados
        /// </summary>
        /// <param name="fileName">Ruta completa del archivo ZIP</param>
        /// <param name="files">Archivos a añadir</param>
        public static void CreateZipFile(string fileName, ArchivosTreeView files, string rutaDestino, Proyecto actualProyecto, ZipArchive originalZip = null)
        {
            try
            {
                // Create and open a new ZIP file
                if (files.GetTotalArchivos() != 0)
                {
                    ZipArchive zip;

                    if (originalZip != null) zip = originalZip;
                    else zip = ZipFile.Open(fileName, ZipArchiveMode.Create);
                    
                    foreach (var file in files.Archivos)
                    {
                        // Add the entry for each file
                        string rutaConCarpeta = Path.Combine(rutaDestino + "\\" + file);
                        string fileConCarpeta = Path.Combine(files.DirName + "\\" + file);

                        zip.CreateEntryFromFile(rutaConCarpeta, fileConCarpeta, CompressionLevel.Optimal);
                    }
                    foreach (var dir in files.Subdir)
                    {
                        CreateZipFile(fileName, dir, Path.Combine(rutaDestino + "\\" + dir.DirName), actualProyecto, zip);
                    }
                    // Dispose of the object when we are done
                    if (originalZip == null) zip.Dispose();
                }
            }
            catch (Exception ex)
            {
                LocalUtilities.MensajeError(StringResource.mensajeError + LocalUtilities.getErrorException(ex),
                    actualProyecto != null ? actualProyecto.ProyectoName : StringResource.nuevoProyecto);
            }
        }
    }
}
