using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3_Cocktails
{
    class DrinkContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }
        //public DbSet<Liquid> Liquids { get; set; }
    }
}
