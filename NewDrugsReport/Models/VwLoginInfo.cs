using System;
namespace NewDrugs.Models
{
    public class VwLoginInfo
    {
        public string userId { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string school { get; set; }
        public int schoolSystemSno { get; set; }
        public string schoolSystemName { get; set; }
        public string schoolPresident { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public string title { get; set; }
        public string titleName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string countyId { get; set; }
        public string city { get; set; }
        public string road { get; set; }
        public string countyName { get; set; }
        public string cityName { get; set; }
        public string roadName { get; set; }
        public string schoolAddress { get; set; }
        public int loginType { get; set; }
        public string loginTypeName { get; set; }
        public string status { get; set; }
        public string showFlag { get; set; }
        public int loginError { get; set; }
        public string isWarn { get; set; }
    }
}
