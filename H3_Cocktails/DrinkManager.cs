using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3_Cocktails
{
    class DrinkManager
    {
        private DrinkContext context;
        public DrinkManager(DbContext context)
        {
            try
            {
                this.context = (DrinkContext)context;
            }
            catch (FormatException)
            {
                Debug.WriteLine("Context was not able to be converted to Drink Context");
            }
        }

        public List<ViewDrink> GetAllDrinks()
        {
            using (context = new DrinkContext())
            {
                List<ViewDrink> drinks = (from drink in context.Set<Drink>()
                                          select new ViewDrink { Name = drink.Name, Liquids = drink.Liquids }).ToList();
                return drinks;
            }
        }

        /// <summary>
        /// Get a list of drinks with a specified name for either a drink, or a liquid in a drink
        /// </summary>
        /// <param name="name">The name of the liquid or drink</param>
        /// <returns></returns>
        public List<ViewDrink> SearchForDrinks(string name)
        {
            using (context = new DrinkContext())
            {

                return (from drink in context.Set<Drink>()
                        from liquid in drink.Liquids
                        where liquid.LiquidName.ToString().ToLower().Contains(name.ToLower()) || 
                        drink.Name.ToLower().Contains(name.ToLower())
                        select new ViewDrink { Name = drink.Name, Liquids = drink.Liquids }).ToList();
            }
        }

        public bool CreateDrink(Drink drink)
        {
            using (context = new DrinkContext())
            {
                try
                {
                    context.Drinks.Add(drink);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
            }
        }

        internal Drink GetDrink(string name)
        {
            using (context = new DrinkContext())
            {
                return context.Drinks.Find(name);
            }
        }

        public bool UpdateDrink(Drink drink)
        {
            using (context = new DrinkContext())
            {
                try
                {
                    Drink dbDrink = context.Drinks.Find(drink.Name);

                    dbDrink.Liquids.AddRange(drink.Liquids);
                    dbDrink.Name = drink.Name;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool DeleteDrink(string name)
        {
            using (context = new DrinkContext())
            {
                try
                {
                    Drink drink = context.Drinks.Find(name);
                    context.Drinks.Remove(drink);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
