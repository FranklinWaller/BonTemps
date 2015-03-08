using BonTemps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonTemps.Services
{
	public class PersonService
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public Person GetPerson(string email)
		{
			return db.Persons.FirstOrDefault(_ => _.Email == email);
		}

		public void UpdatePerson(Person person)
		{
			db.Entry(person).State = System.Data.Entity.EntityState.Modified;
		}
	}
}