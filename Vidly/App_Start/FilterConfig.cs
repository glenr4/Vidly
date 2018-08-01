using System.Web;
using System.Web.Mvc;

namespace Vidly {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());

			// TODO: this does not work for IISExpress
			//filters.Add(new AuthorizeAttribute());
		}
	}
}
