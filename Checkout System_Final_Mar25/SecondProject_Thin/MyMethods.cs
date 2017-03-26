using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyClassLibrary.Lib
{
    public class MyMethods
    {
        public static void checkFileExists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("There is an issue with the path of the file you provided: \n" + fileName);
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        //The following three strings format the string to be outputed for each of the three possible discount types
        public static string promoPrintString(decimal promoTotal, decimal regularTotal, int qty, decimal promoCost)
        {
            return String.Format("; Applied Promo @ {0} for {1} - Cost: {2} (Savings: {3})", qty, promoCost.ToString("C2"),
                                                    promoTotal.ToString("C2"), (regularTotal - promoTotal).ToString("C2"));
        }

        public static string dicountPrintString(decimal discountedTotal, decimal regularTotal, decimal discountPrice)
        {
            return String.Format("; Applied Discount @ {0} each, Cost: {1} (Savings: {2})", discountPrice, discountedTotal.ToString("C2"), (regularTotal - discountedTotal).ToString("C2"));
        }

        public static string bogoPrintString(decimal buyOneGetOneTotal, decimal regularTotal, int bogoQty, decimal percentOff)
        {
            if (percentOff == 100)
            {
                return String.Format("; Applied buy {0} get 1 free,  Cost: {1} (Savings: {2})", bogoQty, buyOneGetOneTotal.ToString("C2"), (regularTotal - buyOneGetOneTotal).ToString("C2"));

            }
            return String.Format("; Applied buy {0} get {1}% off the next one,  Cost: {2} (Savings: {3})", bogoQty, percentOff, buyOneGetOneTotal.ToString("C2"), (regularTotal - buyOneGetOneTotal).ToString("C2"));
        }

        //Validates that the string to be parsed is numeric
        public static void validateNumeric(string strInput, int lineCount, string entryCount, string fileName)
        {
            decimal n;
            bool isNumeric = decimal.TryParse(strInput, out n);
            if (!isNumeric)
            {
                Console.WriteLine("Data validation error. Please check line {0}, at the {1} of the {2}.", lineCount, entryCount, fileName);
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        //Check the length of the array of strings matches the correct length, which should be 7 elements in thhis case
        public static void checkArrayLength(string[] givenArray, int lineCount, int expectedArrayLength, string inputFile)
        {
            if (givenArray.Length != expectedArrayLength)
            {
                Console.WriteLine("Incorrect number of elements in line {0} of the {2}. \nThere should be {1} per line.", lineCount, expectedArrayLength, inputFile);
                Console.WriteLine("There was {0} elements instead. Please close window and correct the info in the {1}.", givenArray.Length, inputFile);
                Console.ReadKey();
                Environment.Exit(1);
            }
        }


        //Find the minimum non-zero value of three supplied numbers. Used in case the user has supplied more than one discount type. 
        public static decimal findMin(decimal num1, decimal num2, decimal num3)
        {
            if (num1 == 0)
            {
                return Math.Min(num2, num3);
            }
            else if (num2 == 0)
            {
                return Math.Min(num1, num3);
            }
            else if (num3 == 0)
            {
                return Math.Min(num1, num2);
            }
            else
            {
                return (Math.Min(num1, Math.Min(num2, num3)));
            }
        }

        //Check if the discounted cost is greater than the regular cost. If so prompt the user to make corrections
        public static void compareDiscountToRegTotal(decimal discountTotal, decimal regularTotal, string itemName)
        {
            if (discountTotal > regularTotal)
            {
                Console.WriteLine("\nFor {0}, applying the promo deal results in a higher than regular cost.", itemName);
                Console.WriteLine("Please check and update the price catalog for this item. Press any key to exit now.");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }


        public static void checkForMatches(List <ItemDetails> itemsGrouped, List<PriceOfItems> listOfPrices, List<Promotions> promotionsAdvertised)
        {

            bool hasMatch = itemsGrouped.Select(x => x.itemName)
                          .Intersect(listOfPrices.Select(x => x.itemName))
                          .Any();
            if (hasMatch)
            {
                Console.WriteLine("There's a match");
            }


            bool itemMatched = false;

            foreach (var itemDetail in itemsGrouped)
            {
                itemMatched = false;
                foreach (var itemInCatalog in listOfPrices)
                {
                    if (itemDetail.itemName == itemInCatalog.itemName)
                    {
                        itemMatched = true;
                        itemDetail.itemPrice = itemInCatalog.itemPrice;
                    }

                }
                if (itemMatched == false)
                {
                    Console.WriteLine("The item: {0} is not in the catalog. Please see cashier for assistance.", itemDetail.itemName);
                    Console.ReadKey();
                    Environment.Exit(1);
                }

                foreach (var promoItem in promotionsAdvertised)
                {
                    if (itemDetail.itemName == promoItem.itemName)
                    {
                        if (itemDetail.itemName == promoItem.itemName)
                        {
                            itemDetail.discountedPrice = promoItem.discountedPrice;
                            itemDetail.promoPrice = promoItem.promoPrice;
                            itemDetail.promoQty = promoItem.promoQty;
                            itemDetail.bogoQty = promoItem.bogoQty;
                            itemDetail.percentOff = promoItem.percentOff;
                        }
                    }
                }
            }

        }

        public static void addPrices(string[] lines, List<PriceOfItems> listOfPrices)
        {
            int countLines = 0;

            foreach (string line in lines)
            {
                countLines++;

                string[] col = line.Split(new char[] { ',' });

                //Check that the given string array has the correct number of elements, which is 7. Exits if not correct number of elements
                MyMethods.checkArrayLength(col, countLines, 2, "PriceCatalog.txt input file");
                col[1] = Regex.Replace(col[1], "[^0-9.]", "");

                MyMethods.validateNumeric(col[1], countLines, "Regular price entry", "PriceCatalog.txt input file");
                listOfPrices.Add(new PriceOfItems(col[0], Decimal.Parse(col[1])));
            }
        }


        public static void addPromos(string[] promotionsLines, List<Promotions> promotionsAdvertised)
        {
            int countLines1 = 0;

            foreach (string line in promotionsLines)
            {
                countLines1++;

                string[] col = line.Split(new char[] { ',' });

                //Check that the given string array has the correct number of elements, which is 6. Exits if not correct number of elements
                checkArrayLength(col, countLines1, 6, "PromotionsCatalog.txt input file");
                col[1] = Regex.Replace(col[1], "[^0-9.]", "");
                col[2] = Regex.Replace(col[2], "[^0-9.]", "");
                col[3] = Regex.Replace(col[3], "[^0-9.]", "");
                col[4] = Regex.Replace(col[4], "[^0-9.]", "");
                col[5] = Regex.Replace(col[5], "[^0-9.]", "");


                validateNumeric(col[1], countLines1, "Regular price entry", "PromotionsCatalog.txt input file");
                validateNumeric(col[2], countLines1, "Discount price entry", "PromotionsCatalog.txt input file");
                validateNumeric(col[3], countLines1, "Promo Quantity entry", "PromotionsCatalog.txt input file");
                validateNumeric(col[4], countLines1, "Promo Price entry", "PromotionsCatalog.txt input file");
                validateNumeric(col[5], countLines1, "BOGO Quantity entry", "PromotionsCatalog.txt input file");

                promotionsAdvertised.Add(new Promotions(col[0], Decimal.Parse(col[1]), Convert.ToInt32(col[2]), Decimal.Parse(col[3]),
                                                                                    Convert.ToInt32(col[4]), Decimal.Parse(col[5])));

            }
        }


        ////Find the regular price total and apply any discounts and promos accordingly, and store in a list of objects
        ////The staff should select only one discount at a time per item, but in case there's two discounts then the cheaper
        ////one is applied 
        ////Go through each item, print item name, total regular cost of item, any applicable discounts/promos and savings 
        ////Add to the correct cost of each item to the cost of the basket of goods 
        //decimal totalPurchase = 0;
        //string printItemReceipt = ""; //Append to this string as necessary and display in the end
        public static void printRecipt(string outFileCustReceipt, List<ItemDetails> items)
        {
            decimal totalPurchase = 0;
            string printItemReceipt = ""; //Append to this string as necessary and display in the end

            using (StreamWriter writer = new StreamWriter(outFileCustReceipt))
            {
                foreach (var itemDetail in items)
                {
                    printItemReceipt = "";
                    itemDetail.calculateRegCost();
                    itemDetail.calculatePromoCost();
                    itemDetail.calculateDiscountCost();
                    itemDetail.calculateBogoCost();

                    compareDiscountToRegTotal(itemDetail.discountedTotal, itemDetail.regularTotal, itemDetail.itemName);
                    compareDiscountToRegTotal(itemDetail.promoTotal, itemDetail.regularTotal, itemDetail.itemName);
                    compareDiscountToRegTotal(itemDetail.buyOneGetOneTotal, itemDetail.regularTotal, itemDetail.itemName);

                    //This will always be printed
                    printItemReceipt += String.Format("{0} - Qty: {1} @ {2} = Regular Cost: {3}", itemDetail.itemName, itemDetail.itemCount, itemDetail.itemPrice.ToString("C2"), itemDetail.regularTotal.ToString("C2"));

                    if (!(itemDetail.promoApplied || itemDetail.discountApplied || itemDetail.bogoApplied))
                    {
                        totalPurchase += itemDetail.regularTotal;
                    }

                    //This block of code deals when there's more than one discount available for a given item. The cheaper discount is applied.
                    else if ((itemDetail.promoApplied && itemDetail.discountApplied) ||
                                                    (itemDetail.promoApplied && itemDetail.bogoApplied) ||
                                                    (itemDetail.bogoApplied && itemDetail.discountApplied))
                    {
                        if (findMin(itemDetail.promoTotal, itemDetail.discountedTotal, itemDetail.buyOneGetOneTotal) == itemDetail.promoTotal)
                        {
                            printItemReceipt += promoPrintString(itemDetail.promoTotal, itemDetail.regularTotal, itemDetail.promoQty, itemDetail.promoPrice);
                            totalPurchase += itemDetail.promoTotal;
                        }
                        else if (findMin(itemDetail.promoTotal, itemDetail.discountedTotal, itemDetail.buyOneGetOneTotal) == itemDetail.discountedTotal)
                        {
                            printItemReceipt += dicountPrintString(itemDetail.discountedTotal, itemDetail.regularTotal, itemDetail.discountedPrice);
                            totalPurchase += itemDetail.discountedTotal;
                        }
                        else if (findMin(itemDetail.promoTotal, itemDetail.discountedTotal, itemDetail.buyOneGetOneTotal) == itemDetail.buyOneGetOneTotal)
                        {
                            printItemReceipt += bogoPrintString(itemDetail.buyOneGetOneTotal, itemDetail.regularTotal, itemDetail.bogoQty, itemDetail.percentOff);
                            totalPurchase += itemDetail.buyOneGetOneTotal;
                        }
                    }

                    //The following three conditionals are used when only one discount is available for a give item
                    else if (itemDetail.promoApplied)
                    {
                        printItemReceipt += promoPrintString(itemDetail.promoTotal, itemDetail.regularTotal, itemDetail.promoQty, itemDetail.promoPrice);
                        totalPurchase += itemDetail.promoTotal;
                    }
                    else if (itemDetail.discountApplied)
                    {
                        printItemReceipt += dicountPrintString(itemDetail.discountedTotal, itemDetail.regularTotal, itemDetail.discountedPrice);
                        totalPurchase += itemDetail.discountedTotal;
                    }
                    else if (itemDetail.bogoApplied)
                    {
                        printItemReceipt += bogoPrintString(itemDetail.buyOneGetOneTotal, itemDetail.regularTotal, itemDetail.bogoQty, itemDetail.percentOff);
                        totalPurchase += itemDetail.buyOneGetOneTotal;
                    }

                    //After the string with details for each item is updated correctly, display it to the screen and write it to the outFile
                    Console.WriteLine(printItemReceipt);
                    writer.WriteLine(printItemReceipt);
                }

                Console.WriteLine("\nThe total due for basket of goods: {0}", totalPurchase.ToString("C2"));
                Console.WriteLine("\nPlease check a copy of the purchase receipt in the file:\n\t{0}", outFileCustReceipt);
                writer.WriteLine("\nThe total due for basket of goods: {0}", totalPurchase.ToString("C2"));
            }
        }
    }
}
