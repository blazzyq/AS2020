using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DronTaxi.Annotations;
using DronTaxi.Database;
using DronTaxi.Models;

namespace DronTaxi.Views
{
    public class UserView : INotifyPropertyChanged
    {
        private string _username;
        private string _passwordHash;
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private DateTime? _birthDate;
        private int? _sex;
        private string _email;
        private string _phone;
        private byte[] _picture;
        private ObservableCollection<UserRole> _roles;

        public int Id { get; set; }

        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (value == _passwordHash) return;
                _passwordHash = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (value == _middleName) return;
                _middleName = value;
                OnPropertyChanged();
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                if (value.Equals(_birthDate)) return;
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public int? Sex
        {
            get => _sex;
            set
            {
                if (value == _sex) return;
                _sex = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone) return;
                _phone = value;
                OnPropertyChanged();
            }
        }

        public byte[] Picture
        {
            get => _picture;
            set
            {
                if (Equals(value, _picture)) return;
                _picture = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserRole> Roles
        {
            get => _roles;
            set
            {
                if (Equals(value, _roles)) return;
                _roles = value;
                OnPropertyChanged();
            }
        }

        public User User { get; set; }

        public static UserView FromUser(User user)
        {
            if (user == null)
                return null;

            UserView view = new UserView
            {
                User = user,
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                LastName = user.Profile?.LastName,
                FirstName = user.Profile?.FirstName,
                MiddleName = user.Profile?.MiddleName,
                BirthDate = user.Profile?.BirthDate,
                Sex = user.Profile?.Sex,
                Email = user.Profile?.Email,
                Phone = user.Profile?.Phone,
                Picture = user.Picture?.Picture,
                Roles = new ObservableCollection<UserRole>(user.Roles)
            };

            return view;
        }

        public void InsertOrUpdate()
        {
            if (User == null)
            {
                User = AppDb.CreateUser(Username ?? Email, PasswordHash, LastName, FirstName, MiddleName, BirthDate ?? DateTime.Now, Sex ?? 1, Email,
                    Phone, Picture);

                Id = User.Id;

                if (Roles == null)
                    Roles = new ObservableCollection<UserRole>();

                foreach (UserRole role in Roles)
                {
                    AppDb.AddRoleToUser(User, role);
                }
            }

            else
            {
                AppDb.UpdateUser(Id, LastName, FirstName, MiddleName, BirthDate ?? DateTime.Now, Sex ?? 1, Email, Phone, PasswordHash, Picture);

                if (Roles == null)
                    Roles = new ObservableCollection<UserRole>();

                foreach (UserRole role in Roles)
                {
                    AppDb.AddRoleToUser(User, role);
                }

                Reload();
            }
        }

        public void Reload()
        {
            if (User != null)
            {
                User.Update();

                Username = User.Username;
                PasswordHash = User.PasswordHash;
                LastName = User.Profile?.LastName;
                FirstName = User.Profile?.FirstName;
                MiddleName = User.Profile?.MiddleName;
                BirthDate = User.Profile?.BirthDate;
                Sex = User.Profile?.Sex;
                Email = User.Profile?.Email;
                Phone = User.Profile?.Phone;
                Picture = User.Picture?.Picture;
                Roles = new ObservableCollection<UserRole>(User.Roles);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
