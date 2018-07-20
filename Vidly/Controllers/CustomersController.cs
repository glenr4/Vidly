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
		List<Customer> customers;

		public CustomersController() {
			customers = new List<Customer> {
				new Customer { Id = 1, Name = "John Smith"},
				new Customer { Id = 2, Name = "Mary Williams"}
			};
		}


		// GET: Customers
		public ActionResult Index()
		{
			return View(customers);
		}

		[Route("Customers/Details/{id}")]
		public ActionResult GetCustomerDetail(int id) {
			return View("Detail", customers.Find(x => x.Id == id));
		}
	}
}