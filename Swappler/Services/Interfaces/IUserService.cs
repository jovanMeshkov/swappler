using System.Collections.Generic;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.ViewModels;

namespace Swappler.Services.Interfaces
{
    
    public interface IUserService : IService<User, UserStatus>
    {
        bool Remove(string username);

        List<User> AllUsers();

        List<User> FindUserByNameOrSurname(string nameOrLastname);

        User FindUserByUsername(string username);

        User FindUserById(long id);

        /// <summary>
        /// Add and register new user.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <returns>UserStatus</returns>
        UserStatus Register(string firstName, string lastName, string username, string email, string password);

        /// <summary>
        /// Validate entered email or username against the password
        /// </summary>
        /// <param name="emailOrUsername"></param>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns>UserStatus</returns>
        UserStatus ValidateCredentials(string emailOrUsername, string password, out User user);

        UserStatus Update(UserUpdateViewModel userUpdateViewModel);
    }
}
