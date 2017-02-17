using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MW_Online_Server
{
    public static class config
    {
        public static int Port = 5555;
        public static string ClientVersion = "0.1";
        public static string ServerName = "Default MW-Online server";
        public static string RconPass = "NONE";

        public static void InitConfig()
        {
            if (!File.Exists("config.ini"))
            {
                Stream stream = File.Create("config.ini");

                StreamWriter writer = new StreamWriter(stream);
                writer.Write("port:5555\r\nname:Default Server\r\nrcon password:NONE");
                writer.Close();
                stream.Close();

                writer.Dispose();
                stream.Dispose();


            }

            StreamReader reader = new StreamReader("config.ini");
            string[] r = reader.ReadToEnd().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            reader.Close(); reader.Dispose();

            foreach (string i in r)
            {
                if (i.IndexOf("port:") >= 0)
                {
                    string ii = i.Replace("port:", String.Empty);
                    Port = int.Parse(ii);
                }
                else if (i.IndexOf("name:") >= 0)
                {
                    string ii = i.Replace("name:", String.Empty);
                    ServerName = ii;
                }
                else if (i.IndexOf("rcon password:") >= 0)
                {
                    string ii = i.Replace("rcon password:", String.Empty);
                    RconPass = ii;
                }

            }
        }
    }
}