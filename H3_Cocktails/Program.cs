using H3_Cocktails.DbContexts;
using System;
using System.Collections.Generic;

namespace H3_Cocktails
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var drinkContext = new DrinkContext())
            {
                Liquid freshCream = new Liquid(LiquidType.Fresh_Cream, 30);
                Liquid kahlua = new Liquid(LiquidType.Kahlua, 30);
                Liquid vodka = new Liquid(LiquidType.Vodka, 90);
                List<Liquid> whiteRussianLiquids = new List<Liquid>
                {
                    freshCream,
                    kahlua,
                    vodka
                };

                Drink whiteRussian = new Drink("White Russian", whiteRussianLiquids);

                drinkContext.Liquids.AddRange(whiteRussianLiquids);
                drinkContext.Drinks.Add(whiteRussian);
                drinkContext.SaveChanges();
            }

        }
    }
}
