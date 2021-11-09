using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Actualizator.Clases
{
    public class ArchivosUtilities
    {

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

                foreach (FileInfo archivo in archivos)
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
    }
}
