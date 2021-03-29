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

namespace ADP2_Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private string[] CSVArray;
        private string XMLPath;
        public Menu(string[] array , string xmlpath)
        {

            CSVArray = array;
            XMLPath = xmlpath;
            InitializeComponent();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.IsChecked == true)
            {
                var vmmediaControl = new MediaPlayerViewModel(new MediaPlayerModel(CSVArray));
                var mediaControlForm = new MediaPlayerView(vmmediaControl);
                mediaControlForm.Show();
                vmmediaControl.startModel();

            }
            if (graphs.IsChecked == true)
            {
                //var graphForm = new ExampleWin();
                //graphForm.Show();
            }
            if (navigatorControls.IsChecked == true)
            {
                //var navigatorControlForm = new Wheel();
                //navigatorControlForm.Show();
            }
            this.Close();
        }
    }
}
