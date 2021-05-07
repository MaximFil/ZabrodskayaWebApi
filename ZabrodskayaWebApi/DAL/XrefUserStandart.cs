using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi.DAL
{
    public class XrefUserStandart
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int StandartId { get; set; }
        public Standart Standart { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
