using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerkoService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerkoService.Controllers.Tests
{
    [TestClass()]
    public class ExpenseParserTest
    {

        [TestMethod]
        public void Get()
        {
            // Arrange
            ExpenseController controller = new ExpenseController();
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

            // Act
            var expense = controller.ReadText(sampleText);

            //Assert
            Assert.AreEqual("DEV002", expense.cost_centre);
            Assert.AreEqual((Decimal)1024.01, expense.total);
            Assert.AreEqual("personal card", expense.payment_method);
            Assert.AreEqual("Viaduct Steakhouse", expense.vendor);
            Assert.AreEqual(" development team’s project end celebration dinner", expense.description);
            Assert.AreEqual(new DateTime(2017, 4, 27), expense.date.Date);

        }
    }
}