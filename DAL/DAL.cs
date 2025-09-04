using System;
using System.Collections.Generic;
using System.Configuration;
using AutoEqlink.ACWS;

namespace AutoEqlink.DAL
{
    public class DataAccessWS
    {
        DuongTest.ACWSSoapClient aws = new DuongTest.ACWSSoapClient();

        ACWS.ACWSSoapClient AB = new ACWS.ACWSSoapClient();

        public DataAccessWS()
        {
            string ServerUrl = ConfigurationManager.AppSettings["ServerURL"];
            aws.Endpoint.Address = new System.ServiceModel.EndpointAddress(ServerUrl);
        }

        //public List<country> getListCountry(out string lstF)
        //{
        //    lstF = "";
        //    List<country> lst = new List<country>();
        //    //ACWS.country[] lst_server = null;
        //    List<ACWS.country> lst_server = new List<ACWS.country>();
        //    lst_server = aws.getListCountry(out lstF);
        //    foreach (ACWS.country item in lst_server)
        //    {
        //        country cty = new country();
        //        cty.CountryCode = item.CountryCode;
        //        cty.Name = item.Name;
        //        cty.ShowHorseInfo = item.ShowHorseInfo;
        //        cty.Flag = item.Flag;
        //        lst.Add(cty);

        //    }
        //    return lst;
        //}

        //public bool UpdateCountry(country item)
        //{
        //    ACWS.country cty = new ACWS.country();
        //    cty.CountryCode = item.CountryCode;
        //    cty.Name = item.Name;
        //    cty.ShowHorseInfo = item.ShowHorseInfo;
        //    cty.Flag = item.Flag;

        //    return aws.UpdateCountry(cty);
        //}

        //public bool SaveLiveToteList1(List<LiveTote> lst)
        //{
        //    ACWS.LiveTote[] lst_server = null;
        //    int i = 0;
        //    foreach (LiveTote item in lst)
        //    {
        //        ACWS.LiveTote lt = new ACWS.LiveTote();
        //        lt.rowid = item.rowid;
        //        lt.RaceCard = item.RaceCard;
        //        lt.RaceCountry = item.RaceCountry;
        //        lt.RaceDay = item.RaceDay;
        //        lt.RaceBoard = item.RaceBoard;
        //        lt.RaceCountryOdds = item.RaceCountryOdds;
        //        lt.RaceNo = item.RaceNo;
        //        lt.Win = item.Win;
        //        lt.Place = item.Place;
        //        lt.txday = item.txday;
        //        lt.events = item.events;
        //        lt.info = item.info;
        //        lt.tstamp = item.tstamp;

        //        lst_server[i] = lt;
        //        i++;

        //    }

        //    return aws.SaveLiveToteList1(lst_server);
        //}


        public bool SaveLiveToteList(List<LiveTote> lst)
        {
            ACWS.LiveTote[] lst_server = new ACWS.LiveTote[lst.Count];
            int i = 0;
            foreach (LiveTote item in lst)
            {
                ACWS.LiveTote lt = new ACWS.LiveTote();
                lt.rowid = item.rowid;
                lt.RaceCard = item.RaceCard;
                lt.RaceCountry = item.RaceCountry;
                lt.RaceDay = item.RaceDay;
                lt.RaceBoard = item.RaceBoard;
                lt.RaceCountryOdds = item.RaceCountryOdds;
                lt.RaceNo = item.RaceNo;
                lt.Win = item.Win;
                lt.Place = item.Place;
                lt.txday = item.txday;
                lt.events = item.events;
                lt.info = item.info;
                lt.tstamp = item.tstamp;

                lst_server[i] = lt;
                i++;

            }
            return AB.SaveLiveToteList(lst_server);
        }
        public bool SaveLiveToteListNew(List<LiveTote> lst)
        {
            ACWS.LiveTote[] lst_server = new ACWS.LiveTote[lst.Count];
            int i = 0;
            foreach (LiveTote item in lst)
            {
                ACWS.LiveTote lt = new ACWS.LiveTote();
                lt.rowid = item.rowid;
                lt.RaceCard = item.RaceCard;
                lt.RaceCountry = item.RaceCountry;
                lt.RaceDay = item.RaceDay;
                lt.RaceBoard = item.RaceBoard;
                lt.RaceCountryOdds = item.RaceCountryOdds;
                lt.RaceNo = item.RaceNo;
                lt.Win = item.Win;
                lt.Place = item.Place;
                lt.txday = item.txday;
                lt.events = item.events;
                lt.info = item.info;
                lt.tstamp = item.tstamp;
                lt.RaceCardId = item.RaceCardId;

                lst_server[i] = lt;
                i++;

            }
            return AB.SaveLiveToteListNew(lst_server);
        }

       
        public void SendMessageNS(string msg)
        {
            //aws.SendMessage(msg);
        }

        public bool UpdateRaceInfo(RaceInfo item)
        {
            var m = new ACWS.RaceInfo()
            {
                RaceID = item.RaceID,
                RaceCard = item.RaceCard,
                RaceCountry = item.RaceCountry,
                items2en = item.items2en,
                items2cn = item.items2cn
            };

            return AB.UpdateRaceInfo(m);
        }

        public bool UpdateQuinella(Quinella item)
        {
            var m = new ACWS.Quinella()
            {
                RaceCardId = item.RaceCardId,
                QuinelaBF = item.QuinelaBF,
                QuinelaPlaceBF = item.QuinelaPlaceBF,               
            };
            return AB.UpdateQuinella(m);
        }
    }
}