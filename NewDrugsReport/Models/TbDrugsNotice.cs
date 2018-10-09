using System;
namespace NewDrugs.Models
{
    public class TbDrugsNotice
    {
        public int sno { get; set; }
        public int idn { get; set; }
        public string userId { get; set; }
        public string noticeSchool { get; set; }
        public int eventSchoolSystem { get; set; }
        public string eventSchoolSystemStr { get; set; }
        public string eventCategory { get; set; }
        public string eventCategoryName { get; set; }
        public string counselingStatus { get; set; }
        public string stuName { get; set; }
        public string sexStr { get; set; }
        public string isAgain { get; set; }
        public DateTime eventReportTime { get; set; }
        public string eventReportTimeStr{ 
            get {
                return eventReportTime != DateTime.MinValue ? eventReportTime.ToString("yyyy/MM/dd") : "";
            }
        }
        public string eventReportTimeTwStr{ 
            get {
                return eventReportTime != DateTime.MinValue ? (eventReportTime.Year - 1911).ToString() + "/" + eventReportTime.ToString("MM/dd") : "";
            }
        }
        public string noticeSchedule { get; set; }
        public string isWrithComplet { get; set; }
        public string isGivenPeople { get; set; }
        public string flowStatus { get; set; }
        public string flowAccount { get; set; }
        public int flowSno { get; set; }
        public string isSetupCh { get; set; }
        public string isMajorCase { get; set; }
        public string noSetupRemark { get; set; }
        public string noSetupRemarkOther { get; set; }
        public DateTime noSetupRemarkDate { get; set; }
        public string noSetupRemarkDateStr{ 
            get {
                return noSetupRemarkDate != DateTime.MinValue ? noSetupRemarkDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string noSetupRemarkDateTwStr{ 
            get {
                return noSetupRemarkDate != DateTime.MinValue ? (noSetupRemarkDate.Year - 1911).ToString() + "/" + noSetupRemarkDate.ToString("MM/dd") : "";
            }
        }
        public string noticeStatus { get; set; }
        public string noticeStatusStr { get; set; }
        public string noticeReason { get; set; }
        public DateTime crDate { get; set; }
        public string upUser { get; set; }
        public string upIp { get; set; }
        public DateTime upDate { get; set; }
    }
}
