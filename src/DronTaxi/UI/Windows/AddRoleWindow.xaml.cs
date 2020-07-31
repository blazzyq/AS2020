using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DronTaxi.Views;

namespace DronTaxi.UI.Windows
{
    /// <summary>
    /// Interaction logic for AddRoleWindow.xaml
    /// </summary>
    public partial class AddRoleWindow
    {
        public List<UserRole> Roles { get; set; } = AppDb.GetUserRoles().ToList();

        public UserView View { get; set; }

        public AddRoleWindow(UserView view)
        {
            InitializeComponent();

            View = view;

            ComboBox.ItemsSource = Roles.Select(r => r.DisplayName);
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            UserRole role = Roles.First(r => r.DisplayName == ComboBox.SelectedItem.ToString());

            if (View.Roles == null)
                View.Roles = new ObservableCollection<UserRole>();

            if (!View.Roles.Contains(role))
                View.Roles.Add(role);

            Close();
        }
    }
}
