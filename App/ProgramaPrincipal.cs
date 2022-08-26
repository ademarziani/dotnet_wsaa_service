using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;
using System.Xml;
using WSAA_Service.Utils;

namespace WSAA_Service.App
{
    /// <summary>
    /// Clase principal
    /// </summary>
    /// <remarks></remarks>
    class ProgramaPrincipal
    {
        public static bool ticketValido()
        {
            bool ticketValido = false;
            string archivo = Variables.TICKET_XML_RESPONSE;

            if (File.Exists(archivo))
            {
                string contenido = "";
                XmlDocument XmlLoginTicketResponse = new XmlDocument();
                FileStream fs = new FileStream(archivo, FileMode.Open, FileAccess.Read,
                                      FileShare.ReadWrite | FileShare.Delete);

                contenido = new StreamReader(fs, Encoding.UTF8).ReadToEnd();
                XmlLoginTicketResponse.LoadXml(contenido);

                ticketValido = DateTime.Now < DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
            }

            return ticketValido;
        }

        public static int getTicket()
        {
            const string ID_FNC = "[Main]";

            string strIdServicioNegocio = Variables.SERVICIO;
            string strUrlWsaaWsdl = Variables.URLWSAAWSDL;
            string strRutaCertSigner = Variables.CERTSIGNER;
            SecureString strPasswordSecureString = new SecureString();
            string strProxy = Variables.PROXY;
            string strProxyUser = Variables.PROXY_USER;
            string strProxyPassword = Variables.PROXY_PASSWORD;

            // Argumentos OK, entonces procesar normalmente...
            LoginTicket objTicketRespuesta = null;
            string strTicketRespuesta = null;

            try
            {
                objTicketRespuesta = new LoginTicket();

                foreach (char c in Variables.PASSPFX)
                    strPasswordSecureString.AppendChar(c);

                strPasswordSecureString.MakeReadOnly();               

                strTicketRespuesta = objTicketRespuesta.ObtenerLoginTicketResponse(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, strPasswordSecureString, strProxy, strProxyUser, strProxyPassword);
            }
            catch (Exception excepcionAlObtenerTicket)
            {
                throw new Exception(ID_FNC + "***EXCEPCION AL OBTENER TICKET: " + excepcionAlObtenerTicket.Message);
            }
            return 0;
        }        
    }
}
