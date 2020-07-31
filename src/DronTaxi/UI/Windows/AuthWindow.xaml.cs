using System;
using System.Collections.Generic;
using System.Configuration;
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
using DronTaxi.Database;
using DronTaxi.Models;
using DronTaxi.Properties;
using DronTaxi.Static;

namespace DronTaxi.UI.Windows
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow
    {
        public User LoggedUser { get; set; }

        public AuthWindow()
        {
            InitializeComponent();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings["LastUsername"] != null && !string.IsNullOrWhiteSpace(config.AppSettings.Settings["LastUsername"].Value))
            {
                CheckBoxRemember.IsChecked = true;
                TextBoxLogin.Text = config.AppSettings.Settings["LastUsername"].Value;
            }
        }

        private void ButtonEnter_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBoxRemember.IsChecked == true)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings.Remove("LastUsername");
                config.AppSettings.Settings.Add("LastUsername", TextBoxLogin.Text);

                config.Save();
            }

            else
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings.Remove("LastUsername");
                config.AppSettings.Settings.Add("LastUsername", "");

                config.Save();
            }

            User user = AppDb.GetUser(TextBoxLogin.Text);

            if (user == null)
            {
                TextBlockError.Visibility = Visibility.Visible;
                TextBlockError.Text = "(!) Не найден пользователь с таким логином";

                return;
            }

            string hash = PasswordBox.Password.Encrypt(App.Key);

            if (user.PasswordHash == hash)
            {
                LoggedUser = user;

                Close();
            }

            else
            {
                TextBlockError.Visibility = Visibility.Visible;
                TextBlockError.Text = "(!) Введен неверный пароль";
            }
        }
    }
}
