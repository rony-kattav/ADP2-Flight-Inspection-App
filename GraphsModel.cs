using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace ADP2_Flight_Inspection_App
{
    public class GraphsModel : IADP2Model
    {
        private double speed;
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
            }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set
            {
                time = value;
                Feature = this.title;
            }
        }

        private int numofrows;
        public int numOfRows
        {
            get { return numofrows; }
        }
        private string XMLpath;
        private string[] dataArray;
        // map between the coloumn name in the csv to it's coloumn number
        private Dictionary<string, int> coloumns;
        private Dictionary<int, int> correlatedFeatures;

        private bool isStop;
        private Menu notifier;

        private List<DataPoint> measure;
        private List<DataPoint> corMeasure;
        private List<DataPoint> correlationLineRegPoints;
        private List<ScatterPoint> correlationPoints;
        private string title;
        private string corTitle;
        private string correlationTitle;

        private double dotSize = 5;
        private double colorValue = 50;

        public bool IsStop { get { return this.isStop; } set { this.isStop = value; } }
        public List<DataPoint> Measure { get { return this.measure; } }
        public List<DataPoint> CorMeasure { get { return this.corMeasure; } }
        public List<ScatterPoint> CorrelationPoints { get { return this.correlationPoints; } }
        public List<DataPoint> CorrelationLineRegPoints { get { return this.correlationLineRegPoints; } }

        public string Feature
        {
            get { return this.title; }
            set
            {
                this.Title = value;
                int corColumn = correlatedFeatures[coloumns[Title]];
                var key = coloumns.FirstOrDefault(x => x.Value == corColumn).Key;
                this.CorTitle = key;
                this.CorrelationTitle = "The Correlation Between " + this.title + " and " + this.corTitle;
                NotifyPropertyChanged("graphsNamesChanged");
            }
        }

        public string Title {
            get { return this.title; }
            set {
                this.title = value;
                lock (this.measure)
                {
                    this.measure.Clear();
                    int i = Math.Max(time - 300, 0);
                    while (i <= time)
                    {
                        this.measure.Add(new DataPoint(i, getValueFromCSV(i, this.title)));
                        i++;
                    }
                } 
            } 
        }
        // TODO: also setting the correlationPoints of the last 30 sec
        public string CorTitle {
            get { return this.corTitle; }
            set
            {
                this.corTitle = value;
                lock (this.corMeasure)
                {
                    this.corMeasure.Clear();
                    int i = Math.Max(time - 300, 0);
                    while (i <= time)
                    {
                        this.corMeasure.Add(new DataPoint(i, getValueFromCSV(i, this.corTitle)));
                        i++;
                    }
                }
            }
        }

        public string CorrelationTitle
        {
            get { return this.correlationTitle; }
            set
            {
                this.correlationTitle = value;
                lock(this.correlationPoints){
                    this.correlationPoints.Clear();
                    int i = Math.Max(time - 300, 0);
                    while (i <= time)
                    {
                        this.correlationPoints.Add(new ScatterPoint(getValueFromCSV(i, this.title), getValueFromCSV(i, this.corTitle), dotSize, colorValue));
                        i++;
                    }
                    lock (this.correlationLineRegPoints)
                    {
                        this.correlationLineRegPoints.Clear();
                        this.correlationLineRegPoints.AddRange(getLinearReg(this.title, this.corTitle));
                    }
                }
                
                
            }
        }

        public GraphsModel(string[] array, string xml, Menu men)
        {
            XMLpath = xml;
            dataArray = array;
            numofrows = array.Length;
            measure = new List<DataPoint>();
            corMeasure = new List<DataPoint>();
            correlationPoints = new List<ScatterPoint>();
            correlationLineRegPoints = new List<DataPoint>();
            speed = 1;
            time = 0;
            isStop = false;
            coloumns = new Dictionary<string, int>();

            readXML();
            correlatedFeatures = findBestCor();

            // Delete after debug
            printCorrelatedFea();

            title = coloumns.ElementAt(0).Key;
            var key = coloumns.FirstOrDefault(x => x.Value == correlatedFeatures[coloumns[Title]]).Key;
            this.CorTitle = key;
            correlationTitle = "The Correlation Between " + title + " and " + corTitle;
            lock (this.correlationLineRegPoints)
            {
                this.correlationLineRegPoints.AddRange(getLinearReg(this.title, this.corTitle));
            }
            notifier = men;
            notifier.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        // Delete after debug
        private void printCorrelatedFea()
        {
            for (int i=0; i<this.correlatedFeatures.Count; i++)
            {
                string fea = coloumns.FirstOrDefault(x => x.Value == correlatedFeatures[i]).Key;
                string corfea = coloumns.FirstOrDefault(x => x.Value == correlatedFeatures[coloumns[fea]]).Key;
                Console.WriteLine(i +"Feature: " + fea +" is corralted to feature: " + corfea+ "\n");

            }
            
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

            else if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

            }

        }


        private void readXML()
        {
            // counter for the rows on the csv
            int i = 0;
            // counter for the throttles (according to number of engines)
            int j = 1;
            using (XmlReader reader = XmlReader.Create(XMLpath))
            {
                while (reader.Read() && String.Compare(reader.Name, "output") != 0)
                {
                }

                while (reader.Read() && String.Compare(reader.Name, "output") != 0)
                {

                    if (String.Compare(reader.Name, "name") == 0)
                    {
                        string name = reader.ReadString();
                        // if there is the same name but for different engines,
                        //the first one will be as it's written but the nexts will be with numbers.
                        // for example - if there are 2 throttles - the first one will be "throttle" and the next "throttle1"
                        int k = 1;
                        while (coloumns.ContainsKey(name))
                        {
                            if (Char.IsDigit(name[name.Length - 1]))
                            {
                                name.Remove(name.Length - 1, 1);
                            }
                            name += k;
                            k++;
                        }
                        coloumns.Add(name, i);
                        i++;
                    }
                }
            }

        }

        public void connect()
        {
            readXML();
           
        }

        public List<string> getColoumnsNames()
        {
            if (coloumns.Count == 0)
            {
                readXML();
            }
            List<string> names = new List<string>();
            for(int i=0; i<coloumns.Count; i++)
            {
                names.Add(coloumns.ElementAt(i).Key);
            }
            return names;
        }

        private List<DataPoint> getLinearReg (string wanttedFeature, string corWanttedFeature)
        { 
            List<DataPoint> linear_regression = new List<DataPoint>();
            List<Double> featureValues = new List<Double>();
            List<Double> corFeatureValues = new List<Double>();
            for (int timeStep = 0; timeStep<dataArray.Length; timeStep++)
            {
                featureValues.Add(getValueFromCSV(timeStep, wanttedFeature));
                corFeatureValues.Add(getValueFromCSV(timeStep, corWanttedFeature));
            }
            double covariance = cov(featureValues, corFeatureValues);
            double variance = var(featureValues);
            double avgFeatureValues = featureValues.Average();
            double avgCorFeatureValues = corFeatureValues.Average();

            double a = covariance / variance;
            double b = avgCorFeatureValues - (a * avgFeatureValues);

            double x1 = featureValues.Min();
            double x2 = featureValues.Max();

            double y1 = (a * x1) + b;
            double y2 = (a * x2) + b;

            linear_regression.Add(new DataPoint(x1, y1));
            linear_regression.Add(new DataPoint(x2, y2));

            return linear_regression;
        }

        
        private double cov (List<double> X, List<double> Y)
        {
            List<double> XY_Mult = new List<double>();
            for (int i = 0; i < X.Count; i++)
            {
                XY_Mult.Add(X.ElementAt(i) * Y.ElementAt(i));
            }
            double av_xy = XY_Mult.Average();
            double av_x = X.Average();
            double av_y = Y.Average();
            return (av_xy - (av_x * av_y));
        }

        private double var(List<double> X)
        {
            double sum_pow = 0;
            for (int i = 0; i < X.Count; i++)
            {
                sum_pow += Math.Pow(X.ElementAt(i), 2.0);
            }
            double exp1 = sum_pow / X.Count;

            double exp2 = Math.Pow( X.Average(), 2.0);
            return (exp1 - exp2);
        }

       
        private double getValueFromCSV(int time, string measure)
        {
            if (time >= dataArray.Length)
            {
                return 0;
            }
            string line = dataArray[time];
            string[] pars = line.Split(',');
            double value = Double.Parse(pars[coloumns[measure]]);
            return value;
        }

        // returns an array of lists - each list is a coloumn from the csv.
        private List<double>[] create2Darray()
        {
            string[] splitline = dataArray[0].Split(',');
            int numOfCols = splitline.Length;
            double[,] array2D = new double[numofrows, numOfCols];
            for (int i = 0; i < numOfRows; i++)
            {
                splitline = dataArray[i].Split(',');
                for (int j = 0; j < numOfCols; j++)
                {
                    array2D[i, j] = Double.Parse(splitline[j]);
                }
            }
            return toList(array2D, numOfCols, numofrows);
        }

        private List<double>[] toList(double[,] arr, int cols, int rows)
        {
            List<double>[] listArr = new List<double>[cols];
            for (int i = 0; i < cols; i++)
            {
                listArr[i] = new List<double>();
                for (int j = 0; j < rows; j++)
                {
                    listArr[i].Add(arr[j, i]);
                }
            }
            return listArr;
        }

        private Dictionary<int, int> findBestCor()
        {
            Dictionary<int, int> correlations = new Dictionary<int, int>();
            List<double>[] array = create2Darray();
            int numofcols = array.Length;
            for (int i = 0; i < numofcols; i++)
            {
                double maxcor = 0;
                int maxcorcol = -1;
                for (int j = 0; j < numofcols; j++)
                {
                    if (i != j)
                    {
                        var avg1 = array[i].Average();
                        var avg2 = array[j].Average();

                        var sum1 = array[i].Zip(array[j], (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

                        var sumSqr1 = array[i].Sum(x => Math.Pow((x - avg1), 2.0));
                        var sumSqr2 = array[j].Sum(y => Math.Pow((y - avg2), 2.0));

                        var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);
                        if (Math.Abs(result) >= Math.Abs(maxcor))
                        {
                            maxcorcol = j;
                            maxcor = result;
                        }
                    }

                }
                if (maxcorcol == -1)
                {
                    maxcorcol = i;
                }
                correlations.Add(i, maxcorcol);
            }
            return correlations;
        }


        public void start()
        {
            new Thread(delegate ()
            {
                int length = dataArray.Length;
                while (time < length && isStop== false)
                {
                    lock (this.measure)
                    {
                        lock (this.corMeasure)
                        {
                            lock (this.correlationPoints)
                            {
                                if (time > 300)
                                {
                                    this.measure.RemoveAt(0);
                                    this.corMeasure.RemoveAt(0);
                                    this.correlationPoints.RemoveAt(0);
                                }
                                this.measure.Add(new DataPoint(time, getValueFromCSV(time, this.title)));
                                this.corMeasure.Add(new DataPoint(time, getValueFromCSV(time, this.corTitle)));
                                this.correlationPoints.Add(new ScatterPoint(getValueFromCSV(time, this.title), getValueFromCSV(time, this.corTitle), dotSize, colorValue));

                            }
                        }

                    }
                    NotifyPropertyChanged("graphsSeriesChanged");
                    time++;
                    while (speed == 0)
                    {

                    }
                    Thread.Sleep((int)(100 / speed));
                }
                stop();
                

            }).Start();
        }
        public void stop()
        {
            isStop = true;
            NotifyPropertyChanged("stopWindow");
        }


    }
}
