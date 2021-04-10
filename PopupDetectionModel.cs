using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace ADP2_Flight_Inspection_App
{


    static class NativeMethods
    {

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);



    }


    public class PopupDetectionModel : IADP2Model
    {
        private bool isStop;
        private Menu notifier;
        private double speed;
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set { 
                time = value;
                nextAnomalyIndex = 0;
                if (anomalyreportSize > 0)
                {
                    nextAnomaly = anomalyReport[nextAnomalyIndex];
                    string[] splitstr = nextAnomaly.Split('-');
                    anomalyTime = int.Parse(splitstr[2]);

                    while (time > anomalyTime && nextAnomalyIndex < (anomalyreportSize - 1))
                    {
                        nextAnomalyIndex++;
                        nextAnomaly = anomalyReport[nextAnomalyIndex];
                        splitstr = nextAnomaly.Split('-');
                        anomalyTime = int.Parse(splitstr[2]);
                    }
                }
            }
        }

        private int numofrows;
        public int numOfRows
        {
            get { return numofrows; }
        }

        private string[] dataArray;
        // map between the column in the CSV to the feature name
        private Dictionary<int, string> features;

        private string regFlightPath;
        private string anomalyFlightPath;
        private string dllPath;

        public string DLLPath
        {
            set
            {
                if (String.Compare(dllPath, value) != 0)
                {
                    dllPath = value;
                    anomalyReport.Clear();
                    getDLLAnomalyReport();
                    while (time > anomalyTime && nextAnomalyIndex < (anomalyreportSize - 1))
                    {
                        nextAnomalyIndex++;
                        nextAnomaly = anomalyReport[nextAnomalyIndex];
                        string[] splitstr = nextAnomaly.Split('-');
                        anomalyTime = int.Parse(splitstr[2]);
                    }
                    
                }

            }
        }

        private List<string> anomalyReport;
        private int nextAnomalyIndex;
        private int anomalyTime;
        private int anomalyreportSize;
        private string nextAnomaly;

        public int AnomalyTime
        {
            get { return anomalyTime; }
        }
        public string NextAnomaly
        {
            get { return nextAnomaly; }
        }


        public PopupDetectionModel(string[] array, string xml, Menu men, string dll , string regFlight, string anomalyFlight)
        {
            anomalyReport = new List<string>();
            dataArray = array;
            dllPath = dll;
            numofrows = array.Length;
            speed = 1;
            time = 0;
            isStop = false;
            notifier = men;
            notifier.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
            features = new Dictionary<int, string>();
            regFlightPath = regFlight;
            anomalyFlightPath = anomalyFlight;
            parseXML(xml);
        }
        

        public void parseXML(string path)
        {
            {
                // counter for the rows on the csv
                int i = 0;
                using (XmlReader reader = XmlReader.Create(path))
                {
                    while (reader.Read() && String.Compare(reader.Name, "output") != 0)
                    {
                    }

                    while (reader.Read() && String.Compare(reader.Name, "output") != 0)
                    {

                        if (String.Compare(reader.Name, "name") == 0)
                        {
                            string name = reader.ReadString();
                            features.Add(i,name);
                            i++;
                        }
                    }
                }

            }
        }
        

        public string getFeature(int coloumn)
        {
            return features[coloumn];
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "Speed") == 0)
            {
                Speed = notifier.Speed;
            }
            else if (String.Compare(propName, "Time") == 0)
            {
                Time = notifier.Time;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }

            else if (String.Compare(propName, "Alarm") == 0) {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }
            else if (String.Compare(propName, "Done") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }

        }

        private string addHeadLineToCSV(string path)
        {
            var csv = new StringBuilder();

            int numOfColoumns = features.Count();
            string headline = "";
            int i;
            for(i=0; i<numOfColoumns-1; i++)
            {
                headline += i.ToString() + ",";
            }
            headline += i.ToString();
            csv.AppendLine(headline);


            var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                csv.AppendLine(line);

            }
            reader.Close();

            string timesstamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string newpath = (path.Split(new string[] { ".csv" }, StringSplitOptions.None))[0] + timesstamp + ".csv";
            File.WriteAllText(newpath, csv.ToString());
            return newpath;
        }

        private void removeHeadLineFromCSV(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


        private void getDLLAnomalyReport()
        {


            IntPtr pDll = NativeMethods.LoadLibrary(dllPath);
            if (pDll == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your DLL.");
                NativeMethods.FreeLibrary(pDll);
                return;
            }

            IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "timeseries_Create");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }


            timeseries_Create timeseries_create = (timeseries_Create)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(timeseries_Create));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_Create");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }

            Anomaly_Detecor_Create simpleDetecor_create = (Anomaly_Detecor_Create)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_Create));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_LearnNormal");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }


            Anomaly_Detecor_LearnNormal simpleDetecor_learnNormal = (Anomaly_Detecor_LearnNormal)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_LearnNormal));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_Detect");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }


            Anomaly_Detecor_Detect simpleDetecor_detect = (Anomaly_Detecor_Detect)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_Detect));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_getReportSize");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }

            Anomaly_Detecor_getReportSize simpleDetecor_getreportSize = (Anomaly_Detecor_getReportSize)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_getReportSize));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_getAnomaly");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }

            Anomaly_Detecor_getAnomaly simpleDetecor_getanomaly = (Anomaly_Detecor_getAnomaly)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_getAnomaly));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_getAnomalyString");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }

            Anomaly_Detecor_getAnomalyString simpleDetecor_getanomalyString = (Anomaly_Detecor_getAnomalyString)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_getAnomalyString));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_DeleteAnomaly");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return;


            }

            Anomaly_Detecor_DeleteAnomaly simpleDetecor_deleteAnomaly = (Anomaly_Detecor_DeleteAnomaly)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_DeleteAnomaly));



            // write a new file that is a copy of the given path but with healines to the coloumns.
            // after we finish using it, we will erase it.
            string newfileNormal = addHeadLineToCSV(regFlightPath);
            string newfileAnomaly = addHeadLineToCSV(anomalyFlightPath);



            IntPtr tsNormal = timeseries_create(newfileNormal);
            IntPtr tsAnomaly = timeseries_create(newfileAnomaly);


            IntPtr sd = simpleDetecor_create();
            simpleDetecor_learnNormal(sd, tsNormal);
            nextAnomalyIndex = 0;
            IntPtr report = simpleDetecor_detect(sd, tsAnomaly);
            anomalyreportSize = (simpleDetecor_getreportSize(report));


            for (int i = 0; i < anomalyreportSize; i++)
            {
                // get the CharHandler
                IntPtr anomaly = simpleDetecor_getanomaly(report, i);
                // get the string from the char handler
                IntPtr strAnomaly = simpleDetecor_getanomalyString(anomaly);
                string stranomaly = Marshal.PtrToStringAnsi(strAnomaly);
                anomalyReport.Add(stranomaly);
                // free the allocated memory
                simpleDetecor_deleteAnomaly(anomaly);

            }
            if(anomalyreportSize > 0)
            {
                nextAnomaly = anomalyReport[0];
                string[] splitstr = nextAnomaly.Split('-');
                anomalyTime = int.Parse(splitstr[2]);
            }


            NativeMethods.FreeLibrary(pDll);
            removeHeadLineFromCSV(newfileNormal);
            removeHeadLineFromCSV(newfileAnomaly);

        }


        public void connect()
        {
            getDLLAnomalyReport();
        }

        public void start()
        {
            // if the something in the dll didnt work,
            //or that there are no anomalies, there is no need to start this model.
            if (anomalyReport.Count > 0)
            {
                new Thread(delegate ()
                {
                    int length = dataArray.Length;
                    while (time != length && isStop != true)
                    {
                        while (time == anomalyTime)
                        {
                            NotifyPropertyChanged("Alarm");
                            if (nextAnomalyIndex < (anomalyreportSize - 1))
                            {
                                nextAnomalyIndex++;
                                nextAnomaly = anomalyReport[nextAnomalyIndex];
                                string[] splitstr = nextAnomaly.Split('-');
                                anomalyTime = int.Parse(splitstr[2]);

                            }


                        }
                        while (time > anomalyTime && nextAnomalyIndex < (anomalyreportSize - 1))
                        {
                            nextAnomalyIndex++;
                            nextAnomaly = anomalyReport[nextAnomalyIndex];
                            string[] splitstr = nextAnomaly.Split('-');
                            anomalyTime = int.Parse(splitstr[2]);
                        }
                        Time++;
                        while (speed == 0)
                        {

                        }
                        Thread.Sleep((int)(100 / speed));
                    }
                    NotifyPropertyChanged("Done");

                }).Start();

            }

        }

        public void stop()
        {
        }


        // functions from the DLL

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr timeseries_Create([MarshalAs(UnmanagedType.LPStr)] string path);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr Anomaly_Detecor_Create();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Anomaly_Detecor_LearnNormal(IntPtr sd, IntPtr ts);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr Anomaly_Detecor_Detect(IntPtr sd, IntPtr ts);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int Anomaly_Detecor_getReportSize(IntPtr report);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr Anomaly_Detecor_getAnomaly(IntPtr report, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr Anomaly_Detecor_getAnomalyString(IntPtr cptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Anomaly_Detecor_DeleteAnomaly(IntPtr cptr);


    }
}
