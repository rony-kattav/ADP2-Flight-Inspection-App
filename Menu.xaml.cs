using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace ADP2_Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private string[] CSVArray;
        private string XMLPath;
        private string CSVPath;
        private string DLLpath;
        private bool dllPathChanged;
        private string Reg_Flightpath;
        private bool regPathChanged;

        private List<IADP2Model> models;

        private double speed;
        public double Speed
        {
            get { return speed; }

        }

        private int time;
        public int Time
        {
            get { return time; }

        }


        public Menu(string csvPath , string xmlpath)
        {

            readCSV(csvPath);
            XMLPath = xmlpath;
            CSVPath = csvPath;
            dllPathChanged = false;
            regPathChanged = false;
            InitializeComponent();
            updateDLLOptions();
            models = new List<IADP2Model>();
            ADP2myModel fgmodel = new ADP2myModel(CSVArray,xmlpath, this);
            models.Add(fgmodel);


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void updateDLLOptions()
        {
            List<string> dllOptions = new List<string>();

            dllOptions.Add("Linear Regression Detector");
            dllOptions.Add("Inner Circle Detector");
            dllOptions.Add("Add..");

            DLLlist.ItemsSource = dllOptions;


        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {


        }



        public void NotifyPropertyChanged(Object sender , string propName)
        {
            // if the media player notifies that the time/speed changed, notify to all listeners. 
            if(String.Compare(propName, "MP_Speed") == 0)
            {
                speed = (sender as MediaPlayerModel).Speed;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Speed"));
                }
            }

            else if (String.Compare(propName, "MP_Time") == 0)
            {
                time = (sender as MediaPlayerModel).Time;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Time"));
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.IsChecked == true)
            {
                var modelmedia = new MediaPlayerModel(CSVArray);
                var vmmediaControl = new MediaPlayerViewModel(modelmedia);
                var mediaControlForm = new MediaPlayerView(vmmediaControl);
                modelmedia.PropertyChanged += delegate (Object s, PropertyChangedEventArgs entry) {
                    NotifyPropertyChanged(s, "MP_" + entry.PropertyName);
                };
                models.Add(modelmedia);
                mediaControlForm.Show();
                //vmmediaControl.startModel();

            }
            if (graphs.IsChecked == true)
            {
                var grapgmodel = new GraphsModel(CSVArray, XMLPath, this);
                models.Add(grapgmodel);
                var graphForm = new Graphs(new GraphsViewModel(grapgmodel));
                graphForm.Show();
            }
            if (navigatorControls.IsChecked == true)
            {
                var modelnavigatorControl = new WheelModel(CSVArray, XMLPath , this);
                var vmnavigatorControl = new WheelViewModel(modelnavigatorControl);
                var navigatorControlForm = new wheel(vmnavigatorControl);
                
                models.Add(modelnavigatorControl);
                navigatorControlForm.Show();

            }
            if(AnomaliesDetecting.IsChecked == true && dllPathChanged && regPathChanged)
            {
                PopupDetectionModel pdmodel = new PopupDetectionModel(CSVArray, XMLPath, this, DLLpath , Reg_Flightpath, CSVPath);
                PopupDetectionViewModel vmpop = new PopupDetectionViewModel(pdmodel);
                models.Add(pdmodel);
                PopupDetection pview = new PopupDetection(vmpop);
                pview.Show();
                

            }


            this.Close();
            foreach( var model in models)
            {
                model.connect();
            }
            foreach (var model in models)
            {
                model.start();
            }

        }

        private void AnomalyDetecting_checked(object sender, RoutedEventArgs e)
        {
            DLLlist.Visibility = Visibility.Visible;
            ChooseDLLLabel.Visibility = Visibility.Visible;
            RegFlight.Visibility = Visibility.Visible;
        }

        private void AnomalyDetecting_unchecked(object sender, RoutedEventArgs e)
        {
            DLLlist.Visibility = Visibility.Collapsed;
            ChooseDLLLabel.Visibility = Visibility.Collapsed;
            RegFlight.Visibility = Visibility.Collapsed;

        }

        private void DLL_file_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == true)
            {
                DLLpath = file.FileName;
                if (File.Exists(DLLpath))
                {
                    dllPathChanged = true;
                }
            }


        }

        private void Reg_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == true)
            {
                Reg_Flightpath = file.FileName;
                if (File.Exists(Reg_Flightpath))
                {
                    regPathChanged = true;
                }
                else
                {
                    regPathChanged = false;
                }
            }
        }

        private void readCSV(string path)
        {
            
                List<string> l = new List<string>();
                // read the CSV into array 
                var reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    l.Add(line);
                }
                reader.Close();
                CSVArray = l.ToArray();
        }

        private void DLLlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBoxItem cbi = (ComboBoxItem)DLLlist.SelectedItem;
            string selection = DLLlist.SelectedItem.ToString();
            if(String.Compare(selection, "Linear Regression Detector") == 0)
            {
                DirectoryInfo parenddir = Directory.GetParent(Directory.GetCurrentDirectory());
                while (String.Compare(parenddir.Name, "ADP2-Flight Inspection App") != 0)
                {
                    parenddir = Directory.GetParent(parenddir.FullName);
                }

                DLLpath = parenddir.FullName + "\\plugins\\Anomaly_Detector_DLL.dll";
                dllPathChanged = true;
            }
            else if(String.Compare(selection, "Inner Circle Detector") == 0)
            {
                DirectoryInfo parenddir = Directory.GetParent(Directory.GetCurrentDirectory());
                while (String.Compare(parenddir.Name, "ADP2-Flight Inspection App") != 0)
                {
                    parenddir = Directory.GetParent(parenddir.FullName);
                }

                DLLpath = parenddir.FullName + "\\plugins\\Anomaly_Detector_Circle_DLL.dll";
                dllPathChanged = true;
            }
            else if (String.Compare(selection, "Add..") == 0)
            {
                OpenFileDialog file = new OpenFileDialog();
                if (file.ShowDialog() == true)
                {
                    DLLpath = file.FileName;
                    if (File.Exists(Reg_Flightpath))
                    {
                        dllPathChanged = true;

                    }
                    else
                    {
                        dllPathChanged = false;

                    }
                }
            }
        }
    }
}
