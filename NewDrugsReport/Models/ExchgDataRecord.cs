using System;
namespace NewDrugs.Models
{
    public class ExchgDataRecord
    {
        public string idn { get; set; }
        public DateTime exchgDataDate { get; set; }
        public string exchgDataDateStr { 
            get{
                return exchgDataDate.ToString("yyyy/MM/dd HH:mm:ss");
            } 
        }
        public string status { get; set; }
        public string msg { get; set; }
    }
}
