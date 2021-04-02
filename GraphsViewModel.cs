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
        public PlotModel MyModel { get; private set; }
        public PlotModel corModel { get; private set; }

        public GraphsViewModel()
        {
            this.MyModel = new PlotModel { Title = "flight data" };

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

    }
}
