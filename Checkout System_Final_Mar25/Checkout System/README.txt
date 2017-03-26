--Checkout kiosk system for GroceryCo--

The format for the price catalog and applicable discounts uses CSV values as follows:
Item, Item Price

The format for the promo catalog file is 6 comma seperated values:
Item, Discount Price, Specific Quantity Reached, Price when it reaches that Quantity, Specific Quantity, Percent off for the next item 
An Example with descriptions:
Apple, 0.9, 3, 2.5, 2, 50
There's a discount now and an apple costs $0.90
There's a promo of buy 3 apples for $2.50
There's a BOGO offer of buy 2 apples and receive the third at 50% off (if it was 100 it would mean that the next item is free, or 100% Off)

There should be 6 comma separated values per each line in the promo catalog file. Characters and words are allowed as there is function to trim non numeric
characters. But there has to be a numeric value, otherwise the user will receive a prompt to update the line where the data validation fails.

There is a number of other data validation functions:
If the sale price is more than the regular price the user will get a prompt
If an item has three available sale types, then the cheapest one is chosen for the customer

Ideally an entry in the promo catalog would be: 
Apple, $0.75, 0, $0.00, 0, 0%


But the following works as well as the extra characters are removed automatically:
Apple, sale $0.75, buy 3, for $2.00, buy 1, get the next 50% off

The GroceryCo staff need to to make sure that the file paths in the App.config file are updated for:

Items File
PriceCatalog File
Receipt File(The Path where the file with the receipt of the purchase is to be saved)
Promotions File

These should be updated before the program runs, otherwise the user will get a prompt that there's an issue with the file.

The program goes through the list of items purchased and counts how many there are of each item. 
It then checks to see if there's a match with an item in the price catalog. If there isn't then the user will get a prompt to check the price catalog file.

