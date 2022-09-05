using System.ComponentModel.DataAnnotations;
using System;

namespace TodoListAPIs.Models.Dtos
{
    public class SubListData

    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "DueDate is required")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Priority is required")]
        public string Priority { get; set; }

        public Guid MainListId { get; set; }
        public MainList MainList { get; set; }

        
    }
}
