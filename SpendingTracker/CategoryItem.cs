using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingTracker
{
    internal class CategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
