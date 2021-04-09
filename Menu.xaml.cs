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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private string[] CSVArray;
        private string XMLPath;

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


        public Menu(string[] array , string xmlpath, string dll)
        {

            CSVArray = array;
            XMLPath = xmlpath;
            InitializeComponent();
            models = new List<IADP2Model>();
            ADP2myModel fgmodel = new ADP2myModel(array,xmlpath, this);
            PopupDetectionModel pdmodel = new PopupDetectionModel(array,xmlpath,this,dll);
            PopupDetectionViewModel vmpop = new PopupDetectionViewModel(pdmodel);
            
            models.Add(fgmodel);
            models.Add(pdmodel);

        }

        public event PropertyChangedEventHandler PropertyChanged;


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
                //var graphForm = new ExampleWin();
                //graphForm.Show();
            }
            if (navigatorControls.IsChecked == true)
            {
                var modelnavigatorControl = new WheelModel(CSVArray, XMLPath , this);
                var vmnavigatorControl = new WheelViewModel(modelnavigatorControl);
                var navigatorControlForm = new wheel(vmnavigatorControl);
                
                models.Add(modelnavigatorControl);
                //navigatorControlForm.Show();


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
    }
}
