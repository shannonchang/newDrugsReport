using System;
namespace NewDrugs.Models
{
    public class VwUserData
    {
        public int sno { get; set; }
        public string userId { get; set; }
        public string password { get; set; }
        public string school { get; set; }
        public string schoolSystemSsno { get; set; }
        public string schoolSystemName { get; set; } 
        public string schoolPresident{ get; set; }
        public string counselingName{ get; set; }
        public string job{ get; set; }
        public int title{ get; set; }
        public string titleName{ get; set; }
        public string phone{ get; set; }
        public string eMail{ get; set; }
        public int countyId{ get; set; }
        public string town{ get; set; }
        public string schoolAddress{ get; set; }
        public int loginType{ get; set; }
        public string loginTypeName{ get; set; }
        public int status{ get; set; }
        public string showFlag{ get; set; }
        public int loginError{ get; set; }
    }
}
