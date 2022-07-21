using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PhoneStore.Enums;

namespace PhoneStore.DataObjects
{
    public class IdentityResult
    {
        public List<string> ErrorMessages { get; set; }
        public StatusCodes StatusCodes { get; set; }
    }
}