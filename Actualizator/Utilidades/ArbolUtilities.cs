using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Actualizator.Clases
{
    public class ArbolUtilities
    {
        /// <summary>
        /// Rellena un TreeView dado un objecto que contiene la estructura de los archivos
        /// </summary>
        public static TreeView PopulateArchivoTreeView(ArchivosTreeView archivosTree, TreeNode treeNode, TreeView treeView = null)
        {
            TreeNode directoryNodeRoot = new TreeNode
            {
                Text = archivosTree.DirName
            };
            // Rellena las subcarpetas
            foreach (var directory in archivosTree.Subdir)
            {
                // si la carpeta y sus subcarpetas estan vacias, no se incluye
                if (directory.GetTotalArchivos() == 0) continue;

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

                AddFilesStringNode(directory, ref directoryNode);
                PopulateArchivoTreeView(directory, directoryNode, null);
            }
            // Rellena a nivel raiz
            if (treeNode == null)
            {
                treeView.Nodes.Add(directoryNodeRoot);

                AddFilesStringNode(archivosTree, ref directoryNodeRoot);
            }

            return treeView;
        }

        /// <summary>
        /// Añade nodes a un TreeNode, lo devuelve como referencia
        /// </summary>
        /// <param name="archivos">lista de nodes a introducir</param>
        /// <param name="directoryNode">node a modificar</param>
        /// <param name="HayFiltros">indica si se usan filtros</param>
        private static void AddFilesStringNode(ArchivosTreeView archivos, ref TreeNode directoryNode)
        {
            foreach (string file in archivos.Archivos)
            {
                TreeNode fileNode = new TreeNode
                {
                    Text = file
                };

                directoryNode.Nodes.Add(fileNode);
            }
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
