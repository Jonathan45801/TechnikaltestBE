using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUser
{
    public class UserLogin
    {
        public decimal? id_userlogin { get; set; }
        public decimal?user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public User User { get; set; }
    }
}
