﻿using System.Web;
using System.Web.Mvc;

namespace Vidly {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());

			// Apply Authorization required by default
			filters.Add(new AuthorizeAttribute());

			filters.Add(new RequireHttpsAttribute());
		}
	}
}
