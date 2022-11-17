using Microsoft.AspNetCore.Razor.TagHelpers;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.TagHelpers
{
    public class PagLinksTagHelper : TagHelper
    {
        //public PagingInfo PagingInfo { get; set; }
        public int Page { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //for (int i = 1; i <= PagingInfo.TotalPages; i++)
            //{
            //    output.TagName = "a";
            //    output.Attributes.SetAttribute("href", i.ToString());
            //    output.Content.SetContent(i.ToString());
            //}
            output.TagName = "a";
            output.Attributes.SetAttribute("href", Page.ToString());
            output.Content.SetContent(Page.ToString());
        }
    }
}
