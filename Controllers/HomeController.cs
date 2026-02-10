// Handles Home page rendering, search, and pagination.
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

        public override IActionResult Index()
        {
            var model = new HomeViewModel(CurrentPage)
            {
                SearchResults = Enumerable.Empty<IPublishedContent>(),
                HasSearched = false
            };

            string query = HttpContext.Request.Query["q"];
            var allArticles = CurrentPage.DescendantsOfType("article").ToList();
            IEnumerable<IPublishedContent> filtered = allArticles;

            if (!string.IsNullOrWhiteSpace(query))
            {
                model.SearchQuery = query;
                model.HasSearched = true;

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

            return CurrentTemplate(model);
        }
    }
}