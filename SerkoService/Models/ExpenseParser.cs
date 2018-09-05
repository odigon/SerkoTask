using System;
using SerkoService.Models.Exceptions;
using System.Globalization;

namespace SerkoService.Models
{
    //class that handles parsing the incoming text string

    public class ExpenseParser : IExpenseParser
    {
        //this is public so its defaulted but can be injected, if we want to replace it
        public IValueParser valueParser = new XMLValueParser();

        public ExpenseParser()
        {

        }


        public Expense ExtractExpenseData(string RawExpenseText)
        {
            var Expense = new Expense();

            try
            {
                //find each of the known xml tags.  Note the <expense> tag which is parent to 2 other tags
                //does not seem to be needed so its ignored

                //field names are literals, could use reflection to get class field names and types

                valueParser.rawText = RawExpenseText;
                Expense.cost_centre = valueParser.ValueFinder("cost_centre", false);
                Expense.payment_method = valueParser.ValueFinder("payment_method", false);
                Expense.vendor = valueParser.ValueFinder("vendor", false);
                Expense.description = valueParser.ValueFinder("description", false);

                // in the real world we might have to just use string for this field if the format isnt reliable 
                DateTime Date;
                if (DateTime.TryParseExact(valueParser.ValueFinder("date", false),
                                            "dddd dd MMMM yyyy", null, DateTimeStyles.None, out Date))
                    Expense.date = Date;
                else
                    Expense.date = DateTime.MinValue;
                
                Expense.total = Decimal.Parse(valueParser.ValueFinder("total", true));
            }
            // any unclosed tag is a fatal error
            catch (NoClosingTagException)
            {
                throw;
            }

            //all fields are optional except for total, which is the only one to raise this exception
            catch (NoOpeningTagException)
            {
                throw;
            }

            return Expense;
        }
    }
}
