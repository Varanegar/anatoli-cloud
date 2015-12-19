using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anatoli.DataAccess.Repositories
{
    public class AnatoliUserStore : UserStore<User, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUserStore<User>, IDisposable

        //IUserStore<User>, IUserPasswordStore<User>, IDisposable
    {
        #region Properties
        UserRepository UserRepository { get; set; }
        #endregion

        #region Ctors
        public AnatoliUserStore(AnatoliDbContext context)
            : base(context)
        {

            UserRepository = new UserRepository(Context);
        }

        //public AnatoliUserStore(AnatoliDbContext dbc)
        //    : this(new UserRepository(dbc))
        //{ }
        //public AnatoliUserStore(UserRepository userRepository)
        //{
        //    UserRepository = userRepository;
        //}
        #endregion

        #region User Store
        public async Task CreateAsync(User user)
        {
            user.CreatedDate = user.LastUpdate = user.LastEntry = DateTime.Now;

            user.PrivateLabelOwner = new PrincipalRepository(Context).FindAsync(p => p.Id == user.PrivateLabelOwner.Id).Result;

            await UserRepository.AddAsync(user);

            await UserRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            await UserRepository.DeleteAsync(user);

            await UserRepository.SaveChangesAsync();
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            var model = await UserRepository.FindAsync(p => p.Id == userId);

            return model;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            var model = await UserRepository.FindAsync(p => p.UserName == userName);

            return model;
        }

        public async Task UpdateAsync(User user)
        {
            UserRepository.EntryModified(user);

            await UserRepository.SaveChangesAsync();
        }
        #endregion

        #region User Password Store
        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task<string>.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task<bool>.FromResult(string.IsNullOrEmpty(user.Password));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;

            return Task.FromResult(0);
        }
        #endregion

        public void Dispose()
        {
            UserRepository.Dispose();

            UserRepository = null;
        }
    }
}
