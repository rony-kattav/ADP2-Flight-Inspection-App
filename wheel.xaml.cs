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
        private bool WindowisOpen;


        public wheel(WheelViewModel viewmodel)
        {
            vm = viewmodel;
            InitializeComponent();
            DataContext = vm;
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
            WindowisOpen = true;


        }


        private void NotifyPropertyChanged(string propName)
        {
            if (WindowisOpen)
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
                if (String.Compare(propName, "VM_Elevator") == 0 || String.Compare(propName, "VM_Aileron") == 0)
                {
                    double elevator = vm.VM_Elevator;
                    double aileron = vm.VM_Aileron;
                    this.Dispatcher.Invoke(() =>
                    {

                        int radius = 26;
                        //ElivatorView = elivator;
                        //ElivatorView = radius * elivator * 10;

                        Canvas.SetLeft(center_circle, radius * aileron * 3);
                        Canvas.SetTop(center_circle, radius * elevator * 3);

                        //Thickness margin = center_circle.Margin;
                        //double temp = 153 + (e * 70);
                        //center_circle.Margin = new Thickness(center_circle.Margin.Left, temp, 0, 0);
                        //margin.Top += (e * 10);
                        //View_margin.Top += (e*100);
                    });
                }
                if (String.Compare(propName, "VM_Altimeter") == 0)
                {
                    double altimeter = vm.VM_Altimeter;
                    this.Dispatcher.Invoke(() =>
                    {
                        string s = altimeter.ToString();
                        altimeterText.Text = s;
                    });
                }
                if (String.Compare(propName, "VM_AirSpeed") == 0)
                {
                    double airSpeed = vm.VM_AirSpeed;
                    this.Dispatcher.Invoke(() =>
                    {
                        string s = airSpeed.ToString();
                        airSpeedText.Text = s;
                    });
                }
                if (String.Compare(propName, "VM_FlightDirection") == 0)
                {
                    double flightDir = vm.VM_FlightDirection;
                    this.Dispatcher.Invoke(() =>
                    {
                        string s = flightDir.ToString();
                        flightDirectionText.Text = s;
                    });
                }
                if (String.Compare(propName, "VM_Pitch") == 0)
                {
                    double pitch = vm.VM_Pitch;
                    this.Dispatcher.Invoke(() =>
                    {
                        string s = pitch.ToString();
                        pitchText.Text = s;
                    });
                }
                if (String.Compare(propName, "VM_Roll") == 0)
                {
                    double roll = vm.VM_Roll;
                    this.Dispatcher.Invoke(() =>
                    {
                        string s = roll.ToString();
                        rollText.Text = s;
                    });
                }
                if (String.Compare(propName, "VM_Way") == 0)
                {
                    double yaw = vm.VM_Yaw;
                    this.Dispatcher.Invoke(() =>
                    {
                        string s = yaw.ToString();
                        yawText.Text = s;
                    });
                }
                if (String.Compare(propName, "VM_Done") == 0)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        WindowisOpen = false;
                        this.Close();
                    });
                }
            }
        }

        void wheel_Closing(object sender, CancelEventArgs e)
        {
            WindowisOpen = false;

        }


        private void throttle_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void rudder_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

    }

}
