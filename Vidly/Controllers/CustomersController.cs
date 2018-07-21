using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
	public class CustomersController : Controller
	{
		private ApplicationDbContext _context;

		public CustomersController() {
			_context = new ApplicationDbContext();
		}


		// GET: Customers
		public ActionResult Index()
		{
			var customers = _context.Customers.Include(c => c.MembershipType).ToList();
			return View(customers);
		}

		[Route("Customers/Details/{id}")]
		public ActionResult GetDetail(int id) {
			var customer = _context.Customers.Where(x => x.Id == id).FirstOrDefault();
			if(customer == null) {
				return new HttpNotFoundResult();
			}

			return View("Detail", customer);
		}

		protected override void Dispose(bool disposing) {
			_context.Dispose();
		}
	}
}