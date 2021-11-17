using System;
using System.Collections.Generic;
using System.Text;

namespace HashtilMobile
{
    internal static class Constants
    {
        
        public const string Heb_UserAndPwdEmpty = "שם משתמש או סיסמא לא יכולים להיות ריקים!";
        public const string Heb_Error = "שגיאה!";
        public const string Heb_Ok = "בסדר";
        public const string Heb_WrongUserPwd = "שם משתמש ו/או סיסמא לא נכונים";
        public const string Heb_Close = "סגירה";

        public static class Urls
        {
            public const string BaseUrl = "https://192.168.254.6/api/Android";
            public const string LoginController = "Login/PostLogin";
        }

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string ThaiGuy = "Thai-Guy";
            public const string ThaiGreenHouse = "Thai-GreenHouse";
            public const string AuditAvgCounter = "AuditAvgCounter";
            public const string ManagerGreenHouse = "Manager-GreenHouse";
            
        }
    }
}
