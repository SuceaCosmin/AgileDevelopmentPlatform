using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgileDevelopmentPlatform.Dtos
{
    public class ProjectDto
    {
        public ProjectDto() { }

        public ProjectDto(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}