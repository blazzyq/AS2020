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

namespace DronTaxi.UI.Windows
{
    /// <summary>
    /// Interaction logic for CreateRoleWindow.xaml
    /// </summary>
    public partial class CreateRoleWindow
    {
        public CreateRoleWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            AppDb.CreateRole(TextBoxName.Text, TextBoxDisplayName.Text);

            MainWindow mw = Owner.CastTo<MainWindow>();

            mw.DataGridRoles.ItemsSource = AppDb.GetUserRoles();

            Close();
        }
    }
}
