using IxIgul.Windows;
using System.Windows;

namespace IxIgul
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constructor
        public MainWindow()
        {
            InitializeComponent();           
        }
        #endregion

        #region buttons events
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
        #endregion
    }
}