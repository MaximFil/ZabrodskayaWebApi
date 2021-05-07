using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi.DAL
{
    public class User
    {

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<XrefUserStandart> XrefUserStandarts { get; set; }

        public User()
        {
            XrefUserStandarts = new List<XrefUserStandart>();
        }
    }
}
