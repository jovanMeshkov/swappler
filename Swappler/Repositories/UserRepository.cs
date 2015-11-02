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
            return usersDAO.AddUser(user);
        }

        Boolean IUserRepository.removeUser(String username)
        {
            return usersDAO.RemoveUser(username);
        }

        Boolean IUserRepository.updateUser(User user)
        {
            return usersDAO.UpdateUser(user);
        }

        List<User> IUserRepository.query(IUserSpecification specification)
        {
            return usersDAO.QueryUsers(specification.toSqlClause());
        }

        List<User> IUserRepository.getAll()
        {
            return usersDAO.GetAllUsers();
        }
    }
}