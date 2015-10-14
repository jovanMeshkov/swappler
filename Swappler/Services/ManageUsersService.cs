using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swappler.Models;
using Swappler.Repositories;

namespace Swappler.Services
{
    /*
     *  Service for managing users.
     *  
     * 
     */
    public class ManageUsersService
    {
        private UserRepository userRepository;
        private IUserRepository userRepositoryInterface;

        /**
         *  Constructor.
         */
        public ManageUsersService()
        {
            userRepository = new UserRepository();
            userRepositoryInterface = (IUserRepository)userRepository;
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
            // TODO: Method stub - getUserByName
            return null;
        }

        /*
         * Get users by its username.
         * 
         */
        public User getUserByUsername(String username)
        {
            // TODO: Method stub - getUserByUsername
            return null;
        }

        /*
         * Add and register new user.
         * 
         */
        public void addNewUser(String name, String lastName, String email, String password, String username, String phone, String address)
        {
            User newUser = new User(name, lastName, email, password, username, phone, address);
            userRepositoryInterface.addUser(newUser);
        }

        /*
         * Remove user with given username.
         * 
         */
        public void removeUser(String username)
        {
            userRepositoryInterface.removeUser(username);
        }

    }
}