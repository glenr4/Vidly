using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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
			var customers = _context.Customers
								.Include(c => c.MembershipType)
								.ToList();
			return View(customers);
		}

		[Route("Customers/Details/{id}")]
		public ActionResult GetDetail(int id) {
			var customer = _context.Customers
							.Where(x => x.Id == id)
							.Include(c => c.MembershipType)
							.FirstOrDefault();

			if(customer == null) {
				return new HttpNotFoundResult();
			}

			return View("Detail", customer);
		}

		public ActionResult New() {
			var membershipTypes = _context.MembershipTypes.ToList();
			var viewModel = new NewCustomerViewModel {
				MembershipTypes = membershipTypes
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(Customer customer) {
			_context.Customers.Add(customer);
			_context.SaveChanges();

			return RedirectToAction("Index", "Customers");
		}

		protected override void Dispose(bool disposing) {
			_context.Dispose();
		}
	}
}