using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePresentServer.Data.Core;
using ThePresentServer.Data.Core.Common;
using ThePresentServer.Data.Entities;
using ThePresentServer.Data.Helpers;

namespace ThePresentServer.Data.Service
{
    public class UserService : IUserService
    {
        private readonly Func<IPresentRepository> _repositoryFunc;

        public UserService(Func<IPresentRepository> repositoryFunc)
        {
            _repositoryFunc = repositoryFunc;
        }

        public async Task<UserEntity> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            using (var repository = _repositoryFunc())
            {
                var user  =  await repository.Users.SingleOrDefaultAsync(x => x.Username == username);
                // check if username exists
                if (user == null)
                    return null;

                // check if password is correct
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                // authentication successful
                return user;
            }

        }

        public IEnumerable<UserEntity> GetAll()
        {

            using (var repository = _repositoryFunc())
            {
                return repository.Users.ToArray();
            }
        }

        public async Task<UserEntity> GetByIdAsync(string id)
        {
            using (var repository = _repositoryFunc())
            {
                return await repository.Users.FirstOrDefaultAsync(x=>x.Id == id);

            }

        }

        public async Task<UserEntity> CreateAsync(UserEntity userEntity, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");


            using (var repository = _repositoryFunc())
            {
                if (repository.Users.Any(x => x.Username == userEntity.Username))
                    throw new AppException("Username \"" + userEntity.Username + "\" is already taken");

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                userEntity.PasswordHash = passwordHash;
                userEntity.PasswordSalt = passwordSalt;

                repository.Add(userEntity);
                await repository.SaveChangesAsync();

                return userEntity;

            }


        }

        public async Task UpdateAsync(UserEntity userEntityParam, string password = null)
        {

            var user = await GetByIdAsync(userEntityParam.Id);

            if (user == null)
                throw new AppException("User not found");




            using (var repository = _repositoryFunc())
            {

                repository.Update(user);
                await repository.SaveChangesAsync();

            }

        }

        public async Task DeleteAsync(string id)
        {
            using (var repository = _repositoryFunc())
            {
                var user = await GetByIdAsync(id);
                if (user != null)
                {
                    repository.Remove(user);
                    await repository.SaveChangesAsync();
                }

            }

        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}