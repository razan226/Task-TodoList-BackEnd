using System.ComponentModel.DataAnnotations;
using System;

namespace TodoListAPIs.Models.Dtos
{
    public class MainListData
    {
         

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is needed")]
        public string Name { get; set; }
     


    }
}
