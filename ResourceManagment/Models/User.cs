﻿using System;
using System.Collections.Generic;

namespace ResourceManagment.Models
{
    public partial class User
    {
        public User()
        {
            UserProjectPcs = new HashSet<UserProject>();
            UserProjectPms = new HashSet<UserProject>();
            UserProjectUsers = new HashSet<UserProject>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? Departmentid { get; set; }
        public int? RoleId { get; set; }
        public int? Status { get; set; }
        public int? Experience { get; set; }
        public string? Skills { get; set; }
        public string? ContactNo { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<UserProject> UserProjectPcs { get; set; }
        public virtual ICollection<UserProject> UserProjectPms { get; set; }
        public virtual ICollection<UserProject> UserProjectUsers { get; set; }
    }
}
