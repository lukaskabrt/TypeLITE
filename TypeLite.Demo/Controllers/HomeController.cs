using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TypeLite.Demo.Models;

namespace TypeLite.Demo.Controllers {
	public class HomeController : Controller {
		//
		// GET: /Home/

		public ActionResult Index() {
			var builder = new TsModelBuilder();
			builder.Add<Customer>();
				var model = builder.Build();

			var generator = new TsGenerator();
			var script = generator.Generate(model);
			return View();
		}

	}
}
