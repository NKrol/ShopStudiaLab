﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Web.Entities.Model;

namespace Shop.Web.Entities.Repository
{
    public class PriceRepository : ExtensionRepository<Cena>
    {
        public PriceRepository(Xkom_ProjektContext db) : base(db)
        {
        }
    }
}