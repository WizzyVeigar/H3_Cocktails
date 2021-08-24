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
                Drink drink = new Drink(name);

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

            List<ViewDrink> drinks = drinkManager.SearchForDrinks(input);
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
            List<ViewDrink> drinks = drinkManager.GetAllDrinks();
            ShowDrinks(drinks);
        }

        private static void ShowDrinks(List<ViewDrink> drinksToShow)
        {
            foreach (ViewDrink drink in drinksToShow)
            {
                Console.WriteLine("\n"+drink.Name);
                foreach (Liquid liquid in drink.Liquids)
                {
                    Console.WriteLine("     " + liquid.LiquidName.ToString().Replace("_", " ") + ":" + liquid.AmountInml + " ml");
                }
            }
        }
    }
}