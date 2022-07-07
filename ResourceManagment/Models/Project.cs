using System;
using System.Collections.Generic;

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
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }
    }
}
