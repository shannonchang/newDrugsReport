using System;
namespace NewDrugs.Models
{
    public class TbFlowSignRecord
    {
        public int rowId { get; set; }
        public int flowSno { get; set; }
        public string userId { get; set; }
        public string taskType { get; set; }
        public int taskNo { get; set; }
        public string taskTitle { get; set; }
        public string taskTitleStr { get; set; }
        public string taskUser { get; set; }
        public string taskUserName { get; set; }
        public string taskAccount { get; set; }
        public string taskAccountName { get; set; }
        public string taskResult { get; set; }
        public string taskResultStr { get; set; }
        public string taskReason { get; set; }
        public DateTime taskSignDate { get; set; }
        public string taskSignDateStr {
            get{
                return taskSignDate != DateTime.MinValue ? taskSignDate.ToString("yyyy/MM/dd HH:mm:ss") : "";
            }
        }
        public string taskSignDateTwStr {
            get{
                return taskSignDate != DateTime.MinValue ? (taskSignDate.Year - 1911).ToString() + "/" + taskSignDate.ToString("MM/dd HH:mm:ss") : "";
            }
        }
    }
}
