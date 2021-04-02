using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;


namespace ADP2_Flight_Inspection_App
{
    public class GraphsViewModel
    {
        private GraphsModel model;
        public PlotModel MyModel { get; private set; }
        public PlotModel corModel { get; private set; }
        public List<string> XML_coloumns_names { get; private set; }
        private string VM_title = "flight data";
        public string VM_Title
        {
            get
            {
                return this.VM_title;
            }
            set
            {
                if (this.VM_title != value)
                {
                    this.VM_title = value;
                }
            }
        }

        public GraphsViewModel(GraphsModel model)
        {
            this.model = model;
            this.MyModel = new PlotModel { Title = VM_title };
            this.XML_coloumns_names = this.model.getColoumnsNames();

            LinearAxis x = new LinearAxis { Title = "time", Position=AxisPosition.Bottom };
            LinearAxis y = new LinearAxis();
            y.Title = "value";
            y.Position = AxisPosition.Left;
            this.MyModel.Axes.Add(x);
            this.MyModel.Axes.Add(y);
            LineSeries lineSeries = new LineSeries();
            lineSeries.Points.Add(new DataPoint(0, 0));
            lineSeries.Points.Add(new DataPoint(10, 20));
            lineSeries.Points.Add(new DataPoint(20, 30));

            this.MyModel.Series.Add(lineSeries);


            this.corModel = new PlotModel { Title = "correlative flight data" };
            this.corModel.Axes.Add(new LinearAxis { Title = "time", Position = AxisPosition.Bottom });
            this.corModel.Axes.Add(new LinearAxis { Title = "value", Position = AxisPosition.Left });

            var rand = new Random();
            List<DataPoint> dataPoints = new List<DataPoint>();
            for(int i=0; i<10; i++)
            {
                dataPoints.Add(new DataPoint(rand.NextDouble(), rand.NextDouble()));
            }

            Func<DataPoint, double> func = point => point.X;

            IOrderedEnumerable<DataPoint> ordered = dataPoints.OrderBy(func);
            
            LineSeries corLineSeries2 = new LineSeries();
            corLineSeries2.Points.AddRange(ordered);
            this.corModel.Series.Add(corLineSeries2);

        }

        public void changeGraphs()
        {
            var rand = new Random();

            this.MyModel.Title = this.VM_title;
            LineSeries lineSeries = new LineSeries();
            lineSeries.Points.Add(new DataPoint(rand.NextDouble()*100, rand.NextDouble()*100));
            lineSeries.Points.Add(new DataPoint(rand.NextDouble()*100, rand.NextDouble()*100));
            lineSeries.Points.Add(new DataPoint(20, 20));
            Func<DataPoint, double> func = point => point.X;

            IOrderedEnumerable<DataPoint> ordered1 = lineSeries.Points.OrderBy(func);
            LineSeries corLineSeries1 = new LineSeries();
            corLineSeries1.Points.AddRange(ordered1);

            this.MyModel.Series.Clear();
            this.MyModel.Series.Add(corLineSeries1);

            
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 0; i < 10; i++)
            {
                dataPoints.Add(new DataPoint(rand.NextDouble(), rand.NextDouble()));
            }

            
            IOrderedEnumerable<DataPoint> ordered = dataPoints.OrderBy(func);

            LineSeries corLineSeries2 = new LineSeries();
            corLineSeries2.Points.AddRange(ordered);
            this.corModel.Series.Clear();
            this.corModel.Series.Add(corLineSeries2);
        }

    }
}
// TODO: if something has changed (like the title or value) represent the graph.
