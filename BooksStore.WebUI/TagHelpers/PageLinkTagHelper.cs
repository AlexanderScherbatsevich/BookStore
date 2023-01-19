using Microsoft.AspNetCore.Razor.TagHelpers;
using BookStore.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public int? CategoryId { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");
            for (int i = 1; i <= PagingInfo.TotalPages; i++)
            {
                TagBuilder item = CreateTag(i, urlHelper);
                tag.InnerHtml.AppendHtml(item);
            }
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int page, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");

            if (page == this.PagingInfo.CurrentPage)
                item.AddCssClass("active");
            else
                link.Attributes["href"] = urlHelper.Action(PageAction, 
                    new { category = CategoryId, page = page });
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(page.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
