using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using WSAA_Service.Utils;
using WSAA_Service.App;

namespace WSAA_Service
{
    public partial class Servicio : ServiceBase
    {
        Timer timer = new Timer();
        public static bool semaforo = true;

        public Servicio()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.guardarLog($"{DateTime.Now} - Servicio Iniciado");

            timer.Elapsed += new ElapsedEventHandler(ProcesoServicio);
            timer.Interval = Variables.SEGUNDOS * 1000; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            Log.guardarLog($"{DateTime.Now} - Servicio Parado");
        }

        private void ProcesoServicio(object source, ElapsedEventArgs e)
        {
            if (semaforo)
            {
                try
                {
                    semaforo = false;

                    if (!ProgramaPrincipal.ticketValido())
                        ProgramaPrincipal.getTicket();                    

                    semaforo = true;
                }
                catch (Exception error)
                {
                    Log.guardarLog($"{DateTime.Now} - Error en la generacion del ticket: {error.Message}");
                }
                finally
                {
                    semaforo = true;
                }
            }
        }
    }
}
