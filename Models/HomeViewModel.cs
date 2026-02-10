using Umbraco.Cms.Core.Models.PublishedContent;

namespace AbraContentSite.Models
{
    public sealed class HomeViewModel
    {
        public HomeViewModel(IPublishedContent? currentPage)
        {
            CurrentPage = currentPage;
        }

        public IPublishedContent? CurrentPage { get; }
    }
}
