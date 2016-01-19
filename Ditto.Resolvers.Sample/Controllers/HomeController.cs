using System.Web.Mvc;
using Ditto.Resolvers.Sample.Models;
using Ditto.Resolvers.Sample.Models.DocTypes;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Ditto.Resolvers.Sample.Controllers
{
    public class HomeController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var viewModel = new ViewModel<Home>
            {
                Content = model.As<Home>()
            };

            var poco = model.As<TypicalPage>();

            return CurrentTemplate(viewModel);
        }
    }
}
