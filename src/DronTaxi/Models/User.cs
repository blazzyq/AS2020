using System.Collections.Generic;
using System.Linq;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class User : AUpdatableElement
    {
        public int Id { get; }
        public string Username { get; internal set; }
        public string PasswordHash { get; internal set; }

        private UserProfile _profile;

        public UserProfile Profile
        {
            get => _profile ?? (_profile = AppDb.GetUserProfile(this));

            internal set => _profile = value;
        }

        private UserPic _picture;

        public UserPic Picture
        {
            get => _picture ?? (_picture = AppDb.GetUserPic(this));

            internal set => _picture = value;
        }

        private List<UserRole> _roles;

        public List<UserRole> Roles
        {
            get => _roles ?? (_roles = AppDb.GetUserRoles(this).ToList());

            set => _roles = value;
        }

        internal User(int id, string username, string passwordHash)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
        }

        public override void Update()
        {
            User user = AppDb.GetUser(Id);

            if (user == null)
            {
                Exists = false;
                return;
            }

            Username = user.Username;
            PasswordHash = user.PasswordHash;
            Profile = user.Profile;
            Picture = user.Picture;
            Roles = user.Roles;
        }
    }
}
