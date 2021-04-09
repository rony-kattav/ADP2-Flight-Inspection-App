using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

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
            set { time = value; }
        }

        private int numofrows;
        public int numOfRows
        {
            get { return numofrows; }
        }

        private string XMLpath;
        private string[] dataArray;
        private string DLLPath;
        
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


        // map between the coloumn name in the csv to it's coloumn number
        private Dictionary<string, int> coloumns;

        public PopupDetectionModel(string[] array, string xml, Menu men, string dll)
        {
            anomalyReport = new List<string>();
            XMLpath = xml;
            dataArray = array;
            DLLPath = dll;
            numofrows = array.Length;
            speed = 1;
            time = 0;
            isStop = false;
            coloumns = new Dictionary<string, int>();
            notifier = men;
            notifier.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
            
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
            }

            else if (String.Compare(propName, "Alarm") == 0) {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }

        }


        private void getDLLAnomalyReport()
        {
            IntPtr pDll = NativeMethods.LoadLibrary(DLLPath);
            if (pDll == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your DLL.");
                return;
            }

            IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "timeseries_Create");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                return;


            }


            timeseries_Create timeseries_create = (timeseries_Create)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(timeseries_Create));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "simpleDetecor_Create");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                return;


            }

            simpleDetecor_Create simpleDetecor_create = (simpleDetecor_Create)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(simpleDetecor_Create));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "simpleDetecor_LearnNormal");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                return;


            }


            simpleDetecor_LearnNormal simpleDetecor_learnNormal = (simpleDetecor_LearnNormal)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(simpleDetecor_LearnNormal));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "simpleDetecor_Detect");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                return;


            }


            simpleDetecor_Detect simpleDetecor_detect = (simpleDetecor_Detect)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(simpleDetecor_Detect));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "simpleDetecor_getReportSize");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                return;


            }

            simpleDetecor_getReportSize simpleDetecor_getreportSize = (simpleDetecor_getReportSize)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(simpleDetecor_getReportSize));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "simpleDetecor_getAnomaly");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                return;


            }

            simpleDetecor_getAnomaly simpleDetecor_getanomaly = (simpleDetecor_getAnomaly)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(simpleDetecor_getAnomaly));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "simpleDetecor_getAnomalyString");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                return;


            }

            simpleDetecor_getAnomalyString simpleDetecor_getanomalyString = (simpleDetecor_getAnomalyString)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(simpleDetecor_getAnomalyString));

            string path = @"C:\Users\User\Desktop\flight\reg_flight.csv";
            string anomalyPath = @"C:\Users\User\Desktop\flight\anomaly_flight.csv";
            IntPtr tsNormal = timeseries_create(path);
            IntPtr tsAnomaly = timeseries_create(anomalyPath);


            IntPtr sd = simpleDetecor_create();
            simpleDetecor_learnNormal(sd, tsNormal);
            nextAnomalyIndex = 0;
            IntPtr report = simpleDetecor_detect(sd, tsAnomaly);
            anomalyreportSize = (simpleDetecor_getreportSize(report));


            for (int i = 0; i < anomalyreportSize; i++)
            {
                IntPtr anomaly = simpleDetecor_getanomaly(report, i);
                IntPtr strAnomaly = simpleDetecor_getanomalyString(anomaly);
                string stranomaly = Marshal.PtrToStringAnsi(strAnomaly);
                anomalyReport.Add(stranomaly);

            }
            nextAnomaly = anomalyReport[0];
            string[] splitstr = nextAnomaly.Split('-');
            anomalyTime = int.Parse(splitstr[2]);

            NativeMethods.FreeLibrary(pDll);

        }



        public void connect()
        {
            getDLLAnomalyReport();
        }

        public void start()
        {

            new Thread(delegate ()
            {
                int length = dataArray.Length;
                while (time != length && isStop != true)
                {
                    if(time == anomalyTime)
                    {
                        NotifyPropertyChanged("Alarm");
                        if(nextAnomalyIndex < (anomalyreportSize - 1))
                        {
                            nextAnomalyIndex++;
                            nextAnomaly = anomalyReport[nextAnomalyIndex];
                            string[] splitstr = nextAnomaly.Split('-');
                            anomalyTime = int.Parse(splitstr[2]);

                        }


                    }
                    else if(time > anomalyTime && nextAnomalyIndex < (anomalyreportSize -1))
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

            }).Start();



        }

        public void stop()
        {
        }


        // functions from the DLL

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr timeseries_Create([MarshalAs(UnmanagedType.LPStr)] string path);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr simpleDetecor_Create();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void simpleDetecor_LearnNormal(IntPtr sd, IntPtr ts);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr simpleDetecor_Detect(IntPtr sd, IntPtr ts);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int simpleDetecor_getReportSize(IntPtr report);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr simpleDetecor_getAnomaly(IntPtr report, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr simpleDetecor_getAnomalyString(IntPtr cptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void simpleDetecor_DeleteAnomaly(IntPtr cptr);


    }
}
