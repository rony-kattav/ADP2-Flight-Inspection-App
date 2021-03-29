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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADP2_Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ADP2ViewModel vm;
        bool csvchanged = false;
        bool xmlchanged = false;

        public MainWindow()
        {
            InitializeComponent();
            vm = new MediaPlayerViewModel(new MediaPlayerModel());
            //DataContext = vm;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            csvchanged = true;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            xmlchanged = true;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
           // vm.modelStart();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerView mp = new MediaPlayerView();
            mp.Show();
            this.Close();
        }
    }
}
