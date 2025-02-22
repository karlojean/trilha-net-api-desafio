using System;
using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Models
{
    
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EnumStatsTask Status { get; set; }
    }
}