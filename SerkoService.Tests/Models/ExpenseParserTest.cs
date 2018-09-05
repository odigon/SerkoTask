using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerkoService.Models;
using SerkoService.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerkoService.Tests.Models
{
    [TestClass]
    public class ExpenseParserTest
    {
        [TestMethod]
        public void SampleData()
        {
            string sampleText =
@"Hi Yvaine,
Please create an expense claim for the below.Relevant details are marked up as
requested…
<expense><cost_centre>DEV002</cost_centre>
<total>1024.01</total><payment_method>personal card</payment_method>
</expense>
From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd @example.com>
Subject: test
Hi Antoine,
Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our
<description> development team’s project end celebration dinner</description> on
<date>Thursday 27 April 2017</date>. We expect to arrive around
7.15pm.Approximately 12 people but I’ll confirm exact numbers closer to the day.
Regards,
Ivan";
            var ExpenseParser = new ExpenseParser();
            var Expense = ExpenseParser.ExtractExpenseData(sampleText);

            Assert.AreEqual("DEV002", Expense.cost_centre);
            Assert.AreEqual((Decimal)1024.01, Expense.total);
            Assert.AreEqual("personal card", Expense.payment_method);
            Assert.AreEqual("Viaduct Steakhouse", Expense.vendor);
            Assert.AreEqual(" development team’s project end celebration dinner", Expense.description);
            Assert.AreEqual(new DateTime(2017, 4, 27), Expense.date.Date);
        }

        [TestMethod]
        public void MissingTotal()
        {
            var ExpenseParser = new ExpenseParser();


            string MissingTotalText =
@"Hi Yvaine,
Please create an expense claim for the below.Relevant details are marked up as
requested…
<expense><cost_centre>DEV002</cost_centre>
<payment_method>personal card</payment_method>
</expense>";
            try
            {
                var Expense = ExpenseParser.ExtractExpenseData(MissingTotalText);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NoOpeningTagException), ex.GetType());
                Assert.AreEqual("No Opening Tag for: total", ex.Message);
            }
        }

        [TestMethod]
        public void MissingClosingTag()
        {
            var ExpenseParser = new ExpenseParser();


            string MissingTotalText =
@"Hi Yvaine,
Please create an expense claim for the below.Relevant details are marked up as
requested…
<expense><cost_centre>DEV002</cost_centre>
<payment_method>personal card</payment_method>
</expense><total>100.00</total> xx <date> zbc ";
            try
            {
                var Expense = ExpenseParser.ExtractExpenseData(MissingTotalText);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NoClosingTagException), ex.GetType());
                Assert.AreEqual("No Closing Tag for: date", ex.Message);
            }
        }


        [TestMethod]
        public void UnknownCostCentre()
        {
            var ExpenseParser = new ExpenseParser();
            string UnknownCostCentre =
@"Hi Yvaine,
Please create an expense claim for the below.Relevant details are marked up as
requested…
<expense>
<payment_method>personal card</payment_method>
</expense><total>100.00</total> xxzbc ";
            var Expense = ExpenseParser.ExtractExpenseData(UnknownCostCentre);
            Assert.AreEqual("UNKNOWN", Expense.cost_centre);
        }
    }
}


