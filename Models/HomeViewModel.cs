// View model for the Home page search and pagination.
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Models;

namespace AbraContentSite.Models
{
    public class HomeViewModel : ContentModel
    {
        public HomeViewModel(IPublishedContent content) : base(content) { }

        public string SearchQuery { get; set; }
        public IEnumerable<IPublishedContent> SearchResults { get; set; }
        public bool HasSearched { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}