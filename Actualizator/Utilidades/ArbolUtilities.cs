using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Actualizator.Clases
{
    public class ArbolUtilities
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
        /// Rellena un TreeView dado un objecto que contiene la estructura de los archivos
        /// </summary>
        public static TreeView PopulateArchivoTreeView(ArchivosTreeView archivosTree, TreeNode treeNode, bool HayFiltros, TreeView treeView = null, bool notRoot = false)
        {
            TreeNode directoryNodeRoot = new TreeNode
            {
                Text = archivosTree.DirName
            };

            // Rellena a nivel raiz
            if (treeNode == null)
            {
                treeView.Nodes.Add(directoryNodeRoot);

                AddFilesStringNode(archivosTree.Archivos, ref directoryNodeRoot, HayFiltros);
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

                AddFilesStringNode(directory.Archivos, ref directoryNode, HayFiltros);
                PopulateArchivoTreeView(directory, directoryNode, HayFiltros, null, true);
            }

            return treeView;
        }

        /// <summary>
        /// Añade nodes a un TreeNode, lo devuelve como referencia
        /// </summary>
        /// <param name="archivos">lista de nodes a introducir</param>
        /// <param name="directoryNode">node a modificar</param>
        /// <param name="HayFiltros">indica si se usan filtros</param>
        private static void AddFilesStringNode(List<string> archivos, ref TreeNode directoryNode, bool HayFiltros)
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

        /// <summary>
        /// Filtra una lista de string
        /// </summary>
        /// <param name="archivos">lista a devolver</param>
        /// <returns></returns>
        public static List<string> FiltrarStringArchivos(List<string> archivos)
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
        /// Para introducir un icono dentro del treeNode, no se usa
        /// </summary>
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

    }
}
