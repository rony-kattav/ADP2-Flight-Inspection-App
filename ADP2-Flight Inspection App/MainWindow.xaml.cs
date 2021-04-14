using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
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
        string dllpath;

        public MainWindow()
        {
            InitializeComponent();
            //vm = new MediaPlayerViewModel(new MediaPlayerModel());
            //DataContext = vm;
        }
       
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (xmlchanged && csvchanged)
            {

                Menu men = new Menu(CSVText.Text, XMLText.Text);
                men.Show();
                this.Close();
            }
        }
        

        private void CSV_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == true)
            {
                CSVText.Text = file.FileName;
            }
            csvchanged = true;
        }

        private void XML_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (file.ShowDialog() == true)
            {
                XMLText.Text = file.FileName;
            }
            xmlchanged = true;
        }

        private void Instructions_file_Click(object sender, RoutedEventArgs e)
        {
            string inst = "Hello!\n" +
                "First, please select the pathes of the CSV anomaly flight and the XML flight.\n" +
                "Then, you can choose which data you want to see: MediaPlayer, WheelControl, graphs and detections.\n" +
                "Be adviced! If you want to see the detections of your anomaly flight, you need to select the pathes " +
                "of a regular flight and an alghorithem for the detections.\n" +
                "Enjoy your flight (:";
            MessageBox.Show(inst);
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            filesInstruction.Visibility = Visibility.Collapsed;
            finish.Visibility = Visibility.Collapsed;
            headline.Content = "Please select the file's path";
            CSV_file.Visibility = Visibility.Visible;
            XML_file.Visibility = Visibility.Visible;
            CSVText.Visibility = Visibility.Visible;
            XMLText.Visibility = Visibility.Visible;
            Apply.Visibility = Visibility.Visible;
            instructions.Visibility = Visibility.Visible;
        }
    }
}
