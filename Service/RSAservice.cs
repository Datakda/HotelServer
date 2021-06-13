using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelServer.Service
{
    public class RSAservice
    {
        private RSACryptoServiceProvider cps = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public RSAservice(string XmlKey)
        {
            _privateKey = cps.ExportParameters(true);
            _publicKey = cps.ExportParameters(false);
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _privateKey);

            using (FileStream privateKey = new FileStream("privateKey.xml", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(sw.ToString());
                // запись массива байтов в файл
                privateKey.Write(array, 0, array.Length);

            }
            

            using (FileStream publicKey = new FileStream("publicKey.xml", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(XmlKey);
                // запись массива байтов в файл
                publicKey.Write(array, 0, array.Length);

            }




        }

        public RSAservice() 
        {
            using (StreamReader _public = new StreamReader("publicKey.xml"))
            {
                _publicKey = ParsXmlToRSAP.GetPubKeyFromXmlString(_public.ReadToEnd());
            }


            using (StreamReader _private = new StreamReader("privateKey.xml"))
            {
                _privateKey = ParsXmlToRSAP.GetPrivKeyFromXmlString(_private.ReadToEnd());
            }





        }


        public string Encrypt(string plainText)
        {
            cps = new RSACryptoServiceProvider();
            cps.ImportParameters(_publicKey);
            var data = Encoding.Unicode.GetBytes(plainText);
            var cypher = cps.Encrypt(data, false);
            return Convert.ToBase64String(cypher);

        }
        public string Decrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            cps.ImportParameters(_privateKey);
            var plainText = cps.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plainText);
        }

        public string GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }

    }
}
