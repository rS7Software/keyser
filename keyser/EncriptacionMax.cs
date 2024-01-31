using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keyser
{
    public static class EncriptacionMax
    {
        public static String ASCII250 = @"■☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼ !¹#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~⌂ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜø£Ø×ƒáíóúñÑªº¿®¬½¼¡«»░▒▓│┤ÁÂÀ©╣║╗╝¢¥┐└┴┬├─┼ãÃ╚╔╩╦╠═╬¤ðÐÊËÈıÍÎÏ┘┌█▄¦Ì▀ÓßÔÒõÕµþÞÚÛÙýÝ¯´­±‗¾³²÷¸°¨·";
        public static String ASCII250Inv = @"■☻☺♠◙♂♀♪♫♥♦♣☼►¶§▬↨↑◄↕‼↓→←▼ !#$∟↔▲%¹&'+,-./01()*23489:;<567=>?@AEFBCDJKLMQRSTUPWXYZ^_`abcd[\]efghlmnopqrstuvwxyz{|}~⌂ÇüéâijkäàåçêëèïîìÄÅÉöòûùÿÖÜø£Ø×ƒáæÆôíóúñÑªº½¼¡«»░▒¿®¬▓│┤ÁÂÀ©╣║╗╝¢¥┐└┴┬├─┼ãÃ╚╔╩╦╠═╬¤ðÐÊËÈıÍÎÏ┘┌█▄¦Ì▀ÓßÔVNOÒõÕµþÞÚGHIÛÙýÝ¯´­±‗¾³²÷¸°•◘○¨·";
        //0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
        public static int hora()
        {
            String sHora = DateTime.Now.ToString("mmss");
            return Convert.ToInt16(sHora);
        }

        public static String buscaCharCodeDeEncriptacion()
        {
            String caracter = "A";
            int Hora = hora();
            byte I = 1; // Empieza desde el 1 porque el 0 es NULL
            int cont = 0;
            while (I < 250)
            {
                I++;
                if (I == 33) { I = 128; }
                if (I == 160) { I = 166; }

                cont++;
                if (cont == Hora) { break; }
                if (I == 249) { I = 1; }
            }

            caracter = charFromASCII(I);
            return caracter;
        }

        public static byte digAlteracion(byte codASCII)
        {
            byte res = 0;
            byte len = Convert.ToByte(codASCII.ToString().Length);
            byte I = 0;

            while (I < len)
            {
                String dig = codASCII.ToString().Substring(I, 1);
                res += Convert.ToByte(dig);
                I++;
            }

            return res;
        }

        public static string Encriptado(string texto)
        {
            List<Campo> lista = new List<Campo>();

            String res = "";
            String charCode = buscaCharCodeDeEncriptacion();
            byte codASCIIcharCode = codASCIIFromChar(charCode);
            res = charCode;

            int lenTexto = texto.Length;
            int I = 0;
            long dMulto = digAlteracion(codASCIIcharCode);
            while (I < lenTexto)
            {
                long dMult = dMulto * (I + 1);
                String carACodificar = texto.Substring(I, 1);
                byte codASCIIcarACodificar = codASCIIFromChar(carACodificar);
                long factor = codASCIIcharCode + dMult;
                long codigo = codASCIIcarACodificar * factor;
                String sHexadec = D250cimal(codigo);
                if (sHexadec.Substring(0, 1) != "■" || sHexadec.Substring(1, 1) != "■" || sHexadec.Substring(2, 1) != "■" || sHexadec.Substring(3, 1) != "■" || sHexadec.Substring(4, 1) != "■")
                { MessageBox.Show("Error código demasiado grande."); }

                String indice = D250cimal(I).Substring(sHexadec.Length - 1, 1);
                long numAleat = NumeroAleatorio(codigo);
                Campo objCampo = new Campo(indice, sHexadec.Substring(sHexadec.Length - 3, 3), Convert.ToInt32(numAleat));
                lista.Add(objCampo);
                I++;
            }

            string[] campos = new string[250];
            string[] indices = new string[250];
            I = 0;
            foreach (var elemento in lista)
            {
                I = Convert.ToInt16(elemento.NumAleatorio) - 1;
                while (true)
                {
                    if (I > 249 || I < 0) { I = 0; }
                    if (campos[I] == null) { campos[I] = elemento.NumHexadecimal; indices[I] = elemento.IdCampo; break; }
                    else { I++; }
                }
            }

            I = 0;
            while (I < 250)
            {
                if (campos[I] != null) { res += indices[I] + campos[I].Substring(campos[I].Length - 3, 3); }
                I++;
            }

            return res;
        }

        public static long NumeroAleatorio(long pNumero)
        {
            long res = pNumero;
            long r = 0;

            while (res > 250)
            {
                r = res / 11;
                res = r;
            }

            return res;
        }

        public static string Decriptado(string textoCodificado)
        {
            List<Campo> lista = new List<Campo>();
            String charCode = textoCodificado.Substring(0, 1);
            byte codASCIIcharCode = codASCIIFromChar(charCode);
            long dMulto = digAlteracion(codASCIIcharCode);

            String res = "";

            int totCampos = 0;
            int I = 1;
            int c = 0;
            int len = textoCodificado.Length;
            while (I < len)
            {
                String IdHex = textoCodificado.Substring(I, 1);
                String campo = textoCodificado.Substring(I + 1, 3);

                Campo objCampo = new Campo(IdHex, campo, 0);
                lista.Add(objCampo);
                totCampos++;
                I = I + 4;
                c++;
            }

            int lenc = 0;
            string[] campos = new string[totCampos];
            foreach (var elemento in lista)
            {
                lenc = elemento.NumHexadecimal.Length;
                byte IdCampo = Convert.ToByte(D250cimalADecimal(elemento.IdCampo));

                campos[Convert.ToInt16(IdCampo)] = elemento.NumHexadecimal;
            }

            I = 0;
            while (I < totCampos)
            {
                long numDecimal = D250cimalADecimal(campos[I]);
                long dMult = dMulto * (I + 1);
                long factor = codASCIIcharCode + dMult;
                long ascii = numDecimal / factor;

                res += charFromASCII(Convert.ToByte(ascii));
                I++;
            }

            return res;
        }

        public static String charFromASCII(byte codASCII)
        {
            String caracter = "A";
            if (codASCII < 0 || codASCII > 249)
            {
                MessageBox.Show("Error en codigo ASCII al convertir a char.");
                return "";
            }

            caracter = ASCII250Inv[codASCII].ToString();

            if (codASCII == 34) { caracter = @""""; }
            if (codASCII == 0) { caracter = "\n"; }     // Se asume que es enter

            return caracter;
        }

        public static byte codASCIIFromChar(string s)
        {
            String caracter = "A";
            int len = ASCII250Inv.Length;
            int I = 1;
            while (I < len)
            {
                caracter = ASCII250Inv.Substring(I, 1);
                if (caracter == s) { break; }
                I++;
            }
            byte codASCII = Convert.ToByte(I);

            if (s == @"""") { codASCII = 34; }
            if (s == "■") { codASCII = 0; }

            if (codASCII < 0 || codASCII > 249)
            {
                //MessageBox.Show("Error en codigo ASCII al convertir a byte. Está entrando un caracter que no se encontró en la tabla." + Environment.NewLine + "Caracter: " + s);
                return 0;
            }

            return codASCII;
        }

        //ASCII =  @"■☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼ !0#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~⌂ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜø£Ø×ƒáíóúñÑªº¿®¬½¼¡«»░▒▓│┤ÁÂÀ©╣║╗╝¢¥┐└┴┬├─┼ãÃ╚╔╩╦╠═╬¤ðÐÊËÈıÍÎÏ┘┌█▄¦Ì▀ÓßÔÒõÕµþÞÚÛÙýÝ¯´­±‗¾¶§÷¸°¨";
        // Convierte un numero en 250cimal
        // el 16 en hexadecimal es 10
        // 1                ☺
        //250               ☺■
        //249               ■¨
        //248               ■°
        //499               ☺¨
        //500               ☻■
        //62499             ¨¨
        //62500             ☺■■    
        //15624999          ¨¨¨
        //15625000          ☺ ■ ■ ■
        //3,906,250,000
        //976,562,500,000

        public static String D250cimal(long numero)
        {
            //MessageBox.Show(numero.ToString());
            //numero = 15624999;
            string[] letras = new string[250];
            letras[0] = "■";
            byte l = 1;
            while (l < 250)
            {
                letras[l] = charFromASCII(l);
                l++;
            }

            String hexadecimal = "";
            //(3906250) (15625000) (62500) (250)
            long resultdivision;
            long restante;
            long codificado = 0;
            int pos = 7;
            long limP = 250;
            long porCodificar = numero;

            while (pos > 0)
            {
                limP = Convert.ToInt64(Math.Pow(250, pos));
                resultdivision = porCodificar / limP;
                restante = numero % limP;

                if (porCodificar >= limP)
                {
                    hexadecimal = hexadecimal + letras[resultdivision];

                    codificado = resultdivision * limP;
                    porCodificar = porCodificar - codificado;
                }
                else { hexadecimal = hexadecimal + letras[0]; }

                pos = pos - 1;
            }

            resultdivision = porCodificar / 250;
            restante = numero % 250;

            if (porCodificar >= 0)
            {
                hexadecimal = hexadecimal + letras[restante]; // primer casilla
                codificado = restante;
                porCodificar = porCodificar - codificado;
            }

            return hexadecimal;
        }

        // Convierte un numero 250cimal en decimal
        public static long D250cimalADecimal(String hexadecimal)
        {
            long ndecimal = 0;

            int len = hexadecimal.Length;
            int c = len - 1;
            String car = "";
            int codigo = 0;
            int pos = 0;
            long limP = 250;

            while (c >= 0)
            {
                car = hexadecimal.Substring(c, 1);
                codigo = codASCIIFromChar(car);
                limP = Convert.ToInt64(Math.Pow(250, pos));
                ndecimal += codigo * limP;
                pos++;
                c = c - 1;
            }

            return Convert.ToInt64(ndecimal);
        }

        public class Campo
        {
            // Campos
            public String IdCampo;
            public String NumHexadecimal;
            public int NumAleatorio;

            // Constructor
            public Campo()
            {
                IdCampo = "";
                NumHexadecimal = "";
                NumAleatorio = 0;
            }

            public Campo(string idCampo, string numHexadecimal, int numAleatorio)
            {
                IdCampo = idCampo;
                NumHexadecimal = numHexadecimal;
                NumAleatorio = numAleatorio;
            }
        }
    }
}
