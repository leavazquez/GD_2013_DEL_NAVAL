using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FrbaBus
{
    static class Config
    {
        private static string m_configPath = "../../FrbaBus.config";
        private static DateTime m_fechaSistema;
        private static string m_string_conexion;

        public static DateTime FechaSistema
        {
            get { return m_fechaSistema; }
        }

        public static string String_Conexion
        {
            get { return m_string_conexion; }
        }

        public static void Inicializar()
        {
            XmlDocument configFile = new XmlDocument();
            configFile.Load(m_configPath);
            XmlElement raiz = configFile.DocumentElement;

            XmlNode nodoFecha = raiz.SelectSingleNode("FechaSistema");
            m_fechaSistema = DateTime.Parse(nodoFecha.InnerText.Trim());

            string dataSource;
            string initialCatalog;
            string userId;
            string password;

            XmlNode nodoDataSource = raiz.SelectSingleNode("DataSource");
            dataSource = nodoDataSource.InnerText.Trim();
            XmlNode nodoInitialCatalog = raiz.SelectSingleNode("InitialCatalog");
            initialCatalog = nodoInitialCatalog.InnerText.Trim();
            XmlNode nodoUserId = raiz.SelectSingleNode("UserId");
            userId = nodoUserId.InnerText.Trim();
            XmlNode nodoPassword = raiz.SelectSingleNode("Password");
            password = nodoPassword.InnerText.Trim();

            m_string_conexion = "Data source=" + dataSource + ";Initial Catalog = " + initialCatalog +
                ";User Id=" + userId + ";Password=" + password;
        }
    }
}
