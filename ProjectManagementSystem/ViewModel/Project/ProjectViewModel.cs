using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.ViewModel.Project
{
    public class ProjectViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Proyecto"), Required]
        public string Name { get; set; }
        [Display(Name = "Codigo Interno"), Required]
        public string InternalCode { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        [Display(Name = "Status")]
        public bool Active { get; set; }
    }
}