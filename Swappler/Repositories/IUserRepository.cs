using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swappler.Models;

namespace Swappler.Repositories
{
    public interface IUserRepository
    {
        Boolean addUser(User user);
        Boolean removeUser(String username);
        Boolean updateUser(User user);
        List<User> query(IUserSpecification specification);
        List<User> getAll();
    }
}
