using System;
namespace NewDrugs.Models
{
    public class TbFlowEngine
    {
        public int flowSno { get; set; }
        public string userId { get; set; }
        public string taskType { get; set; }
        public int nowTask { get; set; }
        public string taskUser { get; set; }
        public string taskAccount { get; set; }
        public string taskTitle { get; set; }
        public int taskNext { get; set; }
        public string flowStatus { get; set; }
        public DateTime engineBeginDate { get; set; }
        public string engineBeginDateStr{
            get{
                return engineBeginDate != DateTime.MinValue ? engineBeginDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string engineBeginDateTwStr{
            get{
                return engineBeginDate != DateTime.MinValue ? (engineBeginDate.Year - 1911).ToString() + "/" + engineBeginDate.ToString("MM/dd") : "";
            }
        }
        public DateTime engineEndDate { get; set; }
        public string engineEndDateStr{
            get{
                return engineEndDate != DateTime.MinValue ? engineEndDate.ToString("yyyy/MM/dd") : "";
            }
        }
        public string engineEndDateTwStr{
            get{
                return engineEndDate != DateTime.MinValue ? (engineEndDate.Year - 1911).ToString() + "/" + engineEndDate.ToString("MM/dd") : "";
            }
        }
    }
}
