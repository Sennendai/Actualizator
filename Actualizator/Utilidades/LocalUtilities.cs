using System;
using System.IO;
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

    }
}
