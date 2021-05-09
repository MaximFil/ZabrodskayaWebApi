using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi.DAL
{
    public class StandartType
    {
        public int Id { get; set; }

        public string StandartTypeName { get; set; }

        public string Discription { get; set; }

        public string Abbr { get; set; }

        public List<Standart> Standarts { get; set; }

        public StandartType()
        {
            Standarts = new List<Standart>();
        }
    }
}
