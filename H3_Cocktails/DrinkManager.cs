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

        public List<Drink> GetAllDrinks()
        {
            using (context = new DrinkContext())
            {
                List<Drink> drinkList = new List<Drink>();
                foreach (Drink drink in context.Drinks.Include(drink => drink.Liquids))
                {
                    drinkList.Add(drink);
                }

                foreach (AccessoryDrink accDrink in context.Drinks
                    .Include(drink => drink.Liquids)
                    .Include(drink => ((AccessoryDrink)drink).AccessoryDic))
                {
                    drinkList.Add(accDrink);
                }
                //List<ViewDrink> drinks = (from drink in context.Set<Drink>()
                //                          select new ViewDrink { Name = drink.Name, Liquids = drink.Liquids }).ToList();
                //List<Drink> drinks = context.set
                return drinkList;
            }
        }

        /// <summary>
        /// Get a list of drinks with a specified name for either a drink, or a liquid in a drink
        /// </summary>
        /// <param name="name">The name of the liquid or drink</param>
        /// <returns></returns>
        public List<Drink> SearchForDrinks(string name)
        {
            using (context = new DrinkContext())
            {
                return (List<Drink>)(from drink in context.Drinks
                                     from liquid in drink.Liquids
                                     where liquid.LiquidName.ToString().ToLower().Contains(name.ToLower()) ||
                                     drink.Name.ToLower().Contains(name.ToLower())
                                     select drink);
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

                    if (drink is AccessoryDrink)
                    {
                        AccessoryDrink accDrink = (AccessoryDrink)drink;
                        for (int i = 0; i < accDrink.AccessoryDic.Count; i++)
                        {
                            ((AccessoryDrink)dbDrink).AccessoryDic.Add(accDrink.AccessoryDic.Keys.ElementAt(i),
                                accDrink.AccessoryDic.Values.ElementAt(i));
                        }
                    }
                    dbDrink.Liquids.AddRange(drink.Liquids);
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
