using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using DronTaxi.Annotations;
using DronTaxi.Database;
using DronTaxi.Models;
using DronTaxi.Static;
using DronTaxi.UI.Windows;
using DronTaxi.Views;
using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace DronTaxi.UI.UserControls
{
    /// <summary>
    /// Interaction logic for EditUserControl.xaml
    /// </summary>
    public partial class EditUserControl : INotifyPropertyChanged
    {
        public UserView UserView
        {
            get => (UserView)GetValue(UserViewProperty);
            set => SetValue(UserViewProperty, value);
        }

        // Using a DependencyProperty as the backing store for UserView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserViewProperty =
            DependencyProperty.Register("UserView", typeof(UserView), typeof(EditUserControl), 
                new PropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EditUserControl control)
            {
                control.OnPropertyChanged(e.Property.Name);
            }
        }

        public EditUserControl()
        {
            InitializeComponent();
        }

        private void ButtonPersonalEdit_OnClick(object sender, RoutedEventArgs e)
        {
            GridPersonal.Visibility = Visibility.Collapsed;
            GridPersonalEdit.Visibility = Visibility.Visible;

            PasswordBoxEdit.Password = "11111111";
            PasswordBoxEditConfirm.Password = "22222222";
        }

        private void ButtonPersonalEditSave_OnClick(object sender, RoutedEventArgs e)
        {
            GridPersonal.Visibility = Visibility.Visible;
            GridPersonalEdit.Visibility = Visibility.Collapsed;

            if (PasswordBoxEdit.Password != "11111111" && PasswordBoxEditConfirm.Password != "22222222"
                                                       && PasswordBoxEdit.Password == PasswordBoxEditConfirm.Password)
            {
                if (UserView != null)
                {
                    UserView.PasswordHash = PasswordBoxEdit.Password.Encrypt(App.Key);
                }
            }

            UserView?.InsertOrUpdate();
        }

        private void ButtonPersonalEditCancel_OnClick(object sender, RoutedEventArgs e)
        {
            GridPersonal.Visibility = Visibility.Visible;
            GridPersonalEdit.Visibility = Visibility.Collapsed;

            UserView?.Reload();
        }

        private void ButtonPersonalPhotoEdit_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Выберите изображение",
                ReadOnlyChecked = true
            };

            if (ofd.ShowDialog() == true && !string.IsNullOrWhiteSpace(ofd.FileName))
            {
                byte[] bytes = File.ReadAllBytes(ofd.FileName);

                if (UserView != null)
                {
                    UserView.Picture = bytes;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DataGridRoles_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRoles.SelectedItem is UserRole role)
            {
                DataGridRoleFunctions.ItemsSource = new ObservableCollection<SystemFunction>(role.Functions);
            }
        }

        private void ButtonAddRole_OnClick(object sender, RoutedEventArgs e)
        {
            AddRoleWindow arw = new AddRoleWindow(UserView)
            {
                Owner = this.TryFindParent<MainWindow>()
            };

            arw.ShowDialog();
        }

        private void MenuItemRemoveRole_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridRoles.SelectedItem is UserRole role)
            {
                AppDb.RemoveRoleFromUser(UserView.User, role);
                UserView.Reload();
            }
        }
    }
}
