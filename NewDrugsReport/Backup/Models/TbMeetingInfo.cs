using System;
namespace NewDrugs.Models
{
    public class TbMeetingInfo
    {
        public int noticeSno { get; set; }
        public string meetingType { get; set; }
        public DateTime meetingTime { get; set; }
        public string meetingTimeStr { 
            get {
                return meetingTime != DateTime.MinValue ? meetingTime.ToString("yyyy/MM/dd") : "";
            }
        }
        public string meetingTimeTwStr { 
            get {
                return meetingTime != DateTime.MinValue ? (meetingTime.Year - 1911).ToString() + "/" + meetingTime.ToString("MM/dd") : "";
            }
        }
        public string meetingPlace { get; set; }
        public string chairman { get; set; }
        public string recorder { get; set; }
        public DateTime counselingBegintime { get; set; }
        public string counselingBegintimeStr { 
            get {
                return counselingBegintime != DateTime.MinValue ? counselingBegintime.ToString("yyyy/MM/dd") : "";
            }
        }
        public string counselingBegintimeTwStr { 
            get {
                return counselingBegintime != DateTime.MinValue ? (counselingBegintime.Year - 1911).ToString() + "/" + counselingBegintime.ToString("MM/dd") : "";
            }
        }
        public DateTime counselingEndtime { get; set; }
        public string counselingEndtimeStr { 
            get {
                return counselingEndtime != DateTime.MinValue ? counselingEndtime.ToString("yyyy/MM/dd") : "";
            }
        }
        public string counselingEndtimeTwStr { 
            get {
                return counselingEndtime != DateTime.MinValue ? (counselingEndtime.Year - 1911).ToString() + "/" + counselingEndtime.ToString("MM/dd") : "";
            }
        }
        public DateTime recordTime { get; set; }
        public string recordTimeStr { 
            get {
                return recordTime != DateTime.MinValue ? recordTime.ToString("yyyy/MM/dd") : "";
            }
        }
        public string recordTimeTwStr { 
            get {
                return recordTime != DateTime.MinValue ? (recordTime.Year - 1911).ToString() + "/" + recordTime.ToString("MM/dd") : "";
            }
        }
        public string isInvite { get; set; }
        public string isInviteStr { get; set; }
        public string chMbr { get; set; }
        public string resolution { get; set; }
        public string memo { get; set; }
        public int setupReason { get; set; }
        public string setupReasonStr { get; set; }
        public int contCounselingReason { get; set; }
        public string contCounselingReasonStr { get; set; }
        public int counselingResult { get; set; }
        public string counselingResultStr { get; set; }
        public DateTime counselingResultDate { get; set; }
        public string counselingResultDateStr { 
            get {
                return counselingResultDate != DateTime.MinValue ? counselingResultDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string counselingResultDateTwStr { 
            get {
                return counselingResultDate != DateTime.MinValue ? (counselingResultDate.Year - 1911).ToString() + "/" + counselingResultDate.ToString("MM/dd") : "";
            }
        }
        public int counselingTrack { get; set; }
        public string counselingTrackStr { get; set; }
        public string toggleSchool { get; set; }
        public DateTime toggleDate { get; set; }
        public string toggleDateStr { 
            get {
                return toggleDate != DateTime.MinValue ? toggleDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string toggleDateTwStr { 
            get {
                return toggleDate != DateTime.MinValue ? (toggleDate.Year - 1911).ToString() + "/" + toggleDate.ToString("MM/dd") : "";
            }
        }
        public string inspectReport { get; set; }
        public string inspectCollection { get; set; }
        public string afterwards { get; set; }
        public string policeLetter { get; set; }
        public string checkinSlip { get; set; }
        public string meetingRecord { get; set; }
        public string otherDoc { get; set; }
        public string crIp { get; set; }
        public string crUser { get; set; }
        public DateTime crDate { get; set; }
        public string upIp { get; set; }
        public string upUser { get; set; }
        public DateTime upDate { get; set; }
    }
}
