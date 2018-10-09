using System;
namespace NewDrugs.Models
{
    public class TbCounselingRecord
    {
        public int noticeSno { get; set; }
        public int counselingCHMbr { get; set; }
        public string counselingCHMbrStr { get; set; }
        public string counselingUser { get; set; }
        public string counselingUserName { get; set; }
        public int counselingWeekNum { get; set; }
        public DateTime counselingDate { get; set; }
        public string counselingDateStr { get{ return counselingDate != DateTime.MinValue ? counselingDate.ToString("yyyy/MM/dd") : "";} }
        public string counselingDateTwStr { get { return counselingDate != DateTime.MinValue ? (counselingDate.Year - 1911).ToString() + "/" + counselingDate.ToString("MM/dd") : ""; } }
        public string isMedicalTreatment { get; set; }
        public string isPsyInSchool { get; set; }
        public string isInspect { get; set; }
        public string inspectResult { get; set; }
        public string isMedicalTreatmentStr { get; set; }
        public string isPsyInSchoolStr { get; set; }
        public string isInspectStr { get; set; }
        public string inspectResultStr { get; set; }
        public string inspectResultFile { get; set; }
        public string counselingReason { get; set; }
        public string writyComplet { get; set; }
        public string crIp { get; set; }
        public string crUser { get; set; }
        public DateTime crDate { get; set; }
        public string upIp { get; set; }
        public DateTime upDate { get; set; }
        public string upDateStr { get { return upDate.ToString("yyyy/MM/dd HH:mm:ss"); } }
        public string upDateTwStr { get { return (upDate.Year - 1911).ToString() + "/" + upDate.ToString("/MM/dd HH:mm:ss"); } }
        public string upUser { get; set; }
    }
}
