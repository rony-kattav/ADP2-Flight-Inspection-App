using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ADP2_Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wheel : Window
    {
        private WheelViewModel vm;
        /*
        public Thickness View_margin
        {
            get { return center_circle.Margin; }
            set { center_circle.Margin = value; }
        }
        */

        public wheel(WheelViewModel viewmodel)
        {
            vm = viewmodel;
            InitializeComponent();
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
            
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_Throttle") == 0)
            {
                double d = vm.VM_Throttle;
                this.Dispatcher.Invoke(() =>
                {
                    throttle_slider.Value = d;
                });
            }
            if (String.Compare(propName, "VM_Rudder") == 0)
            {
                double r = vm.VM_Rudder;
                this.Dispatcher.Invoke(() =>
                {
                    rudder_slider.Value = r;
                });
            }
            if (String.Compare(propName, "VM_Elevator") == 0)
            {
                double e = vm.VM_Elevator;
                this.Dispatcher.Invoke(() =>
                {
                    int radius = 26;
                    //Canvas.SetTop(circle, radius * e * 10);
                    
                    Canvas canvas = new Canvas();
                    Ellipse el = center_circle;
                    //el.Fill = new SolidColorBrush(Colors.Orange);
                    //el.Width = 52;
                    //el.Height = 52;
                    Canvas.SetTop(canvas, radius * e * 10);
                    canvas.Children.Add(el);
                    

                    //Thickness margin = center_circle.Margin;
                    //double temp = 153 + (e * 70);
                    //center_circle.Margin = new Thickness(center_circle.Margin.Left, temp, 0, 0);
                    //margin.Top += (e * 10);
                    //View_margin.Top += (e*100);
                });
            }
            if (String.Compare(propName, "VM_Aileron") == 0)
            {
                double a = vm.VM_Aileron;
                this.Dispatcher.Invoke(() =>
                {
                    int radius = 26;
                    //Canvas.SetLeft(circle, radius * a * 10);
                    
                    Canvas canvas = new Canvas();
                    Ellipse el = center_circle;
                    //el.Fill = new SolidColorBrush(Colors.Orange);
                    //el.Width = 52;
                    //el.Height = 52;
                    Canvas.SetLeft(canvas, radius * a * 10);
                    canvas.Children.Add(el);
                    
                    //Thickness margin = center_circle.Margin;
                    //double temp = 366 + (a * 70);
                    //center_circle.Margin = new Thickness(temp, center_circle.Margin.Top, 0, 0);
                    //margin.Left += (a * 10);
                    //margin.Left += (a*100);
                });
            }
        }


        private void throttle_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void rudder_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

    }

}
