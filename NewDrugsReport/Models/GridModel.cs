using System;
namespace NewDrugs.Models
{
    public class GridModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totel { get; set; }
        public int rowNum { get; set; }
        public dynamic rows { get; set; }
    }
}
