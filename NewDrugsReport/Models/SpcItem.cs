using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugsReport.Models
{
    public class SpcItem
    {
        public int rowNum { get; set; }
        public string school { get; set; }
        public int noticeSno { get; set; }
        public string title { get; set; }
        public string accountName { get; set; }
        public string status { get; set; }
        public int setupReason { get; set; }
        public DateTime actMeetingTime { get; set; }
        public string meeingRecord { get; set; }
        public string actIsInvite { get; set; }
        public string actIsAttend { get; set; }
        public int counselingCount { get; set; }
        public string conselingRecord { get; set; }
        public string isInspect { get; set; }
        public string contConselingReason { get; set; }
        public int contCounselingCount { get; set; }
        public string contCounselingRecord { get; set; }
        public string contIsInspect { get; set; }
        public string inspectReport { get; set; }
        public DateTime endMeetingTime { get; set; }
        public string endIsInvite { get; set; }
        public string endIsAttend { get; set; }
    }
}