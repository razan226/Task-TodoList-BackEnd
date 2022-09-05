﻿using System.ComponentModel.DataAnnotations;
using System;

namespace TodoListAPIs.Models.Dtos
{
    public class SubList

    {
        

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]

        public DateTime DueDate { get; set; }
        [Required]

        public string Priority { get; set; }

        public Guid MainListId{ get; set; }
        public MainList MainList { get; set; }
        public bool Status { get; set; }
        public bool pending { get; internal set; }
    }
}
