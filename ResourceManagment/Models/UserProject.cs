using System;
using System.Collections.Generic;

namespace ResourceManagment.Models
{
    public partial class UserProject
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? Projectid { get; set; }
        public int? Pcid { get; set; }
        public int? Pmid { get; set; }
        public string? Avalibiltty { get; set; }
        public double? TotalBilling { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }

        public virtual User? Pc { get; set; }
        public virtual User? Pm { get; set; }
        public virtual Project? Project { get; set; }
        public virtual User? User { get; set; }
    }
}
