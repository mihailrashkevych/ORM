using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ORM_work.Entities
{
    public class Faculty
    {
        [Key]
        public int FacultyId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FacultyName { get; set; }
        
        public List<Group> Groups { get; set; }
    }
}
