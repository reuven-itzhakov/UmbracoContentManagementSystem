using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using AbraContentSite.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

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
            var allArticles = CurrentPage.DescendantsOfType("article").ToList();
            IEnumerable<IPublishedContent> filtered = allArticles;

            if (!string.IsNullOrWhiteSpace(query))
            {
                model.SearchQuery = query;
                model.HasSearched = true;

                // 3. Run the search by fetching descendants of type Article.
                // (Assuming your article doc type alias is "article")
                filtered = allArticles
                    .Where(x => (x.Name)
                        .Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            const int pageSize = 5;
            int pageNumber = 1;
            string pageValue = HttpContext.Request.Query["page"];
            if (int.TryParse(pageValue, out int parsedPage) && parsedPage > 0)
            {
                pageNumber = parsedPage;
            }

            int totalItems = filtered.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (totalPages > 0 && pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            model.SearchResults = filtered
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.PageNumber = pageNumber;
            model.PageSize = pageSize;
            model.TotalItems = totalItems;
            model.TotalPages = totalPages;

            // 4. Return the view with the custom model (as required by the task).
            return CurrentTemplate(model);
        }
    }
}