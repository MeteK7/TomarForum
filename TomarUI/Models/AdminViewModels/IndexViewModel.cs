using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarData.Models;

namespace TomarUI.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
