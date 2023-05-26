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

        private void Game_Click(object sender, RoutedEventArgs e)
        {
            HomePage hp = new();
            hp.Show();
            Close();
        }
    }
}