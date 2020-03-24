using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogBlog.Models
{
    public partial class BlogTable
    {
        public int BlogId { get; set; }
        [Required(ErrorMessage = "Enter a headline")]
        [StringLength(50, ErrorMessage = "{0} Can not have more than {1} characters")]
        [Display(Name = "Title")]
        public string BlogHeadline { get; set; }

        [Required(ErrorMessage = "Enter content")]
        [StringLength(2000, ErrorMessage = "{0} Can not have more than {1} characters")]
        [Display(Name = "Content")]
        public string BlogEntry { get; set; }
        [Display(Name = "Date and Time")]
        public DateTime BlogDateTime { get; set; }
        [Display(Name = "Category")]
        public int CatId { get; set; }
        [Display(Name = "Category")]
        public virtual Categories Cat { get; set; }
    }
}
