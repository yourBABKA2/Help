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

namespace WpfApp2.View
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        string login, password;

        public Authorization()
        {
            InitializeComponent();
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            Helper.MainFrame.Navigate(new Catalog());
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            login = TextBoxLogin.Text;
            password = TextBoxPassword.Text;

            if (login == "" || password == "")
            {
                MessageBox.Show("Не все данные введены");
                return;
            }

            var user = Helper.DbContext.User.Where(x => x.Login == login && x.Password == password).FirstOrDefault();

            if (user != null)
            {
                var role = user.Role.Name.ToString();
                Helper.user = user;
                MessageBox.Show("Вы зашли в систему под ролью: " + role);
                Helper.MainFrame.Navigate(new Catalog());
                return;
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }
        }
    }
}
