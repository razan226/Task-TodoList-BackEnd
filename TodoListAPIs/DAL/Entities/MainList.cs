using System.ComponentModel.DataAnnotations;
using System;

namespace TodoListAPIs.Models.Dtos
{
    public class MainList
    {
       

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool pending { get; internal set; }
    }
}
