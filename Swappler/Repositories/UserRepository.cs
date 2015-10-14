using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swappler.Models;

namespace Swappler.Repositories
{
    /*
     * Repository for users.
     * 
     */
    public class UserRepository : IUserRepository
    {
        UsersDAO usersDAO = new UsersDAO();

        Boolean IUserRepository.addUser(User user)
        {
            return usersDAO.addUser(user);
        }

        Boolean IUserRepository.removeUser(String username)
        {
            return usersDAO.removeUser(username);
        }

        Boolean IUserRepository.updateUser(User user)
        {
            return usersDAO.updateUser(user);
        }

        List<User> IUserRepository.query(IUserSpecification specification)
        {
            return usersDAO.queryUsers(specification.toSqlClause());
        }

        List<User> IUserRepository.getAll()
        {
            return usersDAO.getAllUsers();
        }
    }
}