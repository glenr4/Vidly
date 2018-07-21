using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
	public class CustomersController : Controller
	{
		private List<Customer> _customers = new List<Customer>();

		public CustomersController() {
			_customers.Add(new Customer { Id = 1, Name = "John Smith" });
			_customers.Add(new Customer { Id = 2, Name = "Mary Williams" });
		}


		// GET: Customers
		public ActionResult Index()
		{
			return View(_customers);
		}

		[Route("Customers/Details/{id}")]
		public ActionResult GetDetail(int id) {
			var customer = _customers.Find(x => x.Id == id);
			if(customer == null) {
				return new HttpNotFoundResult();
			}

			return View("Detail", customer);
		}
	}
}