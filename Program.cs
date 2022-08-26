using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace WSAA_Service
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Servicio()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
