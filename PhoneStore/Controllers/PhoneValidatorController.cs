using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;

namespace PhoneStore.Controllers
{
    public class PhoneValidatorController : Controller
    {
        private readonly MobileContext _db;

        public PhoneValidatorController(MobileContext db)
        {
            _db = db;
        }

        [AcceptVerbs("GET", "POST")]
        public bool CheckName(string name)
        {
            return !_db.Phones
                .AsEnumerable()
                .Any(p => p.Name.Equals(name, StringComparison.CurrentCulture));
        }
    }
}