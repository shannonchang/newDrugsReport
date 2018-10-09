using System;
namespace NewDrugs.Models
{
    public class TbSpcfPersonDet
    {
        public int rowId { get; set; }
        public int sno { get; set; }
        public int masSno { get; set; }
        public string stuName { get; set; }
        public string stuSex { get; set; }
        public string stuSexStr { get; set; }
        public string stuIdType { get; set; }
        public string stuIdCord { get; set; }
        public string stuBirthDay { get; set; }
        public string stuCategory { get; set; }
        public string stuCategoryStr { get; set; }
        public string crIp { get; set; }
        public DateTime crDate { get; set; }
        public string crUser { get; set; }
        public string upIp { get; set; }
        public DateTime upDate { get; set; }
        public string upDateStr{
            get{
                return upDate == DateTime.MinValue ? "" : upDate.ToString("yyyy/MM/dd hh:mm:ss");
            }
        }
        public string upDateTwStr{
            get{
                return upDate == DateTime.MinValue ? "" : (upDate.Year - 1911).ToString() + "/" + upDate.ToString("MM/dd hh:mm:ss");
            }
        }
        public string upUser { get; set; }
        public string upUserName { get; set; }
        public string eduDvs { get; set; }
        public string eduDvsStr { get; set; }
        public string eduGrade { get; set; }
        public string eduDept { get; set; }
        public string countyId { get; set; }
        public string countyIdStr { get; set; }
        public string schoolSystemSno { get; set; }
        public string schoolSystemSnoStr { get; set; }
        public string school { get; set; }
        public string userId { get; set; }
    }
}
