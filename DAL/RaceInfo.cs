namespace AutoCapture.DAL
{
    public class RaceInfo
    {
        public string RaceID { get; set; }
        public int rowid { get; set; }
        public string RaceCard { get; set; }
        public string RaceCountry { get; set; }
        public string items2en { get; set; }
        public string items2cn { get; set; }
    }
    public class Quinella
    {
        public string RaceCardId { get; set; }
        public int Single { get; set; }
        public string QuinelaBF { get; set; }
        public string QuinelaAF { get; set; }
        public string QuinelaPlaceBF { get; set; }
        public string QuinelaPlaceAF { get; set; }
    }

    public class DataGrid
    {
        public string Id { get; set; }
        public string HorseId { get; set; }
        public string HorseName { get; set; }
        public int Count { get; set; }
    }

    public class Info
    {
        public string HorseId { get; set; }
        public string HorseName { get; set; }
    }
}