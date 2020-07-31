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
using DronTaxi.Database;
using DronTaxi.Static;
using DronTaxi.Views;

namespace DronTaxi.UI.Windows
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow
    {
        public UserView View { get; set; }

        public EditUserWindow(UserView view = null)
        {
            InitializeComponent();

            View = view ?? new UserView();
        }

        private void EditUserWindow_OnClosed(object sender, EventArgs e)
        {
            MainWindow mw = Owner.CastTo<MainWindow>();

            mw.DataGridUsers.ItemsSource = AppDb.GetUsers().Select(UserView.FromUser);
        }
    }
}
