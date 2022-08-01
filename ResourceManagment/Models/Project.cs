using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceManagment.Models
{
    public partial class Project
    {
        public Project()
        {
            UserProjects = new HashSet<UserProject>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }=DateTime.Now;
        public int? Status { get; set; }
        public string? ClientName { get; set; }
        public string? Platformm { get; set; }
        public string? Tech { get; set; }
        public string? Code { get; set; }
        public string? Url { get; set; }
        public DateTime? Sdate { get; set; }
        public DateTime? Edate { get; set; }
        [NotMapped]
        public string? SrtDate { get; set; }
        [NotMapped]
        public string? EndDate { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }
    }
}
