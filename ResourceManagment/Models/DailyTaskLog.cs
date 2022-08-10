using System;
using System.Collections.Generic;

namespace ResourceManagment.Models
{
    public partial class DailyTaskLog
    {
        public int Id { get; set; }
        public int? AssignTaskId { get; set; }
        public string? Avalibiltty { get; set; }
        public double? BillingHour { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public int? Status { get; set; }

        public virtual AssignTask? AssignTask { get; set; }
    }
}
