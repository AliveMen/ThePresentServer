using System.Collections.Generic;
using System.Threading.Tasks;
using ThePresentServer.Data.Entities;

namespace ThePresentServer.Data.Core
{
    public interface IUserService
    {
        Task<UserEntity> AuthenticateAsync(string username, string password);
        IEnumerable<UserEntity> GetAll();
        Task<UserEntity> GetByIdAsync(string id);
        Task<UserEntity> CreateAsync(UserEntity userEntity, string password);
        Task UpdateAsync(UserEntity userEntity, string password = null);
        Task DeleteAsync(string id);

    }
}