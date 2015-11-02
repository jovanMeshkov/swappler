using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swappler.Models;
using Swappler.Repositories;
using System.Diagnostics;

namespace Swappler.Services
{
    /*
     *  Service for managing users.
     *  
     * 
     */
    public class UserManagementService
    {
        private UserRepository userRepository;
        private IUserRepository userRepositoryInterface;

        /**
         *  Constructor.
         */
        public UserManagementService()
        {
            userRepository = new UserRepository();
            userRepositoryInterface = (IUserRepository) userRepository;
        }

        /*
         * Retrieve all registered users.
         * 
         */
        public List<User> getAllUsers()
        {
            return userRepositoryInterface.getAll(); 
        }

        /*
         * Get user by name or last name.
         *  
         */
        public List<User> getUserByName(String name)
        {
            return userRepositoryInterface.query(new UserSpecificationByName(name));
        }

        /*
         * Get users by its username.
         * 
         */
        public User getUserByUsername(String username)
        {
            List<User> listUsers = userRepositoryInterface.query(new UserSpecificationByUsername(username));
            if (listUsers.Count == 1)
            {
                return listUsers.ElementAt(0);
            }
            else
            {
                // throw new Exception(); //TODO: Handle it!
                return null;
            }
        }

        /*
         * Add and register new user.
         * 
         */
        public User addNewUser(String name, String lastName, String email, String password, String username, String phone, String address)
        {
            User newUser = new User(name, lastName, email, password, username, phone, address);
            userRepositoryInterface.addUser(newUser);
            return newUser;
        }

        /*
         * Remove user with given username.
         * 
         */
        public Boolean removeUser(String username)
        {
            return userRepositoryInterface.removeUser(username);
        }

    }
}