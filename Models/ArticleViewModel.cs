using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace AbraContentSite.Models
{
    public sealed class ArticleViewModel : ContentModel
    {
        public ArticleViewModel(IPublishedContent content)
            : base(content)
        {
        }

        public string Title => Content.Value<string>("title") ?? Content.Name;
        public string? ContentHtml => Content.Value<string>("content");
        public string? AuthorName => Content.Value<string>("authorName");
        public IPublishedContent? HeroImage => Content.Value<IPublishedContent>("heroImage");
    }
}
