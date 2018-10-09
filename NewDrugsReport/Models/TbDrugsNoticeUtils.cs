using System;
namespace NewDrugs.Models
{
    public class TbDrugsNoticeUtils
    {
        public int rowId { get; set; }
        public int sno { get; set; }
        public string idn { get; set; }
        public int stuSno { get; set; }
        public string stuName { get; set; }
        public string stuNameEnCode { 
            get{
                string encodeString = "";
                for (int i = 0; i < stuName.Length - 2;i++){
                    encodeString += "〇";
                }
                return stuName.Substring(0,1) + encodeString + stuName.Substring(stuName.Length-1, 1);
            }
        }
        public string sex { get; set; }
        public string sexStr { get; set; }
        public string school { get; set; }
        public string isAgain { get; set; }
        public string isAgainStr { get; set; }
        public string isGivenPeople { get; set; }
        public string isGivenPeopleStr { get; set; }
        public string isWrityComplet { get; set; }
        public string isWrityCompletStr { get; set; }
        public string isSetupCh { get; set; }
        public string isMajorCase { get; set; }
        public string counselingStatus { get; set; }
        public string counselingTrack { get; set; } //COUNSELING_TRACK
        public string noticeSchedule { get; set; }
        public int flowSno { get; set; }
        public string flowStatus { get; set; }
        public DateTime eventReportBeginDate { get; set; }
        public string eventReportBeginDateStr{ 
            get {
                return eventReportBeginDate != DateTime.MinValue ? eventReportBeginDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string eventReportBeginDateTwStr{ 
            get {
                return eventReportBeginDate != DateTime.MinValue ? (eventReportBeginDate.Year - 1911).ToString() + "/" + eventReportBeginDate.ToString("MM/dd") : "";
            }
        }
        public DateTime eventReportEndDate { get; set; }
        public string eventReportEndDateStr{ 
            get {
                return eventReportEndDate != DateTime.MinValue ? eventReportEndDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string eventReportEndDateTwStr{ 
            get {
                return eventReportEndDate != DateTime.MinValue ? (eventReportEndDate.Year - 1911).ToString() + "/" + eventReportEndDate.ToString("MM/dd") : "";
            }
        }
        public string noticeStatus { get; set; }
        public string noticeStatusStr { get; set; }
        public string noticeReason { get; set; }
        public string stuIdNo { get; set; }
        public string userId { get; set; }
        public string countyId { get; set; }
        public int schoolSystemSno { get; set; }
        public string countyStr { get; set; }
        public string schoolSystemStr { get; set; }
        public string counselingStatusStr { get; set; }
        public string noticeScheduleStr { get; set; }
        public string flowStatusStr { get; set; }
        public DateTime counselingBeginDate { get; set; }
        public string counselingBeginDateStr{ 
            get {
                return counselingBeginDate != DateTime.MinValue ? counselingBeginDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string counselingBeginDateTwStr{ 
            get {
                return counselingBeginDate != DateTime.MinValue ? (counselingBeginDate.Year - 1911).ToString() + "/" + counselingBeginDate.ToString("MM/dd") : "";
            }
        }
        public DateTime counselingEndDate { get; set; }
        public string counselingEndDateStr{ 
            get {
                return counselingEndDate != DateTime.MinValue ? counselingEndDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string counselingEndDateTwStr{ 
            get {
                return counselingEndDate != DateTime.MinValue ? (counselingEndDate.Year - 1911).ToString() + "/" + counselingEndDate.ToString("MM/dd") : "";
            }
        }
        public DateTime closeMeetBeginDate { get; set; }
        public string closeMeetBeginDateStr{ 
            get {
                return closeMeetBeginDate != DateTime.MinValue ? closeMeetBeginDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string closeMeetBeginDateTwStr{ 
            get {
                return closeMeetBeginDate != DateTime.MinValue ? (closeMeetBeginDate.Year - 1911).ToString() + "/" + closeMeetBeginDate.ToString("MM/dd") : "";
            }
        }
        public DateTime closeMeetEndDate { get; set; }
        public string closeMeetEndDateStr{ 
            get {
                return closeMeetEndDate != DateTime.MinValue ? closeMeetEndDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string closeMeetEndDateTwStr{ 
            get {
                return closeMeetEndDate != DateTime.MinValue ? (closeMeetEndDate.Year - 1911).ToString() + "/" + closeMeetEndDate.ToString("MM/dd") : "";
            }
        }
        public string guardianTitle { get; set; }
        public string familyStatus { get; set; }
        public string familyStructure { get; set; }
        public string partChildRelasp { get; set; }
        public string pclmaOtherDeviaBehavior { get; set; }
        public string pclmaMentalState { get; set; }
        public string pclmaLivingHabit { get; set; }
        public string personTraits { get; set; }
        public string thrStuRelasp { get; set; }
        public string peesRelasp { get; set; }
        public string learningStatus { get; set; }
        public string coachingStatus { get; set; }
        public string drugsEventType { get; set; }
        public string drugsLevel { get; set; }
        public string drugsValue { get; set; }
        public string drugsCode { get; set; }
        public string otherDrugs { get; set; }
        public string drugsOrigin { get; set; }
        public string useReason { get; set; }
        public string isPoliceSeized { get; set; }
        public string isPoliceSeizedStr { get; set; }
        public string counselingPeriod{ get { return counselingBeginDateTwStr + "~" + counselingEndDateTwStr; } }

        public string stuBirth { get; set; }

        public string eduInfo { get; set; }
        public string guardianTitleStr { get; set; }
        public string familyStatusStr { get; set; }
        public string familyStructureStr { get; set; }
        public string partChildRelaspStr { get; set; }
        public string pclmaOtherDeviaBehaviorStr { get; set; }
        public string pclmaMentalStateStr { get; set; }
        public string pclmaLivingHabitStr { get; set; }
        public string personTraitsStr { get; set; }
        public string thrStuRelaspStr { get; set; }
        public string peesRelaspStr { get; set; }
        public string learningStatusStr { get; set; }
        public string coachingStatusStr { get; set; }
        public string drugsEventTypeStr { get; set; }
        public string drugsLevelStr { get; set; }
        public string drugsValueStr { get; set; }
        public string drugsCodeStr { get; set; }
        public string otherDrugsStr { get; set; }
        public string drugsOriginStr { get; set; }
        public string useReasonStr { get; set; }
        public string birthDayStart { get; set; }
        public string birthDayEnd { get; set; }
    }
}
