using System;
using System.Collections.Generic;

namespace H3_Cocktails
{
    class Program
    {
        static DrinkManager drinkManager = new DrinkManager(new DrinkContext());
        static void Main(string[] args)
        {
            bool runProgram = true;
            while (runProgram)
            {
                Console.Clear();
                Console.WriteLine("Please choose a number\n" +
                    "1. Create a new drink\n" +
                    "2. Search for drinks\n" +
                    "3. Delete a drink\n" +
                    "4. Get all drinks\n" +
                    "5. Update a drink\n" +
                    "6. Close the program");

                int input = 0;
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a valid number");
                }
                //Show the designated menu
                switch (input)
                {
                    case 1:
                        CreateDrinkMenu();
                        break;
                    case 2:
                        SearchMenu();
                        break;
                    case 3:
                        DeleteMenu();
                        break;
                    case 4:
                        GetAllDrinks();
                        break;
                    case 5:
                        UpdateDrinkMenu();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Goodbye\n");
                        runProgram = false;
                        break;
                    default:
                        break;
                }
                Console.WriteLine("\nPress enter to continue...");
                Console.ReadLine();
            }

        }

        private static void UpdateDrinkMenu()
        {
            Console.WriteLine("What is the name of the drink you would like to update?");
            string input = Console.ReadLine();

            Drink drink = drinkManager.GetDrink(input);

            if (drink == null)
            {
                Console.WriteLine("No drink was found with that name\n" +
                    "Press enter to continue...");
                Console.ReadLine();
                return;
            }

            int secondInput = 0;
            Console.WriteLine("Would you like to add a liquid or accessory?\n" +
                "1. Liquid\n" +
                "2. Accessory");
            try
            {
                secondInput = int.Parse(Console.ReadLine());

                if (secondInput == 1)
                {
                    AddNewLiquid(drink);
                }
                else if (secondInput == 2)
                {
                    AddNewAccessory(drink);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private static void AddNewAccessory(Drink drink)
        {
            string accName = "";
            string accDesc = "";

            Console.Write("Name of the accessory: ");
            accName = Console.ReadLine();
            Console.WriteLine("Description of the accessory: ");
            accDesc = Console.ReadLine();

            ((AccessoryDrink)drink).AccessoryDic.Add(accName, accDesc);
            if (drinkManager.UpdateDrink(drink))
                Console.WriteLine("Success!");
            else
                Console.WriteLine("Failure!");
        }
        private static void AddNewLiquid(Drink drink)
        {
            Console.WriteLine("What liquid would you like to add?\n");
            foreach (string liquid in Enum.GetNames(typeof(LiquidType)))
            {
                Console.WriteLine(liquid.Replace("_", " "));
            }
            //Used to make a space between liquids and users next input
            Console.WriteLine();
            try
            {
                LiquidType liquidToAdd = (LiquidType)Enum.Parse(typeof(LiquidType), Console.ReadLine().Replace(" ", "_"));
                Console.WriteLine("How much do you want to add in ml");
                float amount = float.Parse(Console.ReadLine());
                drink.Liquids.Add(new Liquid(liquidToAdd, amount));

                if (drinkManager.UpdateDrink(drink))
                {
                    Console.WriteLine("True!");
                }
                else
                    Console.WriteLine("False!");

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong with an input\n");
                return;
            }
        }

        private static void CreateDrinkMenu()
        {
            Console.WriteLine("What is the name of the drink?");
            string name = Console.ReadLine().Trim();

            if (!string.IsNullOrEmpty(name))
            {
                int accDrinkOrNormal = 0;
                Console.WriteLine("Is it a normal drink or an accessory drink? 1 for yes, 2 for no");
                try
                {
                    accDrinkOrNormal = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    throw;
                }

                Drink drink = null;
                if (accDrinkOrNormal == 1)
                {
                    drink = new AccessoryDrink(name);
                }
                else
                {
                    drink = new Drink(name);
                }

                if (drinkManager.CreateDrink(drink))
                    Console.WriteLine("Success");
                else
                    Console.WriteLine("Not success");
            }
            else
            {
                Console.WriteLine("Write a valid string");
            }
        }

        private static void SearchMenu()
        {
            Console.WriteLine("What are you trying to search for?");
            string input = Console.ReadLine();

            List<Drink> drinks = drinkManager.SearchForDrinks(input);
            if (drinks.Count > 0)
            {
                ShowDrinks(drinks);
            }
            else
                Console.WriteLine("No Drinks found with " + input);

        }

        private static void DeleteMenu()
        {
            Console.WriteLine("What is the name of the drink you would like to delete?");
            string input = Console.ReadLine();
            if (drinkManager.DeleteDrink(input))
            {
                Console.WriteLine("Drink deleted successfully!");
            }
            else
                Console.WriteLine("Something went wrong!");
        }

        private static void GetAllDrinks()
        {
            List<Drink> drinks = drinkManager.GetAllDrinks();
            ShowDrinks(drinks);
        }

        private static void ShowDrinks(List<Drink> drinksToShow)
        {
            foreach (Drink drink in drinksToShow)
            {
                Console.WriteLine("\n" + drink.Name);
                foreach (Liquid liquid in drink.Liquids)
                {
                    Console.WriteLine("     " + liquid.LiquidName.ToString().Replace("_", " ") + ":" + liquid.AmountInml + " ml");
                }

                if (drink is AccessoryDrink)
                {
                    Console.WriteLine("   Accessories");
                    AccessoryDrink accDrink = (AccessoryDrink)drink;

                    foreach (KeyValuePair<string, string> kv in accDrink.AccessoryDic)
                    {
                        Console.WriteLine("     " + kv.Key + "      " + kv.Value);
                    }
                }
            }
        }
    }
}