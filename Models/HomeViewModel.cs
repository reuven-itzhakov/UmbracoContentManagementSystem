using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Models;

namespace AbraContentSite.Models
{
    // Inherit from ContentModel to keep Umbraco functionality.
    public class HomeViewModel : ContentModel
    {
        public HomeViewModel(IPublishedContent content) : base(content) { }

        // Search properties.
        public string SearchQuery { get; set; }
        public IEnumerable<IPublishedContent> SearchResults { get; set; }
        public bool HasSearched { get; set; }

        // Paging metadata.
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}