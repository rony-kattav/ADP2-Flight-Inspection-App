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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADP2_Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for PopupDetection.xaml
    /// </summary>
    public partial class PopupDetection : Page
    {
        private PopupDetectionViewModel vm;
        public PopupDetection(PopupDetectionViewModel viewmodel)
        {
            InitializeComponent();
            vm = viewmodel;
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }



        public void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_Alarm") == 0)
            {
               //get feature1, feature2 , anomalyTime from vm and display
               // show a window, wait ~5 sec and then close it.
            }

        }
    }
}
