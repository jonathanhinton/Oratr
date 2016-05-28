﻿using Oratr.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Oratr.DAL
{
    public class OratrContext : ApplicationDbContext
    {
        public virtual DbSet<Speech> Speeches { get; set; }
    }
}