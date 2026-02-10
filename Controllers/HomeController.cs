using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using AbraContentSite.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace AbraContentSite.Controllers
{
    public class HomeController : RenderController
    {
        public HomeController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override IActionResult Index()
        {
            // Create the custom view model here.
            var customModel = new HomeViewModel(CurrentPage);
            
            // Add any custom logic here, e.g., load latest articles if needed.
            
            return CurrentTemplate(customModel);
        }
    }
}