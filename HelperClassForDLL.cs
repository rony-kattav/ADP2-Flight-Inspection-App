using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

    public class HelperClassForDLL
    {
        private string DLLPath;
        private List<string> anomalyReport;
        int featuresNum;
        string regFlightPath;
        string anomalyFlightPath;

        public HelperClassForDLL(string path, int numoffeatures, string regPath, string anomalPath)
        {
            DLLPath = path;
            featuresNum = numoffeatures;
            regFlightPath = regPath;
            anomalyFlightPath = anomalPath;
        }



        public List<string> getDLLAnomalyReport()
        {

            anomalyReport = new List<string>();

            IntPtr pDll = NativeMethods.LoadLibrary(DLLPath);
            if (pDll == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your DLL.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;
            }

            IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "timeseries_Create");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


            }


            timeseries_Create timeseries_create = (timeseries_Create)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(timeseries_Create));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_Create");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


            }

            Anomaly_Detecor_Create simpleDetecor_create = (Anomaly_Detecor_Create)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_Create));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_LearnNormal");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


            }


            Anomaly_Detecor_LearnNormal simpleDetecor_learnNormal = (Anomaly_Detecor_LearnNormal)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_LearnNormal));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_Detect");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


            }


            Anomaly_Detecor_Detect simpleDetecor_detect = (Anomaly_Detecor_Detect)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_Detect));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_getReportSize");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


            }

            Anomaly_Detecor_getReportSize simpleDetecor_getreportSize = (Anomaly_Detecor_getReportSize)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_getReportSize));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_getAnomaly");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


            }

            Anomaly_Detecor_getAnomaly simpleDetecor_getanomaly = (Anomaly_Detecor_getAnomaly)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_getAnomaly));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_getAnomalyString");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


            }

            Anomaly_Detecor_getAnomalyString simpleDetecor_getanomalyString = (Anomaly_Detecor_getAnomalyString)Marshal.GetDelegateForFunctionPointer(
            pAddressOfFunctionToCall,
            typeof(Anomaly_Detecor_getAnomalyString));


            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(pDll, "Anomaly_Detecor_DeleteAnomaly");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Console.WriteLine("There was a problem loading your function.");
                NativeMethods.FreeLibrary(pDll);
                return anomalyReport;


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
            IntPtr report = simpleDetecor_detect(sd, tsAnomaly);
            int anomalyreportSize = (simpleDetecor_getreportSize(report));


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




            NativeMethods.FreeLibrary(pDll);
            removeHeadLineFromCSV(newfileNormal);
            removeHeadLineFromCSV(newfileAnomaly);
            return anomalyReport;

        }

        private string addHeadLineToCSV(string path)
        {
            var csv = new StringBuilder();

            string headline = "";
            int i;
            for (i = 0; i < featuresNum - 1; i++)
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
