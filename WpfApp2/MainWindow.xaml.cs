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
using WpfApp2.Entities;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new View.Authorization());
            Helper.MainFrame = MainFrame;

            Helper.ImportPhoto();          
        }

        private void FremeRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnExit.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnBack.Visibility = Visibility.Collapsed;
                BtnExit.Visibility = Visibility.Visible;
            }

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Вы точно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                Application.Current.Shutdown();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Вы точно хотите выйти из аккаунта", "Выход из аккаунта", MessageBoxButton.YesNo, MessageBoxImage.Question)) MainFrame.GoBack();
        }

        private void FrameNav_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page page) PageTitle.Content = page.Title;
        }
    }
}
