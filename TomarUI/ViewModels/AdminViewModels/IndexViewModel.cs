using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomarDAL.Entities;

namespace TomarUI.ViewModels.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
