using System.Linq;
using System.Windows;
using DronTaxi.Database;
using DronTaxi.Models;
using DronTaxi.Static;

namespace DronTaxi.UI.Windows
{
    /// <summary>
    /// Interaction logic for AddFunctionWindow.xaml
    /// </summary>
    public partial class AddFunctionWindow
    {
        public UserRole Role { get; set; } 

        public AddFunctionWindow(UserRole role)
        {
            InitializeComponent();

            Role = role;

            ComboBox.ItemsSource = AppDb.GetSystemFunctions().Select(f => f.DisplayName);
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            SystemFunction function = AppDb.GetSystemFunctions().First(r => r.DisplayName == ComboBox.SelectedItem.ToString());

            AppDb.AddFunctionToRole(Role, function);

            Role.Update();

            MainWindow mw = Owner.CastTo<MainWindow>();

            mw.DataGridRoleFunctions.ItemsSource = Role.Functions;

            Close();
        }
    }
}
