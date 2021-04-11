using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.ComponentModel;


namespace ADP2_Flight_Inspection_App
{
    public class GraphsViewModel
    {
        private GraphsModel model;
        public PlotModel MyModel { get; private set; }
        public PlotModel corModel { get; private set; }

        public PlotModel correlationModel { get; private set; }
        public List<string> XML_coloumns_names { get; private set; }



        public bool VM_IsStop
        {
            get { return model.IsStop; }
            set
            {
                if (model.IsStop != value)
                {
                    model.IsStop = value;
                }
            }
        }

        public string VM_Feature
        {
            get { return model.Feature; }
            set
            {
                if (model.Feature != value)
                {
                    model.Feature = value;
                }
            }
        }

        public string VM_Title
        {
            get
            {
                return model.Title;
            }
            set
            {
                if (model.Title != value)
                {
                    model.Title = value;
                }
            }
        }

        public string VM_CorTitle
        {
            get
            {
                return model.CorTitle;
            }
            set
            {
                if (model.CorTitle != value)
                {
                    model.CorTitle = value;
                }
            }

        }

        public string VM_CorrelationTitle
        {
            get
            {
                return model.CorrelationTitle;
            }
            set
            {
                if (model.CorrelationTitle != value)
                {
                    model.CorrelationTitle = value;
                }
            }
        }

        public List<DataPoint> VM_Measure
        {
            get
            {
                return model.Measure;
            }
        }

        public List<DataPoint> VM_CorMeasure
        {
            get
            {
                return model.CorMeasure;
            }
        }

        public List<ScatterPoint> VM_CorrelationPoints
        {
            get
            {
                return model.CorrelationPoints;
            }
        }

        public List<DataPoint> VM_CorrelationLineRegPoints
        {
            get
            {
                return model.CorrelationLineRegPoints;
            }
        }

