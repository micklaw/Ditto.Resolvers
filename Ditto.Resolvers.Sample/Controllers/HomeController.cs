using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ditto.Resolvers.Sample.Models;
using Ditto.Resolvers.Sample.Models.DocTypes;
using Our.Umbraco.Ditto;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Ditto.Resolvers.Sample.Models.DocTypes.Composition;

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

            var poco = model.As<TestClass>();

            viewModel.Content = poco.Homepage;

            return CurrentTemplate(viewModel);
        }
    }
}
