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
            if (exception.InnerException != null && !string.IsNullOrEmpty(exception.InnerException.ToString()))
            {
                result = "Inner -> " + exception.InnerException.ToString();
            }
            if (!string.IsNullOrEmpty(exception.Message))
            {
                result += (!string.IsNullOrEmpty(result) ? ". Message -> " : "Message -> " + exception.Message);
            }
            if (string.IsNullOrEmpty(result))
            {
                result = StringResource.error;
            }
            return result;
        }

        public static void MensajeError(string mensaje)
        {
            WriteTextLog(mensaje + StringResource.mensajeFecha + DateTime.Now.ToString());
            MessageBox.Show(mensaje, StringResource.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MensajeInfo(string mensaje)
        {
            WriteTextLog(mensaje + StringResource.mensajeFecha + DateTime.Now.ToString());
            MessageBox.Show(mensaje, StringResource.error, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void WriteTextLog(string text, Label label = null)
        {
            File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), StringResource.archivoLog), text + Environment.NewLine);
            if (label != null) label.Text = text;
        }

    }
}
