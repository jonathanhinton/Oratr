using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oratr.Models;
using System.Data.Entity;

namespace Oratr.DAL
{
    public class OratrRepository
    {
        public OratrContext context { get; set; }
        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

        public OratrRepository()
        {
            context = new OratrContext();
        }

        public OratrRepository(OratrContext _context)
        {
            context = _context;
        }
    }
}