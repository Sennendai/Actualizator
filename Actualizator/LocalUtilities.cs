using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

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
                result = result + (!string.IsNullOrEmpty(result) ? ". Message -> " : "Message -> " + exception.Message);
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "Error.";
            }
            return result;
        }

        public static void MensajeError(string mensaje)
        {
            WriteTextLog(mensaje + " | Fecha: " + DateTime.Now.ToString());
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void WriteTextLog(string text)
        {
            File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), text + Environment.NewLine);
        }
    }
}
