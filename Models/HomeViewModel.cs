using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Models;

namespace AbraContentSite.Models
{
    // יורש מ-ContentModel כדי לשמור על הפונקציונליות של אומברקו
    public class HomeViewModel : ContentModel
    {
        public HomeViewModel(IPublishedContent content) : base(content) { }

        // המאפיינים לחיפוש
        public string SearchQuery { get; set; }
        public IEnumerable<IPublishedContent> SearchResults { get; set; }
        public bool HasSearched { get; set; }
    }
}