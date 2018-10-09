using System;
namespace NewDrugs.Models
{
    public class TbSpcfPersonMas
    {
        public int rowId{ get; set; }
        public int sno{ get; set; }
        public string fillMm{ get; set; }
        public string fillYyyy{ get; set; }
        public string userId{ get; set; }
        public string fillStatus{ get; set; }
        public string fillStatusStr { get; set; }
        public int flowSno{ get; set; }
        public string flowStatus{ get; set; }
        public string flowStatusStr { get; set; }
        public string crIp{ get; set; }
        public DateTime crDate{ get; set; }
        public string crUser{ get; set; }
        public string upIp{ get; set; }
        public DateTime upDate{ get; set; }
        public string upUser{ get; set; }
        public string upUserName{get;set;}
        public string upDateStr{
            get{
                return upDate == DateTime.MinValue ? "" : upDate.ToString("yyyy/MM/dd");
            }
        }
        public string upDateTwStr{
            get{
                return upDate == DateTime.MinValue ? "" : (upDate.Year - 1911).ToString() + "/" + upDate.ToString("MM/dd");
            }
        }
        public string nowTaskUser{ get; set; }
        public string school{ get; set; }
        public int schoolSystemSno{ get; set; }
        public string schoolSystemSnoStr{ get; set; }
        public int countyId{get;set;}
        public string countyIdStr{get;set;}
    }
}
