using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ORM_work.Entities
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        [MaxLength(31)]
        public string GroupName { get; set; }

        [Required]
        [ForeignKey("FacultyID")]
        public int FacultyId { get; set; }
        public Faculty Faculty;
        
        public List<Student> Students { get; set; }
    }
}
