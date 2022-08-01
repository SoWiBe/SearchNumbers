using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchNumbers.MVVM.Models
{
    public class Advertisement
    {
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public List<Category> Categories { get; set; }
        public string Link { get; set; }
        public string DatePublished { get; set; }
    }
}
