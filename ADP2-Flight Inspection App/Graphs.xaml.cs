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
using OxyPlot;
using OxyPlot.Series;

namespace ADP2_Flight_Inspection_App
{


    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : Window
    {
        private GraphsViewModel vm;
   
        public Graphs(GraphsViewModel graphsViewModel)
        {
            this.vm = graphsViewModel;
            DataContext = this.vm;
            InitializeComponent();
            vm.PropertyChanged+=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    NotifyPropertyChanged(e.PropertyName);
                };

            for (int i=0; i<vm.XML_coloumns_names.Count; i++)
            {
                ListBoxItem item = new ListBoxItem { Height = 70, FontSize = 20 };
                item.Content = vm.XML_coloumns_names.ElementAt(i);
                item.Foreground = Brushes.DarkMagenta;
                item.FontStyle = FontStyles.Italic;
                listBox.Items.Add(item);
            }
            
        }

        private void ListBox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)listBox.SelectedItem;
            string title = item.Content.ToString();
            vm.VM_Feature = title;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_graphsNamesChanged") == 0 || String.Compare(propName, "VM_graphsSeriesChanged") == 0)
            {
                measure.InvalidatePlot(true);
                corMeasure.InvalidatePlot(true);
                correlationGraph.InvalidatePlot(true);
            }
            if (String.Compare(propName, "VM_stopWindow") == 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Close();

                });
                
            }

        }
    }
}
