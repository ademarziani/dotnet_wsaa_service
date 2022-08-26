using System.IO;

namespace WSAA_Service.Utils
{
    class Log
    {
        private static StreamWriter arclog;

        public static void crearLog(string archivoCompleto)
        {
            arclog = File.CreateText(archivoCompleto);
            arclog.Close();
        }

        public static void guardarLog(string logMessage, string archivoCompleto = "", bool append = true)
        {
            string auxArchivo;

            if (archivoCompleto == "")
                auxArchivo = Variables.DIRLOG;
            else
                auxArchivo = archivoCompleto;

            if (!File.Exists(auxArchivo))
            {
                crearLog(auxArchivo);
            }

            if (append)
                arclog = File.AppendText(auxArchivo);
            else
                arclog = File.CreateText(auxArchivo);

            arclog.WriteLine(logMessage);
            arclog.Flush();
            arclog.Close();
        }
    }
}