        public int VM_Time
        {
            get
            {
                return model.Time;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_graphsChanged") == 0)
            {
                updateGraphs();
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }

            }
            if (String.Compare(propName, "VM_graphsSeriesChanged") == 0)
            {
                updateSeriesGraphs();
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }

            }
            if (String.Compare(propName, "VM_graphsNamesChanged") == 0)
            {
                updateNameGraphs();
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }

            }
            if (String.Compare(propName, "VM_stopWindow") == 0)
            {

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }

            }
        }


        public GraphsViewModel(GraphsModel model)
        {
            this.model = model;

            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            this.MyModel = createMyModel();
            this.corModel = createCorModel();
            this.correlationModel = createCorrelationModel();
            this.XML_coloumns_names = this.model.getColoumnsNames();


        }

        public void updateGraphs()
        {
            this.MyModel.Title = this.VM_Title;
            this.corModel.Title = this.VM_CorTitle;
            this.correlationModel.Title = this.VM_CorrelationTitle;

            this.MyModel.Series.Clear();
            LineSeries lineSeries = new LineSeries { Color = OxyColors.DarkMagenta };
            lineSeries.Points.AddRange(this.VM_Measure);
            this.MyModel.Series.Add(lineSeries);

            this.corModel.Series.Clear();
            LineSeries corlineSeries = new LineSeries { Color = OxyColors.LightPink };
            corlineSeries.Points.AddRange(this.VM_CorMeasure);
            this.corModel.Axes.ElementAt(0).AbsoluteMinimum = Math.Max(VM_Time - 300, 0);
            this.corModel.Axes.ElementAt(0).AbsoluteMaximum = VM_Time;
            this.corModel.Series.Add(corlineSeries);

            this.correlationModel.Series.Clear();
            ScatterSeries scatterSeries;
            scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.DarkSalmon, MarkerFill = OxyColors.DeepPink, MarkerStrokeThickness = 1 };
            lock (scatterSeries)
            {
                scatterSeries.Points.AddRange(this.VM_CorrelationPoints);
                this.correlationModel.Series.Add(scatterSeries);
            }
            LineSeries linearReg = new LineSeries { Color = OxyColors.LightSalmon, StrokeThickness = 1 };
            lock (linearReg)
            {
                linearReg.Points.AddRange(this.VM_CorrelationLineRegPoints);
                this.correlationModel.Series.Add(linearReg);
            }
            this.correlationModel.Axes.Clear();
            this.correlationModel.Axes.Add(new LinearAxis { Title = VM_Title, Position = AxisPosition.Bottom });
            this.correlationModel.Axes.Add(new LinearAxis { Title = VM_CorTitle, Position = AxisPosition.Left });
        }

        public void updateSeriesGraphs()
        {
            lock (this.MyModel)
            {
                this.MyModel.Series.Clear();
                LineSeries lineSeries = new LineSeries { Color = OxyColors.DarkMagenta };
                lineSeries.Points.AddRange(this.VM_Measure);
                this.MyModel.Series.Add(lineSeries);
            }
            lock (this.corModel)
            {
                this.corModel.Series.Clear();
                LineSeries corlineSeries = new LineSeries { Color = OxyColors.LightPink };
                corlineSeries.Points.AddRange(this.VM_CorMeasure);
                this.corModel.Axes.ElementAt(0).AbsoluteMinimum = Math.Max(VM_Time - 300, 0);
                this.corModel.Axes.ElementAt(0).AbsoluteMaximum = VM_Time;
                this.corModel.Series.Add(corlineSeries);
            }
            lock (this.correlationModel)
            {
                this.correlationModel.Series.Clear();
                ScatterSeries scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.DarkSalmon, MarkerFill = OxyColors.DeepPink, MarkerStrokeThickness = 1 };
                scatterSeries.Points.AddRange(this.VM_CorrelationPoints);
                this.correlationModel.Series.Add(scatterSeries);
                LineSeries linearReg = new LineSeries { Color = OxyColors.LightSalmon, StrokeThickness = 1 };
                linearReg.Points.AddRange(this.VM_CorrelationLineRegPoints);
                this.correlationModel.Series.Add(linearReg);
            }
        }

        public void updateNameGraphs()
        {
            this.MyModel.Title = this.VM_Title;
            this.corModel.Title = this.VM_CorTitle;
            this.correlationModel.Title = this.VM_CorrelationTitle;
            this.correlationModel.Axes.Clear();
            this.correlationModel.Axes.Add(new LinearAxis { Title = VM_Title, Position = AxisPosition.Bottom });
            this.correlationModel.Axes.Add(new LinearAxis { Title = VM_CorTitle, Position = AxisPosition.Left });

        }

            private PlotModel createMyModel()
        {
            var plot = new PlotModel { Title = VM_Title, TitleClippingLength = 1 };
            LinearAxis x = new LinearAxis { Title = "time", Position = AxisPosition.Bottom, Unit = "100ms" };
            LinearAxis y = new LinearAxis();
            y.Title = "value";
            y.Position = AxisPosition.Left;
            plot.Axes.Add(x);
            plot.Axes.Add(y);

            LineSeries lineSeries = new LineSeries { Color = OxyColors.DarkMagenta };
            lineSeries.Points.AddRange(this.VM_Measure);
            plot.Series.Add(lineSeries);
            return plot;
        }

        private PlotModel createCorModel()
        {
            var plot = new PlotModel { Title = VM_CorTitle, TitleClippingLength = 1 };
            plot.Axes.Add(new LinearAxis { Key = "timeAxis", Title = "time", Position = AxisPosition.Bottom, Minimum = Math.Max(VM_Time - 300, 0), Unit = "100ms" });
            plot.Axes.Add(new LinearAxis { Key = "valueAxis", Title = "value", Position = AxisPosition.Left });

            LineSeries lineSeries = new LineSeries { Color = OxyColors.LightPink };
            lineSeries.Points.AddRange(this.VM_CorMeasure);
            plot.Series.Add(lineSeries);
            return plot;
        }


        private PlotModel createCorrelationModel()
        {
            var model = new PlotModel { Title = VM_CorrelationTitle, TitleClippingLength = 1 };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            var lineSeries = new LineSeries { Color = OxyColors.LightSalmon, StrokeThickness = 1 };

            scatterSeries.Points.AddRange(VM_CorrelationPoints);
            lineSeries.Points.AddRange(VM_CorrelationLineRegPoints);

            model.Series.Add(scatterSeries);
            model.Series.Add(lineSeries);
            model.Axes.Add(new LinearAxis { Title = VM_Title, Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Title = VM_CorTitle, Position = AxisPosition.Left });
            return model;

        }
    }
}