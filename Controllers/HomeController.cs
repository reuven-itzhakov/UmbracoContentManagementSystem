using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using AbraContentSite.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace AbraContentSite.Controllers
{
    public class HomeController : RenderController
    {
        public HomeController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        // This action runs automatically when Umbraco loads a "Home Page" document.
        public override IActionResult Index()
        {
            // 1. Create the view model.
            var model = new HomeViewModel(CurrentPage)
            {
                SearchResults = Enumerable.Empty<IPublishedContent>(),
                HasSearched = false
            };

            // 2. Check for a search query parameter (e.g., 'q').
            string query = HttpContext.Request.Query["q"];

            if (!string.IsNullOrWhiteSpace(query))
            {
                model.SearchQuery = query;
                model.HasSearched = true;

                // 3. Run the search by fetching descendants of type Article.
                // (Assuming your article doc type alias is "article")
                model.SearchResults = CurrentPage.DescendantsOfType("article")
                                        .Where(x => x.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                                        .ToList();
            }

            // 4. Return the view with the custom model (as required by the task).
            return CurrentTemplate(model);
        }
    }
}