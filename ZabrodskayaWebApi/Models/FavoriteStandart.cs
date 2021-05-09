using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi.Models
{
    public class FavoriteStandart
    {
        public int StandartId { get; set;}

        public string StandartTypeName { get; set; }

        public string StandartHeader { get; set; }

        public string StandartDetails { get; set; }
    }
}
