using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models {
	/// <summary>
	/// Note: this will only work with server side validation
	/// </summary>
	public class Min18YearsOldDataAttribute : ValidationAttribute {
		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var customer = (Customer)validationContext.ObjectInstance;

			// OK if haven't selected yet or pay as you go
			// TODO refactor this to check against constant code (need new field in Customer model)
			// instead of checking against the Id which could change
			if(customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1) {
				return ValidationResult.Success;
			}

			// Otherwise must have entered birth date
			if(customer.DateOfBirth == null) {
				return new ValidationResult("Please enter your Date of Birth");
			}

			// Calculate age
			var now = DateTime.Now.Date;
			var yearsOld = now.Year - customer.DateOfBirth.Value.Year;
			var days = now.DayOfYear - customer.DateOfBirth.Value.DayOfYear;

			// If negative, haven't had birthday this year yet
			if(days < 0) {
				yearsOld--;
			}

			// Fail if < 18 years old
			if(yearsOld < 18) {
				return new ValidationResult("You must be over 18 years old or choose Pay as You Go Membership Type");
			}

			return ValidationResult.Success;
		}
	}
}