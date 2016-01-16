using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Swappler.Database;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Services.Interfaces;
using Swappler.Utilities;

namespace Swappler.Services
{
    /// <summary>
    /// Service for users.
    /// </summary>
    public class UserService : Service<User, SwapplerSqliteContext>, IUserService
    {
        private string imagesPath;

        public UserService(string imagesPath)
        {
            this.ImagesFullPath = imagesPath;
        }

        private string ImagesFullPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + imagesPath; }
            set { imagesPath = value; }
        }

        public UserStatus Add(User user)
        {
            try
            {
                bool usernameExist =
                    (from tUser in Context.Users
                     where tUser.Username == user.Username
                     select (tUser.Username)).Any();
                
                bool emailExist =
                    (from tUser in Context.Users
                     where tUser.Email == user.Email
                     select (tUser.Email)).Any();

                if (usernameExist && emailExist) 
                    return UserStatus.EmailAndUsernameExist;
                
                if (usernameExist)
                    return UserStatus.UsernameAlreadyExist;

                if (emailExist)
                    return UserStatus.EmailAlreadyExist;

                Context.Entry(user).State = EntityState.Added;
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));

                return UserStatus.Error;
            }

            return UserStatus.Added;
        }

        public UserStatus Remove(User user)
        {
            try
            {
                Context.Users.Attach(user);
                Context.Users.Remove(user);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return UserStatus.Error;
            }

            return UserStatus.Removed;
        }

        public UserStatus Update(User user, params string[] updateFields)
        {
            try
            {
                if (updateFields == null || updateFields.Length == 0)
                {
                    Context.Users.Attach(user);
                    Context.Entry(user).State = EntityState.Modified;
                }
                else
                {
                    Context.Users.Attach(user);
                    foreach (string updateField in updateFields)
                    {
                        Context.Entry(user).Property(updateField).IsModified = true;
                    }
                }
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return UserStatus.Error;
            }

            return UserStatus.Updated;
        }

        public List<User> FindWhere(Func<User, bool> wherePredicate)
        {
            try
            {
                var users = Context.Users.Where(wherePredicate);
                return users.ToList();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return null;
            }
        }

        public UserStatus Register(string firstName, string lastName, string username, string email, string password)
        {
            User newUser = new User {
                Name = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                Password = HashHelper.HashPassword(password),
            };

            var userStatus = Add(newUser);

            if (userStatus == UserStatus.Added)
            {
                return UserStatus.Registered;
            }
            
            return userStatus;
        }
        
        /// <summary>
        /// Remove user with given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True if user with specified username existed and its removed, false otherwise</returns>
        public bool Remove(string username)
        {
            var user = new User()
            {
                Username = username
            };

            return Remove(user) == UserStatus.Removed;
        }

        /// <summary>
        /// Get list of all users from database.
        /// </summary>
        /// <returns>List of all users</returns>
        public List<User> AllUsers()
        {
            try
            {
                var users = from user in Context.Users
                            select user;

                return users.ToList();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return null;
            }
        }

        /// <summary>
        /// Get user by name or last name.
        /// </summary>
        /// <para name="name"></para>
        ///  
        public List<User> FindUserByNameOrSurname(string nameOrLastname)
        {
            try
            {
                var users = from user in Context.Users
                            where user.Name == nameOrLastname || user.LastName == nameOrLastname
                            select user;

                return users.ToList();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return null;
            }
        }

        /// <summary>
        /// Get users by its username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User FindUserByUsername(string username)
        {
            try
            {
                var users = from user in Context.Users
                            where user.Name == username || user.LastName == username
                            select user;

                return users.FirstOrDefault();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return null;
            }
        }

        /// <summary>
        /// Find user by its unique id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public User FindUserById(long id)
        {
            try
            {
                var users = from user in Context.Users
                            where user.UserId == id
                            select user;

                return users.FirstOrDefault();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return null;
            }
        }

        /// <summary>
        /// Validate entered email or username against the password
        /// </summary>
        /// <param name="emailOrUsername"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns>UserStatus</returns>
        public UserStatus ValidateCredentials(string emailOrUsername, string password, out User user)
        {
            user = null;
            try
            {
                var users =
                    (from tUser in Context.Users
                     where tUser.Email == emailOrUsername || tUser.Username == emailOrUsername
                     select tUser).ToList();
                if (users.Count == 1)
                {
                    user = users.FirstOrDefault();
                    
                    if (HashHelper.VerifyPassword(password, user.Password))
                    {
                        return UserStatus.ValidCredentials;
                    }
                    
                    return UserStatus.InvalidPassword;
                }
                
                return UserStatus.EmailOrUsernameDoesNotExist; 
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return UserStatus.Error;
            }
        }
    }
}