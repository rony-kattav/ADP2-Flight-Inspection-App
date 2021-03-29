using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool csvchanged = false;
        bool xmlchanged = false;


        public MainWindow()
        {
            InitializeComponent();
            //vm = new MediaPlayerViewModel(new MediaPlayerModel());
            //DataContext = vm;
        }

        private void CsvBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            csvchanged = true;
        }

        private void XmlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            xmlchanged = true;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
          
            List<string> l = new List<string>();
            // read the CSV into array 
            var reader = new StreamReader(csvText.Text);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                l.Add(line);
            }
            reader.Close();
            string[] dataArray = l.ToArray();


            Menu men = new Menu(dataArray, xmlText.Text);
            men.Show();
            this.Close();
        }


    }
}
