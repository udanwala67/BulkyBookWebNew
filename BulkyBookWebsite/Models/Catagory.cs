using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWebsite.Models
{
    public partial class Catagory
    {
      
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
      
        [Display(Name ="Dispaly Order")]
        [Range(1,100)]
        public int DispalyOrder { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
