using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADP2_Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MediaPlayerView : Window
    {
        private MediaPlayerViewModel vm;

        public MediaPlayerView(MediaPlayerViewModel viewmodel)
        {
            vm = viewmodel;
            InitializeComponent();
            vm = viewmodel;
            vm.PropertyChanged+= delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
            DataContext = vm;
            timeSlider.Maximum = (int)vm.numOfRows;
            
        }

        private void NotifyPropertyChanged(string propName) 
        {

            if (String.Compare(propName, "VM_Time") == 0)
            {
                int t = vm.VM_Time;
                this.Dispatcher.Invoke(() =>
                {
                    timeSlider.Value = t;
                    string minutes = ((t / 10) / 60).ToString();
                    minutes = minutes.PadLeft(2, '0');
                    string seconds = ((t / 10) % 60).ToString();
                    seconds = seconds.PadLeft(2, '0');
                    timePresentor.Text = minutes + ":" + seconds;
                });

            }    
            
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)speedBox.SelectedItem;
            string newSpeed = cbi.Content.ToString();
            string [] arr = newSpeed.Split(' ');
            vm.VM_Speed = Double.Parse(arr[1]);
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_Play = true;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_Pause = true;
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
