using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Frontend.Views
{
    public class ToDoItemView
    {
        public int ToDoItemId { get; set; }
        public required string Name { get; set; }
        [Required(ErrorMessage = "Name is mandatory.")]
        [StringLength(50, MinimumLength = 3)]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Description is mandatory.")]
        [StringLength(250)]
        public bool IsCompleted { get; set; }

        public string? Category { get; set; }
    }


}
