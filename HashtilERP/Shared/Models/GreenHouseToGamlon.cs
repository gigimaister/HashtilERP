using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public  class GreenHouseToGamlon
    {
        public class Hamama
        {
            public string HamamaName { get; set; }
            public string HamamaId { get; set; }
        }

        public class Gamlons
        {
            public string GamlonName { get; set; }
            public string HamamaId { get; set; }
            public string GamlonId { get; set; }
            
        }
        public class Gamlonim {
            public string gamlon { get; set; }
        }

        public static bool CheckIfHamamaAndGamlonExsist(string hamama, string gamlon = "0")
        {
            int t = 0;
            foreach(var i in Hamamas)
            {
                if(i.HamamaId==hamama)
                {
                    t++;
                }
            }
            if(t==1 && gamlon=="0")
            {
                return true;
            }
            if (t == 1 && gamlon != "0")
            {
                foreach(var i in gamlons)
                {
                    if( i.GamlonName == gamlon && i.HamamaId==hamama)
                    {
                        return true;
                    }
                }
            }            
            return false;
        }


        public static List<Hamama> Hamamas = new List<Hamama>() {
        new Hamama(){ HamamaName= "1", HamamaId= "1" },
        new Hamama(){ HamamaName= "2", HamamaId= "2" },
        new Hamama(){ HamamaName= "4", HamamaId= "4" },
        new Hamama(){ HamamaName= "5", HamamaId= "5" },
        new Hamama(){ HamamaName= "6", HamamaId= "6" },
        new Hamama(){ HamamaName= "7", HamamaId= "7" },
    };

        public static List<Gamlonim> AllGamlonim = new List<Gamlonim>()
        {
            new Gamlonim() {gamlon="1"},
            new Gamlonim() {gamlon="2"},
            new Gamlonim() {gamlon="3"},
            new Gamlonim() {gamlon="4"},
            new Gamlonim() {gamlon="5"},
            new Gamlonim() {gamlon="6"},
            new Gamlonim() {gamlon="7"},
            new Gamlonim() {gamlon="8"},
            new Gamlonim() {gamlon="9"},
            new Gamlonim() {gamlon="10"},
            new Gamlonim() {gamlon="11"},
            new Gamlonim() {gamlon="12"},
            new Gamlonim() {gamlon="13"},
            new Gamlonim() {gamlon="14"},
            new Gamlonim() {gamlon="15"},
            new Gamlonim() {gamlon="16"},
            new Gamlonim() {gamlon="17"},
            new Gamlonim() {gamlon="18"},
            new Gamlonim() {gamlon="19"},
            new Gamlonim() {gamlon="20"},
            new Gamlonim() {gamlon="21"},
            new Gamlonim() {gamlon="22"},
            new Gamlonim() {gamlon="23"},
            new Gamlonim() {gamlon="24"},
            new Gamlonim() {gamlon="25"},
            new Gamlonim() {gamlon="26"},
            new Gamlonim() {gamlon="27"},
            new Gamlonim() {gamlon="28"},
            new Gamlonim() {gamlon="29"},
            new Gamlonim() {gamlon="30"},
            new Gamlonim() {gamlon="31"},
            new Gamlonim() {gamlon="32"},
            new Gamlonim() {gamlon="33"},

        };
        
        public static List<Gamlons> gamlons = new List<Gamlons>() {
        new Gamlons() { GamlonName= "5", HamamaId= "1", GamlonId= "105" },
        new Gamlons() { GamlonName= "6", HamamaId= "1", GamlonId= "106"},
        new Gamlons() { GamlonName= "7", HamamaId= "1", GamlonId= "107" },
        new Gamlons() { GamlonName= "8", HamamaId= "1", GamlonId= "108" },
        new Gamlons() { GamlonName= "9", HamamaId= "1", GamlonId= "109" },
        new Gamlons() { GamlonName= "10", HamamaId= "1", GamlonId= "110"},
        new Gamlons() { GamlonName= "11", HamamaId= "1", GamlonId= "111"},
        new Gamlons() { GamlonName= "12", HamamaId= "1", GamlonId= "112"},
        new Gamlons() { GamlonName= "13", HamamaId= "1", GamlonId= "113"},
        new Gamlons() { GamlonName= "14", HamamaId= "1", GamlonId= "114"},
        new Gamlons() { GamlonName= "15", HamamaId= "1", GamlonId= "115"},
        new Gamlons() { GamlonName= "16", HamamaId= "1", GamlonId= "116"},
        new Gamlons() { GamlonName= "17", HamamaId= "1", GamlonId= "117"},
        new Gamlons() { GamlonName= "18", HamamaId= "1", GamlonId= "118"},
        new Gamlons() { GamlonName= "19", HamamaId= "1", GamlonId= "119"},
        new Gamlons() { GamlonName= "20", HamamaId= "1", GamlonId= "120"},
        new Gamlons() { GamlonName= "21", HamamaId= "1", GamlonId= "121"},
        new Gamlons() { GamlonName= "22", HamamaId= "1", GamlonId= "122"},
        new Gamlons() { GamlonName= "23", HamamaId= "1", GamlonId= "123"},
        new Gamlons() { GamlonName= "24", HamamaId= "1", GamlonId= "124"},
        new Gamlons() { GamlonName= "25", HamamaId= "1", GamlonId= "125"},
        new Gamlons() { GamlonName= "26", HamamaId= "1", GamlonId= "126"},
        new Gamlons() { GamlonName= "27", HamamaId= "1", GamlonId= "127"},
        new Gamlons() { GamlonName= "28", HamamaId= "1", GamlonId= "128"},
        new Gamlons() { GamlonName= "29", HamamaId= "1", GamlonId= "129"},
        new Gamlons() { GamlonName= "30", HamamaId= "1", GamlonId= "130"},
        new Gamlons() { GamlonName= "31", HamamaId= "1", GamlonId= "131"},
        new Gamlons() { GamlonName= "32", HamamaId= "1", GamlonId= "132"},
        new Gamlons() { GamlonName= "33", HamamaId= "1", GamlonId= "133"},
        new Gamlons() { GamlonName= "34", HamamaId= "1", GamlonId= "134"},
        new Gamlons() { GamlonName= "1", HamamaId= "2", GamlonId= "201" },
        new Gamlons() { GamlonName= "2", HamamaId= "2", GamlonId= "202" },
        new Gamlons() { GamlonName= "3", HamamaId= "2", GamlonId= "203" },
        new Gamlons() { GamlonName= "4", HamamaId= "2", GamlonId= "204" },
        new Gamlons() { GamlonName= "5", HamamaId= "2", GamlonId= "205" },
        new Gamlons() { GamlonName= "6", HamamaId= "2", GamlonId= "206" },
        new Gamlons() { GamlonName= "8", HamamaId= "2", GamlonId= "208" },
        new Gamlons() { GamlonName= "9", HamamaId= "2", GamlonId= "209" },
        new Gamlons() { GamlonName= "10", HamamaId= "2", GamlonId= "210"},
        new Gamlons() { GamlonName= "11", HamamaId= "2", GamlonId= "211"},
        new Gamlons() { GamlonName= "12", HamamaId= "2", GamlonId= "212"},
        new Gamlons() { GamlonName= "13", HamamaId= "2", GamlonId= "213"},
        new Gamlons() { GamlonName= "14", HamamaId= "2", GamlonId= "214"},
        new Gamlons() { GamlonName= "15", HamamaId= "2", GamlonId= "215"},
        new Gamlons() { GamlonName= "16", HamamaId= "2", GamlonId= "216"},
        new Gamlons() { GamlonName= "17", HamamaId= "2", GamlonId= "217"},
        new Gamlons() { GamlonName= "18", HamamaId= "2", GamlonId= "218"},
        new Gamlons() { GamlonName= "19", HamamaId= "2", GamlonId= "219"},
        new Gamlons() { GamlonName= "20", HamamaId= "2", GamlonId= "220"},
        new Gamlons() { GamlonName= "21", HamamaId= "2", GamlonId= "221"},
        new Gamlons() { GamlonName= "22", HamamaId= "2", GamlonId= "222"},
        new Gamlons() { GamlonName= "1", HamamaId= "4", GamlonId= "401" },
        new Gamlons() { GamlonName= "2", HamamaId= "4", GamlonId= "402" },
        new Gamlons() { GamlonName= "3", HamamaId= "4", GamlonId= "403" },
        new Gamlons() { GamlonName= "4", HamamaId= "4", GamlonId= "404" },
        new Gamlons() { GamlonName= "5", HamamaId= "4", GamlonId= "405" },
        new Gamlons() { GamlonName= "6", HamamaId= "4", GamlonId= "406" },
        new Gamlons() { GamlonName= "8", HamamaId= "4", GamlonId= "408" },
        new Gamlons() { GamlonName= "9", HamamaId= "4", GamlonId= "409" },
        new Gamlons() { GamlonName= "10", HamamaId= "4", GamlonId= "410"},
        new Gamlons() { GamlonName= "11", HamamaId= "4", GamlonId= "411"},
        new Gamlons() { GamlonName= "12", HamamaId= "4", GamlonId= "412"},
        new Gamlons() { GamlonName= "13", HamamaId= "4", GamlonId= "413"},
        new Gamlons() { GamlonName= "14", HamamaId= "4", GamlonId= "414"},
        new Gamlons() { GamlonName= "15", HamamaId= "4", GamlonId= "415"},
        new Gamlons() { GamlonName= "16", HamamaId= "4", GamlonId= "416"},
        new Gamlons() { GamlonName= "17", HamamaId= "4", GamlonId= "417"},
        new Gamlons() { GamlonName= "18", HamamaId= "4", GamlonId= "418"},
        new Gamlons() { GamlonName= "19", HamamaId= "4", GamlonId= "419"},
        new Gamlons() { GamlonName= "20", HamamaId= "4", GamlonId= "420"},
        new Gamlons() { GamlonName= "21", HamamaId= "4", GamlonId= "421"},
        new Gamlons() { GamlonName= "22", HamamaId= "4", GamlonId= "422"},
        new Gamlons() { GamlonName= "1", HamamaId= "5", GamlonId= "501" },
        new Gamlons() { GamlonName= "2", HamamaId= "5", GamlonId= "502" },
        new Gamlons() { GamlonName= "3", HamamaId= "5", GamlonId= "503" },
        new Gamlons() { GamlonName= "4", HamamaId= "5", GamlonId= "504" },
        new Gamlons() { GamlonName= "5", HamamaId= "5", GamlonId= "505" },
        new Gamlons() { GamlonName= "6", HamamaId= "5", GamlonId= "506" },
        new Gamlons() { GamlonName= "8", HamamaId= "5", GamlonId= "508" },
        new Gamlons() { GamlonName= "9", HamamaId= "5", GamlonId= "509" },
        new Gamlons() { GamlonName= "10", HamamaId= "5", GamlonId= "510"},
        new Gamlons() { GamlonName= "11", HamamaId= "5", GamlonId= "511"},
        new Gamlons() { GamlonName= "12", HamamaId= "5", GamlonId= "512"},
        new Gamlons() { GamlonName= "13", HamamaId= "5", GamlonId= "513"},
        new Gamlons() { GamlonName= "14", HamamaId= "5", GamlonId= "514"},
        new Gamlons() { GamlonName= "15", HamamaId= "5", GamlonId= "515"},
        new Gamlons() { GamlonName= "16", HamamaId= "5", GamlonId= "516"},
        new Gamlons() { GamlonName= "17", HamamaId= "5", GamlonId= "517"},
        new Gamlons() { GamlonName= "18", HamamaId= "5", GamlonId= "518"},
        new Gamlons() { GamlonName= "1", HamamaId= "6", GamlonId= "601" },
        new Gamlons() { GamlonName= "2", HamamaId= "6", GamlonId= "602" },
        new Gamlons() { GamlonName= "3", HamamaId= "6", GamlonId= "603" },
        new Gamlons() { GamlonName= "4", HamamaId= "6", GamlonId= "604" },
        new Gamlons() { GamlonName= "5", HamamaId= "6", GamlonId= "605" },
        new Gamlons() { GamlonName= "6", HamamaId= "6", GamlonId= "606" },
        new Gamlons() { GamlonName= "7", HamamaId= "6", GamlonId= "607" },
        new Gamlons() { GamlonName= "8", HamamaId= "6", GamlonId= "608" },
        new Gamlons() { GamlonName= "9", HamamaId= "6", GamlonId= "609" },
        new Gamlons() { GamlonName= "10", HamamaId= "6", GamlonId= "610"},
        new Gamlons() { GamlonName= "11", HamamaId= "6", GamlonId= "611"},
        new Gamlons() { GamlonName= "13", HamamaId= "6", GamlonId= "613"},
        new Gamlons() { GamlonName= "14", HamamaId= "6", GamlonId= "614"},
        new Gamlons() { GamlonName= "15", HamamaId= "6", GamlonId= "615"},
        new Gamlons() { GamlonName= "16", HamamaId= "6", GamlonId= "616"},
        new Gamlons() { GamlonName= "17", HamamaId= "6", GamlonId= "617"},
        new Gamlons() { GamlonName= "18", HamamaId= "6", GamlonId= "618"},
        new Gamlons() { GamlonName= "19", HamamaId= "6", GamlonId= "619"},
        new Gamlons() { GamlonName= "20", HamamaId= "6", GamlonId= "620"},
        new Gamlons() { GamlonName= "21", HamamaId= "6", GamlonId= "621"},
        new Gamlons() { GamlonName= "22", HamamaId= "6", GamlonId= "622"},
        new Gamlons() { GamlonName= "1", HamamaId= "7", GamlonId= "701" },
        new Gamlons() { GamlonName= "2", HamamaId= "7", GamlonId= "702" },
        new Gamlons() { GamlonName= "3", HamamaId= "7", GamlonId= "703" },
        new Gamlons() { GamlonName= "4", HamamaId= "7", GamlonId= "704" },
        new Gamlons() { GamlonName= "5", HamamaId= "7", GamlonId= "705" },
        new Gamlons() { GamlonName= "6", HamamaId= "7", GamlonId= "706" },
        new Gamlons() { GamlonName= "7", HamamaId= "7", GamlonId= "707" },
        new Gamlons() { GamlonName= "8", HamamaId= "7", GamlonId= "708" },
        new Gamlons() { GamlonName= "9", HamamaId= "7", GamlonId= "709" },
        new Gamlons() { GamlonName= "10", HamamaId= "7", GamlonId= "710"},
        new Gamlons() { GamlonName= "11", HamamaId= "7", GamlonId= "711"},
        new Gamlons() { GamlonName= "12", HamamaId= "7", GamlonId= "712"},
        new Gamlons() { GamlonName= "13", HamamaId= "7", GamlonId= "713"},
        new Gamlons() { GamlonName= "14", HamamaId= "7", GamlonId= "714"},
        new Gamlons() { GamlonName= "15", HamamaId= "7", GamlonId= "715"},
        new Gamlons() { GamlonName= "16", HamamaId= "7", GamlonId= "716"},
        new Gamlons() { GamlonName= "17", HamamaId= "7", GamlonId= "717"},
        new Gamlons() { GamlonName= "18", HamamaId= "7", GamlonId= "718"},
        new Gamlons() { GamlonName= "19", HamamaId= "7", GamlonId= "719"},
        new Gamlons() { GamlonName= "20", HamamaId= "7", GamlonId= "720"},
        new Gamlons() { GamlonName= "21", HamamaId= "7", GamlonId= "721"},
        new Gamlons() { GamlonName= "22", HamamaId= "7", GamlonId= "722"},
        new Gamlons() { GamlonName= "23", HamamaId= "7", GamlonId= "723"},
        new Gamlons() { GamlonName= "24", HamamaId= "7", GamlonId= "724"},
        new Gamlons() { GamlonName= "25", HamamaId= "7", GamlonId= "725"},
        new Gamlons() { GamlonName= "26", HamamaId= "7", GamlonId= "726"},
        new Gamlons() { GamlonName= "27", HamamaId= "7", GamlonId= "727"},
        new Gamlons() { GamlonName= "28", HamamaId= "7", GamlonId= "728"},
        new Gamlons() { GamlonName= "29", HamamaId= "7", GamlonId= "729"},
        new Gamlons() { GamlonName= "30", HamamaId= "7", GamlonId= "730"},
        new Gamlons() { GamlonName= "31", HamamaId= "7", GamlonId= "731"},
        new Gamlons() { GamlonName= "32", HamamaId= "7", GamlonId= "732"},

    };
    }
}
