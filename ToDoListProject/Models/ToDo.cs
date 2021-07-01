using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListProject.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Description { set; get; }
        public bool IsDone { set; get; }
        public DateTime DeadLine { set; get; }
        public DateTime Created { set; get; }

        public virtual IdentityUser User { set; get; }
    }
}
