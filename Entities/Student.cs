using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ORM_work.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int AverageRaiting { get; set; }

        [Required]
        [ForeignKey("GroupID")]
        public int GroupID { get; set; }
        public Group Group;
    }
}
