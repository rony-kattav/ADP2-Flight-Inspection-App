using System;
using System.Collections.Generic;
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
        public string selectedMeasure;
        public Graphs(GraphsViewModel graphsViewModel)
        {
            this.vm = graphsViewModel;
            DataContext = this.vm;
            InitializeComponent();
     
            for (int i=0; i<vm.XML_coloumns_names.Count; i++)
            {
                ListBoxItem item = new ListBoxItem { Height = 70, FontSize = 20 };
                item.Content = vm.XML_coloumns_names.ElementAt(i);
                item.Selected += ListBoxItem_Selected;
                item.Foreground = Brushes.DarkMagenta;
                item.FontStyle = FontStyles.Italic;
                //item.Name = item.Content.ToString();
                //item.Name = vm.XML_coloumns_names.ElementAt(i);
                listBox.Items.Add(item);
            }

            selectedMeasure = listBox.Items.GetItemAt(0).ToString();
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            /*ListBoxItem item = (ListBoxItem)listBox.SelectedItem;
            string title = item.Content.ToString();
            string[] arr = title.Split(' ');
            vm.VM_Title = arr[1];
            Console.WriteLine(arr[1]);*/

        }

        private void ListBox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)listBox.SelectedItem;
            string title = item.Content.ToString();
            vm.VM_Title = title;
            Console.WriteLine(title);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.changeGraphs();
            measure.InvalidatePlot(true);
            corMeasure.InvalidatePlot(true);

        }
    }
}
