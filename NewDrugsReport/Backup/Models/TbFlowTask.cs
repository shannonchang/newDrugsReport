using System;
namespace NewDrugs.Models
{
    public class TbFlowTask
    {
        public string userId { get; set; }
        public string taskType { get; set; }
        public int taskNo { get; set; }
        public int taskNext { get; set; }
        public int taskParent { get; set; }
        public string taskTitle { get; set; }
        public string taskTitleStr { get; set; }
        public string taskUser { get; set; }
        public string taskUserName { get; set; }
        public string taskAccount { get; set; }
        public string taskAccountName { get; set; }
        public string taskUserMail { get; set; }
        public string taskUserJob { get; set; }
        public string crIp { get; set; }
        public string crUser { get; set; }
        public string upIp { get; set; }
        public string upUser { get; set; }
    }
}
