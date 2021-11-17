using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared
{
    internal static class Constants
    {
        public static class Tokens
        {
            public const string ThaiSowing = "5FG6$SDGF*YHJST%$^U3245";
            public const string ThaiGreenHouse = "kFGf$SDGF*Y4@ST%$^E3285";
            public const string Admin = "kJGf$LDGF*Y9@ST%$^E*2#5";

        }
        public static string SetToken(string role)
        {
            string token = "";

            switch (role)
            {
                case "Thai-Sowing":
                    token = Tokens.ThaiSowing;
                    break;
                case "Thai-GreenHouse":
                    token = Tokens.ThaiGreenHouse;
                    break;
                case "Administrator":
                    token = Tokens.Admin;
                    break;
            }
            return token;
        }
    }
}
