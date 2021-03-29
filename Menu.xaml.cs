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

namespace ADP2
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.IsChecked == true)
            {
                var mediaControlForm = new ExampleWin();
                mediaControlForm.Show();
            }
            if (graphs.IsChecked == true)
            {
                var graphForm = new ExampleWin();
                graphForm.Show();
            }
            if (navigatorControls.IsChecked == true)
            {
                var navigatorControlForm = new ExampleWin();
                navigatorControlForm.Show();
            }
        }
    }
}
