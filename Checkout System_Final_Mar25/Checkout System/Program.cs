/* 
 * This program is a prototype system that allows customers perform a “checkout” based on prices and promotions defined by GroceryCo
 * It takes as input a number of unsorted items in a file and finds a match in another file/price Catalog
 * Developed by Kleanthi Tupe - Mar 25, 2017
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyClassLibrary.Lib;

namespace Checkout_System
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declare file names as strings. 
            //THESE THREE FILE PATHS NEED TO BE UPDATED APPROPRIATELY IN App.config BEFORE THE PROGRAM RUNS
            string itemsInBasket = System.Configuration.ConfigurationManager.AppSettings["file1"];
            string priceCatalog = System.Configuration.ConfigurationManager.AppSettings["file2"];
            string outFileCustReceipt = System.Configuration.ConfigurationManager.AppSettings["file3"];
            string promotionsCatalog = System.Configuration.ConfigurationManager.AppSettings["file4"];

            MyMethods.checkFileExists(itemsInBasket);
            MyMethods.checkFileExists(priceCatalog);
            MyMethods.checkFileExists(promotionsCatalog);


            //Read the file with all the items that are purchased and count how many occurrences of each item
            List<ItemDetails> itemsGrouped = new List<ItemDetails>();
            List<string> itemsBought = File.ReadLines(itemsInBasket).ToList();
            var g = itemsBought.GroupBy(i => i);
            foreach (var grp in g)
            {
                itemsGrouped.Add(new ItemDetails(grp.Key, grp.Count()));
            }
      
            //Read the csv file/price catalog with all the price
            //Add new objects based on the input from the file 
            List<PriceOfItems> listOfPrices = new List<PriceOfItems>();
            string[] lines = File.ReadAllLines(priceCatalog);
            MyMethods.addPrices(lines, listOfPrices);


            //Read the csv file/promo catalog with all the price promos
            //Add new objects based on the input from the file 
            List<Promotions> promotionsAdvertised = new List<Promotions>();
            string[] promotionsLines = File.ReadAllLines(promotionsCatalog);
            MyMethods.addPromos(promotionsLines, promotionsAdvertised);


            //Go through each item and find the respective match in the list of prices. 
            MyMethods.checkForMatches(itemsGrouped, listOfPrices, promotionsAdvertised);
            MyMethods.printRecipt(outFileCustReceipt, itemsGrouped);

            Console.ReadKey();
        }
    }
}
