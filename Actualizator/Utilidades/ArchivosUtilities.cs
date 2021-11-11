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
        private static BindingList<Filtro> filtros;
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

                    foreach (FileInfo archivo in archivos.FiltrarFileArchivos())
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
                    foreach (FileInfo archivo in archivos.FiltrarFileArchivos())
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
        /// <returns></returns>
        public static ArchivosTreeView GetArchivosTreeView(DirectoryInfo dirInfo, Proyecto actualProyecto)
        {
            ArchivosTreeView archivosTree = new ArchivosTreeView();
            // Rellena a nivel raiz            
            try
            {
                archivosTree.DirName = dirInfo.Name;
                FileInfo[] archivos = dirInfo.GetFiles();

                var test = dirInfo.GetAccessControl();

                foreach (FileInfo archivo in archivos.FiltrarFileArchivos())
                {
                    archivosTree.Archivos.Add(archivo.Name);
                }

                // Rellena las subcarpetas
                foreach (DirectoryInfo directory in dirInfo.GetDirectories())
                {
                    archivosTree.Subdir.Add(GetArchivosTreeView(directory, actualProyecto));
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
        /// Filtra una lista de string
        /// </summary>
        /// <param name="archivos">lista a devolver</param>
        /// <returns></returns>
        public static List<string> FiltrarStringArchivos(this List<string> archivos)
        {
            foreach (Filtro filtro in Filtros)
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

        /// <summary>
        /// Filtra un array de FileInfo
        /// </summary>
        /// <param name="archivos"></param>
        /// <returns></returns>
        public static FileInfo[] FiltrarFileArchivos(this FileInfo[] archivos)
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
