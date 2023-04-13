using Dosug.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Dosug.View
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private TimeSpan _hold;
        private DispatcherTimer _timer;
        private string _captcha;
        private string _password;

        public AuthPage() => InitializeComponent();

        private void OnLoginBtnClick(object sender, RoutedEventArgs e)
        {
            bool captchaValid = ValidateCaptcha();
            bool userValid = ValidateUser();

            if (captchaValid && userValid)
            {
                _captcha = string.Empty;
                Captcha.Visibility = Visibility.Collapsed;
                CaptchaField.Visibility = Visibility.Collapsed;
                AuthUniformGrid.Rows = 3;

                Helper.MainWindow.RoleTitle.Content = $"Вы зашли как: {Helper.LoginUser.Role.Name}";
                Helper.NavFrame.Navigate(new EventPage());
            }
            else
            {
                _captcha = Helper.GenerateCaptcha(6);
                Captcha.Content = _captcha;
                MessageBox.Show("Подьзователь не найден! Проверьте корректность введенных данных!",
                    "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);

                Captcha.Visibility = Visibility.Visible;
                CaptchaField.Visibility = Visibility.Visible;
                AuthUniformGrid.Rows = 4;

                LoginBtn.IsEnabled = false;
                _hold = TimeSpan.FromSeconds(5);
                _timer = new DispatcherTimer();
                _timer.Tick += new EventHandler(OnTimerTick);
                _timer.Interval = new TimeSpan(0, 0, 1);
                _timer.Start();
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _hold = _hold.Subtract(TimeSpan.FromSeconds(1));
            LoginBtnTxt.Content = $"Вы заблокированы: {_hold.TotalSeconds}";
            if (_hold.TotalSeconds == 0)
            {
                _timer.Stop();
                LoginBtnTxt.Content = "Войти";
                LoginBtn.IsEnabled = true;
            }
        }

        private bool ValidateCaptcha()
        {
            if (string.IsNullOrEmpty(_captcha) || _captcha == CaptchaField.Text)
            {
                return true;
            }

            return false;
        }

        private bool ValidateUser()
        {
            var users = Helper.Database.User.ToList();
            Helper.SafeInvoke(() => {
                Helper.LoginUser = Helper.Database.User.Where(x => x.Login == LoginField.Text
                 && x.Password == _password).FirstOrDefault();
            });

            return Helper.LoginUser != null;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _password = passwordBox.Password;
            }
        }

        private void OnGuestLoginBtnClick(object sender, RoutedEventArgs e)
        {
            Helper.MainWindow.RoleTitle.Content = "Вы зашли как: гость";
            Helper.NavFrame.Navigate(new EventPage());
        }
    }
}

