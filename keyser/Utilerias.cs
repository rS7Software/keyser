using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keyser
{
    public class Utilerias
    {
        public static string LeerConfigEncriptado(string pUbicacionConfig)
        {
            string wCadena = "";
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(pUbicacionConfig);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    wCadena = line;
                }
            }
            file.Close();

            return wCadena;
        }
        //Leer Parametro del Config.txt
        public static string ObtieneParametro(string pUbicacionConfig, string pNomParametro)
        {
            string wCadena = "";
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(pUbicacionConfig);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    if (line.Substring(0, 1) != "'")
                    {
                        int wLong = line.Length;
                        string sClave = line.Substring(0, pNomParametro.Length);
                        if (sClave == pNomParametro) { wCadena = line.Substring(27, wLong - 28); }
                    }
                }

            }
            file.Close();

            return wCadena;
        }

        public static string ObtieneParametroConfig(string pNomParametro, string pconfig)
        {
            string wCadena = "";
            string line = "";
            int len = pconfig.Length;
            int c = 0;
            Boolean sw = true;

            while (c < len)
            {
                sw = false;
                while (c < len)
                {
                    string car = pconfig.Substring(c, 1);
                    c++;
                    if (car == "@") { sw = true; }
                    if (car == "\n") { sw = false; break; }
                    if (sw) { line += car; }
                }
                if (line.Length > 0)
                {
                    if (line.Substring(0, 1) != "'")
                    {
                        int wLong = line.Length;
                        string sClave = line.Substring(0, pNomParametro.Length);
                        if (sClave == pNomParametro) { wCadena = line.Substring(27, wLong - 28); break; }
                    }
                }
                line = "";
            }

            return wCadena;
        }

        public static string ObtieneDataSource(string paramConnString)
        {
            string ret = "";
            try
            {
                int wLong = paramConnString.Length;
                int CC = 0;
                int CCIni = 0;
                int CCFin = 0;
                string wchar = "";
                bool sw = false;
                string value = "";
                while (CC < wLong)
                {
                    if (sw == false)
                    {
                        value = paramConnString.Substring(CC, 11);
                        if (value.ToLower() == "data source") { sw = true; }
                    }

                    if (sw == true)
                    {
                        if (paramConnString.Substring(CC, 1) == "=") { CCIni = CC + 1; }
                        if (CCIni != 0 && paramConnString.Substring(CC, 1) == ";") { CCFin = CC; CC = 9999; }
                    }
                    CC++;
                }
                ret = paramConnString.Substring(CCIni, CCFin - CCIni);
            }
            catch (Exception ex) { ret = ""; }

            return ret;
        }

        public static string ObtieneInitialCatalog(string paramConnString)
        {
            string ret = "";
            try 
            { 
                int wLong = paramConnString.Length;
                int CC = 0;
                int CCIni = 0;
                int CCFin = 0;
                string wchar = "";
                bool sw = false;
                string value = "";
                while (CC < wLong)
                {
                    if (sw == false)
                    {
                        value = paramConnString.Substring(CC, 15);
                        if (value.ToLower() == "initial catalog") { sw = true; }
                    }

                    if (sw == true)
                    {
                        if (paramConnString.Substring(CC, 1) == "=") { CCIni = CC + 1; }
                        if (CCIni != 0 && paramConnString.Substring(CC, 1) == ";") { CCFin = CC; CC = 9999; }
                    }
                    CC++;
                }
                ret = paramConnString.Substring(CCIni, CCFin - CCIni);
            }
            catch (Exception ex) { ret = ""; }

            return ret;
        }

        public static string ObtieneUserID(string paramConnString)
        {
            string ret = "";
            try
            {
                int wLong = paramConnString.Length;
                int CC = 0;
                int CCIni = 0;
                int CCFin = 0;
                string wchar = "";
                bool sw = false;
                string value = "";
                while (CC < wLong)
                {
                    if (sw == false)
                    {
                        value = paramConnString.Substring(CC, 7);
                        if (value.ToLower() == "user id") { sw = true; }
                    }

                    if (sw == true)
                    {
                        if (paramConnString.Substring(CC, 1) == "=") { CCIni = CC + 1; }
                        if (CCIni != 0 && paramConnString.Substring(CC, 1) == ";") { CCFin = CC; CC = 9999; }
                    }
                    CC++;
                }
                ret = paramConnString.Substring(CCIni, CCFin - CCIni);
            }
            catch (Exception ex) { ret = ""; }

            return ret;
        }

        public static string ObtienePassword(string paramConnString)
        {
            string ret = "";
            try
            {
                int wLong = paramConnString.Length;
                int CC = 0;
                int CCIni = 0;
                int CCFin = 0;
                string wchar = "";
                bool sw = false;
                string value = "";
                while (CC < wLong)
                {
                    if (sw == false)
                    {
                        value = paramConnString.Substring(CC, 8);
                        if (value.ToLower() == "password") { sw = true; }
                    }

                    if (sw == true)
                    {
                        if (paramConnString.Substring(CC, 1) == "=") { CCIni = CC + 1; }
                        if (CCIni != 0 && paramConnString.Substring(CC, 1) == ";") { CCFin = CC; CC = 9999; }
                    }
                    CC++;
                }
                ret = paramConnString.Substring(CCIni, CCFin - CCIni);
            }
            catch (Exception ex) { ret = ""; }

            return ret;
        }

        public static string ObtieneResto(string paramConnString)
        {
            string ret = "";
            try
            {
                int wLong = paramConnString.Length;
                int CC = 0;
                int CCIni = 0;
                int CCFin = 0;
                string wchar = "";
                bool sw = false;
                string value = "";
                while (CC < wLong)
                {
                    if (sw == false)
                    {
                        value = paramConnString.Substring(CC, 8);
                        if (value.ToLower() == "password") { sw = true; }
                    }

                    if (sw == true)
                    {
                        if (paramConnString.Substring(CC, 1) == "=") { CCIni = CC + 1; }
                        if (CCIni != 0 && paramConnString.Substring(CC, 1) == ";") { CCFin = CC; CC = 9999; }
                    }
                    CC++;
                }
                CCIni = CCFin + 1;
                ret = paramConnString.Substring(CCIni, wLong - CCIni);
            }
            catch (Exception ex) { ret = ""; }

            return ret;
        }
    }
}
