using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCapture.DAL
{
    public class LiveTote
    {
        public string rowid { get; set; }
        public string RaceCard { get; set; }
        public string RaceCountry { get; set; }
        public string RaceDay { get; set; }
        public string RaceBoard { get; set; }
        public string RaceCountryOdds { get; set; }
        public string RaceNo { get; set; }
        public string Win { get; set; }
        public string Place { get; set; }
        public string txday { get; set; }
        public string events { get; set; }
        public string info { get; set; }
        public string status { get; set; }
        public string tstamp { get; set; }
        public string RaceCardId { get; set; }
    }

    public class country
    {
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string ShowHorseInfo { get; set; }
        public string Flag { get; set; }
    }

    
}
