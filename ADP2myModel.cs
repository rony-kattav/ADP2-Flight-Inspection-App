using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;

namespace ADP2_Flight_Inspection_App
{
    public class ADP2myModel 
    {
        private string csv;
        public string CSV
        {
            get { return csv; }
            set
            {
                csv = value;
            }

        }

        private string xml;
        public string XML
        {
            get { return xml; }
            set { xml = value; }
        }


        public ADP2myModel()
        {
            CSV = "";
            XML = "";
        }

        public void updateProperties(string c, string x)
        {
            CSV = c;
            XML = x;
        }

        // copy the xml file from the given path to the flightgear folder
        private void copyXMLToFG()
        {
            /*
            string newFileName = "C:\\Program Files\\FlightGear 2020.3.6\\data\\Protocol\\playback_small_user.xml";
            //string newFileName = @"C:\Users\User\Desktop\playback_small.xml";
            Console.WriteLine(XML);
            
            XmlDocument doc = new XmlDocument();
            doc.Load(XML);

            Console.WriteLine(doc.OuterXml);
            if (File.Exists(newFileName))
                File.Delete(newFileName);
            
            doc.Save(newFileName);
            */
            
        }

        public void start()
        {
            copyXMLToFG();
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5400);
            Socket fg = new Socket(ipAddr.AddressFamily,
                   SocketType.Stream, ProtocolType.Tcp);

                fg.Connect(localEndPoint);
                var writer = new StreamWriter(new NetworkStream(fg));
                //var reader = new StreamReader(@"C:\Users\User\Desktop\flight\reg_flight.csv");
                var reader = new StreamReader(CSV);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    writer.WriteLine(line);
                    Thread.Sleep(100);
                }

                fg.Close();
                reader.Close();
            


        }
    }
}
