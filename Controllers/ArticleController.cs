// Handles Article page rendering.
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using AbraContentSite.Models;

namespace AbraContentSite.Controllers
{
    public sealed class ArticleController : RenderController
    {
        public ArticleController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override IActionResult Index()
        {
            var model = new ArticleViewModel(CurrentPage);
            return CurrentTemplate(model);
        }
    }
}
