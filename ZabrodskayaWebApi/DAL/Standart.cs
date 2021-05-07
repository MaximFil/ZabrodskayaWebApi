using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi.DAL
{
    public class Standart
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string Details { get; set; }

        public int StandartTypeId { get; set; }
        public StandartType StandartType { get; set; }

        public List<XrefUserStandart> XrefUserStandarts { get; set; }

        public Standart()
        {
            XrefUserStandarts = new List<XrefUserStandart>();
        }
    }
}
