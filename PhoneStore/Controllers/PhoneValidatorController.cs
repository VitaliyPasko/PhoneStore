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
        public bool CheckName(string name, int? id)
        {
            if (id.HasValue)
            {
                return !_db.Phones
                    .AsEnumerable()
                    .Any(p => p.Name.Equals(name, StringComparison.CurrentCulture) && p.Id != id.Value);
            }
            
            return !_db.Phones
                .AsEnumerable()
                .Any(p => p.Name.Equals(name, StringComparison.CurrentCulture));
        }
    }
}