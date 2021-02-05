using System.Collections.ObjectModel;
using ThePresentServer.Data.Core.Common;

namespace ThePresentServer.Data.Entities
{
    public class UserEntity : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string ParentUserId { get; set; }
        public UserEntity ParentUser { get; set; }

        public ObservableCollection<PresentEntity> Presents { get; set; } = new NullCollection<PresentEntity>();
        public ObservableCollection<UserEntity> Friends { get; set; } = new NullCollection<UserEntity>();
    }
}