using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DronTaxi.Annotations;
using DronTaxi.Database;
using DronTaxi.Models;
using DronTaxi.Static;
using DronTaxi.UI.Windows;
using DronTaxi.Views;
using Microsoft.Win32;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace DronTaxi.UI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private User _loggedUser = AppDb.GetUser(3);

        public User LoggedUser
        {
            get => _loggedUser;
            set
            {
                if (Equals(value, _loggedUser)) return;
                _loggedUser = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Visibility = Visibility.Hidden;

            AuthWindow aw = new AuthWindow();

            aw.ShowDialog();

            if (aw.LoggedUser == null)
                Close();
            
            LoggedUser = aw.LoggedUser;

            Visibility = Visibility.Visible;
        }

        private void CollapseAllGrids()
        {
            GridProfile.Visibility = Visibility.Collapsed;
            GridUsers.Visibility = Visibility.Collapsed;
            GridRoles.Visibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonProfile_OnClick(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();

            GridProfile.Visibility = Visibility.Visible;
        }

        private void ButtonUsers_OnClick(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();

            GridUsers.Visibility = Visibility.Visible;

            DataGridUsers.ItemsSource = AppDb.GetUsers().Select(UserView.FromUser);
        }

        private void ButtonRoles_OnClick(object sender, RoutedEventArgs e)
        {
            CollapseAllGrids();

            GridRoles.Visibility = Visibility.Visible;

            DataGridRoles.ItemsSource = AppDb.GetUserRoles();
        }


        private void ButtonOrders_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonVehicles_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddUser_OnClick(object sender, RoutedEventArgs e)
        {
            EditUserWindow euw = new EditUserWindow()
            {
                Owner = this
            };

            euw.ShowDialog();
        }

        private void MenuItemEditUser_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridUsers.SelectedItem is UserView view)
            {
                EditUserWindow euw = new EditUserWindow(view)
                {
                    Owner = this
                };

                euw.ShowDialog();
            }
        }

        private void ButtonAddRole_OnClick(object sender, RoutedEventArgs e)
        {
            CreateRoleWindow crw = new CreateRoleWindow
            {
                Owner = this
            };

            crw.ShowDialog();
        }

        private void MenuItemRemoveRole_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridRoles.SelectedItem is UserRole role)
            {
                AppDb.RemoveRole(role);

                DataGridRoles.ItemsSource = AppDb.GetUserRoles();
            }
        }

        private void DataGridRoles_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRoles.SelectedItem is UserRole role)
            {
                DataGridRoleFunctions.ItemsSource = new ObservableCollection<SystemFunction>(role.Functions);
            }
        }

        private void ButtonAddFunction_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridRoles.SelectedItem is UserRole role)
            {
                AddFunctionWindow afw = new AddFunctionWindow(role)
                {
                    Owner = this
                };

                afw.ShowDialog();
            }
        }

        private void MenuItemRemoveFunction_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridRoleFunctions.SelectedItem is SystemFunction function && DataGridRoles.SelectedItem is UserRole role)
            {
                AppDb.RemoveFunctionFromRole(role, function);

                role.Update();

                DataGridRoleFunctions.ItemsSource = role.Functions;
            }
        }

    }
}
