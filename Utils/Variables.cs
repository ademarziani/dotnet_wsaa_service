using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Security;

namespace WSAA_Service.Utils
{
    class Variables
    {
        public readonly static string DIRRAIZ;

        public readonly static string SEP;
        public readonly static string ENTER;
        public readonly static string LOGDEARCHIVOS;
        public readonly static Int64 SEGUNDOS;
        public readonly static string DIRLOG;

        // Valores por defecto, globales en esta clase
        public readonly static string SERVICIO;
        public readonly static string CERTSIGNER;
        public readonly static string PASSPFX;
        public readonly static string PROXY = null;
        public readonly static string PROXY_USER = null;
        public readonly static string PROXY_PASSWORD = null;
        public readonly static string URLWSAAWSDL;
        public readonly static string TICKET_XML_RESPONSE;

        static Variables()
        {
            XmlDocument config = new XmlDocument();
            XmlNode xmlNodo;

            DIRRAIZ = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            config.Load(DIRRAIZ + "\\configuracion.xml");
            xmlNodo = config.GetElementsByTagName("configuracion")[0];

            SEP = "||";
            ENTER = System.Environment.NewLine;
            LOGDEARCHIVOS = DateTime.Now.ToString("yyyyMMdd").Trim() + ".log";
            SEGUNDOS = Int64.Parse(xmlNodo["segundos"].InnerText);
            DIRLOG = DIRRAIZ + "\\log.log";

            SERVICIO = xmlNodo["servicio"].InnerText;
            CERTSIGNER = xmlNodo["rutapfx"].InnerText;
            PASSPFX = xmlNodo["passpfx"].InnerText;
            PROXY = null;
            PROXY_USER = null;
            PROXY_PASSWORD = null;

            if (xmlNodo["homologacion"].InnerText == "1")
            {
                URLWSAAWSDL = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms?WSDL";
                TICKET_XML_RESPONSE = DIRRAIZ + "\\" + "hml_" + SERVICIO + "_ticket-response.xml";
            }
            else
            {
                URLWSAAWSDL = "https://wsaa.afip.gov.ar/ws/services/LoginCms?WSDL";
                TICKET_XML_RESPONSE = DIRRAIZ + "\\" + "prod_" + SERVICIO + "_ticket-response.xml";
            }
        }
    }
}
