using System;
namespace NewDrugs.Models
{
    public class TbStuBasicData
    {
        public int noticeSno { get; set; }
        public int csrcSerialNum { get; set; }
        public string eduDvs { get; set; }
        public string eduLevel { get; set; }
        public string eduDept { get; set; }
        public string eduGrade { get; set; }
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
        public string stuIdType { get; set; }
        public string stuIdNo { get; set; }
        public string sex { get; set; }
        public int birthYear { get; set; }
        public string birthMonth { get; set; }
        public string birthDay { get; set; }
        public int age { get; set; }
        public string blood { get; set; }
        public string areaCode { get; set; }
        public string tel { get; set; }
        public string phone { get; set; }
        public string stuPhoto { get; set; }
        public string addCity { get; set; }
        public string addTown { get; set; }
        public string addStreet { get; set; }
        public string guardianName { get; set; }
        public string guardianTitle { get; set; }
        public string guardianTitleOther { get; set; }
        public string guardianEducation { get; set; }
        public string guardianWork { get; set; }
        public string guardianTel { get; set; }
        public string familyStatus { get; set; }
        public string familyStatusOther { get; set; }
        public string familyStructure { get; set; }
        public string familyStructureOther { get; set; }
        public string partChildRelasp { get; set; }
        public string partChildRelaspOther { get; set; }
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
        public string pclmaOtherDeviaBehaviorOther { get; set; }
        public string pclmaMentalStateOther { get; set; }
        public string pclmaLivingHabitOther { get; set; }
        public string personTraitsOther { get; set; }
        public string thrStuRelaspOther { get; set; }
        public string peesRelaspOther { get; set; }
        public string learningStatusOther { get; set; }
        public string coachingStatusOther { get; set; }
        public string drugsEventTypeOther { get; set; }
        public string drugsLevelOther { get; set; }
        public string drugsValueOther { get; set; }
        public string otherDrugsOther { get; set; }
        public string drugsOriginOther { get; set; }
        public string useReasonOther { get; set; }
        public string isPoliceSeized { get; set; }
        public string isProvideInfo { get; set; }
        public string noSetupRemark { get; set; }
        public string noSetupRemarkOther { get; set; }
        public DateTime noSetupRemarkDate { get; set; }
        public string noSetupRemarkDateTwStr{
            get{
                return noSetupRemarkDate != DateTime.MinValue ? (noSetupRemarkDate.Year - 1911).ToString() + "/" + noSetupRemarkDate.ToString("MM/dd") : "";
            }
        }
        public string noSetupRemarkDateStr{
            get{
                return noSetupRemarkDate != DateTime.MinValue ? noSetupRemarkDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public DateTime crDate { get; set; }
        public string upIp { get; set; }
        public string upUser { get; set; }
        public DateTime upDate { get; set; }
    }
}
