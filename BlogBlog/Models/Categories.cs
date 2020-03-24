using System;
using System.Collections.Generic;

namespace BlogBlog.Models
{
    public partial class Categories
    {
        public Categories()
        {
            BlogTable = new HashSet<BlogTable>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }

        public virtual ICollection<BlogTable> BlogTable { get; set; }
    }
}
