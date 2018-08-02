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
	//[Authorize]
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
			var viewModel = new CustomerFormViewModel {
				MembershipTypes = membershipTypes,
				Customer = new Customer()
			};

			return View("CustomerForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Customer customer) {
			if (!ModelState.IsValid) {
				var viewModel = new CustomerFormViewModel {
					Customer = customer,
					MembershipTypes = _context.MembershipTypes.ToList()
				};


				return View("CustomerForm", viewModel);
			}


			if (customer.Id == 0) {
				_context.Customers.Add(customer);
			}
			else {
				var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

				//Don't use TryUpdateMode as it allows updating all fields
				// Could use AutoMapper instead of listing all 
				// Or use a DTO object which only contains the properties that are
				// allowed to be changed
				customerInDb.Name = customer.Name;
				customerInDb.DateOfBirth = customer.DateOfBirth;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
				customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
			}
			_context.SaveChanges();

			return RedirectToAction("Index", "Customers");
		}

		public ActionResult Edit(int id) {
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if(customer == null) {
				return HttpNotFound();
			}

			var viewModel = new CustomerFormViewModel {
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};

			return View("CustomerForm", viewModel);
		}

		protected override void Dispose(bool disposing) {
			_context.Dispose();
		}
	}
}