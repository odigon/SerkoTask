using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SerkoService.Models;

namespace SerkoService.Controllers
{
    //web service to accept text, and returns and expense object or an error message
    public class ExpenseController : ApiController
    {
        [HttpPost,HttpGet]
        public Expense ReadText(String ExpenseText)
        {
            try
            {
                var ExpenseParser = new ExpenseParser();
                return ExpenseParser.ExtractExpenseData(ExpenseText);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = "Error:" + ex.Message
                };
                var hre = new HttpResponseException(response);
                throw hre;
            }

        }
    }
}
