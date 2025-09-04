using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCapture.DAL
{
   public class EventResult
    {
        public string ID_Event { get; set; }
        public string txtday { get; set; }
        public string Name_en { get; set; }
        public List<TeamResult> listteam { get; set; }
    }
}
