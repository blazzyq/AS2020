using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class UserPic : AUpdatableElement
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

        public byte[] Picture { get; internal set; }

        public UserPic(int id, int userId, byte[] picture)
        {
            Id = id;
            _userId = userId;
            Picture = picture;
        }

        public override void Update()
        {
            UserPic userPic = AppDb.GetUserPic(Id);

            if (userPic == null)
            {
                Exists = false;
                return;
            }

            User = userPic.User;
            Picture = userPic.Picture;
        }
    }
}
