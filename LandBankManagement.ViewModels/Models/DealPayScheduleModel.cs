using System;
using System.Collections.Generic;
using System.Text;

namespace LandBankManagement.Models
{
    public class DealPayScheduleModel : ObservableObject
    {
        public int DealPayScheduleId { get; set; }
        public int DealId { get; set; }
        public DateTimeOffset ScheduleDate { get; set; }
        public string Description { get; set; }
        public string Amount1 { get; set; }
        public string Amount2 { get; set; }
        public string Total { get; set; }
        public int Identity { get; set; }
    }
}
