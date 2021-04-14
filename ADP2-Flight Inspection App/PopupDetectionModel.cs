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
                else
                {
                    anomalyTime = -1;
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
                    connect();                 
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

        public void connect()
        {
            HelperClassForDLL helper = new HelperClassForDLL(dllPath, features.Count,regFlightPath,anomalyFlightPath);
            anomalyReport = helper.getDLLAnomalyReport();
            anomalyreportSize = anomalyReport.Count; 
            nextAnomalyIndex = 0;
            if (anomalyreportSize > 0)
            {
                nextAnomaly = anomalyReport[0];
                string[] splitstr = nextAnomaly.Split('-');
                anomalyTime = int.Parse(splitstr[2]);
            }
            else
            {
                anomalyTime = -1;
            }
            while (time > anomalyTime && nextAnomalyIndex < (anomalyreportSize - 1))
            {
                nextAnomalyIndex++;
                nextAnomaly = anomalyReport[nextAnomalyIndex];
                string[] splitstr = nextAnomaly.Split('-');
                anomalyTime = int.Parse(splitstr[2]);
            }
        }

        public void start()
        {
            // if the something in the dll didnt work,
            //or that there are no anomalies, there is no need to start this model.

                new Thread(delegate ()
                {
                    int length = dataArray.Length;
                    while (time != length && isStop != true)
                    {
                        if (anomalyReport.Count > 0)
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
                            while (time > anomalyTime && nextAnomalyIndex < (anomalyreportSize - 1) && time != length)
                            {
                                nextAnomalyIndex++;
                                nextAnomaly = anomalyReport[nextAnomalyIndex];
                                string[] splitstr = nextAnomaly.Split('-');
                                anomalyTime = int.Parse(splitstr[2]);
                            }
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

        public void stop()
        {
        }
        
    }
}
