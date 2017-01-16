using System.Collections.Generic;

namespace PornHub_BHF
{
   public class Category
    {
        public string category { get; set; }
    }

    public class Categories
    {
        public IList<Category> categories { get; set; }
    }
}
