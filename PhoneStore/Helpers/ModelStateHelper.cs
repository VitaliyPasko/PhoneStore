using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PhoneStore.Helpers
{
    public static class ModelStateHelper
    {
        public static void AddErrors(this ModelStateDictionary self, List<string> errors)
        {
            errors.ForEach(e =>
            {
                self.AddModelError(string.Empty, e);
            });
        }
    }
}