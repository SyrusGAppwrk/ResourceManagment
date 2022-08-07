using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceManagment.Models
{
    public partial class AssignTask
    {
        public AssignTask()
        {
            DailyTaskLogs = new HashSet<DailyTaskLog>();
        }

        public int Id { get; set; }
        public int? Empid { get; set; } = null;
        public int? ProjectId { get; set; }
        public int? Pcid { get; set; }
        public int? Pmid { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? Status { get; set; }

        [NotMapped]
        public virtual User? Emp { get; set; }
        [NotMapped]
        public virtual User? Pc { get; set; }
        [NotMapped]
        public virtual User? Pm { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ICollection<DailyTaskLog> DailyTaskLogs { get; set; }
    }
}
