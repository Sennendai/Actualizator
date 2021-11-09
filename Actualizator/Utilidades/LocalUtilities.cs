using BackGroundWorked_ALG;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Actualizator
{
    public static class LocalUtilities
    {
        public static string getErrorException(this Exception exception)
        {
            string result = string.Empty;
            if (exception?.InnerException != null && !string.IsNullOrEmpty(exception?.InnerException.ToString()))
            {
                result = "Inner -> " + exception?.InnerException.ToString();
            }
            if (!string.IsNullOrEmpty(exception?.Message))
            {
                result += (!string.IsNullOrEmpty(result) ? ". Message -> " : "Message -> " + exception?.Message);
            }
            if (string.IsNullOrEmpty(result))
            {
                result = StringResource.error;
            }
            return result;
        }

        public static void MensajeError(string mensaje, string nombreProyecto)
        {
            WriteTextLog(mensaje + StringResource.mensajeFecha + DateTime.Now.ToString(), nombreProyecto);
            MessageBox.Show(mensaje, StringResource.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MensajeInfo(string mensaje, string nombreProyecto)
        {
            WriteTextLog(mensaje + StringResource.mensajeFecha + DateTime.Now.ToString(), nombreProyecto);
            MessageBox.Show(mensaje, StringResource.error, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void WriteTextLog(string text, string nombreProyecto, Label label = null)
        {
            string capetaLogs = Path.Combine(Directory.GetCurrentDirectory(), StringResource.carpetaLogs);
            Directory.CreateDirectory(capetaLogs);
            File.AppendAllText(Path.Combine(capetaLogs, nombreProyecto + StringResource.archivoLog), text + Environment.NewLine);
            if (label != null) label.Text = text;
        }

        public static MyProcessControl ConfigurarProcessControl(MyProcessControl backWork)
        {
            backWork.TextoMostrar = StringResource.extrayendoInfo;
            backWork.BackColor = Color.White;
            backWork.Enabled = true;
            backWork.Visible = true;
            backWork.TicsNum = 5;
            backWork.Height = 75;
            backWork.Width = 200;
            backWork.CuadroColorRelleno = Color.CornflowerBlue;
            backWork.Efecto = MyProcessControl.eEfecto.Fiu;

            return backWork;
        }

        /// <summary>
        /// Comprueba si se tiene permiso para modificar la ruta especificada
        /// </summary>
        public static bool ComprobarAcceso(DirectoryInfo dirInfo)
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

        /// <summary>
        /// Filtra una lista de archivos
        /// </summary>
        /// <param name="archivos">Lista de archivos</param>
        /// <param name="filtros">Lista de filtros</param>
        /// <returns></returns>
        public static FileInfo[] FiltrarArchivos(FileInfo[] archivos, BindingList<Filtro> filtros)
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

        public static Size ResizeTlpControl(TableLayoutPanel tableLayout)
        {
            int height = tableLayout.Size.Height;
            int width = tableLayout.Size.Width;

            if (tableLayout.Controls.Count != 0)
            {
                height = height / tableLayout.Controls.Count;
                width = width / tableLayout.Controls.Count;
            }

            Size size = new Size(width, height);

            return size;
        }

    }
}
