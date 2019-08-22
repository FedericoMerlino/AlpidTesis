using Alpid.Data;
using Alpid.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpid.ModelClass
{
    public class CajaModels
    {
        private ApplicationDbContext _context;
        private List<IdentityError> errorList = new List<IdentityError>();

        public CajaModels(ApplicationDbContext context)
        {
            this._context = context;
        }
      
    }
}
