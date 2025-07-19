using System;
using System.Collections.Generic;

namespace program
{
    //Entry point of the program
    class Menu_Flow
    {
        static void Main()
        {
            //Dictionary to store the menu options
            Dictionary<char, string> menu = new Dictionary<char, string>()
            {
                { 'a', "Energy Calculator" },
                { 'b', "Shopping List" },
                { 'c', "Character Encoder" },
                { 'd', "End Program" }
            };
            ProductsList shoppingList = new ProductsList();
            while (true)
            {
                foreach (KeyValuePair<char, string> item in menu)
                {
                    Console.WriteLine($"{item.Key}) {item.Value}");
                }
                char input = Console.ReadKey().KeyChar;
                input = char.ToLower(input);
                switch (input) {
                    case 'a': Energy_calculator.calculator();
                        break;
                    case 'b':                        
                        shoppingList.submenu();
                        break;
                    case 'c': CharacterEncoder.character_encoder(); 
                        break;
                    case 'd': return;
                    default: Console.WriteLine("Please enter a valid input");
                        break;
                }
            }
        }
    }
    /*Test Plan for Energy Calculator
     * 1.
     * Input: Dishwasher, 1.8, 10
     * Output: Applicance Dishwasher uses: 18 kWh per day, 540 per month kWh, 6570 kWh per year
     * 2.
     * Input: Television, 1.4, 14
     * Output: Applicance Television uses: 19.6 kWh per day, 588 per month kWh, 7154 kWh per year
     * 3.
     * Input: Freezer, 1.5, 20
     * Output: Applicance Freezer uses: 30 kWh per day, 900 per month kWh, 10950 kWh per year
     * 4.
     * Input: Rice Cooker, 2, 0.8
     * Output: Applicance Rice Cooker uses: 1.6 kWh per day, 48 per month kWh, 584 kWh per year
     * 5.
     * Input: Oven, 2.7, 2
     * Output: Applicance Oven uses: 5.4 kWh per day, 162 per month kWh, 1971 kWh per year
     */
    public class Energy_calculator
    {
        //This is the main function that runs energy calculator and calls other methods to get user input, run calculations and output info
        public static void calculator()
        {
            Console.Clear();
            string applianceName = GetUserInput();
            float powerRating = GetPowerRating();
            float hoursUsed = GetHoursUsed();
            (float dailyUse, float monthlyUse, float annualUse) = Calculations(powerRating, hoursUsed);
            Output(applianceName, dailyUse, monthlyUse, annualUse);
            Console.WriteLine("Press Enter to Continue ...");
            Console.ReadLine();
            Console.Clear();
        }
        //This takes in the appliance name
        static string GetUserInput()
        {
            Console.WriteLine("Enter the name of your appliance:");
            string input;
            while (string.IsNullOrEmpty(input = Console.ReadLine()))
            {
                Console.WriteLine("Please enter a valid input:");
            }
            return input;
        }
        //This takes in the power rating of the appliance
        static float GetPowerRating()
        {
            Console.WriteLine("Enter the power rating (kWh) of your appliance:");
            string input;
            while (string.IsNullOrEmpty(input = Console.ReadLine()) || !float.TryParse(input, out float powerRating))
            {
                Console.WriteLine("Please enter a valid input:");
            }
            return float.Parse(input);
        }
        //This takes in the hours used
        static float GetHoursUsed()
        {
            while (true)
            {
                Console.WriteLine("Enter the number of hours you use your appliance per day:");
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && float.TryParse(input, out float hoursUsed))
                {
                    return hoursUsed;
                }
                Console.WriteLine("Please enter a valid input:");
            }
        }
        //This calculates the energy consumption over the different timeframes
        static (float, float, float) Calculations(float powerRating, float hoursUsed)
        {
            float dailyUse = powerRating * hoursUsed;
            float monthlyUse = dailyUse * 30;
            float annualUse = dailyUse * 365;
            return (dailyUse, monthlyUse, annualUse);
        }
        //This outputs info to the user
        static void Output(string applianceName, float dailyUse, float monthlyUse, float annualUse)
        {
            Console.WriteLine($"Applicance {applianceName} uses:\n{dailyUse} kWh per day\n{monthlyUse} per month kWh\n{annualUse} kWh per year");
        }
    }
    /*Test Plan for Shopping List
     * Note that a correct respective category was included in the input as required
     * 1.
     * Input: Apple, 0.50, 20
     * Output: Name: Apple, Category: Fruit & Vegetables, Price: 0.5, Quantity: 20
     * Total cost of items: £10
     * 2.
     * Input: Bread, 2.00, 5
     * Ouput: Name: Bread, Category: Bakery, Price: 2, Quantity: 5
     * Total cost of items: £10
     * 3.
     * Input: Scones, .25, 15
     * Ouput: Name: Scones, Category: Bakery, Price: 0.25, Quantity: 15
     * Total cost of items: £3.75
     * 4.
     * Input: Cabbage, 1.50, 5
     * Ouput: Name: Cabbage, Category: Fruit & Vegetables, Price: 1.5, Quantity: 5
     * Total cost of items: £7.5
     * 5.
     * Input: Milk, 1.40, 2
     * Ouput: Name: Milk, Category: Dairy, Price: 1.4, Quantity: 2
     * Total cost of items: £2.8
     */
    // This is a class that I made for Prodcuts to be added to the shopping list, all products are objects of this class
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public Product(string name, string category, double price, int quantity)
        {
            Name = name;
            Category = category;
            Price = price;
            Qty = quantity;
        }
    }
    //This class is the class that actually runs the shopping list
    public class ProductsList
    {
        //These dictionaries store the menu options for this mini app
        private Dictionary<char, string> subDirectory = new Dictionary<char, string>
        {
            {'a', "View the List"},
            {'b', "Add an Item" },
            {'c', "Go back" }
        };

        private Dictionary<char, string> categories = new Dictionary<char, string>
        {
            {'a', "Fruit & Vegetables"},
            {'b', "Bakery" },
            {'c', "Dairy" }
        };
        //This is *the* shopping list
        public List<Product> shoppingList = new List<Product>
        {

        };
        //This is the submenu that gives users options to view or add to the list, it's very similar to the top-level menu
        public void submenu()
        {
            Console.Clear();
            while (true)
            {
                foreach (KeyValuePair<char, string> kvp in subDirectory)
                {
                    Console.WriteLine($"{kvp.Key}) {kvp.Value}");
                }
                char input = Console.ReadKey().KeyChar;
                input = char.ToLower(input);
                subDirectory.TryGetValue(input, out string value);
                switch (input)
                {
                    case 'a':
                        ViewList();
                        break;
                    case 'b':
                        AddItem();
                        break;
                    case 'c': return;
                    default:
                        Console.WriteLine("Sorry, please enter a valid input");
                        break;
                }
            }
        }
        //This is the method that adds items to the shopping list
        public void AddItem()
        {
            Console.Clear();
            Console.WriteLine("Please enter the name of your product:");
            string name;
            while (string.IsNullOrEmpty(name = Console.ReadLine()))
            {
                Console.WriteLine("Please enter a valid input:");
            }
            //It calls this method to return a category value for the product
            string category = selectCategory(categories);
            Console.WriteLine("\nWhat is the price of your item?");
            string input_price;
            double price;
            while (string.IsNullOrEmpty(input_price = Console.ReadLine()) || !double.TryParse(input_price, out price))
            {
                Console.WriteLine("Please enter a valid input:");
            }
            Console.WriteLine("\nHow many would you like to add?");
            string input_quantity;
            int quantity;
            while (string.IsNullOrEmpty(input_quantity = Console.ReadLine()) || !int.TryParse(input_quantity, out quantity))
            {
                Console.WriteLine("Please enter a valid input:");
            }
            shoppingList.Add(new Product(name, category, price, quantity));
            Console.WriteLine($"Added {name} to the shopping list.");
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }
        //This is the method called to view the list
        public void ViewList()
        {
            Console.Clear();
            {
                Console.WriteLine("a) View all items\nb) View items by category");
                char input = Console.ReadKey().KeyChar;
                double totalCost = 0;
                switch (input)
                {
                    case 'a':
                        Console.Clear();
                        foreach (var Product in shoppingList)
                        {
                            totalCost += Product.Price * Product.Qty;
                            Console.WriteLine($"Name: {Product.Name}, Category: {Product.Category}, Price: {Product.Price}, Quantity: {Product.Qty}");
                            Console.WriteLine($"Total cost of items: £{totalCost}");
                        }
                    break;
                    case 'b':
                        //It calls this method to return a category value for the product, just like adding
                        string category = selectCategory(categories);
                        bool found = false;
                        foreach (var Product in shoppingList)
                        {
                            if (Product.Category == category)
                            {
                                totalCost += Product.Price * Product.Qty;
                                Console.WriteLine($"Name: {Product.Name}, Category: {Product.Category}, Price: {Product.Price}, Quantity: {Product.Qty}");
                                Console.WriteLine($"Total cost of items: £{totalCost}");
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine($"No items found in {category}");
                        }
                        break;
                    default:
                        Console.WriteLine("\nPlease enter a, b or c");
                        break;
                }
                Console.WriteLine("\nPress enter to continue...");
                Console.ReadLine();
            }
        }
        //This is the selct category method, it uses TryGetValue instead of switch statements so that the dictionary containing categories could be easily expanded
        public static string selectCategory(Dictionary<char, string> categories)
        {
            Console.Clear();

            while (true)
            {
                foreach (KeyValuePair<char, string> kvp in categories)
                {
                    Console.WriteLine($"{kvp.Key}) {kvp.Value}");
                }
                char input = Console.ReadKey().KeyChar;
                input = char.ToLower(input);
                if (categories.TryGetValue(input, out string value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("\nPlease enter a, b or c.");
                }
            }
        }
    }
    /*Test Plan for Character Encoder
     * 1.
     * Input: B
     * Output: 00010
     * 2.
     * Input: ABC
     * Ouput: 00001 00010 00011
     * 3.
     * Input: BB
     * Ouput: 00010 00010
     * 4.
     * Input: XYZ
     * Ouput: 11000 11001 11010
     * 5.
     * Input: MNOQPRS
     * Ouput: 01101 01110 01111 10001 10000 10010 10011
     */
    class CharacterEncoder
    {
        public static void character_encoder()
        {
            while (true)
            {
                string result;
                int value;
                string output = "";
                List<string> con = new List<string> { };
                Console.WriteLine("Please type your input (letters A-Z only):");
                string input;
                while (string.IsNullOrEmpty(input = Console.ReadLine()))
                {
                    Console.WriteLine("Please enter a valid input:");
                }
                input = input.ToLower();
                foreach (char c in input)
                {
                    try
                    {
                        value = c - 'a' + 1;
                        result = Convert.ToString(value, 2).PadLeft(5, '0');
                        con.Add(result);
                        output = String.Join(" ", con);
                    }
                    catch
                    {
                        Console.WriteLine("That didn't work");
                    }
                }
                Console.WriteLine(output);
                Console.WriteLine("\n Press enter to continue...");
                Console.ReadLine();
                break;
            }
        }
    }
}