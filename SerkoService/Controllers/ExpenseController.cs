using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SerkoService.Models;
using SerkoService.Models.Exceptions;

namespace SerkoService.Controllers
{
    //web service to accept text, and returns an expense object or an error message
    public class ExpenseController : ApiController
    {
        [HttpPost,HttpGet]
        public Expense ReadText(string expenseText)
        {
            try
            {
                var expenseParser = new ExpenseParser();
                return expenseParser.ExtractExpenseData(expenseText);
            }
            //report back the specified errors
            catch (CustomExpenseException ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = "Error:" + ex.Message
                };
                throw new HttpResponseException(response);
            }
            //everything else, dont expose, todo: either log or notify in windows event log
            catch (Exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Internal error"),
                    ReasonPhrase = "Internal Error"
                };
                throw new HttpResponseException(response);
            }

        }
    }
}
