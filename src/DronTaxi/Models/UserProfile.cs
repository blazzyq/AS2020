using System;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;
using DronTaxi.Static;

namespace DronTaxi.Models
{
    public class UserProfile : AUpdatableElement
    {
        public int Id { get; }

        private int? _userId;
        private User _user;

        public User User
        {
            get => _user ?? (_user = AppDb.GetUser(_userId));

            internal set
            {
                _user = value;
                _userId = value?.Id;
            }
        }

        public string LastName { get; internal set; }
        public string FirstName { get; internal set; }
        public string MiddleName { get; internal set; }
        public DateTime BirthDate { get; internal set; }
        public int Sex { get; internal set; }
        public string Email { get; internal set; }
        public string Phone { get; internal set; }

        internal UserProfile(int id, int userId, string lastName, string firstName, string middleName,
            DateTime birthDate, int sex, string email, string phone)
        {
            Id = id;
            _userId = userId;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDate = birthDate;
            Sex = sex;
            Email = email;
            Phone = phone;
        }

        public override void Update()
        {
            UserProfile userProfile = AppDb.GetUserProfile(Id);

            if (userProfile == null)
            {
                Exists = false;
                return;
            }

            User = userProfile.User;
            LastName = userProfile.LastName;
            FirstName = userProfile.FirstName;
            MiddleName = userProfile.MiddleName;
            BirthDate = userProfile.BirthDate;
            Sex = userProfile.Sex;
            Email = userProfile.Email;
            Phone = userProfile.Phone;
        }
    }
}