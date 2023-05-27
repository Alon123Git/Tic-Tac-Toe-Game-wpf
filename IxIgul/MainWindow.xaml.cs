using IxIgul.Windows;
using System.Windows;

namespace IxIgul
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void Cp_Click(object sender, RoutedEventArgs e)
        {
            CpLevelsWindow cpl = new();
            cpl.Show();
            Close();
        }

        private void Player2_Click(object sender, RoutedEventArgs e)
        {
            Player2Page p2 = new();
            p2.Show();
            Close();
        }
    }
}