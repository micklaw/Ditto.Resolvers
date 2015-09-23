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

namespace Ditto.Resolvers.Sample.Controllers
{
    public class TextPageController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var viewModel = new ViewModel<TextPage>
            {
                Content = model.As<TextPage>()
            };

            return CurrentTemplate(viewModel);
        }
    }
}
