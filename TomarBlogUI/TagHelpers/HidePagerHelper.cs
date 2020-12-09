using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomarBlogUI.TagHelpers
{
    [HtmlTargetElement(Attributes ="list, count")]
    public class HidePagerHelper:TagHelper
    {
        public IEnumerable<object> List { get; set; }
        public int Count { get; set; }
        public override void Process (TagHelperContext context, TagHelperOutput output)
        {
            if (List.Count()<=Count)
            {
                output.SuppressOutput();//That means don't render it, don't show it.
            }
        }
    }
}
