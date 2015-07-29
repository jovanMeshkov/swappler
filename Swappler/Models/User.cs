using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    public class User
    {
        private String Name { get; set; }
        private String lastName { get; set; }
        private String email { get; set; }
        private String password { get; set; }
        private String username { get; set; }
        private String phone { get; set; }
        private String photoURL { get; set; }
        private Address address { get; set; }
    }
}